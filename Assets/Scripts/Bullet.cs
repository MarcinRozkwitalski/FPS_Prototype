using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using static ProjectileGun;

public class Bullet : MonoBehaviour
{
    private IObjectPool<Bullet> bulletPool;

    [SerializeField]
    private GameObject m_BulletImpactSoundObjectPrefab;

    [SerializeField]
    public List<MaterialTypeDamage> materialTypeDamage = new List<MaterialTypeDamage>();

    private void Awake()
    {
        this.gameObject.SetActive(true);
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        bulletPool = pool;
    }

    public MaterialTypeDamage m_AttackableMaterialType;
    public RangedWeaponType.Name m_WeaponType;
    public int damage = 0;

    void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        Vector3 lastBulletPosition = gameObject.transform.position;

        bulletPool.Release(this);

        if (hitObject.CompareTag("Destroyable") && 
            hitObject.GetComponent<MaterialType>() != null)
        {
            MaterialType hitObjectMaterialType = hitObject.GetComponent<MaterialType>();
            Health hitObjectHealth = hitObject.GetComponent<Health>();

            if (materialTypeDamage.Any(f => f.m_AttackableMaterialType.Contains(hitObjectMaterialType.t_Name)))
            {
                var foundElement = materialTypeDamage.FirstOrDefault(f => f.m_AttackableMaterialType.Contains(hitObjectMaterialType.t_Name));

                hitObjectHealth.ReceiveDamage(foundElement.m_BulletDamage);

                if (m_WeaponType.Equals(RangedWeaponType.Name.NormalBullet) && 
                    hitObjectMaterialType.t_Name.Equals(MaterialType.Name.Metal) && 
                    m_BulletImpactSoundObjectPrefab != null)
                {
                    Instantiate(m_BulletImpactSoundObjectPrefab, lastBulletPosition, Quaternion.identity);
                }
            }
        }
    }
}