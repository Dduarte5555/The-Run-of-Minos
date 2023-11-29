using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("Recorde", 0F);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
