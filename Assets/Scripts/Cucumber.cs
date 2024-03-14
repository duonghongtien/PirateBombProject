using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : MonoBehaviour
{
    public Transform player;
    public float khoangcachplayer = 5f;
    public float tocdochay = 3f;
    public float attackRange = 1f;
    public float distanceToPlayerWhenDroppingBomb = 2.0f;  // Khoảng cách khi thả bom xuống
    public float khoangCachTimKiemTag = 10f;  // Khoảng cách tìm kiếm tag

    private Animator animator;
    private bool isChasing = false;
    private bool hasSpoken = false;
    private bool isAttacking = false;
    public GameObject dialogBoxPrefab;
    private GameObject dialogBoxInstance;
    public GameObject bomtat;
    public int isRight = 0;
    public float dialogBoxHeightOffset = 1f;
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
                    animator.SetInteger("chayne", 1);
                    Vector3 directionToTarget = (player.position - transform.position).normalized;
                    directionToTarget = Vector3.ClampMagnitude(directionToTarget, 0.5f);
                    transform.Translate(directionToTarget * tocdochay * Time.deltaTime);
                    SetDialogBoxPosition();
                }

                if (distanceToPlayer < attackRange && !isAttacking)
                {
                    isAttacking = true;
                    animator.SetTrigger("tancongne");
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
                animator.SetInteger("chayne", 1);
                SetDialogBoxPosition();
            }
        }
        else
        {
            animator.SetInteger("chayne", 0);

            if (isChasing)
            {
                animator.SetInteger("chayne", 0);
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered!");

        if (other.gameObject.CompareTag("BoomPrefab"))
        {
            animator.SetTrigger("thoibom");
        }
    }
}
