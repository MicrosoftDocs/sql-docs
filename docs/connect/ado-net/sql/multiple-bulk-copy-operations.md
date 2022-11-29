---
title: "Multiple bulk copy operations"
description: "Describes how to do multiple bulk copy operations of data into an instance of SQL Server using the SqlBulkCopy class."
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
# Multiple bulk copy operations

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

You can perform multiple bulk copy operations using a single instance of a <xref:Microsoft.Data.SqlClient.SqlBulkCopy> class. If the operation parameters change between copies (for example, the name of the destination table), you must update them prior to any subsequent calls to any of the **WriteToServer** methods, as demonstrated in the following example. Unless explicitly changed, all property values remain the same as they were on the previous bulk copy operation for a given instance.  
  
> [!NOTE]
>  Performing multiple bulk copy operations using the same instance of <xref:Microsoft.Data.SqlClient.SqlBulkCopy> is usually more efficient than using a separate instance for each operation.  
  
If you perform several bulk copy operations using the same <xref:Microsoft.Data.SqlClient.SqlBulkCopy> object, there are no restrictions on whether source or target information is equal or different in each operation. However, you must ensure that column association information is properly set each time you write to the server.  

## Example

> [!IMPORTANT]
>  This sample will not run unless you have created the work tables as described in [Bulk copy example setup](bulk-copy-example-setup.md). This code is provided to demonstrate the syntax for using **SqlBulkCopy** only. If the source and destination tables are located in the same SQL Server instance, it is easier and faster to use a Transact-SQL `INSERT … SELECT` statement to copy the data.  
  
[!code-csharp[DataWorks SqlBulkCopy_._ColumnMappingOrdersDetails#1](~/../sqlclient/doc/samples/SqlBulkCopy_ColumnMappingOrdersDetails.cs#1)]
  
## Next steps
- [Bulk copy operations in SQL Server](bulk-copy-operations-sql-server.md)
