---
title: "Change Column Order in a Table | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "columns [SQL Server], change order in a table"
  - "column order, change"
ms.assetid: cd99ef56-9085-431a-a0fc-58e7add5399f
author: stevestein
ms.author: sstein
manager: craigg
---
# Change Column Order in a Table
  You can change the order of columns in Table Designer in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
> [!CAUTION]  
>  Changing the column order of a table may affect code and applications that depend on the specific order of columns. These include queries, views, stored procedures, user-defined functions, and client applications. Carefully consider any changes you want to make to column order before making it. Best practice is to specify the order in which the columns are returned at the application and query level. You should not rely on the use of SELECT * to return all columns in an expected order based on the order in which they are defined in the table. Always specify the columns by name in your queries and applications in the order in which you would like them to appear.  
  
 **In This Topic**  
  
-   **To change the column order, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To change the column order  
  
1.  In **Object Explorer**, right-click the table with columns you want to reorder and click **Design**.  
  
2.  Select the box to the left of the column name that you want to reorder.  
  
3.  Drag the column to another location within the table.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To change the column order**  
  
 This task cannot be performed using Transact-SQL statements.  
  
###  <a name="TsqlExample"></a>  
