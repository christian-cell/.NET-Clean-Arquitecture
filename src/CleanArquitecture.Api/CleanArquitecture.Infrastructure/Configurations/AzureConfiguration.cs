namespace CleanArquitecture.Infrastructure.Configurations
{
    public class AzureConfiguration
    {
        public SqlConfiguration Sql { get; set; }
         
        public AzureConfiguration(SqlConfiguration sql)
        {
            Sql = sql;
        }
            
        public class SqlConfiguration
        {
            public string DockerConnectionString { get; set; }
            public string LocalConnectionString { get; set; }
                
            public SqlConfiguration(string dockerConnectionString, string localConnectionString)
            {
                DockerConnectionString = dockerConnectionString ??
                                         throw new ArgumentNullException(nameof(dockerConnectionString));
                LocalConnectionString = localConnectionString ??
                                        throw new ArgumentNullException(nameof(localConnectionString));
            }
        }
    }
};

