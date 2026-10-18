---
title: "Access the Current Transaction"
description: In SQL Server CLR integration, the Current property of the System.Transactions.Transaction class allows you to access the current transaction.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "current transaction access"
  - "Current property"
  - "Transaction class"
---
# Access the current transaction

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

If a transaction is active at the point at which common language runtime (CLR) code running on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is entered, the transaction is exposed through the `System.Transactions.Transaction` class. The `Transaction.Current` property is used to access the current transaction. In most cases, it's not necessary to access the transaction explicitly. For database connections, ADO.NET checks `Transaction.Current` automatically when the `Connection.Open` method is called, and transparently enlists the connection in that transaction (unless the `Enlist` keyword is set to false in the connection string).

You might want to use the `Transaction` object directly in the following scenarios:

- If you want to enlist a resource that doesn't do automatic enlistment, or that for some reason wasn't enlisted during initialization.

- If you want to explicitly enlist a resource in the transaction.

- If you want to terminate the external transaction from within your stored procedure or function. In this case, you use <xref:System.Transactions.TransactionScope>. For example, the following code rolls back the current transaction:

  ```csharp
  using(TransactionScope transactionScope = new TransactionScope(TransactionScopeOptions.Required)) { }
  ```

The rest of this article describes other ways to cancel an external transaction.

## Cancel an external transaction

You can cancel external transactions from a managed procedure or function in the following ways:

- The managed procedure or function can return a value by using an output parameter. The calling [!INCLUDE [tsql](../../includes/tsql-md.md)] procedure can check the returned value and, if appropriate, execute `ROLLBACK TRANSACTION`.

- The managed procedure or function can throw a custom exception. The calling [!INCLUDE [tsql](../../includes/tsql-md.md)] procedure can catch the exception thrown by the managed procedure or function in a try/catch block and execute `ROLLBACK TRANSACTION`.

- The managed procedure or function can cancel the current transaction by calling the `Transaction.Rollback` method if a certain condition is met.

When the `Transaction.Rollback` method is called within a managed procedure or function, it throws an exception with an ambiguous error message and can be wrapped in a try/catch block. The error message is similar to the following output:

```output
Msg 3994, Level 16, State 1, Procedure uspRollbackFromProc, Line 0
Transaction is not allowed to roll back inside a user defined routine, trigger or aggregate because the transaction is not started in that CLR level. Change application logic to enforce strict transaction nesting.
```

This exception is expected and the try/catch block is necessary for code execution to continue. Without the try/catch block, the exception is immediately thrown to the calling [!INCLUDE [tsql](../../includes/tsql-md.md)] procedure and managed code execution finishes. When the managed code finishes execution, another exception is raised:

```output
Msg 3991, Level 16, State 1, Procedure uspRollbackFromProc, Line 1
The context transaction which was active before entering user defined routine, trigger or aggregate " uspRollbackFromProc " has been ended inside of it, which is not allowed. Change application logic to enforce strict transaction nesting. The statement has been terminated.
```

This exception is also expected, and for execution to continue, you must have a try/catch block around the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement that performs the action that fires the trigger. Despite the two exceptions thrown, the transaction is rolled back and the changes aren't committed.

### Example

The following code is an example of a transaction being rolled back from a managed procedure by using the `Transaction.Rollback` method. Notice the try/catch block around the `Transaction.Rollback` method in the managed code. The [!INCLUDE [tsql](../../includes/tsql-md.md)] script creates an assembly and managed stored procedure. The `EXEC uspRollbackFromProc` statement is wrapped in a try/catch block, so that the exception thrown when the managed procedure completes execution is caught.

### [C#](#tab/csharp)

```csharp
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Transactions;

public partial class StoredProcedures
{
[Microsoft.SqlServer.Server.SqlProcedure]
public static void uspRollbackFromProc()
{
   using (SqlConnection connection = new SqlConnection(@"context connection=true"))
   {
      // Open the connection.
      connection.Open();

      bool successCondition = true;

      // Success condition is met.
      if (successCondition)
      {
         SqlContext.Pipe.Send("Success condition met in procedure.");
         // Perform other actions here.
      }

      //  Success condition is not met, the transaction will be rolled back.
      else
      {
         SqlContext.Pipe.Send("Success condition not met in managed procedure. Transaction rolling back...");
         try
         {
               // Get the current transaction and roll it back.
               Transaction trans = Transaction.Current;
               trans.Rollback();
         }
         catch (SqlException ex)
         {
            // Catch the expected exception.
            // This allows the connection to close correctly.
         }
      }

      // Close the connection.
      connection.Close();
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
Imports System.Transactions

Partial Public Class StoredProcedures
<Microsoft.SqlServer.Server.SqlProcedure()> _
Public Shared Sub  uspRollbackFromProc ()
   Using connection As New SqlConnection("context connection=true")

   ' Open the connection.
   connection.Open()

   Dim successCondition As Boolean
   successCondition = False

   ' Success condition is met.
   If successCondition Then

      SqlContext.Pipe.Send("Success condition met in procedure.")

      ' Success condition is not met, the transaction will be rolled back.

   Else
      SqlContext.Pipe.Send("Success condition not met in managed procedure. Transaction rolling back...")
      Try
         ' Get the current transaction and roll it back.
         Dim trans As Transaction
         trans = Transaction.Current
         trans.Rollback()

      Catch ex As SqlException
         ' Catch the exception instead of throwing it.
         ' This allows the connection to close correctly.
      End Try

   End If

   ' Close the connection.
   connection.Close()

End Using
End Sub
End Class
```

---

#### Register and execute the assembly in Transact-SQL

1. Register the assembly.

   ```sql
   CREATE ASSEMBLY TestProcs
       FROM 'C:\Programming\TestProcs.dll';
   GO

   CREATE PROCEDURE uspRollbackFromProc
   AS EXTERNAL NAME TestProcs.StoredProcedures.uspRollbackFromProc;
   GO
   ```

1. Run the procedure.

   ```sql
   BEGIN TRY
       BEGIN TRANSACTION;

       -- Perform other actions.
       EXECUTE uspRollbackFromProc;

       -- Perform other actions.
       PRINT N'Commiting transaction...';

       COMMIT TRANSACTION;
   END TRY
   BEGIN CATCH
       SELECT ERROR_NUMBER() AS ErrorNum,
              ERROR_MESSAGE() AS ErrorMessage;
       PRINT N'Exception thrown, rolling back transaction.';
       ROLLBACK;
       PRINT N'Transaction rolled back.';
   END CATCH
   GO
   ```

1. Clean up your environment.

   ```sql
   DROP PROCEDURE uspRollbackFromProc;
   GO

   DROP ASSEMBLY TestProcs;
   GO
   ```

## Related content

- [CLR integration and transactions](clr-integration-and-transactions.md)
