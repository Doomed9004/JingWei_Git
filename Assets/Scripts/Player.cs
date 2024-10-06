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
    public float jumpCooldown;//跳跃冷却
    public float airMultiplier;//空气系数 在空中修正位移

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

        //跳跃时机
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);//延迟
        }
    }
    void PlayerMove()
    {
        dir = transform.right * dx;//角色当前方向
        //localDir = transform.InverseTransformDirection(dir);

        //如果在地面 那么施加力
        if (grounded)
        {
            playerRig.AddForce(dir.normalized * moveSpeed, ForceMode.Force);//归一化方向向量 乘速度
        }
        else if (!grounded)
        {
            playerRig.AddForce(dir.normalized * moveSpeed * airMultiplier, ForceMode.Force);//速度乘空气系数 在空中修正位移
        }
    }
    void CheckGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, groundLayer);

        //如果在地面就增加阻力
        if (grounded)
        {
            playerRig.drag = groundDrag;
        }
        else
        {
            playerRig.drag = airDrag;
        }
    }
    void SpeedControl(float speed)//控制玩家速度不超过speed
    {
        Vector3 flatVel = new Vector3(playerRig.velocity.x, 0, playerRig.velocity.z);

        //如果玩家当前的速度大于 speed 那么将玩家的速度矢量赋值为 speed
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            playerRig.velocity = new Vector3(limitedVel.x, playerRig.velocity.y, limitedVel.z);
        }
    }
    void Jump()
    {
        //重置 velocity.y
        playerRig.velocity = new Vector3(playerRig.velocity.x, 0, playerRig.velocity.z);

        playerRig.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }

}
