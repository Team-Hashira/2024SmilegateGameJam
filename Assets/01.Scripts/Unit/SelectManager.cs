using Crogen.PowerfulInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager : MonoSingleton<SelectManager>
{
    private List<ISelectable> _selectableList;
    [SerializeField]
    private InputReader _inputReader;
    [SerializeField]
    private Transform _selectVisualizer;
    [SerializeField]
    private LayerMask _whatIsSeletable;
    [SerializeField]
    private LayerMask _whatIsEnemyUnit;

    private bool _isHolding = false;

    private Vector2 _startPos;

    private Collider2D[] _colliders;

    private void Awake()
    {
        _selectableList = new List<ISelectable>();
        _colliders = new Collider2D[50];

        _inputReader.OnLeftMouseClickEvent += HandleOnMouseClickEvent;
        _inputReader.OnRightMouseClickEvent += HandleOnRightMouseDownEvent;
        _inputReader.OnMouseMoveEvent += HandleOnMouseMoveEvent;
    }

    private void HandleOnRightMouseDownEvent(bool isClicked, Vector2 mousePos)
    {
        if (_isHolding) return;
        if (isClicked)
        {
            if (_selectableList.Count > 0 && _selectableList[0].SeletableType == ESeletableType.Unit)
            {
                foreach (ISelectable seletable in _selectableList)
                {
                    EnemyUnit target = null;
                    Collider2D collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(mousePos), _whatIsEnemyUnit);
                    if (collider != null)
                    {
                        Debug.Log(collider.name);
                        if (collider.TryGetComponent(out EnemyUnit enemyUnit))
                        {
                            target = enemyUnit;
                        }
                    }
                    if (seletable is AllyUnit unit)
                    {
                        if (target != null)
                        {
                            unit.target = target.transform;
                            unit.StateMachine.ChangeState(EAllyUnitState.Chase);
                        }
                        else
                        {
                            unit.GetCompo<UnitMovement>().SetDestination(Camera.main.ScreenToWorldPoint(mousePos));
                            unit.StateMachine.ChangeState(EAllyUnitState.Move);
                        }
                    }
                }
            }
        }

    }

    private void HandleOnMouseClickEvent(bool isClicked, Vector2 mousePos)
    {
        _isHolding = isClicked;
        if (isClicked)
        {
            _startPos = Camera.main.ScreenToWorldPoint(mousePos);
            _selectableList.ForEach(x => x.Deselect());
            _selectVisualizer.gameObject.SetActive(true);
            HandleOnMouseMoveEvent(mousePos);
        }
        else
        {
            _selectVisualizer.gameObject.SetActive(false);
        }
    }

    private void HandleOnMouseMoveEvent(Vector2 mousePos)
    {
        if (!_isHolding) return;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 visualizerPos = Vector2.Lerp(_startPos, mousePos, 0.5f);
        float diffX = Mathf.Abs(_startPos.x - visualizerPos.x);
        float diffY = Mathf.Abs(_startPos.y - visualizerPos.y);
        _selectVisualizer.localScale = new Vector2(diffX * 2, diffY * 2);
        _selectVisualizer.position = visualizerPos;
        _selectableList.ForEach(x => x.Deselect());
        _selectableList.Clear();
        int count = Physics2D.OverlapBox(_selectVisualizer.position, _selectVisualizer.localScale, 0, new ContactFilter2D { layerMask = _whatIsSeletable, useLayerMask = true, useTriggers = true }, _colliders);
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                if (_colliders[i].TryGetComponent(out ISelectable selectable))
                {
                    if (_selectableList.Count > 0)
                    {
                        if (_selectableList[0].SeletableType == selectable.SeletableType)
                        {
                            selectable.Select();
                            _selectableList.Add(selectable);
                        }
                    }
                    else
                    {
                        selectable.Select();
                        _selectableList.Add(selectable);
                    }
                }
            }
        }
    }

    public List<ISelectable> GetSeletedObjects()
    {
        return _selectableList;
    }

}
