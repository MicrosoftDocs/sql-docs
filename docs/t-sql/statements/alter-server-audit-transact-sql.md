---
title: "ALTER SERVER AUDIT  (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/07/2018"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_SERVER_AUDIT_TSQL"
  - "ALTER SERVER AUDIT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "server audit [SQL Server]"
  - "audits [SQL Server], specification"
  - "ALTER SERVER AUDIT statement"
ms.assetid: 63426d31-7a5c-4378-aa9e-afcf4f64ceb3
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# ALTER SERVER AUDIT  (Transact-SQL)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  Alters a server audit object using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Audit feature. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ALTER SERVER AUDIT audit_name  
{  
    [ TO { { FILE ( <file_options> [, ...n] ) } | APPLICATION_LOG | SECURITY_LOG } | URL]  
    [ WITH ( <audit_options> [ , ...n] ) ]   
    [ WHERE <predicate_expression> ]  
}  
| REMOVE WHERE  
| MODIFY NAME = new_audit_name  
[ ; ]  
  
<file_options>::=  
{  
      FILEPATH = 'os_file_path'   
    | MAXSIZE = { max_size { MB | GB | TB } | UNLIMITED }   
    | MAX_ROLLOVER_FILES = { integer | UNLIMITED }   
    | MAX_FILES = integer   
    | RESERVE_DISK_SPACE = { ON | OFF }   
}  
  
<audit_options>::=  
{  
      QUEUE_DELAY = integer   
    | ON_FAILURE = { CONTINUE | SHUTDOWN | FAIL_OPERATION }   
    | STATE = = { ON | OFF }   
}  
  
<predicate_expression>::=  
{  
    [NOT ] <predicate_factor>   
    [ { AND | OR } [NOT ] { <predicate_factor> } ]   
    [,...n ]  
}  
  
<predicate_factor>::=   
    event_field_name { = | < > | ! = | > | > = | < | < = } { number | ' string ' }  
```  
  
## Arguments  
 TO { FILE | APPLICATION_LOG | SECURITY |URL}  
 Determines the location of the audit target. The options are a binary file, the Windows application log, or the Windows security log.  

> [!IMPORTANT]
> In Azure SQL Database managed instance, SQL Audit works at the server level and stores `.xel` files in Azure blob storage.
  
 FILEPATH **= '**_os\_file\_path_**'**  
 The path of the audit trail. The file name is generated based on the audit name and audit GUID.  
  
 MAXSIZE **=**_max\_size_  
 Specifies the maximum size to which the audit file can grow. The *max_size* value must be an integer followed by **MB**, **GB**, **TB**, or **UNLIMITED**. The minimum size that you can specify for *max_size* is 2 **MB** and the maximum is 2,147,483,647 **TB**. When **UNLIMITED** is specified, the file grows until the disk is full. Specifying a value lower than 2 MB raises MSG_MAXSIZE_TOO_SMALL the error. The default value is **UNLIMITED**.  
  
 MAX_ROLLOVER_FILES **=**_integer_ | **UNLIMITED**  
 Specifies the maximum number of files to retain in the file system. When the setting of MAX_ROLLOVER_FILES=0, there is no limit imposed on the number of rollover files that are created. The default value is 0. The maximum number of files that can be specified is 2,147,483,647.  
  
 MAX_FILES =*integer*  
 Specifies the maximum number of audit files that can be created. Does not roll over to the first file when the limit is reached. When the MAX_FILES limit is reached, any action that causes additional audit events to be generated fails with an error.  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 RESERVE_DISK_SPACE **=** { ON | OFF }  
 This option pre-allocates the file on the disk to the MAXSIZE value. Only applies if MAXSIZE is not equal to UNLIMITED. The default value is OFF.  
  
 QUEUE_DELAY **=**_integer_  
 Determines the time in milliseconds that can elapse before audit actions are forced to be processed. A value of 0 indicates synchronous delivery. The minimum settable query delay value is 1000 (1 second), which is the default. The maximum is 2,147,483,647 (2,147,483.647 seconds or 24 days, 20 hours, 31 minutes, 23.647 seconds). Specifying an invalid number, raises the error MSG_INVALID_QUEUE_DELAY.  
  
 ON_FAILURE **=** { CONTINUE | SHUTDOWN | FAIL_OPERATION}  
 Indicates whether the instance writing to the target should fail, continue, or stop if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot write to the audit log.  
  
 CONTINUE  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] operations continue. Audit records are not retained. The audit continues to attempt to log events and resumes if the failure condition is resolved. Selecting the continue option can allow unaudited activity, which could violate your security policies. Use this option, when continuing operation of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is more important than maintaining a complete audit.  
  
SHUTDOWN  
Forces the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to shut down, if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fails to write data to the audit target for any reason. The login executing the `ALTER` statement must have the `SHUTDOWN` permission within [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The shutdown behavior persists even if the `SHUTDOWN` permission is later revoked from the executing login. If the user does not have this permission, then the statement will fail and the audit will not be modified. Use the option when an audit failure could compromise the security or integrity of the system. For more information, see [SHUTDOWN](../../t-sql/language-elements/shutdown-transact-sql.md). 
  
 FAIL_OPERATION  
 Database actions fail if they cause audited events. Actions, which do not cause audited events can continue, but no audited events can occur. The audit continues to attempt to log events and resumes if the failure condition is resolved. Use this option when maintaining a complete audit is more important than full access to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].   
  
 STATE **=** { ON | OFF }  
 Enables or disables the audit from collecting records. Changing the state of a running audit (from ON to OFF) creates an audit entry that the audit was stopped, the principal that stopped the audit, and the time the audit was stopped.  
  
 MODIFY NAME = *new_audit_name*  
 Changes the name of the audit. Cannot be used with any other option.  
  
 predicate_expression  
 Specifies the predicate expression used to determine if an event should be processed or not. Predicate expressions are limited to 3000 characters, which limits string arguments.  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 event_field_name  
 Is the name of the event field that identifies the predicate source. Audit fields are described in [sys.fn_get_audit_file &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md). All fields can be audited except `file_name` and `audit_file_offset`.  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 number  
 Is any numeric type including **decimal**. Limitations are the lack of available physical memory or a number that is too large to be represented as a 64-bit integer.  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 ' string '  
 Either an ANSI or Unicode string as required by the predicate compare. No implicit string type conversion is performed for the predicate compare functions. Passing the wrong type results in an error.  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
## Remarks  
 You must specify at least one of the TO, WITH, or MODIFY NAME clauses when you call ALTER AUDIT.  
  
 You must set the state of an audit to the OFF option in order to make changes to an audit. If ALTER AUDIT is run when an audit is enabled with any options other than STATE=OFF, you receive a MSG_NEED_AUDIT_DISABLED error message.  
  
 You can add, alter, and remove audit specifications without stopping an audit.  
  
 You cannot change an audit's GUID after the audit has been created.  
  
## Permissions  
 To create, alter, or drop a server audit principal, you must have ALTER ANY SERVER AUDIT or the CONTROL SERVER permission.  
  
## Examples  
  
### A. Changing a server audit name  
 The following example changes the name of the server audit `HIPAA_Audit` to `HIPAA_Audit_Old`.  
  
```  
USE master  
GO  
ALTER SERVER AUDIT HIPAA_Audit  
WITH (STATE = OFF);  
GO  
ALTER SERVER AUDIT HIPAA_Audit  
MODIFY NAME = HIPAA_Audit_Old;  
GO  
ALTER SERVER AUDIT HIPAA_Audit_Old  
WITH (STATE = ON);  
GO  
```  
  
### B. Changing a server audit target  
 The following example changes the server audit called `HIPAA_Audit` to a file target.  
  
```  
USE master  
GO  
ALTER SERVER AUDIT HIPAA_Audit  
WITH (STATE = OFF);  
GO  
ALTER SERVER AUDIT HIPAA_Audit  
TO FILE (FILEPATH ='\\SQLPROD_1\Audit\',  
          MAXSIZE = 1000 MB,  
          RESERVE_DISK_SPACE=OFF)  
