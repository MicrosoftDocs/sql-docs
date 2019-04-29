---
title: "Manage Metadata When Making a Database Available on Another Server | Microsoft Docs"
ms.custom: ""
ms.date: "08/24/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "cross-database queries [SQL Server]"
  - "logins [SQL Server], recreating on another server instance"
  - "triggers [SQL Server], DLL"
  - "user-defined error messages [SQL Server]"
  - "permissions [SQL Server], metadata access"
  - "Full-Text Engine [SQL Server]"
  - "metadata [SQL Server], databases available to other instances"
  - "jobs [SQL Server Agent], recreating on another server instance"
  - "failover [SQL Server], managing metadata"
  - "event notifications [SQL Server], metadata"
  - "database mirroring [SQL Server], metadata"
  - "startup options [SQL Server]"
  - "restoring [SQL Server], onto another server instance"
  - "linked servers [SQL Server], metadata"
  - "WMI Provider for Server Events, metadata"
  - "attaching databases [SQL Server]"
  - "log shipping [SQL Server], metadata"
  - "encryption [SQL Server], metadata"
  - "server configuration [SQL Server]"
  - "distributed queries [SQL Server], metadata"
  - "extended stored procedures [SQL Server], metadata"
  - "credentials [SQL Server], metadata"
  - "copying databases"
ms.assetid: 5d98cf2a-9fc2-4610-be72-b422b8682681
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Manage Metadata When Making a Database Available on Another Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This article is relevant in the following situations:  
  
-   Configuring the availability replicas of an [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] availability group.  
  
-   Setting up database mirroring for a database.  
  
-   When preparing to change roles between primary and secondary servers in a log shipping configuration.  
  
-   Restoring a database to another server instance.  
  
-   Attaching a copy of a database on another server instance.  
  
 Some applications depend on information, entities, and/or objects that are outside of the scope of a single user database. Typically, an application has dependencies on the **master** and **msdb** databases, and also on the user database. Anything stored outside of a user database that is required for the correct functioning of that database must be made available on the destination server instance. For example, the logins for an application are stored as metadata in the **master** database, and they must be re-created on the destination server. If an application or database maintenance plan depends on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs, whose metadata is stored in the **msdb** database, you must re-create those jobs on the destination server instance. Similarly, the metadata for a server-level trigger is stored in **master**.  
  
 When you move the database for an application to another server instance, you must re-create all the metadata of the dependant entities and objects in **master** and **msdb** on the destination server instance. For example, if a database application uses server-level triggers, just attaching or restoring the database on the new system is not enough. The database will not work as expected unless you manually re-create the metadata for those triggers in the **master** database.  
  
##  <a name="information_entities_and_objects"></a> Information, Entities, and Objects That Are Stored Outside of User Databases  
 The remainder of this article summarizes the potential issues that might affect a database that is being made available on another server instance. You might have to re-create one or more of the types of information, entities, or objects listed in the following list. To see a summary, click the link for the item.  
  
