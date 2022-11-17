using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    //upgrade edilecek nesnelere eklenecek

    [System.Serializable]
    public class ObjectClass
    {
        public GameObject item;
    }
    public ObjectClass[] objectClass;

    public int itemCount;

}
