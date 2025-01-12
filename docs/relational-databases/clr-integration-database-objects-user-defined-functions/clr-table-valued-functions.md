---
title: CLR Table-Valued Functions
description: A table-valued function returns a table. In SQL Server CLR integration, you can write table-valued functions in managed code.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "Transact-SQL table-valued functions"
  - "table-valued functions [CLR integration]"
  - "TVFs [CLR integration]"
dev_langs:
  - "TSQL"
  - "VB"
  - "CSharp"
---
# CLR table-valued functions

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

A table-valued function is a user-defined function that returns a table.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] extends the functionality of table-valued functions by allowing you to define a table-valued function in any managed language. Data is returned from a table-valued function through an `IEnumerable` or `IEnumerator` object.

For table-valued functions, the columns of the return table type can't include timestamp columns or non-Unicode string data type columns (such as **char**, **varchar**, and **text**). The `NOT NULL` constraint isn't supported.

## Differences between Transact-SQL and CLR table-valued functions

[!INCLUDE [tsql](../../includes/tsql-md.md)] table-valued functions materialize the results of calling the function into an intermediate table. Since they use an intermediate table, they can support constraints and unique indexes over the results. These features can be useful when large results are returned.

In contrast, common language runtime (CLR) table-valued functions represent a streaming alternative. There's no requirement that the entire set of results be materialized in a single table. The `IEnumerable` object returned by the managed function is directly called by the execution plan of the query that calls the table-valued function, and the results are consumed in an incremental manner. This streaming model ensures that results can be consumed immediately after the first row is available, instead of waiting for the entire table to be populated. It's also a better alternative if you have large numbers of rows returned, because they don't have to be materialized in memory as a whole. For example, a managed table-valued function could be used to parse a text file and return each line as a row.

## Implement table-valued functions

Implement table-valued functions as methods on a class in a [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] assembly. Your table-valued function code must implement the `IEnumerable` interface. The `IEnumerable` interface is defined in the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)]. Types representing arrays and collections in the [!INCLUDE [dnprdnshort-md](../../includes/dnprdnshort-md.md)] already implement the `IEnumerable` interface. This makes it easy for writing table-valued functions that convert a collection or an array into a result set.

## Table-valued parameters

Table-valued parameters are user-defined table types that are passed into a procedure or function and provide an efficient way to pass multiple rows of data to the server. Table-valued parameters provide similar functionality to parameter arrays, but offer greater flexibility and closer integration with [!INCLUDE [tsql](../../includes/tsql-md.md)]. They also provide the potential for better performance.

Table-valued parameters also help reduce the number of round trips to the server. Instead of sending multiple requests to the server, such as with a list of scalar parameters, data can be sent to the server as a table-valued parameter. A user-defined table type can't be passed as a table-valued parameter to, or be returned from, a managed stored procedure or function executing in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] process. For more information about table-valued parameters, see [Use table-valued parameters (Database Engine)](../tables/use-table-valued-parameters-database-engine.md).

## Output parameters and table-valued functions

Information might be returned from table-valued functions using output parameters. The corresponding parameter in the implementation code table-valued function should use a pass-by-reference parameter as the argument. [!INCLUDE [visual-basic-md](../../includes/visual-basic-md.md)] .NET doesn't support output parameters in the same way that [!INCLUDE [c-sharp-md](../../includes/c-sharp-md.md)] does. You must specify the parameter by reference and apply the `<Out()>` attribute to represent an output parameter, as in the following example:

```vb
Imports System.Runtime.InteropServices
...
Public Shared Sub FillRow ( <Out()> ByRef value As SqlInt32)
```

### Define a table-valued function in Transact-SQL

The syntax for defining a CLR table-valued function is similar to that of a [!INCLUDE [tsql](../../includes/tsql-md.md)] table-valued function, with the addition of the `EXTERNAL NAME` clause. For example:

```sql
CREATE FUNCTION GetEmpFirstLastNames()
RETURNS TABLE (
    FirstName NVARCHAR (4000),
    LastName NVARCHAR (4000)
)
AS EXTERNAL NAME MyDotNETAssembly.[MyNamespace.MyClassname].GetEmpFirstLastNames;
```

Table-valued functions are used to represent data in relational form for further processing in queries such as:

