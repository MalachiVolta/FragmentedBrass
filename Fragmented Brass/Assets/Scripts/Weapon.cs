using System.ComponentModel;
using System.Collections.Specialized;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;
using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class Weapon : MonoBehaviour,IWeapon
{
    public WeaponSway sway;
    public GameObject crosshair;
  public LayerMask Ignored;
  public int damage = 10;
  public float range = 100f;
  public float fireRate = 7f;
  public int magSize = 30;
  public int chamberedSize = 31;
  [SerializeField]private float aimAnimationSpeed = 10f;
    public Vector3 ADS;
    public Vector3 hipfire;
    public Quaternion ADS_rotation;
    public Quaternion hipfire_rotation;

  public int currentAmmo = 31;

  public VisualEffect muzzleFlash;

  public Camera fpsCam;

  private float nextTimeToFire = 0f;

    private void Awake()
    {
    
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time - nextTimeToFire > 1 / fireRate && currentAmmo>0)
        {
          nextTimeToFire = Time.time;
          Shoot();
        }

        if(Input.GetButtonDown("Reload") && currentAmmo < chamberedSize)
        {
          Reload();
        }
        
        if(Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, ADS, aimAnimationSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, ADS_rotation, aimAnimationSpeed * Time.deltaTime);
            sway.isActive = false;
            crosshair.SetActive(false);
        }
        if(!Input.GetMouseButton(1) && transform.localPosition != hipfire)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, hipfire, aimAnimationSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, hipfire_rotation, aimAnimationSpeed * Time.deltaTime);
            sway.isActive = true;
            crosshair.SetActive(true);
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
