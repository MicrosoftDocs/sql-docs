---
title: "Replication to SQL Database | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/29/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SQL Database replication"
  - "replication, SQL Database"
ms.assetid: e8484da7-495f-4dac-b38e-bcdc4691f9fa
caps.latest.revision: 15
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Replication to SQL Database
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication can be configured to [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)].  
  
 **Supported Configurations:**  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on-premises or an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running in an Azure virtual machine in the cloud. For more information, see [SQL Server on Azure Virtual Machines overview](https://azure.microsoft.com/documentation/articles/virtual-machines-sql-server-infrastructure-services/).  
  
-   [!INCLUDE[ssSDS](../../includes/sssds-md.md)] must be a push subscriber of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher.  
  
-   The distribution database and the replication agents cannot be placed on [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
-   Snapshot and one-way transactional replication are supported. Peer-to-peer transactional replication and merge replication are not supported.  
  
## Versions  
 The publisher and distributor must be at least at one of the following versions:  
  
-   [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP1 CU3  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] RTM CU10  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP2 CU8  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] expected in SP3  
  
 Attempting to configure replication using an older version can result in error number MSSQL_REPL20084 (The process could not connect to Subscriber.) and MSSQL_REPL40532 (Cannot open server \<name> requested by the login. The login failed.).  
  
 The [!INCLUDE[ssSDS](../../includes/sssds-md.md)] subscriber must be at least V12 and can be in any region.  
  
 To use all the features of [!INCLUDE[ssSDS](/sql-docs/docs/ssms/download-sql-server-management-studio-ssms) and [SQL Server Data Tools](/sql-docs/docs/ssdt/download-sql-server-data-tools-ssdt).  
  
## Remarks  
 Replication can be configured by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by executing [!INCLUDE[tsql](../../includes/tsql-md.md)] statements on the publisher. You cannot configure replication by using the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] portal.  
  
 Replication can only use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication logins to connect to [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
 Replicated table must have a primary key.  
  
 You must have an existing Azure subscription and an existing [!INCLUDE[ssSDS](../../includes/sssds-md.md)] V12.  
  
 A single publication on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can support both [!INCLUDE[ssSDS](../../includes/sssds-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (on-premises and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in an Azure virtual machine) subscribers.  
  
 Replication management, monitoring, and troubleshooting must be performed from the on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Only push subscriptions to [!INCLUDE[ssSDS](../../includes/sssds-md.md)] are supported.  
  
 Only `@subscriber_type = 0` is supported in **sp_addsubscription** for SQL Database.  
  
 [!INCLUDE[ssSDS](../../includes/sssds-md.md)] does not support bi-directional, immediate, updatable, or peer to peer replication.  
  
## Replication Architecture  
 ![replication-to-sql-database](../../relational-databases/replication/media/replication-to-sql-database.png "replication-to-sql-database")  
  
## Scenarios  
  
#### Typical Replication Scenario  
  
1.  Create a transactional replication publication on an on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
2.  On the on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] use the **New Subscription Wizard** or [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to create a push to subscription to [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
3.  The initial data set is typically a snapshot that is created by the Snapshot Agent and distributed and applied by the Distribution Agent. The initial data set can also be supplied through a backup or other means, such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
#### Data Migration Scenario  
  
1.  Use transactional replication to replicate data from an on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database to [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
2.  Redirect the client or middle-tier applications to update the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] copy.  
  
3.  Stop updating the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version of the table and remove the publication.  
  
## Limitations  
 The following options are not supported for [!INCLUDE[ssSDS](../../includes/sssds-md.md)] subscriptions:  
  
-   Copy file groups association  
  
-   Copy table partitioning schemes  
  
-   Copy index partitioning schemes  
  
-   Copy user defined statistics  
  
-   Copy default bindings  
  
-   Copy rule bindings  
  
-   Copy fulltext indexes  
  
-   Copy XML XSD  
  
-   Copy XML indexes  
  
-   Copy permissions  
  
-   Copy spatial indexes  
  
-   Copy filtered indexes  
  
-   Copy data compression attribute  
  
-   Copy sparse column attribute  
  
-   Convert filestream to MAX data types  
  
-   Convert hierarchyid to MAX data types  
  
-   Convert spatial to MAX data types  
  
-   Copy extended properties  
  
-   Copy permissions  
  
 Limitations to be determined:  
  
-   Copy collation  
  
-   Execution in a serialized transaction of the SP  
  
## Examples  
 Create a publication and a push subscription. For more information, see:  
  
-   [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)  
  
-   [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md) by using the [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] logical server name as the subscriber (for example **N'azuresqldbdns.database.windows.net'**) and the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] name as the destination database (for example **AdventureWorks**).  
  
## See Also  
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [Create a Push Subscription](../../relational-databases/replication/create-a-push-subscription.md)   
 [Types of Replication](../../relational-databases/replication/types-of-replication.md)   
 [Monitoring &#40;Replication&#41;](../../relational-databases/replication/monitor/monitoring-replication.md)   
 [Initialize a Subscription](../../relational-databases/replication/initialize-a-subscription.md)  
  
  
