---
title: Work with Datetime Values in mssql-python
description: Learn how to handle datetime, date, time, and time zone data when you use the mssql-python driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Work with datetime values

The mssql-python driver handles SQL Server's date and time types and maps them to Python's `datetime` module objects. SQL Server provides multiple date and time types with varying precision and capabilities:

| SQL Type | Python Type | Range | Precision |
| ---------- | ------------- | ------- | ----------- |
| **date** | `datetime.date` | 0001-01-01 to 9999-12-31 | One day |
| **time** | `datetime.time` | 00:00:00.0000000 to 23:59:59.9999999 | 100 nanoseconds |
| **datetime** | `datetime.datetime` | 1753-01-01 to 9999-12-31 | 3.33 milliseconds |
| **datetime2** | `datetime.datetime` | 0001-01-01 to 9999-12-31 | 100 nanoseconds |
| **smalldatetime** | `datetime.datetime` | 1900-01-01 to 2079-06-06 | 1 minute |
| **datetimeoffset** | `datetime.datetime` (with tzinfo) | 0001-01-01 to 9999-12-31 | 100 nanoseconds + time zone |

## Insert datetime values

### Use Python datetime objects

The following example inserts different date and time types into SQL Server using Python's `datetime` module:

```python
from datetime import datetime, date, time
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Create a temp table with date, time, and datetime columns
cursor.execute("""
    CREATE TABLE #Events (
        EventDate DATE,
        StartTime TIME,
        LogTime DATETIME2
    )
""")

# Insert date
cursor.execute(
    "INSERT INTO #Events (EventDate) VALUES (%(date)s)",
    {"date": date(2024, 12, 25)}
)

# Insert time
cursor.execute(
    "INSERT INTO #Events (StartTime) VALUES (%(time)s)",
    {"time": time(14, 30, 0)}
)

# Insert datetime
cursor.execute(
    "INSERT INTO #Events (LogTime) VALUES (%(ts)s)",
    {"ts": datetime(2024, 3, 15, 14, 30, 45)}
)

conn.commit()
```

### Insert current timestamp

You can insert the current date and time using `datetime.now()` or SQL Server's `GETDATE()` function:

```python
from datetime import datetime

# Create temp table for the demo
cursor.execute("CREATE TABLE #Logs (Message NVARCHAR(200), CreatedAt DATETIME2)")

# Python current time
now = datetime.now()
cursor.execute("INSERT INTO #Logs (Message, CreatedAt) VALUES (%(msg)s, %(ts)s)",
               {"msg": "Event occurred", "ts": now})

# Or use SQL Server's GETDATE()
cursor.execute("INSERT INTO #Logs (Message, CreatedAt) VALUES (%(msg)s, GETDATE())",
               {"msg": "Event occurred"})
conn.commit()
```

### High-precision datetime2

For higher precision timestamps, use the `datetime2` type which supports 100-nanosecond precision:

```python
from datetime import datetime

# Create temp table with a datetime2(7) column
cursor.execute("CREATE TABLE #PreciseLogs (EventTime DATETIME2(7))")

# Microsecond precision (Python supports up to microseconds)
precise_time = datetime(2024, 3, 15, 14, 30, 45, 123456)

cursor.execute(
    "INSERT INTO #PreciseLogs (EventTime) VALUES (%(ts)s)",  # datetime2(7) column
    {"ts": precise_time}
)
conn.commit()
```

## Retrieve datetime values

### Fetch dates and times

When retrieving datetime values from SQL Server, they are automatically converted to Python `datetime` objects:

```python
from datetime import date, time, datetime

# Create and populate a temp table with date, time, and datetime columns
cursor.execute("""
    CREATE TABLE #Events (
        ID INT,
        EventDate DATE,
        StartTime TIME,
        CreatedAt DATETIME2
    )
""")
cursor.execute(
    "INSERT INTO #Events (ID, EventDate, StartTime, CreatedAt) VALUES (1, %(d)s, %(t)s, %(dt)s)",
    {"d": date(2024, 12, 25), "t": time(14, 30, 0), "dt": datetime(2024, 3, 15, 14, 30, 45)}
)
conn.commit()

cursor.execute("SELECT EventDate, StartTime, CreatedAt FROM #Events WHERE ID = 1")
row = cursor.fetchone()

print(type(row.EventDate))   # <class 'datetime.date'>
print(type(row.StartTime))   # <class 'datetime.time'>
print(type(row.CreatedAt))   # <class 'datetime.datetime'>

print(row.EventDate)    # 2024-12-25
print(row.StartTime)    # 14:30:00
print(row.CreatedAt)    # 2024-03-15 14:30:45
```

### Access datetime components

Python datetime objects provide attributes to access individual date and time components:

