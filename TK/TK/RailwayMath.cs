using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TK
{
    public static class RailwayMath
    {
        public const decimal PricePerKm = 8m;

        public static decimal GetComfortCoefficient(string wagonType)
        {
            switch (wagonType)
            {
                case "Плацкарт":
                    return 1.0m;
                case "Купе":
                    return 1.1m;
                case "Полулюкс":
                    return 1.2m;
                case "Люкс":
                    return 1.3m;
                default:
                    throw new System.ArgumentException("Неизвестный тип вагона");
            }
        }

        public static decimal CalculateSingleTicketPrice(decimal distance, decimal comfortCoefficient)
        {
            if (distance <= 0)
                throw new System.ArgumentException("Расстояние должно быть больше 0");

            return distance * PricePerKm * comfortCoefficient;
        }

        public static decimal CalculateTotalPrice(decimal distance, int ticketsCount, decimal comfortCoefficient)
        {
            if (ticketsCount <= 0)
                throw new System.ArgumentException("Количество билетов должно быть больше 0");

            decimal oneTicketPrice = CalculateSingleTicketPrice(distance, comfortCoefficient);
            return oneTicketPrice * ticketsCount;
        }
    }
}