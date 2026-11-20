---
title: "Declare input string types and lengths (PDO_SQLSRV)"
titleSuffix: Microsoft Drivers for PHP for SQL Server
description: "Declare PDO_SQLSRV input string types and lengths, or opt in to adaptive sizing."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 11/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---
# Declare input string types and lengths (PDO_SQLSRV)

Use explicit declarations to send an input string as **varchar** or **nvarchar** with a specified SQL length. Alternatively, opt in to adaptive sizing to start with a chosen length and grow the declaration when you send longer values.

These options require version 5.14.0 or later and apply only to PDO_SQLSRV, the PHP Data Objects (PDO) driver. For SQLSRV, continue to specify SQL types in the parameter array described in [Specify SQL Server data types](how-to-specify-sql-server-data-types-when-using-the-sqlsrv-driver.md). Neither approach changes the destination column's definition.

## Choose a declaration method

| Method | Configuration | Behavior |
| --- | --- | --- |
| Explicit statement metadata | Call `PDOStatement::setAttribute(PDO::SQLSRV_ATTR_PARAM_METADATA, $metadata)` after `prepare`. | Applies to `bindParam`, `bindValue`, and `execute($params)`. |
| Explicit binding metadata | Pass a declaration array as the fifth argument of `bindParam`. | Replaces the entire statement metadata entry for that binding, including its optional encoding. |
| Adaptive sizing | Set `PDO::SQLSRV_ATTR_DEFAULT_STR_LENGTH` on the connection before creating the statement. | Applies to eligible input strings without an explicit declaration. |

Without explicit metadata or adaptive sizing, existing parameter sizing behavior remains unchanged. You don't need these options for ordinary parameterized queries.

## Declare an input string

Each declaration is an array with integer constant keys and integer values.

| Key | Value |
| --- | --- |
| `PDO::SQLSRV_PARAM_DECLARED_STR_TYPE` | `PDO::SQLSRV_PARAM_DECLARED_STR_TYPE_VARCHAR` or `PDO::SQLSRV_PARAM_DECLARED_STR_TYPE_NVARCHAR`. |
| `PDO::SQLSRV_PARAM_DECLARED_STR_LENGTH` | For **varchar**, 1 through 8,000 bytes. For **nvarchar**, 1 through 4,000 code units in 16-bit Unicode Transformation Format (UTF-16). Use `PDO::SQLSRV_PARAM_DECLARED_STR_LENGTH_MAX` for **varchar(max)** or **nvarchar(max)**. |
| `PDO::SQLSRV_ATTR_ENCODING` (optional) | The character encoding of the PHP input string. For input in 8-bit Unicode Transformation Format (UTF-8), use `PDO::SQLSRV_ENCODING_UTF8` or `PDO::SQLSRV_ENCODING_UTF8_VARCHAR`. The declared type, not the encoding, sets the SQL type. You can also use `PDO::SQLSRV_ENCODING_SYSTEM` for input in the system encoding. Binary encoding isn't supported. |

You must include both declaration keys. Numeric strings, Boolean values, zero lengths, and unrecognized keys are invalid. A supplementary Unicode character, such as `"\u{1F600}"`, occupies two UTF-16 code units, even though PHP stores its UTF-8 representation in four bytes.

The encoding describes the source bytes. The declared type describes the SQL parameter sent to the server. For example, you can send UTF-8 input as **varchar** without changing the source bytes to the system encoding. Conversion to **varchar** still depends on the SQL [collation](../../relational-databases/collations/collation-and-unicode-support.md) and its character repertoire. An explicit SQL length doesn't allocate a PHP buffer.

When the driver declares a bound parameter for a prepared statement, it ignores the `PDO::PARAM_STR_NATL` and `PDO::PARAM_STR_CHAR` flags. Use a declaration to choose **varchar** or **nvarchar**.

For statement metadata, use a map from parameter identifiers to declaration arrays:

- Use 1-based positions for question mark placeholders, such as `[1 => $declaration]`. Positions must be integers from 1 through 2,100 and must identify bound parameters when you execute.
- Use a unique placeholder name, with or without its colon, such as `['name' => $declaration]` or `[':name' => $declaration]`. A repeated placeholder name doesn't identify a unique position. Don't specify the same position twice through different names or numeric keys.
- Set metadata after `prepare`, not in its options array. Each successful setter replaces the entire map and resets adaptive size history. An invalid replacement preserves the previous map. Pass `[]` to clear the map; `null` is invalid.
- Call `getAttribute(PDO::SQLSRV_ATTR_PARAM_METADATA)` on the statement to retrieve the map normalized to 1-based positions. It returns statement metadata, not per-binding overrides or adaptive size history.

