---
title: Retry Logic and Connection Resilience with mssql-django
description: Implement retry logic for transient database errors in Django applications using the mssql-django backend with SQL Server and Azure SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Retry logic and connection resilience with mssql-django

Connections to SQL Server and Azure SQL can fail transiently for reasons that have nothing to do with your code:

- An Always On availability group fails over.
- The network drops a packet during connection setup.
- Resource Governor throttles the database.
- An Azure SQL replica is recycled during a scale or upgrade.

Most of these failures clear within seconds. This article shows how to retry transient errors in a Django application that uses the `mssql-django` backend, and how to configure Django and the ODBC driver to recover from idle-connection drops automatically.

## Transient errors

Transient errors are temporary failures that resolve on their own. Retrying the operation after a short delay usually succeeds.

[!INCLUDE [transient-connection-errors](../../includes/transient-connection-errors.md)]

## ODBC driver idle connection resiliency

The Microsoft ODBC Driver for SQL Server provides built-in idle connection resiliency through the `ConnectRetryCount` and `ConnectRetryInterval` connection string keywords. These settings handle dropped idle connections at the driver level, before your application code is involved.

Enable idle connection resiliency in `extra_params`:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>.database.windows.net",
        "PORT": "1433",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
            "extra_params": "ConnectRetryCount=3;ConnectRetryInterval=10",
        },
    },
}
```

| Keyword | Default | Description |
| --- | --- | --- |
| `ConnectRetryCount` | 1 | Number of automatic reconnection attempts for idle connections. |
| `ConnectRetryInterval` | 10 | Seconds between reconnection attempts. |

> [!NOTE]  
> Idle connection resiliency reconnects connections that were dropped while idle. It doesn't retry failed queries or recover from errors that occur during active transactions. For those scenarios, use application-level retry logic.

## Django database middleware for retries

Create a Django middleware that catches transient errors and retries the database operation. This approach works for view-level request handling:

```python
# myproject/middleware.py
import random
import re
import time
import logging
from django.db import OperationalError, connection

logger = logging.getLogger(__name__)

TRANSIENT_ERROR_CODES = {
    "64", "233", "4221",
    "10053", "10054", "10928", "10929",
    "40197", "40501", "40613",
    "49918", "49919", "49920",
    # Include "4060" only if targeting Azure SQL with geo-replication failover.
    # It is usually a permanent error (wrong database name or missing permissions).
}

# Microsoft ODBC driver formats native error codes as "(<number>)" in the
# message. Extracting parenthesized codes avoids false positives that a plain
# substring match would produce for short codes like "64".
_CODE_RE = re.compile(r"\((\d+)\)")


def is_transient(error):
    codes_in_message = set(_CODE_RE.findall(str(error)))
    return bool(codes_in_message & TRANSIENT_ERROR_CODES)


class DatabaseRetryMiddleware:
    """Retry database operations on transient errors."""

    def __init__(self, get_response):
        self.get_response = get_response
        self.max_retries = 3
        self.base_delay = 1   # seconds; doubled each attempt
        self.max_delay = 30   # cap on a single sleep, regardless of attempt

    def __call__(self, request):
        for attempt in range(self.max_retries + 1):
            try:
                return self.get_response(request)
            except OperationalError as e:
                if attempt < self.max_retries and is_transient(e):
                    # Exponential backoff with full jitter, capped at max_delay.
                    # Jitter spreads simultaneous retries so many clients
                    # don't hammer the server in lock-step during an outage.
                    capped = min(self.max_delay, self.base_delay * (2 ** attempt))
                    delay = random.uniform(0, capped)
                    logger.warning(
                        "Transient DB error (attempt %d/%d), retrying in %.2fs: %s",
                        attempt + 1, self.max_retries, delay, e
                    )
                    connection.close()
                    time.sleep(delay)
                    continue
                raise
```

Register the middleware in `settings.py`:

```python
MIDDLEWARE = [
    "myproject.middleware.DatabaseRetryMiddleware",
    "django.middleware.security.SecurityMiddleware",
    # ... other middleware
]
```

> [!IMPORTANT]  
> Place `DatabaseRetryMiddleware` before other middleware that accesses the database so it can catch and retry transient errors from the entire request pipeline.

## Retry decorator for specific operations

For finer-grained control, use a decorator on individual functions:

```python
import random
import re
import time
import functools
import logging
from django.db import OperationalError, connection

logger = logging.getLogger(__name__)

TRANSIENT_ERROR_CODES = {
    "64", "233", "4221",
    "10053", "10054", "10928", "10929",
    "40197", "40501", "40613",
    "49918", "49919", "49920",
    # Include "4060" only if targeting Azure SQL with geo-replication failover.
}

_CODE_RE = re.compile(r"\((\d+)\)")


def is_transient(error):
    codes_in_message = set(_CODE_RE.findall(str(error)))
    return bool(codes_in_message & TRANSIENT_ERROR_CODES)


