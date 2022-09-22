
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HittableBullet : Bullet, IHittable
{
    [SerializeField] private float speedMult;
    [SerializeField] private float accMult;
    [SerializeField] private float pauseTime;
    public UnityEvent onHit;
    public void TakeHit(Vector2 dir)
    {
        StartCoroutine(HitStun());
        onHit.Invoke();
        this.dir = dir;
        speed *= speedMult;
        acc *= accMult;
        rb.velocity = Vector2.zero;
    }

    private IEnumerator HitStun()
    {
        var timeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = timeScale;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("HookSwing"))
            base.OnTriggerEnter2D(col);
    }
}
