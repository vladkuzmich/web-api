using System;
using Microsoft.Extensions.Logging;
using ImageMagick;
using WebAPI.Business.Contracts;

namespace WebAPI.Business.Providers
{
    public class ImageProvider : IImageProvider
    {
        private readonly ILogger<ImageProvider> _logger;

        public ImageProvider(ILogger<ImageProvider> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void UploadImage(byte[] imageData, string urlTo, int width = 0, int height = 0)
        {
            try
            {
                using var image = new MagickImage(imageData);

                if (width > 0 && height > 0)
                {
                    image.Resize(width, height);
                }

                image.Write(urlTo);
            }
            catch (MagickException e)
            {
                _logger.LogError(e, $"Error while uploading photo. Error message: '{e.Message}'");
                throw;
            }
        }
    }
}
