using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _expolsionForce;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private CubeBuilder _cubeBuilder;

    private Material _material;

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
        Destroy(gameObject);
        
        if (Utils.RollChance(ChanceToSplit))
            return;

        for (int i = 0; i < GetAmountCubes(); i++)
            _cubeBuilder.Create();

        ApplyExplosionForce();
    }
    
    private void ApplyExplosionForce()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_expolsionForce, transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }

    private int GetAmountCubes()
    {
        int min = 2;
        int max = 7;

        return Random.Range(min, max);
    }
}