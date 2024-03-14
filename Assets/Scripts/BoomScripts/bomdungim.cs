using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomdungim : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject booomanimation;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
    yield return new WaitForSeconds(0.3f);
    Instantiate(booomanimation, transform.position, Quaternion.identity);
    
    Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {   
        
    }
    
    
}
