---
title: How to Use the bcp Utility
description: Learn how to use the bulk copy program (bcp) to bulk copy data between an instance of SQL Server and a data file in a user-specified format.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: davidengel
ms.date: 06/25/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: how-to
ms.collection:
  - data-tools
ms.custom:
  - peer-review-program
helpviewer_keywords:
  - "bcp utility [SQL Server], examples"
  - "bulk copy [SQL Server], examples"
  - "exporting data, bcp utility"
  - "importing data, bcp utility"
  - "copying data [SQL Server], bcp utility"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =fabric-sqldb || =fabric"
---
# How to use the bcp utility

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW FabricDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

The bulk copy program utility (**bcp**) bulk copies data between an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and a data file in a user-specified format.

## Remarks

- For information about where to find or how to run the **bcp** utility and about the command prompt utilities syntax conventions, see [SQL command-line utilities (Database Engine)](../command-prompt-utility-reference-database-engine.md).

- For information on preparing data for bulk import or export operations, see [Prepare data for bulk export or import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md).

- For information about when row-insert operations that are performed by bulk import are logged in the transaction log, see [Prerequisites for minimal logging in bulk import](../../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md).

- The characters `<`, `>`, `|`, `&`, and `^` are special command shell characters, and they must be preceded by the escape character (`^`), or enclosed in quotation marks when used in a string (for example, `"StringContaining&Symbol"`). If you use quotation marks to enclose a string that contains one of the special characters, the quotation marks are set as part of the environment variable value. For more information, see [Using additional special characters](/windows-server/administration/windows-commands/set_1#remarks).

- **bcp** is currently in preview in [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)].
- **bcp** can't import data in [!INCLUDE [fabric-se](../../includes/fabric-se.md)].

## Native data file support

In [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], the **bcp** utility supports native data files compatible with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions starting with [!INCLUDE [ssVersion2000](../../includes/ssversion2000-md.md)] and later.

## Computed columns and timestamp columns

Values in the data file being imported for computed or **timestamp** columns are ignored, and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically assigns values. If the data file doesn't contain values for the computed or **timestamp** columns in the table, use a format file to specify that the computed or **timestamp** columns in the table should be skipped when importing data; [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically assigns values for the column.

Computed and **timestamp** columns are bulk copied from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to a data file as usual.

## Specify identifiers that contain spaces or quotation marks

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] identifiers can include characters such as embedded spaces and quotation marks. Such identifiers must be treated as follows:

- When you specify an identifier or file name that includes a space or quotation mark at the command prompt, enclose the identifier in quotation marks ("").

  For example, the following `bcp out` command creates a data file named `Currency Types.dat`:

  ```console
  bcp AdventureWorks2022.Sales.Currency out "Currency Types.dat" -T -c
  ```

- To specify a database name that contains a space or quotation mark, you must use the `-q` option.

- For owner, table, or view names that contain embedded spaces or quotation marks, you can either:

  - Specify the `-q` option, or

  - Enclose the owner, table, or view name in brackets (`[]`) inside the quotation marks.

## Data validation

**bcp** now enforces data validation and data checks that can cause scripts to fail if they're executed on invalid data in a data file. For example, **bcp** now verifies that:

- The native representations of float or real data types are valid.

- Unicode data has an even-byte length.

Forms of invalid data that could be bulk imported in earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can fail to load now; whereas, in earlier versions, the failure didn't occur until a client tried to access the invalid data. The added validation minimizes surprises when querying the data after bulkload.

## Bulk exporting or importing SQLXML documents

To bulk export or import SQLXML data, use one of the following data types in your format file.

| Data type | Effect |
| --- | --- |
| `SQLCHAR` or `SQLVARYCHAR` | The data is sent in the client code page or in the code page implied by the collation. The effect is the same as specifying the `-c` switch without specifying a format file. |
| `SQLNCHAR` or `SQLNVARCHAR` | The data is sent as Unicode. The effect is the same as specifying the `-w` switch without specifying a format file. |
| `SQLBINARY` or `SQLVARYBIN` | The data is sent without any conversion. |

## Character mode (`-c`) and native mode (`-n`) best practices

