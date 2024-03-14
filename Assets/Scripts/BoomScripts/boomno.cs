using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boomno : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;

    public int health = 3;
    private AudioSource audioSource; // Thêm biến audioSource

    //------
    public float filedoImpact;
    public float force;
    public LayerMask LaytoHit;

    private bool isCollidingWithBigguy = false;

    // Start is called before the first frame update
    private ControllerPlayer controllerPlayer;
    private OpenDoor doorScript; // Tham chiếu đến script của cửa


    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Gán audioSource từ thành phần AudioSource
        _animator = GetComponent<Animator>();

        // Tìm đối tượng có script ControllerPlayer kế nối
        controllerPlayer = FindObjectOfType<ControllerPlayer>();

        if (controllerPlayer == null)
        {
            Debug.LogError("ControllerPlayer script not found!");
        }

        doorScript = GameObject.FindGameObjectWithTag("Door").GetComponent<OpenDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            explore();
            Destroy(gameObject);
        }
    }

    void explore()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, filedoImpact, LaytoHit);
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, filedoImpact);
        doorScript = GameObject.FindGameObjectWithTag("Door").GetComponent<OpenDoor>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Phát âm thanh khi bom nổ
        if (audioSource != null)
        {
            audioSource.Play();
        }
        // Xử lý va chạm của bom với các đối tượng khác (Enemy, Cucumber, Whale)
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Cucumber"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Whale"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Captain"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bigguy"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Player") && controllerPlayer != null)
            {
                // Trừ 1 từ health của người chơi
                controllerPlayer.health = Mathf.Max(0, controllerPlayer.health - 1);
                Destroy(gameObject); // Hủy đối tượng bom
            }
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