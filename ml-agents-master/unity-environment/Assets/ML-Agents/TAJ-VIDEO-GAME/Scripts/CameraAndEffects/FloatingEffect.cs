using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour {
    // Public variable to set the distance
    public float _distance;
    public float _distanceRotation;

    // Variables for floating
    private float _percent = 0.5f;
    private float _speed = 0.5f;
    private Direction _direction;
    private float _percentrotation = 0.0f;
    private float _speedrotation = 0.5f;
    private Direction _directionrotation;

    public float _initialYposition;

    // Define direction up and down
    enum Direction { UP, DOWN, RIGHT, LEFT };

    // Set the direction to up, and the locations
    void Start()
    {
        transform.position = new Vector3(transform.position.x, _initialYposition, transform.position.z);
        _direction = Direction.UP;
    }

    void Update()
    {
        ApplyFloatingEffect();
        ApplyRotationEffect();
    }

    // Apply the floating effect between the given positions
    void ApplyFloatingEffect()
    {
        if (_direction == Direction.UP && _percent < 1)
        {
            _percent += Time.deltaTime * _speed;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(_initialYposition, _distance, _percent), transform.position.z);
        }
        else if (_direction == Direction.DOWN && _percent < 1)
        {
            _percent += Time.deltaTime * _speed;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(_distance, _initialYposition, _percent), transform.position.z);
        }
        if (_percent >= 1)
        {
            _percent = 0.0f;
            if (_direction == Direction.UP)
            {
                _direction = Direction.DOWN;
            }
            else
            {
                _direction = Direction.UP;
            }
        }
    }

     // Apply a random rotation effect
    void ApplyRotationEffect()
    {
        if (_directionrotation == Direction.RIGHT && _percentrotation < 1)
        {
            _percentrotation += Time.deltaTime * _speedrotation;
            transform.Rotate(Vector3.forward, _percentrotation);
        }
        else if (_directionrotation == Direction.LEFT && _percentrotation < 1)
        {
            _percentrotation += Time.deltaTime * _speedrotation;
            transform.Rotate(Vector3.back, _percentrotation);
        }
        if (_percentrotation >= 1)
        {
            if (_directionrotation == Direction.RIGHT)
            {
                _directionrotation = Direction.LEFT;
            }
            else
            {
                _directionrotation = Direction.RIGHT;
            }
        }
    }
}
