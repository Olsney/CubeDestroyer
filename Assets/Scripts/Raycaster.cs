using UnityEngine;

namespace DefaultNamespace
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;

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
                
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask.value) == false)
                return;

            if (hit.collider.TryGetComponent(out Cube cube) == false) 
                return;
                
            cube.Explode();
        }
    }
}