---
title: "Transaction and bulk copy operations"
description: "Describes how to perform a bulk copy operation within a transaction, including how to commit or rollback the transaction."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "08/15/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Transaction and bulk copy operations

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Bulk copy operations can be performed as isolated operations or as part of a multiple step transaction. This latter option enables you to perform more than one bulk copy operation within the same transaction, as well as perform other database operations such as inserts, updates, and deletes, while still being able to commit or roll back the entire transaction.  
  
By default, a bulk copy operation is done as an isolated operation. The bulk copy operation occurs in a non-transacted way, with no opportunity for rolling it back. If you need to roll back all or part of the bulk copy when an error occurs, you can:

 - Use a <xref:Microsoft.Data.SqlClient.SqlBulkCopy>-managed transaction

 - Do the bulk copy operation within an existing transaction

 - Enlist in a **System.Transactions** <xref:System.Transactions.Transaction>.  
  
## Performing a non-transacted bulk copy operation  
The following Console application shows what happens when a non-transacted bulk copy operation encounters an error partway through the operation.  
  
In the example, the source table and destination table each include an `Identity` column named **ProductID**. The code first prepares the destination table by deleting all rows and then inserting a single row whose **ProductID** is known to exist in the source table. By default, a new value for the `Identity` column is generated in the destination table for each row added. In this example, an option is set when the connection is opened that forces the bulk load process to use the `Identity` values from the source table instead.  
  
The bulk copy operation is executed with the <xref:Microsoft.Data.SqlClient.SqlBulkCopy.BatchSize%2A> property set to 10. When the operation comes across the invalid row, an exception is thrown. In this first example, the bulk copy operation is non-transacted. All batches that are copied up to the point of the error are committed. The batch containing the duplicate key is rolled back, and the bulk copy operation is halted before it processes any remaining batches.  
  
> [!NOTE]
>  This sample will not run unless you have created the work tables as described in [Bulk copy example setup](bulk-copy-example-setup.md). This code is provided to demonstrate the syntax for using **SqlBulkCopy** only. If the source and destination tables are located in the same SQL Server instance, it is easier and faster to use a Transact-SQL`INSERT … SELECT` statement to copy the data.  
  
[!code-csharp[DataWorks SqlBulkCopyOptions_Default#1](~/../sqlclient/doc/samples/SqlBulkCopyOptions_Default.cs#1)]
  
## Performing a dedicated bulk copy operation in a transaction  
By default, a bulk copy operation is its own transaction. When you want to perform a dedicated bulk copy operation, create an instance of <xref:Microsoft.Data.SqlClient.SqlBulkCopy> with a connection string, or use an existing <xref:Microsoft.Data.SqlClient.SqlConnection> object without an active transaction. The bulk copy operation creates a transaction in each scenario and then commits or rolls back the transaction.  
  
You can explicitly specify the <xref:Microsoft.Data.SqlClient.SqlBulkCopyOptions.UseInternalTransaction> option in the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class constructor to execute a bulk copy operation in its own transaction. Each batch of the operation will execute within a separate transaction.  
  
> [!NOTE]
>  Since different batches are executed in different transactions, if an error occurs during the bulk copy operation, all the rows in the current batch will be rolled back, but rows from previous batches will remain in the database.  
  
The following console application is similar to the previous example, with one exception: In this example, the bulk copy operation manages its own transactions. All batches that are copied up to the point of the error are committed. The batch containing the duplicate key is rolled back, and the bulk copy operation is halted before it processes any remaining batches. 
  
> [!IMPORTANT]
>  This sample will not run unless you have created the work tables as described in [Bulk copy example setup](bulk-copy-example-setup.md). This code is provided to demonstrate the syntax for using **SqlBulkCopy** only. If the source and destination tables are located in the same SQL Server instance, it is easier and faster to use a Transact-SQL`INSERT … SELECT` statement to copy the data.  
  
[!code-csharp[DataWorks SqlBulkCopyOptions_UseInternalTransaction#1](~/../sqlclient/doc/samples/SqlBulkCopyOptions_UseInternalTransaction.cs#1)]
  
## Using existing transactions  
You can specify an existing <xref:Microsoft.Data.SqlClient.SqlTransaction> object as a parameter in a <xref:Microsoft.Data.SqlClient.SqlBulkCopy> constructor. In this situation, the bulk copy operation is done in the existing transaction, and no change is made to the transaction state - it is not committed or aborted. This method allows an application to include the bulk copy operation in a transaction with other database operations. However, if you do not specify a <xref:Microsoft.Data.SqlClient.SqlTransaction> object and pass a null reference, and the connection has an active transaction, an exception is thrown.   
  
If you need to roll back the entire bulk copy operation because an error occurs, or if the bulk copy should execute as part of a larger process that can be rolled back, you can provide a <xref:Microsoft.Data.SqlClient.SqlTransaction> object to the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> constructor.  
  
The following console application is similar to the first (non-transacted) example, with one exception: in this example, the bulk copy operation is included in a larger, external transaction. When the primary key violation error occurs, the entire transaction is rolled back and no rows are added to the destination table.  
  
> [!IMPORTANT]
>  This sample will not run unless you have created the work tables as described in [Bulk copy example setup](bulk-copy-example-setup.md). This code is provided to demonstrate the syntax for using **SqlBulkCopy** only. If the source and destination tables are located in the same SQL Server instance, it is easier and faster to use a Transact-SQL`INSERT … SELECT` statement to copy the data.  
  
[!code-csharp[DataWorks SqlBulkCopy_ExternalTransaction#1](~/../sqlclient/doc/samples/SqlBulkCopy_ExternalTransaction.cs#1)]
  
## Next steps
- [Bulk copy operations in SQL Server](bulk-copy-operations-sql-server.md)
