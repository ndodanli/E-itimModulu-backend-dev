public static class DatabaseConfig
{
    public enum DatabaseType
    {
        Small,
        Medium,
        Large,
        Special
    }
    public static DatabaseType dbType = DatabaseType.Special;


    //Database tipini Special olarak belirttiğinizde aşağıdaki ayarları değiştirebilirsiniz
    public static int schoolLimit = 10; //Okul limiti(Okul dışındaki tablo değerlerinin katsayısı olarak düşünülebilir, örn. 1 okul için 5-10 öğretmen 2 okul için 10-20 öğretmen)
    public const int COEFFICIENT = 1; //bu sabit rastgele üretilen tüm entity objelerinin sayısını katlayacaktır
}