using System.ComponentModel.DataAnnotations;

namespace E_Agenda.Domain.ExpenseModule
{
    public enum PaymentMethod
    {
        [Display(Name = "PIX")] Pix,
        [Display(Name = "Cash")] Cash,
        [Display(Name = "Credit Card")] Credit,
        [Display(Name = "Debit Card")] Debit
    }
}