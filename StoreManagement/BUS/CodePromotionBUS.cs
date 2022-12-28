using StoreManagement.DAO;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BUS
{
    public class CodePromotionBUS
    {
        private CodePromotionDAO CodeDAO = new CodePromotionDAO();

        public void Delete(CodePromotion t)
        {
            CodeDAO.Delete(t);
        }

        public CodePromotion Get(string code)
        {
            return CodeDAO.Get(code);
        }

        public List<CodePromotion> GetAll()
        {
            return CodeDAO.GetAll();
        }

        public void Add(CodePromotion t)
        {
            CodeDAO.Add(t);
        }

        public void Update(CodePromotion t)
        {
            CodeDAO.Update(t);
        }
    }
}
