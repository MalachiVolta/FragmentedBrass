using UnityEngine;

public class Weapon : MonoBehaviour
{
  public LayerMask Ignored;
  public int damage = 10;
  public float range = 100f;
  public float fireRate = 15f;

  public ParticleSystem muzzleFlash;

  public Camera fpsCam;

  private float nextTimeToFire = 0f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
          nextTimeToFire = Time.time + 1f/fireRate;
          Shoot();
        }
        
    }

    void Shoot()
    {
      muzzleFlash.Play();

      RaycastHit hit;
      if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~Ignored))
      {
        Debug.Log(hit.transform.name);
        Targetable target = hit.transform.GetComponent<Targetable>();
        if(target != null)
        {
          target.Hit(damage);
        }
      }
    }
}
