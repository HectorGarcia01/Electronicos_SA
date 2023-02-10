using System;
using System.Reflection;

namespace Aplicacion_Web_Tienda_Electronicos_SA.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}