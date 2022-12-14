using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalAnimation : MonoBehaviour
{

    public Animator animator;
    public string param;
    public EnemyAI boss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (boss.currentState == EnemyAI.AIState.dead))
        {
            animator.SetBool(param, true);
        }
    }
}
