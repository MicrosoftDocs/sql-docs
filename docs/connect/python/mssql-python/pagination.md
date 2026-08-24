---
title: Implement Pagination with mssql-python
description: Learn how to implement efficient pagination patterns when querying large datasets using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Implement pagination

The mssql-python driver supports efficient pagination patterns for dividing large result sets into manageable chunks (pages). This approach improves:

- Application performance.
- Memory usage.
- User experience.
- Network efficiency.

## OFFSET-FETCH pagination

The preferred method for SQL Server 2012 and later versions:

### Basic OFFSET-FETCH

Use OFFSET-FETCH to retrieve a specific page of sorted results:

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

def get_page(cursor, page: int, page_size: int) -> list:
    """Get a specific page of results."""
    offset = (page - 1) * page_size
    
    cursor.execute("""
        SELECT ProductID, Name, ListPrice
        FROM Production.Product
        ORDER BY Name
        OFFSET %(offset)s ROWS
        FETCH NEXT %(page_size)s ROWS ONLY
    """, {"offset": offset, "page_size": page_size})
    
    return cursor.fetchall()

# Get page 3 with 20 items per page
products = get_page(cursor, page=3, page_size=20)
for p in products:
    print(f"{p.ProductID}: {p.Name}")
```

### With total count

Combine page data with a total record count to display pagination controls:

```python
def get_page_with_count(cursor, page: int, page_size: int) -> tuple[list, int]:
    """Get page results and total count."""
    offset = (page - 1) * page_size
    
    # Get total count
    cursor.execute("SELECT COUNT(*) FROM Production.Product")
    total_count = cursor.fetchval()
    
    # Get page data
    cursor.execute("""
        SELECT ProductID, Name, ListPrice
        FROM Production.Product
        ORDER BY Name
        OFFSET %(offset)s ROWS
        FETCH NEXT %(page_size)s ROWS ONLY
    """, {"offset": offset, "page_size": page_size})
    
    return cursor.fetchall(), total_count

products, total = get_page_with_count(cursor, page=1, page_size=20)
total_pages = (total + 19) // 20  # Ceiling division
print(f"Page 1 of {total_pages} ({total} total products)")
```

### Pagination helper class

Create a reusable class to manage paged results and calculate pagination properties:

```python
from dataclasses import dataclass
from typing import Generic, TypeVar, List

T = TypeVar('T')

@dataclass
class PagedResult(Generic[T]):
    """Container for paged query results."""
    items: List[T]
    page: int
    page_size: int
    total_count: int
    
    @property
    def total_pages(self) -> int:
        return (self.total_count + self.page_size - 1) // self.page_size
    
    @property
    def has_previous(self) -> bool:
        return self.page > 1
    
    @property
    def has_next(self) -> bool:
        return self.page < self.total_pages

def get_products_paged(cursor, page: int, page_size: int = 20) -> PagedResult:
    """Get paged product results."""
    offset = (page - 1) * page_size
    
    # Single query with COUNT OVER for total
    cursor.execute("""
        SELECT 
            ProductID, Name, ListPrice,
            COUNT(*) OVER() AS TotalCount
        FROM Production.Product
        ORDER BY Name
        OFFSET %(offset)s ROWS
        FETCH NEXT %(page_size)s ROWS ONLY
    """, {"offset": offset, "page_size": page_size})
    
    rows = cursor.fetchall()
    total = rows[0].TotalCount if rows else 0
    
    return PagedResult(
        items=rows,
        page=page,
        page_size=page_size,
        total_count=total
    )

# Usage
result = get_products_paged(cursor, page=2, page_size=10)
print(f"Page {result.page} of {result.total_pages}")
print(f"Has previous: {result.has_previous}, Has next: {result.has_next}")
```

## Keyset pagination

More efficient for large datasets and deep pagination:

### Use keyset (seek method)

Keyset pagination uses a unique column to seek to the next page, avoiding expensive OFFSET scans:

```python
def get_products_after(cursor, last_id: int | None, page_size: int = 20) -> list:
    """Get products after a specific ID."""
    if last_id is None:
        # First page
        cursor.execute("""
            SELECT TOP (%(page_size)s) ProductID, Name, ListPrice
            FROM Production.Product
            ORDER BY ProductID
        """, {"page_size": page_size})
    else:
        # Subsequent pages
        cursor.execute("""
            SELECT TOP (%(page_size)s) ProductID, Name, ListPrice
            FROM Production.Product
            WHERE ProductID > %(last_id)s
            ORDER BY ProductID
        """, {"page_size": page_size, "last_id": last_id})
    
    return cursor.fetchall()

