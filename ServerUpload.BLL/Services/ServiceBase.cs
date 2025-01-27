﻿using System.Text.RegularExpressions;
using ServerUpload.DAL.Interfaces;

namespace ServerUpload.BLL.Services
{
    public abstract class ServiceBase
    {
        private static readonly Regex Regex1 = new Regex(@"^.*(?=\.)");
        private static readonly Regex Regex2 = new Regex(@"[^.]*$");
        protected readonly IUnitOfWork _unitOfWork;

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetHash(byte[] fileBytes)
        {
            var result = _unitOfWork.FileManager.GetHash(fileBytes);
            return result;
        }

        public static string GetVersion(string fileName, string matName, int number)
        {
            string dirName;

            if (!fileName.Contains('.'))
                dirName = matName + $"_v{number}";
            else
                dirName = Regex1.Match(matName).Value + $"_v{number}." + Regex2.Match(fileName).Value;
            return dirName;
        }
        public static string GetName(string fileName)
        {
            return !fileName.Contains('.') ? fileName : Regex1.Match(fileName).Value;
        }
    }
}
