﻿using System;
using System.Collections.Generic;

namespace Core.Intefaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
        void Create(T item);


    }
}