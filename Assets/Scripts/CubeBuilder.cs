using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeBuilder : MonoBehaviour
{
    [SerializeField] private Cube _cube;

    public void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    public void Create()
    {
        Vector3 scale = GetScale();
        Color color = GetColor();
        int chanceToSplit = _cube.ChanceToSplit / 2;
        
        Cube cube = Instantiate(_cube, _cube.transform.position, Quaternion.identity);
        
        cube.Init(scale, color, chanceToSplit);
    }

    private Color GetColor() =>
        Random.ColorHSV();

    private Vector3 GetScale() =>
        transform.localScale / 2;
}