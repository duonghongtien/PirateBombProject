using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGuy : MonoBehaviour
{
    public Transform player;
    public float khoangcachplayer = 5f;
    public float tocdochay = 3f;
    public float attackRange = 1f; // Khoảng cách tấn công
    private bool hasEatenBomb = false;
    private Animator animator;
    private bool isChasing = false;
    private bool hasSpoken = false;
    private bool isAttacking = false;
    public GameObject dialogBoxPrefab;
    private GameObject dialogBoxInstance;
    public float khoangCachTimKiemTag = 5f;
    public float dialogBoxHeightOffset = 2f; // Khoảng cách offset theo trục Y

    public int isRight = 0;

    public float filedoImpact;
    public float force;

    public LayerMask LaytoHit;
    public GameObject bullet;
    public Transform firePos; // lưu trữ tạo viên đạn

    public float TimeBtwfire = 1f; // đạn bắn nhanh hay chậm
    public float bulletForce;
    private float timeBtwfire;
    public float jumpForce = 10f; // Lực nhảy của nhân vật
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dialogBoxInstance = Instantiate(dialogBoxPrefab, transform.position, Quaternion.identity);
        dialogBoxInstance.transform.parent = transform;
        dialogBoxInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        elapsedTime += Time.deltaTime;

        // Bắn súng sau 1 giây
        if (elapsedTime >= TimeBtwfire)
        {
            // bansung();
            elapsedTime = 0f; // Đặt lại thời gian đếm
        }

        GameObject bombTarget = FindNearestObjectWithTag("BoomPrefab");
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < khoangcachplayer)
        {
            if (!hasSpoken)
            {
                dialogBoxInstance.SetActive(true);
                SetDialogBoxPosition();
                isChasing = true;
                hasSpoken = true;
            }
            else
            {
                if (!isAttacking)
                {
                    animator.SetInteger("chayBigguy", 1);
                    Vector3 directionToTarget = (player.position - transform.position).normalized;
                    directionToTarget = Vector3.ClampMagnitude(directionToTarget, 0.5f);
                    transform.Translate(directionToTarget * tocdochay * Time.deltaTime);
                    SetDialogBoxPosition(); // Cập nhật vị trí của hộp thoại theo vị trí của nhân vật Whale
                }

                if (distanceToPlayer < attackRange && !isAttacking)
                {
                    isAttacking = true;
                    animator.SetTrigger("tancongBigguy");
                }
            }
        }
        else if (bombTarget != null)
        {
            if (!hasSpoken)
            {
                dialogBoxInstance.SetActive(true);
                SetDialogBoxPosition();
                isChasing = true;
                hasSpoken = true;
            }
            else
            {
                Debug.Log("Có boom");
                isRight = 1;
                Vector3 direction = (bombTarget.transform.position - transform.position).normalized;
                direction = Vector3.ClampMagnitude(direction, 0.5f);
                transform.Translate(direction * tocdochay * Time.deltaTime);
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                // Sử dụng cả SetTrigger và SetInteger
                animator.SetInteger("chayBigguy", 1);
                SetDialogBoxPosition();
            }
        }
        else
        {
            animator.SetInteger("chayBigguy", 0);
            if (isChasing)
            {
                animator.SetInteger("chayBigguy", 0);
                isChasing = false;
                hasSpoken = false;
                isAttacking = false;
                dialogBoxInstance.SetActive(false);
            }
        }
    }


    private GameObject FindNearestObjectWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        GameObject nearestObject = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject obj in objectsWithTag)
        {
            float distanceToObj = Vector3.Distance(transform.position, obj.transform.position);
            if (distanceToObj < nearestDistance && distanceToObj <= khoangCachTimKiemTag)
            {
                nearestObject = obj;
                nearestDistance = distanceToObj;
            }
        }

        return nearestObject;
    }

    private void SetDialogBoxPosition()
    {
        Vector3 dialogBoxPosition = transform.position;
        dialogBoxPosition.y += dialogBoxHeightOffset;
        dialogBoxInstance.transform.position = dialogBoxPosition;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered!");
        if (other.gameObject.CompareTag("BoomPrefab"))
        {
            Debug.Log("Chạy Loan");
            testda();
        }
    }

    void testda()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, filedoImpact, LaytoHit);
        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag("BoomPrefab"))
            {
                animator.SetTrigger("cambomBigguy");
                animator.SetTrigger("nembomBigguy");
                Vector2 direction = obj.transform.position - transform.position;

                // Góc 45 độ theo hệ trục y
                float angleInRadians = 45f * Mathf.Deg2Rad;

                // Tính toán các thành phần của vectơ lực
                float xForce = Mathf.Cos(angleInRadians);
                float yForce = Mathf.Sin(angleInRadians);

                // Tạo vectơ lực
                Vector2 forceVector = new Vector2(-xForce, -yForce);

                // Áp dụng lực vào Rigidbody của bom
                obj.GetComponent<Rigidbody2D>().AddForce(forceVector * force, ForceMode2D.Impulse);
            }
        }
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, filedoImpact);
    // }

    // void bansung()
    // {
    //     timeBtwfire = TimeBtwfire;
    //     GameObject bullettmp = Instantiate(this.bullet, firePos.position, Quaternion.identity);
    //     Rigidbody2D rb = bullettmp.GetComponent<Rigidbody2D>(); // thêm lực cho viên đạn
    //     // bullettmp.tag = "dan";
    //     rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    // }
}