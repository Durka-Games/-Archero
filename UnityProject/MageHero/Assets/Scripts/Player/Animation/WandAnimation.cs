using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandAnimation : MonoBehaviour
{


    
    [SerializeField] private float RotateSpeed = 45f;
    [SerializeField] private float MoveSpeed = 2f;
    [SerializeField] private float MaxMove = 15f;
    [SerializeField] private float multiply;
    [SerializeField] private float site;


    private bool isDown = false;

    // Update is called once per frame
    void Update()
    {


        transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime * site);

        if (transform.localPosition.y < MaxMove * multiply && !isDown)
        {

            transform.Translate(Vector3.up * MoveSpeed * multiply * Time.deltaTime);

        }
        else
        {
            isDown = true;
            transform.Translate(Vector3.up * MoveSpeed * multiply * Time.deltaTime * (-1));

            if (transform.localPosition.y <= 0) isDown = false;

        }


    }
}
