using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 15f;
    [SerializeField] private float _explosionForce = 15f;

    public void Explode(List<Cube> cubes, Vector3 position) 
    {
        foreach (Rigidbody cube in cubes.Select(cube => cube.Rigidbody))
        {
            cube.AddExplosionForce(_explosionForce, position, _explosionRadius);
        }
    }
}