using E_Agenda.Domain.Shared;
using E_Agenda.Domain.ExpenseModule;
using System;
using System.Collections.Generic;

namespace E_Agenda.Domain.CategoryModule;

public class Category : BaseEntity<Category>
{
    public string Title { get; set; }
    public List<Expense> Expenses { get; set; }

    public Category()
    {
        Expenses = new List<Expense>();
    }

    public Category(string title) : this()
    {
        Id = Guid.NewGuid();
        Title = title;
    }

    public override void Update(Category editedCategory)
    {
        Title = editedCategory.Title;
    }
}