```sql
SELECT *
FROM func();

SELECT *
FROM tbl
     INNER JOIN func() AS f
         ON tbl.col = f.col;

SELECT *
FROM tbl AS t
CROSS APPLY func(t.col);
```

Table-valued functions can return a table when:

- Created from scalar input arguments. For example, a table-valued function that takes a comma-delimited string of numbers and pivots them into a table.

- Generated from external data. For example, a table-valued function that reads the event log and exposes it as a table.

> [!NOTE]  
> A table-valued function can only perform data access through a [!INCLUDE [tsql](../../includes/tsql-md.md)] query in the `InitMethod` method, and not in the `FillRow` method. The `InitMethod` should be marked with the `SqlFunction.DataAccess.Read` attribute property if a [!INCLUDE [tsql](../../includes/tsql-md.md)] query is performed.

## A sample table-valued function

The following table-valued function returns information from the system event log. The function takes a single string argument containing the name of the event log to read.

### Sample code

### [C#](#tab/csharp)

```csharp
using System;
using System.Data.Sql;
using Microsoft.SqlServer.Server;
using System.Collections;
using System.Data.SqlTypes;
using System.Diagnostics;

public class TabularEventLog
{
    [SqlFunction(FillRowMethodName = "FillRow")]
    public static IEnumerable InitMethod(String logname)
    {
        return new EventLog(logname).Entries;
    }

    public static void FillRow(Object obj, out SqlDateTime timeWritten, out SqlChars message, out SqlChars category, out long instanceId)
    {
        EventLogEntry eventLogEntry = (EventLogEntry)obj;
        timeWritten = new SqlDateTime(eventLogEntry.TimeWritten);
        message = new SqlChars(eventLogEntry.Message);
        category = new SqlChars(eventLogEntry.Category);
        instanceId = eventLogEntry.InstanceId;
    }
}
```

### [Visual Basic .NET](#tab/vb)

```vb
Imports System
Imports System.Data.Sql
Imports Microsoft.SqlServer.Server
Imports System.Collections
Imports System.Data.SqlTypes
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Public Class TabularEventLog
    <SqlFunction(FillRowMethodName:="FillRow")> _
    Public Shared Function InitMethod(ByVal logname As String) As IEnumerable
        Return New EventLog(logname).Entries
    End Function

    Public Shared Sub FillRow(ByVal obj As Object, <Out()> ByRef timeWritten As SqlDateTime, <Out()> ByRef message As SqlChars, <Out()> ByRef category As SqlChars, <Out()> ByRef instanceId As Long)
        Dim eventLogEnTry As EventLogEntry = CType(obj, EventLogEntry)
        timeWritten = New SqlDateTime(eventLogEnTry.TimeWritten)
        message = New SqlChars(eventLogEnTry.Message)
        category = New SqlChars(eventLogEnTry.Category)
        instanceId = eventLogEnTry.InstanceId
    End Sub
End Class
```

---

#### Declare and using the sample table-valued function

After the sample table-valued function is compiled, it can be declared in [!INCLUDE [tsql](../../includes/tsql-md.md)] like this:

```sql
USE master;
-- Replace SQL_Server_logon with your SQL Server user credentials.

GRANT EXTERNAL ACCESS ASSEMBLY TO [SQL_Server_logon];

-- Modify the following line to specify a different database.
ALTER DATABASE master
SET TRUSTWORTHY ON;

-- Modify the next line to use the appropriate database.
CREATE ASSEMBLY tvfEventLog
    FROM 'D:\assemblies\tvfEventLog\tvfeventlog.dll'
    WITH PERMISSION_SET = EXTERNAL_ACCESS;
GO

CREATE FUNCTION ReadEventLog
(@logname NVARCHAR (100))
RETURNS TABLE (
    logTime DATETIME,
    Message NVARCHAR (4000),
    Category NVARCHAR (4000),
    InstanceId BIGINT)
AS EXTERNAL NAME tvfEventLog.TabularEventLog.InitMethod;
GO
```

Visual C++ database objects compiled with `/clr:pure` aren't supported for execution on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. For example, such database objects include table-valued functions.

To test the sample, try the following [!INCLUDE [tsql](../../includes/tsql-md.md)] code:

