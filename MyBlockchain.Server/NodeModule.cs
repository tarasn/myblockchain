using System.Web.Script.Serialization;
using MyBlockchain.Business;
using Nancy;

namespace MyBlockchain.Server
{
    public class NodeModule : NancyModule
    {
        public NodeModule(BlockFacade facade)
        {
            Get["/blockchain.json"] = _ =>
            {
                var nodeBlocks = facade.Sync();
                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(nodeBlocks);
            };
        }
    }
}