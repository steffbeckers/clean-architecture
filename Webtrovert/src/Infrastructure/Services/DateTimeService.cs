using Webtrovert.Application.Common.Interfaces;
using System;

namespace Webtrovert.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
