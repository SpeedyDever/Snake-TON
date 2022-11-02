using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Mass : MonoBehaviour
{
    public Action OnMassFilled;
    public Action OnMassDeFilled;

    private Movement _movement;
    public float Weight;

    private void OnEnable()
    {
        _movement.OnFoodEaten += AddMass;
    }

    private void OnDisable()
    {
        _movement.OnFoodEaten -= AddMass;
    }

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void AddMass(float satiety)
    {
        if (Weight < 0) Weight = 0;
        float remain = Weight % 2;
        Weight += satiety;
        CheckForFill(remain, OnMassFilled);
    }

    public void SubstractMass(float value)
    {
        if (Weight < 0) Weight = 0;
        if (Weight > 0)
        {
            float remain = Weight % 2;
            Weight -= value;
            CheckForFill(remain, OnMassDeFilled);
        }
    }

    private void CheckForFill(float remain, Action action)
    {
        if (remain < 1 && Weight % 2 >= 1)
        {
            action?.Invoke();
        }

        if (remain > 1 && (Weight % 2 < 1 || Weight % 2 == 0))
        {
            action?.Invoke();
        }
    }
}