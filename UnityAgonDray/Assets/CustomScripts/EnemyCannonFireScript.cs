using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonFireScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject smokePrefab;
    public float reloadTime = 4f;
    private float reloadVariable = 0f;

    public ParticleSystem spark;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        reloadVariable-= Time.deltaTime;
    }

    public void Fire()
    {
        if (reloadVariable <= 0)
        {
            Instantiate(prefab, this.transform.position, this.transform.rotation);
            Instantiate(smokePrefab, this.transform.position, this.transform.rotation);
            spark.Play(true);
            reloadVariable = reloadTime;
        }
    }
}
