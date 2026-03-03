namespace EnergiApp.Domain
{

    public class CurveOrderPatchRequest
    {
        public List<Curve> Curves { get; set; }
        public string Comment { get; set; }
    }
}