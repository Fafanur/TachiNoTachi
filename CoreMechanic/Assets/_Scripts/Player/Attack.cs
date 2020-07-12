using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float force = 50f;
        HitableComponent hc = collision.gameObject.GetComponent<HitableComponent>();
        if (hc != null && hc.gameObject.GetComponent<Rigidbody2D>())
        {
            SoundLibrary.Instance.audioSource.PlayOneShot(SoundLibrary.Instance.SwordSounds[0], 0.7f);
            hc.Hit();
            Vector2 dir = collision.transform.position - transform.position;
            dir = dir.normalized;
            hc.GetComponent<Rigidbody2D>().AddForce(dir * force, ForceMode2D.Impulse);
        }
        else if (hc != null)
        {
            hc.Hit();
        }
    }
}
