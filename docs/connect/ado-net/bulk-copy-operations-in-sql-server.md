---
title: "Bulk Copy Operations in SQL Server"
ms.date: "08/15/2019"
ms.assetid: 83a7a0d2-8018-4354-97b9-0b1d99f8342b
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: MightyPen
ms.author: genemi
---
# Bulk Copy Operations in SQL Server

![Download-DownArrow-Circled](../../ssdt/media/download.png)[Download ADO.NET](../sql-connection-libraries.md#anchor-20-drivers-relational-access)

Microsoft SQL Server includes a popular command-line utility named **bcp** for quickly bulk copying large files into tables or views in SQL Server databases. The <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class allows you to write managed code solutions that provide similar functionality. There are other ways to load data into a SQL Server table (INSERT statements, for example) but <xref:Microsoft.Data.SqlClient.SqlBulkCopy> offers a significant performance advantage over them.  
  
 Using the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class, you can perform:  
  
- A single bulk copy operation  
  
- Multiple bulk copy operations  
  
- A bulk copy operation within a transaction  
  
> [!NOTE]
>  When using .NET Framework version 1.1 or earlier (which does not support the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class), you can execute the SQL Server Transact-SQL **BULK INSERT** statement using the <xref:Microsoft.Data.SqlClient.SqlCommand> object.  
  
## In This Section  
 [Bulk Copy Example Setup](bulk-copy-example-setup.md)  
 Describes the tables used in the bulk copy examples and provides SQL scripts for creating the tables in the AdventureWorks database.  
  
 [Single Bulk Copy Operations](single-bulk-copy-operations.md)  
 Describes how to do a single bulk copy of data into an instance of SQL Server using the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class, and how to perform the bulk copy operation using Transact-SQL statements and the <xref:Microsoft.Data.SqlClient.SqlCommand> class.  
  
 [Multiple Bulk Copy Operations](multiple-bulk-copy-operations.md)  
 Describes how to do multiple bulk copy operations of data into an instance of SQL Server using the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class.  
  
 [Transaction and Bulk Copy Operations](transaction-and-bulk-copy-operations.md)  
 Describes how to perform a bulk copy operation within a transaction, including how to commit or rollback the transaction.  
  
## See also

- [SQL Server and ADO.NET](index.md)
