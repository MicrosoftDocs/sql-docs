---
title: Retry Logic and Connection Resiliency with mssql-python
description: Learn how to implement robust retry logic and handle transient failures when connecting to SQL Server and Azure SQL using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 08/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Retry logic and connection resiliency with mssql-python

Transient failures are temporary errors that can occur when connecting to SQL Server and Azure SQL through the mssql-python driver. These errors often resolve on their own:

- Network connectivity blips.
- Server resource constraints.
- Azure SQL throttling.
- Failover events.

Implementing retry logic improves application reliability, especially for cloud-hosted databases.

Don't use retries to hide configuration or coding mistakes. A missing database, bad credentials, or exhausted connection pool needs a fix, not another attempt.

## Identify transient errors

mssql-python doesn't expose the SQL Server engine error number as an attribute on exceptions. Instead, the driver maps SQLSTATE codes to a fixed set of PEP 249 exception subclasses (`OperationalError`, `ProgrammingError`, and so on) and to standardized English text in the `driver_error` attribute. Use that combination as the basis for transient classification.

### Reliable transient signals

The following SQLSTATE values come through as `OperationalError` and indicate a condition worth retrying. The right-hand column shows the exact `driver_error` text set by the driver:

| SQLSTATE | `driver_error` text | Condition |
| --- | --- | --- |
| `HYT00` | `Timeout expired` | Statement-level timeout. |
| `HYT01` | `Connection timeout expired` | Connect-time timeout. |
| `08001` | `Client unable to establish connection` | Couldn't open a connection. |
| `08S01` | `Communication link failure` | Network drop, server reset, TCP failure. |
| `08007` | `Connection failure during transaction` | Connection lost mid-transaction. |
| `40001` | `Serialization failure` | Deadlock victim. |
| `40003` | `Statement completion unknown` | Indeterminate transaction state. |

```python
import mssql_python


TRANSIENT_DRIVER_ERRORS = frozenset({
    "Timeout expired",
    "Connection timeout expired",
    "Client unable to establish connection",
    "Communication link failure",
    "Connection failure during transaction",
    "Serialization failure",
    "Statement completion unknown",
})


def is_transient_error(error: BaseException) -> bool:
    """Return True if the exception represents a retryable transient failure.

    Classification is based on the driver's PEP 249 exception subclass and
    on the standardized `driver_error` text that mssql-python sets from
    the SQLSTATE returned by the server.
    """
    if isinstance(error, mssql_python.OperationalError):
        return getattr(error, "driver_error", "") in TRANSIENT_DRIVER_ERRORS
    return False
```

### Azure SQL throttling (best-effort)

Azure SQL throttling errors (`40197`, `40501`, `40613`, `49918`, `49919`, `49920`, and related codes) typically come through with SQLSTATE `42000`, which mssql-python maps to `ProgrammingError`. The engine error number isn't surfaced as an attribute, so the only signal is the server message text in the `ddbc_error` attribute.

If your workload runs against Azure SQL and you need to retry throttling, scan `ddbc_error` for the known number. This is best-effort because the format of the server-side text isn't a stable contract:

```python
import re

# Azure SQL throttling and reconfiguration error numbers.
AZURE_THROTTLING_ERRORS = frozenset({
    40197, 40501, 40540, 40613, 40680, 49918, 49919, 49920, 10928, 10929,
})

_ERROR_NUMBER_RE = re.compile(r"\b(?:Error|Msg)\s+(\d+)\b")


def is_azure_throttling(error: BaseException) -> bool:
    """Best-effort detection of Azure SQL throttling in ProgrammingError text."""
    if not isinstance(error, mssql_python.ProgrammingError):
        return False
    ddbc_text = getattr(error, "ddbc_error", "") or ""
    return any(int(m) in AZURE_THROTTLING_ERRORS for m in _ERROR_NUMBER_RE.findall(ddbc_text))


def is_retryable(error: BaseException) -> bool:
    return is_transient_error(error) or is_azure_throttling(error)
```

### What not to retry

