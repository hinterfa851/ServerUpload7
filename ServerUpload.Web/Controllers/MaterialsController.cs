﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServerUpload.Web.Dto;
using ServerUpload.BLL.Enums;
using ServerUpload.BLL.Interfaces;

namespace ServerUpload.Web.Controllers
{

    [Route("materials")]
    public class MaterialsController : Controller
    {

        private readonly IMaterialsService _materialsService;
        private readonly IConfiguration _configuration;
        public IMapper _mapper;
        public IWebHostEnvironment _appEnvironment;
        
        public MaterialsController(IMaterialsService materialService, IWebHostEnvironment appEnvironment, IMapper mapper, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._materialsService = materialService;
            this._appEnvironment = appEnvironment;
            this._configuration = configuration;
        }

        
        [HttpPost]
        [Route("")]
        public  MaterialDto Material(IFormFile uploadedFile, Categories category)
        {
            var memStream = new MemoryStream();
            uploadedFile.CopyTo(memStream);
            var fileBytes = memStream.ToArray();
            string strHash = _materialsService.GetHash(fileBytes);
            string path = _materialsService.GetPath(category, uploadedFile.FileName, 1, strHash);
            var result = _mapper.Map<MaterialDto>(_materialsService.CreateMaterial(fileBytes, category, uploadedFile.FileName, uploadedFile.Length, path, strHash));
            memStream.Dispose();
            return result;
        }
            
        
        [HttpGet]
        [Route("actual-version")]
        public  FileResult ActualVersion(string name, Categories category)
        { 
            var result =  _materialsService.DownloadActualVersion(name, category);
            return PhysicalFile(_configuration["FilePath"] + result, System.Net.Mime.MediaTypeNames.Application.Octet, $"{name.Split("/").Last()}");
        }

        [HttpGet]
        [Route("info")]
        public ActionResult<MaterialDto> GetMaterialInfo(string name, Categories category)
        {
            var result = _mapper.Map<MaterialDto>(_materialsService.GetMaterialInfo(name, category));
            return Ok(result);
        }

        [HttpPut]
        [Route("new-category")]
        public ActionResult<MaterialDto> NewCategory(string name, Categories oldCategory, Categories newCategory)
        {
            var result = _mapper.Map<MaterialDto>(_materialsService.ChangeCategory(name, oldCategory, newCategory));
            return Ok(result);
        }

        [HttpGet]
        [Route("info/category")]
        public ActionResult<List<MaterialDto>> Category(Categories category)
        {
            var result = _mapper.Map<IEnumerable<MaterialDto>>(_materialsService.FilterMat(category));
            return Ok(result);
        }
    }
}
