---
title: Remote Service Bindings
description: "A remote service binding establishes a relationship between a local database user, the certificate for the user, and the name of a remote service."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Remote service bindings

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

A remote service binding establishes a relationship between a local database user, the certificate for the user, and the name of a remote service. Service Broker uses the remote service binding to provide dialog security for conversations that target the remote service.

Service Broker determines the users for a conversation when a conversation begins, using the information in the database that hosts the initiating service. A conversation that uses dialog security involves four users. Each database must contain a user for the initiator of the conversation and a user for the target of the conversation. The initiator of the conversation is the user that begins the dialog. The remote service binding specifies the user for the target of the conversation. An initiating service can act as **PUBLIC** in the remote database by specifying `ANONYMOUS` = `ON` in the remote service binding.

## Related content

- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md)
- [ALTER REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/alter-remote-service-binding-transact-sql.md)
- [DROP REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/drop-remote-service-binding-transact-sql.md)
