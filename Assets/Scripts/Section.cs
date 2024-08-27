using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    public GameObject objectPrefab;

    public Stack<GameObject> stackObjects = new Stack<GameObject>();

    [SerializeField] private int objectLimit = 5;
    public void AddObject()
    {
        if (stackObjects.Count < objectLimit)
        {
            GameObject element = Instantiate(objectPrefab, transform);
            element.transform.position = new Vector3((transform.position.x - 0.4f) + 0.2f * stackObjects.Count, transform.position.y + 0.2f, transform.position.z);
            stackObjects.Push(element);
        }
    }

    public void TakeOutObject()
    {
        if (stackObjects.Count > 0)
        {
            Destroy(stackObjects.Pop());
        }
    }
}