# Iterate through all products in pages
last_id = None
while True:
    products = get_products_after(cursor, last_id, page_size=100)
    if not products:
        break
    
    for p in products:
        print(f"{p.ProductID}: {p.Name}")
    
    last_id = products[-1].ProductID  # Track last ID for next iteration
```

### Keyset with composite key

For tables with multiple sort columns, use a composite key to ensure stable pagination:

```python
def get_orders_page(cursor, last_date: datetime | None, last_id: int | None, 
                    page_size: int = 20) -> list:
    """Keyset pagination with composite key (OrderDate, SalesOrderID)."""
    if last_date is None:
        cursor.execute("""
            SELECT TOP (%(size)s) SalesOrderID, OrderDate, CustomerID, TotalDue
            FROM Sales.SalesOrderHeader
            ORDER BY OrderDate DESC, SalesOrderID DESC
        """, {"size": page_size})
    else:
        cursor.execute("""
            SELECT TOP (%(size)s) SalesOrderID, OrderDate, CustomerID, TotalDue
            FROM Sales.SalesOrderHeader
            WHERE (OrderDate < %(last_date)s) 
               OR (OrderDate = %(last_date)s AND SalesOrderID < %(last_id)s)
            ORDER BY OrderDate DESC, SalesOrderID DESC
        """, {"size": page_size, "last_date": last_date, "last_id": last_id})
    
    return cursor.fetchall()

# Usage
orders = get_orders_page(cursor, None, None, page_size=50)
if orders:
    # Get cursor for next page
    last = orders[-1]
    next_page = get_orders_page(cursor, last.OrderDate, last.SalesOrderID, page_size=50)
```

## Cursor-based pagination

### Encode/decode cursors

Encrypt the pagination state into an opaque cursor string for API clients:

```python
import base64
import json

def encode_cursor(values: dict) -> str:
    """Encode pagination cursor."""
    return base64.b64encode(json.dumps(values).encode()).decode()

def decode_cursor(cursor_str: str) -> dict:
    """Decode pagination cursor."""
    return json.loads(base64.b64decode(cursor_str.encode()).decode())

def get_page_with_cursor(cursor, after: str | None, page_size: int = 20) -> tuple[list, str | None]:
    """Get page using cursor-based pagination."""
    if after is None:
        cursor.execute("""
            SELECT TOP (%(size)s) ProductID, Name, ListPrice
            FROM Production.Product
            ORDER BY ProductID
        """, {"size": page_size})
    else:
        values = decode_cursor(after)
        cursor.execute("""
            SELECT TOP (%(size)s) ProductID, Name, ListPrice
            FROM Production.Product
            WHERE ProductID > %(last_id)s
            ORDER BY ProductID
        """, {"size": page_size, "last_id": values["id"]})
    
    rows = cursor.fetchall()
    
    next_cursor = None
    if rows:
        next_cursor = encode_cursor({"id": rows[-1].ProductID})
    
    return rows, next_cursor

# Usage - First page
products, next_cursor = get_page_with_cursor(cursor, after=None, page_size=20)

# Next page
if next_cursor:
    products, next_cursor = get_page_with_cursor(cursor, after=next_cursor, page_size=20)
```

## ROW_NUMBER pagination

For compatibility with older SQL Server versions:

```python
def get_page_row_number(cursor, page: int, page_size: int) -> list:
    """Pagination using ROW_NUMBER() - works on SQL Server 2005+."""
    cursor.execute("""
        WITH NumberedProducts AS (
            SELECT 
                ProductID, Name, ListPrice,
                ROW_NUMBER() OVER (ORDER BY Name) AS RowNum
            FROM Production.Product
        )
        SELECT ProductID, Name, ListPrice
        FROM NumberedProducts
        WHERE RowNum > %(start)s AND RowNum <= %(end)s
    """, {"start": (page - 1) * page_size, "end": page * page_size})
    
    return cursor.fetchall()
