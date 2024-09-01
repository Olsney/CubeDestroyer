using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public List<Cube> Create(Cube explodedCube)
    {
        int cubesAmount = GetAmountCubes();
        List<Cube> cubes = new List<Cube>();
        
        for (int i = 0; i < cubesAmount; i++)
        {
            Cube cube = Instantiate(explodedCube, explodedCube.transform.position, Quaternion.identity);
            
            cube.Init(GetScale(explodedCube), GetRandomColor(), GetChanceToSplit(explodedCube));
            
            cubes.Add(cube);
        }

        return cubes;
    }
    
    private int GetChanceToSplit(Cube explodedCube)
    {
        int divider = 2;
            
        return explodedCube.ChanceToSplit / divider;
    }

    private Color GetRandomColor() =>
        Random.ColorHSV();

    private Vector3 GetScale(Cube explodedCube)
    {
        int divider = 2;

        return explodedCube.transform.localScale / divider;
    }

    private int GetAmountCubes()
    {
        int min = 2;
        int max = 7;

        return Random.Range(min, max);
    }
}