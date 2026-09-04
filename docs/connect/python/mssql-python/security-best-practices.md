---
title: Security Best Practices for mssql-python Applications
description: Learn security best practices for building SQL Server applications with the mssql-python driver, including authentication, parameterized queries, and data protection.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# Security best practices for mssql-python applications

Secure your mssql-python applications by following these best practices for authentication, parameterized queries, and data protection.

Start with passwordless authentication whenever you can. Treat local `.env` files and SQL passwords as temporary development aids, and move secrets into managed identities or a secret store before code reaches a shared environment.

## Authentication security

### Use Microsoft Entra authentication over SQL authentication

Microsoft Entra authentication eliminates stored passwords and supports managed identities. Prefer it over SQL authentication in all environments.

For workloads hosted in Azure, use a managed identity with `ActiveDirectoryMSI`. It needs no stored secrets and connects without walking a credential chain:

```python
import mssql_python

def connect_with_managed_identity():
    return mssql_python.connect(
        "Server=<server>.database.windows.net;"
        "Database=<database>;"
        "Authentication=ActiveDirectoryMSI;"
        "Encrypt=yes;"
    )
```

For local development, use `ActiveDirectoryDefault`, which picks up your Azure CLI or other developer credentials automatically. Avoid it in production, because `DefaultAzureCredential` tries each credential provider in order on the first connection, which adds latency that production workloads don't need:

```python
import mssql_python

def connect_with_default_credential():
    return mssql_python.connect(
        "Server=<server>.database.windows.net;"
        "Database=<database>;"
        "Authentication=ActiveDirectoryDefault;"
        "Encrypt=yes;"
    )
```

**Avoid:** SQL authentication stores credentials in code/configuration and is vulnerable to leaks.

```python

conn = mssql_python.connect("Server=...;UID=user;PWD=password")
```

### Never hardcode credentials

Use environment variables for local development only. In shared environments, prefer passwordless authentication. When a legacy SQL authentication flow is unavoidable, retrieve the secret at runtime from a secret store instead of checking a full connection string into source control.

The following approach hardcodes credentials and should never be used:

```python
conn_str = "Server=<server>;UID=<login>;PWD=<password>"
```

For local development, read credentials from environment variables:

```python
import os

conn_str = (
    f"Server={os.environ['DB_SERVER']};"
    f"Database={os.environ['DB_NAME']};"
)
```

For shared environments that still need a secret, retrieve it at runtime from Azure Key Vault:

```python
from azure.keyvault.secrets import SecretClient
from azure.identity import DefaultAzureCredential

def get_connection_string():
    credential = DefaultAzureCredential()
    secret_client = SecretClient(
        vault_url="https://myvault.vault.azure.net/",
        credential=credential
    )
    return secret_client.get_secret("db-connection-string").value
```

## SQL injection prevention

### Always use parameterized queries

Parameterized queries prevent SQL injection by separating user input from the query structure. Always parameterize user input.

The following string-formatted query is vulnerable to SQL injection. Never build queries this way:

```python
user_input = "'; DROP TABLE Person.Person; --"
cursor.execute(f"SELECT * FROM Person.Person WHERE FirstName = '{user_input}'")
```

The following parameterized query is safe, because the driver sends the value separately from the query text:

```python
user_input = "'; DROP TABLE Person.Person; --"
cursor.execute(
    "SELECT * FROM Person.Person WHERE FirstName = %(name)s",
    {"name": user_input}
)
```

### Parameterize all query components

You can't directly parameterize table and column names. Interpolating them from user input makes your app vulnerable to SQL injection:

```python
table = user_input
cursor.execute(f"SELECT * FROM {table}")
```

Instead, validate dynamic identifiers against a allow list of permitted values, and parameterize the remaining values.

```python
ALLOWED_TABLES = {"Person.Person", "Production.Product", "Sales.SalesOrderHeader"}

def query_table(cursor, table_name: str, conditions: dict):
    """Query with validated table name."""
    if table_name not in ALLOWED_TABLES:
        raise ValueError(f"Invalid table: {table_name}")
    
    # Table name is safe, parameters are parameterized
    where_clauses = [f"{k} = %({k})s" for k in conditions.keys()]
    query = f"SELECT * FROM {table_name} WHERE {' AND '.join(where_clauses)}"
    
    cursor.execute(query, conditions)
    return cursor.fetchall()
```

### Validate and sanitize input

When you build dynamic SQL with identifiers, validate each value against a strict pattern before using it:

