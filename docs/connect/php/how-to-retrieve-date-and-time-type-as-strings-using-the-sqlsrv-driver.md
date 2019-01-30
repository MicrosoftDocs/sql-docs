---
title: "Retrieve Date and Time Types as Strings Using the SQLSRV Driver | Microsoft Docs"
ms.custom: ""
ms.date: "02/11/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "date and time types, retrieving as strings"
ms.assetid: 58a974ea-4daf-4e3b-98ed-9731b9c9250f
author: MightyPen
ms.author: genemi
manager: craigg
---
# How to: Retrieve Date and Time Types as Strings Using the SQLSRV Driver
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

When using the SQLSRV driver for the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)], you can retrieve date and time types (**smalldatetime**, **datetime**, **date**, **time**, **datetime2**, and **datetimeoffset**) as strings by specifying the following option in the connection string or at the statement level:

```
'ReturnDatesAsStrings'=>true
```

The default is **false**, which means that **smalldatetime**, **datetime**, **Date**, **Time**, **DateTime2**, and **DateTimeOffset** types will be returned as [PHP DateTime](http://php.net/manual/en/class.datetime.php) objects. If this option is set at the statement level, it overrides the connection level setting.

The PDO_SQLSRV driver returns date and time types as strings by default. To retrieve them as PHP DateTime objects, see [How to: Retrieve Date and Time Types as PHP Datetime Objects Using the PDO_SQLSRV](../../connect/php/how-to-retrieve-date-and-time-type-as-objects-using-the-pdo-sqlsrv-driver.md)

## Example
The following example shows the syntax specifying to retrieve date and time types as strings.

```php
<?php
$serverName = "MyServer";
$connectionInfo = array("Database"=>"AdventureWorks", 'ReturnDatesAsStrings '=> true);
$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
   echo "Could not connect.\n";
   die(print_r(sqlsrv_errors(), true));
}

sqlsrv_close($conn);
?>
```

## Example
The following example shows that you can retrieve dates as strings by specifying UTF-8 when you retrieve the string, even when the connection was made with `"ReturnDatesAsStrings" => false`.

```php
<?php
$serverName = "MyServer";
$connectionInfo = array("Database"=>"AdventureWorks", "ReturnDatesAsStrings" => false);
$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
   echo "Could not connect.\n";
   die(print_r(sqlsrv_errors(), true));
}

$tsql = "SELECT VersionDate FROM AWBuildVersion";

$stmt = sqlsrv_query($conn, $tsql);

if ($stmt === false) {
   echo "Error in statement preparation/execution.\n";
   die(print_r(sqlsrv_errors(), true));
}

sqlsrv_fetch($stmt);

// retrieve date as string
$date = sqlsrv_get_field($stmt, 0, SQLSRV_PHPTYPE_STRING("UTF-8"));

if ($date === false) {
   die(print_r(sqlsrv_errors(), true));
}

echo $date;

sqlsrv_close($conn);
?>
```

## Example
The following example shows how to retrieve dates as strings by specifying UTF-8 and `"ReturnDatesAsStrings" => true` in the connection string.

```php
<?php
$serverName = "MyServer";
$connectionInfo = array("Database"=>"AdventureWorks", 'ReturnDatesAsStrings'=> true, "CharacterSet" => 'utf-8');
$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
   echo "Could not connect.\n";
   die(print_r(sqlsrv_errors(), true));
}

$tsql = "SELECT VersionDate FROM AWBuildVersion";

$stmt = sqlsrv_query($conn, $tsql);

if ($stmt === false) {
   echo "Error in statement preparation/execution.\n";
   die(print_r(sqlsrv_errors(), true));
}

sqlsrv_fetch($stmt);

// retrieve date as string
$date = sqlsrv_get_field($stmt, 0);

if ($date === false) {
   die(print_r(sqlsrv_errors(), true));
}

echo $date;
sqlsrv_close($conn);
?>
```

## Example
The following example shows how to retrieve the date as a PHP type. `'ReturnDatesAsStrings'=> false` is on by default.

```php
<?php
$serverName = "MyServer";
$connectionInfo = array("Database"=>"AdventureWorks");
$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
   echo "Could not connect.\n";
   die(print_r(sqlsrv_errors(), true));
}

$tsql = "SELECT VersionDate FROM AWBuildVersion";

$stmt = sqlsrv_query($conn, $tsql);

if ($stmt === false) {
   echo "Error in statement preparation/execution.\n";
   die(print_r(sqlsrv_errors(), true));
}

sqlsrv_fetch($stmt);

// retrieve date as a DateTime object, then convert to string using PHP's date_format function
$date = sqlsrv_get_field($stmt, 0);

if ($date === false) {
   die(print_r(sqlsrv_errors(), true));
}

$date_string = date_format($date, 'jS, F Y');
echo "Date = $date_string\n";

sqlsrv_close($conn);
?>
```

## Example
The ReturnDatesAsStrings option at the statement level overrides the corresponding connection option.

```php
<?php
$serverName = 'MyServer';
$connectionInfo = array('Database' => 'MyDatabase', 'ReturnDatesAsStrings' => false);
$conn = sqlsrv_connect($serverName, $connectionInfo);
if ($conn === false) {
   echo "Could not connect.\n";
   die(print_r(sqlsrv_errors(), true));
}

$tableName = 'MyTable';
$options = array('ReturnDatesAsStrings' => true);
$query = "SELECT DateTimeCol FROM $tableName";
$stmt = sqlsrv_prepare($conn, $query, array(), $options);
if ($stmt === false) {
   echo "Error in statement preparation/execution.\n";
   die(print_r(sqlsrv_errors(), true));
}
sqlsrv_execute($stmt);

// Expect the fetched value to be a string
$field = sqlsrv_get_field($stmt, 0);
echo $field . PHP_EOL;

sqlsrv_close($conn);
?>
```

## See Also
[Retrieving Data](../../connect/php/retrieving-data.md)

[How to: Retrieve Date and Time Types as PHP Datetime Objects Using the PDO_SQLSRV](../../connect/php/how-to-retrieve-date-and-time-type-as-objects-using-the-pdo-sqlsrv-driver.md)
