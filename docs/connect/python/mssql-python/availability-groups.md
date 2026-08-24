---
title: Use Read-Only Routing and Availability Groups with mssql-python
description: Learn how to connect to SQL Server Always On availability groups and configure read-only routing with the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use read-only routing and availability groups

Always On availability groups provide high availability for SQL Server databases. Before connecting, your database administrator must [create an availability group listener](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md) and [configure read-only routing](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md) on the server. The mssql-python driver supports availability group connections through these connection string keywords:

- `ApplicationIntent` - Route connections to read-only secondary replicas
- `MultiSubnetFailover` - Enable parallel connection attempts across subnets for faster failover

## Connect to availability group listener

### Basic listener connection

Connect to the availability group through the listener DNS name instead of a specific server:

```python
import mssql_python

# Connect via availability group listener
conn = mssql_python.connect(
    "Server=ag-listener.contoso.com;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
)

cursor = conn.cursor()
cursor.execute("SELECT @@SERVERNAME AS ServerName")
print(f"Connected to: {cursor.fetchval()}")
```

### With port specification

Specify a port number when the listener uses a non-default port:

```python
conn = mssql_python.connect(
    "Server=ag-listener.contoso.com,1433;"  # Listener with port
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
)
```

## Read-only routing

### Enable read-only intent

Use `ApplicationIntent=ReadOnly` to route to secondary replicas:

```python
# Connect for read operations - routes to secondary
read_conn = mssql_python.connect(
    "Server=ag-listener.contoso.com;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "ApplicationIntent=ReadOnly;"
    "Encrypt=yes;"
)

# Connect for read-write operations - routes to primary
write_conn = mssql_python.connect(
    "Server=ag-listener.contoso.com;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "ApplicationIntent=ReadWrite;"  # Default
    "Encrypt=yes;"
)
```

### Verify routing

Confirm which replica handled the connection and whether it's primary or secondary:

```python
def check_replica_role(conn) -> str:
    """Check if connected to primary or secondary."""
    cursor = conn.cursor()
    cursor.execute("""
        SELECT 
            @@SERVERNAME AS ServerName,
            CASE 
                WHEN DATABASEPROPERTYEX(DB_NAME(), 'Updateability') = 'READ_WRITE' 
                THEN 'Primary'
                ELSE 'Secondary'
            END AS Role
    """)
    row = cursor.fetchone()
    return f"{row.ServerName} ({row.Role})"

read_conn = mssql_python.connect(connection_string + "ApplicationIntent=ReadOnly;")
print(f"Read connection: {check_replica_role(read_conn)}")

write_conn = mssql_python.connect(connection_string + "ApplicationIntent=ReadWrite;")
print(f"Write connection: {check_replica_role(write_conn)}")
```

## Multi-subnet failover

### Enable multi-subnet failover

For availability groups spanning multiple subnets:

```python
conn = mssql_python.connect(
    "Server=ag-listener.contoso.com;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "MultiSubnetFailover=yes;"
    "Encrypt=yes;"
)
```

This setting:

- Attempts connections to all IP addresses in parallel.
- Reduces failover time in multi-subnet configurations.
- Works best for all availability group connections.

## Connection architecture patterns

### Separate read and write connections

Maintain separate connections so read traffic goes to secondaries and writes go to the primary:

