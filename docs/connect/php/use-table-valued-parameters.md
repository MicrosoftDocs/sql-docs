---
title: Use table-valued parameters
description: Learn how to use table-valued parameters with the Microsoft Drivers for PHP for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 01/31/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Use table-valued parameters (PHP)

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

## Applicable to

- Microsoft Drivers 5.10.0 for PHP for SQL Server

## Introduction

You can use [table-valued parameters](../../relational-databases/tables/use-table-valued-parameters-database-engine.md) to send multiple rows of data to a Transact-SQL statement or a stored procedure. You don't need to create a temporary table. To use a table-valued parameter with the PHP drivers, declare a user-defined table type with a name, as shown in the examples on this page.

## Use a table-valued parameter with a stored procedure

The following examples assume the following tables, table type, and stored procedure exist:

```sql
CREATE TABLE TVPOrd(
    OrdNo INTEGER IDENTITY(1,1),
    OrdDate DATETIME,
    CustID VARCHAR(10))


CREATE TABLE TVPItem(
    OrdNo INTEGER,
    ItemNo INTEGER IDENTITY(1,1),
    ProductCode CHAR(10),
    OrderQty INTEGER,
    SalesDate DATE,
    Label NVARCHAR(30),
    Price DECIMAL(5,2),
    Photo VARBINARY(MAX))


--Create TABLE type for use as a TVP
CREATE TYPE TVPParam AS TABLE(
                ProductCode CHAR(10),
                OrderQty INTEGER,
                SalesDate DATE,
                Label NVARCHAR(30),
                Price DECIMAL(5,2),
                Photo VARBINARY(MAX))


--Create procedure with TVP parameters
CREATE PROCEDURE TVPOrderEntry(
        @CustID VARCHAR(10),
        @Items TVPParam READONLY,
        @OrdNo INTEGER OUTPUT,
        @OrdDate DATETIME OUTPUT)
AS
BEGIN
    SET @OrdDate = GETDATE(); SET NOCOUNT ON;
    INSERT INTO TVPOrd (OrdDate, CustID) VALUES (@OrdDate, @CustID);
    SELECT @OrdNo = SCOPE_IDENTITY();
    INSERT INTO TVPItem (OrdNo, ProductCode, OrderQty, SalesDate, Label, Price, Photo)
    SELECT @OrdNo, ProductCode, OrderQty, SalesDate, Label, Price, Photo
    FROM @Items
END
```

The PHP drivers use row-wise binding for table-valued parameters (TVPs), and you must provide the type name as a non-empty string. In this example, the name is `TVPParam`. The TVP input is essentially a key-value pair with TVP type name as the key and the input data as a nested array. For example:

```php
$image1 = fopen($pic1, 'rb');
$image2 = fopen($pic2, 'rb');
$image3 = fopen($pic3, 'rb');

$items = [
    ['0062836700', 367, "2009-03-12", 'AWC Tee Male Shirt', '20.75', $image1],
    ['1250153272', 256, "2017-11-07", 'Superlight Black Bicycle', '998.45', $image2],
    ['1328781505', 260, "2010-03-03", 'Silver Chain for Bikes', '88.98', $image3],
];

// Create a TVP input array
$tvpType = 'TVPParam';
$tvpInput = array($tvpType => $items);

// To execute the stored procedure, either execute a direct query or prepare this query:
$callTVPOrderEntry = "{call TVPOrderEntry(?, ?, ?, ?)}";
```

### Use the SQLSRV driver

You may call [sqlsrv_query](../../connect/php/sqlsrv-query.md) or [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md) with [sqlsrv_execute](../../connect/php/sqlsrv-execute.md). The following example shows the former use case:

```php
$custCode = 'SRV_123';
$ordNo = 0;
$ordDate = null;
$params = array($custCode,
                array($tvpInput, SQLSRV_PARAM_IN, SQLSRV_PHPTYPE_TABLE, SQLSRV_SQLTYPE_TABLE), // or simply array($tvpInput),
                array(&$ordNo, SQLSRV_PARAM_OUT),
                array(&$ordDate, SQLSRV_PARAM_OUT, SQLSRV_PHPTYPE_STRING(SQLSRV_ENC_CHAR)));
$stmt = sqlsrv_query($conn, $callTVPOrderEntry, $params);
if (!$stmt) {
    print_r(sqlsrv_errors());
}
sqlsrv_next_result($stmt);
```

