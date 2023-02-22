using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlateformeManager : MonoBehaviour
{
    [SerializeField] GameObject[] plateformePrefabs;

    private float spawnZ, spawnY, spawnX = 0f;
    private float plateformeLength = 7f;
    private int numPlatsOnScreen = 4;
    private int lastPrefabIndex = 0;
    private float lifeSpan = 30f;
    private int numRespawns = 0;
    //bool isDead = false;
    private Transform playerTransform;
    private List<GameObject> activePlateforms = new List<GameObject>();
    private ScoreManager scoreManager;

    [SerializeField] string menuSceneName = "Level 2"; // Set the name of the menu scene


    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
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
        RespawnPlayer();
       // Die();
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
        Vector3 randomOffset = new Vector3(Random.Range(4f, 4f), 0f, 0f);
        go.transform.position += randomOffset;

        // Add random rotation between 0 and 180 to each platform except the first three
        if (activePlateforms.Count >= 3)
        {
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 180f), 0f);
            go.transform.rotation = randomRotation;
        }
        //casue of random rotation there could be a overlap if offset is an 0 for x, so adding one unit on top just in case
        spawnZ += plateformeLength + 1f;
        activePlateforms.Add(go);

        spawnY += 1f;
        spawnX += Random.Range(0, 1f);
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

    private void RespawnPlayer()
    {
        // Find the platform below the player
        int platformIndex = activePlateforms.FindIndex(p => p.transform.position.z > playerTransform.position.z);
        if (platformIndex >= 0)
        {
            platformIndex = Mathf.Max(0, platformIndex - 1);
        }
        else
        {
            platformIndex = activePlateforms.Count - 1;
        }
        GameObject currentPlatform = activePlateforms[platformIndex];

        // Respawn the player on the current platform if they fall below it
        if (playerTransform.position.y < (currentPlatform.transform.position.y - 2f) && numRespawns <= 2)
        {
            playerTransform.position = new Vector3(
                currentPlatform.transform.position.x,
                currentPlatform.transform.position.y + 0.5f,
                currentPlatform.transform.position.z
            );


            numRespawns++;
            Debug.Log("player respawned,number of respawns: " + numRespawns);
            scoreManager.PunishScore();
        }
        else
        {
            //isDead = true;
            //playerTransform.gameObject.SetActive(false); // Disable the player game object
            //SceneManager.LoadScene(menuSceneName); // Load the menu scene
        }
    }
    private void Die()
    {
        if (playerTransform.position.y <= -10f)
        {
            playerTransform.gameObject.SetActive(false);
        }

    }
}