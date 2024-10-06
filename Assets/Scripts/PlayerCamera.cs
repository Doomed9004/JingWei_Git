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
        //Vector3 cursorLocalPos = Input.mousePosition;//获取鼠标在屏幕空间的坐标
        //Vector3 cursorWorldPos = playerCamera.ScreenToWorldPoint(new Vector3(cursorLocalPos.x,cursorLocalPos.y,player.position.z));//转换成世界坐标
        //Vector3 camaraWorldPos=(player.position+cursorWorldPos)/ 2;//取光标到角色的中点
        ////Debug.Log(camaraWorldPos);

        //playerCamera.transform.position = camaraWorldPos + new Vector3(0, 0, -distance);//相机离角色的距离

        playerCamera.transform.position = player.position + new Vector3(0, 0, -distance);
        playerCamera.transform.LookAt(player);
    }
}
