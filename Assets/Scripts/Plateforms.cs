
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforms : MonoBehaviour
{
    [SerializeField] GameObject[] plateformPrefabs;
    private Transform playerTransform;
    private List<GameObject> activeRoads = new List<GameObject>();
    private int lastPrefabIndex = 0;
    private int lifeSpan = 3;


    void Start()
    {
        
    }

    void Update()
    {
        SpawnPlateform();
    }
    void SpawnPlateform(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(plateformPrefabs[RandomPrefabIndex()]) as GameObject;
            go.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 1f, playerTransform.position.z);

        }

        else
            go = Instantiate(plateformPrefabs[prefabIndex]) as GameObject;

        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward;

        // Randomize the position of the prefab
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1f);
        go.transform.position += randomOffset;

        activeRoads.Add(go);
    }
    private void DeleteRoad()
    {
        Destroy(activeRoads[0], lifeSpan);
        activeRoads.RemoveAt(0);
    }
    private int RandomPrefabIndex()
    {
        if (plateformPrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, plateformPrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
