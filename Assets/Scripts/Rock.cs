using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rock : MonoBehaviour
{
    //public float targetScale;
    public Vector3 dir;
    public float startScale;
    public float endScale;
    public float tweenTime;

    public float pushForce;

    public bool boomFinish=false;
    public bool exploding = false;

    Collider collider;

    Vector3 targetScale;

    private void Start()
    {
        targetScale = new Vector3(endScale, endScale, 1);
        if (startScale<=1)
        {
            this.transform.localScale = new Vector3(startScale, startScale, startScale);
        }
        else
        {
            this.transform.localScale = new Vector3(startScale,startScale,1);
        }
        collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;

        
    }
    private void FixedUpdate()
    {
        //标记膨胀结束
        if (transform.localScale == targetScale)
        {
            boomFinish = true;
            exploding = false;
        }
        if (boomFinish)
        {
            collider.isTrigger = false;
        }
    }
    public void Boom()
    {
        LockRock();
        transform.DOScale(targetScale, tweenTime);
        exploding = true;//正在发生爆炸
        //float changeScale= Mathf.Lerp(startScale, endScale, lerpTime);
        //if (changeScale<=1)
        //{
        //    this.transform.localScale = new Vector3(changeScale, changeScale, changeScale);
        //}
        //else
        //{
        //    this.transform.localScale = new Vector3(changeScale, changeScale, 1);
        //}
    }
    void TriggerToFalse(Collider collider)
    {
        if (collider.gameObject==Player.Instance.gameObject&&boomFinish)
        {
            collider.isTrigger = false;
        }
    }
    void LockRock()
    {
        //爆炸之后将物体锁在原地
        Rigidbody rg = GetComponent<Rigidbody>();
        Destroy(rg);
    }
    void Push(Collider _other)
    {
        Rigidbody playerRb = Player.Instance.GetComponent<Rigidbody>();
        Vector3 pushDirection = (_other.transform.position - transform.position).normalized;
        playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer==3)
    //    {
    //        Boom();
    //    }
    //    Debug.Log(collision.gameObject.layer);
    //}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnEnter");
        if (other.gameObject.layer==3)
        {
            Boom();
        }
        if (other.gameObject == Player.Instance.gameObject && !boomFinish && exploding)
        {
            Push(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnExit");
        TriggerToFalse(other);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject==Player.Instance.gameObject)
        {
            collider.isTrigger = true;
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject==Player.Instance.gameObject)
    //    {
    //        Rigidbody playerRb = Player.Instance.GetComponent<Rigidbody>();
    //        Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
    //        playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
    //    }
    //}
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject == Player.Instance.gameObject)
    //    {
    //        Rigidbody playerRb = Player.Instance.GetComponent<Rigidbody>();
    //        Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
    //        playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
    //    }
    //}

}
