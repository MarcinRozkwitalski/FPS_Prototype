using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private IObjectPool<Bullet> bulletPool;

    [SerializeField]
    private GameObject m_BulletImpactSoundObjectPrefab;

    private void Awake()
    {
        this.gameObject.SetActive(true);
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        bulletPool = pool;
    }

    public string mWeaponType;
    public int damage = 0;

    void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        Vector3 lastBulletPosition = gameObject.transform.position;

        bulletPool.Release(this);

        if (hitObject.CompareTag("Destroyable"))
        {
            if (hitObject.GetComponent<Material>() != null)
            {
                Material hitObjectMaterial = hitObject.GetComponent<Material>();
                Health hitObjectHealth = hitObject.GetComponent<Health>();

                if (mWeaponType == "Pistol" && hitObjectMaterial.materialType == Material.Type.Metal || mWeaponType == "Pistol" && hitObjectMaterial.materialType == Material.Type.Glass)
                {
                    hitObjectHealth.ReceiveDamage(damage);
                    if (hitObjectMaterial.materialType == Material.Type.Metal && m_BulletImpactSoundObjectPrefab != null)
                    {
                        Instantiate(m_BulletImpactSoundObjectPrefab, lastBulletPosition, Quaternion.identity);
                    }
                }
                else if (mWeaponType == "Laser_Gun" && hitObjectMaterial.materialType == Material.Type.Metal || mWeaponType == "Laser_Gun" && hitObjectMaterial.materialType == Material.Type.Wood)
                {
                    hitObjectHealth.ReceiveDamage(damage);
                }
                else if (mWeaponType == "Sledge_Hammer" && hitObjectMaterial.materialType == Material.Type.Wood || mWeaponType == "Sledge_Hammer" && hitObjectMaterial.materialType == Material.Type.Glass)
                {
                    hitObjectHealth.ReceiveDamage(damage);
                }
            }
        }
    }
}