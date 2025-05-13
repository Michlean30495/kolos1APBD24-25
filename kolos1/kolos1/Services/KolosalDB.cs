namespace kolos1.Services;

public class KolosalDB : IKolosalDB
{
    private readonly IConfiguration _configuration;
    
    public KolosalDB(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}