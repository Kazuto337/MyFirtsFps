using UnityEngine;
using Cinemachine;

public class GunBehavior : MonoBehaviour
{
    [SerializeField] LayerMask shootableLayer;
    InputManager inputManager;
    [SerializeField] Camera camera;
    [SerializeField] float damage, currentAmmo, maxAmmo, raycastRange;


    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    void Update()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward * raycastRange, Color.blue);
        if (inputManager.isPlayeFiring1())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward * raycastRange, Color.magenta);
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, raycastRange, shootableLayer))
        {
            print(hit.transform.name);

            EnemyBehavior enemy;
            enemy = hit.transform.gameObject.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.GetDamage(damage);
            }
        }
    }
}
