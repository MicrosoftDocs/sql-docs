---
title: "Upgrade (Service Broker)"
description: "Service Broker operations do not change when a database or an instance of the Database Engine is upgraded."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: upgrade-and-migration-article
---

# Upgrade (Service Broker)

Service Broker operations don't change when a database or an instance of the Database Engine are upgraded. The Service Broker features available in SQL Server across supported versions.

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Databases are upgraded when the following are true:

- They're attached to an instance of SQL Server Database Engine after they're detached from an instance of a previous version of the database engine.

- The instance of the database engine they're in is upgraded from a previous version.

## Conversation Priorities

When a SQL Server database is upgraded to a newer version, conversations continue to operate as they did in the previous version.

## Related content

- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [CREATE BROKER PRIORITY (Transact-SQL)](../../t-sql/statements/create-broker-priority-transact-sql.md)
- [Conversation priorities](conversation-priorities.md)
- [SQL Server installation guide](../install-windows/install-sql-server.md)
