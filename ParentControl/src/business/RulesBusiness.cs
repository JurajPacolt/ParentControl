/* Created on 18.9.2016 */
using ParentControl.DAO;

/// <summary>
/// Namespace for remote calling.
/// </summary>
namespace ParentControl.Business
{
    /// <summary>
    /// Business class for rules.
    /// </summary>
    public class RulesBusiness
    {
        private RulesDAO rulesDao = new RulesDAO();

        /// <summary>
        /// Storing rule, new or exists. If "id" field is NULL then is automatically new record.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public ObjectModel.Rule StoreRule(ObjectModel.Rule rule)
        {
            return rulesDao.StoreRule(rule);
        }

        /// <summary>
        /// Getting rule object via ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObjectModel.Rule GetRule(int id)
        {
            return rulesDao.GetRule(id);
        }

        /// <summary>
        /// Delete rule via ID. If not exists then generated exception.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRule(int id)
        {
            rulesDao.DeleteRule(id);
        }

        /// <summary>
        /// Getting all exists rules.
        /// </summary>
        /// <returns></returns>
        public ObjectModel.Rule[] ListRules()
        {
            return rulesDao.ListRules();
        }
    }

}