WITH (QUEUE_DELAY = 1000,  
       ON_FAILURE = CONTINUE);  
GO  
ALTER SERVER AUDIT HIPAA_Audit  
WITH (STATE = ON);  
GO  
```  
  
### C. Changing a server audit WHERE clause  
 The following example modifies the where clause created in example C of [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-transact-sql.md). The new WHERE clause filters for the user-defined event if of 27.  
  
```sql  
ALTER SERVER AUDIT [FilterForSensitiveData] WITH (STATE = OFF)  
GO  
ALTER SERVER AUDIT [FilterForSensitiveData]  
WHERE user_defined_event_id = 27;  
GO  
ALTER SERVER AUDIT [FilterForSensitiveData] WITH (STATE = ON);  
GO  
```  
  
### D. Removing a WHERE clause  
 The following example removes a WHERE clause predicate expression.  
  
```sql  
ALTER SERVER AUDIT [FilterForSensitiveData] WITH (STATE = OFF)  
GO  
ALTER SERVER AUDIT [FilterForSensitiveData]  
REMOVE WHERE;  
GO  
ALTER SERVER AUDIT [FilterForSensitiveData] WITH (STATE = ON);  
GO  
```  
  
### E. Renaming a server audit  
 The following example changes the server audit name from `FilterForSensitiveData` to `AuditDataAccess`.  
  
```sql  
ALTER SERVER AUDIT [FilterForSensitiveData] WITH (STATE = OFF)  
GO  
ALTER SERVER AUDIT [FilterForSensitiveData]  
MODIFY NAME = AuditDataAccess;  
GO  
ALTER SERVER AUDIT [AuditDataAccess] WITH (STATE = ON);  
GO  
```  
  
## See Also  
 [DROP SERVER AUDIT  &#40;Transact-SQL&#41;](../../t-sql/statements/drop-server-audit-transact-sql.md)   
 [CREATE SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-server-audit-specification-transact-sql.md)   
 [ALTER SERVER AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)   
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
  
  
