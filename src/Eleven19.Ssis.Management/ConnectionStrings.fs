namespace Eleven19.Ssis.Management

[<AutoOpen>]
module ConnectionStrings =
    type ConnectionStringOrName =
    | ConnectionString of string
    | ConnectionName of string


