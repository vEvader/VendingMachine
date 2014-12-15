using Common.VmEntities;

namespace RepositoryInterface
{
    public interface IVmRepository
    {
        void SetData(VmDto initData);
        VmDto GetData();
    }
}
