---
title: Improve performance by using a cache
titleSuffix: Azure SQL Database
description: Learn how to use Azure Managed Redis with Azure SQL Database to offload repeated reads, absorb bursts, and reduce latency by using cache-aside, reference-data prefetching, write-through, event-driven invalidation, and write-behind patterns.
author: rakhi-thejraj
ms.author: rakhithejraj
ms.reviewer: jucocchi
ms.date: 11/04/2026
ms.service: azure-sql-database
ms.subservice: performance
ms.topic: how-to
ms.devlang: python
monikerRange: "=azuresql || =azuresql-db"
---

# Improve performance by using a cache with Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Azure SQL Database already caches data pages in the buffer pool and compiled plans in the plan cache, so frequently read data is usually served from memory. But every request still needs a session, a worker thread, CPU to run the query, and a network round trip to the database. When the same results are requested thousands of times per second, those costs add up, and they compete with the transactional work that only the database can do.

[Azure Managed Redis](/azure/redis/overview) is an in-memory data store that sits in front of Azure SQL Database. It serves repeated reads in under a millisecond, absorbs traffic spikes, and keeps hot data available while the database scales, fails over, or resumes from auto-pause. This article shows how to choose a caching pattern for an Azure SQL workload, and then implements five patterns in Python with mssql-python, redis-py, and Microsoft Entra ID authentication, using an online retail store as the example.

## When to add a cache

Besides the buffer pool and plan cache, [Hyperscale](service-tier-hyperscale.md) keeps hot pages in a local SSD cache on each compute replica. This built-in caching saves disk reads, but the database still runs every query. An external cache returns stored results before the request reaches the database, so cached reads use no database connections, workers, or CPU. Use the following table to decide between the two.

| Rely on the built-in cache when | Add an external cache (Azure Managed Redis) when |
| --- | --- |
| Queries are cheap, run infrequently, or return different results each time. | The same expensive results, such as joins and aggregates, are requested far more often than the data changes. Product pages, menus, prices, configuration, and lookup tables are typical examples. |
| CPU, workers, and connections have headroom. | You're near a resource limit: high CPU or worker use, error 10928 (worker limit reached), or log rate governance during peaks. In an elastic pool, offloading a read-heavy database also protects the other databases in the pool. |
| Every read must reflect the latest committed transaction. | Reads can tolerate briefly stale data. Write-through narrows the gap to milliseconds but doesn't close it: a failed cache update, or a change made outside your app, stays stale until the time to live (TTL) expires. |
| Your app can tolerate brief read errors or slower reads during failover, scaling, or maintenance. | Reads must keep working while the database is reconfiguring. During failover, scaling, or planned maintenance, the cache keeps serving reads while database connections drop and the buffer pool warms up again. In the [serverless](serverless-tier-overview.md) tier, cached reads also don't wake the database from auto-pause. |
| The workload is write-heavy with little reuse. | You need data structures that SQL doesn't provide efficiently, such as counters, leaderboards, rate limits, or session state. |

> [!NOTE]
> [In-Memory OLTP](in-memory-oltp-overview.md) (Premium and Business Critical) isn't a cache, but its non-durable (`SCHEMA_ONLY`) memory-optimized tables can hold session state or cached data inside the database. Use them when that data must be read or updated in the same transaction as other tables. Hyperscale doesn't support memory-optimized tables.

## Caching patterns

To understand the various caching patterns, consider an online retail store as a running example. Each store feature reads and writes data differently, so each feature requires its own caching pattern. The following table pairs each pattern with a store feature and explains the benefit for Azure SQL Database.

