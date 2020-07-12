using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableComponent : MonoBehaviour
{
    public delegate void GotHitHandler(HitableComponent thisHitComp);
    public event GotHitHandler GotHitEvent;

    public void Hit()
    {
        GotHitEvent?.Invoke(this);
    }
}
