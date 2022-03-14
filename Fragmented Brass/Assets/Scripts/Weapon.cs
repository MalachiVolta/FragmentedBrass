using System.ComponentModel;
using System.Collections.Specialized;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;
using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class Weapon : MonoBehaviour, IWeapon
{
    public Animator viewModelAnimator;
    public Animation toolAnimation;
    public PlayerHandler player;
    public WeaponSway sway;
    public VisualRecoil VisualRecoil_Script;
    public GameObject crosshair;
    private Recoil Recoil_Script;

    public VisualEffect muzzleFlash;
    public Camera fpsCam;

    [SerializeField] private LayerMask Ignored;
    [SerializeField] private int damage = 10;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 7f;
    [SerializeField] private int magSize = 30;
    [SerializeField] private int chamberedSize = 31;
    [SerializeField] private float aimAnimationSpeed = 10f;
    [SerializeField] private Vector3 ADS;
    [SerializeField] private Vector3 hipfire;
    [SerializeField] private Quaternion ADS_rotation;
    [SerializeField] private Quaternion hipfire_rotation;

    //Hipfire Recoil
    public float recoilX;
    public float recoilY;
    public float recoilZ;

    //ADS Recoil
    public float aimRecoilX;
    public float aimRecoilY;
    public float aimRecoilZ;

    //Settings
    public float snapping;
    public float returnSpeed;

    private float nextTimeToFire = 0f;
    public bool isAiming = false;
    public int currentAmmo = 31;

    private void Awake()
    {
        Recoil_Script = player.transform.Find("CameraRotation/CameraRecoil").GetComponent<Recoil>();
    }

    void Update()
    {
        if (player.isBuilding == false)
        {
            if (Input.GetButton("Fire1") && Time.time - nextTimeToFire > 1 / fireRate && currentAmmo > 0 && !viewModelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature_001|GunReloadFull") && !viewModelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature_001|GunDraw_001"))
            {
                viewModelAnimator.SetTrigger("Shoot");
                nextTimeToFire = Time.time;
                Shoot();
            }

            if (Input.GetButtonDown("Reload") && currentAmmo < chamberedSize)
            {
                viewModelAnimator.SetTrigger("Reload");
                Reload();
            }

            if (Input.GetMouseButton(1))
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, ADS, aimAnimationSpeed * Time.deltaTime);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, ADS_rotation, aimAnimationSpeed * Time.deltaTime);
                sway.isActive = false;
                crosshair.SetActive(false);
                viewModelAnimator.SetBool("Aim", true);
                isAiming = true;
            }
            if (!Input.GetMouseButton(1) && transform.localPosition != hipfire)
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, hipfire, aimAnimationSpeed * Time.deltaTime);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, hipfire_rotation, aimAnimationSpeed * Time.deltaTime);
                sway.isActive = true;
                crosshair.SetActive(true);
                viewModelAnimator.SetBool("Aim", false);
                isAiming = false;
            }
        }
    }

    public void Shoot()
    {
        muzzleFlash.Play();
        Recoil_Script.RecoilFire();
        VisualRecoil_Script.Fire();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~Ignored))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Hit(damage);
            }
        }
        currentAmmo--;
    }

    public void Reload()
    {
        if (currentAmmo == 0)
        {
            currentAmmo = magSize;
        }
        else
        {
            currentAmmo = chamberedSize;
        }
    }

    public void EquipTool()
    {
        toolAnimation.Play("Armature|ToolEquip");
    }
    public void EquipGun()
    {
        toolAnimation.Play("Armature|ToolUnequip");
    }

}
