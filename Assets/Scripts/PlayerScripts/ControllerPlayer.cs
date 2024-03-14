using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerPlayer : MonoBehaviour
{
    private Animator animator;
    public float jumpForce = 10f; // Lực nhảy của nhân vật
    public int isRight = 0;
    public GameObject boomPrefab;
    public float boomOffset = 0.1f; // Khoảng cách offset khi thả boom
    private bool isCollidingWithEnemy = false;

    private bool isCollidingWithCucumber = false;

    private bool isCollidingWithWhale = false;
    private bool isCollidingWithCaptain = false;
    private bool isCollidingWithBigguy = false;

    public int health;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    private int demQuaiVat; // Bộ đếm cho số lượng quái vật

    private OpenDoor doorScript; // Tham chiếu đến script của cửa
    public int maune = 0;

    private float timeToChangeScene = 2f; // Thời gian chờ trước khi chuyển màn hình
    private bool isGameOver = false; // Biến để kiểm tra xem trò chơi đã kết thúc chưa

    public Image _fill;

    // public Transform sungne;
    private Vector3 initialPosition;
  

    public Transform trai; // lưu trữ tạo viên đạn

    public Transform phai; // lưu trữ tạo viên đạn

    void Start()
    {
        animator = GetComponent<Animator>();
        health = 3;
        demQuaiVat = 0;
        health = PlayerPrefs.GetInt("Health", 3);
        initialPosition = transform.position;
    }


    void Update()
    {
        
        if (transform.position.y < -10f)
        {
            // Gọi hàm để đặt lại vị trí nhân vật
            ResetPlayerPosition();
        }

        Vector3 vector3 = transform.position;
        vector3.y = vector3.y + 0.5f;
        Vector3 ten = transform.position;
        ten.y = ten.y -2f;
       

        if (health >= 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            PlayerPrefs.SetInt("Health", health);
            PlayerPrefs.Save();
        }
        else if (health == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
            PlayerPrefs.SetInt("Health", health);
            PlayerPrefs.Save();
        }
        else if (health == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
            PlayerPrefs.SetInt("Health", health);
            PlayerPrefs.Save();
        }
        else if (health <= 0)
        {
            animator.SetInteger("chet", 1);
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            SceneManager.LoadScene("Manchoi1");
            if (health <= 0)
            {
                health = 3;
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                PlayerPrefs.SetInt("Health", health);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetInteger("chay", 1);
            transform.Translate(Time.deltaTime * 2.5f, 0, 0);
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;
            isRight = 1;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetInteger("chay", 1);
            transform.Translate(-Time.deltaTime * 2.5f, 0, 0);
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;
            isRight = 1;
        }
        else
        {
            animator.SetInteger("chay", 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isRight = 1;
            animator.SetInteger("nhay", 1);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else
        {
            isRight = -1;
            animator.SetInteger("nhay", 0);
        }

        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     GameObject bullettmp = Instantiate(this.boomPrefab, phai.position, Quaternion.identity);
        //     Rigidbody2D rb = bullettmp.GetComponent<Rigidbody2D>(); // thêm lực cho viên đạn
        //     rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse); // thêm lực, hướng đạn
        //     // Instantiate(boomPrefab, phai.position, Quaternion.identity);
        //
        //     GameObject bullettmp2 = Instantiate(this.boomPrefab, trai.position, Quaternion.identity);
        //     Rigidbody2D rb2 = bullettmp2.GetComponent<Rigidbody2D>(); // thêm lực cho viên đạn
        //     // Để bắn bom sang bên trái, thay đổi hướng lực
        //     rb2.AddForce(transform.right * -bulletForce, ForceMode2D.Impulse); // thêm lực, hướng đạn
        // }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Gọi hàm nhảy
            Vector3 boomPosition = transform.position;
            // Sử dụng transform.localScale.x để xác định hướng bom
            float boomDirection = transform.localScale.x;
            // Đặt vị trí bom dựa trên hướng của nhân vật và thêm offset
            boomPosition.x += boomDirection * (boomOffset - 1.25f); // Thay đổi giá trị offset theo ý muốn
            Instantiate(boomPrefab, boomPosition, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DropRandomBomb();
        }

        // Kiểm tra liên tục nếu có va chạm với Enemy
        if (isCollidingWithEnemy)
        {
            Debug.Log("Liên tục va chạm với Enemy");
            animator.SetTrigger("Doorout");
        }

        // Kiểm tra liên tục nếu có va chạm với Enemy
        if (isCollidingWithCucumber)
        {
            Debug.Log("Liên tục va chạm với Cucumber");
            animator.SetTrigger("Doorout");
        }


        // Kiểm tra liên tục nếu có va chạm với Whale
        if (isCollidingWithWhale)
        {
            Debug.Log("Liên tục va chạm với Whale");
            animator.SetTrigger("Doorout");
        }

        if (isCollidingWithCaptain)
        {
            Debug.Log("Liên tục va chạm với Captain");
            animator.SetTrigger("Doorout");
        }

        if (isCollidingWithBigguy)
        {
            Debug.Log("Liên tục va chạm với Bigguy");
            animator.SetTrigger("Doorout");
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health = health - 1;
            Debug.Log("Bắt đầu va chạm với Enemy");
            isCollidingWithEnemy = true;
        }

        if (collision.gameObject.CompareTag("Cucumber"))
        {
            health = health - 1;
            Debug.Log("Bắt đầu va chạm với Cucumber");
            isCollidingWithCucumber = true;
        }


        if (collision.gameObject.CompareTag("Whale"))
        {
            health = health - 1;
            Debug.Log("Bắt đầu va chạm với Whale");
            isCollidingWithWhale = true;
        }

        if (collision.gameObject.CompareTag("Captain"))
        {
            health = health - 1;
            Debug.Log("Bắt đầu va chạm với Whale");
            isCollidingWithCaptain = true;
        }


        if (collision.gameObject.CompareTag("Bigguy"))
        {
            health = health - 1;
            Debug.Log("Bắt đầu va chạm với Bigguy");
            isCollidingWithBigguy = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Kết thúc va chạm với Enemy");
            isCollidingWithEnemy = false;
        }

        if (collision.gameObject.CompareTag("Cucumber"))
        {
            Debug.Log("Kết thúc va chạm với Cucumber");
            isCollidingWithCucumber = false;
        }

        if (collision.gameObject.CompareTag("Whale"))
        {
            Debug.Log("Kết thúc va chạm với Whale");
            isCollidingWithWhale = false;
        }

        if (collision.gameObject.CompareTag("Captain"))
        {
            Debug.Log("Kết thúc va chạm với Captain");
            isCollidingWithCaptain = false;
        }

        if (collision.gameObject.CompareTag("Bigguy"))
        {
            Debug.Log("Kết thúc va chạm với Bigguy");
            isCollidingWithBigguy = false;
        }
    }

    void ResetPlayerPosition()
    {
        // Đặt lại vị trí của nhân vật về vị trí ban đầu
        transform.position = initialPosition;

        // Đặt lại velocity của nhân vậ Ft
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
    
    
    void DropRandomBomb()
    {
        // Gọi hàm nhảy
        Vector3 boomPosition = transform.position;
        // Sử dụng transform.localScale.x để xác định hướng bom
        float boomDirection = transform.localScale.x;
        // Đặt vị trí bom dựa trên hướng của nhân vật và thêm offset
        boomPosition.x += boomDirection * (boomOffset - 1.25f); // Thay đổi giá trị offset theo ý muốn
        Instantiate(boomPrefab, boomPosition, Quaternion.identity);
    }
}