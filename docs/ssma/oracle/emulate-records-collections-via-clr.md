---
title: "Emulating Records and Collections via CLR UDT"
description: "Covers how the SQL Server Migration Assistant (SSMA) for Oracle uses the SQL Server Common Language Runtime (CLR) User-Defined Data Types (UDT) to emulate Oracle Records and Collections."
author: cpichuka
ms.service: sql
ms.subservice: ssma
ms.devlang: "sql"
ms.topic: "article"
ms.date: "1/22/2020"
ms.author: cpichuka
---

# Emulating Records and Collections via CLR UDT

This article covers how the SQL Server Migration Assistant (SSMA) for Oracle uses the SQL Server Common Language Runtime (CLR) User-Defined Data Types (UDT) to emulate Oracle Records and Collections.

## Declaring Record or Collection types and variables

SSMA creates three CLR-based UDTs:

* `CollectionIndexInt`
* `CollectionIndexString`
* `Record`

The `CollectionIndexInt` type is intended for emulating collections indexed by integer, such as `VARRAY`s, nested tables, and integer key based associative arrays. The `CollectionIndexString` type is used for associative arrays indexed by character keys. The Oracle record functionality is emulated by the `Record` type.

All declarations of the record or collection types are converted to this Transact-SQL declaration:

```sql
declare @Collection$TYPE varchar(max) = '<type definition>'
```

Here `<type definition>` is a descriptive text uniquely identifying the source PL/SQL type.

Consider the following example:

```sql
DECLARE
    TYPE Manager IS RECORD
    (
        mgrid integer,
        mgrname varchar2(40),
        hiredate date
    );

    TYPE Manager_table is TABLE OF Manager INDEX BY PLS_INTEGER;

    Mgr_rec Manager;
    Mgr_table_rec Manager_table;
BEGIN
    mgr_rec.mgrid := 1;
    mgr_rec.mgrname := 'Mike';
    mgr_rec.hiredate := sysdate;

    select
        empno,
        ename,
        hiredate
    BULK COLLECT INTO
        mgr_table_rec
    FROM
        emp;
END;
```

When converted using SSMA it will become the following Transact-SQL code:

```sql
BEGIN
    DECLARE
        @CollectionIndexInt$TYPE varchar(max) =
            ' TABLE INDEX BY INT OF ( RECORD ( MGRID INT , MGRNAME STRING , HIREDATE DATETIME ) )'

    DECLARE
        @Mgr_rec$mgrid int,
        @Mgr_rec$mgrname varchar(40),
        @Mgr_rec$hiredate datetime2(0),
        @Mgr_table_rec dbo.CollectionIndexInt =
            dbo.CollectionIndexInt::[Null].SetType(@CollectionIndexInt$TYPE)

    SET @mgr_rec$mgrid = 1
    SET @mgr_rec$mgrname = 'Mike'
    SET @mgr_rec$hiredate = sysdatetime()

    SET @mgr_table_rec = @mgr_table_rec.RemoveAll()
    SET @mgr_table_rec =
        @mgr_table_rec.AssignData(
            ssma_oracle.fn_bulk_collect2CollectionComplex((
                SELECT
                    CAST(EMP.EMPNO AS int) AS mgrid,
                    EMP.ENAME AS mgrname,
                    EMP.HIREDATE AS hiredate
                FROM dbo.EMP
                FOR XML PATH
            ))
        )
END
```

Here, since the `Manager` table is associated with a numeric index (`INDEX BY PLS_INTEGER`), the corresponding T-SQL declaration used is of type `@CollectionIndexInt$TYPE`. If the table was associated with a character set index, like `VARCHAR2`, the corresponding T-SQL declaration would be of type `@CollectionIndexString$TYPE`:

```sql
-- Oracle
TYPE Manager_table is TABLE OF Manager INDEX BY VARCHAR2(40);

-- SQL Server
@CollectionIndexString$TYPE varchar(max) =
    ' TABLE INDEX BY STRING OF ( RECORD ( MGRID INT , MGRNAME STRING , HIREDATE DATETIME ) )'
```

The Oracle Record functionality is emulated by the `Record` type only.

Each of the types, `CollectionIndexInt`, `CollectionIndexString`, and `Record`, has a static property `[Null]` returning an empty instance. The `SetType` method is called to receive an empty object of a specific type (as seen in the above example).

## Constructor call conversions

