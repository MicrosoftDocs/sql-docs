---
title: "View the Table Definition | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "showing table properties"
  - "displaying table properties"
  - "tables [SQL Server], properties"
  - "viewing table properties"
ms.assetid: 1865fb7c-f480-4100-9007-df5364cd002a
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View the Table Definition
[!INCLUDE[tsql-appliesto-ss2016-all-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

  You can display properties for a table in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To display table properties, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You can only see properties in a table if you either own the table or have been granted permissions to that table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To show table properties in the Properties window  
  
1.  In Object Explorer, select the table for which you want to show properties.  
  
2.  Right-click the table and choose **Properties** from the shortcut menu. For more information, see [Table Properties - SSMS](../../relational-databases/tables/table-properties-ssms.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To show table properties  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example returns all columns from the `sys.tables` catalog view for the specified object.  
  
    ```  
    SELECT * FROM sys.tables  
    WHERE object_id = 1973582069;  
  
    ```  
  
 For more information, see [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md).  
  
###  <a name="TsqlExample"></a>  
