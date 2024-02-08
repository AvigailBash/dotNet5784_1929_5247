using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;

internal class EngineerCollection : IEnumerable
{
    static readonly IEnumerable<BO.Engineerlevel> s_enums =
(Enum.GetValues(typeof(BO.Engineerlevel)) as IEnumerable<BO.Engineerlevel>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
internal class Enums
{

   
}
