using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Material _material;

    public event Action<Cube> Clicked;

    public int ChanceToSplit { get; private set; }

    private void Awake()
    {
        ChanceToSplit = 100;
        _material = GetComponent<Renderer>().material;
    }

    public void Init(Vector3 scale, Color color, int chanceToSplit)
    {
        transform.localScale = scale;
        _material.color = color;
        ChanceToSplit = chanceToSplit;
    }

    public void Explode()
    {
        Clicked?.Invoke(this);
        Destroy(gameObject);
    }
}