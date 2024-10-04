---
title: "Create a publication"
description: Learn how to create a publication in SQL Server by using SQL Server Management Studio, Transact-SQL, or Replication Management Objects.
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "publications [SQL Server replication], creating"
  - "articles [SQL Server replication], defining"
  - "adding articles"
  - "articles [SQL Server replication], adding"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Create a publication

[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to create a publication in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or Replication Management Objects (RMO).

## <a id="Restrictions"></a> Limitations and restrictions

- Publication and article names can't include any of the following characters: `%`, `*`, `[`, `]`, `|`, `:`, `"`, `?`, `'`, `\`, `/`, `<`, or `>`. If objects in the database include any of these characters and you want to replicate them, you must specify an article name that is different from the object name in the **Article Properties - \<Article>** dialog box, which is available from the **Articles** page in the wizard.

### <a id="Security"></a> Security

When possible, prompt users to enter security credentials at runtime. If you must store credentials, use the [cryptographic services](/previous-versions/aa719848(v=vs.71)) provided by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows .NET Framework.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

Create publications and define articles with the New Publication Wizard. After a publication is created, view and modify publication properties in the **Publication Properties - \<Publication>** dialog box. For information about creating a publication from an Oracle database, see [Create a Publication from an Oracle Database](../../../relational-databases/replication/publish/create-a-publication-from-an-oracle-database.md).

#### Create a publication and define articles

1. Connect to the Publisher in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], and then expand the server node.

1. Expand the **Replication** folder, and then right-click the **Local Publications** folder.

1. Select **New Publication**.

1. Follow the pages in the New Publication Wizard to:

   - Specify a Distributor if distribution hasn't been configured on the server. For more information about configuring distribution, see [Configure Publishing and Distribution](../../../relational-databases/replication/configure-publishing-and-distribution.md).

     If you specify on the **Distributor** page that the Publisher server will act as its own Distributor (a local Distributor), and the server isn't configured as a Distributor, the New Publication Wizard will configure the server. You will specify a default snapshot folder for the Distributor on the **Snapshot Folder** page. The snapshot folder is simply a directory that you have designated as a share; agents that read from and write to this folder must have sufficient permissions to access it. For more information about securing the folder appropriately, see [Secure the Snapshot Folder](../../../relational-databases/replication/security/secure-the-snapshot-folder.md).

     If you specify that another server should act as the Distributor, you must enter a password on the **Administrative Password** page for connections made from the Publisher to the Distributor. This password must match the password specified when the Publisher was enabled at the remote Distributor.

     For more information, see [Configure Distribution](../../../relational-databases/replication/configure-distribution.md).

   - Choose a publication database.

   - Select a publication type. For more information, see [Types of Replication](../../../relational-databases/replication/types-of-replication.md).

   - Specify data and database objects to publish; optionally filter columns from table articles, and set article properties.

   - Optionally filter rows from table articles. For more information, see [Filter Published Data](../../../relational-databases/replication/publish/filter-published-data.md).

   - Set the Snapshot Agent schedule.

   - Specify the credentials under which the following replication agents run and make connections:

       - Snapshot Agent for all publications.

       - Log Reader Agent for all transactional publications.

       - Queue Reader Agent for transactional publications that allow updating subscriptions.

         For more information, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md) and [Replication Security Best Practices](../../../relational-databases/replication/security/replication-security-best-practices.md).

   - Optionally script the publication. For more information, see [Scripting Replication](../../../relational-databases/replication/scripting-replication.md).

   - Specify a name for the publication.

## <a id="TsqlProcedure"></a> Use Transact-SQL

Publications can be created programmatically using replication stored procedures. The stored procedures that are used will depend on the type of publication being created.

#### Create a snapshot or transactional publication

1. At the Publisher on the publication database, execute [sp_replicationdboption (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md) to enable publication of the current database using snapshot or transactional replication.

1. For a transactional publication, determine whether a Log Reader Agent job exists for the publication database. (This step isn't required for snapshot publications.)

   - If a Log Reader Agent job exists for the publication database, proceed to step 3.

   - If you are unsure whether a Log Reader Agent job exists for a published database, execute [sp_helplogreader_agent (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-helplogreader-agent-transact-sql.md) at the Publisher on the publication database.

   - If the result set is empty, create a Log Reader Agent job. At the Publisher, execute [sp_addlogreader_agent (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md). Specify the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows credentials under which the agent runs for `@job_name` and `@password`. If the agent will use SQL Server Authentication when connecting to the Publisher, you must also specify a value of `0` for `@publisher_security_mode` and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login information for `@publisher_login` and `@publisher_password`. Proceed to step 3.

1. At the Publisher, execute [sp_addpublication (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md). Specify a publication name for `@publication`, and, for the `@repl_freq` parameter, specify a value of `snapshot` for a snapshot publication or a value of `continuous` for a transactional publication. Specify any other publication options. This defines the publication.

   > [!NOTE]  
   > Publication names cannot include the following characters:  
   >  
   > `%`, `*`, `[`, `]`, `|`, `:`, `"`, `?`, `\`, `/`, `<`, or `>`.

1. At the Publisher, execute [sp_addpublication_snapshot (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md). Specify the publication name used in step 3 for `@publication` and the Windows credentials under which the Snapshot Agent runs for `@snapshot_job_name` and `@password`. If the agent will use SQL Server Authentication when connecting to the Publisher, you must also specify a value of `0` for `@publisher_security_mode` and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login information for `@publisher_login` and `publisher_password`. This creates a Snapshot Agent job for the publication.

    > [!IMPORTANT]  
    >  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine (SQL Server Configuration Manager)](../../../database-engine/configure-windows/configure-sql-server-encryption.md).

1. Add articles to the publication. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).

