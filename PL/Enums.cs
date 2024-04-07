using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;
/// <summary>
/// Enums class
/// </summary>

//enum for EngineerCollection
internal class EngineerCollection : IEnumerable
{
    static readonly IEnumerable<BO.Engineerlevel> s_enums =
(Enum.GetValues(typeof(BO.Engineerlevel)) as IEnumerable<BO.Engineerlevel>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}


//enum for TaskInList
internal class TaskInListCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status> s_enums =
(Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
internal class Enums
{

   
}
