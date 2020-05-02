using SteffBeckers.Application.Common.Interfaces;
using System;

namespace SteffBeckers.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
