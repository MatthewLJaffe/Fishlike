using System;
using System.Collections;
using General;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 dir;
    public float speed;
    public float acc;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float liveTime;
    private bool _lanched;
    [SerializeField] protected ParticleSystem destroyParticleSystem;
    [SerializeField] protected Animator anim;
    [SerializeField] private SoundEffect destroySound;

    private void Start()
    {
        StartCoroutine(LaunchBullet());
    }

    private void FixedUpdate()
    {
        if (!_lanched) return;
        if (rb.velocity.magnitude < speed) {
            rb.velocity += dir.normalized * (acc * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private IEnumerator LaunchBullet()
    {
        yield return new WaitUntil(() => dir != Vector2.zero);
        var trans = transform;
        trans.up = dir.normalized;
        rb.velocity = trans.up;
        _lanched = true;
        yield return new WaitForSeconds(liveTime);
        BulletDestruction();
    }

    private void BulletDestruction()
    {
        rb.velocity = Vector2.zero;
        _lanched = false;
        destroyParticleSystem.gameObject.SetActive(true);
        destroyParticleSystem.Play();
        anim.SetTrigger("Destroy");
    }
    
    public void DestoryBullet()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        destroySound.Play();
        BulletDestruction();
    }
}
