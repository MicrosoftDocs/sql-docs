---
description: "Create a duplicate copy of a table, without the row data."
title: "Duplicate tables without the row data."
ms.custom: ""
ms.date: "10/21/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "copying tables"
  - "tables [SQL Server], duplicating"
  - "duplicating tables"
  - "duplicating table structures"
  - "table copying [SQL Server]"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Duplicate tables
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

You can duplicate an existing table in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)] by creating a new table and then copying column information from an existing table.  
  
 These steps described duplicate only the structure of a table, not the row data.  

##  <a name="Permissions"></a><a name="Security"></a> Permissions  
 Requires CREATE TABLE permission in the destination database.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
#### To duplicate a table  
  
1.  Make sure you are connected to the database in which you want to create the table and that the database is selected in Object Explorer.  
  
2.  In Object Explorer, right-click **Tables** and select **New Table**.  
  
3.  In Object Explorer right-click the table you want to copy and select **Design**.  
  
4.  Select the columns in the existing table and, from the **Edit** menu, select **Copy**.  
  
5.  Switch back to the new table and select the first row.  
  
6.  From the **Edit** menu, select **Paste**.  
  
7.  From the **File** menu, select **Save** _table name_.  
  
8.  In the **Choose Name** dialog box, type a name for the new table. Select **OK**.  

##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
#### To duplicate a table in Query Editor  
  
1.  Make sure you are connected to the database in which you want to create the table and that the database is selected in Object Explorer.  
  
2.  Right-click the table you wish to duplicate, point to **Script Table as**, then point to **CREATE to**, and then select **New Query Editor Window**.  
  
3.  Change the name of the table.  
  
4.  Remove any columns that are not needed in the new table.  
  
5.  Select **Execute** to create the new table.

## Next steps

- [Copy Columns from One Table to Another (Database Engine)](copy-columns-from-one-table-to-another-database-engine.md)
- [Script objects in SQL Server Management Studio](../../ssms/tutorials/scripting-ssms.md)

