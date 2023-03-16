using MyToDo.shared.Dtos;
using MyToDo.shared;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using System.IO.Compression;

namespace MyToDo.api.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class FileService : IFileService
    {
        #region Property

        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        #region Constructor

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        public async Task<ApiResponse> UpLoadAsync(FileDto fileModel)
        {
            IActionResult f;
            try
            {
                var path = Directory.GetCurrentDirectory() + "/files/upload/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fullName = $"{path}{fileModel.File.FileName}";
                using (FileStream fs = new FileStream(fullName, FileMode.Create))
                {
                    await fileModel.File.CopyToAsync(fs); // 存储文件
                    fs.Flush();
                    fs.Close();
                }

                if (File.Exists(fullName))
                {
                    return new ApiResponse(true, "文件上传成功");
                }
                else
                {
                    return new ApiResponse("文件上传失败");
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse("文件上传失败");
            }
            finally
            {

            }

        }


        public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory)
        {
            var zipName = $"archive-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}.zip";

            var files = Directory.GetFiles(Path.Combine(_webHostEnvironment.ContentRootPath, subDirectory)).ToList();

            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                files.ForEach(file =>
                {
                    var theFile = archive.CreateEntry(Path.GetFileName(file));
                    using var binaryWriter = new BinaryWriter(theFile.Open());
                    binaryWriter.Write(File.ReadAllBytes(file));
                });
            }

            return ("application/zip", memoryStream.ToArray(), zipName);
        }


        public string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            return fileSize switch
            {
                _ when fileSize < kilobyte => "Less then 1KB",
                _ when fileSize < megabyte =>
                    $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB",
                _ when fileSize < gigabyte =>
                    $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB",
                _ when fileSize >= gigabyte =>
                    $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB",
                _ => "n/a"
            };
        }

        public Task<IActionResult> DownLoadFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
