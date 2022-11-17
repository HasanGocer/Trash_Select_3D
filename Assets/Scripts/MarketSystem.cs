using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketSystem : MonoBehaviour
{
    [System.Serializable]
    public class Prop
    {
        public Button propButton;
        public Image propImage;
        public Image propMoneyImage;
        public Text propName;
        public Text propMoney;
        public Text propCount;
    }

    [System.Serializable]
    public class Market
    {
        public Image scrollBarTemplate;
        public Image marketImage;
        public Button marketButtton;
        public Prop[] marketField;
    }
    public Market[] market;

    [SerializeField] private GameObject upperScroll;

    [SerializeField] private int _OPScrollBarTemplateCount, _OPUpperScrollBarTemplateCount, _OPPropTemplateCount;
    [SerializeField] private float verticalPropPosPlus, horizontalMarketPosPlus, verticalPropPosTemplate, horizontalMarketPosTemplate;
    public void MarketStart()
    {
        for (int i1 = 0; i1 < market.Length; i1++)
        {
            GameObject obj1 = ObjectPool.Instance.GetPooledObject(_OPScrollBarTemplateCount);
            market[i1].scrollBarTemplate = obj1.GetComponent<Image>();
            market[i1].scrollBarTemplate.rectTransform.up = new Vector3(0, verticalPropPosTemplate + market[i1].marketField.Length * verticalPropPosPlus, 0);
            //boyut ayarla 
            //sat�n al�m i�lemi kodlar� girilir

            //marketbutton ata
            GameObject obj2 = ObjectPool.Instance.GetPooledObject(_OPUpperScrollBarTemplateCount);
            obj2.transform.SetParent(upperScroll.transform);
            Image objImage2 = obj2.GetComponent<Image>();
            objImage2.rectTransform.right = new Vector3(horizontalMarketPosTemplate + i1 * horizontalMarketPosPlus, 0, 0);
            objImage2 = obj2.transform.GetChild(0).gameObject.GetComponent<Image>();
            objImage2 = market[i1].marketImage;
            Button objButton2 = obj2.transform.GetChild(0).gameObject.GetComponent<Button>();
            objButton2 = market[i1].marketButtton;


            //�st resim ayarla
            for (int i2 = 0; i2 < market[i1].marketField.Length; i2++)
            {
                Prop prop = market[i1].marketField[i2];
                GameObject obj = ObjectPool.Instance.GetPooledObject(_OPPropTemplateCount);
                Button objButton = obj.transform.GetChild(0).GetComponent<Button>();
                objButton = prop.propButton;
                //button ata

                Image objImage = obj.transform.GetChild(1).GetComponent<Image>();
                objImage = prop.propImage;

                objImage = obj.transform.GetChild(2).GetComponent<Image>();
                objImage = prop.propMoneyImage;

                Text objText = obj.transform.GetChild(3).GetComponent<Text>();
                objText = prop.propName;

                 objText = obj.transform.GetChild(4).GetComponent<Text>();
                objText = prop.propMoney;

                objText = obj.transform.GetChild(5).GetComponent<Text>();
                objText = prop.propCount;
            } 
        }
    }
}