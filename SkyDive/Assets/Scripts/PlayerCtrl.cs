using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField] float speed = 6f;

    [Header("중력값")]
    [SerializeField] float gravity = -4f;
    [SerializeField] float reverseGravity = -5f;
    public float maxVelocity;
    public float maxParachuteVelocity;
    public Vector3 velocity;

    [Header("바닥체크")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("속도 한계")]
    [SerializeField] float airSpeedLimit = 1.2f;
    [SerializeField] float MaxAirAcceleration = 15f;

    private float baseX;
    private float baseZ;
    private float airX;
    private float airZ;

    //행동방향
    Vector3 move;

    //충격방향
    Vector3 impact = Vector3.zero;
  
    public bool isGrounded;
    bool isDive;
    bool isParachute;
    bool isSideHit;

    float impactTimer;
    float impactFreeTimer;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        isDive = false;
        isParachute = false;
        impactTimer = 0;
        impactFreeTimer = 0;       
    }   

    //충돌판정
    private void OnTriggerEnter(Collider other)
    {
        if (isSideHit)
        {
            return;
        }

        impactFreeTimer = Time.time;
        
        if (other.gameObject.tag == "Building")
        {
            if (isGrounded)
            {
                airX = 0;
                airZ = 0;
                return;
            }
            
            if(Time.time >= impactTimer + 2.0f)
            {
                isSideHit = true;
                Debug.Log("Side Hit");
                impactTimer = Time.time;
            }                

            move.Normalize();
            impact += -move.normalized * 25;

            velocity.y = -4f;
        }
    }

    void Update()
    {
        if (GameManager.instance.isDead || GameManager.instance.isSucces) return;

        CharcterRotation(); //캐릭터 회전
        CharcterEnviromentalControl();
        CharacterMovementControl();

        //다이브 체크
        if (velocity.y <= -4f && !isParachute)
        {
            isDive = true;
        }
        else if (velocity.y >= -8f && isParachute)
        {
            isDive = false;
        }

        //죽음 체크
        if (isGrounded && isDive)
        {
            GameManager.instance.isDead = true;
            animator.SetBool("isDead", GameManager.instance.isDead);
        }
        else if(isGrounded && isParachute)
        {
            GameManager.instance.isSucces = true;
            isParachute = false;
        }
    }

    void CharcterRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * GameManager.instance.mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    void CharcterEnviromentalControl()
    {
        //땅에 닿았는지 체크
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //땅에 붙여놓기
        if (isGrounded && velocity.y < 0 || GameManager.instance.isDead)
        {
            velocity.y = -2f;
        }

        //낙하
        if (!isParachute) velocity.y += gravity * Time.deltaTime; //다이브&일반
        else if(isParachute) velocity.y -= reverseGravity * Time.deltaTime; //낙하산
        controller.Move(velocity * Time.deltaTime);

        if (velocity.y <= -maxVelocity)
        {
            velocity.y = -maxVelocity;
        }
        else if (velocity.y >= -maxParachuteVelocity && isParachute)
        {
            velocity.y = -maxParachuteVelocity;
        }
   
        //충격
        if (impact.magnitude > 0.2f)
        {
            controller.Move(impact * Time.deltaTime);

            airX = airX / 10;
            airZ = airZ / 10;
        }

        //충격 벗어나는 조건
        if (isSideHit)
        {
            if (Time.time >= impactFreeTimer + 1.0f)
            {
                isSideHit = false;
                impact = Vector3.zero;
                Debug.Log("SideHit False");
            }
        }

        //충격 점감
        impact = Vector3.Lerp(impact, Vector3.zero, Time.deltaTime * 2);
    }

    void CharacterMovementControl()
    {
        //키입력
        if (!isSideHit)
        {
            baseX = Input.GetAxis("Horizontal");
            baseZ = Input.GetAxis("Vertical");          
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {         
            isParachute = true;
            airSpeedLimit = 0.8f;
            MaxAirAcceleration = 10f;          
        }       

        //지상에서의 움직임
        if (isGrounded)
        {
            move = transform.right * baseX + transform.forward * baseZ;

            controller.Move(move * speed * Time.deltaTime);
        }
        //공중에서의 움직임
        else
        {
            //float AirXRate = (Mathf.Abs(airX) / airSpeedLimit);
            //float AirZRate = (Mathf.Abs(airZ) / airSpeedLimit);
            //float AirXAcceleration = (MaxAirAcceleration - (MaxAirAcceleration - 1) * AirXRate);
            //float AirZAcceleration = (MaxAirAcceleration - (MaxAirAcceleration - 1) * AirZRate);         
            
            airX += baseX / (MaxAirAcceleration - (MaxAirAcceleration - 1) * (Mathf.Abs(airX) / airSpeedLimit));
            airZ += baseZ / (MaxAirAcceleration - (MaxAirAcceleration - 1) * (Mathf.Abs(airZ) / airSpeedLimit));

            if (Mathf.Abs(airX) >= airSpeedLimit)
            {
                if (airX < 0)
                {
                    airX = -airSpeedLimit;
                }
                else airX = airSpeedLimit;
            }

            if (Mathf.Abs(airZ) >= airSpeedLimit)
            {
                if (airZ < 0)
                {
                    airZ = -airSpeedLimit;
                }
                else airZ = airSpeedLimit;
            }
            
            move = transform.right * airX + transform.forward * airZ;

            controller.Move(move * speed * Time.deltaTime);

        }
    }
    
}
