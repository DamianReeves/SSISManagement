namespace Eleven19.Ssis.Management.Data
    
    type ConnectionString = ConnectionString of string

    [<AutoOpen>]
    module ConnectionStrings =
        type ConnectionStringOrName =
        | ConnectionString of string
        | ConnectionName of string    


