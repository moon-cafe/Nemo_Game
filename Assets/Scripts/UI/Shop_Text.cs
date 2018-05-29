using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Text : MonoBehaviour {
    [SerializeField]
    private Text Shop_Title_name;
    [SerializeField]
    private Text Shop_price;
    [SerializeField]
    private Text Shop_Content_text;

    private int Shop_Code;

    [SerializeField]
    private GameObject[] Shop_List;
    [SerializeField]
    private GameObject Shop_Ganeral;

    [SerializeField]
    private GameObject Sold_Out_Image;

    [SerializeField]
    private Button Buy_Button;

    // Use this for initialization
    void Start () {
        Non_content();
        Shop_List_Setting();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Item_Code(int shop_code)
    {
        if (shop_code == 1)
        {
            Shop_Title_name.text = "Exp_Up(임시)";
            Shop_price.text = "100$";
            Shop_Content_text.text = "네모의 경험치를 증가시켜준다.";
        }
        else if (shop_code == 2)
        {
            Shop_Title_name.text = "무기(임시)";
            Shop_price.text = "300$";
            Shop_Content_text.text = "플레이어에게 무기를 장착시킨다.";
        }
        else if (shop_code == 3)
        {
            Shop_Title_name.text = "방어구1(임시)";
            Shop_price.text = "450$";
            Shop_Content_text.text = "플레이어에게 방어구1을 장착시킨다.";
        }
        else if (shop_code == 4)
        {
            Shop_Title_name.text = "방어구2(임시)";
            Shop_price.text = "400$";
            Shop_Content_text.text = "플레이어에게 방어구2를 장착시킨다.";
        }
        else
            Non_content();

        Shop_Code = shop_code;
    }

    public void Non_content()
    {
        Shop_Title_name.text = "?";
        Shop_price.text = "?";
        Shop_Content_text.text = "?";
        Shop_Code = 0;
    }
    

    public void Item_Buy()
    {
        for (int i = 0; i < Shop_List.Length; i++)
        {
            if (Shop_Code == i)
            {
                if (Coin_text.Instance.coin >= Item_Manager.Instance.item_data[i].Item_Price && Item_Manager.Instance.item_data[i].Item_Buy_Check == false)
                {
                    //Sold_Out_Image.transform.position = Shop_List[i].transform.position;
                    Shop_List[i].GetComponent<Button>().interactable = false;
                    Coin_text.Instance.Coin_Plus(-Item_Manager.Instance.item_data[i].Item_Price);

                    Item_Manager.Instance.item_data[i] = new Item_Data(true);
                    Debug.Log(Item_Manager.Instance.item_data[i].Item_Price);
                }
            }
        }     

        
    }



    public void Shop_List_Setting()
    {
      int shop_list_index= Shop_Ganeral.transform.childCount+1;
        Shop_List = new GameObject[shop_list_index];
        for(int i=1;i<shop_list_index;i++)
        {
            Shop_List[i] = Shop_Ganeral.transform.GetChild(i-1).gameObject;
        }
    }
}
