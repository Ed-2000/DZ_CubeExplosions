using System;
using UnityEngine;

public class RayLauncher : MonoBehaviour
{
    public event Action<Cube> HitInCube;

    [SerializeField] private float _maxDistance = 10;
    
    private Camera _camera;
    private Ray _ray;

    private void Start()
    {
        _camera = transform.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ShootRay();
    }

    private void ShootRay()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.green);

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            Cube cube = hit.transform.GetComponent<Cube>();

            if (cube != null)
                HitInCube?.Invoke(cube);
        }
    }
}