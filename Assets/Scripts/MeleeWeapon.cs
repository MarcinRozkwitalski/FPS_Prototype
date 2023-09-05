using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class MeleeWeapon : MonoBehaviour
{
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

    public BoxCollider m_BoxCollider;
    public CapsuleCollider m_CapsuleCollider;

    [SerializeField]
    private GameObject m_SledgeHammerImpactSoundObjectPrefab;

    public Animator m_WeaponAnimator;
    public AnimationClip anim_Attack;

    public float timeBetweenAttacks;

    bool attacking, readyToAttack;

    public TextMeshProUGUI ammunitionDisplay;

    private void Awake()
    {
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();
        m_CapsuleCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    private void OnEnable() 
    {
        readyToAttack = false;
        StartCoroutine(DelayedUsage(0.6f));
    }

    private void Update()
    {
        MyInput();

        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText("");
    }

    private void MyInput()
    {
        attacking = Input.GetKeyDown(KeyCode.Mouse0);

        if (attacking && readyToAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        readyToAttack = false;
        m_WeaponAnimator.SetTrigger("hasAttacked");

        StartCoroutine(DelayedColliderOn(0.8f));
        StartCoroutine(DelayedColliderOff(1.17f));
        StartCoroutine(DelayedUsage(2.33f));
    }

    IEnumerator StopOnHit()
    {
        m_WeaponAnimator.speed = 0;
        Instantiate(m_SledgeHammerImpactSoundObjectPrefab, m_BoxCollider.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.2f);

        m_WeaponAnimator.StartPlayback();
        m_WeaponAnimator.speed = -1;

        var totalFrames = m_WeaponAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length * (m_WeaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * m_WeaponAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;

        yield return new WaitForSeconds(totalFrames/60);

        m_WeaponAnimator.speed = 1;
        m_WeaponAnimator.Play("Attack", 0, 0.99f);
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;

        m_BoxCollider.enabled = false;
        m_CapsuleCollider.enabled = false;

        if (hitObject.CompareTag("Destroyable") && 
            hitObject.GetComponent<MaterialType>() != null)
        {
            MaterialType hitObjectMaterialType = hitObject.GetComponent<MaterialType>();
            Health hitObjectHealth = hitObject.GetComponent<Health>();

            if (materialTypeDamage.Any(f => f.m_AttackableMaterialType.Contains(hitObjectMaterialType.t_Name)))
            {
                var foundElement = materialTypeDamage.FirstOrDefault(f => f.m_AttackableMaterialType.Contains(hitObjectMaterialType.t_Name));

                hitObjectHealth.ReceiveDamage(foundElement.m_BulletDamage);

                if (hitObjectHealth.m_HealthPoints > 0)
                StartCoroutine(StopOnHit());
                else
                Instantiate(m_SledgeHammerImpactSoundObjectPrefab, m_BoxCollider.transform.position, Quaternion.identity);
            }
        }
        else
        StartCoroutine(StopOnHit());
    }

    IEnumerator DelayedUsage(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        readyToAttack = true;
    }

    IEnumerator DelayedColliderOn(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        m_BoxCollider.enabled = true;
        m_CapsuleCollider.enabled = true;
    }

    IEnumerator DelayedColliderOff(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        m_BoxCollider.enabled = false;
        m_CapsuleCollider.enabled = false;
    }
}