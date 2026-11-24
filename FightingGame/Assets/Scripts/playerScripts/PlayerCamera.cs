using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    [Header("Running Movement Change")]
    [SerializeField] private Transform _movingCameraStart;
    [SerializeField] private Transform _movingStaticPosition;
    [SerializeField] private float _movementTime;
    [SerializeField] private AnimationCurve _movementCurve;

    private float _aniTime;
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
        cameraMovement();
    }

    private void cameraMovement()
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
}