Constructor notation can be used only for nested tables and `VARRAY`s, so all the explicit constructor calls are converted using the `CollectionIndexInt` type. Empty constructor calls are converted via `SetType` call invoked on null instance of `CollectionIndexInt`. The `[Null]` property returns the null instance. If the constructor contains a list of elements, special method calls are applied sequentially to add the value to the collection.

For example:

```sql
-- Oracle

DECLARE
    TYPE nested_type IS TABLE OF VARCHAR2(20);
    TYPE varray_type IS VARRAY(5) OF INTEGER;

    v1 nested_type;
    v2 varray_type;
BEGIN
   v1 := nested_type('Arbitrary','number','of','strings');
   v2 := varray_type(10, 20, 40, 80, 160);
END;

-- SQL Server

BEGIN
    DECLARE
        @CollectionIndexInt$TYPE varchar(max) = ' VARRAY OF INT',
        @CollectionIndexInt$TYPE$2 varchar(max) = ' TABLE OF STRING',
        @v1 dbo.CollectionIndexInt,
        @v2 dbo.CollectionIndexInt

    SET @v1 =
        dbo.CollectionIndexInt::[Null]
            .SetType(@CollectionIndexInt$TYPE$2)
            .AddString('Arbitrary')
            .AddString('number')
            .AddString('of')
            .AddString('strings')

   SET @v2 =
       dbo.CollectionIndexInt::[Null]
            .SetType(@CollectionIndexInt$TYPE)
            .AddInt(10)
            .AddInt(20)
            .AddInt(40)
            .AddInt(80)
            .AddInt(160)
END
```

## Referencing and assigning Record and Collection elements

Each of the UDTs has a set of methods working with elements of the various data types. For example, the `SetDouble` method assigns a `float(53)` value to record or collection, and `GetDouble` can read this value. Here is the complete list of methods:

```sql
GetCollectionIndexInt(@key <KeyType>) returns CollectionIndexInt;
SetCollectionIndexInt(@key <KeyType>, @value CollectionIndexInt) returns <UDT_type>;
GetCollectionIndexString(@key <KeyType>) returns CollectionIndexString;
SetCollectionIndexString(@key <KeyType>, @value CollectionIndexString) returns <UDT_type>;
Record GetRecord(@key <KeyType>) returns Record;
SetRecord(@key <KeyType>, @value Record) returns <UDT_type>;
GetString(@key <KeyType>) returns nvarchar(max);
SetString(@key <KeyType>, @value nvarchar(max)) returns nvarchar(max);
GetDouble(@key <KeyType>) returns float(53);
SetDouble(@key <KeyType>, @value float(53)) returns <UDT_type>;
GetDatetime(@key <KeyType>) returns datetime;
SetDatetime(@key <KeyType>, @value datetime) returns <UDT_type>;
GetVarbinary(@key <KeyType>) returns varbinary(max);
SetVarbinary(@key <KeyType>, @value varbinary(max)) returns <UDT_type>;
SqlDecimal GetDecimal(@key <KeyType>);
SetDecimal(@key <KeyType>, @value numeric) returns <UDT_type>;
GetXml(@key <KeyType>) returns xml;
SetXml(@key <KeyType>, @value xml) returns <UDT_type>;
GetInt(@key <KeyType>) returns bigint;
SetInt(@key <KeyType>, @value bigint) returns <UDT_type>;
```

These methods are used when referencing or assigning a value to an element of a collection/record.

```sql
-- Oracle
a_collection(i) := 'VALUE';

-- SQL Server
SET @a_collection = @a_collection.SetString(@i, 'VALUE');
```

When converting assignment statements for multidimensional collections or collections with record elements, SSMA adds the following methods to refer to a parent element inside the set method:

```sql
GetOrCreateCollectionIndexInt(@key <KeyType>) returns CollectionIndexInt;
GetOrCreateCollectionIndexString(@key <KeyType>) returns CollectionIndexString;
GetOrCreateRecord(@key <KeyType>) returns Record;
```

For example, a collection of record elements is created this way:

```sql
-- Oracle
DECLARE
    TYPE rec_details IS RECORD (id int, name varchar2(20));
    TYPE ntb1 IS TABLE of rec_details index BY binary_integer;
    c ntb1;
BEGIN
    c(1).id := 1;
END;

-- SQL Server
DECLARE
   @CollectionIndexInt$TYPE varchar(max) =
       ' TABLE INDEX BY INT OF ( RECORD ( ID INT , NAME STRING ) )',
   @c dbo.CollectionIndexInt =
       dbo.CollectionIndexInt::[Null].SetType(@CollectionIndexInt$TYPE)

SET @c = @c.SetRecord(1, @c.GetOrCreateRecord(1).SetInt(N'ID', 1))
```

