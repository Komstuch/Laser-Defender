using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] AudioClip fireSound;

    private float averageTimeBetweenShots = 2f;
    private int numberOfProjectiles = 6;
    private float projectileSpeed = 1f;

    private float[] offsets = { 50f, 90f, 120f, 240f, 270f, 310f };

    private void Start()
    {
        GetComponent<Rigidbody2D>().angularVelocity =  30f;
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity / 2f;
    }
    private void Update()
    {
        float propability = Time.deltaTime / averageTimeBetweenShots;
        if (Random.value < propability)
        {
            Fire();
        }
    }

    private void Fire()
    {
        float currentAngle = 360 - transform.rotation.eulerAngles.z;
        
        Debug.Log("Current Angle: " + currentAngle);
        for(int i = 0; i < numberOfProjectiles; i++)
        {
            float radianAngle = (currentAngle + offsets[i]) * Mathf.Deg2Rad;
            Vector2 currentVelocity = new Vector2(projectileSpeed * Mathf.Sin(radianAngle), projectileSpeed * Mathf.Cos(radianAngle));
            GameObject laser = Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, -(currentAngle + offsets[i])));
            laser.GetComponent<Rigidbody2D>().velocity = currentVelocity;
            AudioSource.PlayClipAtPoint(fireSound, Camera.main.transform.position, 0.3f);
        }
    }
}
