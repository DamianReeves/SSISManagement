namespace Eleven19.Ssis.Management
open Eleven19.Ssis.Management.Data

type IntegrationServices(connectionString) =                
    member this.ConnectionString = connectionString

type Catalog(integrationServices:IntegrationServices) = 
    member this.IntegrationServices = integrationServices
