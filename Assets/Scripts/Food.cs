using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public FoodItem Item;
    public float Satiety { get; private set; }

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = Item.Sprite;
        Satiety = Item.Satiety;
    }
}
