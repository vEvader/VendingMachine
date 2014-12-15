using Common.VmEntities;
using RepositoryInterface;

namespace Repository
{
    public class VmRepository : IVmRepository
    {
        public VmDto DataStorage { get; set; }
        public void SetData(VmDto initData)
        {
            DataStorage = initData;
        }

        public VmDto GetData()
        {
            return DataStorage;
        }
    }
}
