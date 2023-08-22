using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Formatting;
using Serilog.Sinks.MSSqlServer;

namespace RestaurantFinalAPI.Configurations.SeriLog
{
    //public class UserNameColumn : MSSqlServerSink
    //{
    //    public UserNameColumn(string connectionString, MSSqlServerSinkOptions sinkOptions, IFormatProvider formatProvider = null, ColumnOptions columnOptions = null, ITextFormatter logEventFormatter = null) : base(connectionString, sinkOptions, formatProvider, columnOptions, logEventFormatter)
    //    {
    //    }

    //    public UserNameColumn(string connectionString, string tableName, int batchPostingLimit, TimeSpan period, IFormatProvider formatProvider, bool autoCreateSqlTable = false, ColumnOptions columnOptions = null, string schemaName = "dbo", ITextFormatter logEventFormatter = null) : base(connectionString, tableName, batchPostingLimit, period, formatProvider, autoCreateSqlTable, columnOptions, schemaName, logEventFormatter)
    //    {
    //    }

    //}

    //public class UserNameWriter : Column
    //{
    //    public UserNameWriter(string name, string type, TableBase table) : base(name, type, table)
    //    {
    //    }


    //    public LogEventProperty CreateProperty(string name, object? value, bool destructureObjects = false)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    //public void Emit(LogEvent logEvent)
    //    //{
    //    //    var (username, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
    //    //    return value?.ToString() ?? null;
    //    //}
    //    public LogEventPropertyValue CreatePropertyValue(object? value, bool destructureObjects = false)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
    //{
    //    var (username, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
    //    return value?.ToString() ?? null;
    //}

    public class UserNameWriter : IFormatProvider
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            //iTextFormatter
            var (username, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
            //    return value?.ToString() ?? null;
            throw new NotImplementedException();
        }

        public object? GetFormat(Type? formatType)
        {//IFormatProvider
            throw new NotImplementedException();
        }
    }
}
