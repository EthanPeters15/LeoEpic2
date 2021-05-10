using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{

    public float health;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            Destroy(transform.gameObject);
        }
    }
}
