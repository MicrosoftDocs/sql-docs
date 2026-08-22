---
title: Connection Pooling with mssql-python
description: Learn how to configure and use connection pooling to improve application performance with the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Connection pooling with mssql-python

Connection pooling improves application performance by reusing database connections instead of creating new ones for each request. Opening a connection involves multiple time-consuming steps:

- The driver establishes a network socket.
- The driver completes the TLS handshake.
- The driver authenticates with the server.
- The driver validates connection parameters.

Connection pooling keeps connections open and available for reuse, so your app doesn't need to repeat these steps for each request.

## Default behavior

Connection pooling is **enabled by default** when you create your first connection. The default settings are:

| Setting | Default value | Description |
| --------- | --------------- | ------------- |
| `max_size` | 100 | Maximum connections per unique connection string. |
| `idle_timeout` | 600 seconds (10 minutes) | Number of seconds before idle connections are closed. |

```python
import mssql_python

# Pooling is automatically enabled with defaults
conn = mssql_python.connect(connection_string)
```

## Configure connection pooling

Configure pooling before creating any connections:

```python
import mssql_python

# Configure custom pool settings
mssql_python.pooling(max_size=50, idle_timeout=300)

# Now create connections
conn = mssql_python.connect(connection_string)
```

### Parameters

The `pooling()` function accepts the following parameters:

| Parameter | Type | Default | Description |
| ----------- | ------ | --------- | ------------- |
| `max_size` | int | 100 | Maximum number of pooled connections per connection string. |
| `idle_timeout` | int | 600 | Seconds before idle connections are evicted from the pool. |
| `enabled` | bool | True | Enable or disable pooling. |

## Disable connection pooling

To disable pooling, call `pooling()` with `enabled=False` before creating connections:

```python
import mssql_python

mssql_python.pooling(enabled=False)

# Connections are now created and destroyed per use
conn = mssql_python.connect(connection_string)
```

> [!NOTE]
> Set the pooling configuration before establishing any connections. Calling `pooling()` after creating connections has no effect.

## How pooling works

### Connection string isolation

Each unique connection string maintains its own independent pool. Pools don't share connections across different connection strings:

```python
# These use separate pools
conn1 = mssql_python.connect("Server=<server1>;Database=<database1>;...")
conn2 = mssql_python.connect("Server=<server2>;Database=<database2>;...")
```

### Connection lifecycle

**Acquire (getting a connection):**

1. The pool removes stale (idle-expired) connections.
1. The pool tries to reuse an existing connection:
   - It checks if the connection is alive.
   - It resets the connection state.
   - If both checks succeed, it returns the connection.
1. If no reusable connection exists and the pool is under `max_size`, the driver creates a new connection.
1. If the pool is at capacity with no valid connections, the driver raises an error.

**Release (returning a connection):**

1. If the pool has capacity, it stores the connection for reuse.
1. If the pool is at `max_size`, the driver closes the connection immediately.

### Connection health checks

The driver performs connection health checks before reusing a pooled connection.

1. **Alive check**: Ensures the network connection is still valid.
1. **Reset check**: Resets session state (isolation level, settings) for clean reuse.

If either check fails, the pool discards the connection and creates a new one.

### Automatic cleanup

- **Idle timeout**: The driver closes connections that are unused for longer than the `idle_timeout` value.
- **Process exit**: An `atexit` handler closes all pooled connections when the Python process exits.

## Best practices

### Size your pool appropriately

Match your pool size to your application's concurrency.

```python
# For a web application with 20 concurrent requests
mssql_python.pooling(max_size=25)  # Slightly more than expected concurrency
```

### Use context managers

Context managers ensure you properly return connections to the pool.

```python
with mssql_python.connect(connection_string) as conn:
    cursor = conn.cursor()
    cursor.execute("SELECT TOP 5 Name, ListPrice FROM Production.Product")
    rows = cursor.fetchall()
# Connection returned to pool
```

### Keep connection strings consistent

Different parameters in connection strings create separate pools.

