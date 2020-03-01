using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    float speed, rotatioAroundY;
    Animator anim;
    CharacterController controller;
    AnimatorStateInfo info;

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
        speed = Input.GetAxis("Vertical");
        rotatioAroundY = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", speed);
        gameObject.transform.Rotate(0, rotatioAroundY, 0);
        if (speed > 0) controller.Move(transform.forward * speed * 2.0f * Time.deltaTime);
    }
}
