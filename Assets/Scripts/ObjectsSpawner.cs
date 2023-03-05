using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectsSpawner : MonoBehaviour
{

    private GameObject door;
    private GameObject chest;
    private GameObject playableArea;
    private Vector3 size;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    [Inject]
    public void Construct(GameObject door, GameObject chest, GameObject playableArea)
    {
        this.door = door;
        this.chest = chest;
        this.playableArea = playableArea;
    }

    private void OnEnable()
    {
        EventHandler.OnGameOver += Restart;
    }

    public void SpawnAllObjects()
    {
        MeshRenderer renderer = playableArea.GetComponent<MeshRenderer>();
        size = renderer.bounds.size / 2;

        SpawnObject(door);
        SpawnObject(chest);
    }

    private void SpawnObject(GameObject spawnObject)
    {
        Vector3 spawnLocation;
        int x;
        int z;
        int collisions = 1;
        Vector3 collider = spawnObject.GetComponent<BoxCollider>().size;
        do
        {
            x = (int)Random.Range(-size.x, size.x);
            z = (int)Random.Range(-size.z, size.z);
            spawnLocation = new Vector3(x, playableArea.transform.position.y + collider.y / 2, z);
            collisions = Physics.OverlapBoxNonAlloc(spawnLocation, collider, null, Quaternion.identity, LayerMask.NameToLayer("Objects"), QueryTriggerInteraction.Collide);

        } while (collisions > 0);
        GameObject spawn = Instantiate(spawnObject, spawnLocation, spawnObject.transform.rotation);
        spawnedObjects.Add(spawn);
    }

    private void Restart(float score)
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            Destroy(spawnedObjects[i]);
        }
    }

    private void OnDisable()
    {
        EventHandler.OnGameOver -= Restart;
    }
}
