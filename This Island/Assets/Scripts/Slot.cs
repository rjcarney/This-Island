using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
 {
    private bool hovered;
    private bool empty;
    private GameObject item;
    private Texture itemIcon;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        hovered = false;
        empty = false;
        item = null;
        itemIcon = null;
        player = GameObject.FindWithTag("Player");
}

    // Update is called once per frame
    void Update()
    {
        if (item) {
            empty = false;
            itemIcon = item.GetComponent<Item>().icon;
        } else {
            empty = true;
            itemIcon = null;
        }
        this.GetComponent<RawImage>().texture = itemIcon;

        if (hovered) {

        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        hovered = false;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (item) {
            Item thisItem = item.GetComponent<Item>();

            switch (thisItem.type) {
                case "Water":
                    player.GetComponent<Player>().Drink(thisItem.decreaseRate);
                    Destroy(item);
                    break;
                case "Food":
                    player.GetComponent<Player>().Eat(thisItem.decreaseRate);
                    Destroy(item);
                    break;
            }
        }
    }

    public bool isEmpty() { return empty; }

    public GameObject getItem() { return item; }
    public void setItem(GameObject newItem) { item = newItem; }
}
