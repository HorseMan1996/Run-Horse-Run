using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab; // Havuzlanacak obje
        [SerializeField] private int initialSize = 10; // Ba�lang�� boyutu

        private Queue<GameObject> pool = new Queue<GameObject>();

        public static ObjectPool Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            // Ba�lang��ta objeleri olu�tur
            for (int i = 0; i < initialSize; i++)
            {
                AddObjectToPool();
            }
        }

        // Yeni obje �retip havuza ekle
        private void AddObjectToPool()
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        // Havuzdan obje al
        public GameObject GetObject()
        {
            if (pool.Count == 0)
            {
                AddObjectToPool();
            }

            var obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // Objeyi havuza geri koy
        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
