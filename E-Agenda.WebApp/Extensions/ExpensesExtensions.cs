using E_Agenda.Domain.ExpenseModule;
using E_Agenda.WebApp.Models;

namespace E_Agenda.WebApp.Extensions;

public static class ExpenseExtensions
{
    public static Expense ToEntity(this ExpenseFormViewModel formVM)
    {
        return new Expense(
            formVM.Description,
            formVM.Value,
            formVM.OccurrenceDate,
            formVM.PaymentMethod
        );
    }

    public static ExpenseDetailsViewModel ToDetailsVM(this Expense expense)
    {
        return new ExpenseDetailsViewModel(
            expense.Id,
            expense.Description,
            expense.Value,
            expense.OccurrenceDate,
            expense.PaymentMethod,
            expense.Categories
        );
    }
}
