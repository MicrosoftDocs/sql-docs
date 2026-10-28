---
title: "Connect on a Specified Port"
description: "Learn how to connect to a SQL Server instance listening on a non-default TCP port using the SQLSRV and PDO_SQLSRV drivers for PHP."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 10/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
helpviewer_keywords:
  - "connecting to the server, specifying a port"
ai-usage: ai-assisted
---
# Connect on a specified port

<a id="how-to-connect-on-a-specified-port"></a>

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

For a Transmission Control Protocol (TCP) connection without an explicit port, the default port is 1433. Named-instance connections can use SQL Server Browser to discover the instance's port. To select TCP and a specific port explicitly, use `tcp:<server>,<port>`. For information about configuring the server side, see [Configure a server to listen on a specific TCP port](../../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md).

<a id="to-connect-on-a-specified-port"></a>

## Prepare the endpoint

1. Ask the server administrator for the host name and TCP port. A client connection string doesn't change the port on which SQL Server listens.
1. Allow that port through the server firewall and intervening network controls.
1. Set `SQL_SERVER` to the host name only, `SQL_PORT` to the decimal port number, and `SQL_DATABASE` to the database name. The programs construct `tcp:<server>,<port>`. Use a comma before the port, not a colon.
1. Set `SQL_USER` and `SQL_PASSWORD` through protected process configuration for these SQL-authentication examples. For integrated or Microsoft Entra authentication, use the appropriate credential settings from [Connecting to the server](connecting-to-the-server.md).

An explicit port avoids instance-port discovery through SQL Server Browser. Keep the server host name consistent with its certificate. Neither example disables certificate validation.

Store the actual database name, username, and password in the environment variables. Both programs escape these values once for the PHP driver's ODBC-style connection handling by doubling closing braces and enclosing each value in braces. For more information, see [Connecting to the server](connecting-to-the-server.md#before-you-connect).

## SQLSRV

Save this program as `connect-port-sqlsrv.php`, and run `php connect-port-sqlsrv.php`.

```php
<?php
$config = [];
foreach (['SQL_SERVER', 'SQL_PORT', 'SQL_DATABASE', 'SQL_USER', 'SQL_PASSWORD'] as $name) {
    $value = getenv($name);
    if ($value === false || $value === '') {
        throw new RuntimeException("Set $name before running this example.");
    }
    $config[$name] = $value;
}
foreach (['SQL_DATABASE', 'SQL_USER', 'SQL_PASSWORD'] as $name) {
    $config[$name] = '{' . str_replace('}', '}}', $config[$name]) . '}';
}
$port = filter_var($config['SQL_PORT'], FILTER_VALIDATE_INT, [
    'options' => ['min_range' => 1, 'max_range' => 65535],
]);
if ($port === false) {
    throw new RuntimeException('SQL_PORT must be an integer from 1 through 65535.');
}
$serverName = 'tcp:' . $config['SQL_SERVER'] . ',' . $port;
$conn = sqlsrv_connect($serverName, [
    'Database' => $config['SQL_DATABASE'],
    'UID' => $config['SQL_USER'],
    'PWD' => $config['SQL_PASSWORD'],
    'Encrypt' => true,
    'TrustServerCertificate' => false,
    'LoginTimeout' => 30,
]);
if ($conn === false) {
    throw new RuntimeException(print_r(sqlsrv_errors(), true));
}
$stmt = null;
try {
    $stmt = sqlsrv_query($conn, 'SELECT CAST(1 AS int) AS Connected');
    if ($stmt === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    $row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
    if ($row === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    if ($row !== ['Connected' => 1]) {
        throw new RuntimeException('Unexpected connection-test result.');
    }
    echo "Connected on TCP port $port.", PHP_EOL;
} finally {
    if (is_resource($stmt)) {
        sqlsrv_free_stmt($stmt);
    }
    sqlsrv_close($conn);
}
```

## PDO_SQLSRV

Save this separate program as `connect-port-pdo.php`, and run `php connect-port-pdo.php` with the same configuration.

```php
<?php
$config = [];
foreach (['SQL_SERVER', 'SQL_PORT', 'SQL_DATABASE', 'SQL_USER', 'SQL_PASSWORD'] as $name) {
    $value = getenv($name);
    if ($value === false || $value === '') {
        throw new RuntimeException("Set $name before running this example.");
    }
    $config[$name] = $value;
}
foreach (['SQL_DATABASE', 'SQL_USER', 'SQL_PASSWORD'] as $name) {
    $config[$name] = '{' . str_replace('}', '}}', $config[$name]) . '}';
}
$port = filter_var($config['SQL_PORT'], FILTER_VALIDATE_INT, [
    'options' => ['min_range' => 1, 'max_range' => 65535],
]);
if ($port === false) {
    throw new RuntimeException('SQL_PORT must be an integer from 1 through 65535.');
}
$serverName = 'tcp:' . $config['SQL_SERVER'] . ',' . $port;
$database = $config['SQL_DATABASE'];
$pdo = new PDO(
    "sqlsrv:Server=$serverName;Database=$database;Encrypt=true;"
    . "TrustServerCertificate=false;LoginTimeout=30",
    $config['SQL_USER'],
    $config['SQL_PASSWORD'],
    [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION]
);
$stmt = null;
try {
    $stmt = $pdo->query('SELECT CAST(1 AS int) AS Connected');
    if ($stmt->fetch(PDO::FETCH_ASSOC) !== ['Connected' => '1']) {
        throw new RuntimeException('Unexpected connection-test result.');
    }
    echo "Connected on TCP port $port.", PHP_EOL;
} finally {
    $stmt = null;
    $pdo = null;
}
```

For `SQL_PORT=1433`, either program prints:

```output
Connected on TCP port 1433.
```

The fetched **int** is PHP `int` in SQLSRV and a string by default in PDO_SQLSRV. The port is part of the server argument for SQLSRV and part of the `Server` DSN keyword for PDO_SQLSRV. There is no separate PDO `Port` keyword in this pattern.

## Troubleshoot the port selection

| Symptom | Action |
| --- | --- |
| A named instance works, but an explicit port doesn't | Confirm the actual listening port. The instance might use a dynamic port that changed after a restart. |
| TCP times out | Check the listener and firewall from the PHP host. An open port still doesn't prove SQL authentication will succeed. |
| Certificate-name validation fails after using an IP address | Use the host name on the certificate with the explicit port. Don't disable validation to compensate for the name change. |
| The application still connects elsewhere | Check the environment of the running PHP process and the generated server setting, without printing credentials. Restart the application process if its configuration requires it. |

TCP selection and instance discovery belong to ODBC and the server's network configuration. PHP supplies the endpoint and reports the connection result. For Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Fabric, or Warehouse in Fabric, retain the service-supplied endpoint and port instead of choosing an arbitrary port. The SQL-login programs aren't authentication examples for either Fabric product. For more information, see [Azure and Fabric connections](connecting-to-microsoft-azure-sql-database.md).

## Related content

- [Connecting to the server](connecting-to-the-server.md)
- [Programming guide for the Microsoft Drivers for PHP for SQL Server](programming-guide-for-php-sql-driver.md)
- [Getting started with the Microsoft Drivers for PHP for SQL Server](getting-started-with-the-php-sql-driver.md)
- [SQLSRV driver API reference](sqlsrv-driver-api-reference.md)
- [PDO_SQLSRV driver reference](pdo-sqlsrv-driver-reference.md)
