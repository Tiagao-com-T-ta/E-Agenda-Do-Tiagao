using E_Agenda.Domain.CategoriesModule;
using E_Agenda.Domain.ExpensesModule;
using E_Agenda.Structure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.Structure.ExpensesModule
{
    public class ExpensesRepositoryFile : BaseRepositoryFile<Expense>, IExpensesRepository
    {
        public ExpensesRepositoryFile(DataContext dataContext) : base(dataContext)
        {
        }

        protected override List<Expense> GetRegisters()
        {
            return dataContext.Expenses;
        }
    }
}
