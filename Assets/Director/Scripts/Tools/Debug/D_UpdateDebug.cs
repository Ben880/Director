using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class D_UpdateDebug : MonoBehaviour
{
    private D_DirectorObjects _dDirector;
    public GameObject textPrefab;
    private List<GameObject> values = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        _dDirector = new D_DirectorObjects();
        addValues();

    }

    private void addValues()
    {
        for (int i = values.Count; i < _dDirector.getData().getFloatList().Count; i++)
        {
            values.Add(Instantiate(textPrefab, new Vector3(0, 0 + (i*10), 0), Quaternion.identity));
            values[i].transform.parent = gameObject.transform;
            values[i].GetComponent<RectTransform>().localPosition = new Vector3(-110, 40+(-13 * i), 0);
            //values[i].GetComponent<RectTransform>().localPosition = new Vector3(-200,13 * i,0);
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _dDirector.getData().getFloatList().Count; i++)
        {
            if (_dDirector.getData().getFloatList().Count > values.Count)
            {
                addValues();
            }
            values[i].GetComponent<TextMeshProUGUI>().text =
                _dDirector.getData().getFloatList()[i].key + ": " + _dDirector.getData().getFloatList()[i].value;
            //values[i].GetComponent<RectTransform>().localPosition =  new Vector3(0, 13 * i, 0);

        }

    }

}
