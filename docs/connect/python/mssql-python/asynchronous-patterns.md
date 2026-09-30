---
title: Async Patterns with mssql-python
description: Learn how to use the mssql-python driver with Python's asyncio for asynchronous database operations.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Async patterns with mssql-python

The mssql-python driver uses synchronous I/O and doesn't provide native `async`/`await` support. Native async is on the driver roadmap. Until then, you can integrate mssql-python with async applications by using these workaround patterns:

- Thread pool executors for offloading blocking calls.
- Async wrappers around synchronous operations.
- Integration with async frameworks like FastAPI.

> [!NOTE]
> The patterns in this article use `ThreadPoolExecutor` to run synchronous mssql-python calls in background threads. This approach adds threading overhead compared to native async drivers. For I/O-bound database workloads, the overhead is typically acceptable.

## When to use async patterns

The thread-pool approach works well when:

- Your application already uses `asyncio` (for example, FastAPI, aiohttp, or Discord bots) and you need to integrate database calls without blocking the event loop.
- Database queries are I/O-bound, not CPU-bound. The thread pool lets the event loop handle other requests while waiting for Microsoft SQL.
- You have moderate concurrency (tens of concurrent queries, not thousands).

For pure synchronous applications, skip these patterns. Use the driver directly with synchronous code for direct, lower-overhead execution.

## Thread pool executor pattern

The following examples show how to wrap synchronous mssql-python calls in a `ThreadPoolExecutor` for use with `asyncio`.

### Basic async wrapper

Create a simple helper function that executes synchronous mssql-python operations in the thread pool and awaits the result.

```python
import asyncio
from concurrent.futures import ThreadPoolExecutor
import mssql_python
from functools import partial
from typing import Any, Callable

# Create a dedicated thread pool for database operations
db_executor = ThreadPoolExecutor(max_workers=10, thread_name_prefix="db_")

async def run_in_executor(func: Callable, *args, **kwargs) -> Any:
    """Run a synchronous function in the thread pool."""
    loop = asyncio.get_running_loop()
    if kwargs:
        func = partial(func, **kwargs)
    return await loop.run_in_executor(db_executor, func, *args)

# Database functions
def _execute_query(connection_string: str, query: str, params: dict = None) -> list:
    """Synchronous query execution."""
    conn = mssql_python.connect(connection_string)
    cursor = conn.cursor()
    try:
        cursor.execute(query, params or {})
        if cursor.description:
            columns = [col[0] for col in cursor.description]
            return [dict(zip(columns, row)) for row in cursor.fetchall()]
        return []
    finally:
        cursor.close()
        conn.close()

def _execute_scalar(connection_string: str, query: str, params: dict = None) -> Any:
    """Synchronous scalar query."""
    conn = mssql_python.connect(connection_string)
    cursor = conn.cursor()
    try:
        cursor.execute(query, params or {})
        return cursor.fetchval()
    finally:
        cursor.close()
        conn.close()

# Async interfaces
async def async_query(connection_string: str, query: str, params: dict = None) -> list:
    """Execute query asynchronously."""
    return await run_in_executor(_execute_query, connection_string, query, params)

async def async_scalar(connection_string: str, query: str, params: dict = None) -> Any:
    """Execute scalar query asynchronously."""
    return await run_in_executor(_execute_scalar, connection_string, query, params)

# Usage
async def main():
    conn_str = "Server=<server>.database.windows.net;Database=<database>;Authentication=ActiveDirectoryDefault;Encrypt=yes"
    
    # Execute query asynchronously
    products = await async_query(conn_str, "SELECT * FROM Production.Product WHERE ProductSubcategoryID = %(cat)s", {"cat": 5})
    print(f"Found {len(products)} products")
    
    # Execute scalar asynchronously
    count = await async_scalar(conn_str, "SELECT COUNT(*) FROM Production.Product")
    print(f"Total products: {count}")

asyncio.run(main())
```

> [!NOTE]
> This example awaits the two queries one after another, so they run sequentially. The `await` keyword frees the event loop to run other tasks while each query waits, but it doesn't overlap these two queries with each other. To run independent queries at the same time, schedule them together with `asyncio.gather`, as shown in the [Async connection pool](#async-connection-pool) section:
>
> ```python
> products, count = await asyncio.gather(
>     async_query(conn_str, "SELECT * FROM Production.Product WHERE ProductSubcategoryID = %(cat)s", {"cat": 5}),
>     async_scalar(conn_str, "SELECT COUNT(*) FROM Production.Product"),
> )
> ```

