using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDynamite : MonoBehaviour
{
    public GameObject dynamite;

    Transform _transform;
    private void Awake()
    {
        _transform = transform;
    }

    public void InstantiateD()
    {
        Debug.Log("Dynamitye");
        Instantiate(dynamite, _transform.position, Quaternion.Euler(-90, 0, 0));
    }
}
