using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAIMove : MonoBehaviour
{
    GridManager gm = null;
    Coroutine move_coroutine = null;

    float timer = 0.0f;
    GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = Camera.main.GetComponent<GridManager>() as GridManager;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 2.0f)
            return;

        timer = 0.0f;
        if (move_coroutine != null) StopCoroutine(move_coroutine);
        move_coroutine = StartCoroutine(gm.MonsterMove(gameObject, target.transform.position));
    }
}
