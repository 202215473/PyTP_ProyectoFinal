using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSpawner : MonoBehaviour
{
    public GameObject imagePrefab;
    
    public GameObject Spawn(Vector2 imagePosition, GameObject map)
    {
        GameObject image = Instantiate(imagePrefab, imagePosition, Quaternion.identity);
        image.transform.SetParent(map.transform, false);
        return image;
    }
}
