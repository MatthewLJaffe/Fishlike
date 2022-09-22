using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HookAttack : MonoBehaviour
{
    [SerializeField] private Collider2D hitBox;
    [SerializeField] private float cooldownTime;
    [SerializeField] private Animator anim;
    private bool _cooldown;
    private Camera _mainCam;
    public UnityEvent onSwing;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    public void Update()
    {
        if (Input.GetButton("Fire1") && !_cooldown)
        {
            anim.Play("HookSwing");
            onSwing.Invoke();
            StartCoroutine(CooldownRoutine());
        }
    }

    public void ToggleHitBox()
    {
        hitBox.enabled = !hitBox.enabled;
    }

    private IEnumerator CooldownRoutine()
    {
        _cooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        _cooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var hittable = col.GetComponentInChildren<IHittable>();
        if (hittable != null)
        {
            hittable.TakeHit(_mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        }
    }
}
