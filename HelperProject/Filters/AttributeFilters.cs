using System;
using System.Reflection;

namespace HelperProject.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CheckForNullParametersAttribute: Attribute
    {
        public void CheckParameterIsNull(ParameterInfo parameter, object value)
        {
            if (value == null || value.ToString().Length == 0)
                throw new ArgumentNullException(parameter.Name, parameter.Name + " should always have a value");
        }
    }
}