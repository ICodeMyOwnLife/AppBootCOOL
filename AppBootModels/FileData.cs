using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using CB.Model.Common;


namespace AppBootModels
{
    public class FileData: ObservableObject
    {
        #region Fields
        private byte[] _data;
        private FileInfo _fileInfo;
        private int? _fileInfoId;
        private byte[] _hash;
        private long _size;
        #endregion


        #region  Constructors & Destructor
        public FileData() { }

        public FileData(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException("File not found", filePath);
            SetDataFrom(filePath);
        }
        #endregion


        #region  Properties & Indexers
        public byte[] Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
        }

        public FileInfo FileInfo
        {
            get { return _fileInfo; }
            set { SetProperty(ref _fileInfo, value); }
        }

        public int? FileInfoId
        {
            get { return _fileInfoId; }
            set { SetProperty(ref _fileInfoId, value); }
        }

        public byte[] Hash
        {
            get { return _hash; }
            set { SetProperty(ref _hash, value); }
        }

        public long Size
        {
            get { return _size; }
            set { SetProperty(ref _size, value); }
        }
        #endregion


        #region Methods
        public static byte[] ComputeHash(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }

        public bool CheckData()
        {
            return Data.Length == Size && EqualsHash(Data, Hash);
        }

        public void CopyFrom(FileData fileData, bool includeId)
        {
            Data = fileData.Data;
            FileInfo = fileData.FileInfo;
            Hash = fileData.Hash;
            Size = fileData.Size;

            if (includeId) FileInfoId = fileData.FileInfoId;
        }
        #endregion


        #region Implementation
        private static bool EqualsHash(byte[] data, IReadOnlyList<byte> hash)
        {
            var dataHash = ComputeHash(data);
            return !dataHash.Where((t, i) => t != hash[i]).Any();
        }

        private void SetDataFrom(string filePath)
        {
            Data = File.ReadAllBytes(filePath);
            Hash = ComputeHash(Data);
            Size = Data.LongLength;
        }
        #endregion
    }
}