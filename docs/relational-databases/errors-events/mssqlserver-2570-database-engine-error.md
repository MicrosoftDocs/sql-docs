---
title: "MSSQLSERVER_2570"
description: "MSSQLSERVER_2570"
author: MashaMSFT
ms.author: mathoma
ms.date: "07/27/2023"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "2570 (Database Engine error)"
---
# MSSQLSERVER_2570
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|2570|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_COLUMN_VALUE_OUT_OF_RANGE|  
|Message Text|Page P_ID, slot S_ID in object ID O_ID, index ID I_ID, partition ID PN_ID, alloc unit ID A_ID (type TYPE). Column COLUMN_NAME value is out of range for data type "DATATYPE". Update column to a legal value.|  
  
## Explanation

The column value that's contained in the specified column is outside the range of possible values for the column data type.  If you have invalid data in a table column, you might encounter problems, depending on the type of operations performed against the invalid data. However, it's also possible that no problem will appear, and the invalid data won't be discovered until you execute a `DBCC CHECKDB` or `DBCC CHECKTABLE` command.

Some symptoms you may notice due to the presence of invalid data include (but aren't limited to): 

- Access violations or other exceptions when executing queries against the affected column. 
- Incorrect results returned by queries executed against the affected column. 
- Errors or problems when statistics are being built against the affected column. 
- Error messages like the following one: 
  > Msg 9100, Level 23, State 2, Line \<LineNum\> Possible index corruption detected. Run DBCC CHECKDB. 

#### DATA_PURITY checks 

When you execute a [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) or [DBCC CHECKTABLE](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md) command, SQL Server performs "data purity" validations of column values in each row of every table in the database. These checks are performed to ensure that the values stored in the columns are valid. That is, the validation ensures the values aren't out-of-range of the domain associated with the data type of the columns. The nature of the validation performed depends on the data type of the column. The following non-exhaustive list gives some examples: 

|Column data type |Type of data validation performed |
|-|-|
|Unicode character |The data length should be a multiple of 2.|
|Datetime |The date field should be between January 1, 1753 and December 31, 9999. The time field must be earlier than "11:59:59.997PM." |
|Real and Float |Check for the existence of invalid floating-point values like SNAN, QNAN, NINF, ND, PD, and PINF.|

Not all data types are checked for the validity of the column data. Only those that may have an out-of-range stored value are checked. For example, the `tinyint` data type has a valid range of 0 to 255 and is stored in a single byte (which can only store values between 0 and 255), so checking the value isn't necessary. 

> [!NOTE]
> These checks are enabled by default and can't be disabled, so there's no need to explicitly use the [DATA_PURITY](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md#data_purity) option when executing a `DBCC CHECKDB` or `DBCC CHECKTABLE` command. However, if you use the [PHYSICAL_ONLY](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md#physical_only) option with `DBCC CHECKDB` or `DBCC CHECKTABLE`, data purity checks aren't performed.

#### DATA_PURITY problem report 

When you execute a `DBCC CHECKDB` or `DBCC CHECKTABLE` command with the `DATA_PURITY` option enabled (or the data purity checks are run automatically), and invalid data exists in the tables checked by the `DBCC` commands, the `DBCC` output will include other messages that indicate the problems related to the data. The following sample error messages indicate data purity problems:

```output
DBCC results for "account_history". 
Msg 2570, Level 16, State 2, Line <LineNum> 
Page (1:1073), slot 33 in object ID <ObjectID>, index ID 0, partition ID <PartitionID>, alloc unit ID <UnitID> (type "In-row data"). Column "account_name" value is out of range for data type "nvarchar". Update column to a legal value. 
 
Msg 2570, Level 16, State 2, Line <LineNum> 
Page (1:1156), slot 120 in object ID <ObjectID>, index ID 0, partition ID <PartitionID>, alloc unit ID <UnitID> (type "In-row data"). Column "account_name" value is out of range for data type "nvarchar". Update column to a legal value.
There are 153137 rows in 1080 pages for object "account_history". 
CHECKDB found 0 allocation errors and 338 consistency errors in table "account_history" (object ID <ObjectID>). 
CHECKDB found 0 allocation errors and 338 consistency errors in database '<DatabaseName>'. 
DBCC execution completed. If DBCC printed error messages, contact your system administrator. 

DBCC results for 'table1'. 
Msg 2570, Level 16, State 3, Line <LineNum> 
Page (1:154), slot 0 in object ID <ObjectID>, index ID 0, partition ID <PartitionID>, alloc unit ID <UnitID> (type "In-row data"). Column "col2" value is out of range for data type "real". Update column to a legal value. 
There are 4 rows in 2 pages for object "table1". 
CHECKDB found 0 allocation errors and 1 consistency errors in table 'table1' (object ID <ObjectID>). 
CHECKDB found 0 allocation errors and 1 consistency errors in database 'realdata'. DBCC execution completed. If DBCC printed error messages, contact your system administrator. 

DBCC results for 'table2'. 
Msg 2570, Level 16, State 3, Line <LineNum> 
Page (1:155), slot 0 in object ID <ObjectID>, index ID 0, partition ID <PartitionID>, alloc unit ID <UnitID> (type "In-row data"). Column "col2" value is out of range for data type "decimal". Update column to a legal value. 
There are 4 rows in 1 pages for object "table2". 
CHECKDB found 0 allocation errors and 1 consistency errors in table 'table2' (object ID <ObjectID>). 
CHECKDB found 0 allocation errors and 1 consistency errors in database 'realdata'. DBCC execution completed. If DBCC printed error messages, contact your system administrator. 

DBCC results for 'table3'. 
Msg 2570, Level 16, State 3, Line <LineNum> 
Page (1:157), slot 0 in object ID <ObjectID>, index ID 0, partition ID <PartitionID>, alloc unit ID <UnitID> (type "In-row data"). Column "col2" value is out of range for data type "datetime". Update column to a legal value. 
There are 3 rows in 1 pages for object "table3". 
CHECKDB found 0 allocation errors and 1 consistency errors in table 'table3' (object ID <ObjectID>). 
CHECKDB found 0 allocation errors and 1 consistency errors in database 'realdata'. DBCC execution completed. If DBCC printed error messages, contact your system administrator. 

For every row that contains an invalid column value, a 2570 error is generated. 
```

## Cause

Invalid or out-of-range data may have been stored in the SQL Server database for the following reasons:

- Invalid data was inserted into SQL Server through remote procedure call (RPC) events. 
- Other potential causes of physical data corruption made the column value invalid.

## Fix the data purity problem 

The 2570 errors can't be repaired using any of the `DBCC` repair options. The reason is that `DBCC` can't determine what value should be used to replace the invalid column value. Thus, the column value must be updated manually. To perform a manual update, you have to find the row that has the problem. Use one of the following methods to find the row:

- Execute a query against the table that contains the invalid values to find the rows that contain the invalid values. 
- Use the information from the error 2570 to identify the rows that have invalid values. 

Both methods are detailed in the following sections and provide examples to find the rows that have invalid data. 
 
Once you find the correct row, a decision needs to be made on the new value that will be used to replace the existing invalid data. This decision has to be made very carefully, based on the range of values applicable to the application and the logical meaning of that particular row of data. You have the following choices: 

- If you know what value it should be, set it to that specific value. 
- Set it to an acceptable default value. 
- Set the column value to `NULL`. 
- Set the column value to the maximum or minimum value for that data type of the column. 
- If you believe that the specific row isn't useful without a valid value for the column, delete that row altogether.

#### Find rows with invalid values using T-SQL queries

The type of query you need to execute to find rows that have invalid values depends on the data type of the column reporting a problem. If you look at the 2570 error message, you'll notice two important pieces of information that can help you with this problem. In the following example, the value of the column `account_name` is out-of-range for the data type `nvarchar`. We can easily identify the column with the problem and the data type of the column involved. Thus, once you know the data type and the column involved, you can formulate queries to find the rows that contain invalid values for that column, and select the columns needed to identify that row (as the predicates in a `WHERE` clause) for any further update or deletion. 

##### Unicode data type

```sql
SELECT col1, DATALENGTH(account_name) AS Length, account_name  
FROM account_history 
WHERE DATALENGTH(account_name) % 2 != 0
```

##### Float data type

Run the following code snippet by changing `col1` to your actual primary key column(s), `col2` to the column from the 2570 error, and `table1` to the table from the `CHECKDB` output.

```sql
SELECT col1, col2 FROM table1 
WHERE col2<>0.0 AND (col2 < 2.23E-308 OR col2 > 1.79E+308) AND (col2 < -1.79E+308 OR col2 > -2.23E-308)
```

##### Real data type

Run the following code snippet by changing `col1` to your actual primary key column(s), `col2` to the column from the 2570 error, and `table1` to the table from the `CHECKDB` output.

```sql
SELECT col1, col2 FROM testReal  
WHERE col2<>0.0 AND (col2 < CONVERT(real,1.18E-38) OR col2 > CONVERT(real,3.40E+38)) AND (col2 < CONVERT(real,-3.40E+38) OR col2 > CONVERT(real,-1.18E-38))  
ORDER BY col1; -- checks for real out of range 
```

##### Decimal and numeric data types

```sql
SELECT col1 FROM table2 
WHERE col2 > 9999999999.99999  
OR col1 < -9999999999.99999
```

Keep in mind that you need to adjust the values based on the precision and scale with which you have defined the `decimal` or `numeric` column. In the above example, the column is defined as `col2 decimal(15,5)`. 
 
##### Datetime data type

You need to execute two different queries to identify the rows that contain invalid values for the `datetime` column. 

```sql
SELECT col1 FROM table3 
WHERE col2 < '1/1/1753 12:00:00 AM' OR col2 > '12/31/9999 11:59:59 PM' 

SELECT col1 FROM table3 WHERE 
((DATEPART(ms,col2)+ (1000*DATEPART(s,col2)) + (1000*60*DATEPART(mi,col2)) + (1000*60*60*DATEPART(hh,col2)))/(1000*0.00333))  > 25919999
```

#### Find rows with invalid values using the physical location

You can use this method if you can't find the rows with invalid values by using the [T-SQL method](#find-rows-with-invalid-values-using-t-sql-queries). In the 2570 error message, the physical location of the row that contains the invalid value is printed. For example, look at the following message: 

```output
Page (1:157), slot 0 in object ID <ObjectID>, index ID 0, partition ID <PartitionID>, alloc unit ID <UnitID> (type "In-row data"). Column "col2" value is out of range for data type "datetime". Update column to a legal value. 
```

In this message, you notice `Page (1:157), slot 0`. It's the information you need to identify the row. The `FileId` is `1`, the `PageInFile` is `157`, and the `SlotId` is `0`.

Once you have this information, you need to execute the following command: 

```sql
DBCC TRACEON (3604)
DBCC PAGE (realdata , 1 , 157 , 3)
```

> [!NOTE]
> This command prints the entire content of a page. The parameters to the `DBCC PAGE` command are:
> - `Database name`: The name of the database.
> - `File number`: The file number of the database file.
> - `Page number`: The number of the page you want to examine.
> - `Print option`: An optional parameter that determines the level of output detail.

Once you execute this command, you'll notice an output that contains information similar to the following format: 

```output
Slot 0  Offset 0x60 Length 19
Record Type = PRIMARY_RECORD Record Attributes = NULL_BITMAP
Memory Dump @0x44D1C060
00000000: 10001000 01000000 ffffffff ffffffff †................
00000010: 0200fc†††††††††††††††††††††††††††††††...
Slot 0 Column 0  Offset 0x4 Length 4  col1 = 1
Slot 0 Column 1  Offset 0x8 Length 8  col2 = Dec 31 1899 19:04PM
Slot 1 Offset 0x73 Length 19
Record Type = PRIMARY_RECORD Record Attributes = NULL_BITMAP
Memory Dump @0x44D1C073
00000000: 10001000 02000000 0ba96301 f8970000 †..........c.....
00000010: 0200fc†††††††††††††††††††††††††††††††...
Slot 1 Column 0 Offset 0x4 Length 4 col1 = 2
Slot 1 Column 1 Offset 0x8 Length 8 col2 = Jul 8 2006 9:34PM
Slot 2 Offset 0x86 Length 19
Record Type = PRIMARY_RECORD Record Attributes = NULL_BITMAP
Memory Dump @0x44D1C086
00000000: 10001000 03000000 0ba96301 f8970000 †..........c.....
00000010: 0200fc†††††††††††††††††††††††††††††††... 
Slot 2 Column 0 Offset 0x4 Length 4 col1 = 3
Slot 2 Column 1 Offset 0x8 Length 8 col2 = Jul 8 2006 9:34PM
```

In this output, you can clearly see the column values for the row of interest. In this case, you need the row stored in `slot 0` of the page. From the error message, you know that `col2` is the one with the problem. So you can take the value of `col1` for `Slot 0` and use it as the predicate in the `WHERE` clause of your update statement or delete statement. 
 
> [!WARNING]
> We recommend that you use the first method (that is, use T-SQL queries to find the required information). Use the `DBCC PAGE` command only as a last resort. Take the utmost care when you use this command in a production environment. It's recommended to restore the production database on a test server, get all the required information using `DBCC PAGE`, and then do the updates on the production server. As always, make sure to keep a backup ready in case something goes wrong, and you need to revert to an earlier copy of the database.

## See also

- [UPDATE (Transact-SQL)](../../t-sql/queries/update-transact-sql.md)
- [Data types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md)
- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
- [Calling a Stored Procedure](../native-client-odbc-stored-procedures/calling-a-stored-procedure.md)
