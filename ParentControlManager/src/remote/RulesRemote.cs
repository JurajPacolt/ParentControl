/* Created on 1.11.2016 */
using ParentControl.ObjectModel;
using ParentControlManager.Remote.Base;

namespace ParentControlManager.Remote
{
    /// <summary>
    /// Client class for remote access to server.
    /// </summary>
    public class RulesRemote : RemoteCommFactory
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        public RulesRemote(string hostname, int port) : base(hostname, port)
        {
        }

        /// <summary>
        /// Remote store rule object.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public Rule StoreRule(Rule rule)
        {
            return (Rule)Call("RulesBusiness.StoreRule", typeof(Rule), rule);
        }

        /// <summary>
        /// Getting rule via ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Rule GetRule(int id)
        {
            return (Rule)Call("RulesBusiness.GetRule", typeof(Rule), null, id);
        }

        /// <summary>
        /// Delete rule record via ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DeleteRule(int id)
        {
            Call("RulesBusiness.DeleteRule", null, null, id);
        }

        /// <summary>
        /// Getting list of the rules.
        /// </summary>
        /// <returns></returns>
        public Rule[] ListRules()
        {
            return (Rule[])Call("RulesBusiness.ListRules", typeof(Rule[]), null);
        }

    }

}
