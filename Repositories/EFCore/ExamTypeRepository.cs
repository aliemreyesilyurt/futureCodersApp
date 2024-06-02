using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class ExamTypeRepository : RepositoryBase<ExamType>, IExamTypeRepository
    {
        public ExamTypeRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneExamType(ExamType examType) => Create(examType);

        public void DeleteOneExamType(ExamType examType) => Delete(examType);

        public async Task<List<ExamType>> GetAllExamTypesAsync(bool trackChanges)
        {
            var examTypes = await FindAll(trackChanges)
                .OrderBy(e => e.Id)
                .ToListAsync();

            return examTypes;
        }

        public async Task<ExamType> GetOneExamTypeByIdAsync(int examTypeId, bool trackChanges) =>
            await FindByCondition(e => e.Id.Equals(examTypeId), trackChanges)
                .SingleOrDefaultAsync();
    }
}
