---
description: "Disable Foreign Key Constraints for Replication"
title: "Disable Foreign Key Constraints for Replication"
ms.custom: ""
ms.date: "10/21/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "constraints [SQL Server], foreign keys"
  - "foreign keys [SQL Server], disabling constraints"
  - "disabling constraints"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Disable foreign key constraints for replication
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

  You can disable foreign key constraints for replication in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. This can be useful if you are publishing data from a previous version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
 
> [!NOTE]  
>  If a table is published using replication, foreign key constraints are automatically disabled for operations performed by replication agents. The NOT FOR REPLICATION option is specified by default for foreign key constraints and check constraints; the constraints are enforced for user operations but not agent operations. When a replication agent performs an insert, update, or delete at a Subscriber, the constraint is not checked; if a user performs an insert, update, or delete, the constraint is checked. The constraint is disabled for the replication agent because the constraint was already checked at the Publisher when the data was originally inserted, updated, or deleted.  
  
## <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
#### To disable a foreign key constraint for replication  
  
1.  In **Object Explorer**, expand the table with the foreign key constraint you want to modify, and then expand the **Keys** folder.  
  
2.  Right-click the foreign key constraint and then select **Modify**.  
  
3.  In the **Foreign Key Relationships** dialog box, select a value of **No** for **Enforce For Replication**.  
  
4.  Select **Close**.  

##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
#### To disable a foreign key constraint for replication  
  
1.  To perform this task in [!INCLUDE[tsql](../../includes/tsql-md.md)], script out the foreign key constraint. In **Object Explorer**, expand the table with the foreign key constraint you want to modify, and then expand the **Keys** folder.  

2. Right-click the foreign key constraint, select **Script Key As**, then select **DROP and CREATE To**, then select **New Query Editor Window**. The resulting script should look similar the following example from the `AdventureWorks2019` sample database:

    ```sql
    ALTER TABLE [Sales].[SalesTerritoryHistory] 
    DROP CONSTRAINT [FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID]
    GO
    
    ALTER TABLE [Sales].[SalesTerritoryHistory]  WITH CHECK 
    ADD CONSTRAINT [FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID] 
    FOREIGN KEY([BusinessEntityID])
    REFERENCES [Sales].[SalesPerson] ([BusinessEntityID]);
    GO
        
    ALTER TABLE [Sales].[SalesTerritoryHistory] 
    CHECK CONSTRAINT [FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID]
    GO
    ```

3. In the `ALTER TABLE ... ADD CONSTRAINT` portion of the script, modify the new foreign key constraint and specify the NOT FOR REPLICATION option. For example:

    ```sql
    ALTER TABLE [Sales].[SalesTerritoryHistory] 
    DROP CONSTRAINT [FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID]
    GO
    
    ALTER TABLE [Sales].[SalesTerritoryHistory]  WITH CHECK 
    ADD CONSTRAINT [FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID] 
    FOREIGN KEY([BusinessEntityID]) 
    REFERENCES [Sales].[SalesPerson] ([BusinessEntityID]) 
    NOT FOR REPLICATION; --added to disable constraint for replication
    GO
    
    ALTER TABLE [Sales].[SalesTerritoryHistory] 
    CHECK CONSTRAINT [FK_SalesTerritoryHistory_SalesPerson_BusinessEntityID]
    GO
    ```
  
## Next steps
 - [ALTER TABLE table_constraint &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)
 - [Script objects in SQL Server Management Studio](../../ssms/tutorials/scripting-ssms.md)
 - [Frequently Asked Questions for Replication Administrators](../replication/administration/frequently-asked-questions-for-replication-administrators.yml)
  

