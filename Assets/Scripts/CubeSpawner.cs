using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private void OnEnable()
    {
        _cube.Clicked += Explode;
    }
    
    public void Create(Cube explodedCube)
    {
        Vector3 scale = GetScale(explodedCube);
        Color color = GetColor();
        int chanceToSplit = GetChanceToSplit(explodedCube);

        Cube cube = Instantiate(explodedCube, explodedCube.transform.position, Quaternion.identity);

        cube.Init(scale, color, chanceToSplit);

        cube.Clicked += Explode;
    }
    
    private void Explode(Cube explodedCube)
    {
        if (Utils.RollChance(explodedCube.ChanceToSplit))
            return;

        for (int i = 0; i < GetAmountCubes(); i++)
            Create(explodedCube);

        List<Rigidbody> cubes = GetExplodableObjects();

        foreach (Rigidbody cube in cubes)
        {
            cube.AddExplosionForce(_explosionForce, explodedCube.transform.position, _explosionRadius);
        }

        explodedCube.Clicked -= Explode;
    }
    
    private int GetChanceToSplit(Cube explodedCube)
    {
        int divider = 2;
            
        return explodedCube.ChanceToSplit / divider;
    }

    private Color GetColor() =>
        Random.ColorHSV();

    private Vector3 GetScale(Cube explodedCube)
    {
        int divider = 2;

        return explodedCube.transform.localScale / divider;
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