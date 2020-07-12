using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public HealthComponent playerHealth;
    public GameObject[] hearts;
    public GameObject[] numbers;
    public GameObject deathScreen;
    public GameObject winScreen;
    public Animator animator;

    public GameObject zero;
    private int hearthCounter = 0;
    public GameManager gameManager;
    void Start()
    {
        if (gameManager != null)
        {
            gameManager.PickedUpEvent += PickupItem;
            gameManager.GameWonEvent += OnWin;
        }
        if (playerHealth != null)
        {
            playerHealth.OnHitEvent += PlayerHit;
            playerHealth.OnDeathEvent += OnDeath;
        }
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
        
    }
    public void OnStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainGame");
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void PlayerHit(HealthComponent playerHealth)
    {
        SoundLibrary.Instance.audioSource.PlayOneShot(SoundLibrary.Instance.HitSounds[Random.Range(0, SoundLibrary.Instance.HitSounds.Length)], 0.8f);
        if (hearthCounter != hearts.Length)
        {
            animator.SetTrigger("GotHitTrigger");
            hearts[hearthCounter].SetActive(false);
            hearthCounter++;
        }
        if(hearthCounter == hearts.Length)
        { 
            hearthCounter = 0;
        }
    }

    private void PickupItem(GameManager manager)
    {
        zero.SetActive(false);
        SoundLibrary.Instance.audioSource.PlayOneShot(SoundLibrary.Instance.pickUpSounds[gameManager.itemPickup], 0.7f);
        numbers[gameManager.itemPickup].SetActive(true);
        if(gameManager.itemPickup >= 1)
        {
            numbers[gameManager.itemPickup -1].SetActive(false);
        }
    }

    public void OnDeath(HealthComponent health)
    {
        deathScreen.SetActive(true);
        SoundLibrary.Instance.audioSource.PlayOneShot(SoundLibrary.Instance.UIsounds[1]);
        playerHealth.OnDeathEvent -= OnDeath;
        gameManager.PickedUpEvent -= PickupItem;
        playerHealth.OnHitEvent -= PlayerHit;
    }

    public void OnWin(GameManager manager)
    {
        winScreen.SetActive(true);
        SoundLibrary.Instance.audioSource.PlayOneShot(SoundLibrary.Instance.UIsounds[0]);
        gameManager.PickedUpEvent -= PickupItem;
        playerHealth.OnHitEvent -= PlayerHit;
        playerHealth.OnDeathEvent -= OnDeath;
    }
}
