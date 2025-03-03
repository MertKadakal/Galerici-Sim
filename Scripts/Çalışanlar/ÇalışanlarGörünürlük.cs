using UnityEngine;

public class ÇalışanlarGörünürlük : MonoBehaviour
{
    public GameObject çalışan1;
    public GameObject çalışan2;
    public GameObject çalışan3;
    void Update()
    {
        
        çalışan1.SetActive((Çalışanlar.çalışanlar.Count > 0) ? true : false);
        çalışan2.SetActive((Çalışanlar.çalışanlar.Count > 1) ? true : false);
        çalışan3.SetActive((Çalışanlar.çalışanlar.Count > 2) ? true : false);
        
    }
}
