using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlPlayer : MonoBehaviour
{
    float speed, rotatioAroundY;
    Animator anim;
    CharacterController controller;
    AnimatorStateInfo info;
    bool isTalking = false;
    GameObject objectToPickUp;
    bool itemToPickUpNearBy = false;
    GameObject userMessage;
    GameObject weapon;
    bool weaponIsActive = false;

    public bool shopIsDisplayed;

    GameObject healthUI, skillsUI, shopUI;
    [Header("Health Settings")]
    [Tooltip("Health value between 0 and 100")]
    public int health = 50;

    public void IncreaseHealth(int amount)
    {

        health += amount;
        if (health > 100) health = 100;
        print("Health: " + health);
        GameObject.Find("healthBar").GetComponent<ManageBar>().SetValue(health);

    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        userMessage = GameObject.Find("userMessage");
        userMessage.SetActive(false);
        GameObject.Find("healthBar").GetComponent<ManageBar>().SetValue(health);
        shopUI = GameObject.Find("shopUI");
        shopUI.SetActive(false);

        weapon = GameObject.Find("playerWeapon").gameObject;
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shopIsDisplayed)
        {
            info = anim.GetCurrentAnimatorStateInfo(0);
            speed = Input.GetAxis("Vertical") * 4f;
            rotatioAroundY = Input.GetAxis("Horizontal");
            anim.SetFloat("speed", speed);
            gameObject.transform.Rotate(0, rotatioAroundY, 0);
            if (speed > 0) controller.Move(transform.forward * speed * 2.0f * Time.deltaTime);

            if (itemToPickUpNearBy)
            {

                if (Input.GetKeyDown(KeyCode.Y)) PickUpObject1();
                if (Input.GetKeyDown(KeyCode.N))
                {
                    GameObject.Find("userMessageText").GetComponent<Text>().text = "";
                    userMessage.SetActive(false);
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {

            weaponIsActive = !weaponIsActive;
            if (weaponIsActive) anim.SetTrigger("useWeapon");
            else anim.SetTrigger("putWeaponBack");

        }
        if (info.IsName("UseWeapon"))
        {
            if (info.normalizedTime % 1.0 >= .50)
            {
                weapon.SetActive(true);

            }

        }
        if (info.IsName("PutWeaponBack"))
        {
            if (info.normalizedTime % 1.0 >= .50)
            {
                weapon.SetActive(false);

            }
            else weapon.SetActive(true);

        }

        if (Input.GetButtonDown("Fire1"))
        {

            if (weaponIsActive) anim.SetTrigger("attackWithWeapon");

        }

        //if(Input.GetKeyDown(KeyCode.B))
        //{
        //GameObject.Find("shopSystem").GetComponent<ShopSystem>().Init();
        //}
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "Diana" && !isTalking)
        {

            StartDialogue(hit);

        }
        else if (hit.collider.gameObject.name == "Mayor" && !isTalking)
        {

            StartDialogue(hit);

        }
    }

    void StartDialogue(ControllerColliderHit hit)
    {
        hit.collider.gameObject.GetComponent<DialogueSystem>().startDialogue();
        isTalking = true;
        anim.SetFloat("speed", 0);
        speed = 0;
        hit.collider.isTrigger = true;
        hit.collider.gameObject.GetComponent<BoxCollider>().size = new Vector3(2, 1, 2);
    }

    public void EndTalking()
    {

        isTalking = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "itemToBeCollected")
        {


            objectToPickUp = other.gameObject;
            itemToPickUpNearBy = true;
            PickUpObject2();

        }
        if (other.gameObject.name == "shop")
        {

            shopIsDisplayed = true;
            anim.SetFloat("speed", 0);
            displayShopUI();
            GameObject.Find("shopSystem").GetComponent<ShopSystem>().Init(); ;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        itemToPickUpNearBy = false;
        if (userMessage.activeInHierarchy)
        {

            GameObject.Find("userMessageText").GetComponent<Text>().text = "";
            userMessage.SetActive(false);
        }
    }

    void PickUpObject1()
    {
        if (GetComponent<InventorySystem>().UpdateItem(objectToPickUp.GetComponent<ObjectToBeCollected>().type, 1))
        {
            Destroy(objectToPickUp);
            itemToPickUpNearBy = false;
            StartCoroutine(showPickedUpMessage());

        }
        else
        {
            string message = "You cant pickup this item as you have reached thelimit";
            GameObject.Find("userMessageText").GetComponent<Text>().text = message;
        }


    }

    void PickUpObject2()
    {
        print("Collected");

        userMessage.SetActive(true);
        string article = objectToPickUp.GetComponent<ObjectToBeCollected>().item.article;
        string message = "You just found " + article + " " + objectToPickUp.GetComponent<ObjectToBeCollected>().item.name + "\n Collect? (y/n)";
        GameObject.Find("userMessageText").GetComponent<Text>().text = message;
    }

    public void displayShopUI()
    {

        shopUI.SetActive(true);

    }

    IEnumerator showPickedUpMessage()
    {
        string article = objectToPickUp.GetComponent<ObjectToBeCollected>().item.article;
        string message = "You just picked up " + article + " " + objectToPickUp.GetComponent<ObjectToBeCollected>().item.name;
        GameObject.Find("userMessageText").GetComponent<Text>().text = message;
        yield return new WaitForSeconds(1.5f);
        userMessage.SetActive(false);
    }

    public void DecreaseHealth(int amount)
    {

        health -= amount;
        if (health <= 0) health = 0;
        GameObject.Find("healthBar").GetComponent<ManageBar>().SetValue(health);
        if (health <= 0)
        {

            health = 50;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }
}
