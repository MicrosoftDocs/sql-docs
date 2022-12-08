---
title: Managing Conversation Priorities
description: "Service Broker conversation priorities let you specify which conversations to prioritize so that their messages are not blocked by large numbers of messages from less important conversations."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Managing Conversation Priorities

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker conversation priorities let you specify which conversations to prioritize so that their messages are not blocked by large numbers of messages from less important conversations.

## Enabling Conversation Priorities

Conversation priorities are always active for RECEIVE statements. The HONOR_BROKER_PRIORITY database option must be on to make conversation priorities active for SEND statements. By default, this option is off for all databases.

An administrator can enable conversation priorities for SEND statements in a database by using the following statement:

```sql
    ALTER DATABASE MyDatabase SET HONOR_BROKER_PRIORITY ON;
```

An administrator can turn off conversation priorities for SEND statements by using the following statement:

```sql
    ALTER DATABASE MyDatabase SET HONOR_BROKER_PRIORITY OFF;
```

## Specifying Conversation Priorities

Conversation priorities are specified by using the CREATE BROKER PRIORITY, ALTER BROKER PRIORITY, and DROP BROKER PRIORITY statements. For more information, see [Conversation Priorities](conversation-priorities.md).

## Querying Conversation Priorities

Conversation priorities are stored in the **sys.conversation_priorities** system view. The following statement lists all the conversation priorities in the current database:

```sql
    SELECT scp.name AS priority_name,
           ssc.name AS contract_name,
           ssvc.name AS local_service_name,
           scp.remote_service_name,
           scp.priority AS priority_level
    FROM sys.conversation_priorities AS scp
        INNER JOIN sys.service_contracts AS ssc
           ON scp.service_contract_id = ssc.service_contract_id
        INNER JOIN sys.services AS ssvc
           ON scp.local_service_id = ssvc.service_id
    ORDER BY contract_name, local_service_name,
             remote_service_name;
```

## See also

- [ALTER BROKER PRIORITY (Transact-SQL)](../../t-sql/statements/alter-broker-priority-transact-sql.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [CREATE BROKER PRIORITY (Transact-SQL)](../../t-sql/statements/create-broker-priority-transact-sql.md)
- [DROP BROKER PRIORITY (Transact-SQL)](../../t-sql/statements/drop-broker-priority-transact-sql.md)
- [sys.conversation_priorities (Transact-SQL)](../../relational-databases/system-catalog-views/sys-conversation-priorities-transact-sql.md)
- [sys.transmission_queue (Transact-SQL)](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)
