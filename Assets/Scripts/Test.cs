using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RadialMenu;

public class Test : MonoBehaviour
{


    public MenuList radialMenu;

    // Start is called before the first frame update
    void Start()
    {
        DummyData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DummyData()
    {
        MenuItem items = new MenuItem("Item").AddSubItem("Give").AddSubItem("Steal");

        MenuItem money = new MenuItem("Money").AddSubItem("Give").AddSubItem("Steal");

        MenuItem talk = new MenuItem("Talk").AddSubItem("Politely").AddSubItem("Not Politelty");

        MenuItem[] allItems = { items, money, talk };

        radialMenu.SetData(allItems);

        radialMenu.Activate();

        radialMenu.itemSelected = OnItemSelection;
    }

    private void OnItemSelection(MenuItem it, bool lastDeep)
    {
        if (lastDeep)
        {
            string type = it.Father.title;
            string command = it.title;
            //Debug.Log(type + " : " + command);
        }
    }
}
