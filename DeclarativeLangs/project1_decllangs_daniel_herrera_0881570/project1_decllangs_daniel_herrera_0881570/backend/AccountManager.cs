using project1_decllangs_daniel_herrera_0881570.backend.schemaClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace project1_decllangs_daniel_herrera_0881570.backend
{
    // CLASS used for the CRUD functions of the accounts

    // this class can:
    // insert a new account with validation
    // select an account instance
    // update an account password instance
    // delete an account instance
    public class AccountManager
    {
        public List<Account> Accounts { get; set; }
        public Account Selected { get; set; }

        public AccountManager()
        {
            Accounts = new List<Account>();
            Selected = null;
        }

        public AccountManager(List<Account> accounts)
        {
            Accounts = accounts;
            Selected = null;
        }

        public void Select(int index) => Selected = Accounts[index];

        public bool Insert(Account new_account)
        {
            if (Validate(new_account, out _))
            {
                Accounts.Add(new_account);
                return true;
            }
            return false;
        }

        public bool Insert(
            string description,
            string userid,
            string password,
            string login_url,
            string account_num
        )
        {
            Account new_account = new Account
            {
                Description = description,
                AccountNum = account_num,
                LoginUrl = login_url,
                UserId = userid,
                Password = _NewPassword(new Password(), password)
            };
            if (Validate(new_account, out _))
            {
                Accounts.Add(new_account);
                return true;
            }
            return false;
        }

        public Account Update(string password)
        {
            Account old;
            if (Accounts.Any() && Selected != null)
            {
                old = Selected;
                Selected.Password = _NewPassword(Selected.Password, password);
            }
            else
                old = null;
            return old;
        }

        public Account Delete()
        {
            Account old;
            if (Accounts.Any() && Selected != null)
            {
                old = Selected;
                Accounts.Remove(Selected);
                Selected = null;
            }
            else
                old = null;
            return old;
        }

        private Password _NewPassword(Password current, string password_value)
        {
            PasswordTester pt = new PasswordTester(password_value);
            current.Value = pt.Value;
            current.StrengthText = pt.StrengthLabel;
            current.StrengthNum = pt.StrengthPercent;
            current.LastReset = DateTime.Now.ToShortDateString();
            return current;
        }

        public static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }
    }
}