-   [Server configuration settings](#server_configuration_settings)  
  
-   [Credentials](#credentials)  
  
-   [Cross-database queries](#cross_database_queries)  
  
-   [Database ownership](#database_ownership)  
  
-   [Distributed queries/linked servers](#distributed_queries_and_linked_servers)  
  
-   [Encrypted data](#encrypted_data)  
  
-   [User-defined error messages](#user_defined_error_messages)  
  
-   [Event notifications and Windows Management Instrumentation (WMI) events (at server level)](#event_notif_and_wmi_events)  
  
-   [Extended stored procedures](#extended_stored_procedures)  
  
-   [Full-text engine for SQL Server properties](#ifts_service_properties)  
  
-   [Jobs](#jobs)  
  
-   [Logins](#logins)  
  
-   [Permissions](#permissions)  
  
-   [Replication settings](#replication_settings)  
  
-   [Service Broker applications](#sb_applications)  
  
-   [Startup procedures](#startup_procedures)  
  
-   [Triggers (at server level)](#triggers)  
  
##  <a name="server_configuration_settings"></a> Server Configuration Settings  
 [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions selectively install and starts key services and features. This helps reduce the attackable surface area of a system. In the default configuration of new installations, many features are not enabled. If the database relies on any service or feature that is off by default, this service or feature must be enabled on the destination server instance.  
  
 For more information about these settings and enabling or disabling them, see [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
  
##  <a name="credentials"></a> Credentials  
 A credential is a record that contains the authentication information that is required to connect to a resource outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Most credentials consist of a Windows login and password.  
  
 For more information about this feature, see [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md).  
  
> **NOTE:** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Proxy accounts use credentials. To learn the credential ID of a proxy account, use the [sysproxies](../../relational-databases/system-tables/dbo-sysproxies-transact-sql.md) system table.  
  
  
##  <a name="cross_database_queries"></a> Cross-Database Queries  
 The DB_CHAINING and TRUSTWORTHY database options are OFF by default. If either of these are set to ON for the original database, you may have to enable them on the database on the destination server instance. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
 Attach-and-detach operations disable cross-database ownership chaining for the database. For information about how to enable chaining, see [cross db ownership chaining Server Configuration Option](../../database-engine/configure-windows/cross-db-ownership-chaining-server-configuration-option.md).  
  
 For more information, see also [Set Up a Mirror Database to Use the Trustworthy Property &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/set-up-a-mirror-database-to-use-the-trustworthy-property-transact-sql.md)  
  
  
##  <a name="database_ownership"></a> Database Ownership  
 When a database is restored on another computer, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or Windows user who initiated the restore operation becomes the owner of the new database automatically. When the database is restored, the system administrator or the new database owner can change database ownership.  
  
##  <a name="distributed_queries_and_linked_servers"></a> Distributed Queries and Linked Servers  
 Distributed queries and linked servers are supported for OLE DB applications. Distributed queries access data from multiple heterogeneous data sources on either the same or different computers. A linked server configuration enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to execute commands against OLE DB data sources on remote servers. For more information about these features, see [Linked Servers &#40;Database Engine&#41;](../../relational-databases/linked-servers/linked-servers-database-engine.md).  
  
  
##  <a name="encrypted_data"></a> Encrypted Data  
 If the database you are making available on another server instance contains encrypted data and if the database master key is protected by the service master key on the original server, it might be necessary to re-create the service master key encryption. The *database master key* is a symmetric key that is used to protect the private keys of certificates and asymmetric keys in an encrypted database. When created, the database master key is encrypted by using the Triple DES algorithm and a user-supplied password.  
  
 To enable the automatic decryption of the database master key on a server instance, a copy of this key is encrypted by using the service master key. This encrypted copy is stored in both the database and in **master**. Typically, the copy stored in **master** is silently updated whenever the master key is changed. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] first tries to decrypt the database master key with the service master key of the instance. If that decryption fails, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] searches the credential store for master key credentials that have the same family GUID as the database for which it requires the master key. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] then tries to decrypt the database master key with each matching credential until the decryption succeeds or there are no more credentials. A master key that is not encrypted by the service master key must be opened by using the OPEN MASTER KEY statement and a password.  
  
 When an encrypted database is copied, restored, or attached to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a copy of the database master key encrypted by the service master key is not stored in **master** on the destination server instance. On the destination server instance, you must open the master key of the database. To open the master key, execute the following statement: OPEN MASTER KEY DECRYPTION BY PASSWORD **='**_password_**'**. We recommend that you then enable automatic decryption of the database master key by executing the following statement: ALTER MASTER KEY ADD ENCRYPTION BY SERVICE MASTER KEY. This ALTER MASTER KEY statement provisions the server instance with a copy of the database master key that is encrypted with the service master key. For more information, see [OPEN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/open-master-key-transact-sql.md) and [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md).  
  
 For information about how to enable automatic decryption of the database master key of a mirror database, see [Set Up an Encrypted Mirror Database](../../database-engine/database-mirroring/set-up-an-encrypted-mirror-database.md).  
  
 For more information, see also:  
  
-   [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
-   [Set Up an Encrypted Mirror Database](../../database-engine/database-mirroring/set-up-an-encrypted-mirror-database.md)  
  
-   [Create Identical Symmetric Keys on Two Servers](../../relational-databases/security/encryption/create-identical-symmetric-keys-on-two-servers.md)  
  
  
##  <a name="user_defined_error_messages"></a> User-defined Error Messages  
 User-defined error messages reside in the [sys.messages](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md) catalog view. This catalog view is stored in **master**. If a database application depends on user-defined error messages and the database is made available on another server instance, use [sp_addmessage](../../relational-databases/system-stored-procedures/sp-addmessage-transact-sql.md) to add those user-defined messages on the destination server instance.  

  
##  <a name="event_notif_and_wmi_events"></a> Event Notifications and Windows Management Instrumentation (WMI) Events (at Server Level)  
  
### Server-Level Event Notifications  
 Server-level event notifications are stored in **msdb**. Therefore, if a database application relies on a server-level event notification, that event notification must be re-created on the destination server instance. To view the event notifications on a server instance, use the [sys.server_event_notifications](../../relational-databases/system-catalog-views/sys-server-event-notifications-transact-sql.md) catalog view. For more information, see [Event Notifications](../../relational-databases/service-broker/event-notifications.md).  
  
 Additionally, event notifications are delivered by using [!INCLUDE[ssSB](../../includes/sssb-md.md)]. Routes for incoming messages are not included in the database that contains a service. Instead, explicit routes are stored in **msdb**. If your service uses an explicit route in the **msdb** database to route incoming messages to the service, when you attach a database in a different instance, you must re-create this route.  
  
### Windows Management Instrumentation (WMI) Events  
 The WMI Provider for Server Events lets you use the Windows Management Instrumentation (WMI) to monitor events in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Any application that relies on server-level events exposed through the WMI provider on which a database relies must be defined the computer of the destination server instance. WMI Event provider creates event notifications with a target service that is defined in **msdb**.  
  
> **NOTE:** For more information, see [WMI Provider for Server Events Concepts](../../relational-databases/wmi-provider-server-events/wmi-provider-for-server-events-concepts.md).  
  
 **To create a WMI alert using SQL Server Management Studio**  
  
-   [Create a WMI Event Alert](../../ssms/agent/create-a-wmi-event-alert.md)  
  
### How Event Notifications Work for a Mirrored Database  
 Cross-database delivery of event notifications that involves a mirrored database is remote, by definition, because the mirrored database can fail over. [!INCLUDE[ssSB](../../includes/sssb-md.md)] provides special support for mirrored databases, in the form of *mirrored routes*. A mirrored route has two addresses: one for the principal server instance and one for the mirror server instance.  
  
 By setting up mirrored routes, you make [!INCLUDE[ssSB](../../includes/sssb-md.md)] routing aware of database mirroring. The mirrored routes enable [!INCLUDE[ssSB](../../includes/sssb-md.md)] to transparently redirect conversations to the current principal server instance. For example, consider a service, Service_A, which is hosted by a mirrored database, Database_A. Assume that you need another service, Service_B, which is hosted by Database_B, to have a dialog with Service_A. For this dialog to be possible, Database_B must contain a mirrored route for Service_A. In addition, Database_A must contain a nonmirrored TCP transport route to Service_B, which, unlike a local route, remains valid after failover. These routes enable ACKs to come back after a failover. Because the service of the sender is always named in the same manner, the route must specify the broker instance.  
  
 The requirement for mirrored routes applies for regardless of whether the service in the mirrored database is the initiator service or the target service:  
  
-   If target service is in the mirrored database, the initiator service must have a mirrored route back to the target. However, the target can have a regular route back to initiator.  
  
-   If initiator service is in the mirrored database, the target service must have a mirrored route back to initiator to deliver acknowledgements and replies. However, the initiator can have a regular route to the target.  
  
  
##  <a name="extended_stored_procedures"></a> Extended Stored Procedures  
  
> **IMPORTANT!** [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CLR Integration](../../relational-databases/clr-integration/common-language-runtime-integration-overview.md) instead.  
  
 Extended stored procedures are programmed by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Extended Stored Procedure API. A member of the **sysadmin** fixed server role can register an extended stored procedure with an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and grant permission to users to execute the procedure. Extended stored procedures can be added only to the **master** database.  
  
 Extended stored procedures run directly in the address space of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and they may produce memory leaks or other problems that reduce the performance and reliability of the server. You should consider storing extended stored procedures in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is separate from the instance that contains the referenced data. You should also consider using distributed queries to access the database.  
  
  > [!IMPORTANT]
  > Before adding extended stored procedures to the server and granting EXECUTE permissions to other users, the system administrator should thoroughly review each extended stored procedure to make sure that it does not contain harmful or malicious code.  
  
 For more information, see [GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md), [DENY Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-object-permissions-transact-sql.md), and [REVOKE Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-object-permissions-transact-sql.md).  
  
  
##  <a name="ifts_service_properties"></a> Full-Text Engine for SQL Server Properties  
 Properties are set on the Full-Text Engine by [sp_fulltext_service](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md). Make sure that the destination server instance has the required settings for these properties. For more information about these properties, see [FULLTEXTSERVICEPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/fulltextserviceproperty-transact-sql.md).  
  
 Additionally, if the [word breakers and stemmers](../../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md) component or [full-text search filters](../../relational-databases/search/configure-and-manage-filters-for-search.md) component have different versions on the original and destination server instances, full-text index and queries may behave differently. Also, the [thesaurus](../../relational-databases/search/configure-and-manage-thesaurus-files-for-full-text-search.md) is stored in instance-specific files. You must either transfer a copy of those files to an equivalent location on the destination server instance or re-create them on new instance.  
  
> **NOTE:** When you attach a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database that contains full-text catalog files onto a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] server instance, the catalog files are attached from their previous location along with the other database files, the same as in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. For more information, see [Upgrade Full-Text Search](../../relational-databases/search/upgrade-full-text-search.md).  
  
 For more information, see also:  
  
-   [Back Up and Restore Full-Text Catalogs and Indexes](../../relational-databases/search/back-up-and-restore-full-text-catalogs-and-indexes.md)  
  
-   [Database Mirroring and Full-Text Catalogs &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-and-full-text-catalogs-sql-server.md)  

  
##  <a name="jobs"></a> Jobs  
 If the database relies on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs, you will have to re-create them on the destination server instance. Jobs depend on their environments. If you plan to re-create an existing job on the destination server instance, the destination server instance might have to be modified to match the environment of that job on the original server instance. The following environmental factors are significant:  
  
-   The login used by the job  
  
     To create or execute [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs, you must first add any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins required by the job to the destination server instance. For more information, see [Configure a User to Create and Manage SQL Server Agent Jobs](../../ssms/agent/configure-a-user-to-create-and-manage-sql-server-agent-jobs.md).  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service startup account  
  
     The service startup account defines the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account in which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs and its network permissions. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs as a specified user account. The context of the Agent service affects the settings for the job and its run environment. The account must have access to the resources, such as network shares, required by the job. For information about how to select and modify the service startup account, see [Select an Account for the SQL Server Agent Service](../../ssms/agent/select-an-account-for-the-sql-server-agent-service.md).  
  
     To operate correctly, the service startup account must be configured to have the correct domain, file system, and registry permissions. Also, a job might require a shared network resource that must be configured for the service account. For information, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service, which is associated with a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], has its own registry hive, and its jobs typically have dependencies on one or more of the settings in this registry hive. To behave as intended, a job requires those registry settings. If you use a script to re-create a job in another [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service, its registry might not have the correct settings for that job. For re-created jobs to behave correctly on a destination server instance, the original and destination [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent services should have the same registry settings.  
  
    > [!CAUTION]  
    >  Changing registry settings on the destination [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to handle a re-created job could be problematic if the current settings are required by other jobs. Furthermore, incorrectly editing the registry can severely damage your system. Before you make changes to the registry, we recommend that you back up any valued data on the computer.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent Proxies  
  
     A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy defines the security context for a specified job step. For a job to run on the destination server instance, all the proxies it requires must be manually re-created on that instance. For more information, see [Create a SQL Server Agent Proxy](../../ssms/agent/create-a-sql-server-agent-proxy.md) and [Troubleshoot Multiserver Jobs That Use Proxies](../../ssms/agent/troubleshoot-multiserver-jobs-that-use-proxies.md).  
  
 For more information, see also:  
  
-   [Implement Jobs](../../ssms/agent/implement-jobs.md)  
  
-   [Management of Logins and Jobs After Role Switching &#40;SQL Server&#41;](../../sql-server/failover-clusters/management-of-logins-and-jobs-after-role-switching-sql-server.md) (for database mirroring)  
  
-   [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md) (when you install an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)])  
  
-   [Configure SQL Server Agent](../../ssms/agent/configure-sql-server-agent.md) (when you install an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)])  
  
-   [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md)  
  
 **To view existing jobs and their properties**  
  
-   [Monitor Job Activity](../../ssms/agent/monitor-job-activity.md)  
  
-   [sp_help_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-job-transact-sql.md)  
  
-   [View Job Step Information](../../ssms/agent/view-job-step-information.md)  
  
-   [dbo.sysjobs &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysjobs-transact-sql.md)  
  
 **To create a job**  
  
-   [Create a Job](../../ssms/agent/create-a-job.md)  
  
-   [Create a Job](../../ssms/agent/create-a-job.md)  
  
#### Best Practices for Using a Script to Re-create a Job  
 We recommend that you start by scripting a simple job, re-creating the job on the other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service, and running the job to see whether it works as intended. This will let you identify incompatibilities and try to resolve them. If a scripted job does not work as intended in its new environment, we recommend that you create an equivalent job that works correctly in that environment.  
  

##  <a name="logins"></a> Logins  
 Logging into an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. This login is used in the authentication process that verifies whether the principal can connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A database user for which the corresponding [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is undefined or is incorrectly defined on a server instance cannot log in to the instance. Such a user is said to be an *orphaned user* of the database on that server instance. A database user can become orphaned if after a database is restored, attached, or copied to a different instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 To generate a script for some or all the objects in the original copy of the database, you can use the Generate Scripts Wizard, and in the **Choose Script Options** dialog box, set the **Script Logins** option to **True**.  
  
> **NOTE:** For information about how to set up logins for a mirrored database, see [Set Up Login Accounts for Database Mirroring or Always On Availability Groups (SQL Server)](../../database-engine/database-mirroring/set-up-login-accounts-database-mirroring-always-on-availability.md) and [Management of Logins and Jobs After Role Switching &#40;SQL Server&#41;](../../sql-server/failover-clusters/management-of-logins-and-jobs-after-role-switching-sql-server.md).  
  
  
##  <a name="permissions"></a> Permissions  
 The following types of permissions might be affected when a database is made available on another server instance.  
  
-   GRANT, REVOKE, or DENY permissions on system objects  
  
-   GRANT, REVOKE, or DENY permissions on server instance (*server-level permissions*)  
  
### GRANT, REVOKE, and DENY Permissions on System Objects  
 Permissions on system objects such as stored procedures, extended stored procedures, functions, and views, are stored in the **master** database and must be configured on the destination server instance.  
  
 To generate a script for some or all the objects in the original copy of the database, you can use the Generate Scripts Wizard, and in the **Choose Script Options** dialog box, set the **Script Object-Level Permissions** option to **True**.  
  
   > [!IMPORTANT]
   > If you script logins, the passwords are not scripted. If you have logins that use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you have to modify the script on the destination.  
  
 System objects are visible in the [sys.system_objects](../../relational-databases/system-catalog-views/sys-system-objects-transact-sql.md) catalog view. The permissions on system objects are visible in the [sys.database_permissions](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md) catalog view in the **master** database. For information about querying these catalog views and granting system-object permissions, see [GRANT System Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-system-object-permissions-transact-sql.md). For more information, see [REVOKE System Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-system-object-permissions-transact-sql.md) and [DENY System Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-system-object-permissions-transact-sql.md).  
  
### GRANT, REVOKE, and DENY Permissions on a Server Instance  
 Permissions at the server scope are stored in the **master** database and must be configured on the destination server instance. For information about the server permissions of a server instance, query the [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md) catalog view, for information about server principals query the [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)s catalog view, and for information about membership of server roles query the [sys.server_role_members](../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md) catalog view.  
  
 For more information, see [GRANT Server Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-server-permissions-transact-sql.md), [REVOKE Server Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-server-permissions-transact-sql.md), and [DENY Server Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-server-permissions-transact-sql.md).  
  
#### Server-Level Permissions for a Certificate or Asymmetric Key  
 Server-level permissions cannot be granted directly to a certificate or asymmetric key. Instead, server-level permissions are granted to a mapped login that is created exclusively for a specific certificate or asymmetric key. Therefore, each certificate or asymmetric key that requires server-level permissions, requires its own *certificate-mapped login* or *asymmetric key-mapped login*. To grant server-level permissions for a certificate or asymmetric key, grant the permissions to its mapped login.  
  
> **NOTE:** A mapped login is used only for authorization of code signed with the corresponding certificate or asymmetric key. Mapped logins cannot be used for authentication.  
  
 The mapped login and its permissions both reside in **master**. If a certificate or asymmetric key resides in a database other than **master**, you must re-create it in **master** and map it to a login. If you move, copy, or restore the database to another server instance, you must re-create its certificate or asymmetric key in the **master** database of the destination server instance, map to a login, and grant the required server-level permissions to the login.  
  
 **To create a certificate or asymmetric key**  
  
-   [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)  
  
-   [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)  
  
 **To map a certificate or asymmetric key to a login**  
  
-   [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)  
  
 **To assign permissions to the mapped login**  
  
-   [GRANT Server Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-server-permissions-transact-sql.md)  
  
 For more information about certificates and asymmetric keys, see [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md).  
  
## Trustworthy Property
The TRUSTWORHTY database property is used to indicate whether this instance of SQL Server trusts the database and the contents within it. When a database is attached, by default and for security, this option is set to OFF, even if this option was set to ON on the original server. For more information about this property, see [TRUSTWORTHY database property](../security/trustworthy-database-property.md) and for information on turning this option ON, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  


##  <a name="replication_settings"></a> Replication Settings  
 If you restore a backup of a replicated database to another server or database, replication settings cannot be preserved. In this case, you must re-create all publications and subscriptions after backups are restored. To make this process easier, create scripts for your current replication settings and, also, for the enabling and disabling of replication. To help re-create your replication settings, copy these scripts and change the server name references to work for the destination server instance.  
  
 For more information, see [Back Up and Restore Replicated Databases](../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md), [Database Mirroring and Replication &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-and-replication-sql-server.md), and [Log Shipping and Replication &#40;SQL Server&#41;](../../database-engine/log-shipping/log-shipping-and-replication-sql-server.md).  
  
  
##  <a name="sb_applications"></a> Service Broker Applications  
 Many aspects of a [!INCLUDE[ssSB](../../includes/sssb-md.md)] application move with the database. However, some aspects of the application must be re-created or reconfigured in the new location.  By default and for security, when a database is attached from another server, the options for *is_broker_enabled* and *is_honoor_broker_priority_on* are set to OFF. For information about how to set these options ON, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
  
##  <a name="startup_procedures"></a> Startup Procedures  
 A startup procedure is a stored procedure that is marked for automatic execution and is executed every time [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts. If the database depends on any startup procedures, they must be defined on the destination server instance and be configured to be automatically executed at startup.  

  
##  <a name="triggers"></a> Triggers (at Server Level)  
 DDL triggers fire stored procedures in response to a variety of Data Definition Language (DDL) events. These events primarily correspond to [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that start with the keywords CREATE, ALTER, and DROP. Certain system stored procedures that perform DDL-like operations can also fire DDL triggers.  
  
 For more information about this feature, see [DDL Triggers](../../relational-databases/triggers/ddl-triggers.md).  
  
  
## See Also  
 [Contained Databases](../../relational-databases/databases/contained-databases.md)   
 [Copy Databases to Other Servers](../../relational-databases/databases/copy-databases-to-other-servers.md)   
 [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)   
 [Fail Over to a Log Shipping Secondary &#40;SQL Server&#41;](../../database-engine/log-shipping/fail-over-to-a-log-shipping-secondary-sql-server.md)   
 [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md)   
 [Set Up an Encrypted Mirror Database](../../database-engine/database-mirroring/set-up-an-encrypted-mirror-database.md)   
 [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md)   
 [Troubleshoot Orphaned Users &#40;SQL Server&#41;](../../sql-server/failover-clusters/troubleshoot-orphaned-users-sql-server.md)  
  
  
