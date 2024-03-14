using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletquai : MonoBehaviour
{
    public int bossCollisionCount = 0;
    public int maxBossCollisions = 10;
    public int numberOfCoins = 1; // Số lượng đồng xu muốn sinh ra
    void Start()
    {
    
    }
    void Update()
    {
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Va chạm với con Enemy");
            Destroy(other.gameObject, 0.1f);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 3f);
        }
    }
}
