using Crogen.PowerfulInput;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraTarget : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private InputReader _inputReader;

    private Vector2 _startPos;
    private Vector2 _startMouseWPos;
    private bool _isClicked;

    private void Awake()
    {
        _inputReader.OnMouseRightDownEvent += HandleMouseRightClick;
    }

    private void HandleMouseRightClick(bool isClick)
    {
        _isClicked = isClick;
        if (_isClicked)
        {
            _startPos = transform.position;
            _startMouseWPos = Camera.main.ScreenToWorldPoint(_inputReader.MousePos);
        }
    }

    private void Update()
    {
        if (_isClicked)
        {
            transform.position = _startPos + (_startMouseWPos - (Vector2)Camera.main.ScreenToWorldPoint(_inputReader.MousePos));
        }
    }
}
