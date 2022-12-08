---
title: "View & modify Distributor & Publisher properties"
description: Learn how to modify the properties for the Distributor and Publisher using SQL Server Management Studio (SSMS), Transact-SQL (T-SQL) or Replication Management Objects (RMO). 
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing replication properties"
  - "Distributors [SQL Server replication], modifying"
  - "modifying replication properties, Distributors"
  - "Distributors [SQL Server replication], properties"
ms.assetid: 5dae1d59-c377-4c6e-adc9-b68c5b328f79
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# View and Modify Distributor and Publisher Properties
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  This topic describes how to view and modify Distributor and Publisher properties in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or Replication Management Objects (RMO).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To view and modify Distributor and Publisher properties, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [Replication Management Objects (RMO)](#RMOProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   For Publishers running versions prior to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], a user in the **sysadmin** fixed server role can register Subscribers on the **Subscribers** page. Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], it is no longer necessary to explicitly register Subscribers for replication.  
  
###  <a name="Security"></a> Security  
 When possible, prompt users to enter security credentials at runtime.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view and modify Distributor properties  
  
1.  Connect to the Distributor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Right-click the **Replication** folder, and then click **Distributor Properties**.  
  
3.  View and modify properties in the **Distributor Properties - \<Distributor>** dialog box.  
  
    -   To view and modify properties for a distribution database, click the properties button (**...**) for the database on the **General** page of thedialog box.  
  
    -   To view and modify Publisher properties associated with the Distributor, click the properties button (**...**) for the Publisher on the **Publishers** page of the dialog box.  
  
    -   To access profiles for replication agents, click the **Profile Defaults** button on the **General** page of the dialog box. For more information, see [Replication Agent Profiles](../../relational-databases/replication/agents/replication-agent-profiles.md).  
  
    -   To change the password for the account used when administrative stored procedures execute at the Publisher and update information at the Distributor, enter a new password in the **Password** and **Confirm password** boxes on the **Publishers** page of the dialog box. For more information, see [Secure the Distributor](../../relational-databases/replication/security/secure-the-distributor.md).  
  
4.  Modify any properties if necessary, and then click **OK**.  
  
#### To view and modify Publisher properties  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then expand the server node.  
  
2.  Right-click the **Replication** folder, and then click **Publisher Properties**.  
  
3.  View and modify properties in the **Publisher Properties - < Publisher >** dialog box.  
  
    -   A user in the **sysadmin** fixed server role can enable databases for replication on the **Publication Databases** page. Enabling a database does not publish that database; rather, it allows any user in the **db_owner** fixed database role for that database to create one or more publications in the database.  
  
4.  Modify any properties if necessary, and then click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 Publisher and Distributor properties can be viewed programmatically using replication stored procedures.  
  
#### To view Distributor and distribution database properties  
  
1.  Execute [sp_helpdistributor](../../relational-databases/system-stored-procedures/sp-helpdistributor-transact-sql.md) to return information about the Distributor, distribution database, and working directory.  
  
2.  Execute [sp_helpdistributiondb](../../relational-databases/system-stored-procedures/sp-helpdistributiondb-transact-sql.md) to return properties of a specified distribution database.  
  
#### To change Distributor and distribution database properties  
  
1.  At the Distributor, execute [sp_changedistributor_property](../../relational-databases/system-stored-procedures/sp-changedistributor-property-transact-sql.md) to modify Distributor properties.  
  
2.  At the Distributor, execute [sp_changedistributiondb](../../relational-databases/system-stored-procedures/sp-changedistributiondb-transact-sql.md) to modify distribution database properties.  
  
3.  At the Distributor, execute [sp_changedistributor_password](../../relational-databases/system-stored-procedures/sp-changedistributor-password-transact-sql.md) to change the Distributor password.  
  
    > [!IMPORTANT]  
    >  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, secure the file to prevent unauthorized access.  
  
4.  At the Distributor, execute [sp_changedistpublisher](../../relational-databases/system-stored-procedures/sp-changedistpublisher-transact-sql.md) to change the properties of a Publisher using the Distributor.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 The following example [!INCLUDE[tsql](../../includes/tsql-md.md)] script returns information about the Distributor and distribution database.  
  
 [!code-sql[HowTo#sp_helpdistributor](../../relational-databases/replication/codesnippet/tsql/view-and-modify-distribu_1.sql)]  
  
 [!code-sql[HowTo#sp_helpdistributiondb](../../relational-databases/replication/codesnippet/tsql/view-and-modify-distribu_2.sql)]  
  
 This example changes retention periods for the Distributor, the password used when connecting to the Distributor, and the interval at which the Distributor checks the status of various replication agents (also known as the heartbeat interval).  
  
> [!IMPORTANT]  
>  When possible, prompt users to enter security credentials at runtime. If you must store credentials in a script file, secure the file to prevent unauthorized access.  
  
 [!code-sql[HowTo#sp_changedistributor_property](../../relational-databases/replication/codesnippet/tsql/view-and-modify-distribu_3.sql)]  
  
 [!code-sql[HowTo#sp_changedistributiondb](../../relational-databases/replication/codesnippet/tsql/view-and-modify-distribu_4.sql)]  
  
 [!code-sql[HowTo#sp_changedistributor_password](../../relational-databases/replication/codesnippet/tsql/view-and-modify-distribu_5.sql)]  
  
##  <a name="RMOProcedure"></a> Using Replication Management Objects (RMO)  
  
#### To view and modify Distributor properties  
  
1.  Create a connection to the Distributor by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationServer> class. Pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object from step 1.  
  
3.  (Optional) Check the <xref:Microsoft.SqlServer.Replication.ReplicationServer.IsDistributor%2A> property to verify that the currently connected server is a Distributor.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.Load%2A> method to get the properties from the server.  
  
5.  (Optional) To change properties, set a new value for one or more of the Distributor properties that can be set on the <xref:Microsoft.SqlServer.Replication.ReplicationServer> object.  
  
6.  (Optional) If the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> property on the <xref:Microsoft.SqlServer.Replication.ReplicationServer> object is set to **true**, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit the changes to the server.  
  
#### To view and modify distribution database properties  
  
1.  Create a connection to the Distributor by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.DistributionDatabase> class. Specify the name property and pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object from step 1.  
  
3.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method to get the properties from the server. If this method returns **false**, the database with the specified name does not exist on the server.  
  
4.  (Optional) To change properties, set a new value for one of the <xref:Microsoft.SqlServer.Replication.DistributionDatabase> properties that can be set.  
  
5.  (Optional) If the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> property on the <xref:Microsoft.SqlServer.Replication.DistributionDatabase> object is set to **true**, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit the changes to the server.  
  
#### To view and modify Publisher properties  
  
1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.DistributionPublisher> class. Specify the <xref:Microsoft.SqlServer.Replication.DistributionPublisher.Name%2A> property and pass the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object from step 1.  
  
3.  (Optional) To change properties, set a new value for one of the <xref:Microsoft.SqlServer.Replication.DistributionPublisher> properties that can be set.  
  
4.  (Optional) If the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CachePropertyChanges%2A> property on the <xref:Microsoft.SqlServer.Replication.DistributionPublisher> object is set to **true**, call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A> method to commit the changes to the server.  
  
#### To change the password for the administrative connection from the Publisher to the Distributor  
  
1.  Create a connection to the Distributor by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationServer> class.  
  
3.  Set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 1.  
  
4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.Load%2A> method to get the properties of the object.  
  
5.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationServer.ChangeDistributorPassword%2A> method. Pass the new password value for the *password* parameter.  
  
    > [!IMPORTANT]  
    >  When possible, prompt users to enter security credentials at runtime. If you must store credentials, use the [cryptographic services](/previous-versions/aa719848(v=vs.71)) provided by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows .NET Framework.  
  
6.  (Optional) Perform the following steps to change the password at each remote Publisher that uses this Distributor:  
  
    1.  Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.  
  
    2.  Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationServer> class.  
  
    3.  Set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the connection created in step 6a.  
  
    4.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.Load%2A> method to get the properties of the object.  
  
    5.  Call the <xref:Microsoft.SqlServer.Replication.ReplicationServer.ChangeDistributorPassword%2A> method. Pass the new password value from Step 5 for the *password* parameter.  
  
###  <a name="PShellExample"></a> Example (RMO)  
 This example shows how to change Distribution and distribution database properties.  
  
> [!IMPORTANT]  
>  To avoid storing credentials in the code, the new Distributor password is supplied at runtime.  
  
 [!code-cs[HowTo#rmo_ChangeDistPub](../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_changedistpub)]  
  
 [!code-vb[HowTo#rmo_vb_ChangeDistPub](../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_changedistpub)]  
  
## See Also  
 [Replication Management Objects Concepts](../../relational-databases/replication/concepts/replication-management-objects-concepts.md)   
 [Disable Publishing and Distribution](../../relational-databases/replication/disable-publishing-and-distribution.md)   
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)   
 [Replication Management Objects Concepts](../../relational-databases/replication/concepts/replication-management-objects-concepts.md)   
 [Distributor and Publisher Information Script](../../relational-databases/replication/administration/distributor-and-publisher-information-script.md)   
 [Replication System Stored Procedures Concepts](../../relational-databases/replication/concepts/replication-system-stored-procedures-concepts.md)   
 [View Information and Perform Tasks for a Publisher &#40;Replication Monitor&#41;](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md)  
  
