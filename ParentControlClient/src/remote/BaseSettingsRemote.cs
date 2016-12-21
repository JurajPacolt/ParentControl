/* Created on 8.10.2016 */
using ParentControl.ObjectModel;
using ParentControlClient.Remote.Base;

namespace ParentControlClient.Remote
{
    /// <summary>
    /// Remote access to base settings.
    /// </summary>
    public class BaseSettingsRemote
    {
        /// <summary>
        /// Remote calling for getting base settings.
        /// </summary>
        /// <returns></returns>
        public BaseSettings GetBaseSettings()
        {
            return (BaseSettings)RemoteCommFactory.Instance.Call("BaseSettingsBusiness.GetBaseSettings", typeof(BaseSettings), null);
        }

        /// <summary>
        /// Remote calling for storing base settings.
        /// </summary>
        /// <param name="bs"></param>
        public void StoreBaseSettings(BaseSettings bs)
        {
            RemoteCommFactory.Instance.Call("BaseSettingsBusiness.StoreBaseSettings", null, bs);
        }
    }

}
