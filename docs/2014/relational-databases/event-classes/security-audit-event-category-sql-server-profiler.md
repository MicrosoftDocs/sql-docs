---
title: "Security Audit Event Category (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Security Audit event category [SQL Server]"
  - "event classes [SQL Server], Security Audit event category"
  - "SQL Server event classes, Security Audit event category"
ms.assetid: e64f7695-2f23-4adb-b83d-52f147cc1a2f
author: stevestein
ms.author: sstein
manager: craigg
---
# Security Audit Event Category (SQL Server Profiler)
  The **Security Audit** event category contains security audit events.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Audit Add DB User Event Class](audit-add-db-user-event-class.md)|Indicates that a login has been added or removed as a database user to a database.|  
|[Audit Add Login to Server Role Event Class](audit-add-login-to-server-role-event-class.md)|Indicates that a login was added or removed from a fixed server role.|  
|[Audit Add Member to DB Role Event Class](audit-add-member-to-db-role-event-class.md)|Indicates that a login has been added to or removed from a role.|  
|[Audit Add Role Event Class](audit-add-role-event-class.md)|Indicates that a database role was added to or removed from a database.|  
|[Audit Addlogin Event Class](audit-addlogin-event-class.md)|Indicates that a login has been added or removed.|  
|[Audit App Role Change Password Event Class](audit-app-role-change-password-event-class.md)|Indicates that a password has been changed for an application role.|  
|[Audit Backup and Restore Event Class](audit-backup-and-restore-event-class.md)|Indicates that a backup or restore statement has been issued.|  
|[Audit Broker Conversation Event Class](broker-conversation-event-class.md)|Reports audit messages related to Service Broker dialog security.|  
|[Audit Broker Login Event Class](audit-broker-login-event-class.md)|Reports audit messages related to Service Broker transport security.|  
|[Audit Change Audit Event Class](audit-change-audit-event-class.md)|Indicates that an audit trace modification has been made.|  
|[Audit Change Database Owner Event Class](audit-change-database-owner-event-class.md)|Indicates that the permissions to change the owner of a database have been checked.|  
|[Audit Database Management Event Class](audit-database-management-event-class.md)|Indicates that a database has been created, altered, or dropped.|  
|[Audit Database Mirroring Login Event Class](audit-database-mirroring-login-event-class.md)|Reports audit messages related to database mirroring transport security.|  
|[Audit Database Object Access Event Class](audit-database-object-access-event-class.md)|Indicates that a database object, such as a schema, has been accessed.|  
|[Audit Database Object GDR Event Class](audit-database-object-gdr-event-class.md)|Indicates that a GDR event for a database object has occurred.|  
|[Audit Database Object Management Event Class](audit-database-object-management-event-class.md)|Indicates that a CREATE, ALTER, or DROP statement was executed on a database object.|  
|[Audit Database Object Take Ownership Event Class](audit-database-object-take-ownership-event-class.md)|Indicates that there has been a change of owner for objects in database scope.|  
|[Audit Database Operation Event Class](audit-database-operation-event-class.md)|Indicates that various operations such as checkpoint or subscribe query notification have occurred.|  
|[Audit Database Principal Impersonation Event Class](audit-database-principal-impersonation-event-class.md)|Indicates that an impersonation has occurred within the database scope.|  
|[Audit Database Principal Management Event Class](audit-database-principal-management-event-class.md)|Indicates that principals have been created, altered, or dropped from a database.|  
|[Audit Database Scope GDR Event Class](audit-database-scope-gdr-event-class.md)|Indicates that a GRANT, REVOKE, or DENY has been issued for a statement permission by a user in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[Audit DBCC Event Class](audit-dbcc-event-class.md)|Indicates that a DBCC command has been issued.|  
|[Audit Fulltext Event Class](audit-fulltext-event-class.md)|Indicates that a full-text event has occured.|  
|[Audit Login Change Password Event Class](audit-login-change-password-event-class.md)|Indicates that a user has changed their [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login password.|  
|[Audit Login Change Property Event Class](audit-login-change-property-event-class.md)|Indicates that **sp_defaultdb**, **sp_defaultlanguage**, or ALTER LOGIN was used to modify a property of a login.|  
|[Audit Login Event Class](audit-login-event-class.md)|Indicates that a user has successfully logged into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[Audit Login Failed Event Class](audit-login-failed-event-class.md)|Indicates that a user attempted to log in to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and failed.|  
|[Audit Login GDR Event Class](audit-login-gdr-event-class.md)|Indicates that a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows login right was added or removed.|  
|[Audit Logout Event Class](audit-logout-event-class.md)|Indicates that a user has logged out of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[Audit Object Derived Permission Event Class](audit-object-derived-permission-event-class.md)|Indicates that a CREATE, ALTER, or DROP was issued for an object.|  
|[Audit Schema Object Access Event Class](audit-schema-object-access-event-class.md)|Indicates that an object permission (such as SELECT) has been used.|  
|[Audit Schema Object GDR Event Class](audit-schema-object-gdr-event-class.md)|Indicates that a GRANT, REVOKE, or DENY was issued for a schema object permission by a user in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[Audit Schema Object Management Event Class](audit-schema-object-management-event-class.md)|Indicates that a server object has been created, altered, or dropped.|  
|[Audit Schema Object Take Ownership Event Class](audit-schema-object-take-ownership-event-class.md)|Indicates that the permissions to change the owner of schema object have been checked.|  
|[Audit Server Alter Trace Event Class](audit-server-alter-trace-event-class.md)|Indicates that the ALTER TRACE permission has been checked.|  
|[Audit Server Object GDR Event Class](audit-server-object-gdr-event-class.md)|Indicates that a GDR event for a schema object has occurred.|  
|[Audit Server Object Management Event Class](audit-server-object-management-event-class.md)|Indicates that a CREATE, ALTER, or DROP event has occurred for a server object.|  
|[Audit Server Object Take Ownership Event Class](audit-server-object-take-ownership-event-class.md)|Indicates that a server object owner has changed.|  
|[Audit Server Operation Event Class](audit-server-operation-event-class.md)|Indicates that Audit operations have occurred in the server.|  
|[Audit Server Principal Impersonation Event Class](audit-server-principal-impersonation-event-class.md)|Indicates that an impersonation has occurred within the server scope.|  
|[Audit Server Principal Management Event Class](audit-server-principal-management-event-class.md)|Indicates that a CREATE, ALTER, or DROP has occurred for a server principal.|  
|[Audit Server Scope GDR Event Class](audit-server-scope-gdr-event-class.md)|Indicates that a GDR event has occurred for server permissions.|  
|[Audit Server Starts and Stops Event Class](audit-server-starts-and-stops-event-class.md)|Indicates that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service state has been modified.|  
|[Audit Statement Permission Event Class](audit-statement-permission-event-class.md)|Indicates that a statement permission has been used.|  
  
## Related Content  
 [Extended Events](../extended-events/extended-events.md)  
  
  
