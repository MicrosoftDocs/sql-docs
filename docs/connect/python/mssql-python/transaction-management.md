---
title: Transaction Management with mssql-python
description: Learn how to manage database transactions, control autocommit, and set isolation levels using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 08/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Transaction management with mssql-python

The mssql-python driver supports full transaction control, including commit, rollback, autocommit configuration, and transaction isolation levels.

## Transaction basics

A transaction groups a sequence of database operations into a single unit of work. Transactions follow the ACID properties:

- **Atomicity**: All operations succeed or all fail.
- **Consistency**: Database remains in a valid state.
- **Isolation**: Concurrent transactions don't interfere with each other.
- **Durability**: Committed changes survive system failures.

## Autocommit mode

The `autocommit` setting controls whether changes are committed automatically.

### Autocommit disabled (default)

By default, `autocommit=False`. You must explicitly commit changes.

```python
import mssql_python

conn = mssql_python.connect(connection_string)
print(conn.autocommit)  # False

cursor = conn.cursor()
cursor.execute("CREATE TABLE #TxnBasic (Name NVARCHAR(50))")
cursor.execute("INSERT INTO #TxnBasic (Name) VALUES ('Widget')")
cursor.execute("INSERT INTO #TxnBasic (Name) VALUES ('Gadget')")

# Changes are staged but not visible to other connections
conn.commit()  # Now changes are permanent

conn.close()
```

If you don't commit, the driver discards changes when the connection closes.

### Autocommit enabled

When you set `autocommit=True`, the driver commits each statement immediately:

```python
conn = mssql_python.connect(connection_string, autocommit=True)
# OR
conn.setautocommit(True)

cursor = conn.cursor()
cursor.execute("CREATE TABLE #AutoDemo (Name NVARCHAR(50))")
cursor.execute("INSERT INTO #AutoDemo (Name) VALUES ('Widget')")
# Immediately committed - no explicit commit needed
```

> [!CAUTION]
> When autocommit is enabled, you can't roll back multiple statements as a group. Use autocommit only when it's appropriate for your use case.

## Commit and rollback

### Commit

Call `commit()` to make pending changes permanent:

```python
cursor.execute("CREATE TABLE #CommitDemo (Name NVARCHAR(50), Price DECIMAL(10,2), CategoryID INT)")
cursor.execute("INSERT INTO #CommitDemo VALUES ('A',10,1),('B',20,1)")
cursor.execute("UPDATE #CommitDemo SET Price = Price * 1.1 WHERE CategoryID = 1")

conn.commit()  # The update is now permanent
```

### Rollback

To discard pending changes, call `rollback()`:

```python
try:
    cursor.execute("CREATE TABLE #RollDemo (Name NVARCHAR(50), Price DECIMAL(10,2), CategoryID INT)")
    cursor.execute("INSERT INTO #RollDemo VALUES ('A',10,1),('B',20,1)")
    cursor.execute("UPDATE #RollDemo SET Price = Price * 1.1 WHERE CategoryID = 1")
    
    # Verify the update
    cursor.execute("SELECT AVG(Price) FROM #RollDemo WHERE CategoryID = 1")
    avg_price = cursor.fetchval()
    
    if avg_price > 100:
        conn.rollback()  # Price too high, undo both updates
        print("Rolled back: average price would exceed limit")
    else:
        conn.commit()
except Exception as e:
    conn.rollback()  # Undo on error
    raise
```

### Cursor-level commit and rollback

For convenience, you can call `commit()` and `rollback()` on cursors:

```python
cursor = conn.cursor()
cursor.execute("CREATE TABLE #CursorDemo (Name NVARCHAR(50))")
cursor.execute("INSERT INTO #CursorDemo (Name) VALUES ('Widget')")
cursor.commit()  # Delegates to connection

cursor.execute("DELETE FROM #CursorDemo WHERE Name = 'Widget'")
cursor.rollback()  # Delegates to connection
```

> [!NOTE]
> Cursor-level commit and rollback affect all cursors on the same connection, not just the cursor on which you call it.

## Context managers

The connection context manager commits the transaction on clean exit and rolls it back if an exception occurs. The connection always closes on exit. When you set `autocommit=True`, the commit and rollback calls have no effect.

```python
with mssql_python.connect(connection_string) as conn:
    cursor = conn.cursor()
    cursor.execute("CREATE TABLE #CtxDemo (Name NVARCHAR(50))")
    cursor.execute("INSERT INTO #CtxDemo (Name) VALUES ('Widget')")
    cursor.execute("INSERT INTO #CtxDemo (Name) VALUES ('Gadget')")
# Transaction is committed and connection is closed on exit
```

If an exception occurs, the transaction is rolled back:

