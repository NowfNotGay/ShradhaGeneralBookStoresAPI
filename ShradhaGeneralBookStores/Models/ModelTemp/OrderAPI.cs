namespace ShradhaGeneralBookStores.Models.ModelTemp;

public class OrderAPI
{
    public int AccountId { get; set; }

    public decimal TotalPrice { get; set; }

    public int StatusId { get; set; }

    public int AddressId { get; set; }

    public int VoucherId { get; set; }

    public int PaymentMethodId { get; set; }

    public List<OrderDetail> listOrderDetail { get; set; } = new List<OrderDetail>();

}