```python
cursor.execute("SELECT OrderDate FROM Sales.SalesOrderHeader WHERE SalesOrderID = 43659")
row = cursor.fetchone()
dt = row.OrderDate

# Date components
print(dt.year)
print(dt.month)
print(dt.day)

# Time components
print(dt.hour)
print(dt.minute)
print(dt.second)
print(dt.microsecond)
```

## Time zones

### Offset-aware versus offset-naive datetimes

Python datetime objects are either offset-aware (have `tzinfo`) or offset-naive (no time zone information). SQL Server has both kinds of columns:

| SQL Server type | Expects | Stores time zone? |
| --- | --- | --- |
| **datetime**, **datetime2**, **smalldatetime** | Offset-naive | No |
| **datetimeoffset** | Offset-aware | Yes |

Sending a time zone-aware datetime to a `datetime2` column works, but the time zone information is silently dropped. Sending a naive datetime to a `datetimeoffset` column assigns UTC (+00:00) as the offset. Be explicit about which kind you send:

The following example shows how to create and use offset-naive and offset-aware datetimes:

```python
from datetime import datetime, timezone, timedelta

# Create temp tables: one datetime2 column and one datetimeoffset column
cursor.execute("CREATE TABLE #Logs (LogTime DATETIME2)")
cursor.execute("CREATE TABLE #GlobalEvents (EventTime DATETIMEOFFSET)")

# Offset-naive - use for datetime2 columns
naive_dt = datetime(2024, 3, 15, 14, 30, 45)
cursor.execute(
    "INSERT INTO #Logs (LogTime) VALUES (%(log_time)s)",  # datetime2 column
    {"log_time": naive_dt}
)

# Offset-aware - use for datetimeoffset columns
eastern = timezone(timedelta(hours=-5))
aware_dt = datetime(2024, 3, 15, 14, 30, 45, tzinfo=eastern)
cursor.execute(
    "INSERT INTO #GlobalEvents (EventTime) VALUES (%(event_time)s)",  # datetimeoffset column
    {"event_time": aware_dt}
)
conn.commit()
```

To convert between the two:

```python
from datetime import datetime, timezone

# Make naive datetime timezone-aware
naive = datetime(2024, 3, 15, 14, 30)
aware = naive.replace(tzinfo=timezone.utc)

# Strip timezone from aware datetime
aware = datetime.now(timezone.utc)
naive = aware.replace(tzinfo=None)
```

### datetimeoffset type

SQL Server's `datetimeoffset` stores time zone information. The following example demonstrates inserting and retrieving timezone-aware datetimes:

```python
from datetime import datetime, timezone, timedelta

# Create temp table with a datetimeoffset column
cursor.execute("CREATE TABLE #GlobalEvents (ID INT, EventTime DATETIMEOFFSET)")

# Create timezone-aware datetime
eastern = timezone(timedelta(hours=-5))
dt_eastern = datetime(2024, 3, 15, 14, 30, tzinfo=eastern)

cursor.execute(
    "INSERT INTO #GlobalEvents (ID, EventTime) VALUES (1, %(ts)s)",  # datetimeoffset column
    {"ts": dt_eastern}
)
conn.commit()

# Retrieve timezone-aware datetime
cursor.execute("SELECT EventTime FROM #GlobalEvents WHERE ID = 1")
row = cursor.fetchone()
print(row.EventTime)          # 2024-03-15 14:30:00-05:00
print(row.EventTime.tzinfo)   # UTC-05:00
```

### Convert between time zones

Use Python's timezone methods to convert datetimeoffset values between different time zone representations:

```python
from datetime import datetime, timezone, timedelta

# Create and populate a temp table with a datetimeoffset column
cursor.execute("CREATE TABLE #GlobalEvents (EventTime DATETIMEOFFSET)")
eastern = timezone(timedelta(hours=-5))
cursor.execute(
    "INSERT INTO #GlobalEvents (EventTime) VALUES (%(ts)s)",
    {"ts": datetime(2024, 3, 15, 14, 30, tzinfo=eastern)}
)
conn.commit()

# Retrieve as-stored
cursor.execute("SELECT EventTime FROM #GlobalEvents")
row = cursor.fetchone()
stored_time = row.EventTime  # Has timezone info

# Convert to UTC
utc_time = stored_time.astimezone(timezone.utc)
print(utc_time)

# Convert to local timezone
local_time = stored_time.astimezone()  # System timezone
print(local_time)
```

### AT TIME ZONE in SQL

SQL Server's `AT TIME ZONE` clause converts datetimeoffset values between time zones within a query:

```python
from datetime import datetime, timezone, timedelta

# Create and populate a temp table with a datetimeoffset column
cursor.execute("CREATE TABLE #GlobalEvents (EventTime DATETIMEOFFSET)")
eastern = timezone(timedelta(hours=-5))
cursor.execute(
    "INSERT INTO #GlobalEvents (EventTime) VALUES (%(ts)s)",
    {"ts": datetime(2024, 3, 15, 14, 30, tzinfo=eastern)}
)
conn.commit()

# Convert timezone in SQL Server (2016+)
cursor.execute("""
    SELECT 
        EventTime,
        EventTime AT TIME ZONE 'Pacific Standard Time' AS PacificTime,
        EventTime AT TIME ZONE 'UTC' AS UTCTime
    FROM #GlobalEvents
""")

for row in cursor:
    print(f"Original: {row.EventTime}")
    print(f"Pacific: {row.PacificTime}")
    print(f"UTC: {row.UTCTime}")
```

## Date arithmetic

### Calculate date differences

Calculate the number of days between two dates by subtracting datetime objects:

```python
from datetime import timedelta

cursor.execute("SELECT OrderDate, ShipDate FROM Sales.SalesOrderHeader WHERE SalesOrderID = 43659")
row = cursor.fetchone()

# Days between dates
if row.ShipDate and row.OrderDate:
    difference = row.ShipDate - row.OrderDate
    print(f"Shipped in {difference.days} days")
```

### Add intervals

Use `timedelta` to add or subtract days, months, or other intervals from a datetime:

```python
from datetime import datetime, timedelta

# Create and populate a temp table
cursor.execute("""
    CREATE TABLE #Subscriptions (
        ID INT,
        CreatedAt DATETIME2,
        ExpiresAt DATETIME2
    )
""")
cursor.execute(
    "INSERT INTO #Subscriptions (ID, CreatedAt) VALUES (1, %(created)s)",
    {"created": datetime(2024, 1, 1, 12, 0, 0)}
)
conn.commit()

cursor.execute("SELECT CreatedAt FROM #Subscriptions WHERE ID = 1")
row = cursor.fetchone()

# Calculate expiration (30 days from creation)
expiration = row.CreatedAt + timedelta(days=30)
print(f"Expires: {expiration}")

# Update with calculated date
cursor.execute(
    "UPDATE #Subscriptions SET ExpiresAt = %(exp)s WHERE ID = %(id)s",
    {"exp": expiration, "id": 1}
)
conn.commit()
```

### DATEADD in SQL

SQL Server's `DATEADD` function performs date arithmetic directly in queries:

```python
# Use SQL Server date functions
cursor.execute("""
    SELECT 
        OrderDate,
        DATEADD(day, 30, OrderDate) AS DueDate,
        DATEADD(month, 1, OrderDate) AS NextMonth
    FROM Sales.SalesOrderHeader
""")
```

## Format values

### Format for display

Format datetime values for display using `strftime()` with format specifiers:

```python
from datetime import datetime

cursor.execute("SELECT TOP(10) OrderDate FROM Sales.SalesOrderHeader")

for row in cursor:
    # strftime formatting
    print(row.OrderDate.strftime("%Y-%m-%d"))           # 2024-03-15
    print(row.OrderDate.strftime("%B %d, %Y"))          # March 15, 2024
    print(row.OrderDate.strftime("%m/%d/%Y %I:%M %p"))  # 03/15/2024 02:30 PM
```

### Parse from strings

Convert string representations to Python datetime objects using `strptime()`:

```python
from datetime import datetime

# Create temp table for the demo
cursor.execute("CREATE TABLE #Events (EventDate DATE)")

# Parse incoming date strings
date_string = "2024-03-15"
parsed_date = datetime.strptime(date_string, "%Y-%m-%d").date()

cursor.execute(
    "INSERT INTO #Events (EventDate) VALUES (%(date)s)",
    {"date": parsed_date}
)
conn.commit()
```

### ISO 8601 format

ISO 8601 format is useful for APIs and JSON serialization. Use `isoformat()` and `fromisoformat()` for conversion:

```python
from datetime import datetime

# Create and populate a temp table
cursor.execute("CREATE TABLE #Logs (ID INT, CreatedAt DATETIME2)")
cursor.execute(
    "INSERT INTO #Logs (ID, CreatedAt) VALUES (1, %(ts)s)",
    {"ts": datetime(2024, 3, 15, 14, 30, 45)}
)
conn.commit()

cursor.execute("SELECT CreatedAt FROM #Logs WHERE ID = 1")
row = cursor.fetchone()

# ISO format for APIs/JSON
iso_string = row.CreatedAt.isoformat()
print(iso_string)  # 2024-03-15T14:30:45

# Parse ISO format
dt = datetime.fromisoformat("2024-03-15T14:30:45")
```

## Common patterns

### Query by date range

Query records within a specific date range by parameterizing the start and end dates:

