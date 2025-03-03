using UnityEngine;
using TMPro;  // TextMeshPro için eklenmeli

public class KöşeBilgiler : MonoBehaviour
{
    public TextMeshProUGUI modeText;
    public TMP_Text galeriIsmi;
    public static string galeriname = "Galerim";
    
    void Update()
    {
        if (FirstPersonLook.stopped) {modeText.text = "";} else {modeText.text = string.Format("Para : {0:N0}", AraçAlımSatım.para);}
        galeriIsmi.text = galeriname;
    }
}