```python
try:
    with mssql_python.connect(connection_string) as conn:
        cursor = conn.cursor()
        cursor.execute("CREATE TABLE #TxnDemo (Name NVARCHAR(50))")
        cursor.execute("INSERT INTO #TxnDemo (Name) VALUES ('Widget')")
        raise ValueError("Something went wrong")
except ValueError:
    pass
# Transaction is rolled back and connection is closed on exit
```

## Transaction isolation levels

Isolation levels control how transactions interact with concurrent transactions. Set the isolation level using `set_attr()`:

```python
import mssql_python

conn = mssql_python.connect(connection_string)

# Set isolation level
conn.set_attr(
    mssql_python.SQL_ATTR_TXN_ISOLATION,
    mssql_python.SQL_TXN_SERIALIZABLE
)
```

### Available isolation levels

| Constant | Description |
| ---------- | ------------- |
| `SQL_TXN_READ_UNCOMMITTED` | Can read uncommitted changes from other transactions (dirty reads allowed) |
| `SQL_TXN_READ_COMMITTED` | Only reads committed data (default in SQL Server) |
| `SQL_TXN_REPEATABLE_READ` | Guarantees consistent reads within transaction |
| `SQL_TXN_SERIALIZABLE` | Highest isolation; transactions appear to run sequentially |

### Choose an isolation level

| Use case | Recommended level |
| ---------- | ------------------- |
| General OLTP workloads | `READ_COMMITTED` (default) |
| Reports that need consistent snapshots | `REPEATABLE_READ` or snapshot |
| Financial calculations requiring accuracy | `SERIALIZABLE` |
| Read-heavy workloads tolerating stale data | `READ_UNCOMMITTED` |

### Snapshot isolation

For snapshot isolation, use Transact-SQL (T-SQL). Snapshot isolation uses [row versioning](../../../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md) in `tempdb`, which can increase storage requirements under heavy write workloads.

```python
# Enable snapshot isolation on the database (one-time setup, requires autocommit)
conn.commit()
conn.autocommit = True
cursor.execute("ALTER DATABASE AdventureWorks2025 SET ALLOW_SNAPSHOT_ISOLATION ON")

# Set isolation level while still in autocommit, then start the transaction
cursor.execute("SET TRANSACTION ISOLATION LEVEL SNAPSHOT")
conn.autocommit = False

cursor.execute("SELECT TOP 5 Name, ListPrice FROM Production.Product")
rows = cursor.fetchall()
for row in rows:
    print(row.Name, row.ListPrice)
conn.commit()
```

## Nested transactions and savepoints

SQL Server supports savepoints for partial rollback within a transaction.

```python
cursor = conn.cursor()

cursor.execute("BEGIN TRANSACTION")
cursor.execute("CREATE TABLE #SaveDemo (Name NVARCHAR(50))")
cursor.execute("INSERT INTO #SaveDemo (Name) VALUES ('Widget')")

cursor.execute("SAVE TRANSACTION SaveDemoPoint")
cursor.execute("INSERT INTO #SaveDemo (Name) VALUES ('Gadget')")

# Roll back to savepoint, keeping first insert
cursor.execute("ROLLBACK TRANSACTION SaveDemoPoint")

cursor.execute("COMMIT TRANSACTION")
```

## Handle deadlocks

Deadlocks occur when two transactions wait for each other's locks. SQL Server automatically detects deadlocks and terminates one transaction.

```python
import time

def execute_with_retry(conn, cursor, sql, params=None, max_retries=3):
    """Execute SQL with deadlock retry logic."""
    for attempt in range(max_retries):
        try:
            cursor.execute(sql, params)
            return
        except mssql_python.OperationalError as e:
            if "1205" in str(e):  # Deadlock error number
                if attempt < max_retries - 1:
                    conn.rollback()  # Clear the failed transaction
                    time.sleep(0.1 * (2 ** attempt))  # Exponential backoff
                    continue
            raise
    raise Exception(f"Failed after {max_retries} attempts")
```

## Best practices

- **Keep transactions short** to minimize lock duration and deadlock potential.

- **Use autocommit=False** for multi-statement transactions that should be atomic.

- **Always handle exceptions** with rollback:

   ```python
   conn = None
   try:
       conn = mssql_python.connect(connection_string)
       cursor = conn.cursor()
       cursor.execute("CREATE TABLE #RollbackPattern (ID INT, Name NVARCHAR(50))")
       cursor.execute("INSERT INTO #RollbackPattern (ID, Name) VALUES (1, 'Widget')")
       cursor.execute("UPDATE #RollbackPattern SET Name = 'Updated Widget' WHERE ID = 1")
       conn.commit()
   except Exception:
       if conn is not None:
           conn.rollback()
       raise
   finally:
       if conn is not None:
           conn.close()
   ```

