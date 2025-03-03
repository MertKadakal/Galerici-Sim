using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlatformTabela : MonoBehaviour
{
    public int id;

    void Update()
    {
        if (AraçAlımSatım.sergiler[id].gelişFiyat == 0) {
            GetComponent<TMP_Text>().text = "";
        } else {
            GetComponent<TMP_Text>().text = string.Format("<b>Fiyat:</b> {0:N0}\n<b>Sene:</b> {1}", AraçAlımSatım.sergiler[id].gelişFiyat, (AraçAlımSatım.sergiler[id].sene == -1) ? "Özel" : AraçAlımSatım.sergiler[id].sene);
        }
    }
}
