using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] Camera cam;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) hit.transform.GetComponent<Dialogue>().GetDialogue();
        }
    }
}
