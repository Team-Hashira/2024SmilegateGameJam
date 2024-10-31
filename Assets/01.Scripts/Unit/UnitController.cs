using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    private List<Unit> _unitQueue;


    public void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartCoroutine(Fuck());
        }
    }

    private IEnumerator Fuck()
    {
        foreach (Unit unit in _unitQueue)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            unit.SetDestination(mousePos);
            yield return null;
        }
    }
}
