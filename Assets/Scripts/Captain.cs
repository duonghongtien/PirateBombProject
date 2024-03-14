using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : MonoBehaviour
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
                    animator.SetInteger("chayCaptain", 1);
                    Vector3 directionToTarget = (player.position - transform.position).normalized;
                    directionToTarget = Vector3.ClampMagnitude(directionToTarget, 0.5f);
                    transform.Translate(directionToTarget * tocdochay * Time.deltaTime);
                    SetDialogBoxPosition(); // Cập nhật vị trí của hộp thoại theo vị trí của nhân vật Whale
                }

                if (distanceToPlayer < attackRange && !isAttacking)
                {
                    isAttacking = true;
                    animator.SetTrigger("tancongCaptain");
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
                transform.Translate(-direction * tocdochay * Time.deltaTime);
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                Debug.Log("Current Animator State: " + stateInfo.fullPathHash);
                // Sử dụng cả SetTrigger và SetInteger
                animator.SetInteger("chayCaptain", 1);
                SetDialogBoxPosition();
            }
        }
        else
        {
            animator.SetInteger("chayCaptain", 0);
            if (isChasing)
            {
                animator.SetInteger("chayCaptain", 0);
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
                // Thay đổi trạng thái của nhân vật
                animator.SetTrigger("chayloanCaptain");
                Debug.Log("Chạy Loan");
        }
    }
}