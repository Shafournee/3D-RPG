using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    float speed, rotatioAroundY;
    Animator anim;
    CharacterController controller;
    AnimatorStateInfo info;
    bool isTalking = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        speed = Input.GetAxis("Vertical") * 4f;
        rotatioAroundY = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", speed);
        gameObject.transform.Rotate(0, rotatioAroundY, 0);
        if (speed > 0) controller.Move(transform.forward * speed * 2.0f * Time.deltaTime);
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
}
