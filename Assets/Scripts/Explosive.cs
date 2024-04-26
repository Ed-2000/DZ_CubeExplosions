using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 25;
    [SerializeField] private float _explosionForce = 200;

    private Cube cube;

    private void OnEnable()
    {
        cube = transform.GetComponent<Cube>();
        cube.WillDisappear += Explode;
    }

    private void OnDisable()
    {
        cube.WillDisappear -= Explode;
    }

    private void Explode()
    {
        foreach (var explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        List<Rigidbody> explodableObjects = new List<Rigidbody>();
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var hit in hits)
            if (hit.attachedRigidbody != null && hit.GetComponent<Marker>().Creator == gameObject)
                explodableObjects.Add(hit.attachedRigidbody);

        return explodableObjects;
    }
}