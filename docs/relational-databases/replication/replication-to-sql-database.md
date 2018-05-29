---
title: "Replication to SQL Database | Microsoft Docs"
ms.custom: ""
ms.date: "6/7/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.component: "replication"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Database replication"
  - "replication, SQL Database"
ms.assetid: e8484da7-495f-4dac-b38e-bcdc4691f9fa
caps.latest.revision: 15
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: "= azuresqldb-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Replication with Azure SQL Database

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

[Azure SQL Database Managed Instance](http://docs.microsoft.com/azure/sql-database/sql-database-managed-instance) (preview) supports transactional and snapshot replication. 

## Publisher or Distributor on Azure SQL Database Managed Instance

SQL Databases on Managed Instance can participate in any replication role - publisher, distributor, or subscriber. Databases participating in replication can be hosted in an instance of SQL Server on-premises or on in the cloud with a Managed Instance.

### Supported topologies

- All participating databases in the cloud
- Publisher and distributor on-premises, with one or more of the subscribers in the cloud
- Publisher and distributor in the cloud, with one or more of the subscribers on-premises

In any scenario, a replication topology can include a mix of on-premises and cloud-hosted subscribers.

### Requirements

- Publisher and distributor databases on Azure SQL Database must be on Managed Instance
- To configure publisher or distributor databases on Azure SQL Databases, use Transact-SQL
- All instances of SQL Server must be on the same virtual network
- Databases on Azure SQL Database Managed Instance use SQL Authentication

## Push subscriptions to Azure SQL Database (either single or elastic pool) 

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication can be configured to [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] or elastic pool as a push subscription. In order to participate as a publisher or distributor, SQL Database has to be in a Managed Instance.

### Supported Configurations 
 -  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on-premises or an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running in an Azure virtual machine in the cloud. For more information, see [SQL Server on Azure Virtual Machines overview](https://azure.microsoft.com/documentation/articles/virtual-machines-sql-server-infrastructure-services/).  
 - [!INCLUDE[ssSDS](../../includes/sssds-md.md)] must be a push subscriber of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher.  
 -  The distribution database and the replication agents cannot be placed on [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
 - Snapshot and one-way transactional replication are supported. Peer-to-peer transactional replication and merge replication are not supported.  
 
### Versions  
- The publisher and distributor must be at least at one of the following versions:  
 
 - Azure SQL Database Managed Instance
 - [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)]  
 - [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]  
 - [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP1 CU3  
 - [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] RTM CU10  
 - [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP2 CU8  
 - [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] expected in SP3  
 
- Attempting to configure replication using an older version can result in error number MSSQL_REPL20084 (The process could not connect to Subscriber.) and MSSQL_REPL40532 (Cannot open server \<name> requested by the login. The login failed.).  
- The [!INCLUDE[ssSDS](../../includes/sssds-md.md)] subscriber must be at least V12 and can be in any region.  
 
 - To use all the features of [!INCLUDE[ssSDS](../../includes/sssds-md.md)] you must be using the latest versions of [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) and [SQL Server Data Tools](../../ssdt/download-sql-server-data-tools-ssdt.md).  
 
### Remarks  
 - Replication can be configured by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by executing [!INCLUDE[tsql](../../includes/tsql-md.md)] statements on the publisher. You cannot configure replication by using the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] portal.  
 - Replication can only use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication logins to connect to [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
 - Replicated table must have a primary key.  
 - You must have an existing Azure subscription and an existing [!INCLUDE[ssSDS](../../includes/sssds-md.md)] V12.  
 - A single publication on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can support both [!INCLUDE[ssSDS](../../includes/sssds-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (on-premises and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in an Azure virtual machine) subscribers.  
 - Replication management, monitoring, and troubleshooting must be performed from the on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
 - Only push subscriptions to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] are supported.  
 - Only `@subscriber_type = 0` is supported in **sp_addsubscription** for SQL Database.  
 - [!INCLUDE[ssSDS](../../includes/sssds-md.md)] does not support bi-directional, immediate, updatable, or peer to peer replication.  
 
### Replication Architecture  
 ![replication-to-sql-database](../../relational-databases/replication/media/replication-to-sql-database.png "replication-to-sql-database")  

## Scenarios  
 
* Create a transactional replication publication on either:
  - An on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance of SQL Server
   or
  - An Azure SQL Database Managed Instance
* To create a push subscription to a [!INCLUDE[ssSDS](../../includes/sssds-md.md)] from an on-premises instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use the **New Subscription Wizard** or [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. If the publisher or distributor are on Azure SQL Database Managed Instance, use [!INCLUDE[tsql](../../includes/tsql-md.md)].
* The initial data set is typically a snapshot that is created by the Snapshot Agent and distributed and applied by the Distribution Agent. The initial data set can also be supplied through a backup or other means, such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
 
### Data Migration Scenario  
 
 1.  Use transactional replication to replicate data to [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
 2.  Redirect the client or middle-tier applications to update the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] copy.  
 3.  Stop updating the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version of the table and remove the publication.  
 
### Limitations  
  The following options are not supported for [!INCLUDE[ssSDS](../../includes/sssds-md.md)] subscriptions:  
 
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
 - Copy permissions  
 
### Limitations to be determined 
 
 - Copy collation  
 - Execution in a serialized transaction of the SP  
 
## Examples  
  Create a publication and a push subscription. For more information, see:  

 - [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)  
 - [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md) by using the [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] logical server name as the subscriber (for example `N'azuresqldbdns.database.windows.net'`) and the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] name as the destination database (for example `AdventureWorks`).  
 
## See Also  
 - [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md) 
 - [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md) 
 - [Types of Replication](../../relational-databases/replication/types-of-replication.md) 
 - [Monitoring &#40;Replication&#41;](../../relational-databases/replication/monitor/monitoring-replication.md) 
 - [Initialize a Subscription](../../relational-databases/replication/initialize-a-subscription.md)  
