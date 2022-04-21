using System;
using System.Reflection;

namespace CumulativeProject1_AkshayaDupati.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}