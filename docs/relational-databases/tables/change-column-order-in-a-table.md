---
description: "Change Column Order in a Table"
title: "Change Column Order in a Table"
ms.custom: ""
ms.date: "01/14/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "columns [SQL Server], change order in a table"
  - "column order, change"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Change Column Order in a Table

[!INCLUDE [sqlserver2016-asdb-asdbmi-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-pdw.md)]

  You can change the order of columns in Table Designer in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
> [!CAUTION]  
>  Changing the column order of a table may affect code and applications that depend on the specific order of columns. These include queries, views, stored procedures, user-defined functions, and client applications. Carefully consider any changes you want to make to column order before making it. Best practice is to specify the order in which the columns are returned at the application and query level. You should not rely on the use of SELECT * to return all columns in an expected order based on the order in which they are defined in the table. Always specify the columns by name in your queries and applications in the order in which you would like them to appear.  
  
 **In This Topic**  
  
-   **To change the column order, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  

#### To change the column order  
  
1.  In **Object Explorer**, right-click the table with columns you want to reorder and select **Design**.  
  
2.  Select the box to the left of the column name that you want to reorder.  
  
3.  Drag the column to another location within the table.  

You may be blocked making these changes by an important safety feature of SSMS, controlled by the setting **Prevent saving changes that require table re-creation**. This setting is enabled to prevent accidental drop/recreate of the table via SSMS dialogues, which may be a disruptive and result in the loss of metadata or permissions. For more information, see [Saving changes is not permitted error message in SSMS](/troubleshoot/sql/ssms/error-when-you-save-table). Instead, it is recommended you execute these type of changes, with full awareness of their impact, via Transact-SQL steps that account for permissions and metadata.
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To change the column order**  
  
 This task is not supported using Transact-SQL statements. The table must be dropped and recreated in order to change column order. 
  
###  <a name="TsqlExample"></a>  
