using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    public static CameraControl _instance;
    public CinemachineFreeLook freeLookCamera;
    public float rotationSpeed = 1.5f;
    private GameObject _player;
    private Transform _playerTransform;

    public void Awake()
    {
        _instance = this;
    }

    public void Initialize()
    {
        _player = GameObject.FindWithTag("Player");
        _playerTransform = _player.GetComponent<Transform>();
        freeLookCamera.Follow = _playerTransform;
        freeLookCamera.LookAt = _playerTransform;
        freeLookCamera.m_LookAt = _playerTransform;
    }

    public void HandleCamera(float xvalue)
    {
        freeLookCamera.m_XAxis.Value += xvalue * rotationSpeed*50 * Time.deltaTime;
    }
}


    
