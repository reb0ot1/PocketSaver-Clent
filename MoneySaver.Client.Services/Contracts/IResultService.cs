using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneySaver.Client.Services.Contracts
{
    public interface IResultService
    {
        Task<string> GetResultsAsync();
    }
}
