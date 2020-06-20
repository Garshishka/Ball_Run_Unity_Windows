using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{

    void Start()
    {
        if (Random.Range(0, 3) != 0)
            Destroy(gameObject);
    }

    public void PickedUp()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false; //making the model invisible and playing the particles
        GetComponent<ParticleSystem>().Play();
    }

    IEnumerator WaitingToDie()
    {
        yield return new WaitForSeconds(3f); //waiting for particles to do their thing
        Destroy(transform.parent.gameObject);
    }
}
