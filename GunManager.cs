using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunItem[] guns;
    public GunItem CurGun;
    public GunItem SecondGun;

    public Transform[] gunTransforms;

    public GameObject currentGunOBJ;

    public int gunint = -1;

    public int currentGunint = 0;
    private void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if(CurGun == guns[i])
            {
                gunint = i;
                break;
            }
        }
        var instantiatedGun = Instantiate(CurGun.Prefab,gunTransforms[gunint].position, gunTransforms[gunint].rotation);
        instantiatedGun.transform.SetParent(this.transform);
        instantiatedGun.transform.localPosition = gunTransforms[gunint].position;
        instantiatedGun.transform.localRotation = gunTransforms[gunint].rotation;
        Destroy(currentGunOBJ);
        currentGunOBJ = instantiatedGun;
       
    }

    private void LateUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel")> 0)
        {
            if (currentGunint == 0)
            {
                currentGunint++;
            }
            else
            {
                currentGunint = 0;
            }
            weaponChange();
        }
        else if(Input.GetAxis("Mouse ScrollWheel") <0) {
            currentGunint--;
            weaponChange();
            if(currentGunint == 1)
            {
                currentGunint--;
            }
            else
            {
                currentGunint = 1;
            }
        }
    }
    void weaponChange()
    {
        if(currentGunint == 0)
        {
            for (int i = 0; i < guns.Length; i++)
            {
                if (CurGun == guns[i])
                {
                    gunint = i;
                    break;
                }
            }
            var instantiatedGun = Instantiate(CurGun.Prefab, gunTransforms[gunint].position, gunTransforms[gunint].rotation);
            instantiatedGun.transform.SetParent(this.transform);
            instantiatedGun.transform.localPosition = gunTransforms[gunint].position;
            instantiatedGun.transform.localRotation = gunTransforms[gunint].rotation;
            Destroy(currentGunOBJ);
            currentGunOBJ = instantiatedGun;
            print(gunTransforms[gunint].position);
        }
        else if(currentGunint >0)
        {
            for (int i = 0; i < guns.Length; i++)
            {
                if (SecondGun == guns[i])
                {
                    gunint = i;
                    break;
                }
            }
            var instantiatedGun = Instantiate(SecondGun.Prefab, gunTransforms[gunint].position, gunTransforms[gunint].rotation);
            instantiatedGun.transform.SetParent(this.transform);
            instantiatedGun.transform.localPosition = gunTransforms[gunint].position;
            instantiatedGun.transform.localRotation = gunTransforms[gunint].rotation;
            Destroy(currentGunOBJ);
            currentGunOBJ = instantiatedGun;
            print(gunTransforms[gunint].position);
        }
    }
}