Examples of errors that should fail fast instead of retrying include invalid credentials (`OperationalError` with driver text `Invalid authorization specification`), a missing or inaccessible database, syntax errors (`ProgrammingError`), missing objects, and connection pool exhaustion. The `is_transient_error` function above excludes all of these by construction.

## Basic retry decorator

### Simple retry with fixed delay

A decorator that retries the wrapped function a fixed number of times with a constant delay:

```python
import time
import functools
import mssql_python

def retry_on_failure(max_retries: int = 3, delay: float = 1.0):
    """Decorator to retry database operations on transient failures."""
    def decorator(func):
        @functools.wraps(func)
        def wrapper(*args, **kwargs):
            last_exception = None
            for attempt in range(max_retries + 1):
                try:
                    return func(*args, **kwargs)
                except mssql_python.Error as e:
                    last_exception = e
                    if not is_transient_error(e) or attempt == max_retries:
                        raise
                    print(f"Attempt {attempt + 1} failed: {e}. Retrying in {delay}s...")
                    time.sleep(delay)
            raise last_exception
        return wrapper
    return decorator

# Usage
@retry_on_failure(max_retries=3, delay=2.0)
def get_user(cursor, user_id: int):
    cursor.execute("SELECT * FROM Person.Person WHERE BusinessEntityID = %(id)s", {"id": user_id})
    return cursor.fetchone()
```

### Exponential backoff

Increase the delay between retries exponentially with optional jitter to spread out concurrent retries:

```python
import time
import random

def retry_with_backoff(max_retries: int = 5, 
                       base_delay: float = 1.0,
                       max_delay: float = 30.0,
                       jitter: bool = True):
    """Retry with exponential backoff and optional jitter."""
    def decorator(func):
        @functools.wraps(func)
        def wrapper(*args, **kwargs):
            last_exception = None
            for attempt in range(max_retries + 1):
                try:
                    return func(*args, **kwargs)
                except mssql_python.Error as e:
                    last_exception = e
                    if not is_transient_error(e) or attempt == max_retries:
                        raise
                    
                    # Calculate delay with exponential backoff
                    delay = min(base_delay * (2 ** attempt), max_delay)
                    if jitter:
                        delay = delay * (0.5 + random.random())
                    
                    print(f"Attempt {attempt + 1} failed. Retrying in {delay:.2f}s...")
                    time.sleep(delay)
            raise last_exception
        return wrapper
    return decorator

@retry_with_backoff(max_retries=5, base_delay=1.0, max_delay=30.0)
def execute_query(cursor, query: str, params: dict):
    cursor.execute(query, params)
    return cursor.fetchall()
```

## Connection retry class

### Resilient connection manager

A connection wrapper that handles both retry and automatic reconnection:

```python
import mssql_python
import time
import logging

# This example uses is_transient_error from the "Identify transient errors"
# section earlier in this article. Include that helper in your module.

# Configure logging so the retry and reconnect messages are visible
logging.basicConfig(level=logging.INFO)

class ResilientConnection:
    """Connection wrapper with automatic retry and reconnection."""
    
    def __init__(self, connection_string: str, max_retries: int = 5,
                 base_delay: float = 1.0, max_delay: float = 60.0):
        self.connection_string = connection_string
        self.max_retries = max_retries
        self.base_delay = base_delay
        self.max_delay = max_delay
        self._conn = None
        self._logger = logging.getLogger(__name__)
    
    def _connect(self) -> mssql_python.Connection:
        """Establish connection with retry logic."""
        last_exception = None
        
        for attempt in range(self.max_retries + 1):
            try:
                self._logger.debug(f"Connection attempt {attempt + 1}")
                return mssql_python.connect(self.connection_string)
            except mssql_python.Error as e:
                last_exception = e
                if not is_transient_error(e) or attempt == self.max_retries:
                    self._logger.error(f"Connection failed: {e}")
                    raise
                
                delay = min(self.base_delay * (2 ** attempt), self.max_delay)
                self._logger.warning(f"Connection attempt {attempt + 1} failed. "
                                   f"Retrying in {delay:.1f}s...")
                time.sleep(delay)
        
        raise last_exception
    
    @property
    def connection(self) -> mssql_python.Connection:
        """Get or create connection."""
        if self._conn is None:
            self._conn = self._connect()
        return self._conn
    
    def execute(self, query: str, params: dict = None):
        """Execute query with automatic retry and reconnection."""
        return self._execute_with_retry(
            lambda c: self._do_execute(c, query, params)
        )
    
    def _do_execute(self, cursor, query: str, params: dict):
        cursor.execute(query, params or {})
        return cursor.fetchall()
    
    def _execute_with_retry(self, operation):
        """Execute an operation with retry logic."""
        last_exception = None
        
        for attempt in range(self.max_retries + 1):
            try:
                cursor = self.connection.cursor()
                return operation(cursor)
            except mssql_python.Error as e:
                last_exception = e
                
                if not is_transient_error(e):
                    raise
                
                if attempt == self.max_retries:
                    raise
                
                # Try to reconnect
                self._logger.warning(f"Operation failed. Reconnecting...")
                self._close()
                
                delay = min(self.base_delay * (2 ** attempt), self.max_delay)
                time.sleep(delay)
        
        raise last_exception
    
    def _close(self):
        """Close connection."""
        if self._conn:
            try:
                self._conn.close()
            except:
                pass
            self._conn = None
    
    def close(self):
        """Public close method."""
        self._close()
    
    def __enter__(self):
        return self
    
    def __exit__(self, exc_type, exc_val, exc_tb):
        self.close()
        return False

# Usage
with ResilientConnection(connection_string) as db:
    users = db.execute("SELECT * FROM Person.Person WHERE EmailPromotion = %(promo)s", 
                       {"promo": 1})
    print(f"Retrieved {len(users)} rows")
```

## Azure SQL specific handling

### Handle Azure throttling

