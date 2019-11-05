using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_SpawnTracker : D_DirectorObject
{
    
    private static Dictionary<string, List<GameObject>> objects = new Dictionary<string, List<GameObject>>();
    // Start is called before the first frame update
    void Start()
    {
        var values = spawnTypes.GetValues(typeof(spawnTypes));
        foreach (var val in values)
        {
            objects.Add(val.ToString(), new List<GameObject>());
        }
        director.getDirectorScript().addToUpdateList(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void shortUpdate()
    {
        base.shortUpdate();
        foreach (var obj in objects)
        {
            for ( int i = obj.Value.Count-1; i >=0; i--)
            {
                if (obj.Value[i] == null)
                    obj.Value.RemoveAt(i);
            }

            director.getData().getFloat(obj.Key + " Count").value = obj.Value.Count;
        }
    }


    public void registerObject(GameObject obj, spawnTypes type)
    {
        objects[type.ToString()].Add(obj);
    }

    public List<GameObject> getObjectsOfType(spawnTypes type)
    {
        return objects[type.ToString()];
    }

    public int getCountOfType(spawnTypes type)
    {
        return objects[type.ToString()].Count;
    }
}
