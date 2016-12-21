/* Created on 21.9.2016 */
using ParentControl.ObjectModel;
using ParentControlClient.Remote.Base;
using System.Threading.Tasks;

namespace ParentControlClient.Remote
{
    /// <summary>
    /// Client class for remote access to server.
    /// </summary>
    public class RulesRemote
    {

        /// <summary>
        /// Remote store rule object.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public Rule StoreRule(Rule rule)
        {
            return (Rule)RemoteCommFactory.Instance.Call("RulesBusiness.StoreRule", typeof(Rule), rule);
        }

        /// <summary>
        /// Getting rule via ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rule GetRule(int id)
        {
            return (Rule)RemoteCommFactory.Instance.Call("RulesBusiness.GetRule", typeof(Rule), null, id);
        }

        /// <summary>
        /// Delete rule record via ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DeleteRule(int id)
        {
            RemoteCommFactory.Instance.Call("RulesBusiness.DeleteRule", null, null, id);
        }

        /// <summary>
        /// Getting list of the rules.
        /// </summary>
        /// <returns></returns>
        public Rule[] ListRules()
        {
            return (Rule[])RemoteCommFactory.Instance.Call("RulesBusiness.ListRules", typeof(Rule[]), null);
        }

    }

}
