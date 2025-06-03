namespace AllUp_BB104.ViewModels;

public class BasketItemGetVM
{
    public int Id { get; set; }
    public int Count { get; set; }
    public int ProductId { get; set; }
    public ProductGetVM Product { get; set; }
}
