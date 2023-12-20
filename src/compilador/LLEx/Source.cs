using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LLEx
{
    public class Source
    {
        private readonly String path;
        private readonly char[] buffer;
        private readonly FileStream fs;
        private readonly StreamReader sr;

        private int i { get; set; }

        public Source(String path, int buffer_size)
        {
            this.path = path;
            this.buffer = new char[buffer_size];
            this.fs = new FileStream(this.path, FileMode.Open, FileAccess.Read);
            this.sr = new StreamReader(this.fs);
            this.Bufferize();
        }

        private void ClearBuffer()
        {
            for(int i = 0; i < this.buffer.Length; i++)
            {
                this.buffer[i] = '\0';
            }
        }

        private void Bufferize()
        {
            ClearBuffer();
            this.i = 0;
            this.sr.ReadBlock(buffer);
        }

        public char Peek()
        {
            bool isEndOfBuffer = !(this.i < this.buffer.Length);

            if(isEndOfBuffer)
            {
                this.Bufferize();
            }

            char c = this.buffer[this.i];
            this.i++;
            return c;
        }

        public void GoBack()
        {
            this.i--;
        }

    }
}
