---
title: "CREATE REMOTE SERVICE BINDING (Transact-SQL)"
description: CREATE REMOTE SERVICE BINDING (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREATE REMOTE SERVICE BINDING"
  - "SERVICE_BINDING_TSQL"
  - "CREATE REMOTE SERVICE"
  - "REMOTE_TSQL"
  - "CREATE_REMOTE_SERVICE_BINDING_TSQL"
  - "CREATE_REMOTE_SERVICE_TSQL"
  - "BINDING"
  - "SERVICE BINDING"
  - "BINDING_TSQL"
  - "CREATE_REMOTE_TSQL"
  - "REMOTE_SERVICE_TSQL"
  - "CREATE REMOTE"
  - "REMOTE SERVICE"
  - "REMOTE_SERVICE_BINDING_TSQL"
  - "REMOTE"
  - "REMOTE SERVICE BINDING"
helpviewer_keywords:
  - "binding remote service [Service Broker]"
  - "dialog security [Service Broker]"
  - "end-to-end security [Service Broker]"
  - "security [Service Broker], remote service bindings"
  - "CREATE REMOTE SERVICE BINDING statement"
  - "conversation security [Service Broker]"
  - "remote service bindings [Service Broker], creating"
dev_langs:
  - "TSQL"
---
# CREATE REMOTE SERVICE BINDING (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates a binding that defines the security credentials to use to initiate a conversation with a remote service.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE REMOTE SERVICE BINDING binding_name   
   [ AUTHORIZATION owner_name ]   
   TO SERVICE 'service_name'   
   WITH  USER = user_name [ , ANONYMOUS = { ON | OFF } ]  
[ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *binding_name*  
 Is the name of the remote service binding to be created. Server, database, and schema names cannot be specified. The *binding_name* must be a valid **sysname**.  
  
 AUTHORIZATION *owner_name*  
 Sets the owner of the binding to the specified database user or role. When the current user is **dbo** or **sa**, *owner_name* can be the name of any valid user or role. Otherwise, *owner_name* must be the name of the current user, the name of a user who the current user has IMPERSONATE permissions for, or the name of a role to which the current user belongs.  
  
 TO SERVICE '*service_name*'  
 Specifies the remote service to bind to the user identified in the WITH USER clause.  
  
 USER = *user_name*  
 Specifies the database principal that owns the certificate associated with the remote service identified by the TO SERVICE clause. This certificate is used for encryption and authentication of messages exchanged with the remote service.  
  
 ANONYMOUS  
 Specifies whether anonymous authentication is used when communicating with the remote service. If ANONYMOUS = ON, anonymous authentication is used and operations in the remote database occur as a member of the **public** fixed database role. If ANONYMOUS = OFF, operations in the remote database occur as a specific user in that database. If this clause is not specified, the default is OFF.  
  
## Remarks  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses a remote service binding to locate the certificate to use for a new conversation. The public key in the certificate associated with *user_name* is used to authenticate messages sent to the remote service and to encrypt a session key that is then used to encrypt the conversation. The certificate for *user_name* must correspond to the certificate for a user in the database that hosts the remote service.  
  
 A remote service binding is only necessary for initiating services that communicate with target services outside of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. A database that hosts an initiating service must contain remote service bindings for any target services outside of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. A database that hosts a target service need not contain remote service bindings for the initiating services that communicate with the target service. When the initiator and target services are in the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], no remote service binding is necessary. However, if a remote service binding is present where the *service_name* specified for TO SERVICE matches the name of the local service, [!INCLUDE[ssSB](../../includes/sssb-md.md)] will use the binding.  
  
 When ANONYMOUS = ON, the initiating service connects to the target service as a member of the **public** fixed database role. By default, members of this role do not have permission to connect to a database. To successfully send a message, the target database must grant the **public** role CONNECT permission for the database and SEND permission for the target service.  
  
 When a user owns more than one certificate, [!INCLUDE[ssSB](../../includes/sssb-md.md)] selects the certificate with the latest expiration date from among the certificates that currently valid and marked as AVAILABLE FOR BEGIN_DIALOG.  
  
## Permissions  
 Permissions for creating a remote service binding default to the user named in the USER clause, members of the **db_owner** fixed database role, members of the **db_ddladmin** fixed database role, and members of the **sysadmin** fixed server role.  
  
 The user that executes the CREATE REMOTE SERVICE BINDING statement must have impersonate permission for the principal specified in the statement.  
  
 A remote service binding may not be a temporary object. Remote service binding names beginning with **#** are allowed, but are permanent objects.  
  
## Examples  
  
### A. Creating a remote service binding  
 The following example creates a binding for the service `//Adventure-Works.com/services/AccountsPayable`. [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses the certificate owned by the `APUser` database principal to authenticate to the remote service and to exchange the session encryption key with the remote service.  
  
```sql  
CREATE REMOTE SERVICE BINDING APBinding  
    TO SERVICE '//Adventure-Works.com/services/AccountsPayable'  
    WITH USER = APUser ;  
```  
  
### B. Creating a remote service binding using anonymous authentication  
 The following example creates a binding for the service `//Adventure-Works.com/services/AccountsPayable`. [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses the certificate owned by the `APUser` database principal to exchange the session encryption key with the remote service. The broker does not authenticate to the remote service. In the database that hosts the remote service, messages are delivered as the **guest** user.  
  
```sql  
CREATE REMOTE SERVICE BINDING APBinding  
    TO SERVICE '//Adventure-Works.com/services/AccountsPayable'  
    WITH USER = APUser, ANONYMOUS=ON ;  
```  
  
## See Also  
 [ALTER REMOTE SERVICE BINDING &#40;Transact-SQL&#41;](../../t-sql/statements/alter-remote-service-binding-transact-sql.md)   
 [DROP REMOTE SERVICE BINDING &#40;Transact-SQL&#41;](../../t-sql/statements/drop-remote-service-binding-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
