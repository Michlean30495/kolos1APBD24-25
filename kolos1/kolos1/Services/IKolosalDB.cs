using kolos1.Model;

namespace kolos1.Services;

public interface IKolosalDB
{
    public Task<DeliveryDTO> GetDeliveriesById(int i);
}