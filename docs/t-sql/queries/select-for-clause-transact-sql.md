---
title: "FOR Clause (Transact-SQL)"
description: "Use the FOR clause to specify options for query results."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 02/02/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "FOR"
  - "FOR CLAUSE"
  - "FOR_TSQL"
  - "FOR_CLAUSE_TSQL"
helpviewer_keywords:
  - "XML option [SQL Server]"
  - "BROWSE option"
  - "FOR clause [Transact-SQL]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SELECT - FOR clause (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabric Lakehouse SQL analytics endpoint Fabric Synapse Data Warehouse Fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]

Use the `FOR` clause to specify one of the following options for query results.

- Specify `FOR BROWSE` to allow updates while viewing query results in a browse mode cursor.
- Specify `FOR XML` to format query results as XML.
- Specify `FOR JSON` to format query results as JSON.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[ FOR { BROWSE | <XML> | <JSON> } ]

<XML> ::=
XML
{
    { RAW [ ( 'ElementName' ) ] | AUTO }
    [
        <CommonDirectivesForXML>
        [ , { XMLDATA | XMLSCHEMA [ ( 'TargetNameSpaceURI' ) ] } ]
        [ , ELEMENTS [ XSINIL | ABSENT ]
    ]
  | EXPLICIT
    [
        <CommonDirectivesForXML>
        [ , XMLDATA ]
    ]
  | PATH [ ( 'ElementName' ) ]
    [
        <CommonDirectivesForXML>
        [ , ELEMENTS [ XSINIL | ABSENT ] ]
    ]
}

<CommonDirectivesForXML> ::=
[ , BINARY BASE64 ]
[ , TYPE ]
[ , ROOT [ ( 'RootName' ) ] ]

<JSON> ::=
JSON
{
    { AUTO | PATH }
    [
        [ , ROOT [ ( 'RootName' ) ] ]
        [ , INCLUDE_NULL_VALUES ]
        [ , WITHOUT_ARRAY_WRAPPER ]
    ]

}
```

## FOR BROWSE

#### BROWSE

Specifies that updates are allowed while viewing the data in a DB-Library browse mode cursor. You can browse a table in an application if the table includes a **timestamp** column, the table has a unique index, and the `FOR BROWSE` option is at the end of the `SELECT` statements sent to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> You can't use the `<lock_hint> HOLDLOCK` in a `SELECT` statement that includes the `FOR BROWSE` option.

`FOR BROWSE` can't appear in `SELECT` statements that the `UNION` operator joins.

> [!NOTE]  
> When the unique index key columns of a table are nullable, and the table is on the inner side of an outer join, browse mode doesn't support the index.

The browse mode lets you scan the rows in your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table and update the data in your table one row at a time. To access a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table in your application in the browse mode, you must use one of the following two options:

- The `SELECT` statement that you use to access the data from your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table must end with the keywords `FOR BROWSE`. When you turn on the `FOR BROWSE` option to use browse mode, temporary tables are created.

- You must run the following [!INCLUDE [tsql](../../includes/tsql-md.md)] statement to turn on the browse mode by using the `NO_BROWSETABLE` option:

  ```sql
  SET NO_BROWSETABLE ON;
  ```

  When you turn on the `NO_BROWSETABLE` option, all the `SELECT` statements behave as if the `FOR BROWSE` option is appended to the statements. However, the `NO_BROWSETABLE` option doesn't create the temporary tables that the `FOR BROWSE` option generally uses to send the results to your application.

When you try to access the data from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tables in browse mode by using a `SELECT` query that involves an `OUTER JOIN` statement, and when a unique index is defined on the table that's present on the inner side of an `OUTER JOIN` statement, the browse mode doesn't support the unique index. The browse mode supports the unique index only when all unique index key columns can accept `NULL` values. The browse mode doesn't support the unique index if the following conditions are true:

- You try to access the data from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tables in browse mode by using a `SELECT` query that involves an `OUTER JOIN` statement.

- A unique index is defined on the table that's present on the inner side of an `OUTER JOIN` statement.

To reproduce this behavior in the browse mode, follow these steps:

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], create a database named `SampleDB`.

1. In the `SampleDB` database, create a `tleft` table and a `tright` table that both contain a single column named `c1`. Define a unique index on the `c1` column in the `tleft` table, and set the column to accept `NULL` values. To do this, run the following [!INCLUDE [tsql](../../includes/tsql-md.md)] statements in an appropriate query window:

   ```sql
   CREATE TABLE tleft (c1 INT NULL UNIQUE);
   GO

   CREATE TABLE tright (c1 INT NULL);
   GO
   ```

1. Insert several values in the `tleft` table and the `tright` table. Make sure that you insert a `NULL` value in the `tleft` table. To do this, run the following [!INCLUDE [tsql](../../includes/tsql-md.md)] statements in the query window:

   ```sql
   INSERT INTO tleft VALUES (2);
   INSERT INTO tleft VALUES (NULL);
   INSERT INTO tright VALUES (1);
   INSERT INTO tright VALUES (3);
   INSERT INTO tright VALUES (NULL);
   ```

1. Turn on the `NO_BROWSETABLE` option. To do this, run the following [!INCLUDE [tsql](../../includes/tsql-md.md)] statements in the query window:

   ```sql
   SET NO_BROWSETABLE ON;
   ```

1. Access the data in the `tleft` table and the `tright` table by using an outer join statement in the `SELECT` query. Make sure that the `tleft` table is on the inner side of the outer join statement. To do this, run the following [!INCLUDE [tsql](../../includes/tsql-md.md)] statements in the query window:

   ```sql
   SELECT tleft.c1
   FROM tleft
        RIGHT OUTER JOIN tright
            ON tleft.c1 = tright.c1
   WHERE tright.c1 <> 2;
   ```

   Notice the following output in the **Results** pane:

   ```output
   c1
   ---
   NULL
   NULL
   ```

After you run the `SELECT` query to access the tables in the browse mode, the result set of the `SELECT` query contains two `NULL` values for the `c1` column in the `tleft` table because of the definition of the `RIGHT OUTER JOIN` statement. Therefore, in the result set, you can't distinguish between the `NULL` values that came from the table and the `NULL` values that the `RIGHT OUTER JOIN` statement introduced. You might receive incorrect results if the query must ignore the `NULL` values from the result set.

> [!NOTE]  
> If the columns that are included in the unique index don't accept `NULL` values, all the `NULL` values in the result set were introduced by the `RIGHT OUTER JOIN` statement.

## FOR XML

#### XML

Specifies that the results of a query are to be returned as an XML document. One of the following XML modes must be specified: `RAW`, `AUTO`, `EXPLICIT`. For more information about XML data and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [FOR XML (SQL Server)](../../relational-databases/xml/for-xml-sql-server.md).

#### RAW [ ('*ElementName*') ]

Takes the query result and transforms each row in the result set into an XML element with a generic identifier `<row />` as the element tag. You can optionally specify a name for the row element. The resulting XML output uses the specified `ElementName` as the row element generated for each row. For more information, see [Use RAW mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md).

#### AUTO

Returns query results in a simple, nested XML tree. Each table in the `FROM` clause, for which at least one column is listed in the `SELECT` clause, is represented as an XML element. The columns listed in the `SELECT` clause are mapped to the appropriate element attributes. For more information, see [Use AUTO mode with FOR XML](../../relational-databases/xml/use-auto-mode-with-for-xml.md).

#### EXPLICIT

Specifies that the shape of the resulting XML tree is defined explicitly. Using this mode, you must write queries in a particular way so that they specify additional information about the desired nesting explicitly. For more information, see [Use EXPLICIT mode with FOR XML](../../relational-databases/xml/use-explicit-mode-with-for-xml.md).

#### XMLDATA

Returns inline XDR schema, but doesn't add the root element to the result. If you specify `XMLDATA`, the XDR schema is appended to the document.

> [!IMPORTANT]  
> **The `XMLDATA` directive is deprecated.** Use XSD generation for `RAW` and `AUTO` modes. There's no replacement for the `XMLDATA` directive in `EXPLICIT` mode. [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

**Suppress unwanted line breaks**: You might use SQL Server Management Studio (SSMS) to run a query that uses the `FOR XML` clause. Sometimes a large amount of XML is returned and displayed in one grid cell. The XML string could be longer than one SSMS grid cell can hold on a single line. In these cases, SSMS might insert line break characters between long segments of the whole XML string. Such line breaks might occur in the middle of a substring that shouldn't be split across lines. You can prevent the line breaks by using a cast `AS XMLDATA`. This solution can also apply when you use `FOR JSON PATH`, as in the following Transact-SQL sample `SELECT` statement:

```sql
SELECT CAST (
    (SELECT column1,
            column2
    FROM my_table
    FOR XML PATH ('')) AS VARCHAR (MAX)
) AS XMLDATA;
```

#### XMLSCHEMA [ ('*TargetNameSpaceURI*') ]

Returns inline XSD schema. You can optionally specify a target namespace URI when you specify this directive, which returns the specified namespace in the schema. For more information, see [Generate an inline XSD schema](../../relational-databases/xml/generate-an-inline-xsd-schema.md).

#### ELEMENTS

Specifies that the columns are returned as subelements. Otherwise, the query maps them to XML attributes. This option is supported in `RAW`, `AUTO`, and `PATH` modes only. For more information, see [Use RAW mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md).

#### XSINIL

Specifies that an element with `xsi:nil` attribute set to **True** is created for `NULL` column values. You can only specify this option with the `ELEMENTS` directive. For more information, see:

- [Generate elements for NULL values with the XSINIL parameter](../../relational-databases/xml/generate-elements-for-null-values-with-the-xsinil-parameter.md).
- [FOR XML (SQL Server)](../../relational-databases/xml/for-xml-sql-server.md)

#### ABSENT

Indicates that for `NULL` column values, corresponding XML elements aren't added in the XML result. Specify this option only with `ELEMENTS`.

#### PATH [ ('*ElementName*') ]

Generates a `<row>` element wrapper for each row in the result set. You can optionally specify an element name for the `<row>` element wrapper. If you provide an empty string, such as `FOR XML PATH (''))`, a wrapper element isn't generated. Using `PATH` might provide a simpler alternative to queries written using the `EXPLICIT` directive. For more information, see [Use PATH mode with FOR XML](../../relational-databases/xml/use-path-mode-with-for-xml.md).

#### BINARY BASE64

Specifies that the query returns the binary data in binary base64-encoded format. When you retrieve binary data by using `RAW` and `EXPLICIT` mode, you must specify this option. This option is the default in `AUTO` mode.

#### TYPE

Specifies that the query returns results as **xml** type. For more information, see [TYPE directive in FOR XML queries](../../relational-databases/xml/type-directive-in-for-xml-queries.md).

#### ROOT [ ('*RootName*') ]

Specifies that a single top-level element is added to the resulting XML. You can optionally specify the name of the root element to generate. If you don't specify the root name, the default `<root>` element is added.

For more information, see [FOR XML (SQL Server)](../../relational-databases/xml/for-xml-sql-server.md).

### Example

The following example specifies `FOR XML AUTO` with the `TYPE` and `XMLSCHEMA` options. Because of the `TYPE` option, the query returns the result set to the client as an **xml** type. The `XMLSCHEMA` option specifies that the inline XSD schema is included in the XML data returned, and the `ELEMENTS` option specifies that the XML result is element-centric.

```sql
USE AdventureWorks2025;
SELECT p.BusinessEntityID,
       FirstName,
       LastName,
       PhoneNumber AS Phone
