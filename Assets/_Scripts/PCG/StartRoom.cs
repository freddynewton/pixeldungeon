﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : Room
{
    [Header("Start Room Settings")]
    public Transform PlayerSpawn;

    public override void Awake()
    {
        base.Awake();
        setLights(mainColor);
        StartCoroutine(setSpawnPos());
    }

    private IEnumerator setSpawnPos()
    {
        yield return new WaitForEndOfFrame();
        PlayerController.Instance.transform.position = PlayerSpawn.position;
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}