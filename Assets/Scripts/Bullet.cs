using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private IObjectPool<Bullet> bulletPool;

    private void Awake() {
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

        this.gameObject.SetActive(false);


        if (hitObject.CompareTag("Destroyable"))
        {
            if (hitObject.GetComponent<Material>() != null)
            {
                Material hitObjectMaterial = hitObject.GetComponent<Material>();
                Health hitObjectHealth = hitObject.GetComponent<Health>();

                if (mWeaponType == "Pistol" && hitObjectMaterial.materialType == Material.Type.Metal)
                {
                    hitObjectHealth.ReceiveDamage(damage);
                }
            }
        }
    }

    private void OnBecameInvisible() {
        bulletPool.Release(this);
    }
}
