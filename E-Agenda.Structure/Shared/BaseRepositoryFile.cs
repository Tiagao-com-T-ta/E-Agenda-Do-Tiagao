using E_Agenda.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Agenda.Structure.Shared
{
    public abstract class BaseRepositoryFile<T> where T : BaseEntity<T>
    {
        protected DataContext dataContext;
        protected List<T> registers = new List<T>();


        protected BaseRepositoryFile(DataContext dataContext)
        {
            this.dataContext = dataContext;
            registers = GetRegisters();
        }

        protected abstract List<T> GetRegisters();

        public void CreateRegister(T newRegister)
        {
            registers.Add(newRegister);
            dataContext.SaveData();
        }

        public bool EditRegister(Guid registerId, T editedRegister)
        {
            foreach (T register in registers)
            {
                if (register.Id == registerId)
                {
                    register.Update(editedRegister);
                    dataContext.SaveData();
                    return true;
                }
            }

            return false;
        }

        public bool DeleteRegister(Guid registerId)
        {
            T registerToDelete = GetRegistersByID(registerId);

            if (registerToDelete != null)
            {
                registers.Remove(registerToDelete);
                dataContext.SaveData();
                return true;
            }

            return false;
        }

        public List<T> GetAllRegisters()
        {
            return registers;
        }

        public T GetRegistersByID(Guid registerId)
        {
            foreach (T register in registers)
            {
                if (register.Id == registerId)
                {
                    return register;
                }
            }
            return null!;
        }
    }
}

