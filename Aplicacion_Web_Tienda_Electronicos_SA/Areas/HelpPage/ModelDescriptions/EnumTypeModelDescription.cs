using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aplicacion_Web_Tienda_Electronicos_SA.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}