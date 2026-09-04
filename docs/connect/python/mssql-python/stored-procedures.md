---
title: Call Stored Procedures with mssql-python
description: Learn how to execute stored procedures and work with output parameters using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Call stored procedures with mssql-python

The mssql-python driver doesn't implement the `callproc()` method from the DB-API 2.0 specification. Calling `callproc()` raises `NotSupportedError`. Instead, use the ODBC `{CALL ...}` escape sequence with standard query execution methods.

## Basic stored procedure execution

### Without parameters

Execute a stored procedure by using the `{CALL}` escape sequence. This example calls the `sp_databases` system stored procedure, which takes no parameters and returns one row per database:

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("{CALL sp_databases}")

for row in cursor:
    print(row.DATABASE_NAME, row.DATABASE_SIZE)
```

### With input parameters

Pass parameters using positional or named placeholders:

```python
# Single parameter
cursor.execute(
    "{CALL dbo.uspGetManagerEmployees(?)}", (16,)
)

for row in cursor:
    print(row.FirstName, row.LastName)
```

## Output parameters

### Declare and retrieve output parameters

SQL Server stored procedures can return values through output parameters. Use Transact-SQL (T-SQL) variables to capture output values, then retrieve them with a `SELECT` statement:

```python
cursor.execute("""
    DECLARE @total_out MONEY;
    SELECT @total_out = SUM(TotalDue)
    FROM Sales.SalesOrderHeader
    WHERE CustomerID = %(customer_id)s;
    SELECT @total_out AS TotalAmount;
""", {"customer_id": 29825})

row = cursor.fetchone()
total = row.TotalAmount
print(f"Customer total: ${total}")
```

### Multiple output parameters

Capture multiple output values by declaring separate variables. The same pattern works with any stored procedure that has `OUTPUT` parameters:

```python
cursor.execute("""
    DECLARE @total_orders INT, @total_spent MONEY;
    SELECT @total_orders = COUNT(*), @total_spent = SUM(TotalDue)
    FROM Sales.SalesOrderHeader
    WHERE CustomerID = %(cust_id)s;
    SELECT @total_orders AS OrderCount, @total_spent AS TotalSpent;
""", {"cust_id": 29825})

stats = cursor.fetchone()
print(f"Orders: {stats.OrderCount}, Total spent: ${stats.TotalSpent}")
```

## Return values

### Capture stored procedure return value

Execute a stored procedure that returns results:

```python
cursor.execute("EXECUTE dbo.uspGetEmployeeManagers @BusinessEntityID = %(emp_id)s", {"emp_id": 5})

rows = cursor.fetchall()
if rows:
    print(f"Found {len(rows)} managers in chain")
    for row in rows:
        print(f"  Manager: {row.FirstName} {row.LastName}")
else:
    print("No managers found")
```

### Return value with output parameters

```python
cursor.execute("""
    DECLARE @return_value INT, @message NVARCHAR(500);
    SELECT @return_value = CASE WHEN COUNT(*) > 0 THEN 0 ELSE 1 END,
           @message = CASE WHEN COUNT(*) > 0 THEN N'Customer found' ELSE N'Customer not found' END
    FROM Sales.Customer WHERE CustomerID = %(cust_id)s;
    SELECT @return_value AS ReturnCode, @message AS Message;
""", {"cust_id": 29825})

result = cursor.fetchone()
print(f"Return code: {result.ReturnCode}, Message: {result.Message}")
```

## Result sets

### Single result set

Execute a stored procedure that returns a single result set:

```python
cursor.execute(
    "{CALL dbo.uspGetBillOfMaterials(?, ?)}",
    (800, "2026-01-01")
)

customers = cursor.fetchall()
for row in customers:
    print(f"{row.ProductAssemblyID}: {row.ComponentDesc}")
```

### Multiple result sets

Some stored procedures return multiple result sets. Use `nextset()` to navigate between them:

```python
cursor.execute("""
    SELECT TOP 1 SalesOrderID, OrderDate, TotalDue
    FROM Sales.SalesOrderHeader WHERE CustomerID = 29825;
    SELECT TOP 3 Name, ListPrice
    FROM Production.Product WHERE ListPrice > 0
    ORDER BY ListPrice DESC;
""")

