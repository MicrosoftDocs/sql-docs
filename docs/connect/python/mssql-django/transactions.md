---
title: Transaction Management in mssql-django
description: Configure transaction handling and isolation levels in Django applications using the mssql-django backend.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Transaction management in mssql-django

This article explains how to configure transaction handling and isolation levels for Django applications using the `mssql-django` backend with SQL Server.

## Default behavior

By default, Django operates in autocommit mode. Each database query runs in its own transaction and is committed immediately. You can change this behavior using the `AUTOCOMMIT` setting or Django's transaction management API.

## AUTOCOMMIT setting

Set `AUTOCOMMIT` to `False` in your database configuration to disable autocommit mode:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "AUTOCOMMIT": False,
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
}
```

> [!NOTE]  
> Disabling autocommit means you must explicitly commit or roll back transactions. Most Django applications leave autocommit enabled and use `transaction.atomic()` for specific operations.

## Use transaction.atomic()

Wrap database operations in `transaction.atomic()` to ensure they execute in a single transaction:

```python
from django.db import transaction
from myapp.models import Account

def transfer_funds(from_account_id, to_account_id, amount):
    with transaction.atomic():
        sender = Account.objects.select_for_update().get(pk=from_account_id)
        receiver = Account.objects.select_for_update().get(pk=to_account_id)

        sender.balance -= amount
        receiver.balance += amount

        sender.save()
        receiver.save()
```

If any exception occurs inside an `atomic()` block, the entire transaction is rolled back.

### Nested transactions

Django supports nested `atomic()` blocks through SQL Server's savepoints:

```python
from django.db import transaction

with transaction.atomic():
    # Outer transaction
    Product.objects.create(name="Widget A", price=9.99)

    try:
        with transaction.atomic():
            # Inner savepoint
            Product.objects.create(name="Widget B", price=14.99)
            raise ValueError("Simulated error")
    except ValueError:
        pass  # Inner savepoint is rolled back, outer continues

    # Widget A is committed, Widget B is not
```

## Transaction isolation levels

Configure the transaction isolation level using the `isolation_level` option in your database configuration:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "isolation_level": "READ COMMITTED",
        },
    },
}
```

### Supported isolation levels

| Isolation level | Description |
| --- | --- |
| `READ UNCOMMITTED` | Allows dirty reads. Lowest isolation, highest concurrency. |
| `READ COMMITTED` | SQL Server default. Prevents dirty reads. |
| `REPEATABLE READ` | Prevents dirty and non-repeatable reads. |
| `SNAPSHOT` | Uses row versioning for consistent reads without blocking. Requires database-level snapshot isolation to be enabled. |
| `SERIALIZABLE` | Highest isolation. Prevents phantom reads. |

### Enable SNAPSHOT isolation

To use `SNAPSHOT` isolation, enable it on the database first:

```sql
ALTER DATABASE [<your-database>]
SET ALLOW_SNAPSHOT_ISOLATION ON;
```

Then configure it in `settings.py`:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "USER": "<your-username>",
        "PASSWORD": "<your-password>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "isolation_level": "SNAPSHOT",
        },
    },
}
```

## Use the @transaction.atomic decorator

Apply transactions to entire view functions:

```python
from django.db import transaction
from django.http import JsonResponse

@transaction.atomic
def create_order(request):
    # All database operations in this view run in a single transaction
    order = Order.objects.create(customer_id=request.user.id)
    for item in request.POST.getlist("items"):
        OrderItem.objects.create(order=order, product_id=item)
    return JsonResponse({"order_id": order.pk})
```

## Read data without blocking (NOLOCK equivalent)

A common request is to query SQL Server with the `NOLOCK` hint or `READ UNCOMMITTED` isolation to avoid blocking on busy tables. Django's ORM doesn't generate table hints, but you have two options.

### Option 1: Set READ UNCOMMITTED per connection

Set the isolation level to `READ UNCOMMITTED` on a dedicated read-only database alias to apply it to all queries on that connection:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
    },
    "read_uncommitted": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "isolation_level": "READ UNCOMMITTED",
        },
    },
}
```

Then route queries to the `read_uncommitted` alias:

```python
# Read with NOLOCK-equivalent behavior
products = Product.objects.using("read_uncommitted").filter(active=True)

# Writes still go through the default connection
Product.objects.create(name="Widget", price=9.99)
```

### Option 2: Use raw SQL with NOLOCK

For targeted queries on specific tables, use raw SQL with the `NOLOCK` table hint:

```python
from django.db import connection

with connection.cursor() as cursor:
    cursor.execute("SELECT id, name, price FROM myapp_product WITH (NOLOCK) WHERE active = %s", [1])
    rows = cursor.fetchall()
```

> [!CAUTION]  
> Both `READ UNCOMMITTED` and `NOLOCK` allow dirty reads, which means queries can return data from uncommitted transactions. Use these techniques only for reporting or analytics queries where absolute consistency isn't required.

### Option 3: Use SNAPSHOT isolation instead

`SNAPSHOT` isolation provides consistent reads without blocking and without dirty reads. It's the recommended alternative to `NOLOCK` for most workloads:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "isolation_level": "SNAPSHOT",
        },
    },
}
```

`SNAPSHOT` requires database-level configuration. See [Enable SNAPSHOT isolation](#enable-snapshot-isolation).

## Row-level locking with select_for_update()

Django's `select_for_update()` is fully supported by the `mssql-django` backend. SQL Server implements this using table hints instead of the `FOR UPDATE` clause used by other databases.

### Basic usage

```python
from django.db import transaction

with transaction.atomic():
    product = Product.objects.select_for_update().get(pk=1)
    product.stock -= 1
    product.save()
```

The backend generates: `SELECT ... FROM [myapp_product] WITH (ROWLOCK, UPDLOCK) WHERE ...`

### NOWAIT and SKIP LOCKED

The `nowait` and `skip_locked` parameters are both supported:

```python
from django.db import transaction

# Raise DatabaseError immediately if the row is already locked
with transaction.atomic():
    product = Product.objects.select_for_update(nowait=True).get(pk=1)

# Skip rows that are locked by other transactions
with transaction.atomic():
    available = Product.objects.select_for_update(skip_locked=True).filter(
        reserved=False
    )[:10]
```

| Parameter | SQL Server table hint |
| --- | --- |
| Default | `WITH (ROWLOCK, UPDLOCK)` |
| `nowait=True` | `WITH (NOWAIT, ROWLOCK, UPDLOCK)` |
| `skip_locked=True` | `WITH (ROWLOCK, UPDLOCK, READPAST)` |

> [!NOTE]  
> `select_for_update()` must be used inside a `transaction.atomic()` block. Django raises an error if you call it outside a transaction.

### Differences from PostgreSQL

- The `of` parameter (`select_for_update(of=(...))`) isn't supported. The backend raises `NotSupportedError` if you pass it.
- SQL Server uses table-level hints (`UPDLOCK`) instead of row-level `FOR UPDATE` clauses. Under high contention, lock escalation can cause more rows or pages to be locked than you targeted. Use the `SNAPSHOT` isolation level if you need non-blocking reads alongside locked writes.

## Related content

- [mssql-django configuration reference](configuration-reference.md)
- [Performance tuning for mssql-django](performance-tuning.md)
- [Django transaction documentation](https://docs.djangoproject.com/en/stable/topics/db/transactions)
- [SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](../../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
