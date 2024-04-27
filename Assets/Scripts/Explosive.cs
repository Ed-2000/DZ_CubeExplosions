using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class Explosive : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 25;
    [SerializeField] private float _explosionForce = 200;

    private Cube cube;

    private void OnEnable()
    {
        cube = transform.GetComponent<Cube>();
        cube.WillDisappear += DisappearanceHandler;
    }

    private void OnDisable()
    {
        cube.WillDisappear -= DisappearanceHandler;
    }

    private void DisappearanceHandler(List<Rigidbody> explodableObjects)
    {
        if (explodableObjects.Count == 0)
            Explode();
        else if (explodableObjects.Count > 0)
            Explode(explodableObjects);
    }

    private void Explode(List<Rigidbody> explodableObjects)
    {
        foreach (var explodableObject in explodableObjects)
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private void Explode()
    {
        foreach (var explodableObject in GetExplodableObjects())
        {
            float force = _explosionForce / transform.localScale.magnitude;
            explodableObject.AddExplosionForce(force, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        List<Rigidbody> explodableObjects = new List<Rigidbody>();

        var hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var hit in hits)
            if (hit.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                explodableObjects.Add(rigidbody);

        return explodableObjects;
    }
}