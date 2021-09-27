using System;
using System.Data;
using Dapper;

namespace MetricsAgent.DAL
{
    public class DateTimeHandler : SqlMapper.TypeHandler<DateTimeOffset>

    {
        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)=>parameter.Value = value.ToUnixTimeSeconds();
  

        public override DateTimeOffset Parse(object value)=>  DateTimeOffset.FromUnixTimeSeconds((int) value);
      
    }
}