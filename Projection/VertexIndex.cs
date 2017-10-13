using System;
using System.Collections.Generic;
using System.Text;

namespace FullerProjection.Projection
{
    public class VertexIndex
    {
        public VertexIndex(int i1, int i2, int i3)
        {
            this.I1 = i1 - 1;
            this.I2 = i2 - 1;
            this.I3 = i3 - 1;
        }
        public int I1 { get; }

        public int I2 { get; }

        public int I3 { get; }

        public IList<int> Indices => new List<int> { I1, I2, I3 };

    }
}
