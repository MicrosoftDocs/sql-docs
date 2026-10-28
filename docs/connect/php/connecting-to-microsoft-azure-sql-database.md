---
title: "Connect to Azure SQL and Fabric SQL from PHP"
description: "Learn how to connect to Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Microsoft Fabric, and Warehouse in Microsoft Fabric with the PHP drivers, and why passwordless Microsoft Entra authentication is the recommended default."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 10/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Connecting to Microsoft Azure SQL Database

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The SQLSRV and PDO_SQLSRV drivers connect to the following Azure and Fabric SQL targets with a fully qualified server name and the connection options in [Connection options](connection-options.md). SQLSRV takes an options array; PDO_SQLSRV takes a data source name (DSN), separate credential arguments when needed, and PDO attributes.

| Target | Server name form | Notes |
|-|-|-|
| Azure SQL Database | `<server>.database.windows.net` | |
| Azure SQL Managed Instance | Your instance's public or private endpoint | Requires network connectivity through a virtual network, virtual private network (VPN), or public endpoint. |
| Azure Synapse Analytics (dedicated SQL pool in a workspace) | `<workspace>.sql.azuresynapse.net` | Supported Transact-SQL (T-SQL) statements differ from SQL Server. For supported syntax, see the [Synapse SQL documentation](/azure/synapse-analytics/sql/overview-architecture). |
| SQL database in Microsoft Fabric | Copy the server and database from the database item's **Settings** > **Connection strings** | Use the SQL database connection, not its SQL analytics endpoint. Microsoft Entra authentication is required. For more information, see [Connect to SQL database in Fabric](/fabric/database/sql/connect). |
| Warehouse in Microsoft Fabric | Copy the warehouse's SQL connection string from the Fabric portal | Set `Database` to the warehouse name and `MultipleActiveResultSets` to `false`. Microsoft Entra authentication is required. For more information, see [Warehouse connectivity](/fabric/data-warehouse/connectivity). |

Both PHP extensions enable Multiple Active Result Sets (MARS) by default. Warehouse doesn't support MARS, so explicitly disable it in the SQLSRV options array or PDO_SQLSRV DSN. SQL database in Fabric and Warehouse are separate products; don't assume that a feature supported by one is supported by the other.

## Prepare the endpoint, identity, and PHP host

Your PHP process needs network access to the SQL endpoint, a supported way to sign in, and permission to open the selected database. Check each requirement separately. For example, opening a firewall port doesn't grant database access.

1. Install the PHP extension and Microsoft ODBC Driver on the machine or container that runs PHP. Verify the extension in that process, not only in a development terminal. For more information, see [Loading the drivers](loading-the-php-sql-driver.md).
1. Copy the server and database from the target's connection details. Don't include an HTTPS URL scheme. Use the service-supplied host name so certificate validation checks the intended endpoint. An Azure SQL Managed Instance public endpoint can include a nondefault port; retain the supplied port.
1. Configure the service's network access for your PHP host. For a private endpoint, confirm private name resolution and a route from the application network. A public firewall rule doesn't provide a route to a private address.
1. Choose the identity used by the PHP process. For an Azure-hosted application, enable its managed identity and identify whether it is system-assigned or user-assigned. A developer's Azure CLI sign-in isn't the application's managed identity.
1. Have a database administrator grant that identity access to the selected database and the minimum permissions needed by the application. For Warehouse, also check Fabric item/workspace access. Azure resource-management permissions don't by themselves grant permission to query application tables.
1. Set `SQL_SERVER` and `SQL_DATABASE` in the application's configuration. Don't store a password for the managed-identity examples.

The examples verify a connection without creating tables. Passing this check doesn't prove the identity can read or write your application schema. Test the actual operations separately under the same identity.

## Use passwordless Microsoft Entra authentication

Use a managed identity when your hosting environment exposes a managed-identity endpoint and the SQL target grants that identity access. The following complete programs use a system-assigned managed identity. They aren't workstation examples. Run them on the configured Azure host, with `SQL_SERVER` and `SQL_DATABASE` available to its PHP process.

Both programs disable MARS, so the connection setting is also suitable when selecting Warehouse. They use one active statement at a time. For identity setup and other authentication modes, see [Connect using Microsoft Entra authentication](azure-active-directory.md).

Set `SQL_DATABASE` to the actual database name. Each program creates a separate escaped value for the connection by doubling closing braces and enclosing the name in braces. Escaping keeps special characters in the database name from changing how the driver reads connection settings. The programs retain the original name to check the result of `DB_NAME()`.

### SQLSRV

Save this program as `connect-azure-sqlsrv.php`, and run `php connect-azure-sqlsrv.php` on the configured host.