- **Use context managers** to manage transactions automatically. The context manager commits on clean exit and rolls back on exception.

   ```python
   with mssql_python.connect(connection_string) as conn:
       cursor = conn.cursor()
       cursor.execute("CREATE TABLE #ContextManagerDemo (ID INT, Name NVARCHAR(50))")
       cursor.execute("INSERT INTO #ContextManagerDemo (ID, Name) VALUES (1, 'Widget')")
       cursor.execute("UPDATE #ContextManagerDemo SET Name = 'Committed Widget' WHERE ID = 1")
   # Committed automatically on exit
   ```

- **Choose appropriate isolation levels** based on your consistency requirements vs. performance needs.

- **Use lock hints for read-modify-write patterns** to prevent lost updates. When you read a value that will be updated within the same transaction, use hints like `WITH (UPDLOCK, ROWLOCK)` on the SELECT to acquire locks early and establish consistent lock ordering, which reduces deadlock risk.

   ```python
   # Good: Acquire lock during read to prevent lost update pattern
   cursor.execute("""
       SELECT Balance FROM Accounts 
       WITH (UPDLOCK, ROWLOCK) 
       WHERE ID = %(id)s
   """, {"id": account_id})
   balance = cursor.fetchval()
   
   if balance >= amount:
       cursor.execute("""
           UPDATE Accounts SET Balance = Balance - %(amount)s 
           WHERE ID = %(id)s
       """, {"amount": amount, "id": account_id})
   ```

- **Implement retry logic** for transient failures like deadlocks.

## Example: Transfer funds (atomic operation)

This example demonstrates atomic transfer logic with lock hints to prevent lost updates in concurrent scenarios:

```python
def transfer_funds(conn, from_account, to_account, amount):
    """Transfer funds atomically between accounts."""
    cursor = conn.cursor()
    
    try:
        # Read balance with lock hint to prevent lost updates
        cursor.execute(
            "SELECT Balance FROM Accounts WITH (UPDLOCK, ROWLOCK) WHERE AccountID = %(account_id)s",
            {"account_id": from_account}
        )
        balance = cursor.fetchval()
        
        if balance is None:
            raise ValueError(f"Source account {from_account} not found")
        
        if balance < amount:
            raise ValueError("Insufficient funds")
        
        # Debit source account
        cursor.execute(
            "UPDATE Accounts SET Balance = Balance - %(amount)s WHERE AccountID = %(account_id)s",
            {"amount": amount, "account_id": from_account}
        )
        
        # Credit destination account
        cursor.execute(
            "UPDATE Accounts SET Balance = Balance + %(amount)s WHERE AccountID = %(account_id)s",
            {"amount": amount, "account_id": to_account}
        )
        
        if cursor.rowcount != 1:
            raise ValueError(f"Destination account {to_account} not found")
        
        conn.commit()
        print(f"Transferred ${amount} from {from_account} to {to_account}")
        
    except Exception:
        conn.rollback()
        raise
```

The lock hints `WITH (UPDLOCK, ROWLOCK)` on the SELECT ensure that the lock is acquired early. This prevents another transaction from reading the same balance concurrently and creating a lost update scenario where both transactions read the old balance, perform separate updates, and only the last update persists.

## Temp table scoping

Temp tables (`#tablename`) are scoped to the session, but their creation is part of the current transaction. If you create a temp table and the transaction rolls back, the temp table is dropped:

```python
conn = mssql_python.connect(connection_string)  # autocommit=False
cursor = conn.cursor()

cursor.execute("CREATE TABLE #Staging (ID INT, Name NVARCHAR(50))")
cursor.execute("INSERT INTO #Staging VALUES (1, 'test')")

# Rollback removes the temp table entirely
conn.rollback()

# This fails: Invalid object name '#Staging'
try:
    cursor.execute("SELECT * FROM #Staging")
except mssql_python.ProgrammingError:
    print("Temp table was dropped by rollback")
```

To keep a temp table independent of your data transaction, commit after creating it:

```python
cursor.execute("CREATE TABLE #Staging (ID INT, Name NVARCHAR(50))")
conn.commit()  # Temp table persists regardless of later rollbacks

# Now data operations can roll back without losing the table
cursor.execute("INSERT INTO #Staging VALUES (1, 'test')")
conn.rollback()  # Data is gone, but #Staging still exists
```

## DDL statements that require autocommit

Some DDL statements, such as `CREATE DATABASE`, `ALTER DATABASE`, and `DROP DATABASE`, can't run inside a transaction. Set `autocommit=True` before executing them:

```python
conn.autocommit = True
cursor.execute("CREATE DATABASE TestDB")
conn.autocommit = False  # Return to transactional mode
```

If you run `CREATE DATABASE` with `autocommit=False`, you get an error: `CREATE DATABASE statement not allowed within multi-statement transaction.`

## Related content

- [Manage connections with mssql-python](connection-management.md)
- [Execute queries with mssql-python](executing-queries.md)
- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
- [Manage cursors and result sets](cursor-management.md)
