using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    public List<Vector3> houses = new List<Vector3>();
    public Transform[] corners;

    public int spawnAmount;

    private void Awake()
    {
        for (int j = 0; j < corners.Length; j++)
        {
            Transform cornerOne = corners[j];
            Transform cornerTwo = corners[(j + 1) % corners.Length];
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 toCorner = cornerTwo.position - cornerOne.position;
                Vector3 step = toCorner.normalized * toCorner.magnitude / spawnAmount;
                Vector3 spawnPos = cornerOne.position + (step * i);

                houses.Add(spawnPos);
            }
        }
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < houses.Count; i++)
        {
            Gizmos.DrawCube(houses[i], new Vector3(0.5f, 0.5f, 0.5f));
        }
    }
}
