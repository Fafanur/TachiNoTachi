using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public delegate void ContainerHandler(ItemContainer thisItemContainer, ItemComponent thisItemComp);
    public event ContainerHandler CrateDestroyedEvent;
    public ItemComponent containedItemPrefab;
    public GameObject dustParticle;
    private HitableComponent hc;

    private void Awake()
    {
        hc = GetComponent<HitableComponent>();
        hc.GotHitEvent += OnHitEvent;
    }


    private void OnHitEvent(HitableComponent hc)
    {
        Instantiate(dustParticle, transform.position, dustParticle.transform.rotation);
        SoundLibrary.Instance.audioSource.PlayOneShot(SoundLibrary.Instance.WoodSounds[Random.Range(0, SoundLibrary.Instance.WoodSounds.Length)]);
        StartCoroutine(GetDestroyed());
    }
    public IEnumerator GetDestroyed()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        ItemComponent item = Instantiate(containedItemPrefab, transform.position, transform.rotation);
        hc.GotHitEvent -= OnHitEvent;
        CrateDestroyedEvent?.Invoke(this, item);
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProjectileBehaviour projectile = collision.gameObject.GetComponent<ProjectileBehaviour>();
        if (projectile != null)
        {
            SoundLibrary.Instance.audioSource.PlayOneShot(SoundLibrary.Instance.arrowTok);
        }
    }
}
