using System;
using System.Collections.Generic;
using System.IO;

namespace BookingArtistMvcCore.Helpers
{
    public class FileHelper
    {
        public static string GetFileType(string filePath)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };
            string[] videoExtensions = { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv" };

            string fileExtension = Path.GetExtension(filePath).ToLower();

            if (Array.IndexOf(imageExtensions, fileExtension) >= 0)
                return "Image";
            else if (Array.IndexOf(videoExtensions, fileExtension) >= 0)
                return "Video";
            else
                return "Unknown";
        }

        public static bool IsImage(string filePath)
        {
            return GetFileType(filePath) == "Image";
        }
        public static bool IsVideo(string filePath)
        {
            return GetFileType(filePath) == "Video";
        }

        public static string GetMimeType(string filePath)
        {
            var mimeTypes = new Dictionary<string, string>
        {
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".bmp", "image/bmp" },
            { ".tiff", "image/tiff" },
            // Video MIME types
            { ".mp4", "video/mp4" },
            { ".avi", "video/x-msvideo" },
            { ".mov", "video/quicktime" },
            { ".wmv", "video/x-ms-wmv" },
            { ".flv", "video/x-flv" },
            { ".mkv", "video/x-matroska" }
        };

            string fileExtension = Path.GetExtension(filePath).ToLower();
            if (mimeTypes.ContainsKey(fileExtension))
            {
                return mimeTypes[fileExtension];
            }
            else
            {
                return "application/octet-stream"; // Generic MIME type for unknown/other file types
            }
        }
    }
}
