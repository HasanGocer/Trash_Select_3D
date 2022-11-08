using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoSingleton<ObjectManager>
{
    //managerde bulunacak
    
    [SerializeField] private GameObject tempalteItem;


    [System.Serializable]
    public class Object›nGame
    {
        public List<GameObject> gameObject›nGame = new List<GameObject>();
        public GameObject stackOutPlace;
    }
    public Object›nGame[] object›nGame;


    public void Awake()
    {
        object›nGame = new Object›nGame[tempalteItem.transform.childCount];
    }
}
