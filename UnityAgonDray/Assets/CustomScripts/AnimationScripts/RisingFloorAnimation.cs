using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingFloorAnimation : MonoBehaviour
{

    public Animator animator;
    public string param;

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
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool(param, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool(param, false);
        }
    }
}
