using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Chao;

    public GameObject prevChao;

    public GameObject Teto;

    public GameObject prevTeto;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > Chao.transform.position.x){
            var tempTeto = prevTeto;
            var tempChao = prevChao;
            prevTeto = Teto;
            prevChao = Chao;

            tempTeto.transform.position += new Vector3(20,0,0);
            tempChao.transform.position += new Vector3(20,0,0);

            Teto = tempTeto;
            Chao = tempChao;
        }
        
    }
}