```python
import re

def validate_identifier(value: str) -> bool:
    """Validate SQL identifier (table/column name)."""
    # Only allow alphanumeric and underscore
    return bool(re.match(r'^[a-zA-Z_][a-zA-Z0-9_]*$', value))

def safe_order_by(cursor, table: str, order_column: str, direction: str):
    """Order by with validation."""
    if not validate_identifier(order_column):
        raise ValueError(f"Invalid column name: {order_column}")
    
    if direction.upper() not in ("ASC", "DESC"):
        raise ValueError(f"Invalid direction: {direction}")
    
    cursor.execute(f"""
        SELECT * FROM {table}
        ORDER BY {order_column} {direction.upper()}
    """)
```

### Use stored procedures for complex operations

Stored procedures reduce the SQL surface area exposed to application code:

```python
employee_id = 5
cursor.execute("""
    EXECUTE dbo.uspGetEmployeeManagers @BusinessEntityID = %(id)s
""", {"id": employee_id})
```

## Connection security

### Require encryption

Always encrypt connections. Azure SQL enforces encryption by default. For on-premises SQL Server, set `Encrypt=yes` explicitly:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Encrypt=yes;"
    "TrustServerCertificate=no"
)
```

### Use TDS 8.0 strict mode for highest security

TDS 8.0 provides:

- TLS 1.3 from connection start
- Certificate validation required
- No fallback to older protocols

```python
conn = mssql_python.connect(
    "Server=tcp:<server>.database.windows.net,1433;"
    "Database=<database>;"
    "Encrypt=strict"
)
```

### Validate server certificates

Always validate the server certificate in production to prevent adversary-in-the-middle attacks. Don't set `TrustServerCertificate=yes`, because it bypasses validation. Instead, set `TrustServerCertificate=no` to validate against the CA certificates, and set `HostNameInCertificate` to verify the hostname:

```python
conn = mssql_python.connect(
    "Server=<server>;"
    "Database=<database>;"
    "Encrypt=yes;"
    "TrustServerCertificate=no;"
    "HostNameInCertificate=<server>.domain.com"
)
```

## Data protection

### Protect sensitive data at the server level

You can't currently configure [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md) through mssql-python connection string keywords. [Dynamic data masking](../../../relational-databases/security/dynamic-data-masking.md) and [row-level security](../../../relational-databases/security/row-level-security.md) can protect sensitive columns, though neither provides the same level of protection.

```python
employee_id = 1
cursor.execute("""
    SELECT NationalIDNumber, LoginID
    FROM HumanResources.Employee
    WHERE BusinessEntityID = %(id)s
""", {"id": employee_id})

row = cursor.fetchone()
```

### Protect data in transit

- Use `Encrypt=yes` in connection strings.
- Use VPN or private endpoints for on-premises connections.
- Use [Azure Private Link](/azure/azure-sql/database/private-endpoint-overview) for Azure SQL.

## Principle of least privilege

### Use minimal database permissions

Application accounts should have minimal permissions. Don't use `sa` or `db_owner` for application connections.

- **Read-only reporting**: `GRANT SELECT ON SCHEMA::dbo TO ReportingApp;`
- **Specific table access**: `GRANT SELECT, INSERT, UPDATE ON Orders TO OrderProcessor;`
- **Stored procedure only**: `GRANT EXECUTE ON ProcessOrder TO OrderProcessor;`

### Use different accounts for different operations

Back each privilege level with its own identity so that a read path can't perform writes. This example uses two user-assigned managed identities, one granted read-only access and one granted write access, selected by client ID:

```python
readonly_client_id = os.environ["READONLY_IDENTITY_CLIENT_ID"]
readwrite_client_id = os.environ["READWRITE_IDENTITY_CLIENT_ID"]

def get_readonly_connection():
    """Connection for read-only operations."""
    return mssql_python.connect(
        f"Server={server};Database={db};"
        f"Authentication=ActiveDirectoryMSI;UID={readonly_client_id};"
        f"Encrypt=yes;ApplicationIntent=ReadOnly;"
    )

def get_readwrite_connection():
    """Connection for write operations."""
    return mssql_python.connect(
        f"Server={server};Database={db};"
        f"Authentication=ActiveDirectoryMSI;UID={readwrite_client_id};"
        f"Encrypt=yes;"
    )
