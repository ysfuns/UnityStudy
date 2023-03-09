using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float Speed = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //float X = Input.GetAxis("Horizontal");
        //float Z = Input.GetAxis("Vertical");
        //transform.Translate(X * Time.deltaTime * Speed, 0, Z * Time.deltaTime * Speed);

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * Speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * Speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * Speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * Speed);
        }
    }
}
