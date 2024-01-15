using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZeminSpawner : MonoBehaviour
{
    public List<GameObject> zemin; // Zemin prefab listesi
    public Transform player; // Oyuncunun transformu
    public float spawnZ = 0f; // Baþlangýç Z pozisyonu
    public float zeminLength = 30f; // Her zeminin uzunluðu
    public int zeminOnScreen = 5; // Ekranda görünecek maksimum zemin sayýsý

    private List<GameObject> zeminler; // Ekranda bulunan zeminlerin listesi

    private void Awake()
    {
        zeminler = new List<GameObject>();

        for (int i = 0; i < zeminOnScreen; i++)
        {
            Spawn();
        }
    }

    private void LateUpdate()
    {
        if (player.position.z - 25 > spawnZ - zeminOnScreen * zeminLength)
        {
            Spawn();
            Delete();
        }
    }

    private void Spawn()
    {
        if(spawnZ == 0)
        {
            zeminler.Add(Instantiate(zemin[Random.Range(1,zemin.Count - 1)], new Vector3(0, 0, spawnZ), Quaternion.identity)); // en sonda stand olmalý
        }
        else
        {
            zeminler.Add(Instantiate(RandomZemin(), new Vector3(0, 0, spawnZ), Quaternion.identity));
        }
        spawnZ += zeminLength;
    }

    private void Delete()
    {
        Destroy(zeminler[0]);
        zeminler.RemoveAt(0);
    }

    private GameObject RandomZemin()
    {
        if (zemin.Count == 1 || zeminler.Count == 0)
        {
            return zemin[Random.Range(0, zemin.Count)];
        }

        GameObject lastTile = zeminler.Last();
        GameObject newTile;
        do
        {
            newTile = zemin[Random.Range(0, zemin.Count)];
        }
        while (newTile == lastTile);

        return newTile;
    }
}
