---
title: Configurable retry logic
description: Use the JDBC driver's configurable retry logic (CRL) to automatically retry failed statements or connection attempts based on SQL Server error numbers, with timing parameters you control.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto
ms.date: 06/02/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# Configurable retry logic (JDBC)

[!INCLUDE [Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Configurable retry logic (CRL) is a rule-based mechanism that automatically retries failed statements or initial connection attempts based on SQL Server error numbers that you choose, with timing parameters that you control. CRL was introduced in Microsoft JDBC Driver 12.10 for SQL Server.

CRL is separate from [idle connection resiliency and the `connectRetryCount` / `connectRetryInterval` properties](connection-resiliency.md). Idle resiliency transparently recovers broken connections, and `connectRetryCount` retries the initial authentication on a fixed schedule for a built-in list of transient errors. CRL lets you decide *which* errors are retryable, *how many times*, and *how long* to wait between attempts. You can use all three mechanisms together.

## What CRL retries

CRL handles two distinct scenarios, each controlled by its own connection property:

| Scenario | Property | When the retry runs | Triggered by |
| --- | --- | --- | --- |
| Statement execution failure | `retryExec` | While executing a statement (for example, `executeQuery`, `executeUpdate`, `execute`, or batch execution) | A `SQLServerException` whose error number matches a configured statement rule |
| Initial connection or authentication failure | `retryConn` | Inside the driver's connect-retry loop (which is gated by `connectRetryCount` and `loginTimeout`) | A `SQLServerException` during authentication whose error number matches a configured connection rule, or by default, any transient error already covered by the built-in retry list |

For statements, the driver retries only the failing command. The driver doesn't reset the current transaction state, so design your rules around errors that leave the session usable, such as deadlock victim (1205) or lock timeout (1222).

For connections, CRL augments or replaces the driver's built-in list of transient connection errors. See [Connection retry rules](#connection-retry-rules-retryconn) for the `+` prefix semantics.

## Enable CRL

CRL has two layers:

1. The connection-retry layer is on by default: as long as `connectRetryCount > 0` (the default is 1), the driver retries the [built-in transient connection error list](#built-in-transient-connection-error-list).
1. The customization layer (your own `retryExec` and `retryConn` rules) is off by default. Both properties are empty strings unless you set them. You can set them through the JDBC URL, a `Properties` object, or a `SQLServerDataSource`. The driver strips the optional `{...}` wrappers in all three forms.

Java snippets in this article omit imports and class wrappers for brevity.

### In the JDBC URL

Each rule (or the whole list of rules) must be wrapped in braces (`{...}`) because the JDBC URL uses `;` as a separator:

```text
jdbc:sqlserver://server;databaseName=db;retryExec={1205,1222:3,2*2:select,update}
jdbc:sqlserver://server;databaseName=db;retryConn={+<customErrorNumber>}
```

### With a `Properties` object

```java
Properties props = new Properties();
props.setProperty("user", "...");
props.setProperty("password", "...");
props.setProperty("retryExec", "1205,1222:3,2*2:select,update");
props.setProperty("retryConn", "+<customErrorNumber>");
Connection c = DriverManager.getConnection("jdbc:sqlserver://server;databaseName=db", props);
```

### With `SQLServerDataSource`

The same setters exist on the `ISQLServerDataSource` interface:

```java
SQLServerDataSource ds = new SQLServerDataSource();
ds.setServerName("server");
ds.setDatabaseName("db");
ds.setRetryExec("1205,1222:3,2*2:select,update");
ds.setRetryConn("+<customErrorNumber>");
```

## Rule syntax

A single rule has up to three colon-separated sections:

```text
<errorNumbers> : <retryTimings> : <queryFilter>
```

| Section | Required? | Meaning |
| --- | --- | --- |
| `errorNumbers` | Yes | One SQL Server error number, or several separated by commas (for example, `1205` or `1205,1222`). For connection rules, an optional leading `+` controls whether the existing transient errors are kept. |
| `retryTimings` | Required for **statement** rules. Omit for connection rules. | `retryCount[,initialRetryTime[<op>retryChange]]` where `<op>` is `+` (additive) or `*` (multiplicative). |
| `queryFilter` | Optional, statement rules only | Comma-separated list of SQL keywords. The driver lowercases the value at parse time and lowercases the previously executed SQL at runtime. The rule fires when the joined filter list contains the first token of the executed SQL. Omit the third section to disable filtering. |

To use multiple rules in the same property, separate them with `;` and wrap each rule with `{...}` when placing them in a JDBC URL.

### Timing parameters

For a statement rule with timings `retryCount, initialRetryTime <op> retryChange`:

- `retryCount`: The number of additional attempts the driver makes after the first failure. A value of `0` disables retry. Negative values are invalid.
- `initialRetryTime`: The number of seconds to wait before the first retry. The default value is `0`.
- `<op>`: The operator, which can be `+` or `*`. The default value is `+`.
- `retryChange`: The amount applied to compute subsequent wait times. The default value is `2`. When the operand is `*` and `retryChange` is omitted from the rule, the driver sets `retryChange = initialRetryTime`.

> [!IMPORTANT]  
> If you supply `initialRetryTime` without an explicit operand (for example, `3,5`), the driver uses the default values for the operand and `retryChange` (`+` and `2`). The waits aren't constant. They grow by 2 every retry. To get a constant wait, use the explicit form `retryCount,N+0` (for example, `3,5+0`).

The driver computes the wait time for attempt *i* (0-based) at parse time:

| Operand | Wait for attempt *i* |
| --- | --- |
| `+` (additive) | `initialRetryTime + (retryChange * i)` |
| `*` (multiplicative) | `initialRetryTime * (retryChange ^ i)` |

Examples of timing strings:

| String | retryCount | initialRetryTime | operand | retryChange | Wait sequence (seconds) |
| --- | --- | --- | --- | --- | --- |
| `3` | 3 | 0 (default) | `+` (default) | 2 (default) | 0, 2, 4 |
| `3,5` | 3 | 5 | `+` (default) | 2 (default) | 5, 7, 9 |
| `3,5+5` | 3 | 5 | `+` | 5 | 5, 10, 15 |
| `3,2*2` | 3 | 2 | `*` | 2 | 2, 4, 8 |
| `4,1*` | 4 | 1 | `*` | 1 (equals `initialRetryTime` because the operand is `*` and `retryChange` is omitted) | 1, 1, 1, 1 |

A `retryTimings` section can contain at most one comma. More than one comma raises `R_invalidParameterNumber`.

## Statement retry rules (`retryExec`)

Statement rules retry failed statement execution. When a statement throws a `SQLServerException`, the driver:

1. Looks up the failing error number in the parsed statement rule set.
1. If a rule exists and the current attempt count is less than `retryCount`, optionally checks the last executed SQL against the rule's `queryFilter`.
1. If everything matches, the driver waits for `waitTimes[retryAttempt]` seconds (subject to `queryTimeout`, see [Interaction with queryTimeout and connectRetryCount](#interaction-with-querytimeout-and-connectretrycount)) and reruns the statement.
1. If no rule matches, the driver rethrows the exception.

### Format (statements)

```text
{errorNumber(s):retryCount[,initialRetryTime[<op>retryChange]][:queryFilter]}
```

Statement rules **must** include a timings section. `retryCount` is mandatory. A rule that contains only an error number is interpreted as a *connection* rule, so for statements always supply at least `retryCount`.

### Examples (statements)

| Rule | Effect |
| --- | --- |
| `{1205:3}` | Retry deadlock victim (1205) up to 3 times, no wait between retries. |
| `{1205,1222:3,5+5}` | Retry deadlock victim and lock timeout up to 3 times, waiting 5, 10, and 15 seconds. |
| `{2714:2,1*2}` | Retry "object already exists" up to 2 times, waiting 1 and 2 seconds. |
| `{1205:4,2+2:select,update}` | Retry only when the failing statement starts with `select` or `update`. |
| `{1205:3,5+5};{1222:2,2}` | Two independent rules, separated by `;`. |

Listing several error numbers (for example, `1205,1222`) is shorthand. The driver expands the rule into one entry per error, all sharing the same timing and query filter.

## Connection retry rules (`retryConn`)

Connection rules work with the existing connect-retry loop. This loop is **only active when `connectRetryCount > 0`** (the default is 1). The loop already retries a built-in list of transient connection errors at `connectRetryInterval` seconds apart, up to `connectRetryCount` extra attempts, and is bounded by `loginTimeout`.

A connection rule supplies only an error number section. It has no timings or query filter:

```text
{[+]errorNumber(s)}
```

- Without `+`, the configured rules **replace** the built-in transient error list. Only errors you list are retried.
- With `+` (for example, `{+4060}`), the configured rules are **added** to the built-in list. Both your errors and the driver defaults are retried.

Replace-or-append mode is global for the entire `retryConn` value. If any rule in that value omits `+`, the driver switches to replace mode for all rules in that value. For example, `retryConn={+4060};{40143}` doesn't append 4060 and 40143 to the built-in list. The `40143` rule omits `+`, so the built-in list is dropped and only 4060 and 40143 are retried. To append both, write `retryConn={+4060};{+40143}` (or `retryConn={+4060,40143}`).

The connect loop continues to use `connectRetryInterval` and `connectRetryCount` for pacing and bounding. The CRL rule expands or replaces the set of errors that are eligible for retry.

### Examples (connections)

| Rule | Effect |
| --- | --- |
| `{+<customErrorNumber>}` | Add a custom error number to the built-in transient error list. |
| `{+<customError1>,<customError2>}` | Add multiple custom error numbers to the built-in transient error list. |
| `{4060}` | Retry **only** error 4060. Built-in transient errors are no longer retried by CRL. |

> [!NOTE]  
> `retryConn` doesn't change `loginTimeout` semantics. The existing connect-retry loop still bounds total elapsed time and gives up early if the next `connectRetryInterval` would push elapsed time past `loginTimeout`.

### Built-in transient connection error list

The connect-retry loop already retries the following errors without any CRL configuration, as long as `connectRetryCount > 0`. Listing any of these errors in a `retryConn` rule with `+` is a no-op (they're already covered). Use a `retryConn` rule when you need to add an error that *isn't* in this list, or when you need to drop the list entirely by using the no-`+` replace form.

> [!NOTE]  
> You don't need to append common Azure SQL transient connection errors such as 40197, 40501, 40613, 49918, 49919, or 49920. The built-in list already retries them.

| Error | Message | Troubleshooting |
| --- | --- | --- |
| 64 | A connection was successfully established with the server, but then an error occurred during the login process. (provider: TCP Provider, error: 0 - The specified network name is no longer available.) | The TCP connection dropped mid-handshake. Not a credential failure. If it persists, check for client-side network instability, NIC offload bugs, or an intermediate device that drops half-established connections. |
| 233 | The client was unable to establish a connection because of an error during connection initialization process before login. | Pre-login transport or TLS failure. The server commonly returns this when it can't accept the connection (resource exhaustion, max-connections reached, or an unsupported client). Not a credential failure. Verify server health, then check `loginTimeout`, TLS settings, and client/server TLS version compatibility. |
| 4060 | Cannot open database *database_name* requested by the login. The login failed. | The login authenticated but couldn't open the requested database. Transient causes include the database being in transition (failover, restore, scale-up) or auto-paused. Persistent causes (database doesn't exist, login lacks access) won't be fixed by retry; check the database name, login mapping, and database state. |
| 4221 | Login to read-secondary failed due to long wait on `HADR_DATABASE_WAIT_FOR_TRANSITION_TO_VERSIONING`. | The readable secondary couldn't accept the login because row versions are still missing from in-flight transactions when the replica was recycled. Mitigate by avoiding long write transactions on the primary; the retry usually succeeds once the primary commits or rolls back the open transactions. |
| 10053 | A transport-level error has occurred when sending the request to the server. (provider: TCP Provider, error: 0 - An established connection was aborted by the software in your host machine.) | The *local* side aborted the connection (Windows Sockets `WSAECONNABORTED`). Often a keepalive failure or the local network stack tearing down an idle or half-open connection. Check client-side network health, OS keepalive timers, and any local firewall or VPN client. |
| 10054 | A transport-level error has occurred when sending the request to the server. (provider: TCP Provider, error: 0 - An existing connection was forcibly closed by the remote host.) | The *remote* side sent a TCP reset (Windows Sockets `WSAECONNRESET`). Common causes: the peer process crashed, a firewall injected a reset, or the Azure SQL gateway closed an idle connection. For idle-reset patterns, enable TCP keepalive on the client or shorten the connection-pool idle timeout. |
| 10928 | Resource ID: *N*. The *limit-type* limit for the database is *N* and has been reached. See `sys.dm_exec_sessions` for usage. | A resource governance limit on the database has been hit (sessions, workers, or requests). Identify the limit type from the message, then reduce concurrency, scale up the database, or shorten long-running operations holding the resource. |
| 10929 | Resource ID: *N*. The *limit-type* minimum guarantee is *N*, maximum limit is *N* and the current usage for the database is *N*. However, the server is currently too busy to support requests greater than *N* for this database. | The database is over its minimum guarantee and the underlying server is throttling. Retry typically succeeds when neighbor load drops. Sustained occurrences indicate you need a higher service tier or a less noisy environment. |
| 40020<br>40143<br>40166<br>40540 | Reported in the `Error code %d` slot of error 40197 during failover. | Sub-codes embedded in a 40197 failover message that some paths surface as the top-level error number. The driver lists each individually so it retries on either form. Treat them the same as 40197. |
| 40197 | The service has encountered an error processing your request. Please try again. Error code *N*. | A software upgrade, hardware failure, or other failover event in Azure SQL. Reconnecting routes you to a healthy replica. The embedded error code identifies the failover type. Persistent occurrences should be reported with the session tracing ID. |
| 40501 | The service is currently busy. Retry the request after 10 seconds. Incident ID: *guid*. Code: *N*. | Azure SQL engine throttling. The recommended floor is a 10-second backoff. Sustained throttling indicates you've exceeded the DTU/vCore allowance; scale up or reduce concurrency. |
| 40613 | Database *database_name* on server *server_name* is not currently available. Please retry the connection later. If the problem persists, contact customer support, and provide them the session tracing ID of *guid*. | The database is unavailable, usually mid-failover or briefly during a scale operation. Retry on a backoff; if it persists past a few minutes, capture the session tracing ID and open a support case. |
| 42108 | Can not connect to the SQL pool since it is paused. Please resume the SQL pool and try again. | The dedicated SQL pool (Synapse) is in a paused state. Retry only helps if something resumes the pool in parallel. Resume the pool explicitly or schedule the workload after resume. |
| 42109 | The SQL pool is warming up. Please try again. | The dedicated SQL pool is resuming. Retry on a backoff until it's online; warmup typically takes a few minutes. |
| 49918 | Cannot process request. Not enough resources to process request. | The control plane couldn't allocate resources for the request right now. Retry on a backoff. Persistent occurrences indicate regional capacity pressure. |
| 49919 | Cannot process create or update request. Too many create or update operations in progress for subscription *N*. | Subscription-level concurrency limit on management operations. Reduce parallel create/update calls or stagger them. |
| 49920 | Cannot process request. Too many operations in progress for subscription *N*. | Subscription-level concurrency limit on operations in flight. Reduce parallelism or wait for in-flight operations to drain. |

The driver's canonical list is the [`TransientError` enum in `SQLServerError.java`](https://github.com/microsoft/mssql-jdbc/blob/main/src/main/java/com/microsoft/sqlserver/jdbc/SQLServerError.java). Error message text is from [Azure SQL transient connection errors](/azure/azure-sql/database/troubleshoot-common-connectivity-issues). Statement-level errors (such as deadlock victim 1205 or lock-request timeout 1222) aren't in this list, because the connect-retry loop only fires during the initial connection. To retry those errors, use a `retryExec` rule.

## Load rules from a properties file

If you don't set `retryExec` or `retryConn` on the connection, CRL looks for a file named `mssql-jdbc.properties` next to the driver JAR on the classpath. The file uses basic `key=value` parsing. Lines that start with `retryExec=` or `retryConn=` are picked up. Values use the same syntax described in this article, with `;` separating multiple rules.

Use exact key names (`retryExec` and `retryConn`), with no leading whitespace. The file isn't parsed as a full Java properties file. The driver does a literal `startsWith` check on each line, so:

- Lines that start with `#` or any other non-`retryExec`/`retryConn` prefix are ignored.
- Lines whose key only starts with `retryExec` or `retryConn` (for example, `retryExec2=...`) are treated as the matching property and can produce parse errors. Don't introduce custom variants.

Example `mssql-jdbc.properties`:

```properties
retryExec=1205:3,5+5;1222:2,2
retryConn=+4060,40143
```

If the file is missing, CRL logs a `FINE` message under the `com.microsoft.sqlserver.jdbc.ConfigurableRetryLogic` logger and continues with no rules. The file path used for the lookup is included in that log message.

Connection string values take precedence. If `retryExec` or `retryConn` are nonempty on the connection, the driver doesn't consult the file for that property.

## Rule refresh behavior

CRL maintains a single, JVM-wide rule set. After construction, the driver refreshes the rules lazily:

- The driver evaluates refresh opportunities during statement execution and connection retries.
- A refresh actually happens only after 30 seconds have elapsed since the previous read.
- If the rules originally came from `mssql-jdbc.properties`, the driver compares the file's last-modified timestamp to the timestamp it recorded on the previous read. If the file changed, the driver reparses it.
- If the rules originally came from a connection string, the driver reapplies the previously stored connection-string value.

This behavior means that edits to `mssql-jdbc.properties` are picked up automatically within about 30 seconds without restarting the application.

> [!IMPORTANT]  
> Because the rule set is a JVM-wide singleton, opening a second connection that sets a different `retryExec` or `retryConn` value replaces the rules for the first connection too. Treat CRL configuration as a process-level setting, not a per-connection setting, when multiple connections in the same JVM disagree.

## Interaction with queryTimeout and connectRetryCount

### Statement retries and `queryTimeout`

When a statement rule fires, the driver compares the next wait time against the connection-level `queryTimeout` value:

- If `queryTimeout >= 0` *and* `timeToWait > queryTimeout`, the driver raises `R_InvalidRetryInterval` instead of retrying. The driver doesn't rethrow the original error. It throws the configuration error.
- The `queryTimeout` connection property defaults to `-1`, so by default the comparison is skipped and any wait is allowed.
- Setting `queryTimeout=0` does **not** disable this check, because `0 >= 0` is true. Any `timeToWait > 0` raises `R_InvalidRetryInterval`.

When you set `queryTimeout` to a positive value, keep `initialRetryTime + (retryCount - 1) * retryChange` (additive) or `initialRetryTime * retryChange^(retryCount-1)` (multiplicative) value below it.

### Connection retries and `connectRetryCount` and `loginTimeout`

`retryConn` doesn't by itself enable authentication retries. The existing properties remain in charge:

- `connectRetryCount` (default 1, range 0-255) is the number of extra authentication attempts. Set it to `0` to disable authentication retries. `retryConn` has no effect when `connectRetryCount = 0`, because the driver throws on the first failure.
- `connectRetryInterval` (default 10 seconds, range 1-60) is the wait between attempts. The first retry runs immediately.
- `loginTimeout` is the overall bound. The driver gives up early if the next interval would push elapsed time past `loginTimeout`.

For more information, see [Connection resiliency (JDBC)](connection-resiliency.md).

## Examples

### Survive deadlocks and lock timeouts on writes

```text
jdbc:sqlserver://server;databaseName=db;retryExec={1205,1222:4,2*2:insert,update,delete,merge}
```

Up to four retries for deadlock victim (1205) or lock timeout (1222), with backoff of 2, 4, 8, and 16 seconds, but only for write statements.

### Rerun schema creation under online operations

```text
retryExec={2714:2,1+1};{3702:2,1+1}
```

Retries error 2714 (`object already exists`) and 3702 (`cannot drop database currently in use`) twice each, with 1 and 2 second waits.

### Add a custom error to the transient-error list

```text
retryConn={+<customErrorNumber>}
```

Adds a custom error number that isn't already in the built-in list. If you append a built-in Azure SQL transient error such as 40197, 40501, 40613, 49918, 49919, or 49920, nothing changes because the driver already retries it.

### Configure CRL through a properties file

Place `mssql-jdbc.properties` next to the driver JAR:

```properties
retryExec=1205:3,5+5:select,update
retryConn=+<customErrorNumber>
```

Don't set `retryExec` or `retryConn` on the connection. The driver reads the rules from the file and rereads after each modification (checked once every 30 seconds).

## Troubleshoot CRL

Enable `FINE` (or finer) logging on the `com.microsoft.sqlserver.jdbc.ConfigurableRetryLogic` logger to see file-read attempts and parsing decisions:

```properties
com.microsoft.sqlserver.jdbc.ConfigurableRetryLogic.level=FINE
```

Common configuration errors:

| Error message key | Cause |
| --- | --- |
| `R_invalidParameterNumber` | A nonnumeric token appeared where the driver expected an error number or a timing parameter, or `retryTimings` contained more than one comma. |
| `R_InvalidRuleFormat` | The rule had more than 3 colon-separated sections. |
| `R_InvalidRetryInterval` | A statement rule's computed wait time exceeds `queryTimeout`. Shorten the wait or raise `queryTimeout`. |
| `R_PathInvalid` or `R_URLInvalid` | The driver couldn't resolve a path to look for `mssql-jdbc.properties`. |
| `R_errorReadingStream` | I/O error while reading `mssql-jdbc.properties`. |

> [!NOTE]  
> The text of an `R_invalidParameterNumber` message reads *The parameter number {0} is not valid*, which is the same resource string the driver uses for prepared-statement parameter-binding errors. When CRL throws it, the offending value is your retry rule token (for example, an error number or timing element that isn't numeric), not a `PreparedStatement` parameter index.

Things to check when a rule doesn't fire:

1. The exception's `SQLServerError.getErrorNumber()` actually matches the number in your rule. SQL Server can wrap some failures into different numbers depending on context (for example, deadlock vs. lock timeout).
1. For statement rules with a `queryFilter`, the first whitespace-delimited token of the SQL you executed (lowercased) is in the filter list. Comments and `WITH` CTEs change the first token.
1. `retryCount` retries are *additional* attempts. The first execution doesn't count.
1. For connection rules, `connectRetryCount` is greater than 0 and `loginTimeout` leaves room for at least one more `connectRetryInterval`.
1. The rule has the right shape. Statement rules need a timings section. Connection rules must not.

## Related content

- [Connection resiliency (JDBC)](connection-resiliency.md)
- [Understanding timeout properties in the JDBC driver](understand-timeouts.md)
- [Set the connection properties](setting-the-connection-properties.md)
- [Diagnosing problems with the JDBC driver](diagnosing-problems-with-the-jdbc-driver.md)
