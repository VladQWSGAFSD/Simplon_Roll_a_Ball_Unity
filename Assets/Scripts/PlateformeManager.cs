using System.Collections.Generic;
using UnityEngine;

public class PlateformeManager : MonoBehaviour
{
    public GameObject[] plateformePrefabs;
    private Transform playerTransform;
    private float spawnZ, spawnY, spawnX = 0f;
    private float plateformeLength = 7f;
    private int numPlatsOnScreen = 4;
   // private float safeZone = 0f;
    private int lastPrefabIndex = 0;
    private List<GameObject> activePlateforms = new List<GameObject>();
    private int lifeSpan = 10;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < numPlatsOnScreen; i++)
        {
            if (i < 2)
                SpawnPlateform(0);
            else
                SpawnPlateform();
        }
    }

    void Update()
    {
        if (playerTransform.position.z > (spawnZ - numPlatsOnScreen * plateformeLength))
        {
            SpawnPlateform();
            DestoyPlateform();
        }
    }

    private void SpawnPlateform(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
            go = Instantiate(plateformePrefabs[RandomPrefabIndex()]);
        else
            go = Instantiate(plateformePrefabs[prefabIndex]);

        go.transform.SetParent(transform);
        go.transform.position = new Vector3(spawnX, spawnY, spawnZ);

        // Randomize the position of the prefab
        Vector3 randomOffset = new Vector3(Random.Range(-7f, 7f), 0f, 0f);
        go.transform.position += randomOffset;

        spawnZ += plateformeLength;
        activePlateforms.Add(go);

        spawnY += 1f;
        spawnX += 3f;
    }


    private void DestoyPlateform()
    {
        Destroy(activePlateforms[0], lifeSpan);
        activePlateforms.RemoveAt(0);
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
