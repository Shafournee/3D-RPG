using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardDestroyItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "itemToBeCollected")
        {
            Destroy(other.gameObject);
        }
    }
}
