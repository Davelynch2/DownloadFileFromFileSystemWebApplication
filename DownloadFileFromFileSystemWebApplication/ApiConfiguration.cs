using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadFileFromFileSystemWebApplication
{
    public static class EnvConfigKeyConstants
    {
        public const string AspNetCoreEnv = "ASPNETCORE_ENVIRONMENT";
        public const string SqlConnection = "CONFIG_SQL_CONNECTION";
    }

    public static class JsonConfigKeyConstants
    {
    }
    public class ApiConfiguration
    {
        public string SqlConnection { get; }
        public ApiConfiguration(IConfiguration configuration)
        {
            ValidateConfigContainsRequiredValues(configuration);
            SqlConnection = configuration[EnvConfigKeyConstants.SqlConnection];
        }

        private static void ValidateConfigContainsRequiredValues(IConfiguration config)
        {
            ValidateEnvironmentVariableHasValue(EnvConfigKeyConstants.SqlConnection);

            void ValidateEnvironmentVariableHasValue(string environmentVariable)
            {
                if (string.IsNullOrEmpty(config[environmentVariable])) throw new Exception($"{environmentVariable} environment variable not defined or has empty value.");
            }
        }
    }
}
