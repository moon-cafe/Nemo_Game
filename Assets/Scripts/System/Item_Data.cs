
public class Item_Data  {

    public int Item_Code { get; set; }

    public int Item_Price;

    public bool Item_Buy_Check;

    public Item_Data(bool Item_Buy_Check)
    {
        this.Item_Buy_Check = Item_Buy_Check;
    }

    public Item_Data(int item_code, int Item_Price)
    {
        this.Item_Code = item_code;
        this.Item_Price = Item_Price;
    }
    public Item_Data(int item_code, int Item_Price, bool Item_Buy_Check)
    {
        this.Item_Code = item_code;
        this.Item_Price = Item_Price;
        this.Item_Buy_Check = Item_Buy_Check;
    }


}