```python
from datetime import date, datetime

# Query by date range
start_date = date(2024, 1, 1)
end_date = date(2024, 12, 31)

cursor.execute("""
    SELECT SalesOrderID, OrderDate, TotalDue
    FROM Sales.SalesOrderHeader
    WHERE OrderDate >= %(start)s AND OrderDate < %(end)s
""", {"start": start_date, "end": end_date})
```

### Query today's records

Query records created today by casting the datetime column to a date and comparing with today's date:

```python
from datetime import date

# Create and populate a temp table with a row logged today
cursor.execute("CREATE TABLE #Logs (LogTime DATETIME2)")
cursor.execute("INSERT INTO #Logs (LogTime) VALUES (GETDATE())")
conn.commit()

cursor.execute("""
    SELECT * FROM #Logs 
    WHERE CAST(LogTime AS DATE) = %(today)s
""", {"today": date.today()})

# Or using SQL Server function
cursor.execute("""
    SELECT * FROM #Logs 
    WHERE CAST(LogTime AS DATE) = CAST(GETDATE() AS DATE)
""")
```

### Handle NULL dates

Check for `None` values before processing datetime columns, since NULL dates appear as `None` in Python:

```python
cursor.execute("SELECT TOP 5 FirstName, ModifiedDate FROM Person.Person")

for row in cursor:
    if row.ModifiedDate is None:
        print(f"{row.FirstName}: Date not on file")
    else:
        age = (date.today() - row.ModifiedDate.date()).days // 365
        print(f"{row.FirstName}: modified {age} years ago")
```

### Store audit timestamps

Create audit columns to track when records are created and modified:

```python
from datetime import datetime

# Create temp table for audit timestamp demo
cursor.execute("""
    CREATE TABLE #AuditOrders (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        CustomerID INT,
        Total DECIMAL(10,2),
        CreatedAt DATETIME2,
        ModifiedAt DATETIME2
    )
""")

def create_order(cursor, customer_id: int, total: float) -> int:
    now = datetime.now()
    cursor.execute("""
        INSERT INTO #AuditOrders (CustomerID, Total, CreatedAt, ModifiedAt)
        VALUES (%(cust)s, %(total)s, %(created)s, %(modified)s);
        SELECT SCOPE_IDENTITY();
    """, {
        "cust": customer_id,
        "total": total,
        "created": now,
        "modified": now
    })
    return cursor.fetchval()

def update_order(cursor, order_id: int, total: float):
    cursor.execute("""
        UPDATE #AuditOrders
        SET Total = %(total)s, ModifiedAt = %(modified)s
        WHERE ID = %(id)s
    """, {
        "total": total,
        "modified": datetime.now(),
        "id": order_id
    })
```

## Specific scenarios

### Dates before 1753

Use `datetime2` for historical dates (the `datetime` type only supports dates from 1753). The following example inserts a historical date:

```python
from datetime import datetime

# Create temp table with a datetime2 column
cursor.execute("CREATE TABLE #HistoricalEvents (EventDate DATETIME2, Description NVARCHAR(200))")

# Historical date (works with datetime2, fails with datetime)
historical = datetime(1500, 1, 1)

cursor.execute(
    "INSERT INTO #HistoricalEvents (EventDate, Description) VALUES (%(date)s, %(desc)s)",
    {"date": historical, "desc": "Archived historical record"}
)
conn.commit()
```

### Time-only values

Store time-of-day values using Python's `time` object:

```python
from datetime import time

# Create temp table with time columns
cursor.execute("""
    CREATE TABLE #BusinessHours (
        DayOfWeek NVARCHAR(10),
        OpenTime TIME,
        CloseTime TIME
    )
""")

# Store time of day without date
opening_time = time(9, 0, 0)   # 9:00 AM
closing_time = time(17, 30, 0) # 5:30 PM

cursor.execute("""
    INSERT INTO #BusinessHours (DayOfWeek, OpenTime, CloseTime)
    VALUES (%(day)s, %(open)s, %(close)s)
""", {"day": "Monday", "open": opening_time, "close": closing_time})
conn.commit()
```

### Calculate business days

A helper function can calculate date intervals while skipping weekends:

```python
from datetime import date, timedelta

def add_business_days(start: date, days: int) -> date:
    """Add business days (skip weekends)."""
    current = start
    remaining = days
    while remaining > 0:
        current += timedelta(days=1)
        if current.weekday() < 5:  # Monday = 0, Friday = 4
            remaining -= 1
    return current

order_date = date(2024, 3, 15)  # Friday
due_date = add_business_days(order_date, 5)  # Skip weekend
print(f"Due date: {due_date}")  # 2024-03-22 (next Friday)
```

## Related content

- [Data type mappings for mssql-python](data-type-mappings.md)
- [Handle NULL values](null-handling.md)
- [Retrieve data with mssql-python](retrieving-data.md)
