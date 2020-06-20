using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGen : MonoBehaviour
{
    [SerializeField]
    GameObject Tile; //prefab for generated tiles
    [SerializeField] 
    int HowManyTilesOnScreen; //how many tiles will be on screen at any  time

    public List<GameObject> tileList; //list for all our tiles
    int num;            // just to keep track on created tiles
        
    void Start()
    {
        StartTheGame();
    }

    void StartTheGame()
    {
        num = 0;
        GameObject T = GameObject.Instantiate(Tile, transform.position, transform.rotation, transform);
        tileList.Add(T);  //creating the first tile 
        for (int i = 0; i < HowManyTilesOnScreen-1; i++)
        {
            GenerateTile();
        }
    }

    void GenerateTile()
    {
        num++;
        GameObject T;
        Vector3 newPos = new Vector3(0, 0, 0);
        if (Random.Range(0, 2) == 1) //making tiles appear forward or right
            newPos = new Vector3(tileList[tileList.Count - 1].transform.position.x, 0, tileList[tileList.Count - 1].transform.position.z + 2);
        else
            newPos = new Vector3(tileList[tileList.Count - 1].transform.position.x + 2, 0, tileList[tileList.Count - 1].transform.position.z);
               
        T = GameObject.Instantiate(Tile, newPos, transform.rotation, transform);
        T.name = "Tile " + num;
        tileList.Add(T);
    }

    public void DestroyLastTile(GameObject GO)
    {
        foreach (GameObject G in tileList)  //checking if the tile in the tile list (so not to destroy starting tiles)
        {
            if (G == GO)
            {
                tileList.Remove(G);
                StartCoroutine(DestroyTile(G));
                GenerateTile();
                break;
            }
        }
    }

    IEnumerator DestroyTile(GameObject T)
    {
        Material tileMat = T.GetComponent<MeshRenderer>().material;
        for (int i=0; i < 50; i++)
        {
            tileMat.SetFloat("_Cutoff", i*0.02f); //changing the Alpha cuttoff parameter in our material to make more parts of a tile invisible
            yield return new WaitForEndOfFrame();
        }
        Destroy(T);
    }

    public void GameOverStartAgain()
    {
        foreach (GameObject G in tileList)
        {
            GameObject.Destroy(G);
        }
        tileList.Clear();

        StartTheGame();
    }
}
