---
title: "ALTER SERVER AUDIT SPECIFICATION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/01/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER SERVER AUDIT SPECIFICATION"
  - "ALTER_SERVER_AUDIT_SPECIFICATION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "server audit [SQL Server]"
  - "audits [SQL Server], specification"
  - "ALTER SERVER AUDIT SPECIFICATION statement"
ms.assetid: 9cac288b-940e-4c16-88d6-de06aeed2b47
author: VanMSFT
ms.author: vanto
manager: craigg
---
# ALTER SERVER AUDIT SPECIFICATION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Alters a server audit specification object using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Audit feature. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ALTER SERVER AUDIT SPECIFICATION audit_specification_name  
{  
    [ FOR SERVER AUDIT audit_name ]  
    [ { { ADD | DROP } ( audit_action_group_name )  
      } [, ...n] ]  
    [ WITH ( STATE = { ON | OFF } ) ]  
}  
[ ; ]  
```  
  
## Arguments  
 *audit_specification_name*  
 The name of the audit specification.  
  
 *audit_name*  
 The name of the audit to which this specification is applied.  
  
 *audit_action_group_name*  
 Name of a group of server-level auditable actions. For a list of Audit Action Groups, see [SQL Server Audit Action Groups and Actions](../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).  
  
 WITH **(** STATE **=** { ON | OFF } **)**  
 Enables or disables the audit from collecting records for this audit specification.  
  
## Remarks  
 You must set the state of an audit specification to the OFF option to make changes to an audit specification. If ALTER SERVER AUDIT SPECIFICATION is executed when an audit specification is enabled with any options other than STATE=OFF, you will receive an error message.  
  
## Permissions  
 Users with the ALTER ANY SERVER AUDIT permission can alter server audit specifications and bind them to any audit.  
  
 After a server audit specification is created, it can be viewed by principals with the CONTROL SERVER, or ALTER ANY SERVER AUDIT permissions, the sysadmin account, or principals having explicit access to the audit.  
  
## Examples  
 The following example creates a server audit specification called `HIPAA_Audit_Specification`. It drops the audit action group for failed logins, and adds an audit action group for Database Object Access for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] audit called `HIPAA_Audit`.  
  
```  
ALTER SERVER AUDIT SPECIFICATION HIPAA_Audit_Specification  
FOR SERVER AUDIT HIPAA_Audit  
    DROP (FAILED_LOGIN_GROUP)  
    ADD (DATABASE_OBJECT_ACCESS_GROUP);  
GO  
```  
  
 For a full example about how to create an audit, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  

## See Also  
 [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-transact-sql.md)   
 [ALTER SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-transact-sql.md)   
 [DROP SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-transact-sql.md)   
 [CREATE SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-specification-transact-sql.md)   
 [DROP SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)   
 [CREATE DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-audit-specification-transact-sql.md)   
 [ALTER DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)   
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
  
  
