using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomchono : MonoBehaviour
{
    public GameObject boomno;
    public float throwForce = 10f; // Lực thả ban đầu
    public GameObject doibomne;

    void Start()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);

        // Tạo một bản sao của "boomno"
        GameObject boomInstance = Instantiate(boomno, transform.position, Quaternion.identity);
        
        // Lấy thành phần Rigidbody của "boomno"
        Rigidbody boomRigidbody = boomInstance.GetComponent<Rigidbody>();
        
        // Kiểm tra xem có thành phần Rigidbody không và áp dụng lực thả
        if (boomRigidbody != null)
        {
            boomRigidbody.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }

        // Hủy đối tượng "Boomchono"
        Destroy(gameObject);
    }


    IEnumerator doibom()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(doibomne, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator dabom()
    {
        yield return new WaitForSeconds(0.5f);
       
    }
    
    IEnumerator ngambom()
    {
        yield return new WaitForSeconds(0.5f);
       
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        // Xử lý va chạm của bom với các đối tượng khác (Enemy, Cucumber, Whale)
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Đang chạm nè");
            StartCoroutine(dabom());
        }

        if (other.gameObject.CompareTag("Cucumber"))
        {
            StartCoroutine(doibom());
        }

        if (other.gameObject.CompareTag("Whale"))
        {
            // StartCoroutine(ngambom());
            Debug.Log("Đang chạm nè");
        }
        
        if (other.gameObject.CompareTag("Captain"))
        {
            // StartCoroutine(ngambom());
            Debug.Log("Đang chạm nè");
        }
        
        if (other.gameObject.CompareTag("Bigguy"))
        {
            // StartCoroutine(ngambom());
            Debug.Log("Đang chạm nè");
        }
    }
}