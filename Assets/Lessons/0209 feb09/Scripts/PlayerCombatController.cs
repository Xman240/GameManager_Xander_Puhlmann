using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : MonoBehaviour
{
    public InputAction attackInput;

    public Transform swordPivotPoint;
    public float swipeDuration;

    public float attackRadius;
    public int damageAmount;
    public LayerMask damageLayers;

    private bool isAttacking;

    private void Start()
    {
        attackInput.Enable();
        attackInput.performed += Attack;
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (isAttacking) return;
        StartCoroutine(SpinAttack());
    }

    private IEnumerator SpinAttack()
    {
        isAttacking = true;
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position,attackRadius,damageLayers);
        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable damageable = hits[i].GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }
        }

        if (swordPivotPoint != null)
        {
            float elapsedTime = 0f;
            float startingRotation = swordPivotPoint.rotation.eulerAngles.z;

            while (elapsedTime < swipeDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / swipeDuration);
                
                float z = startingRotation + 360f * -t;
                swordPivotPoint.rotation = Quaternion.Euler(0, 0, z);
                
                yield return null;
            }
            swordPivotPoint.localRotation = Quaternion.Euler(0, 0, startingRotation);
        }
        isAttacking = false;
    }

    private void OnDestroy()
    {
        attackInput.Disable();
        attackInput.performed -= Attack;
    }
}