```sql
-- Select the top 100 events,
SELECT TOP 100 *
FROM dbo.ReadEventLog(N'Security') AS T;
GO

-- Select the last 10 login events.
SELECT TOP 10 T.logTime,
              T.Message,
              T.InstanceId
FROM dbo.ReadEventLog(N'Security') AS T
WHERE T.Category = N'Logon/Logoff';
GO
```

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Return the results of a SQL Server query

The following sample shows a table-valued function that queries a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database.

Name your source code file FindInvalidEmails.cs or FindInvalidEmails.vb.

### [C#](#tab/csharp)

```csharp
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions {
   private class EmailResult {
      public SqlInt32 CustomerId;
      public SqlString EmailAddress;

      public EmailResult(SqlInt32 customerId, SqlString emailAddress) {
         CustomerId = customerId;
         EmailAddress = emailAddress;
      }
   }

   public static bool ValidateEmail(SqlString emailAddress) {
      if (emailAddress.IsNull)
         return false;

      if (!emailAddress.Value.EndsWith("@adventure-works.com"))
         return false;

      // Validate the address. Put any more rules here.
      return true;
   }

   [SqlFunction(
       DataAccess = DataAccessKind.Read,
       FillRowMethodName = "FindInvalidEmails_FillRow",
       TableDefinition="CustomerId int, EmailAddress nvarchar(4000)")]
   public static IEnumerable FindInvalidEmails(SqlDateTime modifiedSince) {
      ArrayList resultCollection = new ArrayList();

      using (SqlConnection connection = new SqlConnection("context connection=true")) {
         connection.Open();

         using (SqlCommand selectEmails = new SqlCommand(
             "SELECT " +
             "[CustomerID], [EmailAddress] " +
             "FROM [AdventureWorksLT2022].[SalesLT].[Customer] " +
             "WHERE [ModifiedDate] >= @modifiedSince",
             connection)) {
            SqlParameter modifiedSinceParam = selectEmails.Parameters.Add(
                "@modifiedSince",
                SqlDbType.DateTime);
            modifiedSinceParam.Value = modifiedSince;

            using (SqlDataReader emailsReader = selectEmails.ExecuteReader()) {
               while (emailsReader.Read()) {
                  SqlString emailAddress = emailsReader.GetSqlString(1);
                  if (ValidateEmail(emailAddress)) {
                     resultCollection.Add(new EmailResult(
                         emailsReader.GetSqlInt32(0),
                         emailAddress));
                  }
               }
            }
         }
      }

      return resultCollection;
   }

   public static void FindInvalidEmails_FillRow(
       object emailResultObj,
       out SqlInt32 customerId,
       out SqlString emailAddress) {
      EmailResult emailResult = (EmailResult)emailResultObj;

      customerId = emailResult.CustomerId;
      emailAddress = emailResult.EmailAddress;
   }
};
```

### [Visual Basic .NET](#tab/vb)

