using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class BaseCube : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    protected virtual void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void ApplyExplosionForce(float force, Vector3 position, float radius)
    {
        _rigidbody.AddExplosionForce(force, position, radius);
    }
}
