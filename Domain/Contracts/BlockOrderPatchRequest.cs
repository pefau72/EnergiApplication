
namespace EnergiApp.Domain
{
    public class BlockOrderPatchRequest
    {
        public List<Block> Blocks { get; set; }
        public string Comment { get; set; }
    }
}