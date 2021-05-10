using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleLock : MonoBehaviour
{
    [HideInInspector] public bool isHitting;
    [SerializeField] private GameObject player;
    void Update()
    {
        if (!isHitting)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = player.transform.localScale;
        }
    }
    private void LateUpdate()
    {
        if (!isHitting)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = player.transform.localScale;
        }
    }
}
