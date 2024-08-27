using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class StackCustomer : MonoBehaviour
{
    public GameObject customerPrefab;

    public float intervalSpawn = 5f;
    private float initialTime;

    private int limitLine = 3;

    private bool reachedLimit = false;

    public Transform exitCustomer;

    public Queue<Customer> lineCustomers = new Queue<Customer>();

    public List<Transform> customersLine = new List<Transform>();

    [SerializeField] private float PassingTime = 3f;

    private void Awake()
    {
        exitCustomer = GameObject.FindGameObjectWithTag("ExitCustomers").GetComponent<Transform>();
    }

    private void Start()
    {
        initialTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - initialTime > this.PassingTime && reachedLimit == false)
        {
            createCustomers();
            initialTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            exitCustomers();
        }
    }

    private void createCustomers()
    {
        if (customerPrefab != null && lineCustomers.Count < limitLine)
        {
            GameObject customerGo = Instantiate(customerPrefab);
            customerGo.transform.position = customersLine[lineCustomers.Count].position;
            lineCustomers.Enqueue(customerGo.GetComponent<Customer>());
        }
    }

    public void exitCustomers()
    {
        lineCustomers.Peek().exitCustomer = this.exitCustomer;
        lineCustomers.Dequeue().aimedSection = finalShelf();
    }

    public Transform finalShelf()
    {
        GameObject[] shelfArray = GameObject.FindGameObjectsWithTag("Shelf");
        Shelf shelves = shelfArray[Random.Range(0, shelfArray.Length)].GetComponent<Shelf>();
        return shelves.sections[Random.Range(0, shelves.sections.Count)].GetComponent<Transform>();
    }
}
