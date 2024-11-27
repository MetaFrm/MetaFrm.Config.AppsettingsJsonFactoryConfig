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


        Task<string> IFactoryConfig.GetAttributeAsync(ICore core, string attributeName)
        {
            throw new NotImplementedException();
        }
        string IFactoryConfig.GetAttribute(ICore core, string attributeName)
        {
            return (this as IFactoryConfig).GetAttribute($"{core.GetType().FullName}", attributeName);
        }


        Task<string> IFactoryConfig.GetAttributeAsync<T>(ICore core, string attributeName)
        {
            throw new NotImplementedException();
        }
        string IFactoryConfig.GetAttribute<T>(ICore core, string attributeName)
        {
            Type type = core.GetType();

            return (this as IFactoryConfig).GetAttribute($"{type.Namespace}.{type.Name}" + "[{0}]", attributeName);
        }


        Task<List<string>> IFactoryConfig.GetAttributeAsync(ICore core, List<string> listAttributeName)
        {
            throw new NotImplementedException();
        }
        List<string> IFactoryConfig.GetAttribute(ICore core, List<string> listAttributeName)
        {
            List<string> vs = [];

            foreach (var attribute in listAttributeName)
                vs.Add((this as IFactoryConfig).GetAttribute(core, attribute));

            return vs;
        }


        Task<List<string>> IFactoryConfig.GetAttributeAsync<T>(ICore core, List<string> listAttributeName)
        {
            throw new NotImplementedException();
        }
        List<string> IFactoryConfig.GetAttribute<T>(ICore core, List<string> listAttributeName)
        {
            List<string> vs = [];

            foreach (var attribute in listAttributeName)
                vs.Add((this as IFactoryConfig).GetAttribute<T>(core, attribute));

            return vs;
        }


        Task<string> IFactoryConfig.GetAttributeAsync(string namespaceName, string attributeName)
        {
            throw new NotImplementedException();
        }
        string IFactoryConfig.GetAttribute(string namespaceName, string attributeName)
        {
            return configuration.GetValue($"{namespaceName}.{attributeName}") ?? "";
        }


        string IFactoryConfig.GetPath(string namespaceName)
        {
            return configuration.GetValue($"{namespaceName}.Path") ?? "";
        }
    }
}