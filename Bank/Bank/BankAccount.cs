using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountNS
{
    /// <summary>
    /// Класс банковского счета
    /// </summary>
    public class BankAccount
    {
        private readonly string m_customerName;
        private double m_balance;

        /// <summary>
        /// Конструктор счета
        /// </summary>
        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
        }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string CustomerName => m_customerName;

        /// <summary>
        /// Баланс
        /// </summary>
        public double Balance => m_balance;

        /// <summary>
        /// Снятие денег
        /// </summary>
        /// <param name="amount">Сумма</param>
        public void Debit(double amount)
        {
            if (amount > m_balance)
                throw new ArgumentOutOfRangeException(nameof(amount), "Слишком большая сумма");

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Сумма меньше нуля");

            m_balance -= amount; // ВАЖНО (исправленная ошибка)
        }

        /// <summary>
        /// Пополнение счета
        /// </summary>
        public void Credit(double amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            m_balance += amount;
        }

        static void Main()
        {
            BankAccount acc = new BankAccount("Roman", 100);

            acc.Credit(50);
            acc.Debit(20);

            Console.WriteLine(acc.Balance);
            Console.ReadLine();
        }
    }
}