```python
class DatabaseConnections:
    """Manage separate connections for read and write operations."""
    
    def __init__(self, listener: str, database: str):
        self.base_conn_str = f"Server={listener};Database={database};Authentication=ActiveDirectoryDefault;Encrypt=yes;"
        self._read_conn = None
        self._write_conn = None
    
    @property
    def read_connection(self):
        """Get or create read-only connection (secondary replica)."""
        if self._read_conn is None:
            self._read_conn = mssql_python.connect(
                self.base_conn_str + "ApplicationIntent=ReadOnly;MultiSubnetFailover=yes;"
            )
        return self._read_conn
    
    @property
    def write_connection(self):
        """Get or create read-write connection (primary replica)."""
        if self._write_conn is None:
            self._write_conn = mssql_python.connect(
                self.base_conn_str + "ApplicationIntent=ReadWrite;MultiSubnetFailover=yes;"
            )
        return self._write_conn
    
    def close(self):
        if self._read_conn:
            self._read_conn.close()
        if self._write_conn:
            self._write_conn.close()

# Usage
db = DatabaseConnections("ag-listener.contoso.com", "<database>")

# Queries go to secondary
cursor = db.read_connection.cursor()
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product")
products = cursor.fetchall()

# Writes go to primary
cursor = db.write_connection.cursor()
cursor.execute("INSERT INTO #Products (Name) VALUES (%(name)s)", {"name": "New Product"})
db.write_connection.commit()

db.close()
```

### Read-after-write pattern

Write to the primary and then verify the data on a secondary, accounting for replication lag:

```python
def create_order_and_verify(conn_manager, order_data: dict):
    """Create order on primary, verify on secondary with eventual consistency."""
    
    # Write to primary
    write_cursor = conn_manager.write_connection.cursor()
    write_cursor.execute("""
        INSERT INTO #Orders (CustomerID, Total) VALUES (%(cust)s, %(total)s);
        SELECT SCOPE_IDENTITY();
    """, order_data)
    order_id = write_cursor.fetchval()
    conn_manager.write_connection.commit()
    
    # Wait for replication (in production, use more sophisticated approach)
    import time
    time.sleep(1)
    
    # Verify on secondary
    read_cursor = conn_manager.read_connection.cursor()
    read_cursor.execute("SELECT * FROM #Orders WHERE OrderID = %(id)s", {"id": order_id})
    
    if read_cursor.fetchone():
        print(f"Order {order_id} replicated to secondary")
    else:
        print(f"Order {order_id} not yet replicated")
    
    return order_id
```

## Handle failover

### Connection resiliency

Retry queries automatically when a failover interrupts the connection:

```python
import time

def execute_with_failover_retry(conn_str: str, query: str, params: dict,
                                max_retries: int = 3) -> list:
    """Execute query with automatic reconnection on failover."""
    
    for attempt in range(max_retries + 1):
        try:
            conn = mssql_python.connect(conn_str)
            cursor = conn.cursor()
            cursor.execute(query, params)
            results = cursor.fetchall()
            conn.close()
            return results
        except mssql_python.OperationalError as e:
            error_str = str(e)
            # Check for failover-related errors
            # Azure SQL transient error codes
            # See: /azure/azure-sql/database/troubleshoot-common-errors-issues
            if any(code in error_str for code in ["40613", "40197", "40501"]):
                if attempt < max_retries:
                    print(f"Failover detected, retrying ({attempt + 1}/{max_retries})...")
                    time.sleep(5 * (attempt + 1))  # Exponential backoff
                    continue
            raise

# Usage
results = execute_with_failover_retry(
    "Server=<ag-listener>.contoso.com;Database=<database>;Authentication=ActiveDirectoryDefault;MultiSubnetFailover=yes;Encrypt=yes;",
    "SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductSubcategoryID = %(cat)s",
    {"cat": 5}
)
```

The retry loop reacts only to transient error codes. Non-transient failures, such as authentication errors, permission errors, or query syntax errors, are raised immediately because retrying can't resolve them.

### Detect primary role change

Check the current replica role and reconnect if the primary has moved:

```python
def is_primary(conn) -> bool:
    """Check if current connection is to primary replica."""
    cursor = conn.cursor()
    cursor.execute("""
        SELECT DATABASEPROPERTYEX(DB_NAME(), 'Updateability') AS Updateability
    """)
    return cursor.fetchval() == 'READ_WRITE'

def ensure_primary(conn_str: str) -> mssql_python.Connection:
    """Ensure connection is to primary, reconnect if needed."""
    conn = mssql_python.connect(conn_str + "ApplicationIntent=ReadWrite;")
    
    if not is_primary(conn):
        # Might happen during failover
        conn.close()
        time.sleep(2)
        conn = mssql_python.connect(conn_str + "ApplicationIntent=ReadWrite;")
    
    return conn
```

