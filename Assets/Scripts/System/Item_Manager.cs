using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : Singleton<Item_Manager> {
    public List<Item_Data> item_data = new List<Item_Data>();

    public int Item_Count;


    void Start()
    {
        Item_Count = 5;
        Item_data_Start_Setting(0, 0);
        Item_data_Start_Setting(1, 100);
        Item_data_Start_Setting(2, 300);
        Item_data_Start_Setting(3, 450);
        Item_data_Start_Setting(4, 400);
    }



    public void Item_data_Start_Setting(int Item_Numb,int item_price)
    {
        item_data.Add(new Item_Data(Item_Numb,item_price, false));
        Debug.Log(item_data[Item_Numb].Item_Code);
        Debug.Log(item_data[Item_Numb].Item_Price);
        Debug.Log(item_data[Item_Numb].Item_Buy_Check);
    }


}
