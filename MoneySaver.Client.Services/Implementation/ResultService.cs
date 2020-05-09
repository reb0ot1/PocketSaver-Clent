using MoneySaver.Client.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneySaver.Client.Services.Implementation
{
    public class ResultService : IResultService
    {
        
        public async Task<string> GetResultsAsync()
        {
            return "This is a test";
        }
    }
}
