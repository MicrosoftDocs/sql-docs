---
description: "You can view the foreign key attributes of a relationship with SQL Server Management Studio or T-SQL queries."
title: "View Foreign Key Properties"
ms.custom: ""
ms.date: "04/13/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "foreign keys [SQL Server], attributes"
  - "displaying foreign keys attributes"
  - "viewing foreign keys attributes"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View foreign key properties
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

  You can view the foreign key attributes of a relationship in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  

###  <a name="Permissions"></a> Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view the foreign key attributes of a relationship in a specific table  
  
1.  Open the Table Designer for the table containing the foreign key you want to view, right-click in the Table Designer, and choose **Relationships** from the shortcut menu.  
  
2.  In the **Foreign Key Relationships** dialog box, select the relationship with properties you want to view.  

 If the foreign key columns are related to a primary key, the primary key columns are identified in **Table Designer** by a primary key symbol in the row selector.  

<a name="TsqlExample"></a>  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view the foreign key attributes of a relationship in a specific table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. The example returns all foreign keys and their properties for the table `HumanResources.Employee` in the sample database.  
  
    ```sql  
    USE AdventureWorks2012;  
    GO  
    SELECT   
        f.name AS foreign_key_name  
       ,OBJECT_NAME(f.parent_object_id) AS table_name  
       ,COL_NAME(fc.parent_object_id, fc.parent_column_id) AS constraint_column_name  
       ,OBJECT_NAME (f.referenced_object_id) AS referenced_object  
       ,COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS referenced_column_name  
       ,f.is_disabled, f.is_not_trusted
       ,f.delete_referential_action_desc  
       ,f.update_referential_action_desc  
    FROM sys.foreign_keys AS f  
    INNER JOIN sys.foreign_key_columns AS fc   
       ON f.object_id = fc.constraint_object_id   
    WHERE f.parent_object_id = OBJECT_ID('HumanResources.Employee');  
    ```  
  
 For more information, see [sys.foreign_keys &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-foreign-keys-transact-sql.md) and [sys.foreign_key_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-foreign-key-columns-transact-sql.md).  

## Next steps

- [ALTER TABLE Statement](../../odbc/microsoft/alter-table-statement.md)
- [Disable foreign key constraints for replication](disable-foreign-key-constraints-for-replication.md)
- [Disable Foreign Key Constraints with INSERT and UPDATE Statements](disable-foreign-key-constraints-with-insert-and-update-statements.md)