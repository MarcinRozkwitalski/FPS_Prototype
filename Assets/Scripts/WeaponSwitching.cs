using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> m_WeaponList;

    public void DisableWeapon(int weaponID)
    {
        m_WeaponList[weaponID].gameObject.SetActive(false);
    }

    public void EnableWeapon(int weaponID)
    {
        foreach (var weapon in m_WeaponList)
        {
            if (weapon.gameObject != m_WeaponList[weaponID])
            {
                weapon.gameObject.SetActive(false);
            }
        }
        
        m_WeaponList[weaponID].gameObject.SetActive(true);
    }
}