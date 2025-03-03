using UnityEngine;

public class ObjeyeYakınsaGöster : MonoBehaviour
{
    public GameObject obje;
    public GameObject obje2;

    public float distanceToObje;

    void Start()
    {
        GetComponent<CanvasRenderer>().SetAlpha(0f);
    }

    void Update()
    {
        distanceToObje = Vector3.Distance(obje2.transform.position, obje.transform.position);
        if (distanceToObje < 3f) {
            GetComponent<CanvasRenderer>().SetAlpha(1f);
        } else {
            GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
    }
}
