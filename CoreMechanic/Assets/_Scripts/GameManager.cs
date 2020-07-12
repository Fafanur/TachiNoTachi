using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameHandler(GameManager manager);
    public GameHandler PickedUpEvent;
    public GameHandler GameWonEvent;
    public ItemContainer itemContPrefab;
    public int itemPickup = 0;
    public Transform[] itemSpawnPoints;
    public HealthComponent playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCrates();
        playerHealth.OnDeathEvent += LoseGame;

    }

    private void SpawnCrates()
    {
        ItemContainer itemContainer = Instantiate(itemContPrefab, itemSpawnPoints[itemPickup % itemSpawnPoints.Length]);
        itemContainer.CrateDestroyedEvent += OnCrateDestroy;
    }

    private void OnCrateDestroy(ItemContainer container, ItemComponent item)
    {
        container.CrateDestroyedEvent -= OnCrateDestroy;
        item.PickedUpEvent += OnPickedUp;
    }

    private void OnPickedUp(ItemComponent item)
    {
        item.PickedUpEvent -= OnPickedUp;
        PickedUpEvent?.Invoke(this);
        itemPickup++;
        if (itemPickup >= itemSpawnPoints.Length)
        {
            itemPickup = 0;
            WinGame();
        }
        else 
        {
            SpawnCrates();
        }
    }

    private void WinGame()
    {
        GameWonEvent?.Invoke(this);
        Time.timeScale = 0;
    }

    private void LoseGame(HealthComponent healthComp)
    {
        playerHealth.gameObject.GetComponent<Movement>().enabled = false;
    }



}
