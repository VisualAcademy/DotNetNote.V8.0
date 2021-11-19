﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DotNetNoteCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetNote.Controllers;

public class StorageManagerTestController : Controller
{
    private readonly IStorageManager storageManager;

    public StorageManagerTestController(IStorageManager storageManager)
    {
        this.storageManager = storageManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(List<IFormFile> files)
    {
        byte[] byteArray;
        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream);
                    byteArray = stream.ToArray();
                    var folderPath = storageManager.GetFolderPath("Test", "1234", "Files");
                    var newFileName = await storageManager.UploadAsync(byteArray, formFile.FileName, folderPath, false);

                    if (!string.IsNullOrEmpty(newFileName))
                    {
                        // 데이터베이스에 파일 이름 저장 영역 
                    }
                }
            }
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Download(string fileName)
    {
        var folderPath = storageManager.GetFolderPath("Test", "1234", "Files");
        var fileBytes = await storageManager.DownloadAsync(fileName, folderPath);

        if (fileBytes == null)
        {
            return NotFound();
        }

        return File(fileBytes, "application/octet-stream", fileName);
    }
}
