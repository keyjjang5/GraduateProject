using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    protected static PlayerController s_Instance;
    public static PlayerController instance { get { return s_Instance; } }

    private float speed;
    public List<Weapon> ownWeapons = new List<Weapon>();
    Weapon equipment;
    public List<GameObject> bullets;
    Transform weaponPos;

    GameObject heart;
    float healthPoint;
    bool isLive;

    ReloadBarController reloadBar;
    GunUIManager uiManager;

    public float maxForwardSpeed = 8f;        // How fast Ellen can run.
    public float gravity = 20f;               // How fast Ellen accelerates downwards when airborne.
    public float jumpSpeed = 10f;             // How fast Ellen takes off when jumping.
    public float minTurnSpeed = 400f;         // How fast Ellen turns when moving at maximum speed.
    public float maxTurnSpeed = 1200f;        // How fast Ellen turns when stationary.
    public float idleTimeout = 5f;            // How long before Ellen starts considering random idles.
    public bool canAttack;                    // Whether or not Ellen can swing her staff.

    protected float m_DesiredForwardSpeed;         // How fast Ellen aims be going along the ground based on input.
    protected float m_ForwardSpeed;                // How fast Ellen is currently going along the ground.
    protected float m_VerticalSpeed;               // How fast Ellen is currently moving up or down.
    protected PlayerInput m_Input;                 // Reference used to determine how Ellen should move.
    protected Animator m_Animator;                 // Reference used to make decisions based on Ellen's current animation and to set parameters.

    const float k_GroundAcceleration = 20f;
    const float k_GroundDeceleration = 25f;

    private float m_LookAngle;
    [Range(0f, 10f)] [SerializeField]
    private float m_TurnSpeed = 1.5f;
    private Quaternion m_TransformTargetRot;

    // Animator Parameter
    readonly int m_HashForwardSpeed = Animator.StringToHash("Speed");
    readonly int m_HashIsLive = Animator.StringToHash("IsLive");



    protected bool IsMoveInput
    {
        get { return !Mathf.Approximately(m_Input.MoveInput.sqrMagnitude, 0f); }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        m_Input = GetComponent<PlayerInput>();
        m_Animator = GetComponent<Animator>();

        s_Instance = this;

        m_TransformTargetRot = transform.localRotation;

        ownWeapons.Clear();
        weaponPos = GameObject.Find("/unitychan/WeaponPos").transform;

        heart = GameObject.Find("PlayerHealth");
        isLive = true;
        healthPoint = 3.0f;

        reloadBar = GameObject.Find("PlayerReloadBar").GetComponent<ReloadBarController>();
        reloadBar.gameObject.SetActive(false);

        uiManager = GameObject.Find("GunUI").GetComponent<GunUIManager>();
    }
    void Start()
    {
        speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLive)
            return;

        if (Input.anyKey)
        {
            if (Input.GetMouseButtonDown(0))
                shot();
            if (Input.GetKeyDown(KeyCode.R))
                reload();
            if (Input.GetKeyDown(KeyCode.Alpha1))
                swapWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                swapWeapon(1);
        }

        if (equipment as Gun == null)
            return;

        Gun gun = equipment as Gun;
        uiManager.updateMagazine(gun.MaxMagazine, gun.CurrentMagazine);

    }

    private void FixedUpdate()
    {
        if (!isLive)
            return;

        lookAtMouse();
        fixedMoveCharacter();
    }

    // 캐릭터가 마우스를 바라봄
    void lookAtMouse()
    {
        Vector3 lookTarget = new Vector3(0, 0, 0);
        lookTarget = MousePointer.GetMousePos();
        lookTarget.y = transform.position.y;

        transform.LookAt(lookTarget);
    }

    // 캐릭터 상하좌우 이동
    void fixedMoveCharacter()
    {
        if(Input.anyKey)
        {
            if(Input.GetKey(KeyCode.W))
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0.0f, 0.0f, 10.0f), Time.deltaTime * speed);
                CalculateForwardMovement();
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0.0f, 0.0f, -10.0f), Time.deltaTime * speed);
                CalculateForwardMovement();
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-10.0f, 0.0f, 0.0f), Time.deltaTime * speed);
                CalculateForwardMovement();
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(10.0f, 0.0f, 0.0f), Time.deltaTime * speed);
                CalculateForwardMovement();
            }
        }
        else
            m_Animator.SetFloat(m_HashForwardSpeed, 0);
    }

    // 캐릭터 이동 계산
    void CalculateForwardMovement()
    {
        // Cache the move input and cap it's magnitude at 1.
        Vector2 moveInput = m_Input.MoveInput;
        if (moveInput.sqrMagnitude > 1f)
            moveInput.Normalize();

        // Calculate the speed intended by input.
        m_DesiredForwardSpeed = moveInput.magnitude * maxForwardSpeed;

        // Determine change to speed based on whether there is currently any move input.
        float acceleration = IsMoveInput ? k_GroundAcceleration : k_GroundDeceleration;

        // Adjust the forward speed towards the desired speed.
        m_ForwardSpeed = Mathf.MoveTowards(m_ForwardSpeed, m_DesiredForwardSpeed, acceleration * Time.deltaTime);

        // Set the animator parameter to control what animation is being played.
        m_Animator.SetFloat(m_HashForwardSpeed, m_ForwardSpeed);
    }

    // 무기리스트에 추가
    public void addWeapon(Weapon weapon)
    {
        if (ownWeapons.Count >= 3)
        {
            drop(weapon);
            return;
        }

        equipWeapon(weapon);
        ownWeapons.Add(weapon);
    }

    // 무기 떨어뜨림
    void drop(Weapon weapon)
    {

    }

    // 무기 장착
    void equipWeapon(Weapon weapon)
    {
        bullets.Clear();
        equipment = weapon;

        string path = "NormalGun";

        if (equipment as Gun == null)
            return;
        Gun gun = equipment as Gun;
        uiManager.SelectGunUI((int)gun.GunUINum);

        // 새로운 종류의 총이 나올 떄 마다 여기에 방식 추가
        List<GameObject> tempBullets = new List<GameObject>();
        if (gun.GunUINum == (int)GunUI.NormalGunUI)
        {
            path = "NormalGun";
            
            
            // 탄창만큼의 총알을 미리 생성해놓음
            for (int i = 0; i < gun.MaxMagazine; i++)
            {
                GameObject newBullet = Instantiate(Resources.Load("Bullet") as GameObject);
                newBullet.transform.position = new Vector3(100.0f + i * 2.0f, 100.0f, 100.0f);
                tempBullets.Add(newBullet);
            }
        }
        if (gun.GunUINum == (int)GunUI.ShotGunUI)
        {
            path = "ShotGun";

            // 탄창만큼의 총알을 미리 생성해놓음
            for (int i = 0; i < gun.MaxMagazine; i++)
            {
                GameObject newBullet = Instantiate(Resources.Load("ShotGunBullet") as GameObject);
                newBullet.transform.position = new Vector3(100.0f + i * 2.0f, 100.0f, 100.0f);
                tempBullets.Add(newBullet);
            }
        }

        // 이미 장착중인 무기가 있으면 파괴하고 생성
        if (transform.GetChild(4).childCount > 0)
            Destroy(transform.GetChild(4).GetChild(0).gameObject);

        GameObject newWeapon = Instantiate(Resources.Load(path)) as GameObject;
        newWeapon.transform.SetParent(transform.GetChild(4));
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = newWeapon.transform.parent.localRotation;

        bullets = tempBullets;
    }

    // 발사
    void shot()
    {
        if (equipment as Gun == null)
            return;

        Gun gun = equipment as Gun;
        int bulletNum = gun.MaxMagazine - gun.CurrentMagazine;
        if (bulletNum == gun.MaxMagazine)
            return;
        bullets[bulletNum].transform.position = weaponPos.position;
        gun.shot(bullets[bulletNum]);

        uiManager.updateMagazine(gun.MaxMagazine, gun.CurrentMagazine);
    }

    // 재장전
    void reload()
    {
        if (equipment as Gun == null)
            return;

        Gun gun = equipment as Gun;
        reloadBar.gameObject.SetActive(true);
        reloadBar.reload(gun, gun.ReloadTime);

    }

    void swapWeapon(int num)
    {
        equipWeapon(ownWeapons[num]);
    }

    public void hited(float damage)
    {
        if (damage == 0.5f)
            heartHit();
        if (damage == 1.0f)
        {
            heartHit();
            heartHit();
        }
        healthPoint -= damage;
        if (healthPoint <= 0.0f)
            die();
    }

    void die()
    {
        m_Animator.SetBool(m_HashIsLive, false);
        isLive = false;

        Invoke("goEndScene", 3.0f);
    }

    void goEndScene()
    {
        MySceneManager.goEndScene();
    }


    void heartHit()
    {
        Transform first = heart.transform.GetChild(0).GetChild(0);
        Transform second = heart.transform.GetChild(1).GetChild(0);
        Transform third = heart.transform.GetChild(2).GetChild(0);
        if (healthPoint <= 3 && healthPoint > 2)
            third.GetComponent<Image>().fillAmount = third.GetComponent<Image>().fillAmount - 0.5f;
        if (healthPoint <= 2 && healthPoint > 1)
            second.GetComponent<Image>().fillAmount = second.GetComponent<Image>().fillAmount - 0.5f;
        if (healthPoint <= 1 && healthPoint > 0)
            first.GetComponent<Image>().fillAmount = first.GetComponent<Image>().fillAmount - 0.5f;
    }
}
