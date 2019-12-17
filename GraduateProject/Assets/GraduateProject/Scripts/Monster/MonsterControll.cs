using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterControll : MonoBehaviour
{
    public GameObject target; // Public 변수는 inspector에서 값을 지정하거나 수정할 수 있음
    NavMeshAgent agent;
    protected Animator m_Animator;                 // Reference used to make decisions based on Ellen's current animation and to set parameters.

    readonly int m_HashWalkForward = Animator.StringToHash("Walk Forward");
    readonly int m_HashRunForward = Animator.StringToHash("Run Forward");
    readonly int m_HashJump = Animator.StringToHash("Jump");
    readonly int m_HashAttack1 = Animator.StringToHash("Attack 01");
    readonly int m_HashAttack2 = Animator.StringToHash("Attack 02");
    readonly int m_HashTakeDamage = Animator.StringToHash("Take Damage");
    readonly int m_HashDie = Animator.StringToHash("Die");

    public List<Weapon> ownWeapons = new List<Weapon>();
    Weapon equipment;
    public List<GameObject> bullets;
    Transform weaponPos;

    float chaseTimer = 0.0f;
    float shotTimer = 0.0f;

    float healthPoint;
    bool isLive = true;

    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("unitychan");
        m_Animator = GetComponent<Animator>();

        ownWeapons.Add(new MonsterNormalGun());
        equipment = ownWeapons[0];
        if (equipment == null)
            Destroy(gameObject);

        weaponPos = transform.GetChild(13);

        // 총알생성
        Gun gun = equipment as Gun;
        for (int i = 0; i < gun.MaxMagazine; i++)
        {
            GameObject newBullet = Instantiate(Resources.Load("MonsterNormalGunBullet") as GameObject);
            newBullet.transform.position = new Vector3(100.0f + i * 2.0f, 100.0f, 100.0f);
            newBullet.GetComponent<MonsterNormalGunBullet>().setMagazinePos(new Vector3(100.0f + i * 2.0f, 100.0f, 100.0f));
            bullets.Add(newBullet);
        }

        healthPoint = 4.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive)
            return;

        chaseTimer += Time.deltaTime;
        shotTimer += Time.deltaTime;

        if (chaseTimer > 0.5f)
            chaseTarget();

        Gun gun = equipment as Gun;
        if (shotTimer > 60 / gun.Rpm)
            shot();
    }

    void chaseTarget()
    {
        //agent.destination = target.transform.position; // 어떤 위치를 설정하면 해당 위치로 자동으로 이동됨
        m_Animator.SetBool(m_HashWalkForward, true);

        chaseTimer = 0.0f;
    }

    void shot()
    {
        if (equipment as Gun == null)
            return;

        if (Vector2.Distance(target.transform.position, transform.position) > 17.0f)
            return;

        Gun gun = equipment as Gun;
        int bulletNum = gun.MaxMagazine - gun.CurrentMagazine;
        if (bulletNum == gun.MaxMagazine)
        {
            reload();
            return;
        }

        m_Animator.SetTrigger(m_HashAttack2);

        bullets[bulletNum].transform.position = weaponPos.position;
        bullets[bulletNum].GetComponent<MonsterNormalGunBullet>().Direction = transform.forward;
        gun.shot(bullets[bulletNum]);

        shotTimer = 0.0f;
    }

    // 재장전
    void reload()
    {
        if (equipment as Gun == null)
            return;

        pause();

        m_Animator.SetTrigger(m_HashJump);

        Gun gun = equipment as Gun;
        gun.reload();

        StartCoroutine(waitTime(gun.ReloadTime));
    }

    IEnumerator waitTime(float time)
    {
        yield return new WaitForSeconds(time);
        pauseEnd();
    }

    void pause()
    {
        //agent.isStopped = true;
        isLive = false;
        m_Animator.SetBool(m_HashWalkForward, false);
    }

    void pauseEnd()
    {
        //agent.isStopped = false;
        isLive = true;
    }

    public void hited(float damage)
    {
        healthPoint -= damage;
        if (healthPoint <= 0.0f)
            die();
    }

    void die()
    {
        pause();
        m_Animator.SetTrigger(m_HashDie);

        Destroy(gameObject, 1.7f);
    }
}
