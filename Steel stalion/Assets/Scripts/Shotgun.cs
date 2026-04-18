using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Camera playerCamera;
    public int pelletCount = 8;
    public float spreadAngle = 10f;
    public float range = 50f;
    public float damage = 10f;

    public float primaryFireRate = 0.2f;
    public float secondaryFireRate = 0.6f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + primaryFireRate;
            Shoot(1);
        }

        if (Input.GetButtonDown("Fire2") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + secondaryFireRate;
            Shoot(3);
        }
    }

    void Shoot(int shotCount)
    {
        for (int s = 0; s < shotCount; s++)
        {
            for (int i = 0; i < pelletCount; i++)
            {
                // Get the camera's aim direction with spread applied
                Vector3 spread = new Vector3(
                    Random.Range(-spreadAngle, spreadAngle),
                    Random.Range(-spreadAngle, spreadAngle),
                    0
                );

                Vector3 aimDirection = playerCamera.transform.forward;
                aimDirection += playerCamera.transform.TransformDirection(spread * 0.01f);

                // Fire ray from the shotgun toward where camera is pointing
                Ray ray = new Ray(transform.position, aimDirection);
                Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.1f);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, range))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        hit.collider.GetComponent<CatAI>().TakeDamage(damage); ;
                    }
                }
            }
        }
    }
}