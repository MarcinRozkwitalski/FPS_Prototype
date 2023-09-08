using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> weaponList;

    public List<Animator> weaponAnimatorList;

    public List<GameObject> projectileGunsList;

    private int currentWeaponID = 0;
    
    private void Awake() 
    {
        foreach (GameObject weapon in weaponList)
        {
            weaponAnimatorList.Add(weapon.GetComponent<Animator>());

            if(weapon.GetComponent<ProjectileGun>() != null)
                projectileGunsList.Add(weapon);
        }
    }

    public void DisableWeapon(int weaponID)
    {
        weaponList[weaponID].gameObject.SetActive(false);
    }

    public void EnableWeapon(int weaponID)
    {
        foreach (var weapon in weaponList)
        {
            if (weapon.gameObject != weaponList[weaponID])
                weapon.gameObject.SetActive(false);
        }
        
        weaponList[weaponID].gameObject.SetActive(true);
        SetCurrentWeaponID(weaponID);
    }

    public void SetCurrentWeaponID(int weaponID)
    {
        this.currentWeaponID = weaponID;
    }

    public int GetCurrentWeaponID()
    {
        return currentWeaponID;
    }
}