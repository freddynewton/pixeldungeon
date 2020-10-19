﻿using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;


public class PlayerUnit : Unit
{
    [HideInInspector] public float currentStamina;
    [HideInInspector] public bool isStaminaReg = true;

    public float timeToRegStaminaAfterHitZero = 2f;
    private bool foolStamina;

    public override void Start()
    {
        base.Start();
        currentStamina = stats.stamina;
    }

    public void updateStamina()
    {
        if (!foolStamina && isStaminaReg && currentStamina <= stats.stamina)
        {
            setStamina(Time.deltaTime * stats.staminaRate);
        }
    }

    public void setStamina(float amount)
    {
        currentStamina += amount;

        if (currentStamina > stats.stamina) currentStamina = stats.stamina;
        else if (currentStamina < 0)
        {
            foolStamina = true;
            currentStamina = 0;
            Invoke("changeFoolStamina", timeToRegStaminaAfterHitZero);
        }

        // TODO CALL UI UPDATE FUNCTION
        UiManager.Instance.setStamina();
    }

    private void changeFoolStamina()
    {
        foolStamina = false;
    }

    public override void Update()
    {
        base.Update();
        updateStamina();
    }

    public override void death()
    {
        base.death();
        // TODO Player Hit effect & "Game over" Scene 
    }

    public override void hit()
    {
        base.hit();

        // TODO Do Player hit effect
        UiManager.Instance.flashScreen.flashScreen(1);

    }

    public override void DoDamage(Vector3 damageSrcPos, DamageType damageType)
    {
        Weapon.callbackValue v = Inventory.Instance.currWeaponScript.callbackDamageFnc();

        switch (v)
        {
            case Weapon.callbackValue.FAILURE:
                base.DoDamage(damageSrcPos, damageType);
                UiManager.Instance.setHealth();
                break;

            case Weapon.callbackValue.SUCCESS:
                // TODO 
                break;

            case Weapon.callbackValue.NOTHING:
                if (currentStamina - (damageType.damage * 2) < 0)
                {
                    base.DoDamage(damageSrcPos, damageType);
                    UiManager.Instance.setHealth();
                }
                setStamina(-damageType.damage * 2);
                break;

        }
    }

    public void setHealthPlayer(int amount)
    {
        currentHealth += amount;
        if (currentHealth > stats.health) currentHealth = stats.health;

        UiManager.Instance.setHealth();
    }
}
