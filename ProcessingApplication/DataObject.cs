using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXTextControl.Parallel.DataContainer
{
    // the data object that is passed from hosting application
    // to the processing application (process)
    public class PassingObject
    {
        public byte[] Document { get; set; }
        public object Data { get; set; }
    }

    // the object that is returned from the processing process
    // to the hosting application
    public class ReturningObject
    {
        public byte[] Document { get; set; }
        public string Error { get; set; }
    }
}
