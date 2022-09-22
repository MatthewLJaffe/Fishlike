using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 _dir = Vector2.zero;
    [SerializeField] private float zOffset;
    [SerializeField] private float deadZone;
    private Camera _cam;
    private Vector3 _mousePos;

    private void Awake()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.y = Input.GetAxis("Vertical");
        _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(_mousePos, transform.position) < deadZone) return;
        Vector3 upDir = (Vector2)(transform.position - _mousePos);
        upDir = Quaternion.Euler(0, 0, zOffset) * upDir;
        transform.up = upDir;
    }

    private void FixedUpdate()
    {
        rb.velocity = _dir.normalized * speed;
    }

}