```vb
Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports Microsoft.SqlServer.Server

Public Partial Class UserDefinedFunctions
   Private Class EmailResult
      Public CustomerId As SqlInt32
      Public EmailAddress As SqlString

      Public Sub New(customerId__1 As SqlInt32, emailAddress__2 As SqlString)
         CustomerId = customerId__1
         EmailAddress = emailAddress__2
      End Sub
   End Class

   Public Shared Function ValidateEmail(emailAddress As SqlString) As Boolean
      If emailAddress.IsNull Then
         Return False
      End If

      If Not emailAddress.Value.EndsWith("@adventure-works.com") Then
         Return False
      End If

      ' Validate the address. Put any more rules here.
      Return True
   End Function

   <SqlFunction(DataAccess := DataAccessKind.Read, FillRowMethodName := "FindInvalidEmails_FillRow", TableDefinition := "CustomerId int, EmailAddress nvarchar(4000)")> _
   Public Shared Function FindInvalidEmails(modifiedSince As SqlDateTime) As IEnumerable
      Dim resultCollection As New ArrayList()

      Using connection As New SqlConnection("context connection=true")
         connection.Open()

         Using selectEmails As New SqlCommand("SELECT " & "[CustomerID], [EmailAddress] " & "FROM [AdventureWorksLT2022].[SalesLT].[Customer] " & "WHERE [ModifiedDate] >= @modifiedSince", connection)
            Dim modifiedSinceParam As SqlParameter = selectEmails.Parameters.Add("@modifiedSince", SqlDbType.DateTime)
            modifiedSinceParam.Value = modifiedSince

            Using emailsReader As SqlDataReader = selectEmails.ExecuteReader()
               While emailsReader.Read()
                  Dim emailAddress As SqlString = emailsReader.GetSqlString(1)
                  If ValidateEmail(emailAddress) Then
                     resultCollection.Add(New EmailResult(emailsReader.GetSqlInt32(0), emailAddress))
                  End If
               End While
            End Using
         End Using
      End Using

      Return resultCollection
   End Function

   Public Shared Sub FindInvalidEmails_FillRow(emailResultObj As Object, ByRef customerId As SqlInt32, ByRef emailAddress As SqlString)
      Dim emailResult As EmailResult = DirectCast(emailResultObj, EmailResult)

      customerId = emailResult.CustomerId
      emailAddress = emailResult.EmailAddress
   End Sub
End Class

Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports Microsoft.SqlServer.Server

Public Partial Class UserDefinedFunctions
   Private Class EmailResult
      Public CustomerId As SqlInt32
      Public EmailAddress As SqlString

      Public Sub New(customerId__1 As SqlInt32, emailAddress__2 As SqlString)
         CustomerId = customerId__1
         EmailAddress = emailAddress__2
      End Sub
   End Class

   Public Shared Function ValidateEmail(emailAddress As SqlString) As Boolean
      If emailAddress.IsNull Then
         Return False
      End If

      If Not emailAddress.Value.EndsWith("@adventure-works.com") Then
         Return False
      End If

      ' Validate the address. Put any more rules here.
      Return True
   End Function

   <SqlFunction(DataAccess := DataAccessKind.Read, FillRowMethodName := "FindInvalidEmails_FillRow", TableDefinition := "CustomerId int, EmailAddress nvarchar(4000)")> _
   Public Shared Function FindInvalidEmails(modifiedSince As SqlDateTime) As IEnumerable
      Dim resultCollection As New ArrayList()

      Using connection As New SqlConnection("context connection=true")
         connection.Open()

         Using selectEmails As New SqlCommand("SELECT " & "[CustomerID], [EmailAddress] " & "FROM [AdventureWorksLT2022].[SalesLT].[Customer] " & "WHERE [ModifiedDate] >= @modifiedSince", connection)
            Dim modifiedSinceParam As SqlParameter = selectEmails.Parameters.Add("@modifiedSince", SqlDbType.DateTime)
            modifiedSinceParam.Value = modifiedSince

            Using emailsReader As SqlDataReader = selectEmails.ExecuteReader()
               While emailsReader.Read()
                  Dim emailAddress As SqlString = emailsReader.GetSqlString(1)
                  If ValidateEmail(emailAddress) Then
                     resultCollection.Add(New EmailResult(emailsReader.GetSqlInt32(0), emailAddress))
                  End If
               End While
            End Using
         End Using
      End Using

      Return resultCollection
   End Function

   Public Shared Sub FindInvalidEmails_FillRow(emailResultObj As Object, customerId As SqlInt32, emailAddress As SqlString)
      Dim emailResult As EmailResult = DirectCast(emailResultObj, EmailResult)

      customerId = emailResult.CustomerId
      emailAddress = emailResult.EmailAddress
   End Sub
End Class
```

---

Compile the source code to a DLL and copy the DLL to the root directory of your C drive. Then, execute the following [!INCLUDE [tsql](../../includes/tsql-md.md)] query.

```sql
USE AdventureWorksLT2022;
GO

IF EXISTS (SELECT name
           FROM sysobjects
           WHERE name = 'FindInvalidEmails')
    DROP FUNCTION FindInvalidEmails;
GO

IF EXISTS (SELECT name
           FROM sys.assemblies
           WHERE name = 'MyClrCode')
    DROP ASSEMBLY MyClrCode;
GO

CREATE ASSEMBLY MyClrCode
    FROM 'C:\FindInvalidEmails.dll'
    WITH PERMISSION_SET = SAFE;
GO

CREATE FUNCTION FindInvalidEmails
(@ModifiedSince DATETIME)
RETURNS TABLE (
    CustomerId INT,
    EmailAddress NVARCHAR (4000))
AS EXTERNAL NAME MyClrCode.UserDefinedFunctions.[FindInvalidEmails];
GO

SELECT *
FROM FindInvalidEmails('2000-01-01');
GO
```
