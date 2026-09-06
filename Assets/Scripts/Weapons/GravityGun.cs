
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityGun : MonoBehaviour
{
    [SerializeField] private GravityProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera mainCamera;

    private GravityProjectile activeProjectile;
    private GameObject activeBlackHole;

    public void OnFire(InputValue value)
    {
        if(!value.isPressed)
        {
            return;
        }

        if(activeProjectile == null)
        {
            FireProjectile();
        }
        else
        {
            ActivateProjectile();
        }
    }

    private void FireProjectile()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        Vector2 fireDireciton = (mouseWorldPosition - firePoint.position).normalized;
        
        activeProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        activeProjectile.Initialize(fireDireciton);
    }

    private void ActivateProjectile()
    {
        if(activeBlackHole != null)
        {
            Destroy(activeBlackHole);
        }

        activeBlackHole = activeProjectile.ActivateBlackHole();

        activeProjectile = null;
    }
}
