/* Created on 9.10.2016 */
using ParentControlClient.Remote.Base;
using ParentControl.ObjectModel;

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

        /// <summary>
        /// Getting actual observed values.
        /// </summary>
        /// <returns></returns>
        public ObservedValues GetObservedValues()
        {
            return (ObservedValues)RemoteCommFactory.Instance.Call("ObservedValuesBusiness.GetObservedValues", typeof(ObservedValues), null);
        }
    }

}
