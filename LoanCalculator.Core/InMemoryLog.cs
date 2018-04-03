using System.Collections;
using System.Collections.Generic;

namespace LoanCalculator.Core
{
    /// <summary>
    /// Simple logger interface to collect error messages. can be extended or expanded
    /// </summary>
    public interface ILog
    {
        void Error(string message);
    }

    public class InMemoryLog : ILog, IEnumerable
    {
        List<string> _logMessages;
        public InMemoryLog()
        {
            _logMessages = new List<string>();
        }

        public void Error(string message)
        {
            if (!string.IsNullOrEmpty(message))
                _logMessages.Add(message);
        }

        public IEnumerator GetEnumerator()
        {
            return _logMessages.GetEnumerator();
        }
    }
}
