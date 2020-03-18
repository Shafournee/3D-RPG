using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscountCode
{
    public string code;
    public float discount;

    public DiscountCode(string code, float discount)
    {
        this.code = code;
        this.discount = discount;
    }
}
public class ShopSystem : MonoBehaviour
{
    public List<Item> shopItems;
    public GameObject shopItemComponent;
    [SerializeField] GameObject[] shopItemComponents;
    [SerializeField] GameObject discountCodeField;
    public List<DiscountCode> discountCodes = new List<DiscountCode>();
    int totalPurchase = 0;
    int initialMoney;
    public int moneyLeft;
    float topLeftX, topLeftY;
    // Start is called before the first frame update
    void Awake()
    {
        //Init();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {

        //initialMoney = 1000;
        initialMoney = GameObject.Find("Player").GetComponent<InventorySystem>().GetMoney();
        moneyLeft = initialMoney;
        topLeftX = 20; topLeftY = 100;
        shopItems = new List<Item>();
        shopItems.Add(new Item(Item.ItemType.SWORD));
        shopItems.Add(new Item(Item.ItemType.APPLE));
        shopItems.Add(new Item(Item.ItemType.WHITE_DIAMOND));
        shopItems.Add(new Item(Item.ItemType.AXE));
        shopItems.Add(new Item(Item.ItemType.STEW));

        discountCodes.Add(new DiscountCode("discount", .8f));

        //shopItemComponents = new GameObject[shopItems.Count];
        GameObject.Find("shopMoneyLeftValue").GetComponent<Text>().text = "" + initialMoney;
        for (int i = 0; i < shopItems.Count;i++)
        {

            SetupShopItemCompomnent(i);
        }
        for (int i = shopItems.Count; i < 6; i++)
        {
            shopItemComponents[i].SetActive(false);
        }

    }

    void SetupShopItemCompomnent(int index)
    {
        shopItems[index].nb = 0;
        //shopItemComponents[index] = Instantiate(shopItemComponent, transform.position, Quaternion.identity);
        shopItemComponents[index].GetComponent<ShopItem>().index = index;

        float width = shopItemComponents[index].transform.Find("itemBg").GetComponent<RectTransform>().sizeDelta.x / 2;
        float borderAroundEachItem = 1.05f;
        shopItemComponents[index].name = "shopItem_" + index + shopItems[index].name;
        shopItemComponents[index].transform.Find("itemLabel").GetComponent<Text>().text = shopItems[index].name + "($" + shopItems[index].discountPrice + ")";
        shopItemComponents[index].transform.Find("itemQty").GetComponent<Text>().text = "" + shopItems[index].nb;

        //shopItemComponents[index].transform.SetParent(GameObject.Find("shopUI").transform);
        //shopItemComponents[index].transform.localPosition = new Vector3(topLeftX + (index % 3) * (width * borderAroundEachItem), topLeftY - (index / 3) * width * borderAroundEachItem, 0.0f);
        shopItemComponents[index].transform.Find("itemImage").GetComponent<RawImage>().texture = shopItems[index].GetTexture();




    }

    public void UpdateTotal(int itemIndex, int itemAmount)
    {

        shopItems[itemIndex].nb = itemAmount;
        int tempTotal;
        tempTotal = CalculateTotal();
        GameObject.Find("shopTotalValue").GetComponent<Text>().text = "" + tempTotal;
        totalPurchase = tempTotal;
        moneyLeft = initialMoney - tempTotal;
        GameObject.Find("shopMoneyLeftValue").GetComponent<Text>().text = "" + moneyLeft;

    }


    public int CalculateTotal()
    {
        int temp = 0;
        for (int i = 0; i < shopItems.Count; i++)
        {

            temp += shopItems[i].nb * shopItems[i].discountPrice;


        }

        return temp;


    }

    public void DiscountCode()
    {
        string enteredCode = discountCodeField.GetComponent<InputField>().text;

        for (int i = 0; i < discountCodes.Count; i++)
        {
            if(enteredCode == discountCodes[i].code)
            {
                ApplyCode(discountCodes[i]);
            }
        }
    }

    void ApplyCode(DiscountCode code)
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].discountPrice = shopItems[i].originalPrice;
            shopItems[i].discountPrice = (int)(shopItems[i].discountPrice * code.discount);
            shopItemComponents[i].transform.Find("itemLabel").GetComponent<Text>().text = shopItems[i].name + "($" + shopItems[i].discountPrice + ")";
        }
    }

    public bool canAddItemsTocart(int index)
    {

        if (moneyLeft >= shopItems[index].discountPrice && shopItems[index].nb < shopItems[index].maxNb)  return true;
        else return false;

    }
}
