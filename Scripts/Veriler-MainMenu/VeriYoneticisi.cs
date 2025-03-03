using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class VeriYoneticisi : MonoBehaviour
{
    private string dosyaYolu = Application.persistentDataPath + "/veriler.json";



    public void VerileriKaydet(VeriKapsayici veri)
    {
        string json = JsonUtility.ToJson(veri, true); // true: okunabilir format için
        System.IO.File.WriteAllText(dosyaYolu, json);
        Debug.Log("Veriler kaydedildi: " + dosyaYolu);
    }

    //-----------------------------------------------------------------------------
    // Arabalar listesini döndüren metot
    public List<Araba> ArabalariYukle()
    {
        ArabalarWrapper wrapper = LoadWrapperAraba();
        var validArabalar = wrapper.arabalar
            .Where(araba => araba.gelişFiyat != 0)
            .ToList();
        return validArabalar;
    }

    // Teklif arabalar listesini döndüren metot
    public List<Araba> TeklifArabalarYukle()
    {
        ArabalarWrapper wrapper = LoadWrapperAraba();
        var validTeklifArabalar = wrapper.teklifArabalar
            .Where(araba => araba.gelişFiyat != 0)
            .ToList();
        return validTeklifArabalar;
    }

    // Sergiler listesini döndüren metot
    public List<Araba> SergileriYukle()
    {
        ArabalarWrapper wrapper = LoadWrapperAraba();
        var validSergiler = wrapper.sergiler
            .ToList();
        return validSergiler;
    }

    // Çalışanlar listesini döndüren metot
    public List<Çalışan> CalisanlariYukle()
    {
        CalisanlarWrapper wrapper = LoadWrapperCalisan();
        var validÇalışanlar = wrapper.çalışanlar
            .ToList();
        return validÇalışanlar;
    }

    // Mailler listesini döndüren metot
    public List<Mail> MailleriYukle()
    {
        MaillerWrapper wrapper = LoadWrapperMail();
        var validMailler = wrapper.mailler
            .ToList();
        return validMailler;
    }

    // Alımlar listesini döndüren metot
    public List<string> AlımlarıYukle()
    {
        StringWrapper wrapper = LoadWrapperString();
        var validAlımlar = wrapper.alışGeçmişi
            .ToList();
        return validAlımlar;
    }

    // Satışlar listesini döndüren metot
    public List<string> SatislariYukle()
    {
        StringWrapper wrapper = LoadWrapperString();
        var validSatışlar = wrapper.satışGeçmişi
            .ToList();
        return validSatışlar;
    }

    //galeri ismini döndüren metot
    public string galeriİsmiYukle()
    {
        StringWrapper wrapper = LoadWrapperString();
        var validisim = wrapper.galeri_ismi;
        return validisim;
    }

    // Pazarlık geçmişi listesini döndüren metot
    public List<bool> PazarlikGecmisiYukle()
    {
        BoolWrapper wrapper = LoadWrapperBool();
        var validPazarlıklar = wrapper.teklifPazarlıkGeçmişi
            .ToList();
        return validPazarlıklar;
    }

    // Teklif fiyatları listesini döndüren metot
    public List<int> TeklifFiyatlarYukle()
    {
        IntWrapper wrapper = LoadWrapperInt();
        var validTeklifFiyatlar = wrapper.teklifFiyatlar
            .ToList();
        return validTeklifFiyatlar;
    }

    public (int para, int evNo) ParaEvBilgisiniAl()
    {
        IntWrapper wrapper = LoadWrapperInt();
        return (wrapper.para, wrapper.evNo);
    }

    public int TotalGiderBilgisiniAl()
    {
        IntWrapper wrapper = LoadWrapperInt();
        return wrapper.total_gider;
    }

    //-----------------------------------------------------------------------------

    private ArabalarWrapper LoadWrapperAraba()
    {
        string json = File.ReadAllText(dosyaYolu);
        return JsonUtility.FromJson<ArabalarWrapper>(json);
    }

    private CalisanlarWrapper LoadWrapperCalisan()
    {
        string json = File.ReadAllText(dosyaYolu);
        return JsonUtility.FromJson<CalisanlarWrapper>(json);
    }

    private MaillerWrapper LoadWrapperMail()
    {
        string json = File.ReadAllText(dosyaYolu);
        return JsonUtility.FromJson<MaillerWrapper>(json);
    }

    private StringWrapper LoadWrapperString()
    {
        string json = File.ReadAllText(dosyaYolu);
        return JsonUtility.FromJson<StringWrapper>(json);
    }

    private BoolWrapper LoadWrapperBool()
    {
        string json = File.ReadAllText(dosyaYolu);
        return JsonUtility.FromJson<BoolWrapper>(json);
    }

    private IntWrapper LoadWrapperInt()
    {
        string json = File.ReadAllText(dosyaYolu);
        return JsonUtility.FromJson<IntWrapper>(json);
    }
    //-----------------------------------------------------------------------------
    /*
    public VeriKapsayici VerileriYukle()
    {
        if (System.IO.File.Exists(dosyaYolu))
        {
            string json = System.IO.File.ReadAllText(dosyaYolu);
            VeriKapsayici veri = JsonUtility.FromJson<VeriKapsayici>(json);
            return veri;
        }
        else
        {
            Debug.LogWarning("Veri dosyası bulunamadı!");
            return new VeriKapsayici();
        }
    }
    */
}