```

### Keep application role secrets out of source control

Application role passwords are still secrets. Store them in a vault or secret-injected environment variable, and rotate them with the same care as any other credential.

```python
def execute_with_role(cursor, role: str, query: str, params: dict):
    """Execute query with specific application role."""
    # Activate application role
    cursor.execute(
        "EXECUTE sp_setapprole @rolename = %(role)s, @password = %(pwd)s",
        {"role": role, "pwd": os.environ[f"ROLE_{role.upper()}_PWD"]}
    )
    
    try:
        cursor.execute(query, params)
        return cursor.fetchall()
    finally:
        # Reset to original context
        cursor.execute("EXECUTE sp_unsetapprole")
```

## Audit and logging

### Log security events

Don't log sensitive data, but do log security-relevant events like failed connections, permission errors, and suspicious queries.

```python
import logging

logger = logging.getLogger("db_security")

def secure_connect(connection_string: str):
    """Connect with security logging."""
    logger.info("Attempting database connection")
    
    try:
        conn = mssql_python.connect(connection_string)
        logger.info("Database connection established")
        return conn
    except mssql_python.OperationalError as e:
        logger.warning(f"Database connection failed: {type(e).__name__}")
        raise
```

### Audit sensitive operations

Configure database auditing for sensitive tables and operations. You can also implement application-level auditing for critical actions.

```python
def audit_data_access(cursor, user_id: str, action: str, resource: str):
    """Log data access for audit trail."""
    cursor.execute("""
        INSERT INTO AuditLog (UserID, Action, Resource, Timestamp, IPAddress)
        VALUES (%(user)s, %(action)s, %(resource)s, GETUTCDATE(), %(ip)s)
    """, {
        "user": user_id,
        "action": action,
        "resource": resource,
        "ip": get_client_ip()
    })
```

### Never log sensitive data

Don't include parameter values in log messages. Log the operation, not the data.

**Avoid** - leaks the value into the log:

```python
nid = "295847284"
logger.debug(f"Query: SELECT * FROM HumanResources.Employee WHERE NationalIDNumber = '{nid}'")
```

**Recommended** - logs intent without exposing values:

```python
logger.debug("Executing employee lookup query")
nid = "295847284"
cursor.execute("SELECT * FROM HumanResources.Employee WHERE NationalIDNumber = %(nid)s", {"nid": nid})
```

## Error handling

### Don't expose internal details

Log detailed errors internally but return generic messages to users to avoid leaking database structure:

```python
def safe_query(cursor, query: str, params: dict):
    """Execute query with safe error handling."""
    try:
        cursor.execute(query, params)
        return cursor.fetchall()
    except mssql_python.ProgrammingError as e:
        # Log full error internally
        logging.error(f"Query error: {e}")
        # Return generic error to user
        raise UserFacingError("An error occurred processing your request")
    except mssql_python.IntegrityError:
        raise UserFacingError("Invalid data provided")
```

### Sanitize error messages

Map database exceptions to user-friendly messages that don't reveal implementation details:

```python
class UserFacingError(Exception):
    """Exception safe to show to users."""
    pass

def handle_database_error(error: Exception) -> str:
    """Convert database errors to safe user messages."""
    if isinstance(error, mssql_python.IntegrityError):
        if "UNIQUE" in str(error):
            return "A record with this value already exists"
        if "FOREIGN KEY" in str(error):
            return "Referenced item not found"
    
    return "An error occurred. Please try again later."
```

## Security checklist

### Connection security checklist

- [ ] Use Microsoft Entra authentication when possible.
- [ ] Enable encryption (`Encrypt=yes`).
- [ ] Validate server certificates.
- [ ] Store credentials in a secure vault.
- [ ] Keep `.env` files local only, and inject secrets through the target platform in shared environments.
- [ ] Use TDS 8.0 strict mode for Azure SQL.

### Query security

- [ ] Always use parameterized queries.
- [ ] Validate dynamic identifiers.
- [ ] Use stored procedures for complex logic.
- [ ] Limit query result sizes.

### Data security

- [ ] Use server-level data protection (masking, row-level security).
- [ ] Use row-level security where appropriate.
- [ ] Mask sensitive data in logs.

### Access control

- [ ] Use principle of least privilege.
- [ ] Separate read/write accounts.
- [ ] Regularly audit permissions.
- [ ] Implement connection timeouts.

### Monitoring

- [ ] Log security events.
- [ ] Monitor for anomalies.
- [ ] Set up alerts for failures.
- [ ] Conduct regular security reviews.

## Related content

- [Encryption and TLS with mssql-python](encryption-certificates.md)
- [Microsoft Entra authentication with mssql-python](entra-authentication.md)
- [Container and local development with mssql-python](container-local-development.md)
- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
- [Security for SQL Server Database Engine and Azure SQL Database](../../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)
