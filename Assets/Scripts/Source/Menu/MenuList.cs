using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RadialMenu
{

    public class MenuList : MonoBehaviour
    {

        private GameObject[] objects;

        public float radius;
        public GameObject itemPrefab;

        public float spawnDuration = 0.6f;
        public float spawnAngle = 90f;

        private MenuItem prevFather;
        private MenuItem[] currentItem;
        private GameObject backItem;

        public Action<MenuItem,bool> itemSelected;
        public Action onClose;

        private bool activated;
        //private bool busy;

        private void SetItem(GameObject obj, MenuItem item, int index)
        {
            obj.GetComponentInChildren<Text>().text = item.title;
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectItem(index);
            });
        }

        public void BackAction()
        {
            if (!activated) return;

            if(prevFather.Father != null)
            {
                prevFather = prevFather.Father;
                currentItem = prevFather.GetSubItems();
                StartCoroutine(ChangeItem());
            }
            else
            {
                Deactivate();
            }
        }

        private void SetBackItem()
        {
            GameObject obj = NewItem(-(float)Math.PI/2f);
            obj.GetComponentInChildren<Text>().text = "X";
            obj.GetComponent<Button>().onClick.AddListener(BackAction);

            backItem = obj;
        }

        private void SelectItem(int index)
        {
            MenuItem selection = currentItem[index];
            MenuItem[] sub = selection.GetSubItems();

            if (itemSelected != null)
                itemSelected.Invoke(selection, sub.Length == 0);

            if(sub.Length == 0)
            {
                //notify
                //itemSelected?.Invoke(selection, true);
            }
            else
            {
                //itemSelected?.Invoke(selection, false);
                //go to subitem
                prevFather = selection;
                currentItem = sub;
                StartCoroutine(ChangeItem());
            }
            
        }

        private void ClearObjects()
        {
            if (objects == null) return;
            foreach(GameObject obj in objects)
            {
                Destroy(obj);
            }
            objects = null;
        }

        private GameObject NewItem(float angle)
        {
            float x, y;
            GameObject item;
            x = Mathf.Cos(angle) * radius;
            y = Mathf.Sin(angle) * radius;
            item = Instantiate(itemPrefab, this.transform);
            item.transform.localPosition = new Vector3(x, y, 0f);
            return item;
        }

        private void setData(MenuItem[] items)
        {

            backItem.SetActive(true);
            //backItem.SetActive(prevFather.Father != null);

            ClearObjects();

            currentItem = items;
            int count = items.Length;
            float dif = Mathf.PI / (count - 1);
            float angle;
            GameObject item;
            objects = new GameObject[count];
            for (int i = 0; i < count; i++)
            {
                angle = dif * i;
                item = NewItem(angle);
                objects[i] = item;
                SetItem(item, items[i], i);
            }
        }

        public void SetData(MenuItem[] items)
        {
            MenuItem root = new MenuItem("");
            prevFather = root;
            prevFather.AddSubItems(items);
            setData(items);
        }

        private IEnumerator Effect(float dir, float t = 1f)
        {
            //transform.localScale = Vector3.zero;
            //transform.rotation = Quaternion.identity;
            float time = 0f;
            float factor;
            while (time < spawnDuration)
            {
                yield return null;
                time = Mathf.Min(time + Time.unscaledDeltaTime * t, spawnDuration);
                factor = time / spawnDuration;
                transform.localScale = Vector3.one * (dir * factor + (1 - dir) / 2);
                transform.localRotation = Quaternion.Euler(0, 0, spawnAngle * (-(dir + 1) / 2 + factor * dir));
            }
        }

        public IEnumerator ChangeItem()
        {
            yield return StartCoroutine(Effect(-1,2f));
            setData(currentItem);
            yield return StartCoroutine(Effect(1,2f));
        }

        public void Activate()
        {
            if (activated) return;
            activated = true;
            StartCoroutine(Effect(1));
        }

        public void Deactivate()
        {
            if (!activated) return;
            activated = false;
            StartCoroutine(Effect(-1));
            if (onClose != null)
                onClose.Invoke();
        }

        private void Awake()
        {
            SetBackItem();
            MenuItem[] dummy = { new MenuItem("Comprar"), new MenuItem("Vender"), new MenuItem("Sarrar") };
            MenuItem[] subs = new MenuItem[8];
            for (int i = 0; i < subs.Length; i++) subs[i] = new MenuItem(i.ToString());
            foreach (MenuItem it in dummy) it.AddSubItems(subs);
            SetData(dummy);

            transform.localScale = Vector3.zero;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (Input.GetKeyDown(KeyCode.A))
                Activate();
            else if (Input.GetKeyDown(KeyCode.S))
                Deactivate();
            else if (Input.GetKeyDown(KeyCode.D))
                StartCoroutine(ChangeItem());
                */
        }
    }

}
