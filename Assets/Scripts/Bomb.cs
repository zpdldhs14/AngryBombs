using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int explosionDamage = 50; // 폭발 데미지
    public float explosionRadius = 5f; // 폭발 반경
    public LayerMask damageableLayers; // 데미지를 입힐 레이어 (몬스터, 구조물)
    public GameObject explosionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌이 발생했을 때 데미지를 주고 폭발 처리
        DealDamage(collision.collider);
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // 폭탄 제거
        Destroy(gameObject);
    }

    private void DealDamage(Collider hitCollider)
    {
        // 몬스터인지 확인
        if (hitCollider.TryGetComponent<Monster>(out Monster monster))
        {
            // 몬스터에게 데미지 적용
            monster.Damage(explosionDamage);
        }

        // 구조물인지 확인
        if (hitCollider.TryGetComponent<Structure>(out Structure structure))
        {
            // 구조물에게 데미지 적용
            structure.TakeDamage(explosionDamage);
        }
        
        

        // 폭발 반경 내 추가 피해
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, damageableLayers);
        foreach (var nearbyCollider in hitColliders)
        {
            // 각 객체에 일괄적으로 데미지를 주는 로직 (폭발 반경 적용)
            if (nearbyCollider.TryGetComponent<Monster>(out Monster nearbyMonster) && nearbyMonster != monster)
            {
                nearbyMonster.Damage(explosionDamage);
            }
            else if (nearbyCollider.TryGetComponent<Structure>(out Structure nearbyStructure) && nearbyStructure != structure)
            {
                nearbyStructure.TakeDamage(explosionDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Scene 뷰에서 폭발 반경을 시각적으로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}