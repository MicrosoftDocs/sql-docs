---
title: "SQL Server Audit (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "11/21/2016"
ms.prod: sql
ms.prod_service: security
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "audit"
helpviewer_keywords: 
  - "SQL Server Audit"
  - "audits [SQL Server], SQL Server Audit"
ms.assetid: 0c1fca2e-f22b-4fe8-806f-c87806664f00
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# SQL Server Audit (Database Engine)
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

  *Auditing* an instance of the [!INCLUDE[ssDEnoversion](../../../includes/ssdenoversion-md.md)] or an individual database involves tracking and logging events that occur on the [!INCLUDE[ssDE](../../../includes/ssde-md.md)]. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit lets you create server audits, which can contain server audit specifications for server level events, and database audit specifications for database level events. Audited events can be written to the event logs or to audit files.  
  
[!INCLUDE[ssMIlimitation](../../../includes/sql-db-mi-limitation.md)]
  
 There are several levels of auditing for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], depending on government or standards requirements for your installation. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Audit provides the tools and processes you must have to enable, store, and view audits on various server and database objects.  
  
 You can record server audit action groups per-instance, and either database audit action groups or database audit actions per database. The audit event will occur every time that the auditable action is encountered.  
  
 All editions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] support server level audits. All editions support database level audits beginning with [!INCLUDE[ssSQL15_md](../../../includes/sssql15-md.md)] SP1. Prior to that, database level auditing was limited to Enterprise, Developer, and Evaluation editions. For more information, see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
