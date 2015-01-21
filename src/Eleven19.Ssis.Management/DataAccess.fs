namespace Eleven19.Ssis.Management.Data
open System
open Insight.Database

[<AutoOpen>]
module Database =
    open System.Data.SqlClient

    let getConnectionStringBuilder connectionStringOrName =
        match connectionStringOrName with
        | ConnectionName connectionName -> 
            let connectionString = "" // Add way to get connectionString
            new SqlConnectionStringBuilder(connectionString)
        | ConnectionString connectionString -> new SqlConnectionStringBuilder(connectionString)

    let createRepository<'T when 'T: not struct> connectionStringOrName =
        let csb = getConnectionStringBuilder connectionStringOrName
        Insight.Database.SqlConnectionStringBuilderExtensions.As<'T>(csb)