```php
<?php
$server = getenv('SQL_SERVER');
$database = getenv('SQL_DATABASE');
if ($server === false || $server === '' || $database === false || $database === '') {
    throw new RuntimeException('Set SQL_SERVER and SQL_DATABASE.');
}
$databaseValue = '{' . str_replace('}', '}}', $database) . '}';

$conn = sqlsrv_connect($server, [
    'Database' => $databaseValue,
    'Authentication' => 'ActiveDirectoryMsi',
    'Encrypt' => true,
    'TrustServerCertificate' => false,
    'LoginTimeout' => 30,
    'MultipleActiveResultSets' => false,
    'CharacterSet' => 'UTF-8',
]);
if ($conn === false) {
    throw new RuntimeException(print_r(sqlsrv_errors(), true));
}

$stmt = null;
try {
    $stmt = sqlsrv_query(
        $conn,
        'SELECT DB_NAME() AS DatabaseName, CAST(1 AS int) AS Connected',
        [],
        ['QueryTimeout' => 30]
    );
    if ($stmt === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    $row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
    if ($row === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    if ($row !== ['DatabaseName' => $database, 'Connected' => 1]) {
        throw new RuntimeException('Unexpected database or connection result.');
    }
    echo 'Connected to ', $row['DatabaseName'], '.', PHP_EOL;
} finally {
    if (is_resource($stmt)) {
        sqlsrv_free_stmt($stmt);
    }
    sqlsrv_close($conn);
}
```

### PDO_SQLSRV

Save this separate program as `connect-azure-pdo.php`, and run `php connect-azure-pdo.php` on the configured host. It sets `PDO::SQLSRV_ATTR_FETCHES_NUMERIC_TYPE` so the **int** result is PHP `int`. Without it, PDO_SQLSRV returns this column as a string by default. PDO encoding is an attribute, not the SQLSRV `CharacterSet` option.

```php
<?php
$server = getenv('SQL_SERVER');
$database = getenv('SQL_DATABASE');
if ($server === false || $server === '' || $database === false || $database === '') {
    throw new RuntimeException('Set SQL_SERVER and SQL_DATABASE.');
}
$databaseValue = '{' . str_replace('}', '}}', $database) . '}';
$pdo = new PDO(
    "sqlsrv:Server=$server;Database=$databaseValue;Authentication=ActiveDirectoryMsi;"
    . "Encrypt=true;TrustServerCertificate=false;LoginTimeout=30;"
    . "MultipleActiveResultSets=false",
    null,
    null,
    [
        PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
        PDO::SQLSRV_ATTR_ENCODING => PDO::SQLSRV_ENCODING_UTF8,
        PDO::SQLSRV_ATTR_FETCHES_NUMERIC_TYPE => true,
    ]
);

$stmt = null;
try {
    $stmt = $pdo->prepare(
        'SELECT DB_NAME() AS DatabaseName, CAST(1 AS int) AS Connected',
        [PDO::SQLSRV_ATTR_QUERY_TIMEOUT => 30]
    );
    $stmt->execute();
    $row = $stmt->fetch(PDO::FETCH_ASSOC);
    if ($row !== ['DatabaseName' => $database, 'Connected' => 1]) {
        throw new RuntimeException('Unexpected database or connection result.');
    }
    echo 'Connected to ', $row['DatabaseName'], '.', PHP_EOL;
} finally {
    $stmt = null;
    $pdo = null;
}
```

If `SQL_DATABASE` is `appdb`, each program prints:

```output
Connected to appdb.
```

`ActiveDirectoryMsi` doesn't use your local Azure CLI sign-in. For local development or continuous integration (CI), choose an authentication method supported by the PHP extension and ODBC driver. You can obtain a Microsoft Entra access token through an identity library and supply it through `AccessToken`. When using a token, leave out the username, password, and `Authentication` setting. Set `AccessToken` in the SQLSRV connection options array or the PDO_SQLSRV DSN. Keep tokens out of source code and logs. For more information, see [Connect using Microsoft Entra authentication](azure-active-directory.md).

`ActiveDirectoryServicePrincipal` uses a client ID and client secret; it isn't passwordless authentication. If you use it, read the secret from protected application configuration. SQL authentication is an alternative only for targets that support it; neither SQL database in Fabric nor Warehouse supports SQL authentication.

## Adapt authentication to your environment

Use one authentication mechanism for each connection. Don't leave an old username, password, or `Authentication` setting in place when switching to `AccessToken`.