> [!NOTE]  
>  This     topic applies to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  For [!INCLUDE[ssSDS](../../../includes/sssds-md.md)], see [Get started with SQL database auditing](https://azure.microsoft.com/documentation/articles/sql-database-auditing-get-started/).  
  
## SQL Server Audit Components  
 An *audit* is the combination of several elements into a single package for a specific group of server actions or database actions. The components of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit combine to produce an output that is called an audit, just as a report definition combined with graphics and data elements produces a report.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit uses *Extended Events* to help create an audit. For more information about Extended Events, see [Extended Events](../../../relational-databases/extended-events/extended-events.md).  
  
### SQL Server Audit  
 The *SQL Server Audit* object collects a single instance of server or database-level actions and groups of actions to monitor. The audit is at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance level. You can have multiple audits per [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance.  
  
 When you define an audit, you specify the location for the output of the results. This is the audit destination. The audit is created in a *disabled* state, and does not automatically audit any actions. After the audit is enabled, the audit destination receives data from the audit.  
  
### Server Audit Specification  
 The *Server Audit Specification* object belongs to an audit. You can create one server audit specification per audit, because both are created at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance scope.  
  
 The server audit specification collects many server-level action groups raised by the Extended Events feature. You can include *audit action groups* in a server audit specification. Audit action groups are predefined groups of actions, which are atomic events occurring in the [!INCLUDE[ssDE](../../../includes/ssde-md.md)]. These actions are sent to the audit, which records them in the target.  
  
 Server-level audit action groups are described in the topic [SQL Server Audit Action Groups and Actions](../../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).  
  
### Database Audit Specification  
 The *Database Audit Specification* object also belongs to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit. You can create one database audit specification per [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database per audit.  
  
 The database audit specification collects database-level audit actions raised by the Extended Events feature. You can add either audit action groups or audit events to a database audit specification. *Audit events* are the atomic actions that can be audited by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] engine. *Audit action groups* are predefined groups of actions. Both are at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database scope. These actions are sent to the audit, which records them in the target. Do not include server-scoped objects, such as the system views, in a user database audit specification.  
  
 Database-level audit action groups and audit actions are described in the topic [SQL Server Audit Action Groups and Actions](../../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).  
  
### Target  
 The results of an audit are sent to a target, which can be a file, the Windows Security event log, or the Windows Application event log. Logs must be reviewed and archived periodically to make sure that the target has sufficient space to write additional records.  
  
> [!IMPORTANT]  
>  Any authenticated user can read and write to the Windows Application event log. The Application event log requires lower permissions than the Windows Security event log and is less secure than the Windows Security event log.  
  
 Writing to the Windows Security log requires the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account to be added to the **Generate security audits** policy. By default, the Local System, Local Service, and Network Service are part of this policy. This setting can be configured by using the security policy snap-in (secpol.msc). Additionally, the **Audit object access** security policy must be enabled for both **Success** and **Failure**. This setting can be configured by using the security policy snap-in (secpol.msc). In [!INCLUDE[wiprlhext](../../../includes/wiprlhext-md.md)] or Windows Server 2008, you can set the more granular **application generated** policy from the command line by using the audit policy program (**AuditPol.exe)**. For more information about the steps to enable writing to the Windows Security log, see [Write SQL Server Audit Events to the Security Log](../../../relational-databases/security/auditing/write-sql-server-audit-events-to-the-security-log.md). For more information about the Auditpol.exe program, see Knowledge Base article 921469, [How to use Group Policy to configure detailed security auditing](https://support.microsoft.com/kb/921469/). The Windows event logs are global to the Windows operating system. For more information about the Windows event logs, see [Event Viewer Overview](https://go.microsoft.com/fwlink/?LinkId=101455). If you need more precise permissions on the audit, use the binary file target.  
  
 When you are saving audit information to a file, to help prevent tampering, you can restrict access to the file location in the following ways:  
  
-   The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Service Account must have both Read and Write permission.  
  
-   Audit Administrators typically require Read and Write permission. This assumes that the Audit Administrators are Windows accounts for administration of audit files, such as: copying them to different shares, backing them up, and so on.  
  
-   Audit Readers that are authorized to read audit files must have Read permission.  
  
 Even when the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] is writing to a file, other Windows users can read the audit file if they have permission. The [!INCLUDE[ssDE](../../../includes/ssde-md.md)] does not take an exclusive lock that prevents read operations.  
  
 Because the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] can access the file, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] logins that have CONTROL SERVER permission can use the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] to access the audit files. To record any user that is reading the audit file, define an audit on master.sys.fn_get_audit_file. This records the logins with CONTROL SERVER permission that have accessed the audit file through [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 If an Audit Administrator copies the file to a different location (for archive purposes, and so on), the ACLs on the new location should be reduced to the following permissions:  
  
-   Audit Administrator - Read / Write  
  
-   Audit Reader - Read  
  
 We recommend that you generate audit reports from a separate instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], such as an instance of [!INCLUDE[ssExpress](../../../includes/ssexpress-md.md)], to which only Audit Administrators or Audit Readers have access. By using a separate instance of the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] for reporting, you can help prevent unauthorized users from obtaining access to the audit record.  
  
 You can offer additional protection against unauthorized access by encrypting the folder in which the audit file is stored by using Windows BitLocker Drive Encryption or Windows Encrypting File System.  
  
 For more information about the audit records that are written to the target, see [SQL Server Audit Records](../../../relational-databases/security/auditing/sql-server-audit-records.md).  
  
## Overview of Using SQL Server Audit  
 You can use [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)] to define an audit. After the audit is created and enabled, the target will receive entries.  
  
 You can read the Windows event logs by using the **Event Viewer** utility in Windows. For file targets, you can use either the **Log File Viewer** in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or the [fn_get_audit_file](../../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md) function to read the target file.  
  
 The general process for creating and using an audit is as follows.  
  
1.  Create an audit and define the target.  
  
2.  Create either a server audit specification or database audit specification that maps to the audit. Enable the audit specification.  
  
3.  Enable the audit.  
  
4.  Read the audit events by using the Windows **Event Viewer**, **Log File Viewe**r, or the fn_get_audit_file function.  
  
 For more information, see [Create a Server Audit and Server Audit Specification](../../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md) and [Create a Server Audit and Database Audit Specification](../../../relational-databases/security/auditing/create-a-server-audit-and-database-audit-specification.md).  
  
