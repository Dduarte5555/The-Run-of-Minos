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

    public GameObject espinho;
    public GameObject espinho1;
    public GameObject espinho2;
    public GameObject espinho3;
    public GameObject espinho4;
    public GameObject enemy1;
    public GameObject enemy2;

    public float minObsY;
    public float maxObsY;

    public float minObsSpacing;
    public float maxObsSpacing;
    // Start is called before the first frame update
    void Start()
    {
        espinho1 = GenerateObstacle(player.transform.position.x + 10);
        espinho2 = GenerateObstacle(espinho1.transform.position.x + 10);
        espinho3 = GenerateObstacle(espinho2.transform.position.x + 10);
        espinho4 = GenerateObstacle(espinho3.transform.position.x + 10);
    }

    GameObject GenerateObstacle(float referenceX)
    {
        GameObject obstacle = GameObject.Instantiate(espinho);
        SetTransform(obstacle, referenceX);
        return obstacle;
    }

    void SetTransform(GameObject obstacle, float referenceX)
    {
        // ideal -0.5f
        obstacle.transform.position = new Vector3(referenceX + Random.Range(minObsSpacing, maxObsSpacing), -0.5f, 0); 
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > Chao.transform.position.x)
        {
            var tempTeto = prevTeto;
            var tempChao = prevChao;
            prevTeto = Teto;
            prevChao = Chao;

            tempTeto.transform.position += new Vector3(20,0,0);
            tempChao.transform.position += new Vector3(20,0,0);

            Teto = tempTeto;
            Chao = tempChao;
        }

        if (player.transform.position.x > espinho2.transform.position.x)
        {
            var tempObstacle = espinho1;
            espinho1 = espinho2;
            espinho2 = espinho3;
            espinho3 = espinho4;

            SetTransform(tempObstacle, espinho3.transform.position.x);
            espinho4 = tempObstacle;
        }
        
    }
}
