using System;
using System.IO;

namespace ILC.Seve.Util
{
    /// <summary>
    /// A Stream implementation that simply produces a bunch of random bytes.
    /// It can only be read from, not written to.
    /// </summary>
    public class RandomDataStream : Stream
    {
        private readonly Random Random = new Random();

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            byte[] randomData = new byte[count];
            Random.NextBytes(randomData);
            Array.Copy(randomData, 0, buffer, offset, count);

            return count;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
