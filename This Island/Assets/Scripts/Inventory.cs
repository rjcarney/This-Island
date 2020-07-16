using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public GameObject inventory;
    public GameObject slotHolder;
    public GameObject itemManager;

    private int slots;
    private Transform[] slot;
    private bool inventoryEnabled;

    // Start is called before the first frame update
    void Start()
    {
        inventoryEnabled = true;
        slots = slotHolder.transform.childCount;
        slot = new Transform[slots];
        DetectInventorySlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            inventoryEnabled = !inventoryEnabled;
        }

        if (inventoryEnabled) {
            inventory.GetComponent<Canvas>().enabled = true;
            Cursor.lockState = CursorLockMode.None;
        } else {
            inventory.GetComponent<Canvas>().enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Item>()) {
            if (AddItem(other.gameObject)) {
                print("item added: " + other.gameObject.name);
            } else {
                print("problem adding item: " + other.gameObject.name);
            }
        }
    }

    public bool AddItem(GameObject itemPickedUp) {
        bool itemAdded = false;
        for(int i = 0; i < slots; i++) {
            if (slot[i].GetComponent<Slot>().isEmpty() && itemAdded == false) {
                slot[i].GetComponent<Slot>().setItem(itemPickedUp);

                itemPickedUp.transform.parent = itemManager.transform;
                itemPickedUp.transform.position = itemManager.transform.position;

                if (itemPickedUp.GetComponent<MeshRenderer>())
                    itemPickedUp.GetComponent<MeshRenderer>().enabled = false;

                Destroy(itemPickedUp.GetComponent<Rigidbody>());

                itemAdded = true;
            }
        }
        return itemAdded;
    }

    public void DetectInventorySlots() {
        for(int i = 0; i < slots; i++) {
            slot[i] = slotHolder.transform.GetChild(i);
            print(slot[i].name);
        }
        inventoryEnabled = false;
    }
}
