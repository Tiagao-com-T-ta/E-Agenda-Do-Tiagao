using E_Agenda.Domain.ExpensesModule;
using E_Agenda.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.Domain.CategoriesModule
{
    public class Category : BaseEntity<Category>
    {
        public string Title { get; set; }
        public List<Expense> Expenses { get; }        
        public Category()
        {
        }

        public Category(string title) : this()
        {
            Title = title;
            Expenses = new List<Expense>();
            Id = Guid.NewGuid();
        }

        public override void Update(Category editedRegister)
        {
           Title = editedRegister.Title;

        }
    }
}
