using System.ComponentModel.DataAnnotations.Schema;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour,IWeapon
{
  public LayerMask Ignored;
  public int damage = 10;
  public float range = 100f;
  public float fireRate = 7f;
  public int magSize = 30;
  public int chamberedSize = 31;

  public int currentAmmo = 31;

  public VisualEffect muzzleFlash;

  public Camera fpsCam;

  private float nextTimeToFire = 0f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo>0)
        {
          nextTimeToFire = Time.time + 1f/fireRate;
          Shoot();
        }

        if(Input.GetButtonDown("Reload") && currentAmmo < chamberedSize)
        {
          Reload();
        }
        
    }

    public void Shoot()
    {
      muzzleFlash.Play();

      RaycastHit hit;
      if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~Ignored))
      {
        Debug.Log(hit.transform.name);
        Enemy enemy = hit.transform.GetComponent<Enemy>();
        if(enemy != null)
        {
          enemy.Hit(damage);
        }
      }
      currentAmmo--;
    }

    public void Reload()
    {
      if(currentAmmo==0)
      {
        currentAmmo=magSize;
      }
      else
      {
        currentAmmo=chamberedSize;
      }
    }
}
