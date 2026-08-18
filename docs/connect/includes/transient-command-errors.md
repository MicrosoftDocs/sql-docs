---
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 08/14/2026
ms.service: sql
ms.topic: include
ai-usage: ai-assisted
---

The following errors occur after a connection is established, while a command is executing. Retry the whole transaction, not the individual statement. Retrying one statement inside a transaction can duplicate earlier work or violate the transaction's ordering guarantees.

| Error | Failure type | Message | Troubleshooting |
| --- | --- | --- | --- |
| `1204` | Lock resource exhausted | `The instance of the SQL Server Database Engine cannot obtain a LOCK resource at this time. Rerun your statement when there are fewer active users. Ask the database administrator to check the lock and memory configuration for this instance, or to check for long-running transactions.` | The lock manager can't allocate more lock resources on the server. Roll back the transaction and retry after a short backoff. Sustained occurrences indicate contention or memory pressure that scaling or query tuning must address. |
| `1205` | Deadlock victim | `Transaction (Process ID %d) was deadlocked on %.*ls resources with another process and has been chosen as the deadlock victim. Rerun the transaction.` | The engine picked this session to break a deadlock and rolled its transaction back. Roll back on the client side to release any remaining state, then retry the whole transaction. |
| `1222` | Lock-request timeout | `Lock request time out period exceeded.` | The engine gave up waiting for a lock. Retry the transaction on a short backoff. Recurring occurrences indicate blocking that indexing, query tuning, or `SET LOCK_TIMEOUT` review must address. |
| `3960` | Snapshot isolation update conflict | `Snapshot isolation transaction aborted due to update conflict. You cannot use snapshot isolation to access table '%.*ls' directly or indirectly in database '%.*ls' to update, delete, or insert the row that has been modified or deleted by another transaction. Retry the transaction or change the isolation level for the update/delete statement.` | Two transactions running under snapshot isolation tried to update the same row. The engine aborted this transaction. Retry the whole transaction, or change the isolation level for the conflicting write. Add to a custom transient-error list if your application uses snapshot isolation. |

Statement-level errors that reflect a batch or schema problem (for example, `102` syntax errors, `207` invalid column, `2812` missing stored procedure) aren't transient. Fix the query text or the schema binding; retry doesn't help.

Error message text is from the [sys.messages](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md) catalog view. These errors come from the SQL Server engine, so their numbers are the same across SQL Server, Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, and dedicated SQL pools in Azure Synapse Analytics, regardless of driver.
