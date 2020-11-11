﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public DamageType damageType;
    [HideInInspector] public GameObject circleAroundObj;
    [HideInInspector] public BossUdokEnemyUnit bossUdok;
    [HideInInspector] public bool isCirclingAround = false;

    private int targetPosIdx = 0;


    public void ShootToTarget(Vector3 pos)
    {
        isCirclingAround = false;
        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, pos, 5f).setEaseInBack().setOnComplete(() =>
        {
            DestroyProj();
        });
    }



    private void Update()
    {
        if (isCirclingAround)
        {
            gameObject.transform.RotateAround(circleAroundObj.transform.position, Vector3.up, 30 * Time.deltaTime);
        }
    }

    public void startCircleAround()
    {
        isCirclingAround = true;
        
        LeanTween.moveLocalY(gameObject, 2.5f, 1.3f).setEaseInOutQuad().setLoopPingPong();
    }

    public void DestroyProj()
    {
        // TODO Explosion Effect
        // bossUdok.fireBalls.Remove(gameObject);
        // Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Projectile Hit");
            DestroyProj();
        }
        else if (!other.CompareTag("Enemy"))
        {
            DestroyProj();
        }
    }
}
