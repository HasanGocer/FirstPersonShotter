using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    [System.Serializable]
    public class Field
    {
        public float mainHealth, rivalHealth, friendHealth, mainDamage, rivalDamage;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field max;
    public Field fieldPrice;

    public void AwakeID()
    {
        field.mainHealth = standart.mainHealth + (factor.mainHealth * constant.mainHealth);
        fieldPrice.mainHealth = fieldPrice.mainHealth * factor.mainHealth;
        field.rivalHealth = standart.rivalHealth + (factor.rivalHealth * constant.rivalHealth);
        fieldPrice.rivalHealth = fieldPrice.rivalHealth * factor.rivalHealth;
        field.friendHealth = standart.friendHealth + (factor.friendHealth * constant.friendHealth);
        fieldPrice.friendHealth = fieldPrice.friendHealth * factor.friendHealth;
        field.mainDamage = standart.mainDamage + (factor.mainDamage * constant.mainDamage);
        fieldPrice.mainDamage = fieldPrice.mainDamage * factor.mainDamage;
        field.rivalDamage = standart.rivalDamage + (factor.rivalDamage * constant.rivalDamage);
        fieldPrice.rivalDamage = fieldPrice.rivalDamage * factor.rivalDamage;

        /*
         field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        */

        if (factor.mainHealth > maxFactor.mainHealth)
        {
            factor.mainHealth = maxFactor.mainHealth;
            field.mainHealth = standart.mainHealth + (factor.mainHealth * constant.mainHealth);
            fieldPrice.mainHealth = fieldPrice.mainHealth / (factor.mainHealth - 1);
            fieldPrice.mainHealth = fieldPrice.mainHealth * factor.mainHealth;
        }
        if (factor.rivalHealth > maxFactor.rivalHealth)
        {
            factor.rivalHealth = maxFactor.rivalHealth;
            field.rivalHealth = standart.rivalHealth + (factor.rivalHealth * constant.rivalHealth);
            fieldPrice.rivalHealth = fieldPrice.rivalHealth / (factor.rivalHealth - 1);
            fieldPrice.rivalHealth = fieldPrice.rivalHealth * factor.rivalHealth;
        }
        if (factor.friendHealth > maxFactor.friendHealth)
        {
            factor.friendHealth = maxFactor.friendHealth;
            field.friendHealth = standart.friendHealth + (factor.friendHealth * constant.friendHealth);
            fieldPrice.friendHealth = fieldPrice.friendHealth / (factor.friendHealth - 1);
            fieldPrice.friendHealth = fieldPrice.friendHealth * factor.friendHealth;
        }
        if (factor.mainDamage > maxFactor.mainDamage)
        {
            factor.mainDamage = maxFactor.mainDamage;
            field.mainDamage = standart.mainDamage + (factor.mainDamage * constant.mainDamage);
            fieldPrice.mainDamage = fieldPrice.mainDamage / (factor.mainDamage - 1);
            fieldPrice.mainDamage = fieldPrice.mainDamage * factor.mainDamage;
        }
        if (factor.rivalDamage > maxFactor.rivalDamage)
        {
            factor.rivalDamage = maxFactor.rivalDamage;
            field.rivalDamage = standart.rivalDamage + (factor.rivalDamage * constant.rivalDamage);
            fieldPrice.rivalDamage = fieldPrice.rivalDamage / (factor.rivalDamage - 1);
            fieldPrice.rivalDamage = fieldPrice.rivalDamage * factor.rivalDamage;
        }

        /*
          if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }
        */

        StartCoroutine(Buttons.Instance.LoadingScreen());
    }

    public void SetMainHealth()
    {
        factor.mainHealth++;

        field.mainHealth = standart.mainHealth + (factor.mainHealth * constant.mainHealth);
        fieldPrice.mainHealth = fieldPrice.mainHealth / (factor.mainHealth - 1);
        fieldPrice.mainHealth = fieldPrice.mainHealth * factor.mainHealth;

        if (factor.mainHealth > maxFactor.mainHealth)
        {
            factor.mainHealth = maxFactor.mainHealth;
            field.mainHealth = standart.mainHealth + (factor.mainHealth * constant.mainHealth);
            fieldPrice.mainHealth = fieldPrice.mainHealth / (factor.mainHealth - 1);
            fieldPrice.mainHealth = fieldPrice.mainHealth * factor.mainHealth;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetRivalHealth()
    {
        factor.rivalHealth++;

        field.rivalHealth = standart.rivalHealth + (factor.rivalHealth * constant.rivalHealth);
        fieldPrice.rivalHealth = fieldPrice.rivalHealth / (factor.rivalHealth - 1);
        fieldPrice.rivalHealth = fieldPrice.rivalHealth * factor.rivalHealth;

        if (factor.rivalHealth > maxFactor.rivalHealth)
        {
            factor.rivalHealth = maxFactor.rivalHealth;
            field.rivalHealth = standart.rivalHealth + (factor.rivalHealth * constant.rivalHealth);
            fieldPrice.rivalHealth = fieldPrice.rivalHealth / (factor.rivalHealth - 1);
            fieldPrice.rivalHealth = fieldPrice.rivalHealth * factor.rivalHealth;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetFriendHealth()
    {
        factor.friendHealth++;

        field.friendHealth = standart.friendHealth + (factor.friendHealth * constant.friendHealth);
        fieldPrice.friendHealth = fieldPrice.friendHealth / (factor.friendHealth - 1);
        fieldPrice.friendHealth = fieldPrice.friendHealth * factor.friendHealth;

        if (factor.friendHealth > maxFactor.friendHealth)
        {
            factor.friendHealth = maxFactor.friendHealth;
            field.friendHealth = standart.friendHealth + (factor.friendHealth * constant.friendHealth);
            fieldPrice.friendHealth = fieldPrice.friendHealth / (factor.friendHealth - 1);
            fieldPrice.friendHealth = fieldPrice.friendHealth * factor.friendHealth;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetMainDamage()
    {
        factor.mainDamage++;

        field.mainDamage = standart.mainDamage + (factor.mainDamage * constant.mainDamage);
        fieldPrice.mainDamage = fieldPrice.mainDamage / (factor.mainDamage - 1);
        fieldPrice.mainDamage = fieldPrice.mainDamage * factor.mainDamage;

        if (factor.mainDamage > maxFactor.mainDamage)
        {
            factor.mainDamage = maxFactor.mainDamage;
            field.mainDamage = standart.mainDamage + (factor.mainDamage * constant.mainDamage);
            fieldPrice.mainDamage = fieldPrice.mainDamage / (factor.mainDamage - 1);
            fieldPrice.mainDamage = fieldPrice.mainDamage * factor.mainDamage;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetRivalDamage()
    {
        factor.rivalDamage++;

        field.rivalDamage = standart.rivalDamage + (factor.rivalDamage * constant.rivalDamage);
        fieldPrice.rivalDamage = fieldPrice.rivalDamage / (factor.rivalDamage - 1);
        fieldPrice.rivalDamage = fieldPrice.rivalDamage * factor.rivalDamage;

        if (factor.rivalDamage > maxFactor.rivalDamage)
        {
            factor.rivalDamage = maxFactor.rivalDamage;
            field.rivalDamage = standart.rivalDamage + (factor.rivalDamage * constant.rivalDamage);
            fieldPrice.rivalDamage = fieldPrice.rivalDamage / (factor.rivalDamage - 1);
            fieldPrice.rivalDamage = fieldPrice.rivalDamage * factor.rivalDamage;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    /*
     public void SetObjectCount()
    {
        factor.objectCount++;

        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;

        if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    */
}
