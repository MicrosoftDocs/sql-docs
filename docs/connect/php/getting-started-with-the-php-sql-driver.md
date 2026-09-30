---
title: Connect and Query with the Microsoft Drivers for PHP
description: Connect to SQL Server, Azure SQL, or SQL database in Microsoft Fabric from PHP, run a parameterized query with SQLSRV or PDO_SQLSRV, and verify the result.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: quickstart
ms.custom: intro-get-started
ai-usage: ai-assisted
---

# Quickstart: Connect and query with the Microsoft Drivers for PHP for SQL Server

Use this quickstart to install the PHP drivers, connect to Azure SQL with your Microsoft Entra identity, and run a parameterized Transact-SQL (T-SQL) query against the `AdventureWorksLT` sample data. Choose either the SQLSRV procedural API or the PDO_SQLSRV API. Both samples read connection settings from environment variables and return the same result.

The query reads product data and doesn't create database objects.

## Before you start

- Create or connect to a database that contains the `AdventureWorksLT` sample data. For setup instructions for Azure SQL Database, SQL database in Microsoft Fabric, SQL Server, and SQL Server containers, see [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md).
- Ask the database administrator to [create a database user for your Microsoft Entra identity](/azure/azure-sql/database/authentication-aad-configure#create-contained-database-users-mapped-to-microsoft-entra-identities).
- On Windows, sign in with the Windows account that has database access.
- On macOS or Linux, get a Kerberos ticket for a federated Microsoft Entra account. For requirements, see [Using Microsoft Entra ID with the ODBC Driver](../odbc/using-azure-active-directory.md).

## 1. Install PHP and the drivers

Select your operating system. Copy the entire command block, paste it into the specified terminal, and run it.

# [Windows](#tab/windows)

Open PowerShell as an administrator. Copy and run this block:

```powershell
winget install --exact --id PHP.PHP.8.5 --source winget --accept-package-agreements --accept-source-agreements
winget install --exact --id Microsoft.msodbcsql.18 --source winget --accept-package-agreements --accept-source-agreements

$env:Path = [Environment]::GetEnvironmentVariable("Path", "Machine") + ";" +
    [Environment]::GetEnvironmentVariable("Path", "User")
New-Item -ItemType Directory -Force C:\php-quickstart | Out-Null
Set-Location C:\php-quickstart

$phpDirectory = Split-Path (Get-Command php.exe -ErrorAction Stop).Source
$phpIni = Join-Path $phpDirectory "php.ini"
if (-not (Test-Path $phpIni)) {
    Copy-Item (Join-Path $phpDirectory "php.ini-development") $phpIni
}

$configuration = [System.IO.File]::ReadAllText($phpIni)
$configuration = $configuration -replace '(?m)^\s*;\s*extension_dir\s*=\s*"ext"\s*$', 'extension_dir = "ext"'
$configuration = $configuration -replace '(?m)^\s*;\s*extension\s*=\s*openssl\s*$', 'extension=openssl'
$configuration = $configuration -replace '(?m)^\s*;\s*extension\s*=\s*zip\s*$', 'extension=zip'
[System.IO.File]::WriteAllText($phpIni, $configuration)

Invoke-WebRequest https://github.com/php/pie/releases/latest/download/pie.phar -OutFile pie.phar
php .\pie.phar install microsoft/sqlsrv
php .\pie.phar install microsoft/pdo_sqlsrv

php --version
php --ri sqlsrv
php --ri pdo_sqlsrv
```

The last three commands display the installed PHP and extension versions. Close the administrator window after they succeed.

# [macOS](#tab/macos)

Run the following block in a terminal. Homebrew prompts before it installs any missing prerequisites:

```bash
brew install php@8.5 git autoconf automake libtool m4 make pkg-config
brew link --force --overwrite php@8.5
brew tap microsoft/mssql-release https://github.com/Microsoft/homebrew-mssql-release
brew trust microsoft/mssql-release
brew update
HOMEBREW_ACCEPT_EULA=Y brew install msodbcsql18

curl -fL --output /tmp/pie.phar https://github.com/php/pie/releases/latest/download/pie.phar
sudo mkdir -p /usr/local/bin
sudo install /tmp/pie.phar /usr/local/bin/pie
export CPPFLAGS="-I$(brew --prefix unixodbc)/include"
export LDFLAGS="-L$(brew --prefix unixodbc)/lib"
pie install microsoft/sqlsrv
pie install microsoft/pdo_sqlsrv

mkdir -p ~/php-quickstart
cd ~/php-quickstart
php --version
php --ri sqlsrv
php --ri pdo_sqlsrv
```

If `brew` isn't installed, [install Homebrew](https://brew.sh/), open a new terminal, and run the block again.

# [Ubuntu](#tab/ubuntu)

Run the following block in a terminal on a [supported Ubuntu version](microsoft-php-drivers-for-sql-server-support-matrix.md#supported-operating-systems):

```bash
sudo apt-get update
sudo apt-get install -y php-cli php-dev php-xml php-zip curl git autoconf automake libtool m4 make gcc g++ pkg-config unixodbc-dev

source /etc/os-release
curl -fsSLO "https://packages.microsoft.com/config/ubuntu/$VERSION_ID/packages-microsoft-prod.deb"
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install -y msodbcsql18

curl -fL --output /tmp/pie.phar https://github.com/php/pie/releases/latest/download/pie.phar
sudo install /tmp/pie.phar /usr/local/bin/pie
pie install microsoft/sqlsrv
pie install microsoft/pdo_sqlsrv

mkdir -p ~/php-quickstart
cd ~/php-quickstart
php --version
php --ri sqlsrv
php --ri pdo_sqlsrv
```

For other Linux distributions, follow the [Linux and macOS installation tutorial](installation-tutorial-linux-mac.md).

---

If either `php --ri` command reports that the extension isn't present, go to [Installation troubleshooting](#installation-troubleshooting) before you continue.

## 2. Set the connection information

Replace `<server>` and `<database>` in the block for your operating system. Copy and run the entire block in the same terminal that you'll use to run PHP.

# [Windows](#tab/windows)

```powershell
Set-Location C:\php-quickstart
$env:SQL_SERVER = "tcp:<server>.database.windows.net,1433"
$env:SQL_DATABASE = "<database>"
$env:SQL_AUTHENTICATION = "ActiveDirectoryIntegrated"
```

# [macOS](#tab/macos)

```bash
export SQL_SERVER="tcp:<server>.database.windows.net,1433"
export SQL_DATABASE="<database>"
export SQL_AUTHENTICATION="ActiveDirectoryIntegrated"
```

# [Ubuntu](#tab/ubuntu)

```bash
export SQL_SERVER="tcp:<server>.database.windows.net,1433"
export SQL_DATABASE="<database>"
export SQL_AUTHENTICATION="ActiveDirectoryIntegrated"
```

---

> [!IMPORTANT]
> Both samples enable encryption and validate the server certificate. If your server uses a certificate from a private certificate authority (CA), install the issuing root and intermediate CA certificates in the client operating system trust store. Set `SQL_SERVER` to a name in the certificate's Subject Alternative Name or Common Name. Otherwise, the connection fails before the query runs. For more information, see [Certificate chain not trusted](/troubleshoot/sql/database-engine/connect/certificate-chain-not-trusted) and [Configure SQL Server encryption](../../database-engine/configure-windows/configure-sql-server-encryption.md).
>
> `TrustServerCertificate=true` bypasses server identity validation. Use it only to diagnose an isolated local test environment, not as a trust configuration for production or shared environments.

## 3. Create and run a sample

Select one PHP API. Create the named file by using the code in the selected tab, and then run the command after the code.

# [SQLSRV](#tab/sqlsrv)

Create `quickstart-sqlsrv.php` with the following code:

```php
<?php
declare(strict_types=1);

$server = getenv('SQL_SERVER') ?: null;
$database = getenv('SQL_DATABASE') ?: null;
$authentication = getenv('SQL_AUTHENTICATION') ?: null;

if ($server === null || $database === null || $authentication === null) {
    throw new RuntimeException('Set SQL_SERVER, SQL_DATABASE, and SQL_AUTHENTICATION.');
}
if (!in_array($authentication, ['SqlPassword', 'ActiveDirectoryIntegrated', 'ActiveDirectoryMsi'], true)) {
    throw new RuntimeException(
        'Set SQL_AUTHENTICATION to SqlPassword, ActiveDirectoryIntegrated, or ActiveDirectoryMsi.'
    );
}

$user = null;
$password = null;
if ($authentication === 'SqlPassword') {
    $user = getenv('SQL_USER') ?: null;
    $password = getenv('SQL_PASSWORD') ?: null;
    if ($user === null || $password === null) {
        throw new RuntimeException('Set SQL_USER and SQL_PASSWORD for SqlPassword authentication.');
    }
}

$options = [
    'Database' => $database,
    'Authentication' => $authentication,
    'Driver' => 'ODBC Driver 18 for SQL Server',
    'Encrypt' => true,
    'TrustServerCertificate' => false,
];
if ($authentication === 'SqlPassword') {
    $options['UID'] = $user;
    $options['PWD'] = $password;
}

$connection = sqlsrv_connect($server, $options);
if ($connection === false) {
    throw new RuntimeException(print_r(sqlsrv_errors(), true));
}

$sql = <<<'SQL'
SELECT TOP (5) ProductID, Name
FROM SalesLT.Product
WHERE ProductID > ?
ORDER BY ProductID;
SQL;
$parameters = [0];
$statement = sqlsrv_query($connection, $sql, $parameters);
if ($statement === false) {
    $errors = sqlsrv_errors();
    sqlsrv_close($connection);
    throw new RuntimeException(print_r($errors, true));
}

$rows = [];
while (($row = sqlsrv_fetch_array($statement, SQLSRV_FETCH_ASSOC)) !== null) {
    if ($row === false) {
        $errors = sqlsrv_errors();
        sqlsrv_free_stmt($statement);
        sqlsrv_close($connection);
        throw new RuntimeException(print_r($errors, true));
    }
    $rows[] = $row;
}
sqlsrv_free_stmt($statement);
sqlsrv_close($connection);

if (count($rows) !== 5) {
    throw new RuntimeException('Unexpected query result.');
}
$previousProductId = 0;
foreach ($rows as $row) {
    if ($row['ProductID'] <= $previousProductId || $row['Name'] === '') {
        throw new RuntimeException('Unexpected query result.');
    }
    $previousProductId = $row['ProductID'];
}
printf("%-12s%s\n", 'Product ID', 'Name');
printf("%-12s%s\n", '----------', '----');
foreach ($rows as $row) {
    printf("%-12d%s\n", $row['ProductID'], $row['Name']);
}
```

Run the sample:

```console
php quickstart-sqlsrv.php
```

# [PDO_SQLSRV](#tab/pdo-sqlsrv)

Create `quickstart-pdo.php` with the following code:

```php
<?php
declare(strict_types=1);

$server = getenv('SQL_SERVER') ?: null;
$database = getenv('SQL_DATABASE') ?: null;
$authentication = getenv('SQL_AUTHENTICATION') ?: null;

if ($server === null || $database === null || $authentication === null) {
    throw new RuntimeException('Set SQL_SERVER, SQL_DATABASE, and SQL_AUTHENTICATION.');
}
if (!in_array($authentication, ['SqlPassword', 'ActiveDirectoryIntegrated', 'ActiveDirectoryMsi'], true)) {
    throw new RuntimeException(
        'Set SQL_AUTHENTICATION to SqlPassword, ActiveDirectoryIntegrated, or ActiveDirectoryMsi.'
    );
}

$user = null;
$password = null;
if ($authentication === 'SqlPassword') {
    $user = getenv('SQL_USER') ?: null;
    $password = getenv('SQL_PASSWORD') ?: null;
    if ($user === null || $password === null) {
        throw new RuntimeException('Set SQL_USER and SQL_PASSWORD for SqlPassword authentication.');
    }
}

$dsn = "sqlsrv:Driver={ODBC Driver 18 for SQL Server};"
    . "Server=$server;Database=$database;Authentication=$authentication;"
    . "Encrypt=true;TrustServerCertificate=false";
$connection = new PDO(
    $dsn,
    $user,
    $password,
    [
        PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
        PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
        PDO::ATTR_EMULATE_PREPARES => false,
    ]
);

$sql = <<<'SQL'
SELECT TOP (5) ProductID, Name
FROM SalesLT.Product
WHERE ProductID > ?
ORDER BY ProductID;
SQL;
$statement = $connection->prepare($sql);
$statement->execute([0]);
$rows = $statement->fetchAll();
$statement->closeCursor();
$connection = null;

if (count($rows) !== 5) {
    throw new RuntimeException('Unexpected query result.');
}
$previousProductId = 0;
foreach ($rows as $row) {
    if ($row['ProductID'] <= $previousProductId || $row['Name'] === '') {
        throw new RuntimeException('Unexpected query result.');
    }
    $previousProductId = $row['ProductID'];
}
printf("%-12s%s\n", 'Product ID', 'Name');
printf("%-12s%s\n", '----------', '----');
foreach ($rows as $row) {
    printf("%-12d%s\n", $row['ProductID'], $row['Name']);
}
```

Run the sample:

```console
php quickstart-pdo.php
```

---

## 4. Verify the result

The product rows can vary by AdventureWorksLT version. Both samples return output that resembles this example:

```output
Product ID  Name
----------  ----
680         HL Road Frame - Black, 58
706         HL Road Frame - Red, 58
707         Sport-100 Helmet, Red
708         Sport-100 Helmet, Black
709         Mountain Bike Socks, M
```

Each sample checks that the query returned five products with nonempty names and ascending product IDs before it prints the rows. It then releases the statement and closes the connection. The query doesn't leave any database objects or data to remove.

## Use another authentication method

The samples also accept managed identity and SQL Server authentication without changing the PHP files.

### Managed identity

For an application hosted in Azure, [enable a managed identity and create its database user](/azure/azure-sql/database/authentication-aad-configure). Set the server, database, and authentication mode in the application's configuration:

```text
SQL_SERVER=tcp:<server>.database.windows.net,1433
SQL_DATABASE=<database>
SQL_AUTHENTICATION=ActiveDirectoryMsi
```

Don't set `SQL_USER` or `SQL_PASSWORD`.

For SQL database in Microsoft Fabric, grant the identity **Read** item permission through [Fabric access controls](/fabric/database/sql/authorization#fabric-access-controls). Use the SQL connection endpoint from the database item, not the SQL analytics endpoint. SQL database in Fabric doesn't support SQL authentication.

### SQL Server authentication

Use SQL Server authentication only for a SQL Server instance that you control, such as an isolated local development container. Keep credentials in application configuration or a secret store. Don't commit them to source control.

# [Windows](#tab/windows)

```powershell
$env:SQL_SERVER = "tcp:<server>,1433"
$env:SQL_DATABASE = "<database>"
$env:SQL_AUTHENTICATION = "SqlPassword"
$env:SQL_USER = "<user_id>"
$env:SQL_PASSWORD = "<password>"
```

# [macOS](#tab/macos)

```bash
export SQL_SERVER="tcp:<server>,1433"
export SQL_DATABASE="<database>"
export SQL_AUTHENTICATION="SqlPassword"
export SQL_USER="<user_id>"
export SQL_PASSWORD="<password>"
```

# [Ubuntu](#tab/ubuntu)

```bash
export SQL_SERVER="tcp:<server>,1433"
export SQL_DATABASE="<database>"
export SQL_AUTHENTICATION="SqlPassword"
export SQL_USER="<user_id>"
export SQL_PASSWORD="<password>"
```

---

For other Microsoft Entra authentication methods, see [Connect using Microsoft Entra authentication](azure-active-directory.md).

## Installation troubleshooting

Use these checks if the installation block stops or a verification command fails.

# [Windows](#tab/windows)

```powershell
Get-Command php.exe
php --ini
php --ri sqlsrv
php --ri pdo_sqlsrv
Get-OdbcDriver -Name "ODBC Driver 18 for SQL Server"
```

If `Get-Command` can't find `php.exe`, close every terminal, open a new PowerShell window, and run the checks again. If a `php --ri` command reports *Extension not present*, rerun the PIE installation commands from `C:\php-quickstart`.

# [macOS](#tab/macos)

```bash
command -v php
php --ini
php --ri sqlsrv
php --ri pdo_sqlsrv
odbcinst -q -d | grep "ODBC Driver 18 for SQL Server"
```

If `command -v` doesn't find PHP, run `brew link --force --overwrite php@8.5`, open a new terminal, and run the checks again.

# [Ubuntu](#tab/ubuntu)

```bash
command -v php
php --ini
php --ri sqlsrv
php --ri pdo_sqlsrv
odbcinst -q -d | grep "ODBC Driver 18 for SQL Server"
```

If a `php --ri` command reports *Extension not present*, rerun the PIE installation commands. Use the path from `php --ini` to confirm that the command-line runtime loads the configuration that PIE updated.

---

If a connection reports `FA001` and says that the Authentication option can't be used with Integrated Security, confirm that `SQL_AUTHENTICATION` is exactly `ActiveDirectoryIntegrated`. Run `php --ri sqlsrv` or `php --ri pdo_sqlsrv` to check the PHP driver version, and [update the driver](loading-the-php-sql-driver.md) if it's older than 5.10.1.

## Clean up

The samples release their statements and close their connections. They don't create database objects or persist data.

The connection settings apply to the current terminal session. Close the terminal when you finish.

For production retry, timeout, logging, and failover settings, use the [production baseline](microsoft-php-driver-for-sql-server.md#production-baseline-for-azure-sql) instead of extending this first-run sample.

## Related content

- [Overview of the Microsoft Drivers for PHP for SQL Server](overview-of-the-php-sql-driver.md).
- [Connection options](connection-options.md).
- [Perform parameterized queries](how-to-perform-parameterized-queries.md).
- [Troubleshooting](troubleshooting-php-sql-driver.md).
