using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mozo.Comun.Implement;
using Mozo.Comun.Implement.Api;
using Mozo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mozo.ApiSeguridad.Controllers.Admin;

[Route("[controller]")]
public class UploadApiController : BaseApiController
{
    private readonly IConfiguration _configuration;

    public UploadApiController(
        IConfiguration configuration
    )
    {
        _configuration = configuration;
    }


    [AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public IActionResult Reset()
    {
        FileMergeModel.Instance.RemoveAll();
        return new JsonResult(new
        {
            success = true
        });
    }

    //public async Task<IActionResult> CreateAsync(Product product)

    [AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Carga()
    {
        var FileDataContent = HttpContext.Request.Form.Files[0];
        //if (FileDataContent != null && FileDataContent.ContentLength > 0)
        if (FileDataContent.Length > 0)
        {
            var TxPathFolderTemporary =
                Path.Combine(_configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                    _configuration.GetSection("AppSettings").GetSection("FolderTemporary").Value);
            if (!Directory.Exists(TxPathFolderTemporary)) Directory.CreateDirectory(TxPathFolderTemporary);

            var UploadFilePathAndConfig = new UploadFilePathAndConfigModel(Path.GetFileName(FileDataContent.FileName))
            { PathFolderTemporary = TxPathFolderTemporary };


            var TxPathFolderTemporaryAndFileName =
                Path.Combine(TxPathFolderTemporary, UploadFilePathAndConfig.FileNameWithPartAndPrefij);
            try
            {
                if (System.IO.File.Exists(TxPathFolderTemporaryAndFileName))
                    System.IO.File.Delete(TxPathFolderTemporaryAndFileName);

                var stream = FileDataContent.OpenReadStream();
                using (var fileStream = System.IO.File.Create(TxPathFolderTemporaryAndFileName))
                {
                    stream.CopyTo(fileStream);
                }

                stream.Close();
                stream.Dispose();
                stream = null;

                var result = MergeFile(UploadFilePathAndConfig);
                return new JsonResult(new
                {
                    success = result.Item1,
                    noArchivo = result.Item1 ? result.Item2.NoArchivo : null,
                    noExtension = result.Item1 ? result.Item2.NoExtension : null
                });

                //if (res)
                //{
                //    //ArchivoModel Archivo = new ArchivoModel();
                //    //Archivo.NoRutaCompleta = um.BaseFileName; um.BaseFileName = null;
                //    //Archivo.SetArchivoNombreExtension();

                //    return new JsonResult(new
                //    {
                //        success = true,
                //        //ArchivoNombre = Archivo.NoArchivo,
                //        //ArchivoExtension = Archivo.NoExtension
                //    });
                //}
            }
            catch (IOException)
            {
                return new JsonResult(new
                {
                    success = false
                });
            }
        }

        return new JsonResult(new
        {
            success = false
        });
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetByAttributeArchivo([FromQuery] ArchivoModel c)
    {
        return Ok(c.SetArchivoNombreExtension());
    }

    private Tuple<bool, ArchivoModel> MergeFile(UploadFilePathAndConfigModel uploadFilePathAndConfig)
    {
        //if (fileName == null) throw new ArgumentNullException("no tiene nombre de archivo.");
        // parse out the different tokens from the filename according to the convention
        string trailingTokens = uploadFilePathAndConfig.TrailingTokens;
        //int CurrentFileIndex;
        //int FileCount = 0;
        int.TryParse(trailingTokens.Substring(0, trailingTokens.IndexOf(".")), out var CurrentFileIndex);
        int.TryParse(trailingTokens.Substring(trailingTokens.IndexOf(".") + 1), out var CurrentFileCount);
        // get a list of all file parts in the temp folder
        string Searchpattern = uploadFilePathAndConfig.FileNameNotPart + uploadFilePathAndConfig.PartToken + "*";
        string[] Files = Directory.GetFiles(uploadFilePathAndConfig.PathFolderTemporary, Searchpattern);

        bool FileExist = false;
        if (Files.Length == CurrentFileCount)
            FileExist = false;
        if (System.IO.File.Exists(Path.Combine(uploadFilePathAndConfig.PathFolderTemporary,
                uploadFilePathAndConfig.FileNameNotPartAndPrefij))) FileExist = true;

        // add each file located to a list so we can get them into 
        // the correct order for rebuilding the file
        List<UploadChunkFileMergeModel> ChunkFileMergeList = new();
        foreach (var FileName in Files)
        {
            var trailingTokens2 = FileName.Substring(FileName.IndexOf(uploadFilePathAndConfig.PartToken) +
                                                     uploadFilePathAndConfig.PartToken.Length);
            int.TryParse(trailingTokens.Substring(0, trailingTokens2.IndexOf(".")), out CurrentFileIndex);
            ChunkFileMergeList.Add(new UploadChunkFileMergeModel
            {
                Name = FileName,
                Index = CurrentFileIndex
            });
        }
        // sort by the file-part number to ensure we merge back in the correct order
        ChunkFileMergeList = ChunkFileMergeList.OrderBy(x => x.Index).ToList();
        //Se cambia de nombre porque el Garbaje Collection no elimina inmediatamente.
        string FileNameNotPartAndPrefij = FileExist
            ? DateTime.Now.ToString("yyyyMMddHHmmssfff") + uploadFilePathAndConfig.FileNameNotPartAndPrefij
            : uploadFilePathAndConfig.FileNameNotPartAndPrefij;
        FileStream fsAppend =
            new(Path.Combine(uploadFilePathAndConfig.PathFolderTemporary, FileNameNotPartAndPrefij),
                FileMode.Create, FileAccess.ReadWrite);
        for (var i = 0; i < ChunkFileMergeList.Count; i++)
        {
            var chunkFileMerge = ChunkFileMergeList[i];
            FileStream fsChunk = new(chunkFileMerge.Name, FileMode.Open, FileAccess.Read, FileShare.None);
            fsChunk.CopyTo(fsAppend);
            fsChunk.Close();
            fsChunk.Dispose();
            ;
        }
        fsAppend.Close();
        fsAppend.Dispose();
        //await System.IO.File.WriteAllBytesAsync(Path.Combine(uploadFilePathAndConfig.PathFolderTemporary, FileNameNotPartAndPrefij), new byte[7]);
        foreach (var chunkFileMerge in ChunkFileMergeList)
            //File.SetAttributes(chunkFileMerge.Name,  FileAttributes.Normal);
            //File.SetAttributes(chunkFileMerge.Name, FileAttributes.ReadOnly);
            //File.se .IsReadOnly = false;
            System.IO.File.Delete(chunkFileMerge.Name);
        ChunkFileMergeList.Clear(); ChunkFileMergeList = null;


        ArchivoModel Archivo = new() { NoRutaCompleta = FileNameNotPartAndPrefij };

        return new Tuple<bool, ArchivoModel>(true, Archivo.SetArchivoNombreExtension(null, false));

        //return new Tuple<bool, ArchivoModel>(false, null);
    }
}