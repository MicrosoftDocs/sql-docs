---
title: Identity and Access Control (Service Broker)
description: "Most Service Broker applications that involve more than one instance run in the security context of a database principal created specifically for the application."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Identity and Access Control (Service Broker)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Most Service Broker applications that involve more than one instance run in the security context of a database principal created specifically for the application. These database principals should have the minimum permissions required to accomplish the tasks that the application performs.

The following considerations apply to database principals created for Service Broker applications:

- Service Broker remote authorization applies when a remote Service Broker application connects to SQL Server and delivers a message to the instance. The database principal specified for remote authorization must have CONNECT permission in the database that hosts the initiating service and must have SEND permission for the initiating service itself. The user must own the certificate used for authentication. There are no requirements for the user to own other objects, to have other permissions, or to be able to log in with any other mechanism.

- For a database principal to begin a conversation, that principal must have RECEIVE permissions on the queue for the initiating service.

- The database principal that owns the initiating service must have SEND permissions on the target service.

- For a database principal to send messages to a service, that principal must have SEND permissions on the service. For services hosted in a different instance, Service Broker dialog security determines the database principal in the remote instance. For more information, see [Service Broker Dialog Security](service-broker-dialog-security.md). Notice that Service Broker does not consider membership in Windows roles when checking SEND permissions.

- The user specified as the user for an activation stored procedure must have permission to execute the procedure. Frequently, the user specified has the permissions required to execute the statements in the procedure. Notice, however, that if the stored procedure itself is defined with an EXECUTE AS clause, the statements in the stored procedure run with the security context defined by the stored procedure. SQL Server first sets the security context to the user specified for the queue. SQL Server then executes the stored procedure and the stored procedure changes the security context to the user specified for the procedure.

- When Service Broker transport security uses SSPI, the service account for the remote database must have CONNECT permission in **master**, and must also correspond to a login. Therefore, the account that the remote SQL Server instance runs as must have permission to sign in to SQL Server using Windows Authentication. There are no requirements for the sign in to have other permissions or to own objects in any database.

## See also

- [GRANT Service Broker Permissions (Transact-SQL)](../../t-sql/statements/grant-service-broker-permissions-transact-sql.md)
- [Query notifications in SQL Server](../../connect/ado-net/sql/query-notifications-sql-server.md)