This section has recommendations for character mode (`-c`) and native mode (`-n`).

- (Administrator/User) When possible, use native format (`-n`) to avoid the separator issue. Use the native format to export and import using [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Export data from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using the `-c` or `-w` option if you plan to export the data to a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database.

- (Administrator) Verify data when using `bcp out`. For example, when you use `bcp out`, `bcp in`, and then `bcp out` verify that the data is properly exported and the terminator values aren't used as part of some data value. Consider overriding the default terminators (using `-t` and `-r` options) with random hexadecimal values to avoid conflicts between terminator values and data values.

- (User) Use a long and unique terminator (any sequence of bytes or characters) to minimize the possibility of a conflict with the actual string value. This can be done by using the `-t` and `-r` options.

## Examples

The examples in this section make use of the `WideWorldImporters` sample database for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, Azure SQL Database, and Azure SQL Managed Instance. `WideWorldImporters` can be downloaded from <https://github.com/Microsoft/sql-server-samples/releases/tag/wide-world-importers-v1.0>. See [RESTORE Statements](../../t-sql/statements/restore-statements-transact-sql.md) for the syntax to restore the sample database.

### Example test conditions

Except where specified otherwise, the examples assume that you use Windows Authentication and have a trusted connection to the server instance on which you're running the **bcp** command. A directory named `D:\bcp` is used in many of the examples. Replace `<server_name>` and other placeholder values with values for your environment.

The following Transact-SQL script creates an empty copy of the `WideWorldImporters.Warehouse.StockItemTransactions` table and then adds a primary key constraint:

```sql
USE WideWorldImporters;
GO

SET NOCOUNT ON;

IF NOT EXISTS (SELECT *
               FROM sys.tables
               WHERE name = 'Warehouse.StockItemTransactions_bcp')
    BEGIN
        SELECT *
        INTO WideWorldImporters.Warehouse.StockItemTransactions_bcp
        FROM WideWorldImporters.Warehouse.StockItemTransactions
        WHERE 1 = 2;

        ALTER TABLE Warehouse.StockItemTransactions_bcp
        ADD CONSTRAINT PK_Warehouse_StockItemTransactions_bcp
            PRIMARY KEY NONCLUSTERED (StockItemTransactionID ASC);
    END
```

You can truncate the `StockItemTransactions_bcp` table as needed:

```sql
TRUNCATE TABLE WideWorldImporters.Warehouse.StockItemTransactions_bcp;
```

### A. Identify bcp utility version

At a command prompt, enter the following command:

```console
bcp -v
```

### B. Copy table rows into a data file (with a trusted connection)

The following examples illustrate the `out` option on the `WideWorldImporters.Warehouse.StockItemTransactions` table.

- **Basic**

  This example creates a data file named `StockItemTransactions_character.bcp` and copies the table data into it using *character* format.

  At a command prompt, enter the following command:

  ```console
  bcp WideWorldImporters.Warehouse.StockItemTransactions out D:\bcp\StockItemTransactions_character.bcp -c -T
  ```

- **Expanded**

  This example creates a data file named `StockItemTransactions_native.bcp` and copies the table data into it using the *native* format. The example also: specifies the maximum number of syntax errors, an error file, and an output file.

  At a command prompt, enter the following command:

  ```console
  bcp WideWorldImporters.Warehouse.StockItemTransactions OUT D:\bcp\StockItemTransactions_native.bcp -m 1 -n -e D:\bcp\Error_out.log -o D:\bcp\Output_out.log -S <server_name> -T
  ```

Review `Error_out.log` and `Output_out.log`. `Error_out.log` should be blank. Compare the file sizes between `StockItemTransactions_character.bcp` and `StockItemTransactions_native.bcp`.

### C. Copy table rows into a data file (with mixed-mode authentication)

The following example illustrates the `out` option on the `WideWorldImporters.Warehouse.StockItemTransactions` table. This example creates a data file named `StockItemTransactions_character.bcp` and copies the table data into it using **character** format.

The example assumes that you use mixed-mode authentication, and you must use the `-U` switch to specify your login ID. Also, unless you're connecting to the default instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the local computer, use the `-S` switch to specify the system name and, optionally, an instance name.

At a command prompt, enter the following command: (The system prompts you for your password.)

```console
bcp WideWorldImporters.Warehouse.StockItemTransactions out D:\bcp\StockItemTransactions_character.bcp -c -U<login_id> -S<server_name\instance_name>
```

### D. Copy data from a file to a table

The following examples illustrate the `in` option on the `WideWorldImporters.Warehouse.StockItemTransactions_bcp` table using files created previously.

- **Basic**

  This example uses the `StockItemTransactions_character.bcp` data file previously created.

  At a command prompt, enter the following command:

  ```console
  bcp WideWorldImporters.Warehouse.StockItemTransactions_bcp IN D:\bcp\StockItemTransactions_character.bcp -c -T
  ```

- **Expanded**

  This example uses the `StockItemTransactions_native.bcp` data file previously created. The example also uses the `TABLOCK` hint, and specifies the batch size, the maximum number of syntax errors, an error file, and an output file.

  At a command prompt, enter the following command:

  ```console
  bcp WideWorldImporters.Warehouse.StockItemTransactions_bcp IN D:\bcp\StockItemTransactions_native.bcp -b 5000 -h "TABLOCK" -m 1 -n -e D:\bcp\Error_in.log -o D:\bcp\Output_in.log -S <server_name> -T
  ```

  Review `Error_in.log` and `Output_in.log`.

### E. Copy a specific column into a data file

To copy a specific column, you can use the `queryout` option. The following example copies only the `StockItemTransactionID` column of the `Warehouse.StockItemTransactions` table into a data file.

At a command prompt, enter the following command:

```console
bcp "SELECT StockItemTransactionID FROM WideWorldImporters.Warehouse.StockItemTransactions WITH (NOLOCK)" queryout D:\bcp\StockItemTransactionID_c.bcp -c -T
```

### F. Copy a specific row into a data file

To copy a specific row, you can use the `queryout` option. The following example copies only the row for the person named `Amy Trefl` from the `WideWorldImporters.Application.People` table into a data file `Amy_Trefl_c.bcp`.

> [!NOTE]  
> The `-d` switch is used to identify the database.

At a command prompt, enter the following command:

```console
bcp "SELECT * from Application.People WHERE FullName = 'Amy Trefl'" queryout D:\bcp\Amy_Trefl_c.bcp -d WideWorldImporters -c -T
```

### G. Copy data from a query to a data file

To copy the result set from a Transact-SQL statement to a data file, use the `queryout` option. The following example copies the names from the `WideWorldImporters.Application.People` table, ordered by full name, into the `People.txt` data file.

> [!NOTE]  
> The `-t` switch is used to create a comma-delimited file.

At a command prompt, enter the following command:

```console
bcp "SELECT FullName, PreferredName FROM WideWorldImporters.Application.People ORDER BY FullName" queryout D:\bcp\People.txt -t, -c -T
```

### H. Create format files

The following example creates three different format files for the `Warehouse.StockItemTransactions` table in the `WideWorldImporters` database. Review the contents of each created file.

At a command prompt, enter the following commands:

```console
REM non-XML character format
bcp WideWorldImporters.Warehouse.StockItemTransactions format nul -f D:\bcp\StockItemTransactions_c.fmt -c -T

REM non-XML native format
bcp WideWorldImporters.Warehouse.StockItemTransactions format nul -f D:\bcp\StockItemTransactions_n.fmt -n -T

REM XML character format
bcp WideWorldImporters.Warehouse.StockItemTransactions format nul -f D:\bcp\StockItemTransactions_c.xml -x -c -T
```

> [!NOTE]  
> The `-x` switch is supported only on Windows.

For more information, see [Use non-XML format files (SQL Server)](../../relational-databases/import-export/non-xml-format-files-sql-server.md) and [XML format files (SQL Server)](../../relational-databases/import-export/xml-format-files-sql-server.md).

### I. Use a format file to bulk import with bcp

To use a previously created format file when importing data into an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], use the `-f` switch with the `in` option. For example, the following command bulk copies the contents of a data file, `StockItemTransactions_character.bcp`, into a copy of the `Warehouse.StockItemTransactions_bcp` table by using the previously created format file, `StockItemTransactions_c.xml`.