## Reporting workloads

### Offload reports to secondary

Run reporting queries against a secondary replica to reduce load on the primary:

```python
class ReportingService:
    """Service that runs reports on secondary replicas."""
    
    def __init__(self, listener: str, database: str):
        self.conn_str = (
            f"Server={listener};"
            f"Database={database};"
            "Authentication=ActiveDirectoryDefault;"
            "ApplicationIntent=ReadOnly;"
            "MultiSubnetFailover=yes;"
            "Encrypt=yes;"
        )
    
    def run_report(self, report_query: str, params: dict = None) -> list:
        """Execute report query on secondary replica."""
        conn = mssql_python.connect(self.conn_str)
        cursor = conn.cursor()
        
        try:
            cursor.execute(report_query, params or {})
            return cursor.fetchall()
        finally:
            cursor.close()
            conn.close()
    
    def get_sales_summary(self, start_date, end_date) -> dict:
        """Run sales summary report."""
        results = self.run_report("""
            SELECT 
                YEAR(OrderDate) AS Year,
                MONTH(OrderDate) AS Month,
                COUNT(*) AS OrderCount,
                SUM(TotalDue) AS TotalSales
            FROM Sales.SalesOrderHeader
            WHERE OrderDate BETWEEN %(start)s AND %(end)s
            GROUP BY YEAR(OrderDate), MONTH(OrderDate)
            ORDER BY Year, Month
        """, {"start": start_date, "end": end_date})
        
        return [
            {"year": r.Year, "month": r.Month, 
             "orders": r.OrderCount, "sales": r.TotalSales}
            for r in results
        ]

# Usage
reports = ReportingService("ag-listener.contoso.com", "sales_db")
summary = reports.get_sales_summary(date(2024, 1, 1), date(2024, 12, 31))
```

## Best practices

### Always use MultiSubnetFailover

Include `MultiSubnetFailover=yes` in every availability group connection string for faster failover:

```python
# Recommended for all AG connections
conn = mssql_python.connect(
    "Server=ag-listener.contoso.com;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "MultiSubnetFailover=yes;"  # Always include this
    "Encrypt=yes;"
)
```

### Match intent to operation

Route each operation to the correct replica based on whether it reads or writes data:

```python
def get_appropriate_connection(operation: str, connections: DatabaseConnections):
    """Get connection appropriate for the operation type."""
    read_operations = {"SELECT", "REPORT", "EXPORT", "ANALYTICS"}
    
    if operation.upper() in read_operations:
        return connections.read_connection
    else:
        return connections.write_connection
```

### Handle secondary temporarily unavailable

Retry the secondary on transient errors, then fall back to the primary replica if the secondary stays unreachable.

```python
import time

def query_with_fallback(conn_manager, query: str, params: dict, max_retries: int = 2):
    """Query the secondary, retrying transient errors before falling back to the primary."""
    transient_codes = ["40613", "40197", "40501"]
    for attempt in range(max_retries + 1):
        try:
            cursor = conn_manager.read_connection.cursor()
            cursor.execute(query, params)
            return cursor.fetchall()
        except mssql_python.OperationalError as e:
            # Re-raise failures that retrying can't fix, such as authentication,
            # permission, or query syntax errors.
            if not any(code in str(e) for code in transient_codes):
                raise
            # Retry the secondary for transient errors before giving up.
            if attempt < max_retries:
                time.sleep(2 * (attempt + 1))
                continue
            # Secondary still unavailable after retries: fall back to the primary.
            print("Secondary unavailable, using primary")
            cursor = conn_manager.write_connection.cursor()
            cursor.execute(query, params)
            return cursor.fetchall()
```

## Related content

- [Connection strings for mssql-python](connection-strings.md)
- [Retry logic and connection resiliency with mssql-python](retry-logic.md)
- [Connection pooling with mssql-python](connection-pooling.md)
- [What is an Always On availability group?](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
