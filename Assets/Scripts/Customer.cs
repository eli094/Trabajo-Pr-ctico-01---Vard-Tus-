using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public float searchTime = 2f;
    public float speedMovement = 3f;
    public float rangeOfReach = 0.5f;
    public float waitDuration = 3f;
    private float initialTime;

    private int lineNumber;
    private int placeLine;
    private int actualPlaceLine;

    private GameObject collectedObject;

    public Transform aimedSection;
    public Transform exitCustomer;

    public StackCustomer stackCustomer;

    private void Awake()
    {
        stackCustomer = GameObject.FindGameObjectWithTag("StackCustomers").GetComponent<StackCustomer>();
    }

    private void Start()
    {
        lineNumber = stackCustomer.lineCustomers.Count - 1;
        placeLine = lineNumber;
        actualPlaceLine = placeLine;
    }

    private void Update()
    {
        if (exitCustomer == null)
        {
            lineNumber = stackCustomer.lineCustomers.Count - 1;

            if (lineNumber < placeLine)
            {
                actualPlaceLine--;
                aimedSection = stackCustomer.customersLine[actualPlaceLine];
            }
            placeLine = lineNumber;
        }

        if (aimedSection != null) 
        {
            if (Vector3.Distance(transform.position, aimedSection.position) > rangeOfReach)
            {
                transform.position = Vector3.MoveTowards(transform.position, aimedSection.position, speedMovement * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Section")
        {
            initialTime = Time.time;
        }
        
        if (other.gameObject.tag == "ExitCustomers")
        {
            if (FindObjectOfType<StackCustomer>().lineCustomers.Count != 0)
            {
                FindObjectOfType<StackCustomer>().exitCustomers();
            }

            Destroy(this.gameObject);
        }   
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Section" && Time.time - initialTime > waitDuration)
        {
            initialTime = Time.time;
            Section sections = other.GetComponent<Section>();

            if (sections.stackObjects.Count > 0)
            {
                sections.stackObjects.Peek().transform.SetParent(transform);
                sections.stackObjects.Pop().transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
                aimedSection = exitCustomer;
            }
            else
            {
                aimedSection = stackCustomer.finalShelf();
            }
        }
    }
}