## Considerations  
 In the case of a failure during audit initiation, the server will not start. In this case, the server can be started by using the **-f** option at the command line.  
  
 When an audit failure causes the server to shut down or not to start because ON_FAILURE=SHUTDOWN is specified for the audit, the MSG_AUDIT_FORCED_SHUTDOWN event will be written to the log. Because the shutdown will occur on the first encounter of this setting, the event will be written one time. This event is written after the failure message for the audit causing the shutdown. An administrator can bypass audit-induced shutdowns by starting [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in Single User mode using the **-m** flag. If you start in Single User mode, you will downgrade any audit where ON_FAILURE=SHUTDOWN is specified to run in that session as ON_FAILURE=CONTINUE. When [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is started by using the **-m** flag, the MSG_AUDIT_SHUTDOWN_BYPASSED message will be written to the error log.  
  
 For more information about service startup options, see [Database Engine Service Startup Options](../../../database-engine/configure-windows/database-engine-service-startup-options.md).  
  
### Attaching a Database with an Audit Defined  
 Attaching a database that has an audit specification and specifies a GUID that does not exist on the server will cause an *orphaned* audit specification. Because an audit with a matching GUID does not exist on the server instance, no audit events will be recorded. To correct this situation, use the ALTER DATABASE AUDIT SPECIFICATION command to connect the orphaned audit specification to an existing server audit. Or, use the CREATE SERVER AUDIT command to create a new server audit with the specified GUID.  
  
 You can attach a database that has an audit specification defined on it to another edition of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that does not support [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit, such as [!INCLUDE[ssExpress](../../../includes/ssexpress-md.md)] but it will not record audit events.  
  
### Database Mirroring and SQL Server Audit  
 A database that has a database audit specification defined and that uses database mirroring will include the database audit specification. To work correctly on the mirrored SQL instance, the following items must be configured:  
  
-   The mirror server must have an audit with the same GUID to enable the database audit specification to write audit records. This can be configured by using the command CREATE AUDIT WITH GUID **=**_\<GUID from source Server Audit_>.  
  
-   For binary file targets, the mirror server service account must have appropriate permissions to the location where the audit trail is being written.  
  
-   For Windows event log targets, the security policy on the computer where the mirror server is located must allow for service account access to the security or application event log.  
  
### Auditing Administrators  
 Members of the **sysadmin** fixed server role are identified as the **dbo** user in each database. To audit actions of the administrators, audit the actions of the **dbo** user.  
  
## Creating and Managing Audits with Transact-SQL  
 You can use DDL statements, dynamic management views and functions, and catalog views to implement all aspects of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Audit.  
  
### Data Definition Language Statements  
 You can use the following DDL statements to create, alter, and drop audit specifications:  
  
|||  
|-|-|  
|[ALTER AUTHORIZATION](../../../t-sql/statements/alter-authorization-transact-sql.md)|[CREATE SERVER AUDIT](../../../t-sql/statements/create-server-audit-transact-sql.md)|  
|[ALTER DATABASE AUDIT SPECIFICATION](../../../t-sql/statements/alter-database-audit-specification-transact-sql.md)|[CREATE SERVER AUDIT SPECIFICATION](../../../t-sql/statements/create-server-audit-specification-transact-sql.md)|  
|[ALTER SERVER AUDIT](../../../t-sql/statements/alter-server-audit-transact-sql.md)|[DROP DATABASE AUDIT SPECIFICATION](../../../t-sql/statements/drop-database-audit-specification-transact-sql.md)|  
|[ALTER SERVER AUDIT SPECIFICATION](../../../t-sql/statements/alter-server-audit-specification-transact-sql.md)|[DROP SERVER AUDIT](../../../t-sql/statements/drop-server-audit-transact-sql.md)|  
|[CREATE DATABASE AUDIT SPECIFICATION](../../../t-sql/statements/create-database-audit-specification-transact-sql.md)|[DROP SERVER AUDIT SPECIFICATION](../../../t-sql/statements/drop-server-audit-specification-transact-sql.md)|  
  
### Dynamic Views and Functions  
 The following table lists the dynamic views and function that you can use for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Auditing.  
  
|Dynamic views and functions|Description|  
|---------------------------------|-----------------|  
|[sys.dm_audit_actions](../../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)|Returns a row for every audit action that can be reported in the audit log and every audit action group that can be configured as part of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Audit.|  
|[sys.dm_server_audit_status](../../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)|Provides information about the current state of the audit.|  
|[sys.dm_audit_class_type_map](../../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)|Returns a table that maps the class_type field in the audit log to the class_desc field in sys.dm_audit_actions.|  
|[fn_get_audit_file](../../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)|Returns information from an audit file created by a server audit.|  
  
### Catalog Views  
 The following table lists the catalog views that you can use for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] auditing.  
  
|Catalog views|Description|  
|-------------------|-----------------|  
|[sys.database_ audit_specifications](../../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)|Contains information about the database audit specifications in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit on a server instance.|  
|[sys.database_audit_specification_details](../../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)|Contains information about the database audit specifications in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit on a server instance for all databases.|  
|[sys.server_audits](../../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)|Contains one row for each [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit in a server instance.|  
|[sys.server_audit_specifications](../../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)|Contains information about the server audit specifications in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit on a server instance.|  
|[sys.server_audit_specifications_details](../../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)|Contains information about the server audit specification details (actions) in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit on a server instance.|  
|[sys.server_file_audits](../../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)|Contains stores extended information about the file audit type in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] audit on a server instance.|  
  
## Permissions  
 Each feature and command for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Audit has individual permission requirements.  
  
 To create, alter, or drop a Server Audit or Server Audit Specification, server principals require the ALTER ANY SERVER AUDIT or the CONTROL SERVER permission. To create, alter, or drop a Database Audit Specification, database principals require the ALTER ANY DATABASE AUDIT permission or the ALTER or CONTROL permission on the database. In addition, principals must have permission to connect to the database, or ALTER ANY SERVER AUDIT or CONTROL SERVER permissions.  
  
 The VIEW ANY DEFINITION permission provides access to view the server level audit views and VIEW DEFINITION provides access to view the database level audit views. Denial of these permissions, overrides the ability to view the catalog views, even if the principal has the ALTER ANY SERVER AUDIT or ALTER ANY DATABASE AUDIT permissions.  
  
 For more information about how to grant rights and permissions, see [GRANT &#40;Transact-SQL&#41;](../../../t-sql/statements/grant-transact-sql.md).  
  
> [!CAUTION]  
>  Principals in the sysadmin role can tamper with any audit component and those in the db_owner role can tamper with audit specifications in a database. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Audit will validate that a logon that creates or alters an audit specification has at least the ALTER ANY DATABASE AUDIT permission. However, it does no validation when you attach a database. You should assume all Database Audit Specifications are only as trustworthy as those principals in the sysadmin or db_owner role.  
  
## Related Tasks  
 [Create a Server Audit and Server Audit Specification](../../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)  
  
 [Create a Server Audit and Database Audit Specification](../../../relational-databases/security/auditing/create-a-server-audit-and-database-audit-specification.md)  
  
 [View a SQL Server Audit Log](../../../relational-databases/security/auditing/view-a-sql-server-audit-log.md)  
  
 [Write SQL Server Audit Events to the Security Log](../../../relational-databases/security/auditing/write-sql-server-audit-events-to-the-security-log.md)  
  
## Topics Closely Related to Auditing  
 [Server Properties &#40;Security Page&#41;](../../../database-engine/configure-windows/server-properties-security-page.md)  
 Explains how to turn on login auditing for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The audit records are stored in the Windows application log.  
  
 [c2 audit mode Server Configuration Option](../../../database-engine/configure-windows/c2-audit-mode-server-configuration-option.md)  
 Explains the C2 security compliance auditing mode in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 [Security Audit Event Category &#40;SQL Server Profiler&#41;](../../../relational-databases/event-classes/security-audit-event-category-sql-server-profiler.md)  
 Explains the audit events you can use in [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)]. For more information, see [SQL Server Profiler](../../../tools/sql-server-profiler/sql-server-profiler.md).  
  
 [SQL Trace](../../../relational-databases/sql-trace/sql-trace.md)  
 Explains how SQL Trace can be used from within your own applications to create traces manually, instead of using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Profiler.  
  
 [DDL Triggers](../../../relational-databases/triggers/ddl-triggers.md)  
 Explains how you can use Data Definition Language (DDL) triggers to track changes to your databases.  
  
 [Microsoft TechNet: SQL Server TechCenter: SQL Server 2005 Security and Protection](https://go.microsoft.com/fwlink/?LinkId=101152)  
 Provides up-to-date information about [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] security.  
  
## See Also  
 [SQL Server Audit Action Groups and Actions](../../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md)   
 [SQL Server Audit Records](../../../relational-databases/security/auditing/sql-server-audit-records.md)  
  
  

