---
title: "Duplicate Tables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "copying tables"
  - "tables [SQL Server], duplicating"
  - "duplicating tables"
  - "table copying [SQL Server]"
ms.assetid: c6b07423-d1e5-4e5e-8681-5088921f5df3
author: stevestein
ms.author: sstein
manager: craigg
---
# Duplicate Tables
  You can duplicate an existing table in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)] by creating a new table and then copying column information from an existing table.  
  
> [!IMPORTANT]  
>  This operation duplicates only the structure of a table; it does not duplicate any table rows.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To duplicate a table, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires CREATE TABLE permission in the destination database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To duplicate a table  
  
1.  Make sure you are connected to the database in which you want to create the table and that the database is selected in Object Explorer.  
  
2.  In Object Explorer, right-click **Tables** and click **New Table**.  
  
3.  In Object Explorer right-click the table you want to copy and click **Design**.  
  
4.  Select the columns in the existing table and, from the **Edit** menu, click **Copy**.  
  
5.  Switch back to the new table and select the first row.  
  
6.  From the **Edit** menu, click **Paste**.  
  
7.  From the **File** menu, click **Save**_table name_.  
  
8.  In the **Choose Name** dialog box, type a name for the new table and click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To duplicate a table in Query Editor  
  
1.  Make sure you are connected to the database in which you want to create the table and that the database is selected in Object Explorer.  
  
2.  Right-click the table you wish to duplicate, point to **Script Table as**, then point to **CREATE to**, and then select **New Query Editor Window**.  
  
3.  Change the name of the table.  
  
4.  Remove any columns that are not needed in the new table.  
  
5.  Click **Execute**.  
  
  
