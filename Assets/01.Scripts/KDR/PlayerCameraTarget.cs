using Crogen.PowerfulInput;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraTarget : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Vector2 _minPos;
    [SerializeField] private Vector2 _maxPos;

    private Vector2 _startPos;
    private Vector2 _startMouseWPos;
    private bool _isClicked;

    private float _targetZoomIn = 10;

    private void Awake()
    {
        _inputReader.OnRightMouseClickEvent += HandleMouseRightClick;
    }

    private void HandleMouseRightClick(bool isClick, Vector2 vec)
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
            Vector3 targetPos = _startPos + (_startMouseWPos - (Vector2)Camera.main.ScreenToWorldPoint(_inputReader.MousePos)) * 3;
            transform.position = new Vector3(Mathf.Clamp(targetPos.x, _minPos.x, _maxPos.x), Mathf.Clamp(targetPos.y, _minPos.y, _maxPos.y), 0);
        }

        _targetZoomIn = Mathf.Clamp(_targetZoomIn - Input.GetAxisRaw("Mouse ScrollWheel") * 2, 3f, 23f);
        _camera.Lens.OrthographicSize = Mathf.Lerp(_camera.Lens.OrthographicSize, _targetZoomIn, Time.deltaTime * 8);
    }
}
