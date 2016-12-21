/* Created on 8.10.2016 */
using ParentControl.DAO;
using ParentControl.ObjectModel;

namespace ParentControl.Business
{
    /// <summary>
    /// Business class for base settings.
    /// </summary>
    public class BaseSettingsBusiness
    {
        private BaseSettingsDAO baseSettingsDAO = new BaseSettingsDAO();

        /// <summary>
        /// Gettings base settings.
        /// </summary>
        /// <returns></returns>
        public BaseSettings GetBaseSettings()
        {
            return baseSettingsDAO.GetBaseSettings();
        }

        /// <summary>
        /// Storing base settings.
        /// </summary>
        /// <param name="bs"></param>
        public void StoreBaseSettings(BaseSettings bs)
        {
            baseSettingsDAO.StoreBaseSettings(bs);
        }
    }

}
