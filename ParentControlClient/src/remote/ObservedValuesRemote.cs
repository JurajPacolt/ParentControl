/* Created on 9.10.2016 */
using ParentControlClient.Remote.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControlClient.Remote
{
    /// <summary>
    /// Remote class for observed values.
    /// </summary>
    public class ObservedValuesRemote
    {
        /// <summary>
        /// Reset observed values.
        /// </summary>
        public void Reset()
        {
            RemoteCommFactory.Instance.Call("ObservedValuesBusiness.Reset", null, null);
        }
    }

}
