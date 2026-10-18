---
title: "sys.messages (Transact-SQL)"
description: The sys.messages view contains a row for each message_id or language_id of the error messages in the system, for both system-defined and user-defined messages.
author: dimitri-furman
ms.author: dfurman
ms.date: 08/28/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "messages_TSQL"
  - "sys.messages_TSQL"
  - "sys.messages"
  - "messages"
helpviewer_keywords:
  - "error messages [SQL Server]"
  - "sys.messages catalog view"
  - "error numbers [SQL Server]"
dev_langs:
  - "TSQL"
---
# Messages (for errors) catalog views - sys.messages

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Contains a row for each `message_id` or `language_id` of the error messages in the system, for both system-defined and user-defined messages. For more information, see [sp_addmessage](../system-stored-procedures/sp-addmessage-transact-sql.md).

| Column name | Data type | Description |
| --- | --- | --- |
| `message_id` | **int** | ID of the message. Is unique across server. Message IDs less than 50,000 are system messages. |
| `language_id` | **smallint** | Language ID for which the text in `text` is used, as defined in `sys.languages`. This value is unique for a specified `message_id`. |
| `severity` | **tinyint** | Severity level of the message, between 0 and 25. This value is the same for all message languages within a `message_id`. |
| `is_event_logged` | **bit** | 1 = Message is event-logged when an error is raised. This value is the same for all message languages within a `message_id`. |
| `text` | **nvarchar(2048)** | Text of the message used when the corresponding `language_id` is active. |

## Permissions

Requires membership in the **public** role. For more information, see [Metadata visibility configuration](../security/metadata-visibility-configuration.md).

## Related content

- [THROW (Transact-SQL)](../../t-sql/language-elements/throw-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Exception Message Box Programming](/previous-versions/sql/sql-server-2016/ms166343(v=sql.130))
- [Error Messages](../native-client-odbc-error-messages/error-messages.md)
- [Database Engine events and errors](../errors-events/database-engine-events-and-errors.md)
