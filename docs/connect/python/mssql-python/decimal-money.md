---
title: Handle Decimal and Money Values in mssql-python
description: Learn how to work with decimal, numeric, and money data types for accurate financial calculations using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Handle decimal and money values

The mssql-python driver maps SQL Server decimal, numeric, and money data types to Python's `decimal.Decimal` for accurate financial and scientific calculations. SQL Server provides the following precise numeric types:

| SQL Type | Python Type | Precision | Use Case |
| ---------- | ------------- | ----------- | ---------- |
| **decimal(p,s)** | `decimal.Decimal` | 1-38 digits | Exact calculations |
| **numeric(p,s)** | `decimal.Decimal` | 1-38 digits | Same as decimal |
| **money** | `decimal.Decimal` | 19 digits, 4 decimal | Currency |
| **smallmoney** | `decimal.Decimal` | 10 digits, 4 decimal | Small currency |
| **float** | `float` | ~15 digits | Approximate |
| **real** | `float` | ~7 digits | Approximate |

## Python Decimal type

### Receive decimal values

Decimal columns return Python `decimal.Decimal` objects:

```python
from decimal import Decimal
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute(
    "SELECT SubTotal, TaxAmt, TotalDue "
    "FROM Sales.SalesOrderHeader WHERE SalesOrderID = 43659"
)
row = cursor.fetchone()

print(type(row.SubTotal))  # <class 'decimal.Decimal'>
print(row.SubTotal)        # Decimal('20565.6206')
print(row.TaxAmt)          # Decimal('1971.5149')
print(row.TotalDue)        # Decimal('23153.2339')
```

### Send decimal values

Pass `Decimal` objects for precise insertion:

```python
from decimal import Decimal

cursor.execute("""
    CREATE TABLE #ProductDemo (
        Name NVARCHAR(50),
        Price DECIMAL(10,2),
        Cost DECIMAL(10,2)
    )
""")
cursor.execute("""
    INSERT INTO #ProductDemo (Name, Price, Cost)
    VALUES (%(name)s, %(price)s, %(cost)s)
""", {
    "name": "Widget",
    "price": Decimal("199.99"),
    "cost": Decimal("87.50")
})
conn.commit()
```

### Avoid float for financial data

Float types have precision issues that make them unsuitable for financial calculations. Always use `Decimal` for accurate results:

```python
from decimal import Decimal

# Bad: float has precision issues
price = 0.1 + 0.2  # 0.30000000000000004

# Good: Decimal is exact
price = Decimal("0.1") + Decimal("0.2")  # Decimal('0.3')

# Use string constructor for literal values
correct = Decimal("19.99")  # Exact
avoid = Decimal(19.99)      # Might introduce float imprecision
```

## Money type

### Work with money columns

Retrieve and update SQL Server money columns using Python Decimal objects:

```python
from decimal import Decimal

# Create and populate a temp table with a money column
cursor.execute("""
    CREATE TABLE #Accounts (
        ID INT,
        AccountID NVARCHAR(20),
        Balance MONEY
    )
""")
cursor.execute(
    "INSERT INTO #Accounts (ID, AccountID, Balance) VALUES (1, %(acct)s, %(bal)s)",
    {"acct": "ACCT-001", "bal": Decimal("1234.5678")}
)
conn.commit()

cursor.execute("SELECT AccountID, Balance FROM #Accounts WHERE ID = 1")
row = cursor.fetchone()

print(row.Balance)         # Decimal('1234.5678')
print(type(row.Balance))   # <class 'decimal.Decimal'>

# Money arithmetic
cursor.execute("""
    UPDATE #Accounts 
    SET Balance = Balance + %(amount)s 
    WHERE ID = %(id)s
""", {"amount": Decimal("100.00"), "id": 1})
conn.commit()
```

### Money formatting

Format decimal values as currency strings with symbols and comma separators:

```python
from decimal import Decimal

def format_currency(value: Decimal, symbol: str = "$") -> str:
    """Format decimal as currency string."""
    return f"{symbol}{value:,.2f}"

cursor.execute("SELECT TotalDue FROM Sales.SalesOrderHeader")
for row in cursor:
    print(format_currency(row.TotalDue))  # $1,234.56
```