```python
# These create THREE separate pools (inefficient)
conn1 = mssql_python.connect("Server=<server>;Database=<database>;Encrypt=yes;")
conn2 = mssql_python.connect("SERVER=<server>;DATABASE=<database>;ENCRYPT=yes;")  # Different case
conn3 = mssql_python.connect("Server=<server>;Database=<database>;Encrypt=yes;", timeout=30)  # Extra parameter

# Use a constant connection string instead
CONNECTION_STRING = "Server=<server>;Database=<database>;Encrypt=yes;"
conn1 = mssql_python.connect(CONNECTION_STRING)
conn2 = mssql_python.connect(CONNECTION_STRING)  # Same pool
```

### Consider Azure SQL connection limits

Azure SQL Database enforces [connection limits based on the service tier](/azure/azure-sql/database/resource-limits-logical-server). The following values are approximate; check the linked documentation for current limits:

| Service tier | Max concurrent connections |
| -------------- | --------------------------- |
| Basic | 30 |
| Standard S0-S2 | 60-120 |
| Standard S3 and later versions | 200 |
| Premium | 500 |

Size your `max_size` value below these limits.

```python
# For Azure SQL Standard S2 (120 limit)
mssql_python.pooling(max_size=100)  # Leave headroom
```

### Tune idle timeout for your workload

- **Frequent connections**: Use a longer `idle_timeout` value to keep connections warm.
- **Sporadic connections**: Use a shorter `idle_timeout` value to release resources.

```python
# High-frequency API: keep connections warm
mssql_python.pooling(idle_timeout=1800)  # 30 minutes

# Batch job running every hour: release between runs
mssql_python.pooling(idle_timeout=60)  # 1 minute
```

## Limitations

The current implementation has some limitations compared to other drivers:

| Feature | Status |
| --------- | -------- |
| `ClearPool()` / `ClearAllPools()` | Not available. |
| Pool statistics/monitoring | Not available. |
| Per-connection pool override | Not available. |
| Minimum pool size | Not configurable. |

## Example: Web application pattern

The following Flask example shows how connections are pooled transparently across requests:

```python
import mssql_python
from flask import Flask, g

app = Flask(__name__)

# Configure pooling at startup
mssql_python.pooling(max_size=20, idle_timeout=300)

def get_db():
    if 'db' not in g:
        g.db = mssql_python.connect(app.config['DATABASE_URL'])
    return g.db

@app.teardown_appcontext
def close_db(error):
    db = g.pop('db', None)
    if db is not None:
        db.close()  # Returns to pool

@app.route('/products')
def list_products():
    conn = get_db()
    cursor = conn.cursor()
    cursor.execute("SELECT TOP 5 Name, ListPrice FROM Production.Product")
    return cursor.fetchall()
```

## Recognize pool exhaustion

When all connections in the pool are in use and you request a new connection, you see symptoms like:

- Connections hang or time out while waiting for a free connection.
- Application throughput suddenly drops under load.
- Memory usage climbs as the driver creates connections that it can't reuse.

**Common causes:**

- **Connections aren't returned to the pool.** Always close connections when you're done, or use context managers. A connection that isn't closed stays checked out.
- **Pool sized too small for the workload.** If you have 50 concurrent requests but `max_size=20`, 30 requests wait.
- **Long-running queries hold connections.** Break up long operations or use dedicated connections for batch work.

**How to fix:**

```python
# 1. Always use context managers to guarantee return
with mssql_python.connect(connection_string) as conn:
    cursor = conn.cursor()
    cursor.execute("SELECT ...")
    rows = cursor.fetchall()
# Connection returned to pool here, even if an exception occurs

# 2. Size the pool to match your concurrency
mssql_python.pooling(max_size=50)  # Match or slightly exceed expected concurrent connections

# 3. Reduce idle timeout if connections go stale
mssql_python.pooling(idle_timeout=120)
```

## Related content

- [Manage connections with mssql-python](connection-management.md)
- [Connection strings for mssql-python](connection-strings.md)
- [Performance tuning for mssql-python applications](performance-tuning.md)
