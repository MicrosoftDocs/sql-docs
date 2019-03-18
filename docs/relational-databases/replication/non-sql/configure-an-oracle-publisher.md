---
title: "Configure an Oracle Publisher | Microsoft Docs"
ms.custom: ""
ms.date: "09/05/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Oracle publishing [SQL Server replication], configuring"
ms.assetid: 240c8416-c8e5-4346-8433-07e0f779099f
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Configure an Oracle Publisher
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Publications from Oracle Publishers are created in the same way typical snapshot and transactional publications are created, but prior to creating a publication from an Oracle Publisher, you must complete the following steps (steps one, three, and four are described in detail in this topic.):  
  
1.  Create a replication administrative user within the Oracle database using the supplied script.  
  
2.  For the tables that you publish, grant SELECT permission directly on each of them (not through a role) to the Oracle administrative user you created in step one.  
  
3.  Install the Oracle client software and OLE DB provider on the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor, and then stop and restart the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. If the Distributor is running on a 64-bit platform, you must use the 64-bit version of the Oracle OLE DB provider.  
  
4.  Configure the Oracle database as a Publisher at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor.  

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports the following heterogeneous scenarios for transactional and snapshot replication:  
  
-   Publishing data from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers.  

-   Publishing data to and from Oracle has the following restrictions:  

  | |2016 or earlier |2017 or later |
  |-------|-------|--------|
  |Replication from Oracle |Only support Oracle 10g or earlier |Only support Oracle 10g or earlier |
  |Replication to Oracle |Up to Oracle 12c |Not supported |

 Heterogeneous replication to non-SQL Server subscribers is deprecated. Oracle Publishing is deprecated. To move data, create solutions using change data capture and [!INCLUDE[ssIS](../../../includes/ssis-md.md)].  


 For a list of objects that can be replicated from an Oracle database, see [Design Considerations and Limitations for Oracle Publishers](../../../relational-databases/replication/non-sql/design-considerations-and-limitations-for-oracle-publishers.md).  
  
> [!NOTE]  
>  You must be a member of the **sysadmin** fixed server role to enable a Publisher or Distributor and to create an Oracle publication or a subscription from an Oracle publication.  
  
## Creating the Replication Administrative User Schema within the Oracle Database  
 Replication agents connect to the Oracle database and perform operations in the context of a user schema that you must create. This schema must be granted a number of permissions, which are listed in the next section. This schema owns all objects created by the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication process on the Oracle Publisher, with the exception of a public synonym, **MSSQLSERVERDISTRIBUTOR**. For more information about the objects created in the Oracle database, see [Objects Created on the Oracle Publisher](../../../relational-databases/replication/non-sql/objects-created-on-the-oracle-publisher.md).  
  
> [!NOTE]  
>  Dropping the **MSSQLSERVERDISTRIBUTOR** public synonym and the configured Oracle replication user with the **CASCADE** option removes all replication objects from the Oracle Publisher.  
  
 A sample script has been provided to aid in the setup of the Oracle replication user schema. The script is available in the following directory after installation of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: *\<drive>*:\\\Program Files\Microsoft SQL Server\\*\<InstanceName>*\MSSQL\Install\oracleadmin.sql. It is also included in the topic [Script to Grant Oracle Permissions](../../../relational-databases/replication/non-sql/script-to-grant-oracle-permissions.md).  
  
 Connect to the Oracle database using an account with DBA privileges and execute the script. This script prompts for the user and password for the replication administrative user schema as well as the default tablespace in which to create the objects (the tablespace must already exist in the Oracle database). For information about specifying other tablespaces for objects, see [Manage Oracle Tablespaces](../../../relational-databases/replication/non-sql/manage-oracle-tablespaces.md). Choose any user name and strong password, but make note of both because you must provide this information later when you configure the Oracle database as a Publisher. It is recommended that the schema be used only for objects required by replication; do not create tables to be published in this schema.  
  
### Creating the User Schema Manually  
 If you create the replication administrative user schema manually, you must grant the schema the following permissions, either directly or through a database role.  
  
-   CREATE PUBLIC SYNONYM and DROP PUBLIC SYNONYM  
  
-   CREATE PROCEDURE  
  
-   CREATE SEQUENCE  
  
-   CREATE SESSION  
  
 You must also grant the following permissions to the user directly (not through a role):  
  
-   CREATE ANY TRIGGER. This is required only both snapshot and transactional replication.  
  
-   CREATE TABLE  
  
-   CREATE VIEW  
  
## Installing and Configuring Oracle Client Networking Software on the SQL Server Distributor  
 You must install and configure Oracle client networking software and the Oracle OLE DB provider on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor, so that the Distributor can make connections to the Oracle Publisher. After installing the software, set the appropriate permissions on the folders in which the software is installed, and then stop and restart the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance to ensure that all settings are updated (permissions are described later, in the section "Setting Directory Permissions").  
  
> [!NOTE]  
>  The Oracle client networking software must be the most recent version available. Oracle recommends that users install the most recent versions of client software. The client software is therefore often a more recent version than the database software.  
  
 The most straightforward way to install and configure the client networking software is to use the Oracle Universal Installer and Net Configuration Assistant on the Oracle Client disk.  
  
 In the Oracle Universal Installer, you must supply the following information:  
  
|Information|Description|  
|-----------------|-----------------|  
|Oracle Home|This is the path to the install directory for the Oracle software. Accept the default (C:\oracle\ora90 or similar) or enter another path. For more information about the Oracle Home, see the section "Considerations for Oracle Home" later in this topic.|  
|Oracle home name|An alias for the Oracle home path.|  
|Installation type|In Oracle 10g, select the **Administrator** installation option.|  
  
 After the Oracle Universal Installer is complete, use the Net Configuration Assistant to configure network connectivity. You must supply four pieces of information to configure network connectivity. The Oracle database administrator configures the network configuration when setting up the database and listener and should be able to provide this information if you do not have it. You must do the following:  
  
