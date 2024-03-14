using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinbandan : MonoBehaviour
{
    public float TimeBtwfire = 1f; // đạn bắn nhanh hay chậm
    public float bulletForce;
    private float timeBtwfire;
    public GameObject bullet;
    public Transform firePos; // lưu trữ tạo viên đạn
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Bắn súng sau 1 giây
        if (elapsedTime >= TimeBtwfire)
        {
            bansung();
            elapsedTime = 0f; // Đặt lại thời gian đếm
        }
    }

    void bansung()
    {
        timeBtwfire = TimeBtwfire;
        GameObject bullettmp = Instantiate(this.bullet, firePos.position, Quaternion.identity);
        Rigidbody2D rb = bullettmp.GetComponent<Rigidbody2D>(); // thêm lực cho viên đạn
        // bullettmp.tag = "dan";
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    }
}