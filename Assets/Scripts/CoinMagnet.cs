using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    public float magnetRadius = 5f;
    public float magnetForce = 10f;

    private bool isActiveCoinMagnet;

    void Start() 
    {
        isActiveCoinMagnet = false;
    }

    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, magnetRadius);

        foreach (Collider2D col in hitColliders)
        {
            if (col.CompareTag("Moeda") && isActiveCoinMagnet)
            {
                // Attract the coin towards the magnet
                Vector3 direction = (transform.position - col.transform.position).normalized;
                col.transform.position += direction * magnetForce * Time.deltaTime;
            }
        }
    }

    public bool getIsActiveCoinMagnet() 
    {
        return isActiveCoinMagnet;
    }

    public void setIsActiveCoinMagnet(bool shouldActive) 
    {
        isActiveCoinMagnet = shouldActive;
    }
}
