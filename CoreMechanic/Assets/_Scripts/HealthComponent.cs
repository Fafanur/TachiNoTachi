using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void HealthHandler(HealthComponent healthComp);
    public HealthHandler OnDeathEvent;
    public HealthHandler OnHitEvent;
    public int maxHits;
    private int hits;
    // Start is called before the first frame update
    void Awake()
    {
        hits = maxHits;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProjectileBehaviour projectile = collision.gameObject.GetComponent<ProjectileBehaviour>();
        if (projectile != null )
        {
            hits -= 1;
            OnHitEvent?.Invoke(this);
        }

        if (hits == 0)
        {
            hits = maxHits;
            OnDeathEvent?.Invoke(this);
        }
    }
}
