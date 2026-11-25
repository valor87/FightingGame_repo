using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Player Camrera Input")]
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _xHeightClamp = 30;
    [SerializeField] private float _xLowClamp = 30;

    [Header("Running Movement Change")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _movingCameraStart;
    [SerializeField] private Transform _movingStaticPosition;
    [SerializeField] private float _movementTime;
    [SerializeField] private AnimationCurve _movementCurve;

    private float _aniTime;
    private float _xRotation;
    private float _yRotation;

    private bool _switchAni = true;
    private PlayerMovement _playerMovement;
    private PlayerInputController _playerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerInput = GetComponent<PlayerInputController>();
        _playerMovement = GetComponent<PlayerMovement>();
        _movingCameraStart.position = _cameraTransform.position;
        _movingCameraStart.rotation = _cameraTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        cameraInputRotation();
        cameraSprintMovement();
    }

    private void cameraInputRotation()
    {
        _xRotation += _playerInput.lookValue.x * Time.deltaTime;
        _yRotation += _playerInput.lookValue.y * Time.deltaTime;

        _yRotation = Mathf.Clamp(_yRotation, _xLowClamp, _xHeightClamp);

        Debug.Log("The y rotation value: " + _yRotation);
        Debug.Log("The x rotation value: " + _xRotation);

        _cameraHolder.eulerAngles = new Vector3(_yRotation, _xRotation, 0) * _rotationSpeed;
    }

    private void cameraSprintMovement()
    {
        Vector3 movementValue = _playerInput.moveValue;

        if (_aniTime <= _movementTime)
        {
            _aniTime += Time.deltaTime;
        }

        if (movementValue != Vector3.zero && _switchAni)
        {
            preformMovement(_movingCameraStart, _movingStaticPosition);
        } 
        else if (movementValue == Vector3.zero && _switchAni)
        {
            _aniTime = _movementTime - _aniTime;
            _switchAni = false;
        }

        if (movementValue == Vector3.zero && !_switchAni) 
        {
            preformMovement(_movingStaticPosition, _movingCameraStart);
        } 
        else if (movementValue != Vector3.zero && !_switchAni!)
        {
            _aniTime = _movementTime - _aniTime;
            _switchAni = true;
        }
    }
    
    void preformMovement(Transform startTransform, Transform endTransform)
    {
        _cameraTransform.rotation = Quaternion.Lerp(startTransform.rotation, endTransform.rotation, _movementCurve.Evaluate(_aniTime / _movementTime));
        _cameraTransform.position = Vector3.Lerp(startTransform.position, endTransform.position, _movementCurve.Evaluate(_aniTime / _movementTime));
    }
    public Vector3 forwadLookRotation()
    {
        return _cameraHolder.forward;
    }

    public Vector3 rightLookRotation()
    {
        return _cameraHolder.right;
    }
}
