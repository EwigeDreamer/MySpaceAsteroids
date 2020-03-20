using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTools.Extensions.GameObjects;
using MyTools.Extensions.Vectors;
using MyTools.Helpers;

public class PlayerMotor : MonoValidate
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 10f;
    [SerializeField] new Collider[] colliders;

    public Vector3 NormalizedVelocity => rb.velocity / speed;
    public Vector3 Position => transform.position;
    public Rigidbody Rb => this.rb;

    public void SetEnabledCollider(bool state)
    { foreach (var col in this.colliders) col.enabled = state; }
    public void SetRigidbodyKinematic(bool state) => this.rb.isKinematic = state;

    protected override void OnValidate()
    {
        base.OnValidate();
        ValidateGetComponent(ref this.rb);
    }

    [ContextMenu("Get colliders")]
    void GetColliders() => this.colliders = GetComponentsInChildren<Collider>();

    public void Move(Vector2 dir)
    {
        var dir3d = dir.ToV3_xy0();
        this.rb.velocity = dir3d * Mathf.Max(speed, this.rb.velocity.magnitude);
    }
}
