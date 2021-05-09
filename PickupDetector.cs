using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PickupDetector : MonoBehaviour
{
    public GunItem gunInVicinity;

    public GunManager manager;
    public GameObject physicalGun;


    public TextMeshProUGUI pickupText;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "gun")
        {
            pickupText.gameObject.SetActive(true);
            gunInVicinity = other.GetComponent<DroppedGunItem>().gun;
            pickupText.text = "pick up " + gunInVicinity.name;
            if (Input.GetKeyDown(KeyCode.E))
            {
                physicalGun = other.gameObject;
                manager.onNewPickup();
                Destroy(physicalGun);
                pickupText.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        pickupText.gameObject.SetActive(false);
    }
}
