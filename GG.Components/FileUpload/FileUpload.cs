﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.Components.FileUpload
{
    public class FileUpload
    {

        public FileUpload(String filename, byte[] file, String mimeType)
        {

            this.file = file;
            this.filename = filename;
            this.mimeType = mimeType;
        }

        public FileUpload()
        {
            // Default Constructor
        }

        //Id
        private String filename;

        //Lob
        private byte[] file;

        private String mimeType;


        public String getFilename()
        {
            return filename;
        }

        public void setFilename(String filename)
        {
            this.filename = filename;
        }

        public byte[] getFile()
        {
            return file;
        }

        public void setFile(byte[] file)
        {
            this.file = file;
        }

        public String getMimeType()
        {
            return mimeType;
        }

        public void setMimeType(String mimeType)
        {
            this.mimeType = mimeType;
        }
    }
}
