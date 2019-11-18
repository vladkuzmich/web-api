namespace WebAPI.Business.Contracts
{
    public interface IImageProvider
    {
        void UploadImage(byte[] imageData, string urlTo, int width = 0, int height = 0);
    }
}