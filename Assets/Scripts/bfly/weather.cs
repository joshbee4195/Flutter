using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather : MonoBehaviour
{

    public int ID;

    public float force;

    public float forceDuration;

    public bool touched;


    public GameObject playerOBJ;

    public float count;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "wind")
        {
            ID = 1;
        }

        if (gameObject.tag == "rain")
        {
            ID = 2;
        }

        if (gameObject.tag == "plant")
        {
            ID = 3;
        }


        playerOBJ = FindObjectOfType<bflyPlayer>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //

        if (touched)
        {
            count++;

            if (count < forceDuration)
            {
                ApplyForce();
            }

            else
            {
                touched = false;
                count = 0;
            }
        }
    }

    public void ApplyForce()
    {

        //bflyPlayer P = other.gameObject.GetComponent<bflyPlayer>();

        if (ID == 1)
        {
            //change to forward dir of wind

            playerOBJ.GetComponent<Rigidbody>().AddForce(Vector3.forward.normalized * force * 10f, ForceMode.Impulse);
        }

        if (ID == 2)
        {
            playerOBJ.GetComponent<Rigidbody>().AddForce(Vector3.down.normalized * force * 10f, ForceMode.Impulse);

        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            touched = true;

            Debug.Log("pushed");

            playerOBJ.GetComponent<bflyPlayer>().Die();
        }
    }
}
