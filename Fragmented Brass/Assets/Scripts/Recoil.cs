using UnityEngine;

public class Recoil : MonoBehaviour
{
    //Scripts
    [SerializeField] private Weapon Weapon_Script;

    //Rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Weapon_Script.returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, Weapon_Script.snapping * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        if(Weapon_Script.isAiming) targetRotation += new Vector3(Weapon_Script.aimRecoilX, Random.Range(-Weapon_Script.aimRecoilY, Weapon_Script.aimRecoilY), Random.Range(-Weapon_Script.aimRecoilZ, Weapon_Script.aimRecoilZ));
        else targetRotation += new Vector3(Weapon_Script.recoilX, Random.Range(-Weapon_Script.recoilY, Weapon_Script.recoilY), Random.Range(-Weapon_Script.recoilZ, Weapon_Script.recoilZ));
    }
}
