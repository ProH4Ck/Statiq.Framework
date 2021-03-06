﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetEscapades.Extensions.Logging.RollingFile.Internal;

namespace Statiq.App
{
    internal class LogMessage
    {
        public LogMessage(
            string categoryName,
            DateTimeOffset timestamp,
            LogLevel logLevel,
            EventId eventId,
            string formattedMessage,
            Exception exception)
        {
            CategoryName = categoryName;
            Timestamp = timestamp;
            LogLevel = logLevel;
            EventId = eventId;
            FormattedMessage = formattedMessage;
            Exception = exception;
        }

        public string CategoryName { get; }
        public DateTimeOffset Timestamp { get; }
        public LogLevel LogLevel { get; }
        public EventId EventId { get; }
        public string FormattedMessage { get; }
        public Exception Exception { get; }
    }
}
