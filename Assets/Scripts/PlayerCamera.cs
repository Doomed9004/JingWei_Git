using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public float distance;
    Camera playerCamera;

    private void Start()
    {
        playerCamera = this.GetComponent<Camera>();
    }

    private void Update()
    {
        LookAtPlayerAndCursor();   
    }

    void LookAtPlayerAndCursor()
    {
        //Vector3 cursorLocalPos = Input.mousePosition;//��ȡ�������Ļ�ռ������
        //Vector3 cursorWorldPos = playerCamera.ScreenToWorldPoint(new Vector3(cursorLocalPos.x,cursorLocalPos.y,player.position.z));//ת������������
        //Vector3 camaraWorldPos=(player.position+cursorWorldPos)/ 2;//ȡ��굽��ɫ���е�
        ////Debug.Log(camaraWorldPos);

        //playerCamera.transform.position = camaraWorldPos + new Vector3(0, 0, -distance);//������ɫ�ľ���

        playerCamera.transform.position = player.position + new Vector3(0, 0, -distance);
        playerCamera.transform.LookAt(player);
    }
}
