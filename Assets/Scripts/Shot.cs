using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject rockPrefab;
    public float shotForce;
    public Transform rockTree;


    private void Update()
    {
        FollowPlayer();
        LookAtMouse();
        if (Input.GetMouseButtonDown(0))
        {
            ShotRock();
        }
    }
    void FollowPlayer()
    {
        this.transform.position = Player.Instance.transform.position;
    }
    void LookAtMouse()
    {
        //看向光标位置
        Vector3 mouseLocalPos = Input.mousePosition;
        mouseLocalPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseLocalPos);


        transform.LookAt(mouseWorldPos);
    }
    void ShotRock()
    {
        GameObject rockNode = Instantiate(rockPrefab, rockTree);
        rockNode.transform.position = this.transform.position;
        rockNode.GetComponent<Rigidbody>().AddForce(this.transform.forward * shotForce);

    }
}
