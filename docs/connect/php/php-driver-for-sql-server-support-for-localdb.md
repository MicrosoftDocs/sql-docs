---
title: "PHP Driver Support for LocalDB"
description: "Learn how the Microsoft Drivers for PHP for SQL Server connect to a SQL Server Express LocalDB instance on Windows."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 10/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# Support for LocalDB

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

[SQL Server Express LocalDB](../../database-engine/configure-windows/sql-server-express-localdb.md) is a lightweight, user-instance mode of SQL Server Express, **available on Windows only**. Both drivers connect to a LocalDB instance the same way they connect to any named instance, by using the `(localdb)\<instance-name>` server name form.

## Remarks

LocalDB runs under a Windows user account. The programs use Windows integrated authentication, not Microsoft Entra authentication. Use it for local Windows development when a per-user database instance fits your workflow. It isn't a container image or an Azure service.

> [!NOTE]
> LocalDB isn't available on Linux or macOS. Connect to a SQL Server instance on a supported host instead. For containers, check the [supported SQL Server on Linux platforms](../../linux/install-upgrade/setup.md#supported-platforms); don't assume that every macOS or Linux architecture can run the SQL Server container image.

## Connect to a LocalDB instance

Install a supported SQL Server Express LocalDB release and both the ODBC prerequisite and the PHP extension you select. LocalDB installation doesn't load a PHP extension.

1. Run `sqllocaldb.exe i` in your Windows terminal to list your instances.
1. If `MSSQLLocalDB` doesn't exist, run `sqllocaldb.exe create MSSQLLocalDB`.
1. Run `sqllocaldb.exe start MSSQLLocalDB`, then `sqllocaldb.exe info MSSQLLocalDB` to inspect the instance. Perform these steps as the Windows account that runs the PHP program.
1. Save and run one of the programs with command-line PHP. Each connects to `master` by default and verifies that the target is actually LocalDB. No sample database or SQL password is required.

The instance name is local to the Windows machine and owning account. It isn't a DNS host name, TCP port, or connection to another computer.

### SQLSRV

Save this program as `connect-localdb-sqlsrv.php`, and run `php connect-localdb-sqlsrv.php`.

```php
<?php
$instance = getenv('LOCALDB_INSTANCE') ?: 'MSSQLLocalDB';
$database = getenv('LOCALDB_DATABASE');
if ($database === false || $database === '') {
    $database = 'master';
}
$file = getenv('LOCALDB_MDF');
$databaseValue = '{' . str_replace('}', '}}', $database) . '}';
$options = ['Database' => $databaseValue, 'LoginTimeout' => 30, 'CharacterSet' => 'UTF-8'];
if ($file !== false && $file !== '') {
    if ($database === 'master' || !is_file($file)) {
        throw new RuntimeException('Set a non-master database name and an accessible MDF path.');
    }
    $options['AttachDBFileName'] = '{' . str_replace('}', '}}', $file) . '}';
}
$conn = sqlsrv_connect('(localdb)\\' . $instance, $options);
if ($conn === false) {
    throw new RuntimeException(print_r(sqlsrv_errors(), true));
}
$stmt = null;
try {
    $stmt = sqlsrv_query(
        $conn,
        "SELECT DB_NAME() AS DatabaseName, CAST(SERVERPROPERTY('IsLocalDB') AS int) AS IsLocalDB"
    );
    if ($stmt === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    $row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
    if ($row === false) {
        throw new RuntimeException(print_r(sqlsrv_errors(), true));
    }
    if ($row !== ['DatabaseName' => $database, 'IsLocalDB' => 1]) {
        throw new RuntimeException('Expected the selected LocalDB database.');
    }
    echo 'LocalDB database: ', $row['DatabaseName'], PHP_EOL;
} finally {
    if (is_resource($stmt)) {
        sqlsrv_free_stmt($stmt);
    }
    sqlsrv_close($conn);
}
```

### PDO_SQLSRV

Save this separate program as `connect-localdb-pdo.php`, and run `php connect-localdb-pdo.php`.

```php
<?php
$instance = getenv('LOCALDB_INSTANCE') ?: 'MSSQLLocalDB';
$database = getenv('LOCALDB_DATABASE');
if ($database === false || $database === '') {
    $database = 'master';
}
$file = getenv('LOCALDB_MDF');
$databaseValue = '{' . str_replace('}', '}}', $database) . '}';
$dsn = "sqlsrv:Server=(localdb)\\$instance;Database=$databaseValue;LoginTimeout=30";
if ($file !== false && $file !== '') {
    if ($database === 'master' || !is_file($file)) {
        throw new RuntimeException('Set a non-master database name and an accessible MDF path.');
    }
    $dsn .= ';AttachDBFileName={' . str_replace('}', '}}', $file) . '}';
}
$pdo = new PDO($dsn, null, null, [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
    PDO::SQLSRV_ATTR_FETCHES_NUMERIC_TYPE => true,
    PDO::SQLSRV_ATTR_ENCODING => PDO::SQLSRV_ENCODING_UTF8,
]);
$stmt = null;
try {
    $stmt = $pdo->query(
        "SELECT DB_NAME() AS DatabaseName, CAST(SERVERPROPERTY('IsLocalDB') AS int) AS IsLocalDB"
    );
    $row = $stmt->fetch(PDO::FETCH_ASSOC);
    if ($row !== ['DatabaseName' => $database, 'IsLocalDB' => 1]) {
        throw new RuntimeException('Expected the selected LocalDB database.');
    }
    echo 'LocalDB database: ', $row['DatabaseName'], PHP_EOL;
} finally {
    $stmt = null;
    $pdo = null;
}
```

