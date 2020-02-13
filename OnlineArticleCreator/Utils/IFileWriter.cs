using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGT.File
{
    public interface IFileWriter
    {
        void WriteFile(FileWriteArgs writeArgs);
    }

    public interface IFileWriter<TReturnType>
    {
        TReturnType WriteFile(FileWriteArgs writeArgs);
    }
}