|Action|Description|  
|------------|-----------------|  
|Identify the database|There are two methods for identifying the database. The first method uses the Oracle System Identifier (SID) and is available in every Oracle release. The second method uses the Service Name, which is available starting with Oracle release 8.0. Both methods use a value that is configured when the database is created and it is important that the client networking configuration use the same naming method that the administrator used when configuring the listener for the database.|  
|Identify a network alias for the database|You must specify a network alias, which is used to access the Oracle database. You also supply this alias when you identify the Oracle database as a Publisher at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor. The network alias is essentially a pointer to the remote SID or Service Name that was configured when the database was created; it has been referred to by several names in different Oracle releases and products, including Net Service Name and TNS Alias. SQL*Plus prompts for this alias as the "Host String" parameter when you log in.|  
|Select the network protocol|Select the appropriate protocols you would like to support. Most applications use TCP.|  
|Specify the host information to identify the database listener|The host is the name or DNS alias of the computer on which the Oracle listener is running, which is typically the same computer on which the database resides. For some protocols, you must provide additional information. For example, if you select TCP, you must supply the port on which the listener is listening for connection requests to the target database. The default TCP configuration uses port 1521.|  
  
### Setting Directory Permissions  
 The account under which the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service on the Distributor runs must be granted read and execute permissions for the directory (and all subdirectories) in which the Oracle client networking software is installed.  
  
### Testing Connectivity Between the SQL Server Distributor and the Oracle Publisher  
 Near the end of the Net Configuration Assistant, there might be an option to test the connection to the Oracle Publisher. Before you test the connection, ensure that the Oracle database instance is online and that the Oracle Listener is running. If the test is unsuccessful, contact the Oracle DBA responsible for the database to which you are trying to connect.  
  
 After you have made a successful connection to the Oracle Publisher, attempt to log in to the database using the account and password associated with the replication administrative user schema you created. The following must be performed while running under the same Windows account that the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service uses:  
  
1.  Click **Start**, and then click **Run**.  
  
2.  Type `cmd` and click **OK**.  
  
3.  At the command prompt, type:  
  
     `sqlplus <UserSchemaLogin>/<UserSchemaPassword>@<NetServiceName>`  
  
     For example: `sqlplus replication/$tr0ngPasswerd@Oracle90Server`  
  
4.  If the networking configuration was successful, the login succeeds and you will see a `SQL` prompt.  
  
5.  If you experience problems connecting to the Oracle database, see the section "The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor cannot connect to the Oracle database instance" in [Troubleshooting Oracle Publishers](../../../relational-databases/replication/non-sql/troubleshooting-oracle-publishers.md).  
  
### Considerations for Oracle Home  
 Oracle supports side-by-side installation of application binaries, but only one set of binaries can be used by replication at a given time. Each set of binaries is associated with an Oracle Home; the binaries are in the directory %ORACLE_HOME%\bin. You must ensure that the correct set of binaries (specifically the latest version of the client networking software) is used when replication makes connections to the Oracle Publisher.  
  
 Log into the Distributor with the accounts used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent service and set the appropriate environment variables. The %ORACLE_HOME% variable should be set to refer to the installation point you specified when you installed the client networking software. The %PATH% must include the %ORACLE_HOME% \bin directory as the first Oracle entry that is encountered. For information about setting environment variables, see the Windows documentation.  
  
## Configuring the Oracle Database as a Publisher at the SQL Server Distributor  
 Oracle Publishers always use a remote Distributor; you must configure an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to act as a Distributor for your Oracle Publisher (an Oracle Publisher can only use one Distributor, but a single Distributor can service more than one Oracle Publisher). After a Distributor is configured, identify the Oracle database instance as a Publisher at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor through [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], Transact-SQL, or Replication Management Objects (RMO). For more information about configuring a Distributor, see [Configure Distribution](../../../relational-databases/replication/configure-distribution.md).  
  
> [!NOTE]  
>  An Oracle Publisher cannot have the same name as its [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor or the same name as any of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Publishers using the same Distributor.  
  
 When you identify the Oracle database as a Publisher, you must choose an Oracle publishing option: Complete or Oracle Gateway. After a Publisher is identified, this option cannot be changed without dropping and reconfiguring the Publisher. The Complete option is designed to provide snapshot and transactional publications with the complete set of supported features for Oracle publishing. The Oracle Gateway option provides specific design optimizations to improve performance for cases where replication serves as a gateway between systems.  
  
 After the Oracle Publisher is identified at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor, replication creates a linked server with the same name as the TNS service name of the Oracle database. This linked server can be used only by replication. If you need to connect to the Oracle Publisher over a linked server connection, create another TNS service name, and then use this name when calling [sp_addlinkedserver &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md).  
  
 To configure an Oracle Publisher and create a publication, see [Create a Publication from an Oracle Database](../../../relational-databases/replication/publish/create-a-publication-from-an-oracle-database.md).  
  
## See Also  
 [Administrative Considerations for Oracle Publishers](../../../relational-databases/replication/non-sql/administrative-considerations-for-oracle-publishers.md)   
 [Data Type Mapping for Oracle Publishers](../../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md)   
 [Glossary of Terms for Oracle Publishing](../../../relational-databases/replication/non-sql/glossary-of-terms-for-oracle-publishing.md)   
 [Oracle Publishing Overview](../../../relational-databases/replication/non-sql/oracle-publishing-overview.md)  
  
  
