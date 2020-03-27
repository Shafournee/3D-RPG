using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTargetHealth : MonoBehaviour
{
    [Range(100, 1000)]
    public int health = 100;

    float hitTimer;
    bool hitFlash;
    float alpha;


    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.0f;
        gameObject.transform.Find("Sphere").GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (hitFlash)
        {
            alpha -= Time.deltaTime;
            gameObject.transform.Find("Sphere").GetComponent<Renderer>()
                      .material.color = new Color(1, 0, 0, alpha);
            if (alpha <= 0)
            {

                hitFlash = false;
                alpha = 0;
            }

        }
    }

    public void SetHealth(int health)
    {

        this.health = health;
        if (this.health <= 0)
        {

            this.health = 0;
            DestroyTarget();
        }

    }
    public int GetHealth()
    {

        return (this.health);
    }

    void DestroyTarget()
    {
        Destroy(gameObject, 5);

    }
    public void DecreaseHealth(int increment)
    {
        Debug.Log("hit");
        hitFlash = true;
        alpha = .5f;
        SetHealth(this.health - increment);
    }
}