## Decimal precision and scale

### Understand precision and scale

- **Precision (p)**: Total number of digits
- **Scale (s)**: Number of digits after decimal point

For example, `decimal(10, 2)` can store values from -99999999.99 to 99999999.99, while `decimal(5, 4)` can store values from -9.9999 to 9.9999.

### Specify precision in parameters

The mssql-python driver infers precision and scale from Decimal values automatically:

```python
from decimal import Decimal

# Create a temp table for the measurement value
cursor.execute("CREATE TABLE #Measurements (Value DECIMAL(18,6))")

# The driver infers precision from the Decimal value
value = Decimal("123.456789")
cursor.execute("INSERT INTO #Measurements (Value) VALUES (%(v)s)", {"v": value})
conn.commit()
```

### Control rounding

Use the `quantize()` method to round Decimal values to a specific scale:

```python
from decimal import Decimal, ROUND_HALF_UP, ROUND_DOWN

price = Decimal("19.999")

# Round to 2 decimal places
rounded = price.quantize(Decimal("0.01"), rounding=ROUND_HALF_UP)
print(rounded)  # Decimal('20.00')

# Round down (truncate)
truncated = price.quantize(Decimal("0.01"), rounding=ROUND_DOWN)
print(truncated)  # Decimal('19.99')
```

## Financial calculations

### Safe arithmetic

Implement financial calculations using Decimal with proper rounding:

```python
from decimal import Decimal, ROUND_HALF_UP

def calculate_total(price: Decimal, quantity: int, tax_rate: Decimal) -> Decimal:
    """Calculate order total with tax."""
    subtotal = price * quantity
    tax = (subtotal * tax_rate).quantize(Decimal("0.01"), rounding=ROUND_HALF_UP)
    return subtotal + tax

cursor.execute("SELECT ListPrice FROM Production.Product WHERE ProductID = 1")
price = cursor.fetchval()

total = calculate_total(
    price=price,
    quantity=3,
    tax_rate=Decimal("0.0825")  # 8.25% tax
)

cursor.execute("""
    CREATE TABLE #OrderCalc (
        ProductID INT, Quantity INT, Total DECIMAL(10,2), Tax DECIMAL(10,2)
    )
""")
cursor.execute("""
    INSERT INTO #OrderCalc (ProductID, Quantity, Total, Tax)
    VALUES (%(prod)s, %(qty)s, %(total)s, %(tax)s)
""", {"prod": 1, "qty": 3, "total": total, "tax": total * Decimal("0.0825")})
```

### Percentage calculations

Calculate percentage-based discounts and adjustments:

```python
from decimal import Decimal

def calculate_discount(price: Decimal, discount_percent: Decimal) -> Decimal:
    """Calculate discounted price."""
    discount = (price * discount_percent / 100).quantize(Decimal("0.01"))
    return price - discount

original = Decimal("99.99")
discounted = calculate_discount(original, Decimal("15"))  # 15% off
print(f"Original: ${original}, After discount: ${discounted}")
```

### Split amounts

Divide a total amount evenly among multiple parties, handling remainders:

```python
from decimal import Decimal, ROUND_DOWN

def split_amount(total: Decimal, ways: int) -> list[Decimal]:
    """Split amount evenly, handling remainder."""
    per_person = (total / ways).quantize(Decimal("0.01"), rounding=ROUND_DOWN)
    remainder = total - (per_person * ways)
    
    amounts = [per_person] * ways
    # Add remainder to first person
    amounts[0] += remainder
    return amounts

total = Decimal("100.00")
split = split_amount(total, 3)
print(split)  # [Decimal('33.34'), Decimal('33.33'), Decimal('33.33')]
print(sum(split))  # Decimal('100.00') - always equals original
```

## Aggregation operations

### SUM and AVG with decimals

SQL Server aggregation functions return Decimal types for precision:

```python
# SUM preserves decimal type
cursor.execute("SELECT SUM(TotalDue) AS TotalSales FROM Sales.SalesOrderHeader")
total_sales = cursor.fetchval()
print(type(total_sales))  # <class 'decimal.Decimal'>

# AVG might return more decimal places
cursor.execute("SELECT AVG(ListPrice) AS AvgPrice FROM Production.Product")
avg_price = cursor.fetchval()
# Round to desired precision
avg_price = avg_price.quantize(Decimal("0.01"))
```

### Handle NULL in aggregations

SQL Server aggregation functions can return `None` when no rows match the query:

```python
cursor.execute("SELECT SUM(TotalDue) FROM Sales.SalesOrderHeader WHERE CustomerID = 0")
total_discount = cursor.fetchval()

# SUM returns NULL if no rows match
if total_discount is None:
    total_discount = Decimal("0.00")
```

## Output converters for decimal

Output converters use the Python type shown in `cursor.description` as the key, not the ODBC SQL type integer. Register the converter with `decimal.Decimal` so it works for `decimal`, `numeric`, `money`, and `smallmoney` columns, which all map to `decimal.Decimal`.

### Convert to float (when precision isn't critical)

For compatibility with libraries that expect float values, define an output converter:

```python
import mssql_python
from decimal import Decimal

def decimal_to_float(value):
    """Convert Decimal to float."""
    if value is None:
        return None
    return float(value)  # value is already a Decimal object

conn = mssql_python.connect(connection_string)

# Convert decimals to float for compatibility with libraries that expect float
conn.add_output_converter(Decimal, decimal_to_float)

cursor = conn.cursor()
cursor.execute("SELECT ListPrice FROM Production.Product WHERE ProductID = 1")
row = cursor.fetchone()
print(type(row.ListPrice))  # <class 'float'>
```

### Custom money formatting converter

Define a custom output converter to automatically format Decimal values as currency strings:

```python
from decimal import Decimal

def money_to_string(value):
    """Convert Decimal to formatted string."""
    if value is None:
        return None
    return f"${value:,.2f}"  # value is already a Decimal object

conn.add_output_converter(Decimal, money_to_string)

cursor = conn.cursor()
cursor.execute("CREATE TABLE #Accounts (Balance DECIMAL(19,4))")
cursor.execute(
    "INSERT INTO #Accounts (Balance) VALUES (%(bal)s)",
    {"bal": Decimal("1234.56")}
)
conn.commit()

cursor.execute("SELECT Balance FROM #Accounts")
for row in cursor:
    print(row.Balance)  # "$1,234.56"
```

## Common patterns

### Currency conversion

Convert monetary amounts between currencies using exchange rates:

```python
from decimal import Decimal

def convert_currency(amount: Decimal, rate: Decimal) -> Decimal:
    """Convert currency at given exchange rate."""
    return (amount * rate).quantize(Decimal("0.01"))

usd_amount = Decimal("100.00")
eur_rate = Decimal("0.92")
eur_amount = convert_currency(usd_amount, eur_rate)
print(f"${usd_amount} = €{eur_amount}")
```

### Balance updates with transactions

Use transactions with appropriate lock hints to ensure atomic fund transfers and prevent lost updates:

```python
from decimal import Decimal

def transfer_funds(conn, from_account: int, to_account: int, amount: Decimal):
    """Transfer funds between accounts atomically."""
    cursor = conn.cursor()
    conn.autocommit = False
    
    try:
        balances = {}
        for account_id in sorted((from_account, to_account)):
            cursor.execute(
                "SELECT Balance FROM #Accounts WITH (UPDLOCK) WHERE ID = %(id)s",
                {"id": account_id}
            )
            row = cursor.fetchone()
            if row is None:
                raise ValueError(f"Account {account_id} not found")
            balances[account_id] = row.Balance
        
        if balances[from_account] < amount:
            raise ValueError("Insufficient funds")
        
        # Debit source
        cursor.execute("""
            UPDATE #Accounts SET Balance = Balance - %(amount)s
            WHERE ID = %(id)s
        """, {"amount": amount, "id": from_account})
        
        # Credit destination
        cursor.execute("""
            UPDATE #Accounts SET Balance = Balance + %(amount)s
            WHERE ID = %(id)s
        """, {"amount": amount, "id": to_account})
        
        conn.commit()
        
    except Exception:
        conn.rollback()
        raise
    finally:
        conn.autocommit = True

# Set up demo accounts
cursor = conn.cursor()
cursor.execute("CREATE TABLE #Accounts (ID INT, Balance MONEY)")
cursor.executemany(
    "INSERT INTO #Accounts (ID, Balance) VALUES (%(id)s, %(bal)s)",
    [{"id": 1, "bal": Decimal("1000.00")}, {"id": 2, "bal": Decimal("500.00")}]
)
conn.commit()

# Usage
transfer_funds(conn, from_account=1, to_account=2, amount=Decimal("500.00"))
```

