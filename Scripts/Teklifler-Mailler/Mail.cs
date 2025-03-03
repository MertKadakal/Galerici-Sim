
using System.Collections.Generic;
using System;
[System.Serializable]

public class Mail {
    public string tür;
    public Araba araba;
    public string tarih;
    public string mesaj;
    public string isim;
    public List<int> degerler;
    public DateTime kiraBitiş;

    public Mail(string tür, Araba araba, string tarih, string mesaj, List<int> degerler) {
        this.tür = tür;
        this.araba = araba;
        this.tarih = tarih;
        this.mesaj = mesaj;
        this.degerler = degerler;
    }

    public Mail(string tür,string tarih, string mesaj) {
        this.tür = tür;
        this.tarih = tarih;
        this.mesaj = mesaj;
    }

    public Mail(string tür, Araba araba, string tarih, string mesaj, List<int> degerler, string isim) {
        this.tür = tür;
        this.araba = araba;
        this.tarih = tarih;
        this.mesaj = mesaj;
        this.degerler = degerler;
        this.isim = isim;
    }

    public Mail(string tür, Araba araba, string tarih, string mesaj, List<int> degerler, DateTime kiraBitiş) {
        this.tür = tür;
        this.araba = araba;
        this.tarih = tarih;
        this.mesaj = mesaj;
        this.degerler = degerler;
        this.kiraBitiş = kiraBitiş;
    }
}