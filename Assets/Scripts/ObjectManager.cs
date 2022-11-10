using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoSingleton<ObjectManager>
{
    //managerde bulunacak

    [SerializeField] private GameObject tempalateItem;


    [System.Serializable]
    public class Object›nGame
    {
        public List<GameObject> gameObject›nGame = new List<GameObject>();
    }
    public List<Object›nGame> object›nGame = new List<Object›nGame>();


    public void Awake()
    {
        for (int i = 0; i < tempalateItem.transform.childCount; i++)
        {
            Object›nGame obj = new Object›nGame();
            object›nGame.Add(obj);
        }
    }
}