An array passed to `execute` supplies values, not declarations. Its positional values use the usual zero-based PHP array, such as `['text']`; its metadata map still uses `[1 => $declaration]`.

For `bindParam`, keep the fourth argument at `0` for an input declaration. A positive fourth argument selects output-buffer behavior; it isn't the new SQL declaration length. Integer encoding options in the fifth argument continue to work. A declaration array overrides the whole statement entry for that binding, whereas an integer encoding option overrides only the encoding. Rebinding without the declaration array removes that binding's override, but retains statement metadata.

### Example: explicit declarations

The examples in this article assume an open `PDO` connection in `$conn` that uses `PDO::ERRMODE_EXCEPTION`. To open one, see [Connecting to the server](connecting-to-the-server.md). They use the following table:

```sql
CREATE TABLE dbo.Customers
(
    CustomerID int IDENTITY PRIMARY KEY,
    Name nvarchar(100) NOT NULL,
    Code varchar(10) NOT NULL
);
```

Without a declaration, the driver sends each string value in the following examples as **nvarchar(4000)**. Each example declares the type and length of the matching column instead.

The following example uses named parameters, statement metadata, and an array passed to `execute`. The server receives `@P1 nvarchar(100)` and `@P2 varchar(10)`.

```php
$stmt = $conn->prepare('INSERT INTO dbo.Customers (Name, Code) VALUES (:name, :code)');
$stmt->setAttribute(PDO::SQLSRV_ATTR_PARAM_METADATA, [
    'name' => [
        PDO::SQLSRV_PARAM_DECLARED_STR_TYPE => PDO::SQLSRV_PARAM_DECLARED_STR_TYPE_NVARCHAR,
        PDO::SQLSRV_PARAM_DECLARED_STR_LENGTH => 100,
    ],
    'code' => [
        PDO::SQLSRV_PARAM_DECLARED_STR_TYPE => PDO::SQLSRV_PARAM_DECLARED_STR_TYPE_VARCHAR,
        PDO::SQLSRV_PARAM_DECLARED_STR_LENGTH => 10,
    ],
]);
$stmt->execute(['name' => 'Zoë Martín', 'code' => 'C-1001']);
```

The following example uses a question mark placeholder and `bindValue` to find a customer by code. The metadata key `1` identifies the first placeholder. Declaring **varchar(10)** matches the **Code** column. When a **varchar** column is compared to an **nvarchar** parameter, SQL Server converts the column value, which can prevent an index seek.

```php
$stmt = $conn->prepare('SELECT CustomerID, Name FROM dbo.Customers WHERE Code = ?');
$stmt->setAttribute(PDO::SQLSRV_ATTR_PARAM_METADATA, [
    1 => [
        PDO::SQLSRV_PARAM_DECLARED_STR_TYPE => PDO::SQLSRV_PARAM_DECLARED_STR_TYPE_VARCHAR,
        PDO::SQLSRV_PARAM_DECLARED_STR_LENGTH => 10,
    ],
]);
$stmt->bindValue(1, 'C-1001', PDO::PARAM_STR);
$stmt->execute();
$customer = $stmt->fetch(PDO::FETCH_ASSOC);
echo $customer['Name'], "\n";
```

The following example passes a declaration array as the fifth argument of `bindParam`, with `0` as the fourth argument. The declared length stays fixed across executions. A value longer than 10 characters fails with SQLSTATE `22001` and inserts no row.

```php
$stmt = $conn->prepare('INSERT INTO dbo.Customers (Name, Code) VALUES (?, ?)');
$name = 'Kim Abercrombie';
$code = 'C-1002';
$stmt->bindParam(1, $name, PDO::PARAM_STR, 0, [
    PDO::SQLSRV_PARAM_DECLARED_STR_TYPE => PDO::SQLSRV_PARAM_DECLARED_STR_TYPE_NVARCHAR,
    PDO::SQLSRV_PARAM_DECLARED_STR_LENGTH => 100,
]);
$stmt->bindParam(2, $code, PDO::PARAM_STR, 0, [
    PDO::SQLSRV_PARAM_DECLARED_STR_TYPE => PDO::SQLSRV_PARAM_DECLARED_STR_TYPE_VARCHAR,
    PDO::SQLSRV_PARAM_DECLARED_STR_LENGTH => 10,
]);
$stmt->execute();

$name = 'Thomas Hardy';
$code = 'C-100000003';
try {
    $stmt->execute();
} catch (PDOException $error) {
    if ($error->getCode() !== '22001') {
        throw $error;
    }
    echo "The code is longer than 10 characters.\n";
}
```