In addition, you may use [sqlsrv_send_stream_data](../../connect/php/sqlsrv-send-stream-data.md) to send TVP data post execution. For example:

```php
$options = array("SendStreamParamsAtExec" => 0);
$stmt = sqlsrv_prepare($conn, $callTVPOrderEntry, $params, $options);
if (!$stmt) {
    print_r(sqlsrv_errors());
}
$res = sqlsrv_execute($stmt);
if (!$res) {
    print_r(sqlsrv_errors());
}

// Now call sqlsrv_send_stream_data in a loop
while (sqlsrv_send_stream_data($stmt)) {
}
sqlsrv_next_result($stmt);
```

### Use the PDO_SQLSRV driver

This is an equivalent example when using the PDO_SQLSRV driver. You can use prepare/execute with [bindParam](../../connect/php/pdostatement-bindparam.md) and specify the TVP input as a `PDO::PARAM_LOB`. If not, you'll get this error: `Operand type clash: nvarchar is incompatible with …`.

```php
try {
    $stmt = $conn->prepare($callTVPOrderEntry);
    $stmt->bindParam(1, $custCode);
    $stmt->bindParam(2, $tvpInput, PDO::PARAM_LOB);
    // 3 - OrdNo output
    $stmt->bindParam(3, $ordNo, PDO::PARAM_INT, 10);
    // 4 - OrdDate output
    $stmt->bindParam(4, $ordDate, PDO::PARAM_STR, 20);
    $stmt->execute();
} catch (PDOException $e) {
    ...
}
```

If your stored procedure only takes input parameters, you can use [bindValue](../../connect/php/pdostatement-bindvalue.md) instead of [bindParam](../../connect/php/pdostatement-bindparam.md).

### Use a schema other than the default dbo schema

If you're not using the default dbo schema, then you should provide the schema name. Even if the schema name contains a space character, do not use delimiters like `[` or `]`.

```php
    $inputs = [
        ['ABC', 12345, null],
        ['DEF', 6789, 'This is a test']
    ];
    $schema = 'Sales DB';
    $tvpType = 'TestTVP';

    // i.e. the TVP type name is "[Sales DB].[TestTVP]"
    $tvpInput = array($tvpType => $inputs, $schema);
```

## Use a table-valued parameter without a stored procedure

You may use table-valued parameters without stored procedures. Consider the following example:

```sql
CREATE TYPE id_table_type AS TABLE(id INT PRIMARY KEY)

CREATE TABLE test_table (id INT PRIMARY KEY)
```

### Use the SQLSRV driver

This is an example when using a user-defined schema:

```php
$schema = 'my schema';
$tvpName = 'id_table_type';

$tsql = "INSERT INTO [$schema].[test_table] SELECT * FROM ?";
$params = [
[[$tvpname => [[1], [2], [3]], $schema]],
];

$stmt = sqlsrv_query($conn, $tsql, $params);
if (!$stmt) {
    print_r(sqlsrv_errors());
}
sqlsrv_free_stmt($stmt);
```

### Use the PDO_SQLSRV driver

This is an example when using the default dbo schema:

```php
$tsql = "INSERT INTO test_table SELECT * FROM ?";
$tvpInput = array('id_table_type' => [[1], [2], [3]]);

$stmt = $conn->prepare($tsql);
$stmt->bindParam(1, $tvpInput, PDO::PARAM_LOB);
$result = $stmt->execute();
```

## See also
- [PDO](https://php.net/manual/book.pdo.php)
- [PDOStatement Class](../../connect/php/pdostatement-class.md)
- [SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)
- [How to: Perform Parameterized Queries](../../connect/php/how-to-perform-parameterized-queries.md)
- [How to: Send Data as a Stream](../../connect/php/how-to-send-data-as-a-stream.md)
