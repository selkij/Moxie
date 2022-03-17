using System;

namespace ProjectOrizonOS.Interpreter
{
    class FileBadExtension : Exception
    {

        public FileBadExtension() {  }

        public FileBadExtension(string extension)
            :base($"File contains bad extension {extension}")
        {

        }
    }
}
