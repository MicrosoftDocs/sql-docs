---
title: "ALTER REMOTE SERVICE BINDING (Transact-SQL)"
description: ALTER REMOTE SERVICE BINDING (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER REMOTE SERVICE BINDING"
  - "ALTER_REMOTE_SERVICE_BINDING_TSQL"
helpviewer_keywords:
  - "remote service bindings [Service Broker], modifying"
  - "ALTER REMOTE SERVICE BINDING statement"
  - "modifying remote service bindings"
dev_langs:
  - "TSQL"
---
# ALTER REMOTE SERVICE BINDING (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Changes the user associated with a remote service binding, or changes the anonymous authentication setting for the binding.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ALTER REMOTE SERVICE BINDING binding_name   
   WITH [ USER = <user_name> ] [ , ANONYMOUS = { ON | OFF } ]   
[ ; ]  
```  
  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *binding_name*  
 The name of the remote service binding to change. Server, database, and schema names cannot be specified.  
  
 WITH USER = \<*user_name>*  
 Specifies the database user that holds the certificate associated with the remote service for this binding. The public key from this certificate is used for encryption and authentication of messages exchanged with the remote service.  
  
 ANONYMOUS  
 Specifies whether anonymous authentication is used when communicating with the remote service. If ANONYMOUS = ON, anonymous authentication is used and the credentials of the local user are not transferred to the remote service. If ANONYMOUS = OFF, user credentials are transferred. If this clause is not specified, the default is OFF.  
  
## Remarks  
 The public key in the certificate associated with *user_name* is used to authenticate messages sent to the remote service and to encrypt a session key that is then used to encrypt the conversation. The certificate for *user_name* must correspond to the certificate for a login in the database that hosts the remote service.  
  
## Permissions  
 Permission for altering a remote service binding defaults to the owner of the remote service binding, members of the **db_owner** fixed database role, and members of the **sysadmin** fixed server role.  
  
 The user that executes the ALTER REMOTE SERVICE BINDING statement must have impersonate permission for the user specified in the statement.  
  
 To alter the AUTHORIZATION for a remote service binding, use the ALTER AUTHORIZATION statement.  
  
## Examples  
 The following example changes the remote service binding `APBinding` to encrypt messages by using the certificates from the account `SecurityAccount`.  
  
```sql  
ALTER REMOTE SERVICE BINDING APBinding  
    WITH USER = SecurityAccount ;  
```  
  
## See Also  
 [CREATE REMOTE SERVICE BINDING &#40;Transact-SQL&#41;](../../t-sql/statements/create-remote-service-binding-transact-sql.md)   
 [DROP REMOTE SERVICE BINDING &#40;Transact-SQL&#41;](../../t-sql/statements/drop-remote-service-binding-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
