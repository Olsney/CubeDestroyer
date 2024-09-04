using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float FixedForce = 80f;
    [SerializeField] private float MultiplierForce = 15f;
    
    private float _explosionRadius;
    private float _explosionForce;
    
    public void Explode(List<Cube> cubes, Vector3 position)
    {
        foreach (Cube cube in cubes)
        {
            _explosionForce = FixedForce / cube.transform.localScale.magnitude;
            _explosionRadius = FixedForce / cube.transform.localScale.magnitude;
            
            cube.Rigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius);
        }
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

    public void PushByExploderForce(Cube cube)
    {
        _explosionForce = FixedForce / cube.transform.localScale.magnitude * MultiplierForce;
        _explosionRadius = FixedForce / cube.transform.localScale.magnitude * MultiplierForce;

        List<Rigidbody> nearestCubes = GetExplodableObjects();
        
        foreach (Rigidbody nearestCube in nearestCubes)
        {
            nearestCube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}