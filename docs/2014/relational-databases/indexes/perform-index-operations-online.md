---
title: "Perform Index Operations Online | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "index online operations [SQL Server]"
  - "online index operations"
  - "ONLINE option"
ms.assetid: 1e43537c-bf67-4db3-9908-3cb45c6fdaa1
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Perform Index Operations Online
  This topic describes how to create, rebuild, or drop indexes online in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The ONLINE option allows concurrent user access to the underlying table or clustered index data and any associated nonclustered indexes during these index operations. For example, while a clustered index is being rebuilt by one user, that user and others can continue to update and query the underlying data. When you perform data definition language (DDL) operations offline, such as building or rebuilding a clustered index; these operations hold exclusive locks on the underlying data and associated indexes. This prevents modifications and queries to the underlying data until the index operation is complete.  
  
> [!NOTE]  
>  Online index operations are not available in every [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] edition. For more information, see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To rebuild an index online, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   We recommend performing online index operations for business environments that operate 24 hours a day, seven days a week, in which the need for concurrent user activity during index operations is vital.  
  
-   The ONLINE option is available in the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
    -   [CREATE INDEX](/sql/t-sql/statements/create-index-transact-sql)  
  
    -   [ALTER INDEX](/sql/t-sql/statements/alter-index-transact-sql)  
  
    -   [DROP INDEX](/sql/t-sql/statements/drop-index-transact-sql)  
  
    -   [ALTER TABLE](/sql/t-sql/statements/alter-table-transact-sql) (To add or drop UNIQUE or PRIMARY KEY constraints with CLUSTERED index option)  
  
-   For more limitations and restrictions concerning creating, rebuilding, or dropping indexes online, see [Guidelines for Online Index Operations](guidelines-for-online-index-operations.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table or view.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To rebuild an index online  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table on which you want to rebuild an index online.  
  
2.  Expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table on which you want to rebuild an index online.  
  
4.  Expand the **Indexes** folder.  
  
5.  Right-click the index that you want to rebuild online and select **Properties**.  
  
6.  Under **Select a page**, select **Options**.  
  
7.  Select **Allow online DML processing**, and then select **True** from the list.  
  
8.  Click **OK**.  
  
9. Right-click the index that you want to rebuild online and select **Rebuild**.  
  
10. In the **Rebuild Indexes** dialog box, verify that the correct index is in the **Indexes to rebuild** grid and click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create, rebuild, or drop an index online  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example rebuilds an existing online  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    ALTER INDEX AK_Employee_NationalIDNumber ON HumanResources.Employee  
    REBUILD WITH (ONLINE = ON);  
    GO  
    ```  
  
     The following example deletes a clustered index online and moves the resulting table (heap) to the filegroup `NewGroup` by using the `MOVE TO` clause. The `sys.indexes`, `sys.tables`, and `sys.filegroups` catalog views are queried to verify the index and table placement in the filegroups before and after the move.  
  
     [!code-sql[IndexDDL#DropIndex4](../../snippets/tsql/SQL14/tsql/indexddl/transact-sql/dropindex.sql#dropindex4)]  
  
 For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql).  
  
  
