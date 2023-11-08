using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    [SerializeField] int _numberOfPoints = 100;
    [SerializeField] float _power = 10;
    [SerializeField] float _maxDrag = 5;
    [SerializeField] Rigidbody2D _rb;


    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] LineRenderer _previewLine;

    float _bladeSpeed;
    Vector3 _force;
    Vector3 _clampedDirection;
    Vector3 _startPoint;
    Vector3 _endPoint;
    Touch _touch;

    bool _isStartDone = false;
    bool _isNewInputAfterAllOk = false;

    PlayerController _playerController;
    Camera _camera;

    private void Start()
    {
        _playerController = PlayerController.Instance;
        _camera = Camera.main;

        _bladeSpeed = _playerController.BladeSpeed;
    }

    private void Update()
    {
        if ((_playerController.IsGrounded() || _playerController.IsWalled(Vector2.left) || _playerController.IsWalled(Vector2.right)) && _playerController.CanCastBlade && GameManager.Instance.PlayerCanPlay)
        {
            if (_isNewInputAfterAllOk == false && Input.touchCount == 0)
            {
                _isNewInputAfterAllOk = true;
            }

            if (Input.touchCount > 0 && _isNewInputAfterAllOk)
            {
                _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Began || _isStartDone == false)
                {
                    DragStart();

                }

                if (_touch.phase == TouchPhase.Moved)
                {
                    Dragging();
                }

                if (_touch.phase == TouchPhase.Ended)
                {
                    if (_isStartDone)
                    {
                        DragRelease();
                    }
                }
            }
        }
        else
        {
            _isNewInputAfterAllOk = false;

            if (_isStartDone)
            {
                _lineRenderer.positionCount = 0;
                _previewLine.positionCount = 0;

                _isStartDone = false;

                TimeManager.Instance.SetTime(1);
            }
        }
    }

    void DragStart()
    {
        _lineRenderer.positionCount = 0;
        _previewLine.positionCount = _numberOfPoints;

        _startPoint = _camera.ScreenToWorldPoint(_touch.position);
        _startPoint.z = 0;
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, _startPoint);

        TimeManager.Instance.SetTime();

        _isStartDone = true;
    }

    void Dragging()
    {
        Vector3 draggingPos = _camera.ScreenToWorldPoint(_touch.position);
        draggingPos.z = 0;

        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(1, draggingPos);

        _endPoint = _camera.ScreenToWorldPoint(_touch.position);
        _endPoint.z = 0;

        _force = _startPoint - _endPoint;

        _clampedDirection = Vector3.ClampMagnitude(_force, _maxDrag) * _power;

        Vector3[] positions = new Vector3[_numberOfPoints];
        Vector3 currentPosition = transform.position;

        for (int i = 0; i < _numberOfPoints; i++)
        {
            float time = i * _clampedDirection.magnitude / 100;
            positions[i] = currentPosition + _clampedDirection * _bladeSpeed * time + 0.5f * Physics.gravity * time * time;
        }

        _previewLine.SetPositions(positions);
    }

    void DragRelease()
    {
        _lineRenderer.positionCount = 0;
        _previewLine.positionCount = 0;

        _endPoint = _camera.ScreenToWorldPoint(_touch.position);
        _endPoint.z = 0;

        _force = _startPoint - _endPoint;

        _clampedDirection = Vector3.ClampMagnitude(_force, _maxDrag) * _power;

        PlayerController.Instance.CastBlade(_clampedDirection);

        _isStartDone = false;

        TimeManager.Instance.SetTime(1);
    }
}
