namespace AllUp_BB104.ViewModels.CommentViewModels;

public class CommentCreateVM
{
    public string Text { get; set; } = null!;
    public int Rating { get; set; }
    public int ProductId { get; set; }
}