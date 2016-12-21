/* Created on 9.10.2016 */
using ParentControl.DAO;
using ParentControl.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Business
{
    /// <summary>
    /// Observed values business class.
    /// </summary>
    public class ObservedValuesBusiness
    {
        private ObservedValuesDAO observedValuesDAO = new ObservedValuesDAO();

        /// <summary>
        /// Reset observed values.
        /// </summary>
        public void Reset()
        {
            ObservedValues ov = new ObservedValues();
            ov.ActualDate = null;
            ov.Duration = null;
            observedValuesDAO.StoreObservedValues(ov);
        }

        /// <summary>
        /// Getting actual observed values.
        /// </summary>
        /// <returns></returns>
        public ObservedValues GetObservedValues()
        {
            return observedValuesDAO.GetObservedValues();
        }
    }

}
