---
title: "Enable Indexes and Constraints | Microsoft Docs"
ms.custom: ""
ms.date: "02/17/2017"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "indexes [SQL Server], enabling"
  - "nonclustered indexes [SQL Server], enabling a disabled index"
  - "index enabling [SQL Server]"
  - "disabled indexes [SQL Server], how to enable"
  - "constraints [SQL Server], enabling"
  - "clustered indexes, enabling disabled indexes"
ms.assetid: c55c8865-322e-4ab0-ba04-ea1f56735353
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Enable Indexes and Constraints
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  This topic describes how to enable a disabled index in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. After an index is disabled, it remains in a disabled state until it is rebuilt or dropped  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To enable a disabled index, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   After rebuilding the index, any constraints that were disabled because of disabling the index must be manually enabled. PRIMARY KEY and UNIQUE constraints are enabled by rebuilding the associated index. This index must be rebuilt (enabled) before you can enable FOREIGN KEY constraints that reference the PRIMARY KEY or UNIQUE constraint. FOREIGN KEY constraints are enabled by using the ALTER TABLE CHECK CONSTRAINT statement.  
  
-   Rebuilding a disabled clustered index cannot be performed when the ONLINE option is set to ON.  
  
-   When the clustered index is disabled or enabled and the nonclustered index is disabled, the clustered index action has the following results on the disabled nonclustered index.  
  
    |Clustered Index Action|Disabled Nonclustered Index ...|  
    |----------------------------|-----------------------------------|  
    |ALTER INDEX REBUILD.|Remains disabled.|  
    |ALTER INDEX ALL REBUILD.|Is rebuilt and enabled.|  
    |DROP INDEX.|Remains disabled.|  
    |CREATE INDEX WITH DROP_EXISTING.|Remains disabled.|  
  
     Creating a new clustered index, behaves the same as ALTER INDEX ALL REBUILD.  
  
-   Allowed actions on nonclustered indexes associated with a clustered index depend on the state, whether disabled or enabled, of both index types. The following table summarizes the allowed actions on nonclustered indexes.  
  
    |Nonclustered Index Action|When both the clustered and nonclustered indexes are disabled.|When the clustered index is enabled and the nonclustered index is in either state.|  
    |-------------------------------|--------------------------------------------------------------------|----------------------------------------------------------------------------------------|  
    |ALTER INDEX REBUILD.|The action fails.|The action succeeds.|  
    |DROP INDEX.|The action succeeds.|The action succeeds.|  
    |CREATE INDEX WITH DROP_EXISTING.|The action fails.|The action succeeds.|  

-   When rebuilding disabled compressed nonclustered indexes, data_compression will default to 'none', meaning that indexes will be uncompressed. This is due to compression settings metadata is lost when nonclustered indexes are disabled. To work around this you must specify explicit data compression in rebuild statement.

###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table or view. If using DBCC DBREINDEX, user must either own the table; or be a member of the **sysadmin** fixed server role; or the **db_ddladmin** and **db_owner** fixed database roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To enable a disabled index  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table on which you want to enable an index.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table on which you want to enable an index.  
  
4.  Click the plus sign to expand the **Indexes** folder.  
  
5.  Right-click the index you want to enable and select **Rebuild**.  
  
6.  In the **Rebuild Indexes** dialog box, verify that the correct index is in the **Indexes to rebuild** grid and click **OK**.  
  
#### To enable all indexes on a table  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table on which you want to enable the indexes.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table on which you want to enable the indexes.  
  
4.  Right-click the **Indexes** folder and select **Rebuild All**.  
  
5.  In the **Rebuild Indexes** dialog box, verify that the correct indexes are in the **Indexes to rebuild** grid and click **OK**. To remove an index from the **Indexes to rebuild** grid, select the index and then press the Delete key.  
  
 The following information is available in the **Rebuild Indexes** dialog box:  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To enable a disabled index using ALTER INDEX  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- Enables the IX_Employee_OrganizationLevel_OrganizationNode index  
    -- on the HumanResources.Employee table.  
  
    ALTER INDEX IX_Employee_OrganizationLevel_OrganizationNode ON HumanResources.Employee  
    REBUILD;   
    GO  
    ```  
  
#### To enable a disabled index using CREATE INDEX  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- re-creates the IX_Employee_OrganizationLevel_OrganizationNode index  
    -- on the HumanResources.Employee table  
    -- using the OrganizationLevel and OrganizationNode columns  
    -- and then deletes the existing IX_Employee_OrganizationLevel_OrganizationNode index  
    CREATE INDEX IX_Employee_OrganizationLevel_OrganizationNode ON HumanResources.Employee  
       (OrganizationLevel, OrganizationNode)  
    WITH (DROP_EXISTING = ON);  
    GO  
    ```  
  
#### To enable a disabled index using DBCC DBREINDEX  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;   
    GO  
    -- enables the IX_Employee_OrganizationLevel_OrganizationNode index  
    -- on the HumanResources.Employee table  
    DBCC DBREINDEX ("HumanResources.Employee", IX_Employee_OrganizationLevel_OrganizationNode);  
    GO  
    ```  
  
#### To enable all indexes on a table using ALTER INDEX  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- enables all indexes  
    -- on the HumanResources.Employee table  
    ALTER INDEX ALL ON HumanResources.Employee  
    REBUILD;  
    GO  
    ```  
  
#### To enable all indexes on a table using DBCC DBREINDEX  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;   
    GO  
    -- enables all indexes  
    -- on the HumanResources.Employee table  
    DBCC DBREINDEX ("HumanResources.Employee", " ");  
    GO  
    ```  
  
 For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md), [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md), and [DBCC DBREINDEX &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-dbreindex-transact-sql.md).  
  
  
