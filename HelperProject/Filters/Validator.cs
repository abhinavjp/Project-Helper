using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace HelperProject.Filters
{
    public class Validator
    {
        public static bool CheckParametersAreNull(params object[] parameterList)
        {
            if (parameterList.Length <= 0)
                return false;
            foreach (var parameter in parameterList)
            {
                if (parameter == null || parameter.ToString().Length == 0)
                    return true;
            };
            return false;
        }
    }
}