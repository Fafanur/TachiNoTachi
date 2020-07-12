using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public delegate void ProjectileHandler(ProjectileBehaviour thisProjectile);
    public event ProjectileHandler TargetReachedEvent;
    public GameObject fakeProjectile;

    private Vector2 target;
    private Rigidbody2D rb;
    private float moveForce = 0.04f;
    private HitableComponent hc;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hc = GetComponent<HitableComponent>();
        hc.GotHitEvent += OnHitEvent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 curPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 step = target - curPos;
        step = step.normalized * moveForce;

        if (Vector2.Distance(curPos, target) <= rb.velocity.magnitude * Time.fixedDeltaTime)
        {
            if (TargetReachedEvent != null)
            {
                TargetReachedEvent(this);
            }
        }
        rb.AddForce(step, ForceMode2D.Impulse);
        LookAt();
    }

    public void SetTarget(Vector2 newTarget)
    {
        target = newTarget;
        rb.velocity = (target - rb.position).normalized * moveForce;
        SoundLibrary.Instance.audioSource.PlayOneShot(SoundLibrary.Instance.arrowSounds[Random.Range(0, SoundLibrary.Instance.arrowSounds.Length)], 0.5f);
    }

    private void LookAt()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StickyComponent stickTarget = collision.gameObject.GetComponent<StickyComponent>();
        if (stickTarget != null)
        {
            GetStuck(stickTarget.stickContainer, collision.contacts[0].point);
        }
    }

    public void GetStuck(GameObject parentObject, Vector2 contactPoint)
    {
        gameObject.SetActive(false);
        GameObject fakeInstance;
        fakeInstance = Instantiate(fakeProjectile, contactPoint, transform.rotation);
        fakeInstance.transform.SetParent(parentObject.transform, true);
        Vector3 pos = fakeInstance.transform.localPosition;
        pos.z = 0.5f;
        fakeInstance.transform.localPosition = pos;
        hc.GotHitEvent -= OnHitEvent;
        Destroy(gameObject);
    }


    private void OnHitEvent(HitableComponent hc)
    {
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1f);
        hc.GotHitEvent -= OnHitEvent;
        Destroy(gameObject);
    }
}
