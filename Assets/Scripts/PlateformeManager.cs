using System.Collections.Generic;
using UnityEngine;

public class PlateformeManager : MonoBehaviour
{
    public GameObject[] plateformePrefabs;
    private Transform playerTransform;
    private float spawnZ = 0f;
    private float plateformeLength = 7f;
    private int numRoadsOnScreen = 4;
    private float safeZone = 10f;
    private int lastPrefabIndex = 0;
    private List<GameObject> activeRoads = new List<GameObject>();
    private int lifeSpan = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < numRoadsOnScreen; i++)
        {
            if (i < 2)
                SpawnRoad(0);
            else
                SpawnRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - numRoadsOnScreen * plateformeLength))
        {
            SpawnRoad();
            DisableRoad();
        }
    }

    private void SpawnRoad(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
            go = Instantiate(plateformePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            go = Instantiate(plateformePrefabs[prefabIndex]) as GameObject;

        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;

        // Randomize the position of the prefab
        Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0f, 0f);
        go.transform.position += randomOffset;

        spawnZ += plateformeLength;
        activeRoads.Add(go);
    }

    private void DisableRoad()
    {
        Destroy(activeRoads[0], lifeSpan);
        activeRoads.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (plateformePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, plateformePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
