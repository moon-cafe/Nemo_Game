using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pool : MonoBehaviour {

    public List<GameObject> pooledObjects = new List<GameObject>();

    

    [SerializeField]
    private GameObject[] objectPrefabs;

    public GameObject GetObject(string type)
    {
        foreach (GameObject go in pooledObjects)
        {
            if (go.name == type && !go.activeInHierarchy)
            {
                go.SetActive(true);

                return go;
            }
        }
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            if (objectPrefabs[i].name == type)
            {
               
                GameObject newObject = Instantiate(objectPrefabs[i]);
                newObject.name = type;
                pooledObjects.Add(newObject);
                return newObject;
            }
        }

        return null;
    }
    


    public void ReleaseObject(GameObject gameObj)
    {
        gameObj.SetActive(false);
    }

}
