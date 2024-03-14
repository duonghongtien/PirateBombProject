using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    private Animator _animator;
    private int remainingCucumbers = 1;
    private int remainingEnemies = 1;
    private int remainingBigGuys = 1;
    private int remainingCaptain = 1;
    public int requiredEnemyCount = 3;
    private int remainingWhale = 1;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
    }

    public void QuaiVatBiPhaHuy(string tag)
    {
        if (tag == "Cucumber")
        {
            remainingCucumbers--;
            Debug.Log("Cucumber bị hủy");
        }

        if (tag == "Enemy")
        {
            remainingEnemies--;
            Debug.Log("Enemy bị hủy");
        }

        if (tag == "Bigguy")
        {
            remainingBigGuys--;
            Debug.Log("Bigguy bị hủy");
        }

        if (tag == "Captain")
        {
            remainingCaptain--;
            Debug.Log("Captain bị hủy");
        }

        if (tag == "Whale")
        {
            remainingWhale--;
            Debug.Log("Whale bị hủy");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (remainingCucumbers == 0 && remainingCaptain == 0 && remainingBigGuys == 0)
            {
                _animator.SetTrigger("Doorout");

                OnDoorAnimationThree();
            }

            if (remainingCucumbers == 0 && remainingEnemies == 0 && remainingBigGuys == 0)
            {
                _animator.SetTrigger("Doorout");
                OnDoorAnimationTwo();
            }

            if (remainingCaptain == 0 && remainingWhale == 0 && remainingBigGuys == 0)
            {
                _animator.SetTrigger("Doorout");
                OnDoorAnimationOne();
            }
        }
    }

    public void OnDoorAnimationTwo()
    {
        // Thực hiện các hành động cần thiết khi cổng 2 được mở
        // ...

        SceneManager.LoadScene("Manchoi2");
    }

    public void OnDoorAnimationThree()
    {
        // Thực hiện các hành động cần thiết khi cổng 3 được mở
        // ...

        SceneManager.LoadScene("Mannchoi3");
    }

    public void OnDoorAnimationOne()
    {
        // Thực hiện các hành động cần thiết khi cổng 3 được mở
        // ...

        SceneManager.LoadScene("Manchoi1");
    }
}