using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public enum ItemType
    {
        APPLE = 0, MEAT = 1, GOLD = 2, RED_DIAMOND = 3, BLUE_DIAMOND = 4, YELLOW_DIAMOND = 5, SWORD = 6, BATON = 7, PURSE = 8, MONEY = 9, AXE = 10, STEW = 11,
        WHITE_DIAMOND = 12

    }

    public enum ItemFamilyType {FOOD = 0, LOOT = 1, WEAPON = 3, QUEST = 4}


    public string name, description;
    public int originalPrice, discountPrice, healthBenefits, dammage, nb, maxNb;
    public ItemType type;
    public string article;
    public ItemFamilyType familyType;


    public Item (ItemType type)
    {
        switch (type)
        {

            case ItemType.APPLE:
                name = "Apple";
                originalPrice = 50;
                discountPrice = originalPrice;
                healthBenefits = 10;
                dammage = 0;
                nb = 1;
                maxNb = 5;
                description = "A juicy Apple";
                familyType = ItemFamilyType.FOOD;
                article = "an";
                break;

            case ItemType.MEAT:
                name = "Meat";
                originalPrice = 50;
                discountPrice = originalPrice;
                healthBenefits = 30;
                dammage = 0;
                nb = 1;
                maxNb = 2;
                description = "A nice piece of cooked meat to nurture your muscles";
                familyType = ItemFamilyType.FOOD;
                article = "some";
                break;

            case ItemType.RED_DIAMOND:
                name = "Red Diamond";
                originalPrice = 250;
                discountPrice = originalPrice;
                healthBenefits = 0;
                dammage = 0;
                nb = 1;
                maxNb = 5;
                description = "A valuable diamond crafted by the best jewellers with some know magic properties";
                familyType = ItemFamilyType.LOOT;
                article = "a";
                break;

            case ItemType.YELLOW_DIAMOND:
                name = "Yellow Diamond";
                originalPrice = 200;
                discountPrice = originalPrice;
                healthBenefits = 0;
                dammage = 0;
                nb = 1;
                maxNb = 10;
                description = "A valuable diamond crafted by the best jewellers with some know magic properties";
                familyType = ItemFamilyType.LOOT;
                article = "a";
                break;

            case ItemType.BLUE_DIAMOND:
                name = "Blue Diamond";
                originalPrice = 100;
                discountPrice = originalPrice;
                healthBenefits = 0;
                dammage = 0;
                nb = 1;
                maxNb = 10;
                description = "A valuable diamond crafted by the best jewellers with some know magic properties";
                familyType = ItemFamilyType.LOOT;
                article = "a";
                break;


            case ItemType.GOLD:
                name = "Gold";
                originalPrice = 100;
                discountPrice = originalPrice;
                healthBenefits = 0;
                dammage = 0;
                nb = 1;
                maxNb = 1000;
                description = "Gold Coins";
                familyType = ItemFamilyType.LOOT;
                article = "some";
                break;


            case ItemType.SWORD:
                name = "Sword";
                originalPrice = 100;
                discountPrice = originalPrice;
                healthBenefits = 0;
                dammage = 10;
                nb = 1;
                maxNb = 1;
                description = "A powerful sword that defeats most opponents";
                familyType = ItemFamilyType.WEAPON;
                article = "a";
                break;

            case ItemType.BATON:
                name = "Baton";
                originalPrice = 50;
                discountPrice = originalPrice;
                healthBenefits = 0;
                dammage = 5;
                nb = 1;
                maxNb = 1;
                description = "A simple wooden stick that you can handle easily";
                familyType = ItemFamilyType.WEAPON;
                article = "a";
                break;

            case ItemType.PURSE:
                name = "Purse";
                originalPrice = 200;
                discountPrice = originalPrice;
                healthBenefits = 0;
                dammage = 0;
                nb = 1;
                maxNb = 1;
                description = "A purple purse. Maybe someone is looking for this?";
                familyType = ItemFamilyType.QUEST;
                article = "a";
                break;

            case ItemType.MONEY:
                name = "Money";
                originalPrice = 200;
                discountPrice = originalPrice;
                healthBenefits = 0;
                dammage = 0;
                nb = 1;
                maxNb = 1000;
                description = "Some money. Maybe someone dropped this?";
                familyType = ItemFamilyType.QUEST;
                article = "some";
                break;

            case ItemType.AXE:
                name = "Axe";
                originalPrice = 200;
                discountPrice = originalPrice;
                healthBenefits = 0;
                dammage = 150;
                nb = 1;
                maxNb = 1000;
                description = "Let me axe you a question.";
                familyType = ItemFamilyType.WEAPON;
                article = "an";
                break;

            case ItemType.STEW:
                name = "Stew";
                originalPrice = 100;
                discountPrice = originalPrice;
                healthBenefits = 75;
                dammage = 0;
                nb = 1;
                maxNb = 1000;
                description = "A nice hearty stew.";
                familyType = ItemFamilyType.FOOD;
                article = "a";
                break;

            case ItemType.WHITE_DIAMOND:
                name = "White Diamond";
                originalPrice = 1000;
                discountPrice = originalPrice;
                healthBenefits = 0;
                dammage = 0;
                nb = 1;
                maxNb = 1000;
                description = "The most rare and expensive diamond.";
                familyType = ItemFamilyType.LOOT;
                article = "a";
                break;

        }
        this.type = type;


    }


    public Texture GetTexture()
    {

        if (this.familyType == Item.ItemFamilyType.WEAPON)
        {
            return (Resources.Load<Texture2D>("weapons/" + this.name.Replace(" ", "_")));
        }
        else if (this.familyType == Item.ItemFamilyType.FOOD)
        {
            return (Resources.Load<Texture2D>("food/" + this.name.Replace(" ", "_")));
        }
        else if (this.familyType == Item.ItemFamilyType.LOOT)
        {
            return (Resources.Load<Texture2D>("loot/" + this.name.Replace(" ", "_")));
        }
        else if (this.familyType == Item.ItemFamilyType.QUEST)
        {
            return (Resources.Load<Texture2D>("quest/" + this.name.Replace(" ", "_")));
        }
        else return null;


    }

    public string ItemInfo()
    {

        string info = "name:" + this.name + ", health benefits" + this.healthBenefits + ", damage" + this.dammage + ", nb" + this.nb + ", type" + this.type;
        return info;

    }

}
