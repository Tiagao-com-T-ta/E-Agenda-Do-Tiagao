using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.CategoryModule;
using E_Agenda.Domain.Shared;
using E_Agenda.Structure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.Structure.CategoriesModule
{
    public class CategoryRepositoryFile : BaseRepositoryFile<Category>, ICategoryRepository
    {
        public CategoryRepositoryFile(DataContext dataContext) : base(dataContext)
        {
        }

        protected override List<Category> GetRegisters()
        {
            return dataContext.Categories;
        }
    }
}
