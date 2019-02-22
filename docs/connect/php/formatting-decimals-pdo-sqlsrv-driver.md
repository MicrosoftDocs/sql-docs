---
title: "Formatting Decimal Strings and Money Values (PDO_SQLSRV Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "02/11/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "formatting, decimal types, money values"
author: "yitam"
ms.author: "v-yitam"
manager: "mbarwin"
---
# Formatting Decimal Strings and Money Values (PDO_SQLSRV Driver)
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

To preserve accuracy, [decimal or numeric types](https://docs.microsoft.com/sql/t-sql/data-types/decimal-and-numeric-transact-sql) are always fetched as strings with exact precisions and scales. If any value is less than 1, the leading zero is missing. It is the same with money and smallmoney fields as they are decimal fields with a fixed scale equal to 4.

## Add leading zeroes if missing
Beginning with version 5.6.0, the connection or statement attribute `PDO::SQLSRV_ATTR_FORMAT_DECIMALS` allows the user to format decimal strings. This attribute expects a boolean value (true or false) and only affects the formatting of the decimal or numeric values in the fetched results. In other words, this attribute has no effect on other operations like insertion or update.

By default, `PDO::SQLSRV_ATTR_FORMAT_DECIMALS` is **false**. If set to true, the leading zeroes to decimal strings will be added for any decimal value less than 1.

## Configure number of decimal places
With `PDO::SQLSRV_ATTR_FORMAT_DECIMALS` turned on, another connection or statement attribute, `PDO::SQLSRV_ATTR_DECIMAL_PLACES`, allows users to configure the number of decimal places when displaying money and smallmoney data. It accepts integer values in the range of [0, 4], and rounding may occur when shown. However, the underlying money data remain the same.

The statement attributes always override the corresponding connection settings. Note that the `PDO::SQLSRV_ATTR_DECIMAL_PLACES` option **only** affects money data, and `PDO::SQLSRV_ATTR_FORMAT_DECIMALS` must be set to true. Otherwise, formatting is turned off regardless of `PDO::SQLSRV_ATTR_DECIMAL_PLACES` setting.

> [!NOTE]
> Since money or smallmoney fields have scale 4, setting `PDO::SQLSRV_ATTR_DECIMAL_PLACES`
> to any negative number or any value larger than 4 will be ignored. It is not recommended
> to use any formatted money data as inputs to any calculation.

### To set the connection attributes

-   Set attributes at the point of connection:

    ```php
    $attrs = array(PDO::SQLSRV_ATTR_FORMAT_DECIMALS => true,
                   PDO::SQLSRV_ATTR_DECIMAL_PLACES => 2);

    $conn = new PDO("sqlsrv:Server = myServer; Database = myDB", $username, $password, $attrs);
    ```

-   Set attributes post connection:

    ```php
    $conn = new PDO("sqlsrv:Server = myServer; Database = myDB", $username, $password);
    $conn->setAttribute(PDO::SQLSRV_ATTR_FORMAT_DECIMALS, true);
    $conn->setAttribute(PDO::SQLSRV_ATTR_DECIMAL_PLACES, 2);
    ```

## Example - format money data
The following example shows how to fetch money data using [PDOStatement::bindColumn](../../connect/php/pdostatement-bindcolumn.md):

```php
<?php
$database = "myDB";
$server = "(local)";
$conn = new PDO( "sqlsrv:server=$server; Database = $database", "", "");
$conn->setAttribute(PDO::SQLSRV_ATTR_FORMAT_DECIMALS, true);

$numDigits = 3;
$query = "SELECT smallmoney1 FROM aTable";
$options = array(PDO::SQLSRV_ATTR_DECIMAL_PLACES => $numDigits);
$stmt = $conn->prepare($query, $options);
$stmt->execute();

$stmt->bindColumn('smallmoney1', $field);
$result = $stmt->fetch(PDO::FETCH_BOUND);
echo $field;    // expect a number string with 3 decimal places

unset($stmt);
unset($conn);
?>
```

## Example - override connection attributes
The following example shows how to override the connection attributes:

```php
<?php
$database = 'myDatabase';
$server = 'myServer';
$username = 'myuser';
$password = 'mypassword'

$conn = new PDO("sqlsrv:server=$server; Database = $database", $username, $password);
$conn->setAttribute(PDO::SQLSRV_ATTR_FORMAT_DECIMALS, true);
$conn->setAttribute(PDO::SQLSRV_ATTR_DECIMAL_PLACES, 2);

$query = 'SELECT smallmoney1 FROM testTable1';
$options = array(PDO::SQLSRV_ATTR_FORMAT_DECIMALS => false);
$stmt = $conn->prepare($query, $options);
$stmt->execute();

$stmt->bindColumn('smallmoney1', $field);
$result = $stmt->fetch(PDO::FETCH_BOUND);  
echo $field;   // expect a number string showing the original scale -- 4 decimal places

unset($stmt);
unset($conn);
?>
```

## See Also
[Formatting Decimal Strings and Money Values](../../connect/php/formatting-decimal-strings-and-money-values.md)

[Retrieving Data](../../connect/php/retrieving-data.md)
