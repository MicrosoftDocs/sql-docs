---
title: "REVOKE Service Broker Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "routes [Service Broker], permissions"
  - "Service Broker, permissions"
  - "remote service bindings [Service Broker], permissions"
  - "permissions [Service Broker]"
  - "message types [Service Broker], permissions"
  - "contracts [Service Broker], permissions"
  - "services [Service Broker], permissions"
  - "REVOKE statement, Service Broker"
ms.assetid: 70f1d938-97e2-48a4-9bc0-8be9f2f2c36d
author: VanMSFT
ms.author: vanto
manager: craigg
---
# REVOKE Service Broker Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Revokes permissions on a [!INCLUDE[ssSB](../../includes/sssb-md.md)] contract, message type, remote service binding, route, or service.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
REVOKE [ GRANT OPTION FOR ] permission [ ,...n ] ON  
    {   
       [ CONTRACT :: contract_name ]   
       | [ MESSAGE TYPE :: message_type_name ]    
       | [ REMOTE SERVICE BINDING :: remote_binding_name ]    
       | [ ROUTE :: route_name ]   
       | [ SERVICE :: service_name ]      
        }  
    { TO | FROM } database_principal [ ,...n ]  
    [ CASCADE ]  
    [ AS revoking_principal ]  
```  
  
## Arguments  
 GRANT OPTION FOR  
 Indicates that the right to grant the specified right to other principals will be revoked. The permission itself will not be revokes.  
  
> [!IMPORTANT]  
>  If the principal has the specified permission without the GRANT option, the permission itself will be revoked.  
  
 *permission*  
 Specifies a permission that can be revoked on a [!INCLUDE[ssSB](../../includes/sssb-md.md)] securable. For a list of these permissions, see the Remarks section later in this topic.  
  
 CONTRACT **::**_contract_name_  
 Specifies the contract on which the permission is being revoked. The scope qualifier **::** is required.  
  
 MESSAGE TYPE **::**_message_type_name_  
 Specifies the message type on which the permission is being revoked. The scope qualifier **::** is required.  
  
 REMOTE SERVICE BINDING **::**_remote_binding_name_  
 Specifies the remote service binding on which the permission is being revoked. The scope qualifier **::** is required.  
  
 ROUTE **::**_route_name_  
 Specifies the route on which the permission is being revoked. The scope qualifier **::** is required.  
  
 SERVICE **::**_message_type_name_  
 Specifies the service on which the permission is being revoked. The scope qualifier **::** is required.  
  
 *database_principal*  
 Specifies the principal from which the permission is being revoked. *database_principal* can be one of the following:  
  
-   Database user  
  
-   Database role  
  
-   Application role  
  
-   Database user mapped to a Windows login  
  
-   Database user mapped to a Windows group  
  
-   Database user mapped to a certificate  
  
-   Database user mapped to an asymmetric key  
  
-   Database user not mapped to a server principal  
  
 CASCADE  
 Indicates that the permission being revoked is also revoked from other principals to which it has been granted or denied by this principal.  
  
> [!CAUTION]  
>  A cascaded revocation of a permission granted WITH GRANT OPTION will revoke both GRANT and DENY of that permission.  
  
 AS *revoking_principal*  
 Specifies a principal from which the principal executing this query derives its right to revoke the permission. *revoking_principal* can be one of the following:  
  
-   Database user  
  
-   Database role  
  
-   Application role  
  
-   Database user mapped to a Windows login  
  
-   Database user mapped to a Windows group  
  
-   Database user mapped to a certificate  
  
-   Database user mapped to an asymmetric key  
  
-   Database user not mapped to a server principal  
  
## Remarks  
  
## Service Broker Contracts  
 A [!INCLUDE[ssSB](../../includes/sssb-md.md)] contract is a database-level securable that is contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be revoked on a [!INCLUDE[ssSB](../../includes/sssb-md.md)] contract are listed in the following table, together with the more general permissions that include them by implication.  
  
|Service Broker contract permission|Implied by Service Broker contract permission|Implied by database permission|  
|----------------------------------------|---------------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY CONTRACT|  
|REFERENCES|CONTROL|REFERENCES|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Service Broker Message Types  
 A [!INCLUDE[ssSB](../../includes/sssb-md.md)] message type is a database-level securable that is contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be revoked on a [!INCLUDE[ssSB](../../includes/sssb-md.md)] message type are listed in the following table, together with the more general permissions that include them by implication.  
  
|Service Broker message type permission|Implied by Service Broker message type permission|Implied by database permission|  
|--------------------------------------------|-------------------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY MESSAGE TYPE|  
|REFERENCES|CONTROL|REFERENCES|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Service Broker Remote Service Bindings  
 A [!INCLUDE[ssSB](../../includes/sssb-md.md)] remote service binding is a database-level securable that is contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be revoked on a [!INCLUDE[ssSB](../../includes/sssb-md.md)] remote service binding are listed in the following table, together with the more general permissions that include them by implication.  
  
|Service Broker remote service binding permission|Implied by Service Broker remote service binding permission|Implied by database permission|  
|------------------------------------------------------|-----------------------------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY REMOTE SERVICE BINDING|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Service Broker Routes  
 A [!INCLUDE[ssSB](../../includes/sssb-md.md)] route is a database-level securable that is contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be revoked on a [!INCLUDE[ssSB](../../includes/sssb-md.md)] route are listed in the following table, together with the more general permissions that include them by implication.  
  
|Service Broker route permission|Implied by Service Broker route permission|Implied by database permission|  
|-------------------------------------|------------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY ROUTE|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
### Service Broker Services  
 A [!INCLUDE[ssSB](../../includes/sssb-md.md)] service is a database-level securable that is contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be revoked on a [!INCLUDE[ssSB](../../includes/sssb-md.md)] service are listed in the following table, together with the more general permissions that include them by implication.  
  
|Service Broker service permission|Implied by Service Broker service permission|Implied by database permission|  
|---------------------------------------|--------------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|SEND|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY SERVICE|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the [!INCLUDE[ssSB](../../includes/sssb-md.md)] contract, message type, remote service binding, route, or service  
  
## See Also  
 [GRANT Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-service-broker-permissions-transact-sql.md)   
 [DENY Service Broker Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-service-broker-permissions-transact-sql.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
