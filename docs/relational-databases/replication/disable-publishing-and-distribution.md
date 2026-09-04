---
title: "Disable Publishing and Distribution"
description: Learn how to disable publishing and distribution in SQL Server by using SQL Server Management Studio, Transact-SQL, or Replication Management Objects.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "disabling publishing"
  - "publishing [SQL Server replication], disabling"
  - "distribution disabling [SQL Server replication]"
  - "removing replication"
  - "replication [SQL Server], removing"
  - "disabling replication"
  - "disabling distribution"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Disable publishing and distribution
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  This article describes how to disable publishing and distribution in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 You can perform the following steps:  
  
-   Delete all distribution databases on the Distributor.  
  
-   Disable all Publishers that use the Distributor and delete all publications on those Publishers.  
  
-   Delete all subscriptions to the publications. Data in the publication and subscription databases will not be deleted; however, it loses its synchronization relationship to any publication databases. If you want the data at the Subscriber to be deleted, you must delete it manually.  

<a id="BeforeYouBegin"></a>

##  <a name="Prerequisites"></a> Prerequisites
  
-   To disable publishing and distribution, all distribution and publication databases must be online. If any *database snapshots* exist for distribution or publication databases, you must drop them before disabling publishing and distribution. A database snapshot is a read-only offline copy of a database and isn't related to a replication snapshot. For more information, see [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Use the Disable Publishing and Distribution Wizard to disable publishing and distribution.  
  
#### To disable publishing and distribution  
  
1.  Connect to the Publisher or Distributor you want to disable in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
1.  Right-click the **Replication** folder, and then click **Disable Publishing and Distribution**.  
  
1.  Complete the steps in the Disable Publishing and Distribution Wizard.  

##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 You can disable publishing and distributing by using replication stored procedures.  
  
#### To disable publishing and distribution  
  
1.  Stop all replication-related jobs. For a list of job names, see the "Agent Security Under SQL Server Agent" section of [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
1.  At each Subscriber on the subscription database, execute [sp_removedbreplication](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md) to remove replication objects from the database. This stored procedure doesn't remove replication jobs at the Distributor.  
  
1.  At the Publisher on the publication database, execute [sp_removedbreplication](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md) to remove replication objects from the database.  
  
1.  If the Publisher uses a remote Distributor, execute [sp_dropdistributor](../../relational-databases/system-stored-procedures/sp-dropdistributor-transact-sql.md).  
  
1.  At the Distributor, execute [sp_dropdistpublisher](../../relational-databases/system-stored-procedures/sp-dropdistpublisher-transact-sql.md). Run this stored procedure once for each Publisher registered at the Distributor.  
  
1.  At the Distributor, execute [sp_dropdistributiondb](../../relational-databases/system-stored-procedures/sp-dropdistributiondb-transact-sql.md) to delete the distribution database. Run this stored procedure once for each distribution database at the Distributor. This action also removes any Queue Reader Agent jobs associated with the distribution database.  
  
1.  At the Distributor, execute [sp_dropdistributor](../../relational-databases/system-stored-procedures/sp-dropdistributor-transact-sql.md) to remove the Distributor designation from the server.  
  
    > [!NOTE]  
    > If you don't drop all replication publishing and distribution objects before you execute [sp_dropdistpublisher](../../relational-databases/system-stored-procedures/sp-dropdistpublisher-transact-sql.md) and [sp_dropdistributor](../../relational-databases/system-stored-procedures/sp-dropdistributor-transact-sql.md), these procedures return an error. To drop all replication-related objects when a Publisher or Distributor is dropped, set the `@no_checks` parameter to **1**. If a Publisher or Distributor is offline or unreachable, set the `@ignore_distributor` parameter to **1** so that you can drop them. However, you must manually remove any publishing and distributing objects left behind.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 This example script removes replication objects from the subscription database.  
  
 :::code language="sql" source="codesnippet/tsql/disable-publishing-and-d_1.sql":::
  
 This example script disables publishing and distribution on a server that is a Publisher and Distributor and drops the distribution database.  
  
 :::code language="sql" source="codesnippet/tsql/disable-publishing-and-d_2.sql":::
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
  
#### To disable publishing and distribution  
  
1.  Remove all subscriptions to publications that use the Distributor. For more information, see [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md) and [Delete a Push Subscription](../../relational-databases/replication/delete-a-push-subscription.md).  
  
1.  Remove all publications that use the Distributor, and disable publishing for all databases if the Publisher and Distributor are on the same server. For more information, see [Delete a Publication](../../relational-databases/replication/publish/delete-a-publication.md).  
  
1.  Create a connection to the Distributor by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
1.  Create an instance of the <xref:Microsoft.SqlServer.Replication.DistributionPublisher> class. Specify the <xref:Microsoft.SqlServer.Replication.DistributionPublisher.Name%2A> property, and pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object from step 3.  
  
1.  (Optional) Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object and verify that the Publisher exists. If this method returns **false**, the Publisher name set in step 4 was incorrect or the Publisher isn't used by this Distributor.  
  
1.  Call the <xref:Microsoft.SqlServer.Replication.DistributionPublisher.Remove%2A> method. Pass a value of **true** for *force* if the Publisher and Distributor are on different servers, and when the Publisher should be uninstalled at the Distributor without first verifying that publications no longer exist at the Publisher.  
  
1.  Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationServer> class. Pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object from step 3.  
  
1.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationServer.UninstallDistributor%2A> method. Pass a value of **true** for *force* to remove all replication objects at the Distributor without first verifying that all local publication databases are disabled, and distribution databases are uninstalled.  
  
###  <a name="PShellExample"></a> Examples (RMO)  
 The following example removes the Publisher registration at the Distributor, drops the Distribution database, and uninstalls the Distributor.  
  
 [!code-cs[HowTo#rmo_DropDistPub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_dropdistpub)]  
  
 [!code-vb[HowTo#rmo_vb_DropDistPub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_dropdistpub)]  
  
 The following example uninstalls the Distributor without first disabling local publication databases or dropping the distribution database.  
  
 [!code-cs[HowTo#rmo_DropDistPubForce](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_dropdistpubforce)]  
  
 [!code-vb[HowTo#rmo_vb_DropDistPubForce](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_dropdistpubforce)]  
  
## Related content

- [Replication Management Objects Concepts](concepts/replication-management-objects-concepts.md)
- [Replication System Stored Procedures Concepts](concepts/replication-system-stored-procedures-concepts.md)
