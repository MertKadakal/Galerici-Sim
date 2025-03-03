[System.Serializable]
public class Araba {
    public int gelişFiyat;
    public int modelDerece;
    public int sene;
    public int karBeklentisi;
    public int karOrani;

    public Araba(int gelişFiyat, int modelDerece, int sene, int karBeklentisi, int karOrani)
    {
        this.gelişFiyat = gelişFiyat;
        this.modelDerece = modelDerece;
        this.sene = sene;
        this.karBeklentisi = karBeklentisi;
        this.karOrani = karOrani;
    }
}