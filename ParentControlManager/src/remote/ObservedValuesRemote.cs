/* Created on 1.11.2016 */
using ParentControl.ObjectModel;
using ParentControlManager.Remote.Base;

namespace ParentControlManager.Remote
{
    /// <summary>
    /// Remote class for observed values.
    /// </summary>
    public class ObservedValuesRemote : RemoteCommFactory
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        public ObservedValuesRemote(string hostname, int port) : base(hostname, port)
        {
        }

        /// <summary>
        /// Reset observed values.
        /// </summary>
        public void Reset()
        {
            Call("ObservedValuesBusiness.Reset", null, null);
        }

        /// <summary>
        /// Getting actual observed values.
        /// </summary>
        /// <returns></returns>
        public ObservedValues GetObservedValues()
        {
            return (ObservedValues)Call("ObservedValuesBusiness.GetObservedValues", typeof(ObservedValues), null);
        }
    }

}