| Environment | Configuration | Responsibility |
| --- | --- | --- |
| Azure host with a system-assigned managed identity | Set `Authentication` to `ActiveDirectoryMsi`. Don't supply a username or password. | ODBC obtains the token from the host's identity endpoint. |
| Azure host with a user-assigned managed identity | Set `Authentication` to `ActiveDirectoryMsi`. Supply the identity's ID through SQLSRV's `UID` option or PDO's username argument. Don't supply a password. | The required ID depends on the hosting service. App Service and Container Instances use the client ID; check the ODBC identity guidance for other hosts. |
| Workstation or CI with an external token provider | Obtain a token for the SQL service, then set `AccessToken`. Leave out the username, password, and `Authentication` setting. | Your application or identity library obtains and refreshes the token. Neither PHP extension automatically uses your Azure CLI sign-in. |
| Service principal with a client secret | Set `Authentication` to `ActiveDirectoryServicePrincipal`. For SQLSRV, set `UID` to the client ID and `PWD` to the secret. For PDO, pass the client ID and secret as the constructor's username and password arguments. | Protect and rotate the secret. This method isn't passwordless. |
| SQL login on a target that supports it | SQLSRV uses `UID`/`PWD`; PDO uses constructor arguments two and three. | The target validates the SQL credentials. This option doesn't apply to either Fabric product. |

Request a token for the SQL service, not for Azure resource management. The token's *audience* identifies the service that accepts it. For Azure public cloud, request the scope `https://database.windows.net/.default` from your identity library. Confirm the appropriate resource for another cloud. Pass the token to the PHP extension as a string; don't manually convert it into an ODBC access-token structure.

A token can expire while a worker continues running. Obtain a valid token before opening another connection, and refresh tokens as needed in long-running workers. Never log a DSN containing an access token. For more information about ODBC authentication, see [Using Microsoft Entra ID with the ODBC Driver](../odbc/using-azure-active-directory.md).

## Keep encryption and identity checks separate

`Encrypt=true` requests an encrypted transport. `TrustServerCertificate=false` requires validation of the server certificate, including its identity. Microsoft Entra authentication establishes who your application is; it doesn't replace server-certificate validation.

Use the endpoint supplied by the SQL service and maintain the client operating system's certificate trust store. Don't replace the host name with an IP address to bypass a name-resolution problem. For a private endpoint, fix name resolution and routing while retaining the service's expected connection name.

`LoginTimeout` bounds connection establishment. `QueryTimeout` for SQLSRV and `PDO::SQLSRV_ATTR_QUERY_TIMEOUT` for PDO_SQLSRV apply to statement execution. Raising a query timeout won't repair a missing managed-identity endpoint or an incorrect database user.

## Troubleshoot by failure stage

| Failure stage | What to check |
| --- | --- |
| PHP can't load the extension | Confirm runtime version, architecture, extension build, ODBC installation, and the `php.ini` used by the application process. |
| Transmission Control Protocol (TCP) connection times out | Verify the copied endpoint and port, name resolution, firewall rules, virtual network routing, and private-endpoint configuration from the PHP host. |
| Certificate validation fails | Check the endpoint name, certificate chain, and client trust store. Keep certificate validation enabled. |
| Managed identity can't acquire a token | Confirm that the identity is enabled on this host and that its identity endpoint is available. Don't test `ActiveDirectoryMsi` on an ordinary laptop and expect Azure CLI sign-in to satisfy it. |
| Token authentication fails | Check the token's intended service, expiration, tenant, and identity. Confirm database access for that identity. Remove any username, password, and `Authentication` settings. |
| Login succeeds but the selected database can't open | Confirm the database name and that its administrator created or granted access to the intended identity. |
| Connection opens but an application query fails | Check object permissions and the target's supported T-SQL features. The connection test doesn't establish application-schema access. |
| Warehouse rejects connection settings | Set `MultipleActiveResultSets` to `false` in the SQLSRV options array or PDO DSN, and use Microsoft Entra authentication. |

SQLSRV returns `false` and exposes an error collection through `sqlsrv_errors()`. PDO construction throws `PDOException`; the examples also enable exceptions for statement failures. Capture diagnostics immediately in restricted logs and keep SQLSRV warnings distinct from errors. Don't return raw diagnostics, tokens, or connection details to a browser.

## Plan for interrupted connections

A service can disconnect an idle session or interrupt an operation during maintenance or failover. ODBC idle connection resiliency and application retries solve different problems. For retry keywords and a controlled disconnect test, see [Idle connection resiliency](connection-resiliency.md).

Don't replay a write merely because the client lost its connection. The server might have committed before the response was lost. Use an application-specific retry policy and a way to detect duplicate work. Release statements and connection handles when the request or job finishes, including error paths. For lifecycle and pooling behavior, see [Connecting to the server](connecting-to-the-server.md#manage-connection-lifetime).

## Related content

- [Connect using Microsoft Entra authentication](azure-active-directory.md)
- [Connection options](connection-options.md)
- [How to access Azure SQL Database from PHP](/azure/azure-sql/database/connect-query-content-reference-guide)
