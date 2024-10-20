namespace TeachBook.Web.Repositories
{
    public interface IUploadImg
    {
        Task<string> UploadImgAsync(IFormFile file);
    }
}
