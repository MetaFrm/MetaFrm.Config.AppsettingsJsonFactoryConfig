using MetaFrm.Extensions;
using Microsoft.Extensions.Configuration;

namespace MetaFrm.Config
{
    /// <summary>
    /// FactoryConfig를 appsettings.json으로 관리하는 클래스 입니다.
    /// </summary>
    public class AppsettingsJsonFactoryConfig : IFactoryConfig
    {
        readonly IConfigurationRoot configuration;

        /// <summary>
        /// AppsettingsJsonFactoryConfig 인스턴스를 생성합니다.
        /// </summary>
        public AppsettingsJsonFactoryConfig()
        {
            IConfigurationBuilder builder1 = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            this.configuration = builder1.Build();
        }

        string IFactoryConfig.GetAttribute(ICore core, string attributeName)
        {
            return (this as IFactoryConfig).GetAttribute($"{core.GetType().FullName}", attributeName);
        }
        string IFactoryConfig.GetAttribute<T>(ICore core, string attributeName)
        {
            Type type = core.GetType();

            return (this as IFactoryConfig).GetAttribute($"{type.Namespace}.{type.Name}" + "[{0}]", attributeName);
        }

        List<string> IFactoryConfig.GetAttribute(ICore core, List<string> listAttributeName)
        {
            List<string> vs = new();

            foreach (var attribute in listAttributeName)
                vs.Add((this as IFactoryConfig).GetAttribute(core, attribute));

            return vs;
        }
        List<string> IFactoryConfig.GetAttribute<T>(ICore core, List<string> listAttributeName)
        {
            List<string> vs = new();

            foreach (var attribute in listAttributeName)
                vs.Add((this as IFactoryConfig).GetAttribute<T>(core, attribute));

            return vs;
        }

        string IFactoryConfig.GetAttribute(string namespaceName, string attributeName)
        {
            //await Task.Run(() => { });
            return configuration.GetValue($"{namespaceName}.{attributeName}") ?? "";
        }

        string IFactoryConfig.GetPath(string namespaceName)
        {
            return configuration.GetValue($"{namespaceName}.Path") ?? "";
        }
    }
}