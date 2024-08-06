//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using RadialMenu;

//public class MenuController3D : MonoBehaviour {

//    bool busy = false;
//    Enemy currentEn;

//    public string Interactable;

//    private MenuList menu;

//    private void Awake()
//    {
//        menu = GetComponent<MenuList>();

//        menu.itemSelected = OnItemSelection;
//        menu.onClose = OnClose;
//    }

//    // Update is called once per frame
//    void Update () {
//		if(Input.GetMouseButtonDown(0))
//        {
//            Camera cam = Camera.main;
//            Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
//            Collider2D[] col = Physics2D.OverlapPointAll(pos);
//            foreach(Collider2D c in col)
//            {
//                if (c.CompareTag(Interactable))
//                {
//                    Time.timeScale = 0f;
//                    busy = true;
//                    transform.position = c.transform.position;
//                    currentEn = c.GetComponent<Enemy>();
//                    currentEn.engagedInAction = true;
//                    OpenMenu();
//                    break;
//                }
//            }
//        }
//	}

//    private void OpenMenu()
//    {
//        MenuItem[] allItems = DummyData();

//        menu.SetData(allItems);

//        menu.Activate();
//    }

//    private void OnItemSelection(MenuItem menuItem, bool isLast)
//    {
//        if (isLast)
//        {

//            string actionType = menuItem.Father.title;
//            string actionCmd = menuItem.title;

//            string action = actionType == "Item" ? actionCmd == "Give" ? "is_giving_item" : "is_stealing_item" : actionType == "Money" ? actionCmd == "Give" ? "is_giving_money" : "is_stealing_money" : actionType == "Talk" ? actionCmd == "Politely" ? "is_talking_politely" : "is_not_talking_politely" : "";

//            currentEn.DispatchPlayerState(action);

//            //do action
//            menu.Deactivate();
    
//        }
//    }

//    private void OnClose()
//    {
//        busy = false;
//        currentEn.engagedInAction = false;
//        currentEn = null;
//        Time.timeScale = 1f;
//    }

//    private MenuItem[] DummyData()
//    {
//        MenuItem items = new MenuItem("Item").AddSubItem("Give").AddSubItem("Steal");

//        MenuItem money = new MenuItem("Money").AddSubItem("Give").AddSubItem("Steal");

//        MenuItem talk = new MenuItem("Talk").AddSubItem("Politely").AddSubItem("Not Politelty");

//        MenuItem[] allItems = { items, money, talk };

//        return allItems;
//    }
//}
