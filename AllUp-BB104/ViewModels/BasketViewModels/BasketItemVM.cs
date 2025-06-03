namespace AllUp_BB104.ViewModels;

public class BasketItemVM
{
    public List<BasketItemGetVM> BasketItems { get; set; } = [];
    public decimal TotalPrice { get; set; }
    public int Count { get; set; }
}
