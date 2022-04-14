using System;

namespace Moxie.Interpreter
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
