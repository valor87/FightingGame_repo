using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _movementSpeed = 3;


    [Header("Player Rotation")]
    [SerializeField] float _rotationSpeed;
    [SerializeField] int _amountOfPoints = 12;
    [SerializeField] float _laserSize = 5;
    
    private PlayerInputController _inputController;
    private PlayerCamera _playerCamrea;
    private Rigidbody _thisRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputController = GetComponent<PlayerInputController>();
        _playerCamrea = GetComponent<PlayerCamera>();
        _thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementInput();
        rotationInput();
        debugDrawRoatation(_amountOfPoints, transform.position, _laserSize);
    }

    void movementInput()
    {
        Vector3 inputVelocty = new Vector3(_inputController.moveValue.x, 0, _inputController.moveValue.y);

    }

    void rotationInput()
    {
        Vector3 inputValue = new Vector3(_inputController.lookValue.x, 0, _inputController.lookValue.y);
        _thisRigidbody.position += ((_playerCamrea.forwadLookRotation()) * (_inputController.moveValue.y * _movementSpeed)) * Time.fixedDeltaTime;
    }

    void debugDrawRoatation(int _amountOfPoint, Vector3 playerPos, float laserSize)
    {
        Color lineColor = Color.green;
        float angleStep = 360f/_amountOfPoint;
        float radius = angleStep * Mathf.Deg2Rad;
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < _amountOfPoint; i++)
        {
            float adjustment = radius * i;
            points.Add(new Vector3(Mathf.Cos(radius + adjustment) * laserSize, playerPos.y, Mathf.Sin(radius + adjustment) * laserSize));
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            Debug.DrawLine(transform.position + points[points.Count - 1], transform.position + points[0], lineColor);

            Debug.DrawLine(transform.position + points[i], transform.position + points[i + 1], lineColor);
        }
    }
}
