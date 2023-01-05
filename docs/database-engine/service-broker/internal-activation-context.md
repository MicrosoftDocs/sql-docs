---
title: Internal Activation Context
description: "This topic describes the execution context for a stored procedure that is started by internal activation."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Internal Activation Context

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This topic describes the execution context for a stored procedure that is started by internal activation.

## Security Context

A queue configured for activation must also specify the user that the activation stored procedure runs as. SQL Server impersonates this user before starting the stored procedure.

When the stored procedure also specifies an EXECUTE AS clause, two impersonations occur. SQL Server first impersonates the user specified for the queue and executes the stored procedure. When the stored procedure executes, the procedure impersonates the user specified in the EXECUTE AS clause of the procedure.

Notice that the user specified for a remote service binding is generally a different user from the user specified for activation. The permissions required for each user also differ. The remote service binding user does not need permission to read from the queue or execute stored procedures in the database, while the user specified for activation does not need permission to send messages to the service. For more information on user permissions, see [Identity and Access Control (Service Broker)](identity-and-access-control.md) and [Service Broker Dialog Security](service-broker-dialog-security.md).

## Session Settings

Service Broker executes internally activated service programs on a background session distinct from the connection that created the message. The options set for this session are the default options for the database.

Within a session started by Service Broker, SQL Server writes the output of PRINT and RAISERROR statements to the SQL Server error log. Service Broker does not provide parameters to an activated stored procedure. Service Broker does not consider return values from an activated stored procedure and does not process result sets from an activated stored procedure.

## Transaction Context

An activated stored procedure is responsible for managing transactions. SQL Server does not start a transaction before activating the stored procedure, and the stored procedure runs in a different transaction context than the internal operation that activates the procedure. For a discussion of managing transactions in activated stored procedures, see [Transactional Messaging](transactional-messaging.md).

## Failure Detection

An activated stored procedure must receive messages from the queue that activated the procedure. If the stored procedure exits without receiving messages or the queue monitor detects that the stored procedure is not receiving messages after a short time-out, the queue monitor considers the stored procedure to have failed. In this case, the queue monitor stops activating the stored procedure.

## See also

- [PRINT (Transact-SQL)](../../t-sql/language-elements/print-transact-sql.md)
- [RAISERROR (Transact-SQL)](../../t-sql/language-elements/raiserror-transact-sql.md)
- [Implementing Internal Activation](implementing-internal-activation.md)