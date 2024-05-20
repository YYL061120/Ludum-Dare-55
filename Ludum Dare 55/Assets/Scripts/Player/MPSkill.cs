using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MPSkill : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Light2D spotLight;
    private Camera mainCamera;

    public GameObject MPSkill1;

    public GameObject MPSkill2_1;
    public GameObject MPSkill2_2;
    public GameObject MPSkill2_3;
    public GameObject MPSkill2_4;

    public GameObject image_MPSkill1_1;
    public GameObject image_MPSkill1_2;
    public GameObject image_MPSkill2_1;
    public GameObject image_MPSkill2_2;

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

    public static bool MPSkill2_using = false;
    public static bool MPSkill1_using = false;

    public static bool canUseSkill2 = true;
    public static bool canUseSkill1 = true;

    public static int PlayerSkill1Count = 0;
    public static int PlayerSkill2Count = 0;

    public static bool SkillsCanBeActive = false;

    public static bool isSheltered=false;

    public bool isSkill1Filting;
    public bool isSkill2Filting;
    void Start()
    {
        // 获取聚光灯组件
        spotLight = GetComponent<Light2D>();
        // 获取主摄像机
        mainCamera = Camera.main;

        MPSkill2_1.SetActive(false);
        MPSkill2_2.SetActive(false);
        MPSkill2_3.SetActive(false);
        MPSkill2_4.SetActive(false);

        canUseSkill1 = true;
        canUseSkill2 = true;
    }

    void Update()
    {
        MPSkillNumberChecker();

        //MPSkill2 using
        if(Input.GetMouseButton(1) && canUseSkill2 && PlayerSkill2Count>0)  
        {
            isSheltered = true;
            PlayerSkill2Count -= 1;
            MPSkill2_Isusing();
            GetSkill2StartTime();
            MPSkill2_using=true;
            StartCoroutine(StopMPSkill2());
        }

        if(MPSkill2_using) 
        {
            MPSkill2Rotation();
        }

        //MPSkill1 using
        if(Input.GetMouseButton(0) && canUseSkill1 && PlayerSkill1Count>0)
        {
            MPSkill1.SetActive(true);
            PlayerSkill1Count -= 1;
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

        BarsFilterChecker();
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
        isSheltered = false;
        MPSkill2_using = false;

        MPSkill2_1.SetActive(false);
        MPSkill2_2.SetActive(false);
        MPSkill2_3.SetActive(false);
        MPSkill2_4.SetActive(false);

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

    public void MPSkill2_Isusing()
    {
        MPSkill2_1.SetActive(true);
        MPSkill2_2.SetActive(true);
        MPSkill2_3.SetActive(true);
        MPSkill2_4.SetActive(true);
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
            if (hit.collider.CompareTag("unwatered") || hit.collider.CompareTag("watered")){
                Debug.Log("hit Plants");
                Plants plant=hit.collider.GetComponent<Plants>();
                plant.isLighting = true;
            }

            if (hit.collider.CompareTag("ice"))
            {
                Debug.Log("hit ice");
                iceTransforming ice=hit.collider.GetComponent<iceTransforming>();
                ice.isMelting=true;
            }

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

    public void BarsFilterChecker()
    {
        if (PlayerSkill1Count == 0)
        {
            if (!MPSkill1_using)
            {
                MPSkill1Bar.size = 0;
            }
        }
        else
        {
            if (!MPSkill1_using)
            {
                MPSkill1Bar.size = 1;
            }
        }

        if (PlayerSkill2Count == 0)
        {
            if (!MPSkill2_using)
            {
                MPSkill2Bar.size = 0;
            }
        }
        else
        {
            if (!MPSkill2_using)
            {
                MPSkill2Bar.size = 1;
            }
        }
    }

    public void MPSkillNumberChecker()
    {
        switch(PlayerSkill1Count)
        {
            case 0:
                image_MPSkill1_1.SetActive(false);
                image_MPSkill1_2.SetActive(false);
                break;
            case 1:
                image_MPSkill1_1.SetActive(true);
                image_MPSkill1_2.SetActive(false);
                break;
            case 2:
                image_MPSkill1_1.SetActive(true);
                image_MPSkill1_2.SetActive(true);
              
                
                break;
        }

        switch (PlayerSkill2Count)
        {
            case 0:
                image_MPSkill2_1.SetActive(false);
                image_MPSkill2_2.SetActive(false);
                break;
            case 1:
                image_MPSkill2_1.SetActive(true);
                image_MPSkill2_2.SetActive(false); 
                break;
            case 2:
                image_MPSkill2_1.SetActive(true);
                image_MPSkill2_2.SetActive(true);
                break;
        }
    }

    //enable the usage of MPSkills after 1f
    public static IEnumerator DelayEnableMPUsing()
    {
        yield return new WaitForSeconds(1f);
        canUseSkill2 = true;
        canUseSkill1 = true; 
    }
}
