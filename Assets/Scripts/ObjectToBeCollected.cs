using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToBeCollected : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject baton, sword, apple, meat, gold, redDiamond, yellowDiamond, blueDiamond, purse, money;
    public Item.ItemType type;
    public Item item;
    void Start()
    {

        item = new Item(type);
        GameObject g = new GameObject();

        switch (type)
        {
            case Item.ItemType.BATON: g = baton;break;
            case Item.ItemType.SWORD: g = sword;break;            
            case Item.ItemType.APPLE: g = apple;break;            
            case Item.ItemType.MEAT: g = meat; break;           
            case Item.ItemType.GOLD: g = gold;break;            
            case Item.ItemType.RED_DIAMOND: g = redDiamond; break;           
            case Item.ItemType.YELLOW_DIAMOND: g = yellowDiamond; break;                           
            case Item.ItemType.BLUE_DIAMOND: g = blueDiamond;break;
            case Item.ItemType.PURSE: g = purse; break;
            case Item.ItemType.MONEY: g = money; break;
            default:break;
        }

        GameObject g1 = Instantiate(g, transform.position, Quaternion.identity);
        g1.transform.parent = transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
