using Common.VmEntities;

namespace Common.Responces
{
    public class VmResponse
    {
        public VmDto Content { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