# First result set: order header
order = cursor.fetchone()
print(f"Order: {order.SalesOrderID}, Date: {order.OrderDate}")

cursor.nextset()

# Second result set: products
print("Products:")
for item in cursor:
    print(f"  {item.Name}: ${item.ListPrice}")
```

### Check for more result sets

Iterate through all result sets returned by a stored procedure using `nextset()`:

```python
cursor.execute("SELECT TOP 3 ProductID, Name FROM Production.Product; SELECT TOP 3 FirstName, LastName FROM Person.Person")

result_set_num = 1
while True:
    print(f"--- Result Set {result_set_num} ---")
    for row in cursor:
        print(row)
    
    if not cursor.nextset():
        break
    result_set_num += 1
```

## Transactions with stored procedures

### Explicit transaction control

Wrap multiple stored procedure calls in a transaction to ensure atomicity:

```python
conn.autocommit = False

try:
    cursor.execute("{CALL dbo.DebitAccount(?, ?)}", (1001, 100.00))
    
    cursor.execute("{CALL dbo.CreditAccount(?, ?)}", (1002, 100.00))
    
    conn.commit()
    print("Transfer completed")
except mssql_python.DatabaseError as e:
    conn.rollback()
    print(f"Transfer failed: {e}")
```

### Let stored procedure manage transaction

If the stored procedure handles its own transactions:

```python
conn.autocommit = True  # Let SP manage transactions

cursor.execute("""
    DECLARE @result INT;
    EXECUTE @result = dbo.TransferFunds 
        @FromAccount = %(from_acc)s,
        @ToAccount = %(to_acc)s,
        @Amount = %(amount)s;
    SELECT @result AS TransferResult;
""", {"from_acc": 1001, "to_acc": 1002, "amount": 100.00})

result = cursor.fetchone()
if result.TransferResult == 0:
    print("Transfer successful")
```

## Error handling

### Catch stored procedure errors

Handle exceptions raised by stored procedures or Transact-SQL statements:

```python
try:
    cursor.execute("{CALL dbo.DangerousProcedure}")
except mssql_python.ProgrammingError as e:
    # Handle SQL errors raised by RAISERROR or THROW
    print(f"Stored procedure error: {e}")
except mssql_python.DatabaseError as e:
    # Handle other database errors
    print(f"Database error: {e}")
```

### Capture PRINT statements and informational messages

SQL Server `PRINT` statements and `RAISERROR` with severity below 11 are captured in `cursor.messages` after execution. Each entry is a `(message_type, message_text)` tuple. When a `PRINT` runs before a result set, it occupies a rowless result set of its own, so read `cursor.messages` first, then call `nextset()` to reach the rows:

```python
cursor.execute("PRINT 'Operation complete'; SELECT 1 AS Status")

for msg_type, msg_text in cursor.messages:
    print(f"Server message: {msg_text}")

cursor.nextset()
row = cursor.fetchone()
print(f"Status: {row.Status}")
```

When a stored procedure emits `PRINT` messages across multiple result sets, read `cursor.messages` after `execute()` and again after each `nextset()` call so messages from every result set are captured:

```python
cursor.execute("""
    PRINT 'Starting first result set';
    SELECT TOP 3 ProductID, Name FROM Production.Product;
    PRINT 'Starting second result set';
    SELECT TOP 3 FirstName, LastName FROM Person.Person;
""")

all_messages = []
while True:
    all_messages.extend(cursor.messages)
    if cursor.description:
        for row in cursor:
            print(row)
    if not cursor.nextset():
        break

for _, text in all_messages:
    print(f"Server: {text}")
```

For the full `cursor.messages` API, see [Cursor management](cursor-management.md#diagnostic-messages).

## Best practices

### Use named parameters

Named parameters are clearer and maintain order independence:

```python
# Recommended: {CALL} with positional parameters
cursor.execute(
    "{CALL dbo.uspGetBillOfMaterials(?, ?)}",
    (800, "2026-01-01")
)

