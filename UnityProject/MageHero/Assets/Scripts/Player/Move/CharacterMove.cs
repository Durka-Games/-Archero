using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{

    public float speedMove;

    private float gravityForce;
    private Vector3 moveVector;
    private MobileController controller;

    private CharacterController ch_controller;

    // Start is called before the first frame update
    void Start()
    {

        ch_controller = GetComponent<CharacterController>();
        controller = GameObject.FindGameObjectWithTag("Joystic").GetComponent<MobileController>();

    }

    // Update is called once per frame
    void Update()
    {

        CharacterMoving();
        GameGravity();

    }

    private void CharacterMoving()
    {

        moveVector = Vector3.zero;
        moveVector.x = controller.Horizontal() * speedMove;
        moveVector.z = controller.Vertival() * speedMove;


        if(Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {

            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);

        }


        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime);

    }

    private void GameGravity()
    {

        if (!ch_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;

    }

}
