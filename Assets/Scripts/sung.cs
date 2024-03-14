using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sung : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos; // lưu trữ tạo viên đạn

    public float TimeBtwfire = 1f; // đạn bắn nhanh hay chậm
    public float bulletForce;
    private float timeBtwfire;
    private float elapsedTime;
    void Update()
    {
        bazokaGun();
        elapsedTime += Time.deltaTime;

        // Bắn súng sau 1 giây
        if (elapsedTime >= TimeBtwfire)
        {
            bansung();
            elapsedTime = 0f; // Đặt lại thời gian đếm
        }
        
    }

    void bazokaGun()
    {
        Vector3 vitrichuot = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Lấy vị trí con chuột trên màn hình
        Vector2 vitrinhanvat = vitrichuot - transform.position;  // lấy vector từ nhân vật sang con chuột
        float angle = Mathf.Atan2(vitrinhanvat.y, vitrinhanvat.x) * Mathf.Rad2Deg; // góc trong hệ radian chuyển sang hệ degree
        Quaternion rotation = Quaternion.Euler(0, 0, angle); // thay đổi súng bằng góc Z
        transform.rotation = rotation;
   
    }

    void bansung()
    {
        //
        // timeBtwfire = TimeBtwfire;
        // GameObject bullettmp = Instantiate(this.bullet, firePos.position, Quaternion.identity);
        // Rigidbody2D rb = bullettmp.GetComponent<Rigidbody2D>(); // thêm lực cho viên đạn
        // rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse); // thêm lực, hướng đạn
        // bullettmp.tag = "dan";
    }

}
