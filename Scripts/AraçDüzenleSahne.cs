using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class AraçDüzenleSahne : MonoBehaviour
{
    public Material material;
    public Material materialWheel;  // Materyal referansı
    public Texture[] textures;  // Texture dizisi
    public Color[] colors;
    private int currentIndex = 0;
    private int currentIndex2 = 0;  // Mevcut texture indeksini takip etmek için sayaç

    public void ChangeBodyColor()
    {
        // Eğer texture'lar dizisi boş değilse ve material atanmışsa
        if (textures.Length > 0 && material != null)
        {
            // Yeni texture'ı mevcut indeks ile seç
            material.SetTexture("_BaseMap", textures[currentIndex]);

            // Bir sonraki texture'a geç
            currentIndex++;

            // Eğer dizinin sonuna gelindiyse, başa dön
            if (currentIndex >= textures.Length)
            {
                currentIndex = 0;  // İndeksi sıfırla, diziyi döngüsel hale getir
            }
        }
    }

    public void ChangeWheelColor()
    {
        // Eğer renkler dizisi boş değilse ve material atanmışsa
        if (colors.Length > 0 && material != null)
        {
            // Yeni rengi mevcut indeks ile seç
            materialWheel.SetColor("_BaseColor", colors[currentIndex2]);

            // Bir sonraki renge geç
            currentIndex2++;

            // Eğer dizinin sonuna gelindiyse, başa dön
            if (currentIndex2 >= colors.Length)
            {
                currentIndex2 = 0;  // İndeksi sıfırla, diziyi döngüsel hale getir
            }
        }
    }

    public void Back() {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
