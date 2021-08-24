namespace EgitimModuluApp
{
    public class AppSettings
    {
        public string Secret { get; set; }

        //refresh token yaşam süresi(!kullanıcı pasif kaldığı sürece)
        public int RefreshTokenTTL { get; set; }
    }
}