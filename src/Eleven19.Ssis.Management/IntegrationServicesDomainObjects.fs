namespace Eleven19.Ssis.Management
open Eleven19.Ssis.Management.Data

type IntegrationServices(connectionString:ConnectionString) =                
    member this.ConnectionString = connectionString

type Catalog(integrationServices:IntegrationServices) = 
    member this.IntegrationServices = integrationServices
