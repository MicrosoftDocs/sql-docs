---
title: Perform Index Operations Online
description: Perform Index Operations Online
author: MikeRayMSFT
ms.author: mikeray
ms.date: 11/15/2019
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "index online operations [SQL Server]"
  - "online index operations"
  - "ONLINE option"
ms.assetid: 1e43537c-bf67-4db3-9908-3cb45c6fdaa1
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Perform Index Operations Online
[!INCLUDE [SQL Server Azure SQL Database Azure SQL MI](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  This topic describes how to create, rebuild, or drop indexes online in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The ONLINE option allows concurrent user access to the underlying table or clustered index data and any associated nonclustered indexes during these index operations. For example, while a clustered index is being rebuilt by one user, that user and others can continue to update and query the underlying data. When you perform data definition language (DDL) operations offline, such as building or rebuilding a clustered index; these operations hold exclusive locks on the underlying data and associated indexes. This prevents modifications and queries to the underlying data until the index operation is complete.  
  
> [!NOTE]  
>  Online index operations are not available in every [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] edition. For more information, see [Editions and supported features of SQL Server](../../sql-server/editions-and-components-of-sql-server-2019.md). 
>
> Online index operations are available in Azure SQL Database and Azure SQL Managed Instance.
  
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
  
    -   [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md)  
  
    -   [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md)  
  
    -   [DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md)  
  
    -   [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) (To add or drop UNIQUE or PRIMARY KEY constraints with CLUSTERED index option)  
  
-   For more limitations and restrictions concerning creating, rebuilding, or dropping indexes online, see [Guidelines for Online Index Operations](../../relational-databases/indexes/guidelines-for-online-index-operations.md).  
  
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
  
### To create, rebuild, or drop an index online  
  
The following example rebuilds an existing online index in the AdventureWorks database.

```sql
ALTER INDEX AK_Employee_NationalIDNumber
    ON HumanResources.Employee
    REBUILD WITH (ONLINE = ON);
```  
  
The following example deletes a clustered index online and moves the resulting table (heap) to the filegroup `NewGroup` by using the `MOVE TO` clause. The `sys.indexes`, `sys.tables`, and `sys.filegroups` catalog views are queried to verify the index and table placement in the filegroups before and after the move.  
  
[!code-sql[IndexDDL#DropIndex4](../../relational-databases/indexes/codesnippet/tsql/perform-index-operations_1.sql)]  

For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).

## Next steps

- [Resumable index considerations](guidelines-for-online-index-operations.md#resumable-index-considerations)
