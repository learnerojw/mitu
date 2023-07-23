using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class VCamera : MonoBehaviour
{
    private CinemachineVirtualCamera VrCamera;
    private CinemachineConfiner confiner;
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        VrCamera = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();
    }
    private void Start()
    {
        VrCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        if(GameObject.Find("CameraArea")!=null)
        {
            Collider2D collider2D = GameObject.Find("CameraArea").GetComponent<Collider2D>();
            confiner.m_BoundingShape2D = collider2D;
        }
    }

    private void LateUpdate()
    {
        
    }
}
