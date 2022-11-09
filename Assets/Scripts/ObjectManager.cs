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
    public Object›nGame[] object›nGame;


    public void Awake()
    {
        object›nGame = new Object›nGame[tempalateItem.transform.childCount];
    }
}