1. Start the Snapshot Agent job to generate the initial snapshot for this publication. For more information, see [Create and Apply the Initial Snapshot](../../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).

#### Create a merge publication

1. At the Publisher, execute [sp_replicationdboption (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md) to enable publication of the current database using merge replication.

1. At the Publisher on the publication database, execute [sp_addmergepublication (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md). Specify a name for the publication for `@publication` and any other publication options. This defines the publication.

    > [!NOTE]  
    >  Publication names cannot include the following characters:  
    >  
    > `%`, `*`, `[`, `]`, `|`, `:`, `"`, `?`, `\`, `/`, `<`, or `>`.

1. At the Publisher, execute [sp_addpublication_snapshot (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md). Specify the publication name used in step 2 for `@publication` and the Windows credentials under which the Snapshot Agent runs for `@snapshot_job_name` and `@password`. If the agent will use SQL Server Authentication when connecting to the Publisher, you must also specify a value of `0` for `@publisher_security_mode` and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login information for `@publisher_login` and `@publisher_password`. This creates a Snapshot Agent job for the publication.

    > [!IMPORTANT]  
    >  When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *job_login* and *job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Enable Encrypted Connections to the Database Engine (SQL Server Configuration Manager)](../../../database-engine/configure-windows/configure-sql-server-encryption.md).

1. Add articles to the publication. For more information, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).

1. Start the Snapshot Agent job to generate the initial snapshot for this publication. For more information, see [Create and Apply the Initial Snapshot](../../../relational-databases/replication/create-and-apply-the-initial-snapshot.md).

### <a id="TsqlExample"></a> Example (Transact-SQL)

This example creates a transactional publication. Scripting variables are used to pass Windows credentials that are needed to create jobs for the Snapshot Agent and Log Reader Agent.

:::code language="sql" source="../codesnippet/tsql/create-a-publication_1.sql":::

This example creates a merge publication. Scripting variables are used to pass Windows credentials that are needed to create the job for the Snapshot Agent.

:::code language="sql" source="../codesnippet/tsql/create-a-publication_2.sql":::

## <a id="RMOProcedure"></a> Use Replication Management Objects (RMO)

You can create publications programmatically by using Replication Management Objects (RMO). The RMO classes that you use to create a publication depend on the type of publication you create.

#### Create a snapshot or transactional publication

1. Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.

1. Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationDatabase> class for the publication database, set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the instance of <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1, and call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method. If <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> returns `false`, verify that the database exists.

1. If the <xref:Microsoft.SqlServer.Replication.ReplicationDatabase.EnabledTransPublishing%2A> property is `false`, set it to `true`.

1. For a transactional publication, check the value of the <xref:Microsoft.SqlServer.Replication.ReplicationDatabase.LogReaderAgentExists%2A> property. If this property is `true`, a Log Read Agent job already exists for this database. If this property is `false`, do the following:

   - Set the <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> fields of <xref:Microsoft.SqlServer.Replication.ReplicationDatabase.LogReaderAgentProcessSecurity%2A> to provide the credentials for the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows account under which the Log Reader Agent runs.

     > [!NOTE]  
     > Setting <xref:Microsoft.SqlServer.Replication.ReplicationDatabase.LogReaderAgentProcessSecurity%2A> is not required when the publication is created by a member of the **sysadmin** fixed server role. In this case, the agent will impersonate the SQL Server Agent account. For more information, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md).

   - (Optional) Set the <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> or <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SecureSqlStandardPassword%2A> fields of <xref:Microsoft.SqlServer.Replication.ReplicationDatabase.LogReaderAgentPublisherSecurity%2A> when using SQL Server Authentication to connect to the Publisher.

   - Call the <xref:Microsoft.SqlServer.Replication.ReplicationDatabase.CreateLogReaderAgent%2A> method to create the Log Reader Agent job for the database.

1. Create an instance of the <xref:Microsoft.SqlServer.Replication.TransPublication> class, and set the following properties for this object:

   - The <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.

   - The name of the published database for <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A>.

   - A name for the publication for <xref:Microsoft.SqlServer.Replication.Publication.Name%2A>.

   - A `Type` (which is of type <xref:Microsoft.SqlServer.Replication.PublicationType>) of either <xref:Microsoft.SqlServer.Replication.PublicationType.Transactional> or <xref:Microsoft.SqlServer.Replication.PublicationType.Snapshot>.

   - The <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> fields of <xref:Microsoft.SqlServer.Replication.Publication.SnapshotGenerationAgentProcessSecurity%2A> to provide the credentials for the Windows account under which the Snapshot Agent runs. This account is also used when the Snapshot Agent makes connections to the local Distributor and for any remote connections when using Windows Authentication.

     > [!NOTE]  
     >  Setting <xref:Microsoft.SqlServer.Replication.Publication.SnapshotGenerationAgentProcessSecurity%2A> is not required when the publication is created by a member of the **sysadmin** fixed server role. In this case, the agent will impersonate the SQL Server Agent account. For more information, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md).

   - (Optional) The <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardLogin%2A> and <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SqlStandardPassword%2A> or <xref:Microsoft.SqlServer.Replication.ConnectionSecurityContext.SecureSqlStandardPassword%2A> fields of <xref:Microsoft.SqlServer.Replication.Publication.SnapshotGenerationAgentPublisherSecurity%2A> when using SQL Server Authentication to connect to the Publisher.

   - (Optional) Use the inclusive logical OR operator (`|` in Visual C# and `Or` in Visual Basic) and the exclusive logical OR operator (`^` in Visual C# and `Xor` in Visual Basic) to set the <xref:Microsoft.SqlServer.Replication.PublicationAttributes> values for the <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> property.

   - (Optional) The name of the Publisher for <xref:Microsoft.SqlServer.Replication.TransPublication.PublisherName%2A> when the Publisher is a non-SQL Server Publisher.

1. Call the <xref:Microsoft.SqlServer.Replication.Publication.Create%2A> method to create the publication.

   > [!IMPORTANT]  
   >  When configuring a Publisher with a remote Distributor, the values supplied for all properties, including <xref:Microsoft.SqlServer.Replication.Publication.SnapshotGenerationAgentProcessSecurity%2A>, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before calling the <xref:Microsoft.SqlServer.Replication.Publication.Create%2A> method. For more information, see [Enable Encrypted Connections to the Database Engine (SQL Server Configuration Manager)](../../../database-engine/configure-windows/configure-sql-server-encryption.md).

1. Call the <xref:Microsoft.SqlServer.Replication.Publication.CreateSnapshotAgent%2A> method to create the Snapshot Agent job for the publication.

#### Create a merge publication

1. Create a connection to the Publisher by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> class.

1. Create an instance of the <xref:Microsoft.SqlServer.Replication.ReplicationDatabase> class for the publication database, set the <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A> property to the instance of <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1, and call the <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> method. If <xref:Microsoft.SqlServer.Replication.ReplicationObject.LoadProperties%2A> returns `false`, verify that the database exists.

1. If <xref:Microsoft.SqlServer.Replication.ReplicationDatabase.EnabledMergePublishing%2A> Property is `false`, set it to `true`, and call <xref:Microsoft.SqlServer.Replication.ReplicationObject.CommitPropertyChanges%2A>.

1. Create an instance of the <xref:Microsoft.SqlServer.Replication.MergePublication> class, and set the following properties for this object:

   - The <xref:Microsoft.SqlServer.Management.Common.ServerConnection> from step 1 for <xref:Microsoft.SqlServer.Replication.ReplicationObject.ConnectionContext%2A>.

   - The name of the published database for <xref:Microsoft.SqlServer.Replication.Publication.DatabaseName%2A>.

   - A name for the publication for <xref:Microsoft.SqlServer.Replication.Publication.Name%2A>.

   - The <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Login%2A> and <xref:Microsoft.SqlServer.Replication.IProcessSecurityContext.Password%2A> fields of <xref:Microsoft.SqlServer.Replication.Publication.SnapshotGenerationAgentProcessSecurity%2A> to provide the credentials for the Windows account under which the Snapshot Agent runs. This account is also used when the Snapshot Agent makes connections to the local Distributor and for any remote connections when using Windows Authentication.

     > [!NOTE]  
     >  Setting <xref:Microsoft.SqlServer.Replication.Publication.SnapshotGenerationAgentProcessSecurity%2A> is not required when the publication is created by a member of the **sysadmin** fixed server role. For more information, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md).

   - (Optional) Use the inclusive logical OR operator (`|` in Visual C# and `Or` in Visual Basic) and the exclusive logical OR operator (`^` in Visual C# and `Xor` in Visual Basic) to set the <xref:Microsoft.SqlServer.Replication.PublicationAttributes> values for the <xref:Microsoft.SqlServer.Replication.Publication.Attributes%2A> property.

1. Call the <xref:Microsoft.SqlServer.Replication.Publication.Create%2A> method to create the publication.

    > [!IMPORTANT]  
    >  When configuring a Publisher with a remote Distributor, the values supplied for all properties, including <xref:Microsoft.SqlServer.Replication.Publication.SnapshotGenerationAgentProcessSecurity%2A>, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before calling the <xref:Microsoft.SqlServer.Replication.Publication.Create%2A> method. For more information, see [Enable Encrypted Connections to the Database Engine (SQL Server Configuration Manager)](../../../database-engine/configure-windows/configure-sql-server-encryption.md).

1. Call the <xref:Microsoft.SqlServer.Replication.Publication.CreateSnapshotAgent%2A> method to create the Snapshot Agent job for the publication.

### <a id="PShellExample"></a> Examples (RMO)

This example enables the `AdventureWorks` database for transactional publishing, defines a Log Reader Agent job, and creates the `AdvWorksProductTran` publication. An article must be defined for this publication. The Windows account credentials that are needed to create the Log Reader Agent job and the Snapshot Agent job are passed at runtime. To learn how to use RMO to define snapshot and transactional articles, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).

[!code-cs[HowTo#rmo_CreateTranPub](../../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_createtranpub)]

[!code-vb[HowTo#rmo_vb_CreateTranPub](../../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_createtranpub)]

This example enables the `AdventureWorks` database for merge publishing and creates the `AdvWorksSalesOrdersMerge` publication. Articles must still be defined for this publication. The Windows account credentials that are needed to create the Snapshot Agent job are passed at runtime. To learn how to use RMO to define merge articles, see [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).

[!code-cs[HowTo#rmo_CreateMergePub](../../../relational-databases/replication/codesnippet/csharp/rmohowto/rmotestevelope.cs#rmo_createmergepub)]

[!code-vb[HowTo#rmo_vb_CreateMergePub](../../../relational-databases/replication/codesnippet/visualbasic/rmohowtovb/rmotestenv.vb#rmo_vb_createmergepub)]

## Related content

- [Use sqlcmd with Scripting Variables](../../../tools/sqlcmd/sqlcmd-use-scripting-variables.md)
- [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)
- [Replication Management Objects Concepts](../../../relational-databases/replication/concepts/replication-management-objects-concepts.md)
- [Define an Article](../../../relational-databases/replication/publish/define-an-article.md)
- [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md)
- [Configure Distribution](../../../relational-databases/replication/configure-distribution.md)
- [Secure the Distributor](../../../relational-databases/replication/security/secure-the-distributor.md)
- [Secure the Publisher](../../../relational-databases/replication/security/secure-the-publisher.md)
