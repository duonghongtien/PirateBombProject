using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public event Action<GameObject> OnBulletHit;

    public int bossCollisionCount = 0;
    public int maxBossCollisions = 10;
    public GameObject coinPrefab; // Kéo Prefab của đồng xu vào đây
    public int numberOfCoins = 1; // Số lượng đồng xu muốn sinh ra
    private OpenDoor doorScript; // Tham chiếu đến script của cửa

    void Start()
    {
        doorScript = GameObject.FindGameObjectWithTag("Door").GetComponent<OpenDoor>();
    }

    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Bigguy") || other.gameObject.CompareTag("Cucumber"))
        // {
        //     Debug.Log("Va chạm với quái");
        //     Destroy(other.gameObject, 0.1f);
        //     Destroy(gameObject);
        //     // Hủy cả đối tượng có tag "BomPrefab"
        //     GameObject[] bomObjects = GameObject.FindGameObjectsWithTag("BoomPrefab");
        //     foreach (GameObject bomObject in bomObjects)
        //     {
        //         Destroy(bomObject);
        //     }
        // }
        //
        // else
        // {
        //     Destroy(gameObject, 3f);
        // }


        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Va chạm với con Enemy");
            Destroy(other.gameObject, 0.1f);
            Destroy(gameObject);
        }

        else if (other.gameObject.CompareTag("Bigguy"))
        {
            // // Sinh ra 5 đồng xu
            // for (int i = 0; i < numberOfCoins; i++)
            // {
            //     GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            //     // Đặt vị trí của đồng xu
            //     coin.transform.position = new Vector3(transform.position.x + i, transform.position.y,
            //         transform.position.z);
            //     // Có thể đặt thêm chức năng khởi đầu của đồng xu tại đây
            //     // ...
            //     // Kích hoạt đồng xu
            //     coin.SetActive(true);
            // }
            Debug.Log("Va chạm với con Bigguy");
            Destroy(other.gameObject, 0.1f);
            Destroy(gameObject);
        }

        else if (other.gameObject.CompareTag("Cucumber"))
        {
            Debug.Log("Va chạm với con Cucumber");
            Destroy(other.gameObject, 0.1f);
            Destroy(gameObject);
        }
        // else if (other.gameObject.CompareTag("boss"))
        // {
        //     Debug.Log("Va chạm với Boss");
        //     bossCollisionCount++;
        //     Debug.Log("Số lần chạm:" + bossCollisionCount);
        //     if (bossCollisionCount >= maxBossCollisions)
        //     {
        //         Destroy(other.gameObject, 0.1f); // Hủy "boss"
        //         Destroy(gameObject); // Hủy viên đạn
        //     }
        // }
        else
        {
            Destroy(gameObject, 3f);
        }


        if (other.gameObject.CompareTag("Cucumber") ||
            other.gameObject.CompareTag("Enemy") ||
            other.gameObject.CompareTag("Bigguy") ||
            other.gameObject.CompareTag("Captain") ||
            other.gameObject.CompareTag("Whale"))
        {
            if (doorScript != null)
            {
                doorScript.QuaiVatBiPhaHuy(other.gameObject.tag);
                Debug.Log("QuaiVatBiPhaHuy called for tag: " + other.gameObject.tag);
            }
        }
    }
}