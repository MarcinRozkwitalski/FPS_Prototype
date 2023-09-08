using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileGun : MonoBehaviour, IWeaponUsage
{
    [SerializeField] Bullet bulletPrefab;
    private IObjectPool<Bullet> bulletPool;

    [System.Serializable]
    public class MaterialTypeDamage 
    {
        public MaterialType.Name[] m_AttackableMaterialType;
        public int m_BulletDamage;
        
        public MaterialTypeDamage (MaterialType.Name[] attackableMaterialType, int bulletDamage)
        {
            m_AttackableMaterialType = attackableMaterialType;
            m_BulletDamage = bulletDamage;
        }
    }

    [SerializeField]
    public List<MaterialTypeDamage> materialTypeDamage = new List<MaterialTypeDamage>();

    public RangedWeaponType.Name m_WeaponType;

    public float shootForce;

    public float timeBetweenShooting, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;

    public int bulletsLeft;

    bool shooting, readyToShoot, reloading;

    public Camera FPSCamera;
    public Transform attackPoint;

    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;

    private RangedWeaponSoundManager m_RangedWeaponSoundManager;
    private WeaponParticleManager m_WeaponParticleManager;

    IWeaponEffects weaponEffects;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGet,
            OnRelease,
            OnInstanceDestroy,
            maxSize:6
            );

        bulletsLeft = magazineSize;
        
        if (gameObject.GetComponent<RangedWeaponSoundManager>() != null)
            m_RangedWeaponSoundManager = gameObject.GetComponent<RangedWeaponSoundManager>();
        
        if (gameObject.GetComponent<WeaponParticleManager>() != null)
            m_WeaponParticleManager = gameObject.GetComponent<WeaponParticleManager>();
    }

    private void Start()
    {
        weaponEffects = GetComponent<IWeaponEffects>();
    }

    private void OnEnable() 
    {
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);

        readyToShoot = false;
        StartCoroutine(DelayedUsage(0.6f));
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.SetPool(bulletPool);

        readyToShoot = false;

        return bullet;
    }
    
    public void Use()
    {
        shooting = true;

        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
            Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            bulletPool.Get();

        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }

    public void Reload()
    {
        if (bulletsLeft < magazineSize && !reloading)
        {
            reloading = true;

            if (weaponEffects != null)
                weaponEffects.PlayReloadEffect(ammunitionDisplay);
            else
                Debug.LogError("Script " + weaponEffects + " not found!");

            Invoke("ReloadFinished", reloadTime);
        }
    }

    private void AddVelocityAndDirection(Bullet bullet)
    {
        if (weaponEffects != null)
            weaponEffects.PlayShootEffect();
        else
            Debug.LogError("Script " + weaponEffects + " not found!");

        Ray ray = FPSCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 direction = targetPoint - attackPoint.position;

        bullet.materialTypeDamage = materialTypeDamage;
        bullet.weaponType = m_WeaponType;

        Quaternion rotation = Quaternion.LookRotation(direction.normalized);
        bullet.transform.rotation = rotation;
        bullet.transform.rotation *= Quaternion.Euler(0, 90, 0);
        
        bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

        bulletsLeft--;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void OnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.transform.position = attackPoint.position;
        AddVelocityAndDirection(bullet);
    }

    private void OnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnInstanceDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    IEnumerator DelayedUsage(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        readyToShoot = true;
    }

    private void OnDisable()
    {
        CancelInvoke("ReloadFinished");
        reloading = false;
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        reloading = false;
    }    
}