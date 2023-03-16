using MyToDo.shared.Dtos;
using MyToDo.shared;
using Microsoft.AspNetCore.Mvc;

namespace MyToDo.api.Service
{
    public interface IFileService
    {
        Task<ApiResponse> UpLoadAsync(FileDto model);
        (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory);

        Task<IActionResult> DownLoadFile(string fileName);
    }
}