> [!NOTE]  
> The `-L` switch is used to import only the first 100 records.

At a command prompt, enter the following command:

```console
bcp WideWorldImporters.Warehouse.StockItemTransactions_bcp in D:\bcp\StockItemTransactions_character.bcp -L 100 -f D:\bcp\StockItemTransactions_c.xml -T
```

> [!NOTE]  
> Format files are useful when the data file fields are different from the table columns; for example, in their number, ordering, or data types. For more information, see [Format files to import or export data (SQL Server)](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).

### J. Specify a code page

The following partial code example shows **bcp** import while specifying a code page 65001:

```console
bcp MyTable in "D:\data.csv" -T -c -C 65001 -t , ...
```

### K. Example output file using a custom field and row terminators

This example shows two sample files, generated by **bcp** using custom field and row terminators.

1. Create a table `dbo.T1` in the `tempdb` database, with two columns, `ID` and `Name`.

   ```sql
   USE tempdb;
   GO

   CREATE TABLE dbo.T1
   (
       ID INT,
       [Name] NVARCHAR (20)
   );
   GO

   INSERT INTO dbo.T1 VALUES (1, N'Natalia');
   INSERT INTO dbo.T1 VALUES (2, N'Mark');
   INSERT INTO dbo.T1 VALUES (3, N'Randolph');
   GO
   ```

