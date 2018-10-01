---
title: "Create a Publication from an Oracle Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "publications [SQL Server replication], Oracle databases"
  - "Oracle publishing [SQL Server replication], configuring"
ms.assetid: b3812746-14b0-4b22-809e-b4a95e1c8083
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Create a Publication from an Oracle Database
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to create a publication from an Oracle database in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
-   **To create a publication from an Oracle database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   Before creating a publication, you must install Oracle software on the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor, and you must configure the Oracle database. For more information, see [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 Create a snapshot or transactional publication from an Oracle Database with the New Publication Wizard.  
  
 The first time you create a publication from an Oracle database, you must identify the Oracle Publisher at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor (you do not need to do this for subsequent publications from the same database.). Identifying the Oracle Publisher can be accomplished from the New Publication Wizard or the **Distributor Properties - \<Distributor>** dialog box; this topic shows the **Distributor Properties - \<Distributor>** dialog box.  
  
#### To identify the Oracle Publisher at the SQL Server Distributor  
  
1.  In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance that the Oracle Publisher will use as a Distributor, and then expand the server node.  
  
2.  Right-click the **Replication** folder, and then click **Distributor Properties**.  
  
3.  On the **Publishers** page of the **Distributor Properties - \<Distributor>** dialog box, click **Add**, and then click **Add Oracle Publisher**.  
  
4.  In the **Connect to Server** dialog box, click the **Options** button.  
  
5.  On the **Login** tab:  
  
    1.  Enter the Oracle database instance name or select **Browse for more** in the **Server instance** combo box.  
  
    2.  Select **Oracle Standard Authentication** (recommended) or **Windows Authentication**.  
  
         If you select **Windows Authentication**: the Oracle server must be configured to allow connections using Windows credentials (for more information, see the Oracle documentation); and you must be currently logged in with the same [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows account you specified for the replication administrative user schema.  
  
    3.  If you select **Oracle Standard Authentication**, enter the login and password of the replication administrative user schema you created on the Oracle Publisher during configuration.  
  
6.  On the **Connection Properties** tab, select a Publisher type of **Gateway** or **Complete**.  
  
     The **Complete** option is designed to provide snapshot and transactional publications with the complete set of supported features for Oracle publishing. The **Gateway** option provides specific design optimizations to improve performance for cases where replication serves as a gateway between systems. The **Gateway** option cannot be used if you plan to publish the same table in multiple transactional publications. A table can appear in at most one transactional publication and any number of snapshot publications if you select **Gateway**.  
  
7.  Click **Connect**, which creates a connection to the Oracle Publisher and configures it for replication. The **Connect to Server** dialog box closes and you are returned to the **Distributor Properties - \<Distributor>** dialog box.  
  
    > [!NOTE]  
    >  If there are any problems with the network configuration, you will receive an error at this point. If you experience problems connecting to the Oracle database, see the section "The SQL Server Distributor cannot connect to the Oracle database instance" in [Troubleshooting Oracle Publishers](../../../relational-databases/replication/non-sql/troubleshooting-oracle-publishers.md).  
  
8.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
#### To create a publication from an Oracle database  
  
1.  Connect to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance that the Oracle Publisher will use as a Distributor, and then expand the server node.  
  
2.  Expand the **Replication** folder.  
  
3.  Right-click the **Local Publications** folder, and then click **New Oracle Publication**.  
  
4.  On the **Oracle Publisher** page of the New Publication Wizard, select the Oracle Publisher. If the Oracle Publisher is not displayed, click **Add Oracle Publisher**, which takes you through the steps from the previous procedure.  
  
5.  On the **Publication Type** page, select **Snapshot publication** or **Transactional publication**.  
  
6.  On the **Articles** page, select the database objects you want to publish.  
  
     Optionally, filter out table columns by expanding a table and then clearing the checkbox for one or more columns. Click **Article Properties** to view and modify article properties and to specify alternative data type mappings if necessary. For more information about data type mappings, see [Specify Data Type Mappings for an Oracle Publisher](../../../relational-databases/replication/publish/specify-data-type-mappings-for-an-oracle-publisher.md).  
  
7.  On the **Filter Table Rows** page, optionally apply filters to publish a subset of data from one or more tables.  
  
8.  On the **Snapshot Agent** page, clear **Create a snapshot immediately** only if you have created all objects and added all required data in the subscription database.  
  
9. On the **Agent Security** page, specify credentials for the Snapshot Agent (for all publications) and the Log Reader Agent (for transactional publications). The agents run and make connections to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor using the context of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows account you specify. The agents make connections to the Oracle database using the context of the account you specified as the replication administrative user schema. For more information, see [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
     For more information about the permissions required by each agent, see [Replication Agent Security Model](../../../relational-databases/replication/security/replication-agent-security-model.md) and [Replication Security Best Practices](../../../relational-databases/replication/security/replication-security-best-practices.md).  
  
10. On the **Wizard Actions** page, optionally script the publication. For more information, see [Scripting Replication](../../../relational-databases/replication/scripting-replication.md).  
  
11. On the **Complete the Wizard** page, specify a name for the publication.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 After the Oracle database has been configured as a Publisher, you can create a transactional or snapshot publication the same way that you would from a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publisher, by using system stored procedures.  
  
#### To create an Oracle Publication  
  
1.  Configure the Oracle database as a Publisher. For more information, see [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
2.  If a remote Distributor does not exist, configure the remote Distributor. For more information, see [Configure Publishing and Distribution](../../../relational-databases/replication/configure-publishing-and-distribution.md).  
  
3.  At the remote Distributor that the Oracle Publisher will use, execute [sp_adddistpublisher &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md). Specify the Transparent Network Substrate (TNS) name of the Oracle database instance for **@publisher** and a value of **ORACLE** or **ORACLE GATEWAY** for **@publisher_type**. `Specify` the security mode used when connecting from the Oracle Publisher to the remote [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor as one of the following:  
  
    -   To use Oracle Standard Authentication, the default, specify a value of **0** for **@security_mode**, the login of the replication administrative user schema you created on the Oracle Publisher during configuration for **@login**, and the password for **@password**.  
  
        > [!IMPORTANT]  
        >  When possible, prompt users to enter security credentials at runtime. If you store credentials in a script file, you must secure the file to prevent unauthorized access.  
  
    -   To use Windows Authentication, specify a value of **1** for **@security_mode**.  
  
        > [!NOTE]  
        >  To use Windows Authentication, the Oracle server must be configured to allow connections using Windows credentials (for more information, see the Oracle documentation); and you must be currently logged in with the same Microsoft Windows account you specified for the replication administrative user schema..  
  
4.  Create a Log Reader Agent job for the publication database.  
  
    -   If you are unsure whether a Log Reader Agent job exists for a published database, execute [sp_helplogreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-helplogreader-agent-transact-sql.md) at the Distributor used by the Oracle Publisher on the distribution database. Specify the name of the Oracle Publisher for **@publisher**. If the result set is empty, then a Log Reader Agent job must be created.  
  
    -   If a Log Reader Agent job already exists for the publication database, proceed to step 5.  
  
    -   At the Distributor used by the Oracle Publisher on the distribution database, execute [sp_addlogreader_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlogreader-agent-transact-sql.md). Specify the Windows credentials under which the agent runs for **@job_login** and **@job_password**.  
  
        > [!NOTE]  
        >  The **@job_login** parameter must match the login supplied in step 3. Do not supply publisher security information. The Log Reader agent connects to the Publisher using the security information provided in step 3.  
  
5.  At the Distributor on the distribution database, execute [sp_addpublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-transact-sql.md) to create the publication. For more information, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md).  
  
6.  At the Distributor on the distribution database, execute [sp_addpublication_snapshot &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addpublication-snapshot-transact-sql.md). Specify the publication name used in step 4 for **@publication** and the Windows credentials under which the Snapshot Agent runs for **@job_name** and **@password**. To use Oracle Standard Authentication when connecting to the Publisher, you must also specify a value of **0** for **@publisher_security_mode** and the Oracle login information for **@publisher_login** and **@publisher_password**. This creates a Snapshot Agent job for the publication.  
  
## See Also  
 [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md)   
 [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Configure the Transaction Set Job for an Oracle Publisher &#40;Replication Transact-SQL Programming&#41;](../../../relational-databases/replication/administration/configure-the-transaction-set-job-for-an-oracle-publisher.md)   
 [Oracle Publishing Overview](../../../relational-databases/replication/non-sql/oracle-publishing-overview.md)   
 [Script to Grant Oracle Permissions](../../../relational-databases/replication/non-sql/script-to-grant-oracle-permissions.md)  
  
  
