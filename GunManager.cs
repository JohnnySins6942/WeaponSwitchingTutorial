using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunManager : MonoBehaviour
{
    public GunItem[] guns;
    public GunItem CurGun;
    public GunItem SecondGun;

    public Transform[] gunTransforms;

    public GameObject currentGunOBJ;

    public int gunint = -1;

    public Transform dropzone;

    public int currentGunint = 0;

    public PickupDetector detector;

    public Image CurWepImage, SecondWepImage;
    public TextMeshProUGUI curWepName, SecondWepName;

    public void onNewPickup()
    {
        if(currentGunint == 0)
        {
            if (SecondGun != null)
            {
                Instantiate(SecondGun.DroppedPrefab, dropzone.transform.position, dropzone.transform.rotation);
            }
            SecondGun = detector.gunInVicinity;
   
        }
        else if(currentGunint == 1)
        {
            if (CurGun != null)
            {
                Instantiate(CurGun.DroppedPrefab, dropzone.transform.position, dropzone.transform.rotation);
            }
            CurGun = detector.gunInVicinity;
        
        }
        if (CurGun != null)
        {
            CurWepImage.enabled = true;
            CurWepImage.sprite = CurGun.gunIcon;
            curWepName.text = CurGun.name;
        }
        else
        {
            CurWepImage.enabled = false;
            curWepName.text = "no weapon";
        }
        if (SecondGun != null)
        {
            SecondWepImage.enabled = true;
            SecondWepImage.sprite = SecondGun.gunIcon;
            SecondWepName.text = SecondGun.name;
        }
        else
        {
            SecondWepImage.enabled = false;
            SecondWepImage.sprite = null;
            SecondWepName.text = "no weapon";
        }
    }
    private void Start()
    {
        Load();

        for (int i = 0; i < guns.Length; i++)
        {
            if(CurGun == guns[i])
            {
                gunint = i;
                break;
            }
        }
        if (CurGun != null)
        {
            var instantiatedGun = Instantiate(CurGun.Prefab, gunTransforms[gunint].position, gunTransforms[gunint].rotation);
            instantiatedGun.transform.SetParent(this.transform);
            instantiatedGun.transform.localPosition = gunTransforms[gunint].position;
            instantiatedGun.transform.localRotation = gunTransforms[gunint].rotation;
            Destroy(currentGunOBJ);
            currentGunOBJ = instantiatedGun;
        }
        else
        {
            var instantiatedGun = Instantiate(SecondGun.Prefab, gunTransforms[gunint].position, gunTransforms[gunint].rotation);
            instantiatedGun.transform.SetParent(this.transform);
            instantiatedGun.transform.localPosition = gunTransforms[gunint].position;
            instantiatedGun.transform.localRotation = gunTransforms[gunint].rotation;
            Destroy(currentGunOBJ);
            currentGunOBJ = instantiatedGun;
        }
        if (CurGun != null)
        {
            CurWepImage.enabled = true;
            CurWepImage.sprite = CurGun.gunIcon;
            curWepName.text = CurGun.name;
        }
        else
        {
            CurWepImage.enabled = false;
            curWepName.text = "no weapon";
        }
        if (SecondGun != null)
        {
            SecondWepImage.enabled = true;
            SecondWepImage.sprite = SecondGun.gunIcon;
            SecondWepName.text = SecondGun.name;
        }
        else
        {
            SecondWepImage.enabled = false;
            SecondWepImage.sprite = null;
            SecondWepName.text = "no weapon";
        }
       
       
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    void Save()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if(guns[i] == CurGun)
            {
                PlayerPrefs.SetInt("curGunIndex", i);
            }
        }
        if(CurGun == null)
        {
            PlayerPrefs.SetInt("curGunIndex", -1);
        }
        for (int j = 0; j < guns.Length; j++)
        {
            if (guns[j] == SecondGun)
            {
                PlayerPrefs.SetInt("secondaryGunIndex", j);
            }
        }
        if (SecondGun == null)
        {
            PlayerPrefs.SetInt("secondaryGunIndex", -1);
        }
    }
    void Load()
    {
        if (PlayerPrefs.HasKey("curGunIndex"))
        {
            if (PlayerPrefs.GetInt("curGunIndex") > -1)
            {
                CurGun = guns[PlayerPrefs.GetInt("curGunIndex")];
            }
            else
            {
                CurGun = null;
            }
        }
        if (PlayerPrefs.HasKey("secondaryGunIndex"))
        {
            if (PlayerPrefs.GetInt("secondaryGunIndex") > -1)
            {
                SecondGun = guns[PlayerPrefs.GetInt("secondaryGunIndex")];
            }
            else
            {
                SecondGun = null;
            }
        }
    }

    private void LateUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel")> 0)
        {
            if (currentGunint == 0)
            {
                if(SecondGun == null)
                {
                    return;
                }
                else
                {
                    currentGunint++;
                }
            }
            else
            {
                if (CurGun == null)
                {
                    return;
                }
                else
                {
                    currentGunint = 0;
                }
            }
            weaponChange();

        }
        else if(Input.GetAxis("Mouse ScrollWheel") <0) {
            if(currentGunint == 1)
            {
                if (CurGun == null)
                {
                    return;
                }
                else
                {
                    currentGunint--;
                }
            }
            else
            {
                if (SecondGun == null)
                {
                    return;
                }
                else
                {
                    currentGunint = 1;
                }
            }
            weaponChange();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DropWeapon();
        }    
    }
    void DropWeapon()
    {
        if (CurGun && SecondGun != null)
        {
            if (currentGunint == 0)
            {
                Instantiate(CurGun.DroppedPrefab, dropzone.transform.position, dropzone.transform.rotation);
                CurGun = null;
                currentGunint = 1;
                gunint = 0;
                var instantiatedGun = Instantiate(SecondGun.Prefab, gunTransforms[gunint].position, gunTransforms[gunint].rotation);
                instantiatedGun.transform.SetParent(this.transform);
                instantiatedGun.transform.localPosition = gunTransforms[gunint].position;
                instantiatedGun.transform.localRotation = gunTransforms[gunint].rotation;
                Destroy(currentGunOBJ);
                if (CurGun != null)
                {
                    CurWepImage.enabled = true;
                    CurWepImage.sprite = CurGun.gunIcon;
                    curWepName.text = CurGun.name;
                }
                else
                {
                    CurWepImage.enabled = false;
                    curWepName.text = "no weapon";
                }
                if (SecondGun != null)
                {
                    SecondWepImage.enabled = true;
                    SecondWepImage.sprite = SecondGun.gunIcon;
                    SecondWepName.text = SecondGun.name;
                }
                else
                {
                    SecondWepImage.enabled = false;
                    SecondWepImage.sprite = null;
                    SecondWepName.text = "no weapon";
                }
                currentGunOBJ = instantiatedGun;
            }
            else
            {

                Instantiate(SecondGun.DroppedPrefab, dropzone.transform.position, dropzone.transform.rotation);
                SecondGun = null;
                currentGunint = 0;
                gunint = 1;
                var instantiatedGun = Instantiate(CurGun.Prefab, gunTransforms[gunint].position, gunTransforms[gunint].rotation);
                instantiatedGun.transform.SetParent(this.transform);
                instantiatedGun.transform.localPosition = gunTransforms[gunint].position;
                instantiatedGun.transform.localRotation = gunTransforms[gunint].rotation;
                if (CurGun != null)
                {
                    CurWepImage.enabled = true;
                    CurWepImage.sprite = CurGun.gunIcon;
                    curWepName.text = CurGun.name;
                }
                else
                {
                    CurWepImage.enabled = false;
                    curWepName.text = "no weapon";
                }
                if (SecondGun != null)
                {
                    SecondWepImage.enabled = true;
                    SecondWepImage.sprite = SecondGun.gunIcon;
                    SecondWepName.text = SecondGun.name;
                }
                else
                {
                    SecondWepImage.enabled = false;
                    SecondWepImage.sprite = null;
                    SecondWepName.text = "no weapon";
                }
                Destroy(currentGunOBJ);
                currentGunOBJ = instantiatedGun;
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
            if (CurGun != null)
            {
                CurWepImage.enabled = true;
                CurWepImage.sprite = CurGun.gunIcon;
                curWepName.text = CurGun.name;
            }
            else
            {
                CurWepImage.enabled = false;
                curWepName.text = "no weapon";
            }
            if (SecondGun != null)
            {
                SecondWepImage.enabled = true;
                SecondWepImage.sprite = SecondGun.gunIcon;
                SecondWepName.text = SecondGun.name;
            }
            else
            {
                SecondWepImage.enabled = false;
                SecondWepImage.sprite = null;
                SecondWepName.text = "no weapon";
            }
            currentGunOBJ = instantiatedGun;
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
            if (CurGun != null)
            {
                CurWepImage.enabled = true;
                CurWepImage.sprite = CurGun.gunIcon;
                curWepName.text = CurGun.name;
            }
            else
            {
                CurWepImage.enabled = false;
                curWepName.text = "no weapon";
            }
            if (SecondGun != null)
            {
                SecondWepImage.enabled = true;
                SecondWepImage.sprite = SecondGun.gunIcon;
                SecondWepName.text = SecondGun.name;
            }
            else
            {
                SecondWepImage.enabled = false;
                SecondWepImage.sprite = null;
                SecondWepName.text = "no weapon";
            }
            currentGunOBJ = instantiatedGun;
        }
    }

}