## Async connection pool

This section shows how to build an async-friendly wrapper around mssql-python's built-in connection pool for use in `asyncio` applications.

> [!NOTE]
> The mssql-python driver includes built-in connection pooling. The async pool shown here wraps synchronous pooled connections with async context managers for use in `asyncio` applications. You don't need to manage a custom pool if you're only calling mssql-python from a thread pool executor.

### Pooled async database class

Build a reusable async connection pool class that manages a queue of mssql-python connections and provides async methods for query execution.

```python
import asyncio
from concurrent.futures import ThreadPoolExecutor
from contextlib import asynccontextmanager
from typing import Any, Optional
import mssql_python
from dataclasses import dataclass
from queue import Queue, Empty
import threading

@dataclass
class PooledConnection:
    """Wrapper for pooled connection."""
    connection: Any
    cursor: Any
    in_use: bool = False

class AsyncDatabasePool:
    """Async-friendly connection pool for mssql-python."""
    
    def __init__(self, connection_string: str, pool_size: int = 10):
        self.connection_string = connection_string
        self.pool_size = pool_size
        self._pool: Queue[PooledConnection] = Queue(maxsize=pool_size)
        self._executor = ThreadPoolExecutor(max_workers=pool_size, thread_name_prefix="dbpool_")
        self._lock = threading.Lock()
        self._initialized = False
    
    async def initialize(self):
        """Initialize the connection pool."""
        if self._initialized:
            return
        
        loop = asyncio.get_running_loop()
        
        async def create_connection():
            def _create():
                conn = mssql_python.connect(self.connection_string)
                cursor = conn.cursor()
                return PooledConnection(connection=conn, cursor=cursor)
            return await loop.run_in_executor(self._executor, _create)
        
        # Create initial connections
        tasks = [create_connection() for _ in range(self.pool_size)]
        connections = await asyncio.gather(*tasks)
        
        for conn in connections:
            self._pool.put(conn)
        
        self._initialized = True
    
    async def acquire(self, timeout: float = 30.0) -> PooledConnection:
        """Acquire a connection from the pool."""
        loop = asyncio.get_running_loop()
        
        def _acquire():
            try:
                conn = self._pool.get(timeout=timeout)
                conn.in_use = True
                return conn
            except Empty:
                raise TimeoutError("Could not acquire connection from pool")
        
        return await loop.run_in_executor(self._executor, _acquire)
    
    def release(self, conn: PooledConnection):
        """Release a connection back to the pool."""
        conn.in_use = False
        try:
            conn.connection.commit()
        except Exception:
            conn.connection.rollback()
        self._pool.put(conn)
    
    @asynccontextmanager
    async def connection(self):
        """Async context manager for getting a connection."""
        conn = await self.acquire()
        try:
            yield conn
        except Exception:
            conn.connection.rollback()
            raise
        else:
            conn.connection.commit()
        finally:
            self.release(conn)
    
    async def execute(self, query: str, params: dict = None) -> list:
        """Execute query and return results."""
        async with self.connection() as conn:
            loop = asyncio.get_running_loop()
            
            def _execute():
                conn.cursor.execute(query, params or {})
                if conn.cursor.description:
                    columns = [col[0] for col in conn.cursor.description]
                    return [dict(zip(columns, row)) for row in conn.cursor.fetchall()]
                return []
            
            return await loop.run_in_executor(self._executor, _execute)
    
    async def execute_scalar(self, query: str, params: dict = None) -> Any:
        """Execute query and return single value."""
        async with self.connection() as conn:
            loop = asyncio.get_running_loop()
            
            def _execute():
                conn.cursor.execute(query, params or {})
                return conn.cursor.fetchval()
            
            return await loop.run_in_executor(self._executor, _execute)
    
    async def close(self):
        """Close all connections in the pool."""
        while not self._pool.empty():
            try:
                conn = self._pool.get_nowait()
                conn.cursor.close()
                conn.connection.close()
            except Empty:
                break
        
        self._executor.shutdown(wait=True)

# Usage
async def main():
    pool = AsyncDatabasePool("Server=<server>.database.windows.net;Database=<database>;Authentication=ActiveDirectoryDefault;Encrypt=yes", pool_size=5)
    await pool.initialize()
    
    try:
        # Execute queries concurrently
        tasks = [
            pool.execute("SELECT * FROM Production.Product WHERE ProductSubcategoryID = %(cat)s", {"cat": i})
            for i in range(1, 6)
        ]
        results = await asyncio.gather(*tasks)
        
        for i, products in enumerate(results, 1):
            print(f"Category {i}: {len(products)} products")
        
        # Single scalar query
        total = await pool.execute_scalar("SELECT COUNT(*) FROM Production.Product")
        print(f"Total: {total}")
    finally:
        await pool.close()

# Only run the demo when this file is executed directly, not when imported.
if __name__ == "__main__":
    asyncio.run(main())
```