```

## Filtered and sorted pagination

### With dynamic filters

Combine `WHERE` clauses, sorting, and `OFFSET-FETCH` to support dynamic search filters with pagination. This example reuses the `PagedResult` class from [Pagination helper class](#pagination-helper-class).

```python
def search_products_paged(cursor, search: str | None, category: int | None,
                         page: int = 1, page_size: int = 20, 
                         sort: str = "Name") -> PagedResult:
    """Search products with filters, sorting, and pagination."""
    
    # Build WHERE clause
    conditions = []
    params = {"offset": (page - 1) * page_size, "page_size": page_size}
    
    if search:
        conditions.append("Name LIKE %(search)s")
        params["search"] = f"%{search}%"
    
    if category:
        conditions.append("ProductSubcategoryID = %(category)s")
        params["category"] = category
    
    where_clause = "WHERE " + " AND ".join(conditions) if conditions else ""
    
    # Validate sort column
    sort_columns = {"Name": "Name", "Price": "ListPrice", "ID": "ProductID"}
    order_column = sort_columns.get(sort, "Name")
    
    # Execute query
    query = f"""
        SELECT 
            ProductID, Name, ListPrice, ProductSubcategoryID,
            COUNT(*) OVER() AS TotalCount
        FROM Production.Product
        {where_clause}
        ORDER BY {order_column}
        OFFSET %(offset)s ROWS
        FETCH NEXT %(page_size)s ROWS ONLY
    """
    
    cursor.execute(query, params)
    rows = cursor.fetchall()
    
    return PagedResult(
        items=rows,
        page=page,
        page_size=page_size,
        total_count=rows[0].TotalCount if rows else 0
    )

# Usage
results = search_products_paged(
    cursor, 
    search="Road", 
    category=2,
    page=1, 
    page_size=15,
    sort="Price"
)
```

## Generator-based iteration

### Iterate all results in pages

Use a generator to efficiently process all records by yielding results one page at a time:

```python
def iter_all_products(cursor, page_size: int = 1000):
    """Generator that yields all products in pages."""
    offset = 0
    
    while True:
        cursor.execute("""
            SELECT ProductID, Name, ListPrice
            FROM Production.Product
            ORDER BY ProductID
            OFFSET %(offset)s ROWS
            FETCH NEXT %(size)s ROWS ONLY
        """, {"offset": offset, "size": page_size})
        
        rows = cursor.fetchall()
        if not rows:
            break
        
        for row in rows:
            yield row
        
        offset += page_size

# Process all products without loading into memory
for product in iter_all_products(cursor, page_size=500):
    print(product)  # Replace with your own row-handling logic
```

## Performance tips

### Use appropriate indexes

Create indexes on columns used in ORDER BY and WHERE clauses to optimize pagination queries:

```sql
-- Index for OFFSET-FETCH on Name
CREATE INDEX IX_Products_Name ON Production.Product (Name);

-- Index for keyset pagination on ID
CREATE INDEX IX_Products_ID ON Production.Product (ProductID);

-- Covering index for common query
CREATE INDEX IX_Products_SubCategory_Name 
ON Production.Product (ProductSubcategoryID, Name) 
INCLUDE (ListPrice);
```

### Avoid deep OFFSET pagination

OFFSET scans skipped rows, making deep pages slow. Use keyset pagination for better performance:

```python
# Slow for deep pages
get_page(cursor, page=1000, page_size=20)  # Scans 19,980 rows first

# Use keyset for better deep-page performance
get_products_after(cursor, last_id=19980, page_size=20)  # Seeks directly
```

### Cache total count

Avoid recounting rows on every pagination request by caching the total for a period of time:

```python
import time

class PaginatedQuery:
    def __init__(self, cursor, count_cache_seconds: int = 60):
        self.cursor = cursor
        self.count_cache_seconds = count_cache_seconds
        self._count_cache = {}
    
    def get_count(self, query_key: str, count_query: str, params: dict) -> int:
        """Get cached count or execute count query."""
        now = time.time()
        
        if query_key in self._count_cache:
            count, timestamp = self._count_cache[query_key]
            if now - timestamp < self.count_cache_seconds:
                return count
        
        self.cursor.execute(count_query, params)
        count = self.cursor.fetchval()
        self._count_cache[query_key] = (count, now)
        return count
```

## Related content

- [Execute queries with mssql-python](executing-queries.md)
- [Retrieve data with mssql-python](retrieving-data.md)
- [Connection pooling with mssql-python](connection-pooling.md)
