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
    private List<GameObject> espinhos = new List<GameObject>();
    // private GameObject espinho1;
    // private GameObject espinho2;
    // private GameObject espinho3;
    // private GameObject espinho4;

    public GameObject enemy;
    private List<GameObject> enemies = new List<GameObject>();
    // private GameObject enemy1;
    // private GameObject enemy2;
    // private GameObject enemy3;
    // private GameObject enemy4;

    public GameObject moeda;
    private List<GameObject> moedas = new List<GameObject>();
    // private GameObject moeda1;
    // private GameObject moeda2;
    // private GameObject moeda3;
    // private GameObject moeda4;

    public float minObsY;
    public float maxObsY;

    public float minObsSpacing;
    public float maxObsSpacing;
    public int numberOfObjects = 5;

    // Start is called before the first frame update
    void Start()
    {   
        // espinho1 = GenerateObstacleEspinho(player.transform.position.x + 10);
        // espinho2 = GenerateObstacleEspinho(espinho1.transform.position.x + 10);
        // espinho3 = GenerateObstacleEspinho(espinho2.transform.position.x + 10);
        // espinho4 = GenerateObstacleEspinho(espinho3.transform.position.x + 10);
        
        // enemy1 = GenerateObstacleEnemy(player.transform.position.x + 5);
        // enemy2 = GenerateObstacleEnemy(enemy1.transform.position.x + 5);
        // enemy3 = GenerateObstacleEnemy(enemy2.transform.position.x + 5);
        // enemy4 = GenerateObstacleEnemy(enemy3.transform.position.x + 5);

        // moeda1 = GenerateObstacleMoeda(player.transform.position.x + 3);
        // moeda2 = GenerateObstacleMoeda(moeda1.transform.position.x + 4);
        // moeda3 = GenerateObstacleMoeda(moeda2.transform.position.x + 5);
        // moeda4 = GenerateObstacleMoeda(moeda3.transform.position.x + 5);

        Vector3 lastEspinhoPosition = player.transform.position;
        Vector3 lastEnemyPosition = player.transform.position;
        Vector3 lastMoedaPosition = player.transform.position;

        for (int i = 0; i < numberOfObjects; i++)
        {
            espinhos.Add(GenerateObstacleEspinho(lastEspinhoPosition.x));
            lastEspinhoPosition = espinhos[i].transform.position;

            enemies.Add(GenerateObstacleEnemy(lastEnemyPosition.x));
            lastEnemyPosition = enemies[i].transform.position;

            moedas.Add(GenerateObstacleMoeda(lastMoedaPosition.x));
            lastMoedaPosition = moedas[i].transform.position;
        }
    }

    GameObject GenerateObstacleEspinho(float referenceX)
    {
        GameObject obstacle = GameObject.Instantiate(espinho);
        SetTransformEspinho(obstacle, referenceX);
        return obstacle;
    }

    GameObject GenerateObstacleEnemy(float referenceX)
    {
        GameObject obstacle = GameObject.Instantiate(enemy);
        SetTransformEnemy(obstacle, referenceX);
        return obstacle;
    }

    GameObject GenerateObstacleMoeda(float referenceX)
    {
        GameObject obstacle = GameObject.Instantiate(moeda);
        SetTransformMoeda(obstacle, referenceX);
        return obstacle;
    }

    void SetTransformEspinho(GameObject obstacle, float referenceX)
    {
        // ideal -0.5f
        obstacle.transform.position = new Vector3(referenceX + Random.Range(minObsSpacing, maxObsSpacing), -0.5f, 0); 
    }

    void SetTransformEnemy(GameObject obstacle, float referenceX)
    {
        // ideal Random.Range(minObsY, maxObsY)
        obstacle.transform.position = new Vector3(referenceX + Random.Range(minObsSpacing, maxObsSpacing), Random.Range(minObsY, maxObsY), 0); 
    }

    void SetTransformMoeda(GameObject obstacle, float referenceX)
    {
        // ideal Random.Range(minObsY, maxObsY)
        obstacle.transform.position = new Vector3(referenceX + Random.Range(minObsSpacing, maxObsSpacing), Random.Range(minObsY, maxObsY), 0); 
        obstacle.SetActive(true);
    }

    void ShiftAndTransform(List<GameObject> obstacles, System.Action<GameObject, float> setTransform)
    {
        if (player.transform.position.x > obstacles[1].transform.position.x)
        {
            GameObject tempObstacle = obstacles[0];

            for (int i = 1; i < obstacles.Count; i++)
            {
                obstacles[i - 1] = obstacles[i];
            }

            obstacles[obstacles.Count - 1] = tempObstacle;

            setTransform(obstacles[obstacles.Count - 1], obstacles[obstacles.Count - 2].transform.position.x);
        }
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

        // if (player.transform.position.x > espinho2.transform.position.x)
        // {
        //     var tempObstacle = espinho1;
        //     espinho1 = espinho2;
        //     espinho2 = espinho3;
        //     espinho3 = espinho4;

        //     SetTransformEspinho(tempObstacle, espinho3.transform.position.x);
        //     espinho4 = tempObstacle;
        // }

        // if (player.transform.position.x > enemy2.transform.position.x)
        // {
        //     var tempObstacleEnemy = enemy1;
        //     enemy1 = enemy2;
        //     enemy2 = enemy3;
        //     enemy3 = enemy4;

        //     SetTransformEnemy(tempObstacleEnemy, enemy3.transform.position.x);
        //     enemy4 = tempObstacleEnemy;
        // }

        // if (player.transform.position.x > moeda2.transform.position.x)
        // {
        //     var tempObstacleMoeda = moeda1;
        //     moeda1 = moeda2;
        //     moeda2 = moeda3;
        //     moeda3 = moeda4;

        //     SetTransformMoeda(tempObstacleMoeda, moeda3.transform.position.x);
        //     moeda4 = tempObstacleMoeda;
        // }

        ShiftAndTransform(espinhos, SetTransformEspinho);
        ShiftAndTransform(enemies, SetTransformEnemy);
        ShiftAndTransform(moedas, SetTransformMoeda); 
    }
}
