---
title: "How to: Retrieve Date and Time Types as PHP DateTime Objects Using the PDO_SQLSRV Driver | Microsoft Docs"
ms.custom: ""
ms.date: "02/11/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "date and time types, retrieving as datetime objects"
author: "yitam"
ms.author: "v-yitam"
manager: "mbarwin"
---
# How to: Retrieve Date and Time Types as PHP DateTime Objects Using the PDO_SQLSRV Driver
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This feature, added in version 5.6.0, is only valid when using the PDO_SQLSRV driver for the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].

### To retrieve date and time types as DateTime objects

When using PDO_SQLSRV, date and time types (**smalldatetime**, **datetime**, **date**, **time**, **datetime2**, and **datetimeoffset**) are by default returned as strings. Neither the PDO::ATTR_STRINGIFY_FETCHES nor the PDO::SQLSRV_ATTR_FETCHES_NUMERIC_TYPE attribute has
any effect. In order to retrieve date and time types as [PHP DateTime](http://php.net/manual/en/class.datetime.php) objects, set the connection or statement attribute `PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE` to **true** (it is **false** by default).

> [!NOTE]
> This connection or statement attribute only applies to regular fetching of date and
> time types because DateTime objects cannot be specified as output parameters.

## Example
The following examples omit error checking for clarity. This one shows how to set the connection attribute:

```php
<?php
$server = 'myserver';
$databaseName = 'mydatabase';
$username = 'myusername';
$passwd = 'mypasword';
$tableName = 'mytable';

$conn = new PDO("sqlsrv:Server = $server; Database = $databaseName", $username, $passwd);

// To set the connection attribute
$conn->setAttribute(PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE, true);
$query = "SELECT DateTimeCol FROM $tableName";
$stmt = $conn->prepare($query);
$stmt->execute();

// Expect a DateTimeCol value as a PHP DateTime type
$row = $stmt->fetch(PDO::FETCH_ASSOC);
var_dump($row);

unset($stmt);
unset($conn);
?>
```

## Example
This example shows how to set the statement attribute:

```php
<?php
$database = "test";
$server = "(local)";
$conn = new PDO("sqlsrv:server = $server; Database = $database", "", "");
$query = "SELECT DateTimeCol FROM myTable";
$stmt = $conn->prepare($query);
$stmt->setAttribute(PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE, true);
$stmt->execute();

// Expect a DateTimeCol value as a PHP DateTime type
$row = $stmt->fetch(PDO::FETCH_NUM);
var_dump($row);

unset($stmt);
unset($conn);
?>
```

## Example
Alternatively, the statement attribute can be set as an option:

```php
<?php
$database = "test";
$server = "(local)";
$conn = new PDO("sqlsrv:server = $server; Database = $database", "", "");

$dateObj = null;
$query = "SELECT DateTimeCol FROM aTable";
$options = array(PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE => true);
$stmt = $conn->prepare($query, $options);
$stmt->execute();
$stmt->bindColumn(1, $dateObj, PDO::PARAM_LOB);
$row = $stmt->fetch(PDO::FETCH_BOUND);
var_dump($dateObj);

unset($stmt);
unset($conn);
?>
```

## Example
The following example shows how to achieve the opposite (which is not really necessary because it is false by default):

```php
<?php
$database = "MyData";
$conn = new PDO("sqlsrv:server = (local); Database = $database");

$dateStr = null;
$query = 'SELECT DateTimeCol FROM table1';
$options = array(PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE => false);
$stmt = $conn->prepare($query, $options);
$stmt->execute();
$stmt->bindColumn(1, $dateStr);
$row = $stmt->fetch(PDO::FETCH_BOUND);
echo $dateStr . PHP_EOL;

unset($stmt);
unset($conn);
?>
```

## See Also
[Retrieving Data](../../connect/php/retrieving-data.md)

[Retrieve Date and Time Types as Strings Using the SQLSRV Driver](../../connect/php/how-to-retrieve-date-and-time-type-as-strings-using-the-sqlsrv-driver.md)