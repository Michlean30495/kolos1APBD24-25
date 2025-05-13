using kolos1.Model;
using Microsoft.Data.SqlClient;

namespace kolos1.Services;

public class KolosalDB : IKolosalDB
{
    private readonly IConfiguration _configuration;

    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=apbd;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
        ;

    public KolosalDB(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<DeliveryDTO> GetDeliveriesById(int i)
    {
        var delivery = new DeliveryDTO();
        
        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        
        var transaction = conn.BeginTransaction();

        try
        {
            var checkIfIdExist = @"SELECT date, customer_id, driver_id FROM Delivery WHERE delivery_id = @delivery_id";
            var command = new SqlCommand(checkIfIdExist, conn, transaction);
            command.Parameters.AddWithValue("@delivery_id", i);
            var result = await command.ExecuteScalarAsync();
            if (result == null)
            {
                throw new Exception("Delivery not found (29)");
            }
            
            delivery.date = Convert.ToDateTime(result);
            
            var customer_id = Convert.ToInt32(result);
            var driver_id = Convert.ToInt32(result);
            
            var getCustomerQuery = @"SELECT first_name, last_name, date_of_birth FROM Customers WHERE Customer_id = @customer_id";
            var commandGetCustomer = new SqlCommand(getCustomerQuery, conn, transaction);
            commandGetCustomer.Parameters.AddWithValue("@customer_id", customer_id);
            var resultCustomer = await commandGetCustomer.ExecuteScalarAsync();
            if (resultCustomer == null)
                throw new Exception("Customer not found");
            
            var thisCustomer = new CustomerDTO();
            thisCustomer.fristName = Convert.ToString(resultCustomer);
            thisCustomer.lastName = Convert.ToString(resultCustomer);
            thisCustomer.dateOfBirth = Convert.ToDateTime(resultCustomer);
            
            delivery.customer = thisCustomer;
            
            var getDriverQuery = @"SElECT first_name, last_name, licence_number FROM Drivers WHERE Driver_id = @driver_id";
            var commandGetDriver = new SqlCommand(getDriverQuery, conn, transaction);
            commandGetDriver.Parameters.AddWithValue("@driver_id", driver_id);
            var resultDriver = await commandGetDriver.ExecuteScalarAsync();
            if (resultDriver == null)
                throw new Exception("Driver not found");
            
            var thisDriver = new DriverDTO();
            thisDriver.fristName = Convert.ToString(resultDriver);
            thisDriver.lastName = Convert.ToString(resultDriver);
            thisDriver.licenceNumber = Convert.ToString(resultDriver);
            
            delivery.driver = thisDriver;

            transaction.CommitAsync();    
            
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            
        }
        
        return delivery;
    }
}