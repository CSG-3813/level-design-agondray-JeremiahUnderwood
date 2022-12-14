using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFireScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject smokePrefab;
    public float reloadTime = 4f;
    [HideInInspector] public float reloadVariable = 0f;

    public ParticleSystem spark;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        reloadVariable-= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && (reloadVariable <= 0f))
        {
            Instantiate(prefab, this.transform.position, this.transform.rotation);
            Instantiate(smokePrefab, this.transform.position, this.transform.rotation);
            spark.Play(true);
            reloadVariable = reloadTime; 
        }
    }
}