Azure SQL throttling errors need longer delays and more retries than standard transient errors. Reuse `is_azure_throttling` from [Identify transient errors](#identify-transient-errors):

```python
def execute_with_throttle_handling(cursor, query: str, params: dict,
                                   max_retries: int = 10,
                                   base_delay: float = 5.0):
    """Execute with extended retry for Azure SQL throttling."""
    for attempt in range(max_retries + 1):
        try:
            cursor.execute(query, params)
            return cursor.fetchall()
        except mssql_python.Error as e:
            if is_azure_throttling(e):
                if attempt < max_retries:
                    # Longer delays for throttling
                    delay = base_delay * (2 ** min(attempt, 4))  # Cap at 80s
                    print(f"Throttled. Waiting {delay}s before retry...")
                    time.sleep(delay)
                    continue
            raise
```

### Handle failover

Reconnect and retry when Azure SQL or availability group failover interrupts a connection:

```python
def execute_with_failover_retry(connect, query: str, params: dict,
                                max_retries: int = 3,
                                recovery_delay: float = 10.0):
    """Reconnect and retry during Azure SQL failover scenarios."""
    failover_numbers = frozenset({40613, 40197, 40540})
    last_exception = None

    for attempt in range(max_retries + 1):
        conn = None
        try:
            conn = connect()
            cursor = conn.cursor()
            cursor.execute(query, params)
            return cursor.fetchall()
        except mssql_python.Error as e:
            last_exception = e

            # Failover surfaces either as a transient OperationalError or as
            # a ProgrammingError whose ddbc_error text contains the engine
            # error number. Treat both as recoverable.
            ddbc_text = getattr(e, "ddbc_error", "") or ""
            is_failover = is_transient_error(e) or any(
                int(m) in failover_numbers for m in _ERROR_NUMBER_RE.findall(ddbc_text)
            )

            if is_failover and attempt < max_retries:
                print(f"Failover detected. Reconnecting in {recovery_delay}s...")
                if conn is not None:
                    try:
                        conn.close()
                    except mssql_python.Error:
                        pass
                time.sleep(recovery_delay)
                continue
            raise

    raise last_exception


# Usage
connection_string = (
    "Server=tcp:<server>.database.windows.net,1433;"
    "Database=AdventureWorks2025;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;TrustServerCertificate=no"
)

rows = execute_with_failover_retry(
    lambda: mssql_python.connect(connection_string),
    "SELECT TOP 10 ProductID, Name FROM Production.Product WHERE Color = %(color)s",
    {"color": "Silver"}
)
```

## Deadlock handling

### Retry on deadlock

Deadlocks (error 1205) are transient. Retry with a short random delay to break the deadlock cycle. Retrying handles the immediate failure, but recurring deadlocks indicate a design problem that you should investigate server-side. For guidance on analyzing and resolving the root cause, see [Deadlock errors](troubleshooting.md#deadlock-errors).

```python
def execute_with_deadlock_retry(cursor, query: str, params: dict,
                                max_retries: int = 3):
    """Automatically retry deadlocked transactions.

    Deadlocks (SQL Server error 1205) surface as OperationalError with
    driver_error == "Serialization failure" (SQLSTATE 40001).
    """
    for attempt in range(max_retries + 1):
        try:
            cursor.execute(query, params)
            return cursor.fetchall()
        except mssql_python.OperationalError as e:
            if getattr(e, "driver_error", "") == "Serialization failure":
                if attempt < max_retries:
                    delay = random.uniform(0.1, 0.5) * (attempt + 1)
                    print(f"Deadlock detected. Retry {attempt + 1} in {delay:.2f}s")
                    time.sleep(delay)
                    continue
            raise

# Usage in transaction
conn.autocommit = False
try:
    cursor = conn.cursor()
    rows = execute_with_deadlock_retry(
        cursor,
        "SELECT TOP 5 Name, ListPrice FROM Production.Product WHERE ListPrice > %(price)s",
        {"price": 100}
    )
    conn.commit()
except Exception:
    conn.rollback()
    raise
```

## Structured retry with configuration

### Retry policy class

Encapsulate retry configuration in a dataclass for reuse across different operations:

```python
from dataclasses import dataclass, field
from typing import FrozenSet
import time
import random

# This example uses TRANSIENT_DRIVER_ERRORS from the "Identify transient errors"
# section earlier in this article. Include that allowlist in your module.

@dataclass
class RetryPolicy:
    """Configuration for retry behavior."""
    max_retries: int = 3
    base_delay: float = 1.0
    max_delay: float = 30.0
    exponential_base: float = 2.0
    jitter: bool = True
    transient_driver_errors: FrozenSet[str] = field(default_factory=lambda: TRANSIENT_DRIVER_ERRORS)

    def get_delay(self, attempt: int) -> float:
        """Calculate delay for given attempt number."""
        delay = min(
            self.base_delay * (self.exponential_base ** attempt),
            self.max_delay,
        )
        if self.jitter:
            delay *= (0.5 + random.random())
        return delay

    def should_retry(self, error: BaseException, attempt: int) -> bool:
        """Determine if operation should be retried."""
        if attempt >= self.max_retries:
            return False
        if isinstance(error, mssql_python.OperationalError):
            return getattr(error, "driver_error", "") in self.transient_driver_errors
        return False

def execute_with_policy(cursor, query: str, params: dict,
                        policy: RetryPolicy = None):
    """Execute query with configurable retry policy."""
    policy = policy or RetryPolicy()
    last_exception = None

    for attempt in range(policy.max_retries + 1):
        try:
            cursor.execute(query, params)
            return cursor.fetchall()
        except mssql_python.Error as e:
            last_exception = e
            if not policy.should_retry(e, attempt):
                raise

            delay = policy.get_delay(attempt)
            time.sleep(delay)

    raise last_exception

# Usage with custom policy
aggressive_retry = RetryPolicy(max_retries=10, base_delay=0.5, max_delay=60.0)
conservative_retry = RetryPolicy(max_retries=2, base_delay=5.0, max_delay=10.0)

results = execute_with_policy(cursor, query, params, aggressive_retry)
```

## Circuit breaker pattern

Prevent cascading failures by tracking consecutive errors and temporarily blocking calls when a threshold is reached:

```python
import time
from enum import Enum
from threading import Lock

class CircuitState(Enum):
    CLOSED = "closed"      # Normal operation
    OPEN = "open"          # Failing, reject all calls
    HALF_OPEN = "half_open"  # Testing if service recovered

class CircuitBreaker:
    """Circuit breaker to prevent cascading failures."""
    
    def __init__(self, failure_threshold: int = 5,
                 recovery_timeout: float = 30.0):
        self.failure_threshold = failure_threshold
        self.recovery_timeout = recovery_timeout
        self.state = CircuitState.CLOSED
        self.failure_count = 0
        self.last_failure_time = None
        self._lock = Lock()
    
    def can_execute(self) -> bool:
        """Check if circuit allows execution."""
        with self._lock:
            if self.state == CircuitState.CLOSED:
                return True
            
            if self.state == CircuitState.OPEN:
                # Check if recovery timeout has passed
                if time.time() - self.last_failure_time > self.recovery_timeout:
                    self.state = CircuitState.HALF_OPEN
                    return True
                return False
            
            # HALF_OPEN: allow one test request
            return True
    
    def record_success(self):
        """Record successful operation."""
        with self._lock:
            self.failure_count = 0
            self.state = CircuitState.CLOSED
    
    def record_failure(self):
        """Record failed operation."""
        with self._lock:
            self.failure_count += 1
            self.last_failure_time = time.time()
            
            if self.failure_count >= self.failure_threshold:
                self.state = CircuitState.OPEN

# Usage
circuit = CircuitBreaker(failure_threshold=5, recovery_timeout=30.0)

def execute_with_circuit_breaker(cursor, query: str, params: dict):
    if not circuit.can_execute():
        raise Exception("Circuit breaker is open")
    
    try:
        cursor.execute(query, params)
        result = cursor.fetchall()
        circuit.record_success()
        return result
    except mssql_python.Error as e:
        if is_transient_error(e):
            circuit.record_failure()
        raise
```

## Don't retry configuration errors

Not every error is transient. Retrying a configuration or coding error wastes time and can mask the real problem. Only retry errors that might resolve on their own. Because mssql-python doesn't expose the engine error number as an attribute, classify by exception subclass plus the `driver_error` text.

**Never retry these** (fix the code or configuration instead):

| Condition | Exception type | `driver_error` text | Fix |
| --- | --- | --- | --- |
| Invalid object name (engine 208) | `ProgrammingError` | `Base table or view not found` | The table doesn't exist. Fix the query or create the table. |
| Invalid column name (engine 207) | `ProgrammingError` | `Column not found` | The column doesn't exist. Check the schema. |
| Incorrect syntax (engine 102) | `ProgrammingError` | `Syntax error or access violation` | Fix the query. |
| Login failed (engine 18456) | `OperationalError` | `Invalid authorization specification` | Wrong credentials. Fix the connection string. |
| Cannot open database (engine 4060) | `OperationalError` | `Server rejected the connection` | Database doesn't exist or isn't accessible to the login. Fix the target or permissions. |
| Connection pool exhaustion | `OperationalError` | (varies) | Increase pool capacity, release connections promptly, or reduce concurrency. |
| `ConnectionStringParseError` | Standalone | n/a | Typo in connection string keyword. Fix the string. |
| Unsupported feature | `NotSupportedError` | `Optional feature not implemented` | Use an alternative approach. |

**Always retry these** (they resolve on their own):

| Condition | Exception type | `driver_error` text |
| --- | --- | --- |
| Statement timeout | `OperationalError` | `Timeout expired` |
| Connect timeout | `OperationalError` | `Connection timeout expired` |
| Couldn't open connection | `OperationalError` | `Client unable to establish connection` |
| Network drop | `OperationalError` | `Communication link failure` |
| Connection dropped mid-transaction | `OperationalError` | `Connection failure during transaction` |
| Deadlock victim (engine 1205) | `OperationalError` | `Serialization failure` |
| Indeterminate transaction state | `OperationalError` | `Statement completion unknown` |
| Azure SQL throttling (40197, 40501, 40613, 49918&ndash;49920) | `ProgrammingError` | `Syntax error or access violation` (engine number is only in `ddbc_error`; use `is_azure_throttling`) |

## Related content

- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
- [Connection pooling with mssql-python](connection-pooling.md)
- [Troubleshoot mssql-python](troubleshooting.md)
- [Troubleshoot transient connection errors](/azure/azure-sql/database/troubleshoot-common-connectivity-issues)