## Collection built-in methods

SSMA uses the following UDT methods to emulate built-in methods of PL/SQL collections.

Oracle collection methods | `CollectionIndexInt` and `CollectionIndexString` equivalent
--- | ---
COUNT | `Count returns int`
DELETE | `RemoveAll() returns <UDT_type>`
DELETE(n) | `Remove(@index int) returns <UDT_type>`
DELETE(m,n) | `RemoveRange(@indexFrom int, @indexTo int) returns <UDT_type>`
EXISTS | `ContainsElement(@index int) returns bit`
EXTEND | `Extend() returns <UDT_type>`
EXTEND(n) | `Extend() returns <UDT_type>`
EXTEND(n,i) | `ExtendDefault(@count int, @def int) returns <UDT_type>`
FIRST | `First() returns int`
LAST | `Last() returns int`
LIMIT | N/A
PRIOR | `Prior(@current int) returns int`
NEXT | `Next(@current int) returns int`
TRIM | `Trim() returns <UDT_type>`
TRIM(n) | `TrimN(@count int) returns <UDT_type>`

## BULK COLLECT operation

SSMA converts `BULK COLLECT INTO` statements into SQL Server `SELECT ... FOR XML PATH` statement, whose result is wrapped into one of the following functions:

* `ssma_oracle.fn_bulk_collect2CollectionSimple`
* `ssma_oracle.fn_bulk_collect2CollectionComplex`

The choice depends on the type of the target object. These functions return XML values that can be parsed by `CollectionIndexInt`, `CollectionIndexString` and `Record` types. A special `AssignData` function assigns XML-based collection to the UDT.

SSMA recognizes three kinds of `BULK COLLECT INTO` statements.

### The collection contains elements with scalar types, and the `SELECT` list contains one column

```sql
-- Oracle
SELECT column_name_1
BULK COLLECT INTO <collection_name_1>
FROM <data_source>

-- SQL Server
SET @<collection_name_1> =
    @<collection_name_1>.AssignData(
        ssma_oracle.fn_bulk_collect2CollectionSimple(
            (SELECT column_name_1 FROM <data_source> FOR XML PATH)))
```

### The collection contains elements with record types, and the `SELECT` list contains one column

```sql
-- Oracle
SELECT
    column_name_1[, column_name_2...]
BULK COLLECT INTO
    <collection_name_1>
FROM
    <data_source>

-- SQL Server
SET @<collection_name_1> =
    @<collection_name_1>.AssignData(
        ssma_oracle.fn_bulk_collect2CollectionComplex(
            (SELECT
                column_name_1 as [collection_name_1_element_field_name_1],
                column_name_2 as [collection_name_1_element_field_name_2]
            FROM <data_source>
            FOR XML PATH)))
```

### The collection contains elements with scalar type, and the `SELECT` list contains multiple columns

```sql
-- Oracle
SELECT
    column_name_1[, column_name_2 ...]
BULK COLLECT INTO
    <collection_name_1>[, <collection_name_2> ...]
FROM
    <data_source>

-- SQL Server
;WITH bulkC AS (
    SELECT
        column_name_1 [collection_name_1_element_field_name_1],
        column_name_2 [collection_name_1_element_field_name_2]
    FROM
        <data_source>
)
SELECT
    @<collection_name_1> =
        @<collection_name_1>.AssignData(
            ssma_oracle.fn_bulk_collect2CollectionSimple(
                (SELECT
                    [collection_name_1_element_field_name_1]
                FROM
                    bulkC
                FOR XML PATH))),
    @<collection_name_2> =
        @<collection_name_2>.AssignData(
            ssma_oracle.fn_bulk_collect2CollectionSimple(
                (SELECT
                    [collection_name_1_element_field_name_2]
                FROM bulkC
                FOR XML PATH)))
```

## SELECT INTO Record

When the result of the Oracle query is saved in a PL/SQL record variable, you have two options depending on the SSMA setting for **Convert record as a list of separated variables** (available under **Tools** menu, **Project Settings**, then **General** -> **Conversion**). If the value of this setting is **Yes** (the default), then SSMA does not create an instance of the Record type. Instead, it splits the record into the constituting fields by creating a separate Transact-SQL variable per each record field. If the setting is **No**, the record is instantiated and each field is assigned a value using `Set` methods.
