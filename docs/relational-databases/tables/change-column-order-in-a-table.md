---
title: "Change Column Order in a Table"
description: "Change Column Order in a Table"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/18/2023
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "columns [SQL Server], change order in a table"
  - "column order, change"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Change Column Order in a Table

[!INCLUDE [sqlserver2016-asdb-asdbmi-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-pdw.md)]

You can change the order of columns in **Table Designer** in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS). By default, a safety mechanism of SSMS blocks changing the column order. Though it isn't recommended, you can change the column order in a table by re-creating the table. 

[Adding columns to a table](add-columns-to-a-table-database-engine.md) by default adds them to the end of the table, as is recommended.

## Recommendations

 Best practices with table column order:

 - To change the order of columns displayed in a result set, report, or application, use the column order in a [SELECT (Transact-SQL)](../../t-sql/queries/select-transact-sql.md) statement. Always specify the columns by name in your queries and applications in the order in which you would like them to appear.
 - Don't use `SELECT *` in applications. Added or removed columns could cause unexpected behavior or errors in applications.
 - Add new columns to the end of tables.

> [!CAUTION]  
> Changing the column order of a table may affect code and applications that depend on the specific order of columns. These include queries, views, stored procedures, user-defined functions, and client applications. Carefully consider any changes you want to make to column order.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

#### Change the column order

Though not recommended, you can change the order of columns in a table using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS). This requires recreating the table.

> [!IMPORTANT]
> Always use the latest version of [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).

1. In **Object Explorer**, right-click the table with columns you want to reorder and select **Design**.  
  
1. Select the box to the left of the column name that you want to reorder.  
  
1. Drag the column to another location within the table.  

You may be blocked making these changes by an important safety feature of SSMS, controlled by the setting **Prevent saving changes that require table re-creation**. This setting is enabled to prevent accidental drop/recreate of the table via SSMS dialogues, which may be a disruptive and result in the loss of metadata or permissions. For more information, see ["Saving changes is not permitted" error message in SSMS](/troubleshoot/sql/ssms/error-when-you-save-table). Instead, it's recommended you execute these type of changes, with full awareness of their impact, via Transact-SQL steps that account for permissions and metadata.

> [!CAUTION]
> Re-creating a table will block concurrent access to the table for other users and applications. For large tables, this could require a long duration and a large amount of transaction log space.
  
## <a id="TsqlProcedure"></a> Use Transact-SQL
 
 Changing column order isn't supported using Transact-SQL statements. The table must be dropped and recreated in order to change column order. 

## Remarks

To query existing columns, use the [sys.columns](../system-catalog-views/sys-columns-transact-sql.md) object catalog view.

## Next steps

- [Tables](tables.md)
- [Adding columns to a table](add-columns-to-a-table-database-engine.md)
- [Delete columns from a table](delete-columns-from-a-table.md)