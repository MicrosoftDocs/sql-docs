---
title: "Delete foreign key relationships"
description: "Learn more about how to delete foreign key from tables in the SQL Server Database Engine."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 08/28/2023
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "foreign keys [SQL Server], deleting"
  - "removing foreign keys"
  - "deleting foreign keys"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Delete foreign key relationships

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

You can delete a foreign key constraint in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. Deleting a foreign key constraint removes the requirement to enforce referential integrity.

Foreign keys reference keys in other tables, for more information, see [Primary and Foreign Key Constraints](primary-and-foreign-key-constraints.md).
  
## <a id="Permissions"></a> Permissions
 Requires ALTER permission on the table.  
  
## <a id="SSMSProcedure"></a> Use SQL Server Management Studio
  
### To delete a foreign key constraint
  
1. In **Object Explorer**, expand the table with the constraint and then expand **Keys**.  
  
1. Right-click the constraint and then select **Delete**.  
  
1. In the **Delete Object** dialog box, select **OK**.  

## <a id="TsqlProcedure"></a> Use Transact-SQL
  
### To delete a foreign key constraint
  
1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].  
  
1. On the Standard bar, select **New Query**.  
  
1. Copy and paste the following example into the query window and select **Execute**.  
  
    ```sql
    USE AdventureWorks2022;
    GO
    ALTER TABLE dbo.DocExe
    DROP CONSTRAINT FK_Column_B;
    GO
    ```  
  
 For more information, see [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md).  

## Next steps

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [sys.key_constraints (Transact-SQL)](../../relational-databases/system-catalog-views/sys-key-constraints-transact-sql.md)
- [Create Foreign Key Relationships](create-foreign-key-relationships.md)
- [Modify Foreign Key Relationships](modify-foreign-key-relationships.md)
