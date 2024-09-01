using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Material _material;

    public int ChanceToSplit { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        ChanceToSplit = 100;
        _material = GetComponent<Renderer>().material;
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 scale, Color color, int chanceToSplit)
    {
        transform.localScale = scale;
        _material.color = color;
        ChanceToSplit = chanceToSplit;
    }

    public void Explode()
    {
        Destroy(gameObject);
    }
}