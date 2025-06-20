namespace AllUp_BB104.ViewModels.CommentViewModels;

public class CommentGetVM
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public string AppUserName { get; set; } = null!;
    public int Rating { get; set; }
}