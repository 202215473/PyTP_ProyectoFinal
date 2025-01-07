using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    public GameObject femaleGPrefab;
    public GameObject femaleKPrefab;
    public GameObject maleGPrefab;
    public GameObject maleKPrefab;
    public GameObject doctorPrefab;
    public GameObject elderPrefab;
    public GameObject boyPrefab;

    private Dictionary<int, GameObject> prefabDictionary;
    private List<Client> clients = new List<Client>();

    public event Action<Client> newClientSpawned;

    // Start is called before the first frame update
    void Start()
    {
        prefabDictionary = new Dictionary<int, GameObject>()
        {
            {1, femaleGPrefab},
            {2, femaleKPrefab},
            {3, maleGPrefab},
            {4, maleKPrefab},
            {5, doctorPrefab},
            {6, elderPrefab},
            {7, boyPrefab}
        };
    }

    public void Spawn(Vector3 positionNewClient, Vector3 clientsDestination)
    {
        int numberNewClient = UnityEngine.Random.Range(1, 7);
        GameObject newClient2Spawn = prefabDictionary[numberNewClient];

        GameObject newClient = Instantiate(newClient2Spawn, positionNewClient, Quaternion.identity);
        Client client = newClient.GetComponent<Client>();
        client.SetDestination(clientsDestination);
        clients.Add(client);
        newClientSpawned.Invoke(client);
    }

    public void DestroyFirstClient()
    {
        Destroy(clients[0].gameObject);
    }
}
