---
title: "Formatting decimal strings and money values (SQLSRV driver)"
description: "Learn how to use FormatDecimals and DecimalPlaces options to format decimal or money values when using the Microsoft SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "formatting, decimal types, money values"
---
# Formatting Decimal Strings and Money Values (SQLSRV Driver)
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

To preserve accuracy, [decimal or numeric types](../../t-sql/data-types/decimal-and-numeric-transact-sql.md) are always fetched as strings with exact precisions and scales. If any value is less than 1, the leading zero is missing. It is the same with money and smallmoney fields as they are decimal fields with a fixed scale equal to 4.

## Add leading zeroes if missing
Beginning with version 5.6.0, the option `FormatDecimals` is added to sqlsrv connection and statement levels, which allows the user to format decimal strings. This option expects a boolean value (true or false) and only affects the formatting of decimal or numeric values in the fetched results. In other words, the `FormatDecimals` option has no effect on other operations like insertion or update.

By default, `FormatDecimals` is **false**. If set to true, the leading zeroes to decimal strings will be added for any decimal value less than 1.

## Configure number of decimal places
With `FormatDecimals` turned on, another option, `DecimalPlaces`, allows users to configure the number of decimal places when displaying money and smallmoney data. It accepts integer values in the range of [0, 4], and rounding may occur when shown. However, the underlying money data remain the same.

Both options can be set to connection or statement level, and the statement setting always overrides the corresponding connection setting. Note that the `DecimalPlaces` option **only** affects money data, and `FormatDecimals` must be set to true for `DecimalPlaces` to take effect. Otherwise, formatting is turned off regardless of `DecimalPlaces` setting.

> [!NOTE]
> Since money or smallmoney fields have scale 4, setting `DecimalPlaces` value to any 
> negative number or any value larger than 4 will be ignored. It is not recommended to use 
> any formatted money data as inputs to any calculation.

## Example - a simple fetch
The following example shows how to use the new options in a simple fetch.

```php
<?php
$username = 'myusername';
$password = 'mypasword';
$tableName = 'mytable';

$connectionInfo = array("UID" => $username, "PWD" => $password, "Database" => "myDB", "FormatDecimals" => true);  
$server = "myServer";  // IP address also works
$conn = sqlsrv_connect( $server, $connectionInfo);  

$numDigits = 2;
$query = "SELECT money1 FROM $tableName";
$options = array("DecimalPlaces" => $numDigits);
$stmt = sqlsrv_prepare($conn, $query, array(), $options);
sqlsrv_execute($stmt);

if (sqlsrv_fetch($stmt)) {
    $field = sqlsrv_get_field($stmt, 0);  
    echo $field;   // expect a numeric value string with 2 decimal places
}

sqlsrv_free_stmt($stmt);
sqlsrv_close($conn);
?>
```

## Example - format the output parameter
If a decimal or numeric field is returned as the [output parameter](../../connect/php/how-to-retrieve-output-parameters-using-the-sqlsrv-driver.md), the returned value will be regarded as a regular varchar string. However, if either SQLSRV_SQLTYPE_DECIMAL or SQLSRV_SQLTYPE_NUMERIC is specified, you can set `FormatDecimals` to true to ensure there is no missing leading zero for the numerical string value. For more information, please read [How to: Retrieve Output Parameters Using the SQLSRV Driver](../..//connect/php/how-to-retrieve-output-parameters-using-the-sqlsrv-driver.md).

The following example shows how to format the output parameter of a stored procedure that returns a decimal(8,4) value.

```php
$outString = '';
$outSql = '{CALL myStoredProc(?)}';
$stmt = sqlsrv_prepare($conn, 
                       $outSql, 
                       array(array(&$outString, SQLSRV_PARAM_OUT, null, SQLSRV_SQLTYPE_DECIMAL(8, 4))),
                       array('FormatDecimals' => true));

if (sqlsrv_execute($stmt)) {
    echo $outString;  // expect a numeric value string with no missing leading zero
}
```

## See Also
[Formatting Decimal Strings and Money Values (PDO_SQLSRV Driver)](../../connect/php/formatting-decimals-pdo-sqlsrv-driver.md)

[Retrieving Data](../../connect/php/retrieving-data.md)