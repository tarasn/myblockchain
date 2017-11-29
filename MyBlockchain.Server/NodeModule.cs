using System.Web.Script.Serialization;
using MyBlockchain.Business;
using Nancy;

namespace MyBlockchain.Server
{
    public class NodeModule : NancyModule
    {
        public NodeModule(SyncFacade facade)
        {
            Get["/blockchain.json"] = _ =>
            {
                var chain = facade.SyncLocal();
                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(chain.Blocks);
            };
        }
    }
}