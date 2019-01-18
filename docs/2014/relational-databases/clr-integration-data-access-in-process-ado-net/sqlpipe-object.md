---
title: "SqlPipe Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "custom result sets [CLR integration]"
  - "SqlPipe object"
  - "tabular results"
ms.assetid: 3e090faf-085f-4c01-a565-79e3f1c36e3b
author: rothja
ms.author: jroth
manager: craigg
---
# SqlPipe Object
  In previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it is very common to write a stored procedure (or an extended stored procedure) that sends results or output parameters to the calling client.  
  
 In a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure, any `SELECT` statement that returns zero or more rows sends the results to the connected caller's "pipe."  
  
 For common language runtime (CLR) database objects running in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can send results to the connected pipe using the `Send` methods of the `SqlPipe` object. Access the `Pipe` property of the `SqlContext` object to obtain the `SqlPipe` object. The `SqlPipe` class is conceptually similar to the `Response` class found in ASP.NET. For more information, see the SqlPipe Class reference documentation in the .NET Framework software development kit.  
  
## Returning Tabular Results and Messages  
 The `SqlPipe` has a `Send` method, which has three overloads. They are:  
  
-   `void Send(string message)`  
  
-   `void Send(SqlDataReader reader)`  
  
-   `void Send(SqlDataRecord record)`  
  
 The `Send` method sends data straight to the client or caller. It is usually the client that consumes the output from the `SqlPipe`, but in the case of nested CLR stored procedures the output consumer can also be a stored procedure. For example, Procedure1 calls SqlCommand.ExecuteReader() with the command text "EXEC Procedure2". Procedure2 is also a managed stored procedure. If Procedure2 now calls SqlPipe.Send( SqlDataRecord ), the row is sent to Procedure1's reader, not the client.  
  
 The `Send` method sends a string message that appears on the client as an information message, equivalent to PRINT in [!INCLUDE[tsql](../../includes/tsql-md.md)]. It can also send a single-row result-set using `SqlDataRecord`, or a multi-row result-set using a `SqlDataReader`.  
  
 The `SqlPipe` object also has an `ExecuteAndSend` method. This method can be used to execute a command (passed as a `SqlCommand` object) and send results directly back to the caller. If there are errors in the command that was submitted, exceptions are sent to the pipe, but a copy is also sent to calling managed code. If the calling code does not catch the exception, it propagates up the stack to the [!INCLUDE[tsql](../../includes/tsql-md.md)] code and appears in the output twice. If the calling code does catch the exception, the pipe consumer still sees the error, but there is not a duplicate error.  
  
 It can only take a `SqlCommand` that is associated with the context connection; it cannot take a command that is associated with the non-context connection.  
  
## Returning Custom Result Sets  
 Managed stored procedures can send result sets that do not come from a `SqlDataReader`. The `SendResultsStart` method, along with `SendResultsRow` and `SendResultsEnd`, allows stored procedures to send custom result sets to the client.  
  
 `SendResultsStart` takes a `SqlDataRecord` as an input. It marks the beginning of a result set and uses the record metadata to construct the metadata that describes the result set. It does not send the value of the record with `SendResultsStart`. All the subsequent rows, sent using `SendResultsRow`, must match that metadata definition.  
  
> [!NOTE]  
>  After calling the `SendResultsStart` method only `SendResultsRow` and `SendResultsEnd` can be called. Calling any other method in the same instance of `SqlPipe` causes an `InvalidOperationException`. `SendResultsEnd` sets `SqlPipe` back to the initial state in which other methods can be called.  
  
### Example  
 The `uspGetProductLine` stored procedure returns the name, product number, color, and list price of all products within a specified product line. This stored procedure accepts exact matches for *prodLine*.  
  
 C#  
  
```  
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
  
 Visual Basic  
  
```  
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
  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement executes the `uspGetProduct` procedure, which returns a list of touring bike products.  
  
```  
EXEC uspGetProductLineVB 'T';  
```  
  
## See Also  
 [SqlDataRecord Object](sqldatarecord-object.md)   
 [CLR Stored Procedures](../../database-engine/dev-guide/clr-stored-procedures.md)   
 [SQL Server In-Process Specific Extensions to ADO.NET](sql-server-in-process-specific-extensions-to-ado-net.md)  
  
  
