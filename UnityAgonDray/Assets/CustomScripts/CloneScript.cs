using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneScript : MonoBehaviour
{
    public GameObject prefab;
    public float cloneTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("clone", cloneTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clone()
    {
        Instantiate(prefab);
        Invoke("clone", cloneTime);
    }
}
