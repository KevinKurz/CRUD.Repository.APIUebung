using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.xUnitTests
{
    public interface IMockConfigurator<T>
    {
        public void Mockservice(T mockedObject);
    }
}
