namespace kolos1.Model;

public class DeliveryDTO
{
    public DateTime date { get; set; }
    public CustomerDTO customer { get; set; }
    public DriverDTO driver { get; set; }
    public ProductDTO product { get; set; }
}