---
title: "ALTER DATABASE AUDIT SPECIFICATION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_DATABASE_AUDIT_SPECIFICATION_TSQL"
  - "ALTER DATABASE AUDIT SPECIFICATION"
  - "ALTER_DATABASE_AUDIT_TSQL"
  - "ALTER DATABASE AUDIT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER DATABASE AUDIT SPECIFICATION statement"
ms.assetid: 85f4e7e6-a330-4de0-9048-64f386ccc314
author: VanMSFT
ms.author: vanto
manager: craigg
---
# ALTER DATABASE AUDIT SPECIFICATION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Alters a database audit specification object using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Audit feature. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER DATABASE AUDIT SPECIFICATION audit_specification_name  
{  
    [ FOR SERVER AUDIT audit_name ]  
    [ { { ADD | DROP } (   
           { <audit_action_specification> | audit_action_group_name }   
                )   
      } [, ...n] ]  
    [ WITH ( STATE = { ON | OFF } ) ]  
}  
[ ; ]  
<audit_action_specification>::=  
{  
      <action_specification>[ ,...n ] ON [ class :: ] securable   
     BY principal [ ,...n ]   
}  
  
```  
  
## Arguments  
 *audit_specification_name*  
 The name of the audit specification.  
  
 *audit_name*  
 The name of the audit to which this specification is applied.  
  
 *audit_action_specification*  
 Name of one or more database-level auditable actions. For a list of audit action groups, see [SQL Server Audit Action Groups and Actions](../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).  
  
 *audit_action_group_name*  
 Name of one or more groups of database-level auditable actions. For a list of audit action groups, see [SQL Server Audit Action Groups and Actions](../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).  
  
 *class*  
 Class name (if applicable) on the securable.  
  
 *securable*  
 Table, view, or other securable object in the database on which to apply the audit action or audit action group. For more information, see [Securables](../../relational-databases/security/securables.md).  
  
 *column*  
 Column name (if applicable) on the securable.  
  
 *principal*  
 Name of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] principal on which to apply the audit action or audit action group. For more information, see [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md).  
  
 WITH **(** STATE **=** { ON | OFF } **)**  
 Enables or disables the audit from collecting records for this audit specification. Audit specification state changes must be done outside a user transaction and may not have other changes in the same statement when the transition is ON to OFF.  
  
## Remarks  
 Database audit specifications are non-securable objects that reside in a given database. You must set the state of an audit specification to the OFF option in order to make changes to a database audit specification. If ALTER DATABASE AUDIT SPECIFICATION is executed when an audit is enabled with any options other than STATE=OFF, you will receive an error message. For more information, see [tempdb Database](../../relational-databases/databases/tempdb-database.md).  
  
## Permissions  
 Users with the ALTER ANY DATABASE AUDIT permission can alter database audit specifications and bind them to any audit.  
  
 After a database audit specification is created, it can be viewed by principals with the CONTROL SERVER,or ALTER ANY DATABASE AUDIT permissions, the sysadmin account, or principals having explicit access to the audit.  
  
## Examples  
 The following example alters a database audit specification called `HIPAA_Audit_DB_Specification` that audits the `SELECT` statements by the `dbo` user, for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] audit called `HIPAA_Audit`.  
  
```  
ALTER DATABASE AUDIT SPECIFICATION HIPAA_Audit_DB_Specification  
FOR SERVER AUDIT HIPAA_Audit  
    ADD (SELECT  
         ON OBJECT::dbo.Table1  
         BY dbo)  
    WITH (STATE = ON);  
GO  
```  
  
 For a full example about how to create an audit, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
## See Also  
 [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-transact-sql.md)   
 [ALTER SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-transact-sql.md)   
 [DROP SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-transact-sql.md)   
 [CREATE SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-specification-transact-sql.md)   
 [ALTER SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)   
 [DROP SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)   
 [CREATE DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-audit-specification-transact-sql.md)   
 [DROP DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)   
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)   
 [sys.fn_get_audit_file &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)   
 [sys.server_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)   
 [sys.server_file_audits &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)   
 [sys.server_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)   
 [sys.server_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)   
 [sys.database_audit_specifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)   
 [sys.database_audit_specification_details &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)   
 [sys.dm_server_audit_status &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)   
 [sys.dm_audit_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)   
 [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)  
  
  
