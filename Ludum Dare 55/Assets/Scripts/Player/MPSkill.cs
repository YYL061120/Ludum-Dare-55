using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MPSkill : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Light2D spotLight;
    private Camera mainCamera;

    public GameObject MPSkill1;
    public GameObject MPSkill2;

    public LayerMask MPSkill1DetectLayerMask;

    public Transform player; // 玩家对象
    public Transform firePoint;
    public Light2D[] pointLights; // 点光源数组

    public Scrollbar MPSkill2Bar;
    public Scrollbar MPSkill1Bar;

    public float maxBarSize=5f;

    public float rotationSpeed = 50f; // 旋转速度
    public float Skill2Duration = 5f;
    public float Skill1Duration = 5f;

    public float Skill2StartTime;
    public float Skill1StartTime;

    public bool MPSkill2_using = false;
    public bool MPSkill1_using = false;

    public static bool canUseSkill2 = true;
    public static bool canUseSkill1 = true;

    void Start()
    {
        // 获取聚光灯组件
        spotLight = GetComponent<Light2D>();
        // 获取主摄像机
        mainCamera = Camera.main;

        MPSkill2.SetActive(false);  
        MPSkill1.SetActive(false);
        canUseSkill1 = true;
        canUseSkill2 = true;
    }

    void Update()
    {
        //MPSkill2 using
        if(Input.GetMouseButton(1) && canUseSkill2)  
        {
            MPSkill2.SetActive(true);
            GetSkill2StartTime();
            MPSkill2_using=true;
            StartCoroutine(StopMPSkill2());
        }

        //MPSkill1 using
        if(Input.GetMouseButton(0) && canUseSkill1)
        {
            Debug.Log("Enter successfully MPSkill1");
            MPSkill1.SetActive(true);
            GetSkillStartTime();
            MPSkill1_using = true;
            StartCoroutine(StopMPSkill1());
        }

        //update the size of MPSkill2 && MPSkill1 each frame
        if (MPSkill2_using)
        {
            MPSkill2Filter();
        }
        if (MPSkill1_using)
        {
            MPSkill1Filter();
            MPSkill1Detect();
        }

        MPSkill1Follow();
    }

    public void GetSkill2StartTime()
    {
        Skill2StartTime = Time.time;
    }

    public void MPSkill2Filter()
    {
        float skill2Progress = (Time.time - Skill2StartTime) / Skill2Duration;
        MPSkill2Bar.size = 1 - skill2Progress;
    }

    public IEnumerator StopMPSkill2()
    {
        yield return new WaitForSeconds(Skill2Duration);
        MPSkill2_using = false;
        MPSkill2.SetActive(false);
        MPSkill2Bar.size = 0;
    }

    public void GetSkillStartTime()
    {
        Skill1StartTime=Time.time;
    }

    public void MPSkill1Filter()
    {
        float skill1Progress = (Time.time - Skill1StartTime) / Skill2Duration;
        MPSkill1Bar.size = 1 - skill1Progress;
    }

    public IEnumerator StopMPSkill1()
    {
        yield return new WaitForSeconds(Skill1Duration);
        MPSkill1_using = false;
        MPSkill1.SetActive(false);
        MPSkill1Bar.size = 0;
    }

    //4 spotlights rotate
    public void MPSkill2Rotation()
    {
        // 获取玩家位置
        Vector3 playerPosition = player.position;

        // 获取当前时间
        float currentTime = Time.time;

        // 计算旋转角度
        float angle = currentTime * rotationSpeed;

        // 计算四个点光源的位置
        for (int i = 0; i < pointLights.Length; i++)
        {
            // 计算旋转角度
            float offsetAngle = angle + i * 90f;

            // 计算旋转后的位置
            float x = playerPosition.x + Mathf.Cos(offsetAngle * Mathf.Deg2Rad);
            float y = playerPosition.y + Mathf.Sin(offsetAngle * Mathf.Deg2Rad);
            Vector3 newPosition = new Vector3(x, y, playerPosition.z);

            // 更新点光源的位置
            pointLights[i].transform.position = newPosition;
        }
    }
    
    public void MPSkill1Follow()
    {
        // 获取鼠标指向的位置
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z; // 与摄像机的 z 坐标相同
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // 获取聚光灯的位置
        Vector3 lightPosition = transform.position;

        // 计算方向向量
        Vector3 direction = (worldMousePosition - lightPosition).normalized;
        float angle = Vector3.Angle(Vector3.up, direction);
        // 设置聚光灯的方向
        //if (direction.y > 0)
        //{
        //    angle = -angle;
        //}
        if (direction.x > 0)
        {
            angle = -angle;
        }
        MPSkill1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //spotLight.transform.right = direction;
    }

    public void MPSkill1Detect()
    {
        // 获取鼠标位置
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; // 设置 z 轴以适应2D游戏

        // 将鼠标位置转换为世界坐标
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 计算射线方向
        Vector2 direction = (worldMousePosition - firePoint.position).normalized;

        // 发射射线
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, Mathf.Infinity, MPSkill1DetectLayerMask);

        // 如果射线击中了物体
        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

            // 在控制台中输出击中的物体名称
            // 在这里可以执行其他操作，比如对击中的物体进行处理
        }
        else
        {
            Debug.Log("Raycast hit nothing.");
        }

        // 绘制射线以便在 Scene 视图中可视化
        Debug.DrawRay(firePoint.position, direction * 10, Color.red, 1);
    }

}
