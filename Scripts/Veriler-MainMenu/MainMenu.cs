using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    VeriKapsayici veriKapsayici;
    VeriYoneticisi kayit;
    public GameObject mainMenu;
    

    void Start() {
        veriKapsayici = new VeriKapsayici();
        kayit = new VeriYoneticisi();
        Oyna();
    }

    public void Cikis()
    {
        // verileri kaydet
        veriKapsayici.arabalar = AraçAlımSatım.arabalar;
        veriKapsayici.çalışanlar = Çalışanlar.çalışanlar;
        veriKapsayici.mailler = Mailler.mailler;
        veriKapsayici.alışGeçmişi = AlışGeçmişi.geçmişAlımlar;
        veriKapsayici.satışGeçmişi = SatışGeçmişi.geçmişSatışlar;
        veriKapsayici.teklifArabalar = AraçAlımSatım.teklifArabalar;
        veriKapsayici.teklifPazarlıkGeçmişi = AraçAlımSatım.teklifPazarlıkGeçmişi;
        veriKapsayici.teklifFiyatlar = AraçAlımSatım.teklifFiyatlar;
        veriKapsayici.sergiler = AraçAlımSatım.sergiler;
        veriKapsayici.para = AraçAlımSatım.para;
        veriKapsayici.evNo = FirstPersonLook.evNo;
        veriKapsayici.galeri_ismi = KöşeBilgiler.galeriname;
        veriKapsayici.total_gider = AlışGeçmişi.geçmişAlımlarFiyat;

        kayit.VerileriKaydet(veriKapsayici);
        PlayerPrefs.SetInt("ever_played", 0);

        // Editör'de çalışırken durdurur
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Derlenmiş oyunda çıkış yapar
        Application.Quit();
        #endif
    }

    public void Oyna() {
        if (PlayerPrefs.GetInt("ever_played", 0) == 0) {
            PlayerPrefs.SetInt("ever_played", 1);

            if (PlayerPrefs.GetInt("tanitim") == 0 && PlayerPrefs.GetInt("first_on_the_computer", 1) == 1) {PlayerPrefs.SetInt("tanitim", 1);}

            // verileri çek
            AraçAlımSatım.arabalar = kayit.ArabalariYukle();
            AraçAlımSatım.teklifArabalar = kayit.TeklifArabalarYukle();
            AraçAlımSatım.sergiler = kayit.SergileriYukle();
            Çalışanlar.çalışanlar = kayit.CalisanlariYukle();
            Mailler.mailler = kayit.MailleriYukle();
            AlışGeçmişi.geçmişAlımlar = kayit.AlımlarıYukle();
            SatışGeçmişi.geçmişSatışlar = kayit.SatislariYukle();
            AraçAlımSatım.teklifPazarlıkGeçmişi = kayit.PazarlikGecmisiYukle();
            AraçAlımSatım.teklifFiyatlar = kayit.TeklifFiyatlarYukle();
            AraçAlımSatım.para = kayit.ParaEvBilgisiniAl().para;
            FirstPersonLook.evNo = kayit.ParaEvBilgisiniAl().evNo;
            KöşeBilgiler.galeriname = kayit.galeriİsmiYukle();
            AlışGeçmişi.geçmişAlımlarFiyat = kayit.TotalGiderBilgisiniAl();
        }
        
        PlayerPrefs.SetInt("first_on_the_computer", 0);
        mainMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        FirstPersonLook.stopped = false;
    }
}