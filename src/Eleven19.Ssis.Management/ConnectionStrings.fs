namespace Eleven19.Ssis.Management.Data
    

    [<AutoOpen>]
    module ConnectionStrings =
        type ConnectionStringOrName =
        | ConnectionString of string
        | ConnectionName of string    


