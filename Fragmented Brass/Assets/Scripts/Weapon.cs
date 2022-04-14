using System.ComponentModel;
using System.Collections.Specialized;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;
using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class Weapon : MonoBehaviour, IWeapon
{
    [Header("References")]
    public Animator viewModelAnimator;
    public Animation toolAnimation;
    public PlayerHandler player;
    public WeaponSway sway;
    public VisualRecoil VisualRecoil_Script;
    public GameObject crosshair;
    private Recoil Recoil_Script;
    public GameObject gun;
    public GameObject tool;

    public VisualEffect muzzleFlash;
    public Camera fpsCam;

    [Header("Audio Clips")]
    private AudioSource[] gunSound;
    public AudioClip shot;
    private AudioSource walkSound;
    public AudioClip[] walk;
    public AudioSource[] toolSound;

    [Header("Private Variables")]
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

    [Header("Recoil")]
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

    [Header("Assistive Variables")]
    private int index;
    private float nextTimeToFire = 0f;
    public bool isAiming = false;
    public int currentAmmo = 31;
    private bool canAim = false;

    private void Awake()
    {
        Recoil_Script = player.transform.Find("CameraRotation/CameraRecoil").GetComponent<Recoil>();
        gunSound = gun.GetComponents<AudioSource>();
        walkSound = this.GetComponent<AudioSource>();
        toolSound = tool.GetComponents<AudioSource>();
    }

    void LateUpdate()
    {
        if (!canAim && transform.localPosition != hipfire)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, hipfire, aimAnimationSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, hipfire_rotation, aimAnimationSpeed * Time.deltaTime);
            sway.isActive = true;
            crosshair.SetActive(true);
            viewModelAnimator.SetLayerWeight(1, 1);
            isAiming = false;
            viewModelAnimator.ResetTrigger("Shoot");
        }

        if (!player.isBuilding)
        {
            canAim = true;


            if (Input.GetButton("Fire1") && Time.time - nextTimeToFire > 1 / fireRate && currentAmmo > 0 && (viewModelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature_001|GunIdle") || viewModelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature_001|GunShoot")))
            {
                viewModelAnimator.SetTrigger("Shoot");
                nextTimeToFire = Time.time;
                Shoot();
                StartCoroutine(playAudio(shot));
            }

            if (Input.GetButtonDown("Reload") && currentAmmo < chamberedSize && !(viewModelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature_001|GunReloadFull")))
            {
                viewModelAnimator.SetTrigger("Reload");
            }

            if (Input.GetMouseButton(1) && canAim)
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, ADS, aimAnimationSpeed * Time.deltaTime);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, ADS_rotation, aimAnimationSpeed * Time.deltaTime);
                sway.isActive = false;
                crosshair.SetActive(false);
                viewModelAnimator.SetLayerWeight(1, 0.1f);
                isAiming = true;
            }
        }
        else
        {
            canAim = false;
        }

        if (!Input.GetMouseButton(1) && transform.localPosition != hipfire)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, hipfire, aimAnimationSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, hipfire_rotation, aimAnimationSpeed * Time.deltaTime);
            sway.isActive = true;
            crosshair.SetActive(true);
            viewModelAnimator.SetLayerWeight(1, 0);
            isAiming = false;
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
        viewModelAnimator.ResetTrigger("Reload");
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

    public void SoundEquip()
    {
        gunSound[0].Play();
    }
    public void SoundButton()
    {
        gunSound[5].Play();
    }
    public void SoundMagOut()
    {
        gunSound[3].Play();
    }
    public void SoundMagIn()
    {
        gunSound[2].Play();
    }
    public void SoundRechamber()
    {
        gunSound[4].Play();
    }

    IEnumerator playAudio(AudioClip soundEffect)
    {
        AudioSource.PlayClipAtPoint(soundEffect, transform.position);
        yield break;
    }

    public void SoundWalk()
    {
        index = Random.Range(0, walk.Length);
        StartCoroutine(playAudio(walk[index]));
    }
    public void SoundBeepP()
    {
        toolSound[0].Play();
    }
    public void SoundBeepN()
    {
        toolSound[1].Play();
    }
    public void SoundToolEquip()
    {
        toolSound[2].Play();
    }
}
