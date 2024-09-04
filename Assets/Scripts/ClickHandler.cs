using UnityEngine;

namespace DefaultNamespace
{
    
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private Exploder _exploder;
        [SerializeField] private CubeSpawner _spawner;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) == false) 
                return;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask.value) == false)
                return;
            
            if (hit.collider.TryGetComponent(out Cube cube) == false) 
                return;

            if (Utils.RollChance(cube.ChanceToSplit))
            {
                _exploder.Explode(_spawner.Create(cube), cube.transform.position);
                cube.Explode();

                return;
            }

            _exploder.PushByExploderForce(cube);
            cube.Explode();
        }
    }
}