using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScript : MonoBehaviour
{
    [SerializeField]
    UiScript UiS;
    [SerializeField]
    TileGen TileGenS;

    Rigidbody rb;
    public float spd;
    public bool keyToMove;
    Vector3 moveVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        keyToMove = false;
    }


    void Update()
    {
        if (transform.position.y < -5)
        UiS.GameOver(); 

        if (Input.anyKey && !keyToMove)
        {
            keyToMove = true;
            moveVector = MoveInDirection(0);
        }

        if(Input.GetKey(KeyCode.W))
            moveVector = MoveInDirection(0);
        if (Input.GetKey(KeyCode.A))
            moveVector = MoveInDirection(1);
        if (Input.GetKey(KeyCode.D))
            moveVector = MoveInDirection(2);

        if (keyToMove)
            rb.MovePosition(transform.position + moveVector);
    }

    Vector3 MoveInDirection(int dir)
    {
        Vector3 Vect = new Vector3(0, 0, 1);
        switch (dir)
        {
            default:
                Vect = new Vector3(0, 0, 1);
                break;
            case 1:
                Vect = new Vector3(-1, 0, 0);
                break;
            case 2:
                Vect = new Vector3(1, 0, 0);
                break;
        }
        Vect = Vect.normalized * spd * Time.deltaTime;
        return Vect;
    }

    private void OnCollisionExit(Collision collision)
    {
        TileGenS.DestroyLastTile(collision.gameObject); //when we stop touching some tile we try to delete it
    }

    private void OnTriggerEnter(Collider other)
    {
        UiS.AddScore();
        other.gameObject.GetComponent<CrystalScript>().PickedUp(); //destroy crystals when we touch them 
    }

}
