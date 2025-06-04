using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSound : MonoBehaviour
{
    public AudioClip gunShotClip;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) // ���콺 ���� Ŭ��
        {
            Shoot();
        }
    }

    void Shoot()
    { 
        audioSource.PlayOneShot(gunShotClip);
    }
}
