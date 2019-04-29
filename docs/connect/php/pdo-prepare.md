---
title: "PDO::prepare | Microsoft Docs"
ms.custom: ""
ms.date: "04/22/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: a8b16fdc-c748-49be-acf2-a6ac7432d16b
author: MightyPen
ms.author: genemi
manager: craigg
---
# PDO::prepare
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Prepares a statement for execution.

## Syntax

```
PDOStatement PDO::prepare ( $statement [, array(key_pair)] )
```

#### Parameters
$*statement*: A string containing the SQL statement.

*key_pair*: An array containing an attribute name and value. See the Remarks section for more information.

## Return Value
Returns a PDOStatement object on success. On failure, returns a PDOException object, or false depending on the value of `PDO::ATTR_ERRMODE`.

## Remarks
The [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] does not evaluate prepared statements until execution.

The following table lists the possible *key_pair* values.

|Key|Description|
|-------|---------------|
|PDO::ATTR_CURSOR|Specifies cursor behavior. The default is `PDO::CURSOR_FWDONLY`, a non-scrollable forward cursor. `PDO::CURSOR_SCROLL` is a scrollable cursor.<br /><br />For example, `array( PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY )`.<br /><br />When set to `PDO::CURSOR_SCROLL`, you can then use `PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE` to set the type of scrollable cursor, which is described below.<br /><br />See [Cursor Types &#40;PDO_SQLSRV Driver&#41;](../../connect/php/cursor-types-pdo-sqlsrv-driver.md) for more information about result sets and cursors in the PDO_SQLSRV driver.|
|PDO::ATTR_EMULATE_PREPARES|By default, this attribute is false, which can be changed by this `PDO::ATTR_EMULATE_PREPARES => true`. See [Emulate Prepare](#emulate-prepare) for details and example.|
|PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE|Specifies the type of scrollable cursor. Only valid when `PDO::ATTR_CURSOR` is set to `PDO::CURSOR_SCROLL`. See below for the values this attribute can take.|
|PDO::SQLSRV_ATTR_ENCODING|PDO::SQLSRV_ENCODING_UTF8 (default)<br /><br />PDO::SQLSRV_ENCODING_SYSTEM<br /><br />PDO::SQLSRV_ENCODING_BINARY|
|PDO::SQLSRV_ATTR_DIRECT_QUERY|When True, specifies direct query execution. False means prepared statement execution. For more information about `PDO::SQLSRV_ATTR_DIRECT_QUERY`, see [Direct Statement Execution and Prepared Statement Execution in the PDO_SQLSRV Driver](../../connect/php/direct-statement-execution-prepared-statement-execution-pdo-sqlsrv-driver.md).|
|PDO::SQLSRV_ATTR_QUERY_TIMEOUT|For more information, see [PDO::setAttribute](../../connect/php/pdo-setattribute.md).|

When using `PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL`, you can use `PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE` to specify the type of cursor. For example, pass the following array to PDO::prepare to set a dynamic cursor:
```
array(PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL, PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE => PDO::SQLSRV_CURSOR_DYNAMIC));
```
The following table shows the possible values for `PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE`. For more information about scrollable cursors, see [Cursor Types &#40;PDO_SQLSRV Driver&#41;](../../connect/php/cursor-types-pdo-sqlsrv-driver.md).

|Value|Description|
|---------|---------------|
|PDO::SQLSRV_CURSOR_BUFFERED|Creates a client-side (buffered) static cursor, which buffers the result set in memory on the client machine.|
|PDO::SQLSRV_CURSOR_DYNAMIC|Creates a server-side (unbuffered) dynamic cursor, which lets you access rows in any order and will reflect changes in the database.|
|PDO::SQLSRV_CURSOR_KEYSET_DRIVEN|Creates a server-side keyset cursor. A keyset cursor does not update the row count if a row is deleted from the table (a deleted row is returned with no values).|
|PDO::SQLSRV_CURSOR_STATIC|Creates a server-side static cursor, which lets you access rows in any order but will not reflect changes in the database.<br /><br />`PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL` implies `PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE => PDO::SQLSRV_CURSOR_STATIC`.|

You can close a PDOStatement object by calling `unset`:
```
unset($stmt);
```

## Example
This example shows how to use PDO::prepare with parameter markers and a forward-only cursor.

```
<?php
$database = "Test";
$server = "(local)";
$conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");

$col1 = 'a';
$col2 = 'b';

$query = "insert into Table1(col1, col2) values(?, ?)";
$stmt = $conn->prepare( $query, array( PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY, PDO::SQLSRV_ATTR_QUERY_TIMEOUT => 1  ) );
$stmt->execute( array( $col1, $col2 ) );
print $stmt->rowCount();
echo "\n";

$query = "insert into Table1(col1, col2) values(:col1, :col2)";
$stmt = $conn->prepare( $query, array( PDO::ATTR_CURSOR => PDO::CURSOR_FWDONLY, PDO::SQLSRV_ATTR_QUERY_TIMEOUT => 1  ) );
$stmt->execute( array( ':col1' => $col1, ':col2' => $col2 ) );
print $stmt->rowCount();

unset($stmt);
?>
```

## Example
This example shows how to use PDO::prepare with a server-side static cursor. For an example showing a client-side cursor, see [Cursor Types &#40;PDO_SQLSRV Driver&#41;](../../connect/php/cursor-types-pdo-sqlsrv-driver.md).

```
<?php
$database = "AdventureWorks";
$server = "(local)";
$conn = new PDO( "sqlsrv:server=$server ; Database = $database", "", "");

$query = "select * from Person.ContactType";
$stmt = $conn->prepare( $query, array(PDO::ATTR_CURSOR => PDO::CURSOR_SCROLL));
$stmt->execute();

echo "\n";

while ( $row = $stmt->fetch( PDO::FETCH_ASSOC ) ){
   print "$row[Name]\n";
}
echo "\n..\n";

$row = $stmt->fetch( PDO::FETCH_BOTH, PDO::FETCH_ORI_FIRST );
print_r($row);

$row = $stmt->fetch( PDO::FETCH_ASSOC, PDO::FETCH_ORI_REL, 1 );
print "$row[Name]\n";

$row = $stmt->fetch( PDO::FETCH_NUM, PDO::FETCH_ORI_NEXT );
print "$row[1]\n";

$row = $stmt->fetch( PDO::FETCH_NUM, PDO::FETCH_ORI_PRIOR );
print "$row[1]..\n";

$row = $stmt->fetch( PDO::FETCH_NUM, PDO::FETCH_ORI_ABS, 0 );
print_r($row);

$row = $stmt->fetch( PDO::FETCH_NUM, PDO::FETCH_ORI_LAST );
print_r($row);
?>
```

<a name="emulate-prepare" />

## Example

This example shows how to use PDO::prepare with `PDO::ATTR_EMULATE_PREPARES` set to true.

```
<?php
$serverName = "yourservername";
$username = "yourusername";
$password = "yourpassword";
$database = "tempdb";
$conn = new PDO("sqlsrv:server = $serverName; Database = $database", $username, $password);

$pdo_options = array();
$pdo_options[PDO::ATTR_EMULATE_PREPARES] = true;
$pdo_options[PDO::SQLSRV_ATTR_ENCODING] = PDO::SQLSRV_ENCODING_UTF8;

$stmt = $conn->prepare("CREATE TABLE TEST([id] [int] IDENTITY(1,1) NOT NULL,
                                          [name] nvarchar(max))",
                                          $pdo_options);
$stmt->execute();

$prefix = '가각';
$name = '가각ácasa';
$name2 = '가각sample2';

$stmt = $conn->prepare("INSERT INTO TEST(name) VALUES(:p0)", $pdo_options);
$stmt->execute(['p0' => $name]);
unset($stmt);

$stmt = $conn->prepare("SELECT * FROM TEST WHERE NAME LIKE :p0", $pdo_options);
$stmt->execute(['p0' => "$prefix%"]);
foreach ($stmt as $row) {
    echo "\n" . 'FOUND: ' . $row['name'];
}

unset($stmt);
unset($conn);
?>
```

The PDO_SQLSRV driver internally replaces all the placeholders with the parameters that are bound by [PDOStatement::bindParam()](../../connect/php/pdostatement-bindparam.md). Therefore, a SQL query string with no placeholders is sent to the server. Consider this example,

```
$statement = $PDO->prepare("INSERT into Customers (CustomerName, ContactName) VALUES (:cus_name, :con_name)");
$statement->bindParam(:cus_name, "Cardinal");
$statement->bindParam(:con_name, "Tom B. Erichsen");
$statement->execute();
```

With `PDO::ATTR_EMULATE_PREPARES` set to false (the default case), the data sent to the database is:

```
"INSERT into Customers (CustomerName, ContactName) VALUES (:cus_name, :con_name)"
Information on :cus_name parameter
Information on :con_name parameter
```

The server will execute the query using its parameterized query feature for binding parameters. On the other hand, with `PDO::ATTR_EMULATE_PREPARES` set to true, the query sent to the server is essentially:

```
"INSERT into Customers (CustomerName, ContactName) VALUES ('Cardinal', 'Tom B. Erichsen')"
```

Setting `PDO::ATTR_EMULATE_PREPARES` to true can bypass some restrictions in SQL Server. For example, SQL Server does not support named or positional parameters in some Transact-SQL clauses. Moreover, SQL Server has a limit of binding 2100 parameters.

> [!NOTE]
> With emulate prepares set to true, the security of parameterized queries is not in effect. Therefore, your application should ensure that the data that is bound to the parameter(s) does not contain malicious Transact-SQL code.

### Encoding

If user wishes to bind parameters with different encodings (for instance, UTF-8 or binary), user should clearly specify the encoding in the PHP script.

The PDO_SQLSRV driver first checks the encoding specified in `PDO::bindParam()` (for example, `$statement->bindParam(:cus_name, "Cardinal", PDO::PARAM_STR, 10, PDO::SQLSRV_ENCODING_UTF8)`).

If not found, the driver checks if any encoding is set in `PDO::prepare()` or `PDOStatement::setAttribute()`. Otherwise, the driver will use the encoding specified in `PDO::__construct()` or `PDO::setAttribute()`.

### Limitations

As you can see, binding is done internally by the driver. A valid query is sent to the server for execution without any parameter. Compared to the regular case, some limitations result when the parameterized query feature is not in use.

- It does not work for parameters that are bound as `PDO::PARAM_INPUT_OUTPUT`.
    - When the user specifies `PDO::PARAM_INPUT_OUTPUT` in `PDO::bindParam()`, a PDO exception is thrown.
- It does not work for parameters that are bound as output parameters.
    - When the user creates a prepared statement with placeholders that are meant for output parameters (that is, having an equal sign immediately after a placeholder, like `SELECT ? = COUNT(*) FROM Table1`), a PDO exception is thrown.
    - When a prepared statement invokes a stored procedure with a placeholder as the argument for an output parameter, no exception is thrown because the driver cannot detect the output parameter. However, the variable that the user provides for the output parameter will remain unchanged.
- Duplicated placeholders for a binary encoded parameter will not work

## See Also
[PDO Class](../../connect/php/pdo-class.md)

[PDO](https://php.net/manual/book.pdo.php)

