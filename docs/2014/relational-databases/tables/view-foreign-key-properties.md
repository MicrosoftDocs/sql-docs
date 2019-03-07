---
title: "View Foreign Key Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "foreign keys [SQL Server], attributes"
  - "displaying foreign keys attributes"
  - "viewing foreign keys attributes"
ms.assetid: b0e57cb7-9b26-4b96-b76a-1f59f5f498c5
author: stevestein
ms.author: sstein
manager: craigg
---
# View Foreign Key Properties
  You can view the foreign key attributes of a relationship in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To view the foreign key attributes of a specific table, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view the foreign key attributes of a relationship in a specific table  
  
1.  Open the Table Designer for the table containing the foreign key you want to view, right-click in the Table Designer, and choose **Relationships** from the shortcut menu.  
  
2.  In the **Foreign Key Relationships** dialog box, select the relationship with properties you want to view.  
  
 If the foreign key columns are related to a primary key, the primary key columns are identified in **Table Designer** by a primary key symbol in the row selector.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view the foreign key attributes of a relationship in a specific table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example returns all foreign keys and their properties for the table `HumanResources.Employee` in the sample database.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    SELECT   
        f.name AS foreign_key_name  
       ,OBJECT_NAME(f.parent_object_id) AS table_name  
       ,COL_NAME(fc.parent_object_id, fc.parent_column_id) AS constraint_column_name  
       ,OBJECT_NAME (f.referenced_object_id) AS referenced_object  
       ,COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS referenced_column_name  
       ,is_disabled  
       ,delete_referential_action_desc  
       ,update_referential_action_desc  
    FROM sys.foreign_keys AS f  
    INNER JOIN sys.foreign_key_columns AS fc   
       ON f.object_id = fc.constraint_object_id   
    WHERE f.parent_object_id = OBJECT_ID('HumanResources.Employee');  
    ```  
  
 For more information, see [sys.foreign_keys &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-foreign-keys-transact-sql) and [sys.foreign_key_columns &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-foreign-key-columns-transact-sql).  
  
###  <a name="TsqlExample"></a>  
