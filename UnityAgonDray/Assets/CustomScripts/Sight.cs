using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{

    public EnemyAI attachedAI;
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
        if (other.tag == "Player")
        {
            RaycastHit hit;
            if(Physics.Raycast(this.transform.position, other.gameObject.transform.position - transform.position, out hit, 40f))
            {
                if (hit.collider.tag == "Player")
                {
                    attachedAI.playerInSight = true;
                }
            }
        }
    }

}