| Pattern | How it works | In the online retail store | Why it fits Azure SQL Database |
| --- | --- | --- | --- |
| Cache-aside | The application checks the cache first. On a miss, it reads from the database, then populates the cache with a TTL. | Product detail pages that combine product, category, and review summary. | Replaces a multi-table join and aggregate with one key lookup, which saves CPU and workers. |
| Reference-data prefetching | Stable data is loaded into the cache up front and refreshed when the source changes, instead of on a miss. | Categories, tax rates, and shipping options. | Change tracking returns only the rows that changed since the last sync, so each refresh is small. |
| Write-through | After a write commits, the application writes the new value to the cache. | Price and inventory updates. | The `UPDATE` statement returns the new values with an `OUTPUT` clause, so the app updates the cache without reading the row again. |
| Event-driven invalidation | Cache entries are deleted in response to data-change events instead of on a timer. | Order status updated by the warehouse's fulfillment system, or any change from other apps, jobs, or admins. | The Azure Functions SQL trigger reacts to changes from any writer. |
| Write-behind | The application updates the cache right away and flushes changes to the database in batches. | Product view counts. | Turns thousands of single-row updates into one batch, which reduces log rate and lock contention. Best for counters and analytics, where losing a few increments if the cache fails doesn't matter. |
| Session and state offload | Transient state lives in the cache instead of the database. | Shopping carts and user sessions. | Moves frequent session reads and writes out of the database, which avoids hot-row contention and log pressure. |
| Multi-region caching | A cache in each region serves local reads, kept in sync with active geo-replication. | Shoppers in multiple regions. | Serves repeated reads close to shoppers, while all writes still go to the database. |

## Prerequisites

- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/free/).
- An Azure SQL database with [Microsoft Entra authentication](authentication-aad-configure.md) configured. To create one, see [Create a single database](single-database-create-quickstart.md). Allow your client IP address in the server firewall so that you can connect from your machine.
- The sample tables (`Categories`, `Products`, `Reviews`, `ProductStats`, and `Orders`), with change tracking enabled on `Categories` and `Orders` for the reference-data prefetching and event-driven invalidation examples. The [sample app](https://github.com/AzureManagedRedis/amr-caching-pattern-samples/tree/main/azure-sql-retail-sample) creates the tables and adds data with `azd up`.
- An Azure Managed Redis instance. To create one, see [Create an Azure Managed Redis instance](/azure/redis/quickstart-create-managed-redis). Create it in the same region as your application and database to minimize latency.
- Data access for your developer or application identity on both services: a database user created from Microsoft Entra ID, and a Redis access policy assignment on the cache. For Redis, see [Use Microsoft Entra ID for authentication with Azure Managed Redis](/azure/redis/entra-for-authentication). The sample app grants this access for you. In production, grant your managed identities only the access they need, through your deployment pipeline.
- Python 3.10 or later.
- The Azure CLI. Sign in with `az login` so that `DefaultAzureCredential` can use your identity.
- For event-driven invalidation, a [function app](/azure/azure-functions/create-first-function-cli-python) that runs Python and has a managed identity, and Azure Functions Core Tools to publish the code.

> [!TIP]
> For the complete, deployable version of this sample, including infrastructure as code and all five patterns, see [azure-sql-retail-sample](https://github.com/AzureManagedRedis/amr-caching-pattern-samples/tree/main/azure-sql-retail-sample) in the amr-caching-pattern-samples repository on GitHub.

## Implement the caching patterns

The first two steps install the client libraries and connect to both services. The remaining steps show code for the first five patterns in the [Caching patterns](#caching-patterns) table.

## Step 1: Install the client libraries

Install the Azure SQL and Redis client libraries, along with the Azure Identity library for Microsoft Entra authentication.

```bash
pip install "mssql-python>=1.0,<2.0" "redis>=5.0,<9.0" "azure-identity>=1.17,<2.0"
```

[mssql-python](/sql/connect/python/mssql-python/python-sql-driver-mssql-python) is the Microsoft Python driver for SQL Server and Azure SQL. It doesn't require a separate ODBC driver and supports Microsoft Entra authentication directly. On Linux and macOS, install a few operating system libraries first, as described in [Install mssql-python](/sql/connect/python/mssql-python/installation#platform-specific-notes).

## Step 2: Connect to Azure SQL Database and Azure Managed Redis

Use Microsoft Entra ID authentication instead of access keys. Microsoft Entra ID removes the need to store secrets in your application and lets you manage access centrally.

The following code creates a Redis client and an Azure SQL Database connection. Both authenticate through `DefaultAzureCredential`, which uses the application's managed identity when the code runs in Azure, and your developer credential, such as your Azure CLI sign-in, when it runs on your machine.

The cache in this example uses the OSS clustering policy, so the code uses the `RedisCluster` client. By using the OSS policy, the cache redirects the client to other nodes by IP address, which the server certificate doesn't list, so the client turns off the hostname check. The client still verifies that a trusted authority issued the certificate, but it can't confirm that the certificate belongs to your cache. If you use the OSS policy, connect to the cache only through a [private endpoint](/azure/redis/private-link) so that traffic stays on your virtual network. To keep the hostname check on, create the cache with the Enterprise clustering policy and use the standard `redis.Redis` client instead.

<details>
<summary class="color-primary padding-block-xxs">Expand to view the connection code (39 lines)</summary>

```python
import json
import os
import random
from azure.identity import DefaultAzureCredential
from mssql_python import connect
from redis.cluster import RedisCluster
from redis.credentials import CredentialProvider

credential = DefaultAzureCredential()

class EntraCredentialProvider(CredentialProvider):
    """Supplies a fresh Microsoft Entra token to redis-py."""

    def __init__(self, credential, username):
        self._credential = credential
        self._username = username

    def get_credentials(self):
        token = self._credential.get_token("https://redis.azure.com/.default")
        return self._username, token.token

cache = RedisCluster(
    host=os.environ["REDIS_HOST"],          # <name>.<region>.redis.azure.net
    port=10000,
    ssl=True,
    ssl_check_hostname=False,               # Cluster redirects use node IPs; the certificate is still verified.
    credential_provider=EntraCredentialProvider(credential, os.environ["REDIS_USER_OBJECT_ID"]),  # object ID of your Microsoft Entra identity
    decode_responses=True,
)

SQL_CONNECTION_STRING = (
    f"Server=tcp:{os.environ['SQL_SERVER']},1433;"  # for example, myserver.database.windows.net
    f"Database={os.environ['SQL_DATABASE']};"       # for example, retail
    "Encrypt=yes;TrustServerCertificate=no;"
    "Authentication=ActiveDirectoryDefault"
)

def get_db():
    return connect(SQL_CONNECTION_STRING)
```

</details>

> [!NOTE]
> Microsoft Entra tokens expire, typically after about one hour. Azure SQL Database checks the token only when a connection opens, so open connections, including the ones that mssql-python pools by default, keep working after it expires. New connections get a current token from `DefaultAzureCredential`. For Redis, the preceding code gets a token only when a connection opens. To renew tokens on open connections, use the [redis-entraid](https://github.com/redis/redis-py-entraid) package, or reconnect with a new token.

## Step 3: Cache product pages with cache-aside

Cache-aside is the most common caching pattern. A product page joins products and categories and aggregates reviews. With cache-aside, the application checks Redis first, runs the query only on a miss, and then populates the cache with a TTL. A few popular products drive most reads, so the hit rate is high.

Because most reads are served from memory, fewer queries reach the database. That lowers CPU and worker use, so you can absorb read spikes without scaling up the database or adding read replicas. To identify the queries worth caching, see [Find what to cache](#find-what-to-cache).

<details>
<summary class="color-primary padding-block-xxs">Expand to view the cache-aside code (44 lines)</summary>

```python
PRODUCT_TTL = 300   # seconds
NOT_FOUND_TTL = 30  # seconds

# The product page query: one product, its category name, and its review summary.
PRODUCT_PAGE_SQL = """
SELECT p.ProductId, p.Name, p.Price, p.Stock,
       c.Name AS Category,
       r.ReviewCount, r.AvgRating
FROM dbo.Products AS p
JOIN dbo.Categories AS c ON c.CategoryId = p.CategoryId
OUTER APPLY (
    SELECT COUNT(*) AS ReviewCount, CAST(AVG(Rating * 1.0) AS DECIMAL(3,2)) AS AvgRating
    FROM dbo.Reviews WHERE ProductId = p.ProductId
) AS r
WHERE p.ProductId = ?;
"""

def get_product(product_id: int) -> dict | None:
    key = f"product:{product_id}"
    # 1. Try the cache first.
    cached = cache.get(key)
    if cached is not None:
        return json.loads(cached)  # "null" means the product doesn't exist.
    # 2. On a miss, read from Azure SQL Database.
    with get_db() as conn, conn.cursor() as cursor:
        cursor.execute(PRODUCT_PAGE_SQL, (product_id,))
        row = cursor.fetchone()
    # 3. Cache "not found" briefly so that repeated requests for a missing ID don't reach the database.
    if row is None:
        cache.set(key, json.dumps(None), ex=NOT_FOUND_TTL)
        return None
    product = {
        "id": row.ProductId,
        "name": row.Name,
        "price": float(row.Price),
        "stock": row.Stock,
        "category": row.Category,
        "reviewCount": row.ReviewCount,
        "avgRating": float(row.AvgRating) if row.AvgRating is not None else None,
    }
    # 4. Populate the cache with a TTL, then return.
    ttl = PRODUCT_TTL + random.randint(0, 60)  # jitter avoids synchronized expiry
    cache.set(key, json.dumps(product), ex=ttl)
    return product
```

</details>

## Step 4: Reference data prefetch with change tracking

Reference data such as categories changes rarely but is joined into many queries. Load it into a Redis hash at startup with no TTL, so reads are always warm, and read the whole hash in one call. Then use change tracking to apply only the rows that changed since the last sync.

<details>
<summary class="color-primary padding-block-xxs">Expand to view the full load code (20 lines)</summary>

```python
SYNC_KEY = "sync:categories:version"  # the change tracking version the cache reflects

# Full load: at startup, or when the cache is too far behind to sync.
def load_categories():
    with get_db() as conn, conn.cursor() as cursor:
        # Read the version first so changes made during the load are caught by the next sync.
        cursor.execute("SELECT CHANGE_TRACKING_CURRENT_VERSION();")
        version = cursor.fetchone()[0]
        cursor.execute("SELECT CategoryId, Name FROM dbo.Categories;")
        rows = cursor.fetchall()
    # 1. Remove the saved version first. If this reload stops partway, the next sync reloads everything.
    cache.delete(SYNC_KEY)
    # 2. Replace the hash (CategoryId -> Name).
    pipe = cache.pipeline()
    pipe.delete("ref:categories")
    if rows:
        pipe.hset("ref:categories", mapping={str(r.CategoryId): r.Name for r in rows})
    pipe.execute()
    # 3. Save the version only after the hash is rebuilt.
    cache.set(SYNC_KEY, version)
```

</details>

Load the full table once at startup. Then run a sync on a schedule, for example every 30 seconds. Each run reads only the changed rows, so it costs far less than rereading the table. Keep the interval much shorter than the change tracking retention period (two days in the sample). If the cache falls behind that period, for example after a long outage, the sync does a full reload.

<details>
<summary class="color-primary padding-block-xxs">Expand to view the change tracking sync code (34 lines)</summary>

```python
# Incremental sync: run on a schedule.
def sync_categories():
    last = cache.get(SYNC_KEY)
    with get_db() as conn, conn.cursor() as cursor:
        cursor.execute("SELECT CHANGE_TRACKING_CURRENT_VERSION(), "
                       "CHANGE_TRACKING_MIN_VALID_VERSION(OBJECT_ID('dbo.Categories'));")
        current, min_valid = cursor.fetchone()
        # Never loaded, or older than the retained change history: reload everything.
        if last is None or int(last) < min_valid:
            load_categories()
            return
        # Rows changed since the last sync. Name is NULL for deleted rows.
        cursor.execute(
            "SELECT ct.CategoryId, ct.SYS_CHANGE_OPERATION AS Op, c.Name "
            "FROM CHANGETABLE(CHANGES dbo.Categories, ?) AS ct "
            "LEFT JOIN dbo.Categories AS c ON c.CategoryId = ct.CategoryId;",
            (int(last),),
        )
        changes = cursor.fetchall()
        # Cleanup can remove change history mid-read. If the version is now too old, reload everything.
        cursor.execute("SELECT CHANGE_TRACKING_MIN_VALID_VERSION(OBJECT_ID('dbo.Categories'));")
        if int(last) < cursor.fetchone()[0]:
            load_categories()
            return
    # Apply the changes first.
    pipe = cache.pipeline()
    for ch in changes:  # Op is I (insert), U (update), or D (delete)
        if ch.Op == "D":
            pipe.hdel("ref:categories", str(ch.CategoryId))
        else:
            pipe.hset("ref:categories", str(ch.CategoryId), ch.Name)
    pipe.execute()
    # Save the version last. If the batch fails, the next run safely reapplies the same changes.
    cache.set(SYNC_KEY, current)
```

</details>

Run the sync from a single scheduled job, and start the schedule only after the initial load finishes. Overlapping runs can apply changes out of order, and because the hash has no TTL, a wrong value would stay in the cache until the next change to that row.

Product pages cached with cache-aside keep the old category name until they expire. To show a rename right away, cache only the category ID in the product page, and look up the name in the reference data hash.

## Step 5: Keep the cache current with write-through

With cache-aside alone, a cached value stays stale until its TTL expires, even after the database changes. Write-through closes that gap: every time the application writes to the database, it also writes the new value to the cache. Use it for data that users expect to see change right away, such as prices and stock levels.

In the online retail store, when the application changes a price, it saves the change to the database first and then updates the cached product page, so the next read shows the new price right away. Write-through covers only changes that your application makes.

<details>
<summary class="color-primary padding-block-xxs">Expand to view the write-through code (29 lines)</summary>

```python
def update_price(product_id: int, new_price: float):
    # 1. Save the new price. OUTPUT returns it in the same call, so there's no need to read it again.
    with get_db() as conn, conn.cursor() as cursor:
        cursor.execute(
            """
            UPDATE dbo.Products
            SET Price = ?
            OUTPUT inserted.ProductId, inserted.Price
            WHERE ProductId = ?;
            """,
            (new_price, product_id),
        )
        row = cursor.fetchone()
        conn.commit()  # The database is updated first; the cache comes next.
    # 2. No row back means no product has this ID, so nothing changed. Stop.
    if row is None:
        return
    # 3. The product exists and its price changed. Check whether it's in the cache.
    key = f"product:{product_id}"
    cached = cache.get(key)
    product = json.loads(cached) if cached is not None else None
    # 4. Not cached, or cached as "not found" (now wrong): delete the key so the next read reloads it.
    if product is None:
        cache.delete(key)
        return
    # 5. Cached: update the price. keepttl=True keeps the original expiry. xx=True skips a
    #    key that expired or was deleted after the read, so it isn't recreated without an expiry.
    product["price"] = float(row.Price)
    cache.set(key, json.dumps(product), keepttl=True, xx=True)
```

</details>

Write-through updates the cache after the database commit, but the two operations aren't atomic. A failed cache update or a concurrent read can leave stale data in the cache.

Keep Azure SQL Database as the source of truth. If a cache update fails, retry the update or invalidate the key when connectivity is restored. Retain a TTL as a fallback.

## Step 6: Event-driven invalidation with the Azure Functions SQL trigger

Write-through caching covers only changes your application makes. Other systems can change the same data directly in the database. When that happens, the cache still holds the old value.

For example, shoppers check whether their order ships. To make that check fast, the store caches each order's status in Redis, using cache-aside with a TTL. When the warehouse ships an order, its system updates the order in the database directly. Your application doesn't know about the change, so shoppers keep seeing "Processing" until the cached value expires.

Use this pattern when data changes outside your application and you can't wait for the cached value to expire.

To catch these changes, use the [Azure SQL trigger for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger). The trigger watches the orders table. Whenever a row changes, no matter who changed it, the trigger runs a function that deletes the cached status for that order. The next time a shopper checks, the application reads "Shipped" from the database and caches it again.

<details>
<summary class="color-primary padding-block-xxs">Expand to view the event-driven invalidation code (20 lines)</summary>

```python
# function_app.py
import json
import azure.functions as func
from shared.cache import cache             # The RedisCluster client

app = func.FunctionApp()

# Invoked automatically when the SQL trigger detects order changes
@app.sql_trigger(
    arg_name="changes",
    table_name="dbo.Orders",
    connection_string_setting="SqlConnectionString",
)
def invalidate_order_status(changes: str) -> None:
    # 1. changes is a JSON list of changed rows. Build the cache key for each one.
    #    A set removes duplicates when the same order changed more than once.
    keys = {f"order:{c['Item']['OrderId']}:status" for c in json.loads(changes)}
    # 2. Delete the keys so the next read reloads them. Skip an empty batch (DEL needs keys).
    if keys:
        cache.delete(*keys)
```

</details>

The function deletes keys instead of updating them, which also works for deleted rows, because the trigger sends only the ID for those rows. By default, the trigger checks for changes once per second, so shoppers typically see the new status within a few seconds.

The function connects to the database and the cache by using a managed identity. The SQL trigger also needs database permissions to read change tracking data and to record its progress. The sample includes the complete setup.

## Step 7: Buffer high-frequency writes with write-behind

Updating a view counter on every page load generates many small transactions on the same rows. Instead, increment a Redis hash and flush the totals to Azure SQL Database in one batch.

Use write-behind only for data that can tolerate a short delay and an occasional lost or double-counted increment, such as counters and analytics. Write orders, payments, and inventory directly to the database.

<details>
<summary class="color-primary padding-block-xxs">Expand to view the write-behind code (35 lines)</summary>

```python
# Both names share the {views} hash tag, so Redis stores them in the same cluster slot.
PENDING = "{views}:pending"    # New views collect here
FLUSHING = "{views}:flushing"  # The batch being written to the database

# Add each product's count to its total. Insert a row on its first view; skip deleted products.
UPSERT_VIEWS_SQL = """
MERGE dbo.ProductStats WITH (HOLDLOCK) AS t
USING (SELECT j.ProductId, j.Views
       FROM OPENJSON(?) WITH (ProductId INT, Views BIGINT) AS j
       JOIN dbo.Products AS p ON p.ProductId = j.ProductId) AS s
    ON t.ProductId = s.ProductId
WHEN MATCHED THEN UPDATE SET Views = t.Views + s.Views
WHEN NOT MATCHED THEN INSERT (ProductId, Views) VALUES (s.ProductId, s.Views);
"""

def record_view(product_id: int):
    # Runs on every page view. Adds 1 to the product's count in Redis only.
    cache.hincrby(PENDING, str(product_id), 1)

def flush_views():
    # Runs on a schedule. 1. If the last flush failed, retry its batch in FLUSHING.
    #    Otherwise, move the pending counts to FLUSHING. New views start a fresh PENDING.
    if not cache.exists(FLUSHING):
        if not cache.exists(PENDING):
            return  # No views since the last flush.
        cache.rename(PENDING, FLUSHING)
    # 2. Turn the batch into JSON: [{"ProductId": 3, "Views": 5}, ...]
    counts = cache.hgetall(FLUSHING)
    rows = json.dumps([{"ProductId": int(pid), "Views": int(n)} for pid, n in counts.items()])
    # 3. Add all the counts to the database in one statement.
    with get_db() as conn, conn.cursor() as cursor:
        cursor.execute(UPSERT_VIEWS_SQL, (rows,))
        conn.commit()
    # 4. The database has the counts now, so remove the batch from Redis.
    cache.delete(FLUSHING)
```

</details>

Each flush first renames the pending hash, which sets the current batch aside, so new views keep collecting in a fresh hash while the flush writes the old batch to the database. `RENAME` works only when both keys are in the same cluster slot, so give both key names the same hash tag.

If the database update fails, the set-aside batch stays in Redis, and the next run retries it before taking a new batch. The retry is also where the occasional double count comes from: if a run fails after the database commit but before the batch is deleted, the next run adds the same counts again. Two flushes running at the same time can do the same thing, so run the flush from a single scheduled job, such as a timer-triggered function.

## Best practices

- **Cache the shape the page needs.** Store the joined, ready-to-use result rather than a copy of each table. One key lookup then replaces the whole query. Skip data that's rarely reused, because it wastes memory and pushes out useful keys.
- **Use a consistent, versioned key naming scheme.** Name each key after the data it holds, such as `product:{ProductId}`, so your application and invalidation logic can find the cached result. When the cached value's schema changes, add a version, such as `product:v2:{ProductId}`, so new code doesn't read entries written in the old format. Update the invalidation logic to match.
- **Update the cache only after commit, and invalidate keys for external changes.** Don't update cached values inside an open database transaction because a rollback could leave the cache showing data that was never committed. For changes from other writers, delete affected keys so the next read reloads the data. Deletion is safe to repeat. Retain a TTL as a fallback for missed invalidations.
- **Prevent stampedes.** Add TTL jitter so that keys don't expire together. For very hot keys, let one request rebuild an expired entry, for example by taking a short lock with `SET` and the `NX` and `EX` options, while other requests wait briefly and read the cache again.
- **Fall back gracefully.** If Redis is unavailable, read from the database. Set short Redis timeouts, such as `socket_timeout` and `socket_connect_timeout` in redis-py, and use a circuit breaker so that a cache outage doesn't slow every request. Make sure the database tier can carry the load for a short period.
- **Choose an eviction policy that fits your keys.** This article mixes keys that have a TTL, such as product pages, with keys that don't, such as reference data and pending view counts. A `volatile-lru` policy evicts only keys that have a TTL, which protects the reference data and the counters. If keys without a TTL fill the cache, Redis rejects writes with an out-of-memory error, so monitor the **Used Memory Percentage** metric. If a cache holds only cached query results, use `allkeys-lru` instead.
- **Co-locate and secure resources.** Put the application, the cache, and the database in the same region to keep latency low. In production, connect to both services through [private endpoints](private-endpoint-overview.md).
- **Monitor both sides and right-size.** Track the Redis hit ratio (from the **Cache Hits** and **Cache Misses** metrics), **Evicted Keys**, and **Server Load** alongside the `avg_cpu_percent`, `avg_data_io_percent`, and `max_worker_percent` columns in [sys.dm_db_resource_stats](/sql/relational-databases/system-dynamic-management-objects/sys-dm-db-resource-stats-azure-sql-database) to confirm that the cache is offloading work from the database. If evictions or server load stay high, [scale the cache](/azure/redis/how-to-scale).
- **Serialize efficiently.** JSON is easy to read and debug. For high-throughput paths, test a compact binary format, and measure memory and latency before you switch.

## Find what to cache

The best candidates are queries that run often, use the most CPU in total, and return results that rarely change. [Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store), which is on by default in Azure SQL Database, keeps this history. The following query lists the most frequently run queries over the last seven days. It needs the `VIEW DATABASE PERFORMANCE STATE` permission.

<details>
<summary class="color-primary padding-block-xxs">Expand to view the Query Store query (16 lines)</summary>

```sql
SELECT TOP (20)
    q.query_id,
    SUM(rs.count_executions)                                   AS executions,
    SUM(rs.count_executions * rs.avg_cpu_time) / 1000.0        AS total_cpu_ms,
    SUM(rs.count_executions * rs.avg_duration) / 1000.0        AS total_duration_ms,
    SUM(rs.count_executions * rs.avg_rowcount)
        / NULLIF(SUM(rs.count_executions), 0)                  AS avg_rows,
    MAX(qt.query_sql_text)                                     AS query_text
FROM sys.query_store_runtime_stats          AS rs
JOIN sys.query_store_runtime_stats_interval AS i  ON i.runtime_stats_interval_id = rs.runtime_stats_interval_id
JOIN sys.query_store_plan                   AS p  ON p.plan_id = rs.plan_id
JOIN sys.query_store_query                  AS q  ON q.query_id = p.query_id
JOIN sys.query_store_query_text             AS qt ON qt.query_text_id = q.query_text_id
WHERE i.start_time >= DATEADD(DAY, -7, SYSUTCDATETIME())
GROUP BY q.query_id
ORDER BY executions DESC;
```

</details>

[Query Performance Insight](query-performance-insight-use.md) shows the same data as charts in the Azure portal. For current activity, use [sys.dm_exec_query_stats](/sql/relational-databases/system-dynamic-management-objects/sys-dm-exec-query-stats-transact-sql). Its statistics reset when a plan leaves the plan cache, for example after a failover, and a short spike might not reflect normal load. Confirm candidates in Query Store.

## Related content

- [Best practices for development with Azure Managed Redis](/azure/redis/best-practices-development)
- [Caching guidance](/azure/architecture/best-practices/caching)
- [Resource limits for Azure SQL Database](resource-limits-logical-server.md)
