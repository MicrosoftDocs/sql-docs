---
title: Upgrade (Service Broker)
description: "Service Broker operations do not change when a database or an instance of the Database Engine is upgraded."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Upgrade (Service Broker)

Service Broker operations do not change when a database or an instance of the Database Engine are upgraded. The Service Broker features available in SQL Server across supported versions.

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Databases are upgraded when the following are true:

- They are attached to an instance of SQL Server Database Engine after they are detached from an instance of a previous version of the database engine.

- The instance of the database engine they are in is upgraded from a previous version.

## Conversation Priorities

When a SQL Server database is upgraded to a newer version, conversations continue to operate as they did in the previous version.

## See also

- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [CREATE BROKER PRIORITY (Transact-SQL)](../../t-sql/statements/create-broker-priority-transact-sql.md)
- [Conversation Priorities](conversation-priorities.md)
- [SQL Server installation guide](../install-windows/install-sql-server.md)