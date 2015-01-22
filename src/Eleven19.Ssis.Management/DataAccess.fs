namespace Eleven19.Ssis.Management.Data
open System
open System.Data
open Insight.Database

type IUseDatabase = 
    abstract member GetConnection  : IDbConnection 

[<AutoOpen>]
module Database =
    open System.Data.SqlClient

    let GetConnectionStringBuilder connectionStringOrName =
        match connectionStringOrName with
        | ConnectionName connectionName -> 
            let connectionString = "" // Add way to get connectionString
            new SqlConnectionStringBuilder(connectionString)
        | ConnectionString connectionString -> new SqlConnectionStringBuilder(connectionString)

    let CreateRepository<'T when 'T: not struct> resolver connectionStringOrName =
        let sqlConnectionStringBuilder = connectionStringOrName |> resolver
        Insight.Database.SqlConnectionStringBuilderExtensions.As<'T>(sqlConnectionStringBuilder)
    
//    let CreateRepository<'T when 'T: not struct> connectionStringOrName =
//        let csb = getConnectionStringBuilder connectionStringOrName
//        Insight.Database.SqlConnectionStringBuilderExtensions.As<'T>(csb)