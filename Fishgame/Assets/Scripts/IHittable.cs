using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public void TakeHit(Vector2 dir);
}
