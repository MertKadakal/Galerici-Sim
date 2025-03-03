using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VeriKapsayici
{
    public List<Araba> arabalar;
    public List<Çalışan> çalışanlar;
    public List<Mail> mailler;
    public List<string> alışGeçmişi;
    public List<string> satışGeçmişi;
    public List<Araba> teklifArabalar;
    public List<bool> teklifPazarlıkGeçmişi;
    public List<int> teklifFiyatlar;
    public List<Araba> sergiler;
    public int para;
    public int evNo;
    public string galeri_ismi;
    public int total_gider;
}
