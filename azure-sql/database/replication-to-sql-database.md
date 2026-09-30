---
title: Replication to Azure SQL Database
description: You can configure a database in Azure SQL Database as the push subscriber in a one-way transactional or snapshot replication topology from SQL Server or Azure SQL Managed Instance.
author: ferno-ms
ms.author: ferno
ms.reviewer: wiassaf
ms.date: 09/28/2026
ms.service: azure-sql-database
ms.subservice: replication
ms.topic: concept-article
ms.custom:
  - sqldbrb=1
  - ignite-2024
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi || =azuresql-vm || =fabricsql"
---
# Replication to Azure SQL Database

[!INCLUDE[appliesto-sqldb-fabricsqldb](../includes/appliesto-sqldb-fabricsqldb.md)]

You can configure Azure SQL Database or SQL database in Fabric as the push subscriber in a one-way transactional or snapshot replication topology from SQL Server and Azure SQL Managed Instance.

> [!NOTE]
> This article describes the use of [transactional replication](/sql/relational-databases/replication/transactional/transactional-replication) to push data to Azure SQL Database or Fabric SQL database. This article applies equally to Azure SQL Database and Fabric SQL database unless otherwise noted.
> 
> It's unrelated to [active geo-replication](./active-geo-replication-overview.md), an Azure SQL Database feature that allows you to create complete readable replicas of individual databases.

## Supported configurations
  