## Opt in to adaptive sizing

Set `PDO::SQLSRV_ATTR_DEFAULT_STR_LENGTH` in the connection constructor's options array or with `PDO::setAttribute`. The value must be an integer from 0 through 8,000. The default, `0`, disables adaptive sizing.

Each new statement copies the connection setting. Changing it later affects new statements, not existing ones. This attribute isn't a statement or `prepare` option.

For each eligible parameter position, the driver starts with the configured floor and grows the declaration to fit longer values. It retains the largest size used at that position when you later send shorter, empty, or null values. For UTF-8 input with the default encoding, it uses **nvarchar** and measures growth in UTF-16 code units. For system-encoded input, it uses **varchar** and reserves three extra bytes for each non-ASCII byte, because conversion to a UTF-8 collation can lengthen the value. It promotes declarations beyond 4,000 **nvarchar** units or 8,000 **varchar** bytes to **max**, including when the initial floor exceeds the type's bounded limit.

Explicit declarations take precedence over adaptive sizing. Parameters bound as integers, binary data, streams, or output/input-output parameters retain their existing behavior. Changing the inferred SQL type by changing the encoding starts a new size history for that parameter.

### Example: adaptive sizing

The following example uses the table from the explicit-declaration example. It sets a floor of 20 on the connection before it prepares the statement. The first execution sends `@P1 nvarchar(20)` and `@P2 nvarchar(20)`. The second name has 30 characters, so the driver grows `@P1` to **nvarchar(30)**. Both parameters are **nvarchar** because the input is UTF-8, even though the **Code** column is **varchar**. To match a **varchar** column, use an explicit declaration.

```php
$conn->setAttribute(PDO::SQLSRV_ATTR_DEFAULT_STR_LENGTH, 20);
$stmt = $conn->prepare('INSERT INTO dbo.Customers (Name, Code) VALUES (?, ?)');
$stmt->execute(['Ana Trujillo', 'C-1003']);
$stmt->execute(['Hanna Moos-Fernández Bergström', 'C-1004']);
```

The size setting controls parameter declarations, not the destination column's capacity.

## Limitations and errors

- Explicit declarations support only input strings and null values bound with `PDO::PARAM_STR` or `PDO::PARAM_NULL`. They reject binary encoding, streams, other parameter types, and output/input-output parameters.
- In version 5.14.0, bounded explicit **varchar** accepts only American Standard Code for Information Interchange (ASCII) input. For non-ASCII input, use **nvarchar**, **varchar(max)** with a suitable collation, or adaptive sizing. Adaptive sizing doesn't remove collation conversion constraints.
- Explicit declarations and adaptive sizing aren't supported with Always Encrypted or [emulated prepares](pdo-prepare.md#prepare-example). A nonzero adaptive setting makes statement creation fail for either path. Disable it before creating those statements. Direct execution through `PDO::SQLSRV_ATTR_DIRECT_QUERY` is distinct from emulated prepares and supports these declarations.
- Invalid metadata, unsupported declaration paths, and invalid adaptive settings report SQLSTATE `IMSSP`. An input exceeding a valid bounded declaration reports `22001`. Handle errors according to `PDO::ATTR_ERRMODE`; the examples assume exception mode.
- The driver doesn't look up the destination column or rewrite your Transact-SQL to choose a declaration. Choose types and lengths that match your schema.

## Related content

- [PDOStatement::bindParam](pdostatement-bindparam.md)
- [PDOStatement::bindValue](pdostatement-bindvalue.md)
- [PDOStatement::execute](pdostatement-execute.md)
- [PDO::setAttribute](pdo-setattribute.md)
- [Constants](constants-microsoft-drivers-for-php-for-sql-server.md)
- [Driver implementation](https://github.com/microsoft/msphpsql/tree/v5.14.0/source/pdo_sqlsrv)