Expected output with the default configuration:

```output
LocalDB database: master
```

SQLSRV returns a connection resource and reports connection failures as `false`. PDO returns an object and throws `PDOException` if construction fails. The PDO program explicitly enables numeric fetching so the **int** value is PHP `int`, matching the SQLSRV result. Both use UTF-8 for character data and release statement and connection handles even when a query fails.

## Select an existing database or attach a file

The programs accept these optional process variables:

| Variable | Meaning |
| --- | --- |
| `LOCALDB_INSTANCE` | Instance name without the `(localdb)\` prefix. Defaults to `MSSQLLocalDB`. |
| `LOCALDB_DATABASE` | An existing database name, or the name to give an attached database. Defaults to `master`. |
| `LOCALDB_MDF` | Absolute path to an existing, accessible `.mdf` file to attach. Omit it when connecting to an already registered database. |

To use a database already registered with the instance, set `LOCALDB_DATABASE` and leave `LOCALDB_MDF` unset. To attach an existing database file, set both. For example, in PowerShell:

```powershell
$env:LOCALDB_DATABASE = 'myData'
$env:LOCALDB_MDF = 'C:\Data\myData.mdf'
```

Use a database file prepared for this purpose, not a file still attached to another running instance. Grant the Windows account access to the database files and their directory. `AttachDBFileName` doesn't create a missing MDF file, migrate a database, or resolve an incompatible database-file version. An attachment registers the database with the instance; closing PHP doesn't detach or delete it. Test each API's attachment path from a detached state if you need to verify both independently.

The server string's backslash is escaped in PHP single-quoted strings as `\\`. PHP double-quoted strings also interpret backslashes and variables, so the PDO program constructs the instance string deliberately. Both programs escape the database name for the connection while keeping the original name for validation and result checks. They also check the actual filesystem path before escaping the attachment value. For each connection value, they double closing braces and enclose the value in braces. SQLSRV requires that escaping even for string options supplied in its connection array. Set `LOCALDB_DATABASE` and `LOCALDB_MDF` to the actual name and path, without adding or doubling braces yourself.

These connections use a local instance, not a remote server. For remote SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Fabric, or Warehouse in Fabric, use the actual service endpoint. Set authentication, encryption, and certificate validation explicitly. For more information, see [Connecting to the server](connecting-to-the-server.md).

## Diagnose account and instance problems

You can also manage a LocalDB instance's databases from the command line with [sqlcmd](../../tools/sqlcmd/sqlcmd-utility.md), for example `sqlcmd -S "(localdb)\MSSQLLocalDB"`. A LocalDB instance that your interactive Windows account owns doesn't automatically grant access to an Internet Information Services (IIS) application pool identity. Check instance ownership and file permissions when changing the account that runs PHP. For more information, see [SQL Server Express LocalDB](../../database-engine/configure-windows/sql-server-express-localdb.md).

| Symptom | Check |
| --- | --- |
| `sqllocaldb.exe` isn't found | Confirm that LocalDB is installed and its utility is available to the terminal. Installing the PHP drivers isn't sufficient. |
| The instance isn't found | List instances as the same Windows account that runs PHP, and check `LOCALDB_INSTANCE`. |
| The initial connection takes too long | Start the instance explicitly and retry with the documented login timeout. Investigate an instance startup failure rather than retrying indefinitely. |
| `master` works, but `myData` doesn't | Confirm that the database exists in that instance, or configure an accessible attachment file and database name. |
| File attachment fails | Check the exact path, file ownership, directory permissions, whether the file is already in use, and LocalDB version compatibility. |
| Terminal PHP works but IIS doesn't | Compare the account, instance ownership, file access, runtime configuration, and extension loading. Don't assume an interactive test proves web-hosting access. |

LocalDB startup and per-user ownership belong to SQL Server LocalDB and Windows. The PHP extensions supply connection options through ODBC. Don't add SQL credentials or Microsoft Entra options to compensate for an instance-ownership problem.

## Related content

- [Connecting to the server](connecting-to-the-server.md)
- [SQL Server Express LocalDB](../../database-engine/configure-windows/sql-server-express-localdb.md)
