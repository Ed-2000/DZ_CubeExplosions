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

    private void Explode(List<Rigidbody> explodableObjects)
    {
        foreach (var explodableObject in explodableObjects)
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}