﻿namespace TDV.MoyNalog.Tests.Helpers;

public static class EnviromentHelper
{
    public static string GetVariable(string variable)
    {
        var result = Environment.GetEnvironmentVariable(variable);
        if (string.IsNullOrEmpty(result))
        {
            throw new ApplicationException(variable + " enviroment variable is not set");
        }
        return result;
    }
}
