using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public static CombatManager instance
    {
        get; private set;
    }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    //TODO Negli eventi impostare un cd per il prossimo attacco (almeno per i nemici)
    public void ApplyDmgOutput(Collider2D objToDmg, float dmgAmount)
    {
        IDamageable objDamageable = null;
        objToDmg.TryGetComponent(out objDamageable);
        if (objDamageable != null)
            objDamageable.ApplyDamage(dmgAmount);
    }
}