- Azure SQL Database or SQL database in Fabric can only be the push subscriber of a SQL Server publisher and distributor.
- Replication to SQL database in Fabric doesn't work when Private Link is enabled. 
- The SQL Server instance acting as publisher and/or distributor can be an instance of [SQL Server running on-premises](https://www.microsoft.com/sql-server/sql-server-downloads), an [Azure SQL Managed Instance](../managed-instance/instance-create-quickstart.md), or an instance of [SQL Server running on an Azure virtual machine in the cloud](../virtual-machines/windows/sql-vm-create-portal-quickstart.md). 
- The distribution database and the replication agents can't be placed on a database in Azure SQL Database.  
- [Snapshot](/sql/relational-databases/replication/snapshot-replication) and [one-way transactional](/sql/relational-databases/replication/transactional/transactional-replication) replication are supported. Peer-to-peer transactional replication and merge replication aren't supported.

### Versions  

To successfully replicate to a database in Azure SQL Database, SQL Server publishers and distributors must be using (at least) one of the following versions:

> [!NOTE]
> Attempting to configure replication using an unsupported version can result in error number MSSQL_REPL20084 (The process could not connect to Subscriber.) and MSSQL_REPL40532 (Cannot open server \<name> requested by the login. The login failed.).  

To use all the features of Azure SQL Database, you must be using the latest versions of [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) and [SQL Server Data Tools](/sql/ssdt/download-sql-server-data-tools-ssdt).  

Publishing to any **Azure SQL Database** from a SQL Server database is supported by the following versions of SQL Server:

- SQL Server 2016 and later versions

Publishing to **Fabric SQL database** from a SQL Server database is supported by the following versions of SQL Server:

- SQL Server 2022 [RTM CU12](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate12) and greater

### Types of replication

There are different [types of replication](/sql/relational-databases/replication/types-of-replication):

| Replication | Azure SQL Database | Azure SQL Managed Instance |
| :----| :------------- | :--------------- |
| [**Standard Transactional**](/sql/relational-databases/replication/transactional/transactional-replication) | Yes (only as subscriber) | Yes | 
| [**Snapshot**](/sql/relational-databases/replication/snapshot-replication) | Yes (only as subscriber) | Yes|
| [**Merge replication**](/sql/relational-databases/replication/merge/merge-replication) | No | No|
| [**Peer-to-peer**](/sql/relational-databases/replication/transactional/peer-to-peer-transactional-replication) | No | No|
| [**Bidirectional**](/sql/relational-databases/replication/transactional/bidirectional-transactional-replication) | No | Yes|
| [**Updatable subscriptions**](/sql/relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication) | No | No|

  
## Remarks

- Only push subscriptions to Azure SQL Database are supported.  
- Replication can be configured by using [SQL Server Management Studio](/ssms/sql-server-management-studio-ssms) or by executing Transact-SQL statements on the publisher. You can't configure replication by using the Azure portal.  
- To authenticate:
    - [Azure Arc enabled SQL Servers](/sql/sql-server/azure-arc/overview) allow replication to use [Microsoft Entra ID authentication](/sql/relational-databases/replication/configure-replication-with-azure-ad-authentication).
    - Replication can use Microsoft Entra ID authentication with a [service principal](/entra/identity-platform/app-objects-and-service-principals).
    - Replication can use SQL Server authentication logins to connect to Azure SQL Database only.
- Replicated tables must have a primary key.  
- You must have an existing Azure subscription.  
- The Azure SQL Database subscriber can be in any region.  
- A single publication on SQL Server can support both Azure SQL Database and SQL Server (on-premises and SQL Server in an Azure virtual machine) subscribers.  
- Replication management, monitoring, and troubleshooting must be performed from SQL Server rather than Azure SQL Database.  
- Only `@subscriber_type = 0` is supported in `sp_addsubscription` for SQL Database.  
- Azure SQL Database doesn't support bi-directional, immediate, updatable, or peer-to-peer replication.

## Replication Architecture  

![Diagram shows the replication architecture with Azure SQL Database, which contains several subscriber clusters in different regions, and on-premises Azure virtual machines, which contains a Publisher, Logread executable, and distributor executables that connect to remote clusters.](./media/replication-to-sql-database/replication-to-sql-database.png)  

## Scenarios  

### Typical Replication Scenario  

1. Create a transactional replication publication on a SQL Server database.  
2. On SQL Server use the **New Subscription Wizard** or Transact-SQL statements to create a push to subscription to Azure SQL Database.  
3. With single and pooled databases in Azure SQL Database, the initial data set is a snapshot that is created by the Snapshot Agent and distributed and applied by the Distribution Agent. With a SQL Managed Instance publisher, you can also use a database backup to seed the Azure SQL Database subscriber.

### Data migration scenario  

1. Use transactional replication to replicate data from a SQL Server database to Azure SQL Database.  
2. Redirect the client or middle-tier applications to update the database copy.  
3. Stop updating the SQL Server version of the table and remove the publication.  

## Limitations

The following replication options aren't supported by Azure SQL Database:

- Copy file groups association  
- Copy table partitioning schemes  
- Copy index partitioning schemes  
- Copy user defined statistics  
- Copy default bindings  
- Copy rule bindings  
- Copy fulltext indexes  
- Copy XML XSD  
- Copy XML indexes  
- Copy permissions  
- Copy spatial indexes  
- Copy filtered indexes  
- Copy data compression attribute  
- Copy sparse column attribute  
- Convert filestream to MAX data types  
- Convert hierarchyid to MAX data types  
- Convert spatial to MAX data types  
- Copy extended properties  

### Limitations to be determined

- Copy collation  
- Execution in a serialized transaction of the SP  

## Examples

Create a publication and a push subscription. For more information, see:
  
- [Create a Publication](/sql/relational-databases/replication/publish/create-a-publication)
- [Create a Push Subscription](/sql/relational-databases/replication/create-a-push-subscription/) by using the server name as the subscriber (for example `<Azure SQL logical server name>.database.windows.net`) and the Azure SQL Database name as the destination database (for example `AdventureWorks`).  

## See Also  

- [Transactional replication](../managed-instance/replication-transactional-overview.md)
- [Create a Publication](/sql/relational-databases/replication/publish/create-a-publication)
- [Create a Push Subscription](/sql/relational-databases/replication/create-a-push-subscription/)
- [Types of Replication](/sql/relational-databases/replication/types-of-replication)
- [Monitoring (Replication)](/sql/relational-databases/replication/monitor/monitoring-replication)
- [Initialize a Subscription](/sql/relational-databases/replication/initialize-a-subscription)
