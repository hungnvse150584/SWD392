using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace Booking.Common
{
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        private static string ApiKey = "AIzaSyBT721fJRNgzah4Vs3IoczpHHjiTXmi9jI";
        private static string Bucket = "bpks-ee4a1.appspot.com";
        private static string AuthEmail = "bpks@gmail.com";
        private static string AuthPassword = "Abc@123";

        public FileStorageService()
        {
            //_userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {

            // Validate stream state before use
            if (!mediaBinaryStream.CanRead)
            {
                throw new InvalidOperationException("Media stream is not readable.");
            }

            var task = new FirebaseStorage(Bucket,
                new FirebaseStorageOptions
                { 
                    ThrowOnCancel = true 
                })
                .Child("images")
                .Child("ProductImage")
                .Child(fileName)
                .PutAsync(mediaBinaryStream);
            
            var filepath = await task;
           

        }

        //public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        //{
        //    var filePath = Path.Combine(_userContentFolder, fileName);
        //    using var output = new FileStream(filePath, FileMode.Create);
        //    await mediaBinaryStream.CopyToAsync(output);
        //}

        public async void Upload(FileStream file, string filename)
        {
           
        }


        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
