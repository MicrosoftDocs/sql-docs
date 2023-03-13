---
title: Mapping Remote Principals to Local Principals
description: "Service Broker dialog security uses certificates to map remote operations to a local security principal."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Mapping Remote Principals to Local Principals

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker dialog security uses certificates to map remote operations to a local security principal. This topic describes some of the considerations involved in choosing a local principal to map to a remote user.

## General Considerations

Access to SQL Server resources occurs within the security context of a database principal. Service Broker dialog security uses remote authorization to determine the local security context -- that is, the local database principal -- within which messages are sent for a specific dialog. The local security principal is determined by the certificate used for the conversation. For more information, see [Certificates for Dialog Security](certificates-for-dialog-security.md).

The local principal need only have SEND permission on the service or services that the principal sends messages to. There is no need for the principal to have any other permissions in the database. In particular, CONNECT permission is not required. Therefore, remote authorization generally uses a database principal specifically created for remote authorization. That principal has no other permissions, and should not be used for any other purpose. For a discussion of security principals in SQL Server, see [Principals (Database Engine)](../../relational-databases/security/authentication-access/principals-database-engine.md).

In general, you use one principal for each service. This helps to limit access to services. In some cases, if your application uses a closely related set of services, you may decide to use the same principal for all of the services. For example, if you design your application so that one service accepts expense report submissions while another service provides status information on expense reports, you may decide to secure both services with the same principal. In this case, access to one service implies access to the other service, so there is no need to separate the principals.

## Principal Types

Dialog security can use either a database user or an application role as the local principal. Each principal type has different characteristics. Select the type of principal that best suits the needs of your application. In most cases, a database user without a login provides the most flexible way to authorize remote connections, while minimizing the privileges required.

### Database Users Without Logins

A database user without a login cannot connect to the database directly to execute Transact-SQL. However, a user of this type can own objects in the database and can be granted database-level permissions. Therefore, many applications use a database user without a login as the local principal. The setup process for the application creates the user and grants only SEND permission on the service or services used for the application. Because these users do not correspond to a login, you can easily move the database that hosts the service to another instance without changing the security configuration.

### Application Roles

An application role does not require a server login. However, the Transact-SQL statement that creates an application role must include a password for the application role. Therefore, use caution when planning setup scripts that create application roles. Because an application role is contained within a specific database, you can easily move the database that hosts the service to another instance without changing the security configuration.

### Database Users With Logins

A database user may be mapped to a server login. In general, avoid using a database user with a login for remote authorization. Because these users can potentially connect to the database directly to execute Transact-SQL, a database user with a login has privileges that are not required for remote authorization.

Notice that the remote authorization procedure checks only database permissions. Therefore, remote authorization does not take into account permissions that would normally be available through the Windows login. For example, a user that maps to a login that is a member of the **BUILTIN\\Administrators** group normally has CONTROL SERVER permission. When used for remote authorization, however, the user has only the permissions that have been explicitly granted to the user.

## See also

- [CREATE APPLICATION ROLE (Transact-SQL)](../../t-sql/statements/create-application-role-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [Service Broker Dialog Security](service-broker-dialog-security.md)
- [Certificates for Dialog Security](certificates-for-dialog-security.md)