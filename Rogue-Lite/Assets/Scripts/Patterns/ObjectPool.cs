using System;
using System.Collections.Generic;
using UnityEngine;

namespace GoldPillowGames.Patterns
{
    public class ObjectPool : MonoBehaviour
    {
        #region Singleton
        private static ObjectPool _instance;
        private static readonly object Lock = new object();
        private static bool _isShuttingDown = false;
        public static ObjectPool Instance
        {
            get
            {
                if (_isShuttingDown)
                {
                    return null;
                }
                
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        var auxGameObject = new GameObject();
                        _instance = auxGameObject.AddComponent<ObjectPool>();
                        DontDestroyOnLoad(auxGameObject);
                    }

                    return _instance;
                }
            }
        }

        private void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }


        private void OnDestroy()
        {
            _isShuttingDown = true;
        }
        #endregion
        
        private readonly Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

        public GameObject GetObject(GameObject prefabRequested)
        {
            if (_pools.TryGetValue(prefabRequested.name, out Queue<GameObject> objectList))
            {
                if (objectList.Count == 0)
                {
                    return CreateNewObject(prefabRequested);
                }
                else
                {
                    var objectGotten = objectList.Dequeue();
                    objectGotten.SetActive(true);
                    return objectGotten;
                }
            }
            else
            {
                return CreateNewObject(prefabRequested);
            }
        }

        private GameObject CreateNewObject(GameObject newPrefab)
        {
            GameObject newObject = Instantiate(newPrefab);
            newObject.name = newPrefab.name;
            return newObject;
        }

        public void ReturnObject(GameObject objectReturned)
        {
            if (_pools.TryGetValue(objectReturned.name, out Queue<GameObject> objectList))
            {
                objectList.Enqueue(objectReturned);
            }
            else
            {
                Queue<GameObject> newPool = new Queue<GameObject>();
                newPool.Enqueue(objectReturned);
                _pools.Add(objectReturned.name, newPool);
            }
        }
    }
}