## Integration with FastAPI

FastAPI's `lifespan` context manager handles pool initialization and cleanup automatically.

### Async FastAPI with mssql-python

This example builds on the [Async connection pool](#async-connection-pool) section. Save that section's code in a file named `db.py`, and then create the following FastAPI app in a file named `main.py` next to it. The pool example protects its demo with `if __name__ == "__main__":`, so importing `db.py` doesn't run the demo. This app uses the `lifespan` context manager to initialize the pool on startup and clean it up on shutdown.

```python
from fastapi import FastAPI, Depends, HTTPException
from contextlib import asynccontextmanager
from typing import Optional
import asyncio

from db import AsyncDatabasePool  # The pool class from the previous section

# Initialize pool on startup
pool: Optional[AsyncDatabasePool] = None

@asynccontextmanager
async def lifespan(app: FastAPI):
    """Manage database pool lifecycle."""
    global pool
    pool = AsyncDatabasePool(
        "Server=<server>.database.windows.net;Database=<database>;Authentication=ActiveDirectoryDefault;Encrypt=yes",
        pool_size=10
    )
    await pool.initialize()
    yield
    await pool.close()

app = FastAPI(lifespan=lifespan)

async def get_db():
    """Dependency for database access."""
    return pool

@app.get("/products")
async def list_products(db: AsyncDatabasePool = Depends(get_db)):
    products = await db.execute("SELECT ProductID, Name, ListPrice FROM Production.Product")
    return {"products": products}

@app.get("/products/{product_id}")
async def get_product(product_id: int, db: AsyncDatabasePool = Depends(get_db)):
    products = await db.execute(
        "SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID = %(id)s",
        {"id": product_id}
    )
    if not products:
        raise HTTPException(status_code=404, detail="Product not found")
    return products[0]

@app.get("/stats")
async def get_stats(db: AsyncDatabasePool = Depends(get_db)):
    # Execute multiple queries concurrently
    product_count, subcategory_count, total_value = await asyncio.gather(
        db.execute_scalar("SELECT COUNT(*) FROM Production.Product"),
        db.execute_scalar("SELECT COUNT(*) FROM Production.ProductSubcategory"),
        db.execute_scalar("SELECT SUM(ListPrice) FROM Production.Product"),
    )
    
    return {
        "products": product_count,
        "subcategories": subcategory_count,
        "total_value": float(total_value) if total_value else 0
    }
```

Install the dependencies and run the app with an ASGI server such as Uvicorn. Run this command from the folder that contains `main.py` and `db.py`:

```bash
pip install fastapi uvicorn mssql-python
uvicorn main:app --reload
```

With the server running, open `http://127.0.0.1:8000/products`, `http://127.0.0.1:8000/products/1`, or `http://127.0.0.1:8000/stats` to call each endpoint.

## Background tasks

Run periodic database operations on a schedule without blocking the application event loop.

### Async background worker

Implement a task runner that executes registered database operations at specified intervals, preventing duplicate concurrent runs. This example builds on the [Async connection pool](#async-connection-pool) section, so save that section's code as `db.py`. Then, save the following code as `worker.py` next to it. It configures logging so each run reports its result.

```python
import asyncio
from typing import Callable, Any
from dataclasses import dataclass
from datetime import datetime
import logging

from db import AsyncDatabasePool  # The pool class from the Async connection pool section

logger = logging.getLogger(__name__)

@dataclass
class Task:
    """Background task definition."""
    name: str
    func: Callable
    interval: float  # seconds
    last_run: datetime = None
    running: bool = False

class AsyncTaskRunner:
    """Run database tasks in the background."""
    
    def __init__(self, pool: AsyncDatabasePool):
        self.pool = pool
        self.tasks: dict[str, Task] = {}
        self._running = False
    
    def register(self, name: str, func: Callable, interval: float):
        """Register a periodic task."""
        self.tasks[name] = Task(name=name, func=func, interval=interval)
    
    async def _run_task(self, task: Task):
        """Execute a single task."""
        if task.running:
            return
        
        task.running = True
        try:
            await task.func(self.pool)
            task.last_run = datetime.now()
            logger.info(f"Task {task.name} completed")
        except Exception as e:
            logger.error(f"Task {task.name} failed: {e}")
        finally:
            task.running = False
    
    async def start(self):
        """Start the task runner."""
        self._running = True
        
        while self._running:
            now = datetime.now()
            
            for task in self.tasks.values():
                if task.last_run is None or \
                   (now - task.last_run).total_seconds() >= task.interval:
                    asyncio.create_task(self._run_task(task))
            
            await asyncio.sleep(1)  # Check every second
    
    def stop(self):
        """Stop the task runner."""
        self._running = False

# Example tasks
async def count_products(pool: AsyncDatabasePool):
    """Read-only task: report the current product count."""
    total = await pool.execute_scalar("SELECT COUNT(*) FROM Production.Product")
    logger.info("count_products: %s products", total)

async def check_low_inventory(pool: AsyncDatabasePool):
    """Report how many products are below an inventory threshold."""
    rows = await pool.execute(
        """
        SELECT ProductID, LocationID, Quantity
        FROM Production.ProductInventory
        WHERE Quantity < %(threshold)s
        """,
        {"threshold": 100},
    )
    logger.info("check_low_inventory: %s rows below threshold", len(rows))

# Usage
async def main():
    logging.basicConfig(level=logging.INFO, format="%(asctime)s %(levelname)s %(message)s")

    pool = AsyncDatabasePool(
        "Server=<server>.database.windows.net;Database=<database>;Authentication=ActiveDirectoryDefault;Encrypt=yes",
        pool_size=3,
    )
    await pool.initialize()

    runner = AsyncTaskRunner(pool)
    runner.register("count_products", count_products, interval=2)
    runner.register("low_inventory", check_low_inventory, interval=3)

    # Run the runner in the background, let it cycle a few times, then stop.
    # In a real app, run the runner for the application's lifetime instead,
    # for example from a FastAPI lifespan handler.
    runner_task = asyncio.create_task(runner.start())
    await asyncio.sleep(7)
    runner.stop()
    await runner_task
    await pool.close()

if __name__ == "__main__":
    asyncio.run(main())
```

Run the worker:

```bash
python worker.py
```

Each registered task logs when it runs, so you see repeating output every few seconds:

```output
2026-07-17 15:07:25 INFO count_products: 504 products
2026-07-17 15:07:25 INFO Task count_products completed
2026-07-17 15:07:26 INFO check_low_inventory: 179 rows below threshold
2026-07-17 15:07:26 INFO Task low_inventory completed
```

## Concurrent query execution

Use `asyncio.gather` with a semaphore to limit the number of queries running simultaneously. The examples in this section build on the [Async connection pool](#async-connection-pool) section, so save that section's code as `db.py` and run each example in its own file next to it.

### Parallel queries with semaphore

Execute multiple queries concurrently while using a semaphore to cap the number of simultaneous operations, preventing thread pool saturation.

```python
import asyncio

from db import AsyncDatabasePool  # The pool class from the Async connection pool section

async def parallel_queries(pool: AsyncDatabasePool, queries: list[tuple[str, dict]],
                          max_concurrent: int = 5) -> list:
    """Execute multiple queries with concurrency limit."""
    semaphore = asyncio.Semaphore(max_concurrent)

    async def run_query(query: str, params: dict):
        async with semaphore:
            return await pool.execute(query, params)

    tasks = [run_query(q, p) for q, p in queries]
    return await asyncio.gather(*tasks)

# Usage
async def main():
    pool = AsyncDatabasePool(
        "Server=<server>.database.windows.net;Database=<database>;Authentication=ActiveDirectoryDefault;Encrypt=yes",
        pool_size=5,
    )
    await pool.initialize()
    try:
        queries = [
            ("SELECT * FROM Production.Product WHERE ProductSubcategoryID = %(cat)s", {"cat": 1}),
            ("SELECT * FROM Production.Product WHERE ProductSubcategoryID = %(cat)s", {"cat": 2}),
            ("SELECT * FROM Sales.SalesOrderHeader WHERE Status = %(status)s", {"status": 5}),
            ("SELECT * FROM Sales.Customer WHERE TerritoryID = %(territory)s", {"territory": 1}),
        ]

        results = await parallel_queries(pool, queries, max_concurrent=3)

        for i, rows in enumerate(results, 1):
            print(f"Query {i}: {len(rows)} rows")
    finally:
        await pool.close()

if __name__ == "__main__":
    asyncio.run(main())
```

Each query runs concurrently, and the output reports how many rows each one returned:

```output
Query 1: 32 rows
Query 2: 43 rows
Query 3: 31465 rows
Query 4: 3520 rows
```

To load large volumes of rows, don't reach for concurrency. Use fewer round-trips instead, as described in [Bulk copy](bulk-copy.md).

## Streaming large results

Yield rows from large result sets using `OFFSET`/`FETCH` pagination to keep memory usage bounded. This example builds on the [Async connection pool](#async-connection-pool) section, so save that section's code as `db.py` and run this example next to it.

### Async generator for large datasets

Implement an async generator function that fetches result pages on-demand, allowing callers to iterate over large datasets without loading everything into memory.

```python
import asyncio

from db import AsyncDatabasePool  # The pool class from the Async connection pool section

async def stream_results(pool: AsyncDatabasePool, query: str,
                        params: dict = None, chunk_size: int = 1000):
    """Stream query results as async generator."""
    offset = 0

    while True:
        paged_query = f"""
            {query}
            ORDER BY (SELECT NULL)
            OFFSET %(offset)s ROWS
            FETCH NEXT %(limit)s ROWS ONLY
        """
        chunk_params = {**(params or {}), "offset": offset, "limit": chunk_size}

        results = await pool.execute(paged_query, chunk_params)

        if not results:
            break

        for row in results:
            yield row

        offset += chunk_size

        # Allow event loop to process other tasks
        await asyncio.sleep(0)

# Usage
async def main():
    pool = AsyncDatabasePool(
        "Server=<server>.database.windows.net;Database=<database>;Authentication=ActiveDirectoryDefault;Encrypt=yes",
        pool_size=5,
    )
    await pool.initialize()
    try:
        processed = 0
        async for order in stream_results(
            pool, "SELECT SalesOrderID FROM Sales.SalesOrderHeader", chunk_size=500
        ):
            processed += 1
        print(f"Streamed {processed} orders in chunks of 500.")
    finally:
        await pool.close()

if __name__ == "__main__":
    asyncio.run(main())
```

The generator fetches one page at a time, so memory stays bounded no matter how large the result set:

```output
Streamed 31465 orders in chunks of 500.
```

## Best practices

Apply these guidelines to keep async patterns safe and efficient.

### Proper executor sizing

Size the thread pool to match I/O-bound workloads, not CPU count alone.

```python
import os

# Rule of thumb: 2-4x CPU cores for I/O-bound database work
cpu_count = os.cpu_count() or 4
pool_size = cpu_count * 2

db_executor = ThreadPoolExecutor(max_workers=pool_size)
```

### Graceful shutdown

Stop the task runner, drain in-flight work, then close the pool and executor in order.

```python
async def graceful_shutdown(pool: AsyncDatabasePool, runner: AsyncTaskRunner):
    """Gracefully shut down all async components."""
    # Stop accepting new tasks
    runner.stop()
    
    # Wait for running tasks to complete
    await asyncio.sleep(2)
    
    # Close database pool
    await pool.close()
    
    # Shutdown executor
    db_executor.shutdown(wait=True)
```

### Error handling

Retry only on transient failures, and back off with a capped delay and jitter. Reuse the `is_transient_error` classifier from [Retry logic and connection resiliency](retry-logic.md#identify-transient-errors) so that permanent failures like bad credentials or syntax errors fail fast instead of retrying.

```python
import asyncio
import random
import mssql_python

# Reuse is_transient_error() from the Retry logic article.

async def resilient_query(pool: AsyncDatabasePool, query: str,
                          params: dict = None, retries: int = 3,
                          base_delay: float = 1.0, max_delay: float = 30.0) -> list:
    """Execute a query, retrying only on transient failures."""
    for attempt in range(retries + 1):
        try:
            return await pool.execute(query, params)
        except mssql_python.Error as e:
            if not is_transient_error(e) or attempt == retries:
                raise
            delay = min(base_delay * (2 ** attempt), max_delay)
            delay *= 0.5 + random.random()  # Add jitter
            await asyncio.sleep(delay)
```

## Related content

- [Connection pooling with mssql-python](connection-pooling.md)
- [Use mssql-python with FastAPI](fastapi-integration.md)
- [Performance tuning for mssql-python applications](performance-tuning.md)
- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
