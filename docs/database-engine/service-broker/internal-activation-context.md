---
title: Internal Activation Context
description: Learn about the execution context for a stored procedure that is started by internal activation.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 09/03/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Internal activation context

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes the execution context for a stored procedure that is started by internal activation.

## Security context

A queue configured for activation must also specify the user that the activation stored procedure runs as. SQL Server impersonates this user before starting the stored procedure.

When the stored procedure also specifies an `EXECUTE AS` clause, two impersonations occur. SQL Server first impersonates the user specified for the queue and executes the stored procedure. When the stored procedure executes, the procedure impersonates the user specified in the `EXECUTE AS` clause of the procedure.

The user specified for a remote service binding is generally a different user from the user specified for activation. The permissions required for each user also differ. The remote service binding user doesn't need permission to read from the queue or execute stored procedures in the database, while the user specified for activation doesn't need permission to send messages to the service. For more information on user permissions, see [Identity and access control (Service Broker)](identity-and-access-control.md) and [Service Broker dialog security](service-broker-dialog-security.md).

## Session settings

Service Broker executes internally activated service programs on a background session distinct from the connection that created the message. The options set for this session are the default options for the database.

Within a session started by Service Broker, SQL Server writes the output of `PRINT` and `RAISERROR` statements to the SQL Server error log. Service Broker doesn't provide parameters to an activated stored procedure. Service Broker doesn't consider return values from an activated stored procedure and doesn't process result sets from an activated stored procedure.

## Transaction context

An activated stored procedure is responsible for managing transactions. SQL Server doesn't start a transaction before activating the stored procedure, and the stored procedure runs in a different transaction context than the internal operation that activates the procedure. For a discussion of managing transactions in activated stored procedures, see [Transactional messaging](transactional-messaging.md).

## Failure detection

An activated stored procedure must receive messages from the queue that activated the procedure. If the stored procedure exits without receiving messages, or the queue monitor detects that the stored procedure isn't receiving messages after a short timeout, the queue monitor considers the stored procedure to have failed. In this case, the queue monitor stops activating the stored procedure.

## Monitor

To check the user running the activation procedure, you can either use Extended Events (the `broker_activation` event, with `username` and `server_principal_name` actions), or the [sys.dm_broker_activated_tasks](../../relational-databases/system-dynamic-management-objects/sys-dm-broker-activated-tasks-transact-sql.md) dynamic management view, which returns the database `principal_id` configured in the queue.

## Related content

- [PRINT (Transact-SQL)](../../t-sql/language-elements/print-transact-sql.md)
- [RAISERROR (Transact-SQL)](../../t-sql/language-elements/raiserror-transact-sql.md)
- [Implement internal activation in Service Broker](implementing-internal-activation.md)
