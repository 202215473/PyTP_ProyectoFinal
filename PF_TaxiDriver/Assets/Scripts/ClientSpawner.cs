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

        foreach (var prefab in prefabDictionary)
        {
            if (prefab.Value == null)
            {
                Debug.LogError($"Prefab con clave {prefab.Key} no está asignado en el inspector.");
            }
        }
    }

    public void Spawn(Vector3 positionNewClient, Vector3 clientsDestination)
    {
        int numberNewClient = UnityEngine.Random.Range(1, 8);
        GameObject newClient2Spawn = prefabDictionary[numberNewClient];

        GameObject newClient = Instantiate(newClient2Spawn, Vector3.zero, Quaternion.identity);
        newClient.transform.position = positionNewClient;
        Client client = newClient.GetComponent<Client>();
        client.SetPosition(newClient.transform.position);
        client.SetDestination(clientsDestination);
        clients.Add(client);
        newClientSpawned.Invoke(client);
    }

    public void DestroyClient(Client client)
    {
        bool isPickedUp = client.IsPickedUp();
        if (isPickedUp)
        { 
            Destroy(client.gameObject);
            clients.Remove(client);
        }
        
    }
}