def retry_on_transient(max_retries=3, base_delay=1, max_delay=30):
    """Retry on transient database errors with exponential backoff and full jitter."""

    def decorator(func):
        @functools.wraps(func)
        def wrapper(*args, **kwargs):
            for attempt in range(max_retries + 1):
                try:
                    return func(*args, **kwargs)
                except OperationalError as e:
                    if attempt < max_retries and is_transient(e):
                        # Exponential cap doubled per attempt, then jittered
                        # within [0, cap] and limited by max_delay.
                        capped = min(max_delay, base_delay * (2 ** attempt))
                        delay = random.uniform(0, capped)
                        logger.warning(
                            "Transient error in %s (attempt %d/%d), retrying in %.2fs: %s",
                            func.__name__, attempt + 1, max_retries, delay, e
                        )
                        connection.close()
                        time.sleep(delay)
                        continue
                    raise
        return wrapper
    return decorator
```

Apply the decorator to database-heavy functions:

```python
from myproject.retry import retry_on_transient

@retry_on_transient(max_retries=3, base_delay=2)
def process_order(order_id):
    """Process an order with automatic retry on transient failures."""
    order = Order.objects.select_for_update().get(id=order_id)
    order.status = "processing"
    order.save()
    return order
```

## Retry with transactions

When a transient error occurs inside a transaction, the entire transaction is rolled back by the server. Retry the complete transaction, not just the failed statement:

```python
from django.db import transaction

@retry_on_transient(max_retries=3)
def transfer_funds(from_account_id, to_account_id, amount):
    """Transfer funds between accounts with retry."""
    with transaction.atomic():
        from_account = Account.objects.select_for_update().get(id=from_account_id)
        to_account = Account.objects.select_for_update().get(id=to_account_id)

        from_account.balance -= amount
        to_account.balance += amount

        from_account.save()
        to_account.save()
```

> [!CAUTION]  
> Don't retry inside `transaction.atomic()`. The retry decorator must wrap the entire `atomic()` block so that each retry starts a fresh transaction.

### Statement-level errors

The error list in the previous section covers connection-level failures. Two other errors are commonly retried at the statement level:

- **1205**: The session was chosen as the *deadlock victim*. Rerun the transaction.
- **1222**: A lock-request timeout was exceeded. Rerun the transaction, or increase `LOCK_TIMEOUT` for the session if the default value is too aggressive.

`ConnectRetryCount` retries broken connections, so it doesn't apply to these statement-level errors. Handle them with the same decorator pattern by adding `"1205"` and `"1222"` to `TRANSIENT_ERROR_CODES` for transactions that are safe to rerun.

## CONN_MAX_AGE and stale connections

Django reuses database connections across requests when `CONN_MAX_AGE` is set. A long-lived connection might become stale if the server closes it (for example, during an Azure SQL scale operation or a firewall timeout).

Set `CONN_MAX_AGE` to balance reuse against staleness:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>.database.windows.net",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
        "CONN_MAX_AGE": 600,  # Close and reopen connections after 10 minutes
    },
}
```

- `CONN_MAX_AGE=0` (default): Close the connection at the end of each request. Safest but slowest.
- `CONN_MAX_AGE=600`: Reuse connections for 10 minutes. Good balance for most web applications.
- `CONN_MAX_AGE=None`: Keep connections open indefinitely. Use only with a retry mechanism for stale connections.

## CONN_HEALTH_CHECKS (Django 4.1 and later)

Django 4.1 introduced `CONN_HEALTH_CHECKS`, which validates a reused connection before each request. Enable it alongside `CONN_MAX_AGE` to detect stale connections automatically:

```python
DATABASES = {
    "default": {
        "ENGINE": "mssql",
        "NAME": "<your-database>",
        "HOST": "<your-server>.database.windows.net",
        "OPTIONS": {
            "driver": "ODBC Driver 18 for SQL Server",
        },
        "CONN_MAX_AGE": 600,
        "CONN_HEALTH_CHECKS": True,
    },
}
```

With health checks enabled, Django issues a lightweight validation query before reusing a connection. If the connection is broken, Django transparently opens a new one instead of raising an error.

## Best practices

- **Use exponential backoff with full jitter.** Double the cap each attempt, then sleep a random amount within `[0, cap]`. Jitter prevents many clients from retrying in lock-step during a regional outage, which can otherwise turn a brief failure into sustained overload. Cap the per-attempt sleep (for example, 30 seconds) so total recovery time stays bounded.
- **Set a retry ceiling.** Three retries with exponential backoff is a reasonable default. More than five retries usually indicates a nontransient problem.
- **Close the connection before retrying.** Call `connection.close()` so Django opens a fresh connection on the next attempt.
- **Log every retry.** Retries that succeed silently can hide performance problems. Log at `WARNING` level so you can track frequency.
- **Don't retry nontransient errors.** Authentication failures, permission errors, and syntax errors don't benefit from retries.
- **Retry the entire transaction.** Wrap `transaction.atomic()` inside the retry logic, not the other way around.
- **Enable `CONN_HEALTH_CHECKS`** (Django 4.1 and later) for web applications that use `CONN_MAX_AGE`.

## Related content

- [Connection options for mssql-django](connection-options.md)
- [Connection pooling in mssql-django](connection-pooling.md)
- [Transaction management in mssql-django](transactions.md)
- [Performance tuning for mssql-django](performance-tuning.md)
- [Troubleshoot mssql-django](troubleshooting.md)
