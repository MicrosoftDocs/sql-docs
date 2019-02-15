---
title: "Configure Publishing and Distribution | Microsoft Docs"
ms.custom: ""
ms.date: "09/23/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords:
 - "replication [SQL Server], distribution"
 - "distribution configuration [SQL Server replication]"
 - "publishing [SQL Server replication], configuring"
ms.assetid: 3cfc8966-833e-42fa-80cb-09175d1feed7
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Configure Publishing and Distribution
[!INCLUDE[appliesto-ss-asdbmi-asdbmi-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
 This topic describes how to configure publishing and distribution in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).

##  <a name="BeforeYouBegin"></a> Before You Begin 

###  <a name="Security"></a> Security 
For more information, see [View and modify replication security settings](../../relational-databases/replication/security/view-and-modify-replication-security-settings.md).

##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio 
Configure distribution using the New Publication Wizard or the Configure Distribution Wizard. After the Distributor is configured, view and modify properties in the **Distributor Properties - \<Distributor>** dialog box. Use the Configure Distribution Wizard if you want to configure a Distributor so that members of the `db_owner` fixed database roles can create publications, or because you want to configure a remote Distributor that is not a Publisher.

#### To configure distribution 

1. In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the server that will be the Distributor (in many cases, the Publisher and Distributor are the same server), and then expand the server node.

2. Right-click the **Replication** folder, and then click **Configure Distribution**.

3. Follow the Configure Distribution Wizard to: 

  - Select a Distributor. To use a local Distributor, select **ServerName will act as its own Distributor; SQL Server will create a distribution database and log**. To use a remote Distributor, select **Use the following server as the Distributor**, and then select a server. The server must already be configured as a Distributor, and the Publisher must be enabled to use the Distributor. For more information, see [Enable a Remote Publisher at a Distributor &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/enable-a-remote-publisher-at-a-distributor-sql-server-management-studio.md).

     If you select a remote Distributor, you must enter a password on the **Administrative Password** page for connections made from the Publisher to the Distributor. This password must match the password specified when the Publisher was enabled at the remote Distributor.

  - Specify a root snapshot folder (for a local Distributor). The snapshot folder is simply a directory that you have designated as a share; agents that read from and write to this folder must have sufficient permissions to access it. Each Publisher that uses this Distributor creates a folder under the root folder, and each publication creates folders under the Publisher folder in which to store snapshot files. For more information on securing the folder appropriately, see [Secure the Snapshot Folder](../../relational-databases/replication/security/secure-the-snapshot-folder.md).

  - Specify the distribution database (for a local Distributor). The distribution database stores metadata and history data for all types of replication and transactions for transactional replication.

  - Optionally enable other Publishers to use the Distributor. If other Publishers are enabled to use the Distributor, you must enter a password on the **Distributor Password** page for connections made from these Publishers to the Distributor.

  - Optionally script configuration settings. For more information, see [Scripting Replication](../../relational-databases/replication/scripting-replication.md).

##  <a name="TsqlProcedure"></a> Using Transact-SQL 
Replication publishing and distribution can be configured programmatically using replication stored procedures.
### To configure publishing using a local distributor
1. Execute [sp_get_distributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-get-distributor-transact-sql.md) to determine if the server is already configured as a Distributor.

  - If the value of `installed` in the result set is `0`, execute [sp_adddistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributor-transact-sql.md) at the Distributor on the master database.

  - If the value of `distribution db installed` in the result set is `0`, execute [sp_adddistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributiondb-transact-sql.md) at the Distributor on the master database. Specify the name of the distribution database for `@database`. Optionally, you can specify the maximum transactional retention period for `@max_distretention` and the history retention period for `@history_retention`. If a new database is being created, specify the desired database property parameters.

2. At the Distributor, which is also the Publisher, execute [sp_adddistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md), specifying the UNC share that will be used as default snapshot folder for `@working_directory`.

   For a distributor on SQL Database Managed Instance, use an Azure storage account for `@working_directory` and the storage access key for `@storage_connection_string`. 

3. At the Publisher, execute [sp_replicationdboption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md). Specify the database being published for `@dbname`, the type of replication for `@optname`, and a value of `true` for `@value`.

#### To configure publishing using a remote distributor 

1. Execute [sp_get_distributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-get-distributor-transact-sql.md) to determine if the server is already configured as a Distributor.

   - If the value of `installed` in the result set is `0`, execute [sp_adddistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributor-transact-sql.md) at the Distributor on the master database. Specify a strong password for `@password`. This password for the `distributor_admin` account will be used by the Publisher when connecting to the Distributor.

   - If the value of `distribution db installed` in the result set is `0`, execute [sp_adddistributiondb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributiondb-transact-sql.md) at the Distributor on the master database. Specify the name of the distribution database for `@database`. Optionally, you can specify the maximum transactional retention period for `@max_distretention` and the history retention period for `@history_retention`. If a new database is being created, specify the desired database property parameters.

2. At the Distributor, execute [sp_adddistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md), specifying the UNC share that will be used as default snapshot folder for `@working_directory`. If the Distributor will use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher, you must also specify a value of `0` for `@security_mode` and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login information for `@login` and `@password`.

   For a distributor on SQL Database Managed Instance, use an Azure storage account for `@working_directory` and the storage access key for `@storage_connection_string`. 

3. At the Publisher on the master database, execute [sp_adddistributor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistributor-transact-sql.md). Specify the strong password used in step 1 for `@password`. This password will be used by the Publisher when connecting to the Distributor.

4. At the Publisher, execute [sp_replicationdboption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md). Specify the database being published for `@dbname`, the type of replication for `@optname`, and a value of true for `@value`.

###  <a name="TsqlExample"></a> Example (Transact-SQL) 
The following example demonstrates how to configure publishing and distribution programmatically. In this example, the name of the server that is being configured as a publisher and a local distributor is supplied using scripting variables. Replication publishing and distribution can be configured programmatically using replication stored procedures.

[!code-sql[HowTo#AddDistPub](../../relational-databases/replication/codesnippet/tsql/configure-publishing-and_1.sql)] 

##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO) 

#### To configure publishing and distribution on a single server 

1. Create a connection to the server by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.

2. Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationServer> class. Pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1.

3. Create an instance of the <xref:Microsoft.SqlServer.Replication.DistributionDatabase> class.

4. Set the <xref:Microsoft.SqlServer.Replication.DistributionDatabase.Name%2A> property to the database name and set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1.

5. Install the Distributor by calling the <xref:Microsoft.SqlServer.Replication.ReplicationServer.InstallDistributor%2A> method. Pass the <xref:Microsoft.SqlServer.Replication.DistributionDatabase> object from step 3.

6. Create an instance of the <xref:Microsoft.SqlServer.Replication.DistributionPublisher> class.

7. Set the following properties of <xref:Microsoft.SqlServer.Replication.DistributionPublisher>: 

  - <xref:Microsoft.SqlServer.Replication.DistributionPublisher.Name%2A> - name of the Publisher.

  - <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> - the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1.

  - <xref:Microsoft.SqlServer.Replication.DistributionPublisher.DistributionDatabase%2A> - the name of the database created in step 5.

  - <xref:Microsoft.SqlServer.Replication.DistributionPublisher.WorkingDirectory%2A> - the share used to access snapshot files.

  - <xref:Microsoft.SqlServer.Replication.DistributionPublisher.PublisherSecurity%2A> - the security mode used when connecting to the Publisher. <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> is recommended.

8. Call the <xref:Microsoft.SqlServer.Replication.DistributionPublisher.Create%2A> method.

#### To configure publishing and distribution using a remote Distributor 

1. Create a connection to the remote Distributor server by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.

2. Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationServer> class. Pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1.

3. Create an instance of the <xref:Microsoft.SqlServer.Replication.DistributionDatabase> class.

4. Set the <xref:Microsoft.SqlServer.Replication.DistributionDatabase.Name%2A> property to the database name, and set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1.

5. Install the Distributor by calling the <xref:Microsoft.SqlServer.Replication.ReplicationServer.InstallDistributor%2A> method. Specify a secure password (used by the Publisher when connecting to the remote Distributor) and the <xref:Microsoft.SqlServer.Replication.DistributionDatabase> object from step 3. For more information, see [Secure the Distributor](../../relational-databases/replication/security/secure-the-distributor.md).

   > `IMPORTANT!!` When possible, prompt users to enter security credentials at runtime. If you must store credentials, use the [cryptographic services](https://go.microsoft.com/fwlink/?LinkId=34733) provided by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows .NET Framework.

6. Create an instance of the <xref:Microsoft.SqlServer.Replication.DistributionPublisher> class.

7. Set the following properties of <xref:Microsoft.SqlServer.Replication.DistributionPublisher>: 

  - <xref:Microsoft.SqlServer.Replication.DistributionPublisher.Name%2A> - name of the local Publisher server.

  - <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> - the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1.

  - <xref:Microsoft.SqlServer.Replication.DistributionPublisher.DistributionDatabase%2A> - the name of the database created in step 5.

  - <xref:Microsoft.SqlServer.Replication.DistributionPublisher.WorkingDirectory%2A> - the share used to access snapshot files.

  - <xref:Microsoft.SqlServer.Replication.DistributionPublisher.PublisherSecurity%2A> - the security mode used when connecting to the Publisher. <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.WindowsAuthentication%2A> is recommended.

8. Call the <xref:Microsoft.SqlServer.Replication.DistributionPublisher.Create%2A> method.

9. Create a connection to the local Publisher server by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.

10. Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationServer> class. Pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 9.

11. Call the <xref:Microsoft.SqlServer.Replication.ReplicationServer.InstallDistributor%2A> method. Pass the name of the remote Distributor and the password for the remote Distributor specified in step 5.

> [!IMPORTANT]
> When possible, prompt users to enter security credentials at runtime. If you must store credentials, use the [cryptographic services](https://go.microsoft.com/fwlink/?LinkId=34733) provided by the Windows .NET Framework.

###  <a name="PShellExample"></a> Example (RMO) 
You can programmatically configure replication publishing and distribution by using Replication Management Objects (RMO).

[!code-cs[HowTo#rmo_AddDistPub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_adddistpub)] 

[!code-vb[HowTo#rmo_vb_AddDistPub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_adddistpub)] 

## See Also 
[View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)  
[Replication System Stored Procedures Concepts](../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)  
[Configure Distribution](../../relational-databases/replication/configure-distribution.md)  
[Replication Management Objects Concepts](../../relational-databases/replication/concepts/replication-management-objects-concepts.md)  
[Configure Replication for Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/configure-replication-for-always-on-availability-groups-sql-server.md) 


