using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    public Transform camTrans;
    public float turretTurnSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(this.transform.rotation.eulerAngles.x, camTrans.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z), turretTurnSpeed * Time.fixedDeltaTime);
    }
}
