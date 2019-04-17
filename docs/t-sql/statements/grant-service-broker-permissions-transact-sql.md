---
title: "GRANT Service Broker Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "granting permissions [SQL Server], Service Broker"
  - "routes [Service Broker], permissions"
  - "Service Broker, permissions"
  - "GRANT statement, Service Broker"
  - "remote service bindings [Service Broker], permissions"
  - "message types [Service Broker], permissions"
  - "contracts [Service Broker], permissions"
ms.assetid: c5579976-97c4-4123-be0c-d0b98a9e38fb
author: VanMSFT
ms.author: vanto
manager: craigg
---
# GRANT Service Broker Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Grants permissions on a Service Broker contract, message type, remote binding, route, or service.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
GRANT permission  [ ,...n ] ON  
    {    
              [ CONTRACT :: contract_name ]   
       | [ MESSAGE TYPE :: message_type_name ]    
       | [ REMOTE SERVICE BINDING :: remote_binding_name ]    
       | [ ROUTE :: route_name ]   
       | [ SERVICE :: service_name ]      
    }  
    TO database_principal [ ,...n ]   
    [ WITH GRANT OPTION ]  
        [ AS granting_principal ]  
```  
  
## Arguments  
 *permission*  
 Specifies a permission that can be granted on a Service Broker securable.  Listed below.  
  
 CONTRACT **::**_contract_name_  
 Specifies the contract on which the permission is being granted. The scope  qualifier "::" is required.  
  
 MESSAGE TYPE **::**_message_type_name_  
 Specifies the message type on which the permission is being granted. The scope qualifier "::" is required.  
  
 REMOTE SERVICE BINDING **::**_remote_binding_name_  
 Specifies the remote service binding on which the permission is being granted. The scope qualifier "::" is required.  
  
 ROUTE **::**_route_name_  
 Specifies the route on which the permission is being granted. The scope qualifier "::" is required.  
  
 SERVICE **::**_service_name_  
 Specifies the service on which the permission is being granted. The scope qualifier "::" is required.  
  
 *database_principal*  
 Specifies the principal to which the permission is being granted. One of the following:  
  
-   database user  
  
-   database role  
  
-   application role  
  
-   database user mapped to a Windows login  
  
-   database user mapped to a Windows group  
  
-   database user mapped to a certificate  
  
-   database user mapped to an asymmetric key  
  
-   database user not mapped to a server principal.  
  
 GRANT OPTION  
 Indicates that the principal will also be given the ability to grant the specified permission to other principals.  
  
 *granting_principal*  
 Specifies a principal from which the principal executing this query derives its right to grant the permission. One of the following:  
  
-   database user  
  
-   database role  
  
-   application role  
  
-   database user mapped to a Windows login  
  
-   database user mapped to a Windows group  
  
-   database user mapped to a certificate  
  
-   database user mapped to an asymmetric key  
  
-   database user not mapped to a server principal.  
  
## Remarks  
  
## Service Broker Contracts  
 A Service Broker contract is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a Service Broker contract are listed below, together with the more general permissions that include them by implication.  
  
|Service Broker contract permission|Implied by Service Broker contract permission|Implied by database permission|  
|----------------------------------------|---------------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY CONTRACT|  
|REFERENCES|CONTROL|REFERENCES|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Service Broker Message Types  
 A Service Broker message type is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a Service Broker message type are listed below, together with the more general permissions that include them by implication.  
  
|Service Broker message type permission|Implied by Service Broker message type permission|Implied by database permission|  
|--------------------------------------------|-------------------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY MESSAGE TYPE|  
|REFERENCES|CONTROL|REFERENCES|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Service Broker Remote Service Bindings  
 A Service Broker remote service binding is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a Service Broker remote service binding are listed below, together with the more general permissions that include them by implication.  
  
|Service Broker remote service binding permission|Implied by Service Broker remote service binding permission|Implied by database permission|  
|------------------------------------------------------|-----------------------------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY REMOTE SERVICE BINDING|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Service Broker Routes  
 A Service Broker route is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a Service Broker route are listed below, together with the more general permissions that include them by implication.  
  
|Service Broker route permission|Implied by Service Broker route permission|Implied by database permission|  
|-------------------------------------|------------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY ROUTE|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
### Service Broker Services  
 A Service Broker service is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a Service Broker service are listed below, together with the more general permissions that include them by implication.  
  
|Service Broker service permission|Implied by Service Broker service permission|Implied by database permission|  
|---------------------------------------|--------------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|SEND|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY SERVICE|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 The grantor (or the principal specified with the AS option) must have either the permission itself with GRANT OPTION, or a higher permission that implies the permission being granted.  
  
 If using the AS option, these additional requirements apply.  
  
|AS *granting_principal*|Additional permission required|  
|------------------------------|------------------------------------|  
|Database user|IMPERSONATE permission on the user, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to a Windows login|IMPERSONATE permission on the user, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to a Windows group|Membership in the Windows group, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to a certificate|Membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to an asymmetric key|Membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user not mapped to any server principal|IMPERSONATE permission on the user, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database role|ALTER permission on the role, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Application role|ALTER permission on the role, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
  
 Object owners can grant permissions on the objects they own. Principals with CONTROL permission on a securable can grant permission on that securable.  
  
 Grantees of CONTROL SERVER permission, such as members of the **sysadmin** fixed server role, can grant any permission on any securable in the server. Grantees of CONTROL permission on a database, such as members of the **db_owner** fixed database role, can grant any permission on any securable in the database. Grantees of CONTROL permission on a schema can grant any permission on any object within the schema.  
  
## See Also  
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
