---
title: "Disable Publishing and Distribution | Microsoft Docs"
description: Learn how to disable publishing and distribution in SQL Server by using SQL Server Management Studio, Transact-SQL, or Replication Management Objects.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "disabling publishing"
  - "publishing [SQL Server replication], disabling"
  - "distribution disabling [SQL Server replication]"
  - "removing replication"
  - "replication [SQL Server], removing"
  - "disabling replication"
  - "disabling distribution"
ms.assetid: 6d4a1474-4d13-4826-8be2-80050fafa8a5
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"

---
# Disable Publishing and Distribution
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  This topic describes how to disable publishing and distribution in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 You can do the following:  
  
-   Delete all distribution databases on the Distributor.  
  
-   Disable all Publishers that use the Distributor and delete all publications on those Publishers.  
  
-   Delete all subscriptions to the publications. Data in the publication and subscription databases will not be deleted; however, it loses its synchronization relationship to any publication databases. If you want the data at the Subscriber to be deleted, you must delete it manually.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
-   **To disable publishing and distribution, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   To disable publishing and distribution, all distribution and publication databases must be online. If any *database snapshots* exist for distribution or publication databases, they must be dropped before disabling publishing and distribution. A database snapshot is a read-only offline copy of a database and is not related to a replication snapshot. For more information, see [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Disable publishing and distribution by using the Disable Publishing and Distribution Wizard.  
  
#### To disable publishing and distribution  
  
1.  Connect to the Publisher or Distributor you want to disable in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Right-click the **Replication** folder, and then click **Disable Publishing and Distribution**.  
  
3.  Complete the steps in the Disable Publishing and Distribution Wizard.  

##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Publishing and distributing can be disabled programmatically using replication stored procedures.  
  
#### To disable publishing and distribution  
  
1.  Stop all replication-related jobs. For a list of job names, see the "Agent Security Under SQL Server Agent" section of [Replication Agent Security Model](../../relational-databases/replication/security/replication-agent-security-model.md).  
  
2.  At each Subscriber on the subscription database, execute [sp_removedbreplication](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md) to remove replication objects from the database. This stored procedure will not remove replication jobs at the Distributor.  
  
3.  At the Publisher on the publication database, execute [sp_removedbreplication](../../relational-databases/system-stored-procedures/sp-removedbreplication-transact-sql.md) to remove replication objects from the database.  
  
4.  If the Publisher uses a remote Distributor, execute [sp_dropdistributor](../../relational-databases/system-stored-procedures/sp-dropdistributor-transact-sql.md).  
  
5.  At the Distributor, execute [sp_dropdistpublisher](../../relational-databases/system-stored-procedures/sp-dropdistpublisher-transact-sql.md). This stored procedure should be run once for each Publisher registered at the Distributor.  
  
6.  At the Distributor, execute [sp_dropdistributiondb](../../relational-databases/system-stored-procedures/sp-dropdistributiondb-transact-sql.md) to delete the distribution database. This stored procedure should be run once for each distribution database at the Distributor. This also removes any Queue Reader Agent jobs associated with the distribution database.  
  
7.  At the Distributor, execute [sp_dropdistributor](../../relational-databases/system-stored-procedures/sp-dropdistributor-transact-sql.md) to remove the Distributor designation from the server.  
  
    > [!NOTE]  
    > If all replication publishing and distribution objects are not dropped before you execute [sp_dropdistpublisher](../../relational-databases/system-stored-procedures/sp-dropdistpublisher-transact-sql.md) and [sp_dropdistributor](../../relational-databases/system-stored-procedures/sp-dropdistributor-transact-sql.md), these procedures will return an error. To drop all replication-related objects when a Publisher or Distributor is dropped, the `@no_checks` parameter must be set to **1**. If a Publisher or Distributor is offline or unreachable, the `@ignore_distributor` parameter can be set to **1** so that they can be dropped; however, any publishing and distributing objects left behind must be removed manually.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 This example script removes replication objects from the subscription database.  
  
 [!code-sql[HowTo#sp_removedbreplication](../../relational-databases/replication/codesnippet/tsql/disable-publishing-and-d_1.sql)]  
  
 This example script disables publishing and distribution on a server that is a Publisher and Distributor and drops the distribution database.  
  
 [!code-sql[HowTo#sp_DropDistPub](../../relational-databases/replication/codesnippet/tsql/disable-publishing-and-d_2.sql)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
  
#### To disable publishing and distribution  
  
1.  Remove all subscriptions to publications that use the Distributor. For more information, see [Delete a Pull Subscription](../../relational-databases/replication/delete-a-pull-subscription.md) and [Delete a Push Subscription](../../relational-databases/replication/delete-a-push-subscription.md).  
  
2.  Remove all publications that use the Distributor, and disable publishing for all databases if the Publisher and Distributor are on the same server. For more information, see [Delete a Publication](../../relational-databases/replication/publish/delete-a-publication.md).  
  
3.  Create a connection to the Distributor by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
4.  Create an instance of the <xref:Microsoft.SqlServer.Replication.DistributionPublisher> class. Specify the <xref:Microsoft.SqlServer.Replication.DistributionPublisher.Name%2A> property, and pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object from step 3.  
  
5.  (Optional) Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties of the object and verify that the Publisher exists. If this method returns **false**, the Publisher name set in step 4 was incorrect or the Publisher is not used by this Distributor.  
  
6.  Call the <xref:Microsoft.SqlServer.Replication.DistributionPublisher.Remove%2A> method. Pass a value of **true** for *force* if the Publisher and Distributor are on different servers, and when the Publisher should be uninstalled at the Distributor without first verifying that publications no longer exist at the Publisher.  
  
7.  Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationServer> class. Pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object from step 3.  
  
8.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationServer.UninstallDistributor%2A> method. Pass a value of **true** for *force* to remove all replication objects at the Distributor without first verifying that all local publication databases have been disabled, and distribution databases have been uninstalled.  
  
###  <a name="PShellExample"></a> Examples (RMO)  
 This example removes the Publisher registration at the Distributor, drops the distribution database, and uninstalls the Distributor.  
  
 [!code-cs[HowTo#rmo_DropDistPub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_dropdistpub)]  
  
 [!code-vb[HowTo#rmo_vb_DropDistPub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_dropdistpub)]  
  
 This example uninstalls the Distributor without first disabling local publication databases or dropping the distribution database.  
  
 [!code-cs[HowTo#rmo_DropDistPubForce](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_dropdistpubforce)]  
  
 [!code-vb[HowTo#rmo_vb_DropDistPubForce](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_dropdistpubforce)]  
  
## See Also  
 [Replication Management Objects Concepts](../../relational-databases/replication/concepts/replication-management-objects-concepts.md)   
 [Replication System Stored Procedures Concepts](../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)  
  
  
