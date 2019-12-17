using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterHp : MonoBehaviour
{
    protected Animator m_Animator;                 // Reference used to make decisions based on Ellen's current animation and to set parameters.

    readonly int m_HashDie = Animator.StringToHash("Die");
    readonly int m_HashWalkForward = Animator.StringToHash("Walk Forward");

    NavMeshAgent agent;

    float healthPoint;
    bool isLive = true;
    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();

        healthPoint = 4.0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void pause()
    {
        //agent.isStopped = true;
        isLive = false;
        m_Animator.SetBool(m_HashWalkForward, false);
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
