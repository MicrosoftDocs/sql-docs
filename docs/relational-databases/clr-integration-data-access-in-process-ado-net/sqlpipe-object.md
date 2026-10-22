---
title: "SqlPipe Object"
description: For CLR database objects running in SQL Server, you can send results to the connected pipe using the Send methods of the SqlPipe object.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "custom result sets [CLR integration]"
  - "SqlPipe object"
  - "tabular results"
---
# SqlPipe object

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

In previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], it was common to write a stored procedure (or an extended stored procedure) that sent results or output parameters to the calling client.

In a [!INCLUDE [tsql](../../includes/tsql-md.md)] stored procedure, any `SELECT` statement that returns zero or more rows sends the results to the connected caller's "pipe."

For common language runtime (CLR) database objects running in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can send results to the connected pipe using the `Send` methods of the `SqlPipe` object. Access the `Pipe` property of the `SqlContext` object to obtain the `SqlPipe` object. The `SqlPipe` class is conceptually similar to the `Response` class found in ASP.NET.

For more information, see [Microsoft.SqlServer.Server.SqlPipe](/dotnet/api/microsoft.sqlserver.server.sqlpipe).

## Return tabular results and messages

The `SqlPipe` object has a `Send` method, which has three overloads. They are:

- `void Send(string message)`
- `void Send(SqlDataReader reader)`
- `void Send(SqlDataRecord record)`

The `Send` method sends data straight to the client or caller. It's usually the client that consumes the output from the `SqlPipe`, but with nested CLR stored procedures, the output consumer can also be a stored procedure. For example, `Procedure1` calls `SqlCommand.ExecuteReader()` with the command text `EXEC Procedure2`. `Procedure2` is also a managed stored procedure. If `Procedure2` now calls `SqlPipe.Send(SqlDataRecord)`, the row is sent to the reader in `Procedure1`, not the client.

The `Send` method sends a string message that appears on the client as an information message, equivalent to `PRINT` in [!INCLUDE [tsql](../../includes/tsql-md.md)]. It can also send a single-row result-set using `SqlDataRecord`, or a multi-row result-set using a `SqlDataReader`.

The `SqlPipe` object also has an `ExecuteAndSend` method. This method can be used to execute a command (passed as a `SqlCommand` object) and send results directly back to the caller. If there are errors in the command that was submitted, exceptions are sent to the pipe, and a copy is sent to calling managed code. If the calling code doesn't catch the exception, it propagates up the stack to the [!INCLUDE [tsql](../../includes/tsql-md.md)] code and appears in the output twice. If the calling code does catch the exception, the pipe consumer still sees the error, but there isn't a duplicate error.

It can only take a `SqlCommand` that is associated with the context connection; it can't take a command that is associated with the non-context connection.

## Return custom result sets

Managed stored procedures can send result sets that don't come from a `SqlDataReader`. The `SendResultsStart` method, along with `SendResultsRow` and `SendResultsEnd`, allows stored procedures to send custom result sets to the client.

`SendResultsStart` takes a `SqlDataRecord` as an input. It marks the beginning of a result set and uses the record metadata to construct the metadata that describes the result set. It doesn't send the value of the record with `SendResultsStart`. All the subsequent rows, sent using `SendResultsRow`, must match that metadata definition.

After you call the `SendResultsStart` method, only `SendResultsRow` and `SendResultsEnd` can be called. Calling any other method in the same instance of `SqlPipe` causes an `InvalidOperationException`. `SendResultsEnd` sets `SqlPipe` back to the initial state in which other methods can be called.

### Example

The `uspGetProductLine` stored procedure returns the name, product number, color, and list price of all products within a specified product line. This stored procedure accepts exact matches for `prodLine`.

### [C#](#tab/csharp)

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
[Microsoft.SqlServer.Server.SqlProcedure]
public static void uspGetProductLine(SqlString prodLine)
{
    // Connect through the context connection.
    using (SqlConnection connection = new SqlConnection("context connection=true"))
    {
        connection.Open();

        SqlCommand command = new SqlCommand(
            "SELECT Name, ProductNumber, Color, ListPrice " +
            "FROM Production.Product " +
            "WHERE ProductLine = @prodLine;", connection);

        command.Parameters.AddWithValue("@prodLine", prodLine);

        try
        {
            // Execute the command and send the results to the caller.
            SqlContext.Pipe.ExecuteAndSend(command);
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            // An error occurred executing the SQL command.
        }
     }
}
};
```

### [Visual Basic .NET](#tab/vb)

```vb
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports Microsoft.SqlServer.Server

Partial Public Class StoredProcedures
<Microsoft.SqlServer.Server.SqlProcedure()> _
Public Shared Sub uspGetProductLine(ByVal prodLine As SqlString)
    Dim command As SqlCommand

    ' Connect through the context connection.
    Using connection As New SqlConnection("context connection=true")
        connection.Open()

        command = New SqlCommand( _
        "SELECT Name, ProductNumber, Color, ListPrice " & _
        "FROM Production.Product " & _
        "WHERE ProductLine = @prodLine;", connection)
        command.Parameters.AddWithValue("@prodLine", prodLine)

        Try
            ' Execute the command and send the results
            ' directly to the caller.
            SqlContext.Pipe.ExecuteAndSend(command)
        Catch ex As System.Data.SqlClient.SqlException
            ' An error occurred executing the SQL command.
        End Try
    End Using
End Sub
End Class
```

---

The following [!INCLUDE [tsql](../../includes/tsql-md.md)] statement executes the `uspGetProduct` procedure, which returns a list of touring bike products.

```sql
EXECUTE uspGetProductLineVB 'T';
```

## Related content

- [SqlDataRecord object](sqldatarecord-object.md)
- [CLR stored procedures](/dotnet/framework/data/adonet/sql/clr-stored-procedures)
- [SQL Server in-process specific extensions to ADO.NET](sql-server-in-process-specific-extensions-to-ado-net.md)
