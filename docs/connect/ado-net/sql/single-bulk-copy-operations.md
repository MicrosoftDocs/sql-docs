---
title: "Single bulk copy operations"
description: "Describes how to do a single bulk copy of data into an instance of SQL Server using the SqlBulkCopy class, and how to perform the bulk copy operation using Transact-SQL statements and the SqlCommand class."
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
# Single bulk copy operations

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

The simplest approach to performing a SQL Server bulk copy operation is to perform a single operation against a database. By default, a bulk copy operation is performed as an isolated operation: the copy operation occurs in a non-transacted way, with no opportunity for rolling it back.  
  
> [!NOTE]
>  If you need to roll back all or part of the bulk copy when an error occurs, you can either use a <xref:Microsoft.Data.SqlClient.SqlBulkCopy>-managed transaction, or perform the bulk copy operation within an existing transaction. **SqlBulkCopy** will also work with <xref:System.Transactions> if the connection is enlisted (implicitly or explicitly) into a **System.Transactions** transaction.  
>   
>  For more information, see [Transaction and bulk copy operations](transaction-bulk-copy-operations.md).  
  
The general steps for performing a bulk copy operation are as follows:  
  
1. Connect to the source server and obtain the data to be copied. Data can also come from other sources, if it can be retrieved from an <xref:System.Data.IDataReader> or <xref:System.Data.DataTable> object.  
  
2. Connect to the destination server (unless you want **SqlBulkCopy** to establish a connection for you).  
  
3. Create a <xref:Microsoft.Data.SqlClient.SqlBulkCopy> object, setting any necessary properties.  
  
4. Set the **DestinationTableName** property to indicate the target table for the bulk insert operation.  
  
5. Call one of the **WriteToServer** methods.  
  
6. Optionally, update properties and call **WriteToServer** again as necessary.  
  
7. Call <xref:Microsoft.Data.SqlClient.SqlBulkCopy.Close%2A>, or wrap the bulk copy operations within a `Using` statement.  
  
> [!CAUTION]
>  We recommend that the source and target column data types match. If the data types do not match, **SqlBulkCopy** attempts to convert each source value to the target data type, using the rules employed by <xref:Microsoft.Data.SqlClient.SqlParameter.Value%2A>. Conversions can affect performance, and also can result in unexpected errors. For example, a `Double` data type can be converted to a `Decimal` data type most of the time, but not always.  
  
## Example  
The following console application demonstrates how to load data using the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class. In this example, a <xref:Microsoft.Data.SqlClient.SqlDataReader> is used to copy data from the **Production.Product** table in the SQL Server **AdventureWorks** database to a similar table in the same database.  
  
> [!IMPORTANT]
>  This sample will not run unless you have created the work tables as described in [Bulk copy example setup](bulk-copy-example-setup.md). This code is provided to demonstrate the syntax for using **SqlBulkCopy** only. If the source and destination tables are located in the same SQL Server instance, it is easier and faster to use a Transact-SQL `INSERT … SELECT` statement to copy the data.  
  
[!code-csharp[DataWorks SqlBulkCopy_WriteToServer#1](~/../sqlclient/doc/samples/SqlBulkCopy_WriteToServer.cs#1)]
  
## Performing a bulk copy operation using transact-SQL and the command class  
The following example illustrates how to use the <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteNonQuery%2A> method to execute the BULK INSERT statement.  
  
> [!NOTE]
>  The file path for the data source is relative to the server. The server process must have access to that path in order for the bulk copy operation to succeed.  
  
```csharp  
using (SqlConnection connection = New SqlConnection(connectionString))  
{  
string queryString =  "BULK INSERT Northwind.dbo.[Order Details] " +  
    "FROM 'f:\mydata\data.tbl' " +  
    "WITH ( FORMATFILE='f:\mydata\data.fmt' )";  
connection.Open();  
SqlCommand command = new SqlCommand(queryString, connection);  
  
command.ExecuteNonQuery();  
}  
```  
  
## Next steps
- [Bulk copy operations in SQL Server](bulk-copy-operations-sql-server.md)
