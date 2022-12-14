using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeScript : MonoBehaviour
{
    public float time = 3f;
    // Start is called before the first frame update
    void Awake()
    {
        Invoke("DestroyThis", time);
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