The transfer locks both rows up front, lowest ID first, by issuing a separate `SELECT ... WITH (UPDLOCK)` for each account in sorted order. `UPDLOCK` prevents the lost update pattern where two transactions read the same balance and apply updates based on stale data. Issuing the locks as separate statements in sorted order is what guarantees the acquisition order, preventing opposing transfers between the same two accounts from acquiring locks in the reverse order and deadlocking.

### Bulk insert with decimals

Insert multiple rows with Decimal values using `executemany()`:

```python
from decimal import Decimal

cursor.execute("""
    CREATE TABLE #BulkProducts (
        Name NVARCHAR(50),
        Price DECIMAL(10,2),
        Cost DECIMAL(10,2)
    )
""")

products = [
    {"name": "Widget A", "price": Decimal("19.99"), "cost": Decimal("8.50")},
    {"name": "Widget B", "price": Decimal("29.99"), "cost": Decimal("12.75")},
    {"name": "Widget C", "price": Decimal("39.99"), "cost": Decimal("18.00")},
]

cursor.executemany("""
    INSERT INTO #BulkProducts (Name, Price, Cost)
    VALUES (%(name)s, %(price)s, %(cost)s)
""", products)
conn.commit()
```

## Best practices

### Always use Decimal for financial math

Never use float for financial calculations; always use the Decimal type for precision:

```python
# Wrong: float precision issues
price = 0.10
quantity = 3
total = price * quantity  # 0.30000000000000004

# Correct: Decimal is exact
price = Decimal("0.10")
quantity = 3
total = price * quantity  # Decimal('0.30')
```

### Use string constructors

Construct Decimal values from strings to avoid float imprecision:

```python
# Good: exact value
amount = Decimal("123.45")

# Risky: might inherit float imprecision
amount = Decimal(123.45)  # Could be 123.4500000000000028...
```

### Always round before display or storage

Round Decimal values to the appropriate scale before displaying or persisting to the database:

```python
from decimal import Decimal, ROUND_HALF_UP

calculated = Decimal("123.456789")
display = calculated.quantize(Decimal("0.01"), rounding=ROUND_HALF_UP)
```

### Set decimal context if needed

The decimal context precision affects the results of *calculations*, not values parsed from strings or read from the database. A query always returns a `money` or `decimal` value with its full stored scale, regardless of `getcontext().prec`. The context only takes effect when you compute with it. To see the precision take effect, perform an operation such as division.

```python
from decimal import Decimal, getcontext

# A money value read from SQL Server arrives at its full stored scale,
# regardless of the context precision.
cursor.execute(
    "SELECT SubTotal FROM Sales.SalesOrderHeader WHERE SalesOrderID = 43659"
)
subtotal = cursor.fetchone().SubTotal
print(subtotal)  # Decimal('20565.6206')

# The context precision governs the calculation, not the fetched value.
# Split the subtotal into three equal installments.
getcontext().prec = 10
print(subtotal / 3)  # 6855.206867 (10 significant digits)

getcontext().prec = 28  # Reset to default
print(subtotal / 3)  # 6855.206866666666666666666667 (28 significant digits)
```

## Related content

- [Data type mappings for mssql-python](data-type-mappings.md)
- [Custom type converters with mssql-python](custom-type-converters.md)
- [Transaction management with mssql-python](transaction-management.md)
