using UnityEngine;

public class BaldPirate : MonoBehaviour
{
    public Transform player;
    public float khoangcachplayer = 5f;
    public float tocdochay = 3f;
    public float attackRange = 1f; // Khoảng cách tấn công

    private Animator animator;
    private bool isChasing = false;
    private bool hasSpoken = false;
    private bool isAttacking = false;
    public GameObject dialogBoxPrefab;
    private GameObject dialogBoxInstance;
    public int isRight = 0;
    public float dialogBoxHeightOffset = 1f; // Khoảng cách offset theo trục Y

    public float khoangCachTimKiemTag = 5f; // Khoảng cách tìm kiếm tag

    public float filedoImpact;
    public float force;
    public LayerMask LaytoHit;
    public float jumpForce = 10f; // Lực nhảy của nhân vật
    void Start()
    {
        animator = GetComponent<Animator>();
        dialogBoxInstance = Instantiate(dialogBoxPrefab, transform.position, Quaternion.identity);
        dialogBoxInstance.transform.parent = transform;
        dialogBoxInstance.SetActive(false);
    }

    void Update()
    {
        
        
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
                    animator.SetInteger("chay", 1);
                    Vector3 directionToTarget = (player.position - transform.position).normalized;
                    directionToTarget = Vector3.ClampMagnitude(directionToTarget, 0.5f);
                    transform.Translate(directionToTarget * tocdochay * Time.deltaTime);
                    SetDialogBoxPosition(); // Cập nhật vị trí của hộp thoại theo vị trí của nhân vật Whale
                }

                if (distanceToPlayer < attackRange && !isAttacking)
                {
                    isAttacking = true;
                    animator.SetTrigger("tancong");
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
                animator.SetInteger("chay", 1);
                SetDialogBoxPosition();
            }
        }
        else
        {
            animator.SetInteger("chay", 0);

            if (isChasing)
            {
                animator.SetInteger("chay", 0);
                isChasing = false;
                hasSpoken = false;
                isAttacking = false;
                dialogBoxInstance.SetActive(false);
            }
        }
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
            animator.SetTrigger("dabom");
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
                Vector2 direction = obj.transform.position - transform.position;

                // Góc 45 độ theo hệ trục y
                float angleInRadians = 45f * Mathf.Deg2Rad;

                // Tính toán các thành phần của vectơ lực
                float xForce = Mathf.Cos(angleInRadians);
                float yForce = Mathf.Sin(angleInRadians);

                // Tạo vectơ lực
                Vector2 forceVector = new Vector2(xForce, yForce);

                // Áp dụng lực vào Rigidbody của bom
                obj.GetComponent<Rigidbody2D>().AddForce(forceVector * force, ForceMode2D.Impulse);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, filedoImpact);
    } // Hàm này được gọi khi quái vật bị hủy
    
}