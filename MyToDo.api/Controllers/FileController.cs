using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyToDo.api.Service;
using MyToDo.shared;
using MyToDo.shared.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MyToDo.api.Controllers
{
    /// <summary>
    /// 上传文件接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FileController:ControllerBase
    {
        private readonly IFileService fileService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileService"></param>
        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> UpLoadFile([FromForm] FileDto model) => await this.fileService.UpLoadAsync(model);



        /// <summary>
        /// 下载指定目录下的全部文件
        /// </summary>
        /// <param name="subDirectory"></param>
        /// <returns>以zip压缩包形式返回</returns>
        [HttpGet]
        public IActionResult DownloadFiles([Required] string subDirectory)
        {
            try
            {
                var (fileType, archiveData, archiveName) = fileService.DownloadFiles(subDirectory);

                return File(archiveData, fileType, archiveName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 下载指定名称的文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DownloadFile([Required] string fileName)
        {
            var fullName = Directory.GetCurrentDirectory() + "/files/upload/" + fileName;
            if (!System.IO.File.Exists(fullName))
            {
                return BadRequest("文件不存在");
            }

            byte[] bytes = System.IO.File.ReadAllBytes(fullName);

            MemoryStream stream = new MemoryStream(bytes);
            return File(stream, "application/oct-stream", fileName);

        }

    }
}