FROM Person.Person AS p
     INNER JOIN Person.PersonPhone AS pph
         ON p.BusinessEntityID = pph.BusinessEntityID
WHERE LastName LIKE 'G%'
ORDER BY LastName, FirstName
FOR XML AUTO, TYPE, XMLSCHEMA, ELEMENTS XSINIL;
```

## FOR JSON

### Remarks

In Fabric Data Warehouse, the query must end with `FOR JSON`, so you can't use it inside subqueries.

#### JSON

Specify `FOR JSON` to return the results of a query formatted as JSON text. You also need to specify one of the following JSON modes: `AUTO` or `PATH`. For more information about the `FOR JSON` clause, see [Format query results as JSON with FOR JSON](../../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md).

#### AUTO

Format the JSON output automatically based on the structure of the `SELECT` statement by specifying `FOR JSON AUTO`. For more info and examples, see [Format JSON output automatically with AUTO mode](../../relational-databases/json/format-json-output-automatically-with-auto-mode-sql-server.md).

#### PATH

Get full control over the format of the JSON output by specifying `FOR JSON PATH`. `PATH` mode lets you create wrapper objects and nest complex properties. For more info and examples, see [Format nested JSON output with PATH mode](../../relational-databases/json/format-nested-json-output-with-path-mode-sql-server.md).

#### INCLUDE_NULL_VALUES

Include `NULL` values in the JSON output by specifying the `INCLUDE_NULL_VALUES` option with the `FOR JSON` clause. If you don't specify this option, the output doesn't include JSON properties for `NULL` values in the query results. For more info and examples, see [Include Null Values in JSON - INCLUDE_NULL_VALUES Option](../../relational-databases/json/include-null-values-in-json-include-null-values-option.md).

#### ROOT [ ('*RootName*') ]

Add a single, top-level element to the JSON output by specifying the `ROOT` option with the `FOR JSON` clause. If you don't specify the `ROOT` option, the JSON output doesn't have a root element. For more info and examples, see [Add a Root Node to JSON Output with the ROOT Option](../../relational-databases/json/add-a-root-node-to-json-output-with-the-root-option-sql-server.md).

#### WITHOUT_ARRAY_WRAPPER

Remove the square brackets that surround the JSON output by default by specifying the `WITHOUT_ARRAY_WRAPPER` option with the `FOR JSON` clause. If you don't specify this option, the JSON output is enclosed within square brackets. Use the `WITHOUT_ARRAY_WRAPPER` option to generate a single JSON object as output. For more info, see [Remove Square Brackets from JSON - WITHOUT_ARRAY_WRAPPER Option](../../relational-databases/json/remove-square-brackets-from-json-without-array-wrapper-option.md).

For more info, see [Format query results as JSON with FOR JSON](../../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md).

## Related content

- [SELECT (Transact-SQL)](select-transact-sql.md)
- [Use FOR JSON output in the SQL Database Engine and in client apps](../../relational-databases/json/use-for-json-output-in-sql-server-and-in-client-apps-sql-server.md)
