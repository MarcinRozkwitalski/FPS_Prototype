using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using static ProjectileGun;

public class Bullet : MonoBehaviour
{
    private IObjectPool<Bullet> m_BulletPool;

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
        m_BulletPool = pool;
    }

    bool m_HasHit = false;
    public MaterialTypeDamage attackableMaterialType;
    public RangedWeaponType.Name weaponType;
    public int damage = 0;

    private void OnEnable()
    {
        m_HasHit = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!m_HasHit)
        {
            m_HasHit = true;

            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            Vector3 lastBulletPosition = gameObject.transform.position;

            m_BulletPool.Release(this);

            if (damageable != null)
            {
                GameObject hitObject = collision.gameObject;

                MaterialType hitObjectMaterialType = hitObject.GetComponent<MaterialType>();
                HealthObject hitObjectHealth = hitObject.GetComponent<HealthObject>();

                if (materialTypeDamage.Any(f => f.m_AttackableMaterialType.Contains(hitObjectMaterialType.t_Name)))
                {
                    var foundElement = materialTypeDamage.FirstOrDefault(f => f.m_AttackableMaterialType.Contains(hitObjectMaterialType.t_Name));

                    damageable.TakeDamage(foundElement.m_BulletDamage);

                    if (weaponType.Equals(RangedWeaponType.Name.NormalBullet) && 
                        hitObjectMaterialType.t_Name.Equals(MaterialType.Name.Metal) && 
                        m_BulletImpactSoundObjectPrefab != null)
                    {
                        Instantiate(m_BulletImpactSoundObjectPrefab, lastBulletPosition, Quaternion.identity);
                    }
                }
            }
        }
    }
}