1. Generate an output file from the example table `dbo.T1`, using a custom field terminator.

   In this example, `-t ,` specifies the custom field terminator. Replace `<server_name>` with a value for your environment.

   ```console
   bcp dbo.T1 out T1.txt -T -S <server_name> -d tempdb -w -t ,
   ```

   [!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

   ```output
   1,Natalia
   2,Mark
   3,Randolph
   ```

1. Generate an output file from the example table `dbo.T1`, using a custom field terminator and custom row terminator.

   In this example, `-t ,` specifies the custom field terminator, and `-r :` specifies the custom row terminator. Replace `<server_name>` with a value for your environment.

   ```console
   bcp dbo.T1 out T1.txt -T -S <server_name> -d tempdb -w -t , -r :
   ```

   [!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

   ```output
   1,Natalia:2,Mark:3,Randolph:
   ```

   > [!NOTE]  
   > The row terminator is always added, even to the last record. The field terminator, however, isn't added to the last field.

## Additional examples

The following articles contain examples of using **bcp**:

- Data formats for bulk import or bulk export (SQL Server)

  - [Use native format to import or export data (SQL Server)](../../relational-databases/import-export/use-native-format-to-import-or-export-data-sql-server.md)
  - [Use character format to import or export data (SQL Server)](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)
  - [Use Unicode Native Format to Import or Export Data (SQL Server)](../../relational-databases/import-export/use-unicode-native-format-to-import-or-export-data-sql-server.md)
  - [Use Unicode character format to import or export data (SQL Server)](../../relational-databases/import-export/use-unicode-character-format-to-import-or-export-data-sql-server.md)

- [Specify field and row terminators (SQL Server)](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md)

- [Keep nulls or default values during bulk import (SQL Server)](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)

- [Keep identity values when bulk importing data (SQL Server)](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)

- Format files for importing or exporting data (SQL Server)

  - [Create a format file with bcp (SQL Server)](../../relational-databases/import-export/create-a-format-file-sql-server.md)
  - [Use a format file to bulk import data (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)
  - [Use a format file to skip a table column (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)
  - [Use a format file to skip a data field (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)
  - [Use a format file to map table columns to data-file fields (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)

- [Examples of bulk import and export of XML documents (SQL Server)](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)

## Related content

- [Prepare data for bulk export or import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md)
- [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md)
- [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md)
- [SET QUOTED_IDENTIFIER (Transact-SQL)](../../t-sql/statements/set-quoted-identifier-transact-sql.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [sp_tableoption (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md)
- [Format files to import or export data (SQL Server)](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md)

[!INCLUDE [get-help-options](../../includes/paragraph-content/get-help-options.md)]
