﻿using AutoMapper;
using Version = ServerUpload7.BLL.BusinessModels.Version;

namespace ServerUpload7.BLL.Interfaces
{   
    public interface IVersionsService
    {
        public Version CreateVersion(byte [] fileBytes, string name, int category, long size, string path, string strHash, string fileName);
        public string DownloadVersion(int number, string name, int category);

        public string GetHash(byte[] fileBytes);
        public string GetPath(int category, string materialName, string hashString, string fileName);
    }
}
