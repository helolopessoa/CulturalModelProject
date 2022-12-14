using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadialMenu
{
    public class MenuItem
    {
        public string title;
        public int tag;

        Sprite icon;

        List<MenuItem> subItems = new List<MenuItem>();

        private MenuItem father;

        public MenuItem Father { get { return father; } }

        public MenuItem(string title)
        {
            this.title = title;
        }
        public MenuItem(string title, Sprite icon)
        {
            this.title = title;
            this.icon = icon;
        }

        public MenuItem[] GetSubItems()
        {
            return subItems.ToArray();
        }


        public MenuItem AddSubItem(string title, Sprite icon = null)
        {
            return AddSubItem(new MenuItem(title, icon));
        }

        public MenuItem AddSubItem(MenuItem item)
        {
            item.father = this;
            subItems.Add(item);
            return this;
        }

        public MenuItem AddSubItems(MenuItem[] items)
        {
            foreach (MenuItem it in items)
            {
                AddSubItem(it);
            }
            return this;
        }
    }

}