using Entities.Models;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface IExamTypeRepository : IRepositoryBase<ExamType>
    {
        Task<List<ExamType>> GetAllExamTypesAsync(bool trackChanges);
        Task<ExamType> GetOneExamTypeByIdAsync(int examTypeId, bool trackChanges);
        void CreateOneExamType(ExamType examType);
        void DeleteOneExamType(ExamType examType);
    }
}
