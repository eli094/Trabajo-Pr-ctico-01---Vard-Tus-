using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
 
    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        Ray myRay = _camera.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        bool hitFound = Physics.Raycast(myRay, out hit);

        if (Input.GetMouseButtonDown(0))
        {
            if (hitFound && hit.transform.tag == "Section")
            {
                hit.transform.GetComponent<Section>().AddObject();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (hitFound && hit.transform.tag == "Section")
            {
                hit.transform.GetComponent<Section>().TakeOutObject();
            }
        }
    }
}
