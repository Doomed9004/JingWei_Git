using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("PlayerBag");
                    instance = obj.AddComponent<Player>();
                }
            }
            return instance;
        }
    }

    public float shotForce;
    public float jumpForce;
    public float moveSpeed;
    public float playerHeight;
    public float groundDrag;
    public float airDrag;
    public LayerMask groundLayer;
    public float jumpCooldown;//��Ծ��ȴ
    public float airMultiplier;//����ϵ�� �ڿ�������λ��

    Vector3 dir;
    float dx;
    public bool grounded;
    bool readyToJump=true;

    Rigidbody playerRig;

    private void Start()
    {
        playerRig = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        MyInput();
        CheckGround();
        PlayerMove();
        SpeedControl(moveSpeed);
    }
    void MyInput()
    {
        dx = Input.GetAxisRaw("Horizontal");

        //��Ծʱ��
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);//�ӳ�
        }
    }
    void PlayerMove()
    {
        dir = transform.right * dx;//��ɫ��ǰ����
        //localDir = transform.InverseTransformDirection(dir);

        //����ڵ��� ��ôʩ����
        if (grounded)
        {
            playerRig.AddForce(dir.normalized * moveSpeed, ForceMode.Force);//��һ���������� ���ٶ�
        }
        else if (!grounded)
        {
            playerRig.AddForce(dir.normalized * moveSpeed * airMultiplier, ForceMode.Force);//�ٶȳ˿���ϵ�� �ڿ�������λ��
        }
    }
    void CheckGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, groundLayer);

        //����ڵ������������
        if (grounded)
        {
            playerRig.drag = groundDrag;
        }
        else
        {
            playerRig.drag = airDrag;
        }
    }
    void SpeedControl(float speed)//��������ٶȲ�����speed
    {
        Vector3 flatVel = new Vector3(playerRig.velocity.x, 0, playerRig.velocity.z);

        //�����ҵ�ǰ���ٶȴ��� speed ��ô����ҵ��ٶ�ʸ����ֵΪ speed
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            playerRig.velocity = new Vector3(limitedVel.x, playerRig.velocity.y, limitedVel.z);
        }
    }
    void Jump()
    {
        //���� velocity.y
        playerRig.velocity = new Vector3(playerRig.velocity.x, 0, playerRig.velocity.z);

        playerRig.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }

}