# Also valid: EXECUTE with named T-SQL parameters
cursor.execute("""
    EXECUTE dbo.uspGetBillOfMaterials
        @StartProductID = ?,
        @CheckDate = ?
""", (800, "2026-01-01"))
```

### Handle nullable output parameters

Check for NULL values when retrieving output parameters from stored procedures:

```python
cursor.execute("""
    DECLARE @optional_value NVARCHAR(100);
    SELECT @optional_value = Color FROM Production.Product WHERE ProductID = %(id)s;
    SELECT @optional_value AS OutputValue;
""", {"id": 1})

result = cursor.fetchone()
if result.OutputValue is not None:
    print(f"Value: {result.OutputValue}")
else:
    print("No value returned")
```

### Use SET NOCOUNT ON in stored procedures

For better performance and cleaner result handling, ensure your stored procedures include:

```sql
CREATE PROCEDURE dbo.MyProcedure
AS
BEGIN
    SET NOCOUNT ON;  -- Prevents "n rows affected" messages
    -- procedure logic
END
```

## Example: Complete workflow

Here's a practical example that calls a stored procedure, retrieves output values, and handles errors:

```python
import mssql_python

def get_employee_report(manager_id: int) -> dict:
    """Look up a manager's employees and compute average vacation hours."""
    conn = mssql_python.connect(connection_string)
    conn.autocommit = False
    cursor = conn.cursor()

    try:
        # Get manager info
        cursor.execute("""
            SELECT BusinessEntityID, JobTitle
            FROM HumanResources.Employee
            WHERE BusinessEntityID = %(mgr)s
        """, {"mgr": manager_id})
        mgr = cursor.fetchone()
        print(f"Manager {mgr.BusinessEntityID}: {mgr.JobTitle}")

        # Get direct reports via stored procedure
        cursor.execute("{CALL dbo.uspGetManagerEmployees(?)}", (manager_id,))
        employees = cursor.fetchall()
        print(f"Found {len(employees)} employee(s)")

        # Compute average vacation hours
        cursor.execute("""
            DECLARE @avg_hours INT;
            SELECT @avg_hours = AVG(VacationHours)
            FROM HumanResources.Employee;
            SELECT @avg_hours AS AvgVacation;
        """)
        avg = cursor.fetchone().AvgVacation
        print(f"Avg vacation hours: {avg}")

        conn.commit()
        return {"manager": mgr.JobTitle, "reports": len(employees), "avg_vacation": avg}

    except mssql_python.DatabaseError as e:
        conn.rollback()
        raise
    finally:
        cursor.close()
        conn.close()

# Usage
result = get_employee_report(manager_id=16)
```

## Get generated keys by using OUTPUT INSERTED

To retrieve an identity value from an INSERT (with or without a stored procedure), use `OUTPUT INSERTED` instead of `SCOPE_IDENTITY()`. This approach returns the value in the same result set:

```python
cursor.execute("""
    INSERT INTO Production.ProductCategory (Name)
    OUTPUT INSERTED.ProductCategoryID
    VALUES (%(name)s)
""", {"name": "Custom Parts"})

new_id = cursor.fetchval()
print(f"New category ID: {new_id}")
```

This pattern works for any table with an identity column and doesn't require a stored procedure.

## Unsupported features

### callproc()

The `mssql-python` driver raises `NotSupportedError` if you call `cursor.callproc()`. Use `cursor.execute("{CALL ...}")` or `cursor.execute("EXECUTE ...")` instead, as shown throughout this article.

### Table-valued parameters (TVPs)

Table-valued parameters aren't supported in the current version (1.14.0) of `mssql-python`. If you need to pass a set of rows to a stored procedure, use these alternatives:

- Insert into a temp table first, then have the stored procedure read from it.
- Use `bulkcopy()` to load data into a staging table.
- Pass a JSON string and parse it with `OPENJSON` inside the procedure.

## Related content

- [Execute queries with mssql-python](executing-queries.md)
- [Retrieve data with mssql-python](retrieving-data.md)
- [Transaction management with mssql-python](transaction-management.md)
- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
