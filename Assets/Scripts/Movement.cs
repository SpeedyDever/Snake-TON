using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    public Action OnFoodEaten;
    public float Speed = 3f;
    public float BaseSpeed;
    public float RotationSpeed = 90f;

    [SerializeField] private float _sprintMultyplier;
    [SerializeField] private float _energy;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _energyChange;

    private FoodManager _foodManager;

    private void OnEnable()
    {
        _foodManager = FindObjectOfType<FoodManager>();
        _foodManager.Snakes.Add(this);
        _foodManager.UpdateSnakes();
    }

    private void Start()
    {
        BaseSpeed = Speed;
    }

    private void Update()
    {
        Move();
        Sprint();
    }

    private void Sprint()
    {
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(0)) && _energy > 0)
        {
            Speed = BaseSpeed * _sprintMultyplier;
            _energy -= _energyChange * Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetMouseButtonUp(0))
        {
            Speed = BaseSpeed;
        }
        else if (Speed != BaseSpeed)
        {
            Speed = BaseSpeed;
        }

        if (_energy < _maxEnergy && !(Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(0)))
        {
            _energy += _energyChange / 2 * Time.deltaTime;
        }

        if (_energy > _maxEnergy)
        {
            _energy = _maxEnergy;
        }

        if (_energy < 0)
        {
            _energy = 0;
        }
    }

    public void Rotate(float direction)
    {
        transform.rotation *= Quaternion.Euler(transform.rotation.x, transform.rotation.y, direction * RotationSpeed * Time.deltaTime);
    }

    public void Rotate(Vector2 direction)
    {
        float angle = Vector2.SignedAngle(Vector2.down, direction);
        Vector3 targetRotation = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), RotationSpeed * Time.deltaTime);
    }

    public void Move()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Food"))
        {
            OnFoodEaten?.Invoke();
            Destroy(other.gameObject);
        }
    }
}