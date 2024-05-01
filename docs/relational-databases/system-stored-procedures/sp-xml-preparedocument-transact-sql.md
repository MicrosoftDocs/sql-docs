---
title: "sp_xml_preparedocument (Transact-SQL)"
description: "Reads the XML text provided as input, parses the text by using the MSXML parser, and provides the parsed document."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_xml_preparedocument_TSQL"
  - "sp_xml_preparedocument"
helpviewer_keywords:
  - "sp_xml_preparedocument"
dev_langs:
  - "TSQL"
---
# sp_xml_preparedocument (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Reads the XML text provided as input, parses the text by using the MSXML parser (`msxmlsql.dll`), and provides the parsed document in a state ready for consumption. This parsed document is a tree representation of the various nodes in the XML document: elements, attributes, text, comments, and so on.

`sp_xml_preparedocument` returns a handle that can be used to access the newly created internal representation of the XML document. This handle is valid during the session, or until the handle is invalidated by executing `sp_xml_removedocument`.

A parsed document is stored in the internal cache of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The MSXML parser can use one-eighth of the total memory available for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To avoid running out of memory, run `sp_xml_removedocument` to free up the memory as soon as the document is no longer required. In many cases, the nodes() method might be a better alternative, and help to avoid excessive memory use.

For backward compatibility, `sp_xml_preparedocument` collapses the CR (`char(13)`) and LF (`char(10)`) characters in attributes even if these characters are entitized.

> [!NOTE]  
> The XML parser invoked by `sp_xml_preparedocument` can parse internal DTDs and entity declarations. Because maliciously constructed DTDs and entity declarations can be used to perform a denial of service attack, we strongly recommend that users not directly pass XML documents from untrusted sources to `sp_xml_preparedocument`.  
>  
> To mitigate recursive entity expansion attacks, `sp_xml_preparedocument` limits to 10,000 the number of entities that can be expanded underneath a single entity at the top level of a document. The limit does not apply to character or numeric entities. This limit allows documents with many entity references to be stored, but prevents any one entity from being recursively expanded in a chain longer than 10,000 expansions.

`sp_xml_preparedocument` limits the number of elements that can be open at one time to 256.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_xml_preparedocument hdoc OUTPUT
    [ , xmltext ]
    [ , xpath_namespaces ]
[ ; ]
```

## Arguments

#### *hdoc*

The handle to the newly created document. *hdoc* is an integer.

#### [ *xmltext* ]

The original XML document. The MSXML parser parses this XML document. *xmltext* is a text parameter: **char**, **nchar**, **varchar**, **nvarchar**, **text**, **ntext** or **xml**. The default value is NULL, in which case an internal representation of an empty XML document is created.

> [!NOTE]  
> `sp_xml_preparedocument` can only process text or untyped XML. If an instance value to be used as input is already typed XML, first cast it to a new untyped XML instance or as a string and then pass that value as input. For more information, see [Compare Typed XML to Untyped XML](../xml/compare-typed-xml-to-untyped-xml.md).

#### [ *xpath_namespaces* ]

Specifies the namespace declarations that are used in row and column XPath expressions in OPENXML. *xpath_namespaces* is a text parameter: **char**, **nchar**, **varchar**, **nvarchar**, **text**, **ntext** or **xml**.

The default value is `<root xmlns:mp="urn:schemas-microsoft-com:xml-metaprop">`. *xpath_namespaces* provides the namespace URIs for the prefixes used in the XPath expressions in OPENXML, with a well-formed XML document. *xpath_namespaces* declares the prefix that must be used to refer to the namespace `urn:schemas-microsoft-com:xml-metaprop`; this provides metadata about the parsed XML elements. Although you can redefine the namespace prefix for the metaproperty namespace by using this technique, this namespace isn't lost. The prefix `mp` is still valid for `urn:schemas-microsoft-com:xml-metaprop` even if *xpath_namespaces* contains no such declaration.

## Return code values

`0` (success) or `> 0` (failure).

## Permissions

Requires membership in the **public** role.

## Examples

### A. Prepare an internal representation for a well-formed XML document

The following example returns a handle to the newly created internal representation of the XML document that is provided as input. In the call to `sp_xml_preparedocument`, a default namespace prefix mapping is used.

```sql
DECLARE @hdoc INT;
DECLARE @doc VARCHAR(1000);

SET @doc = '
<ROOT>
<Customer CustomerID="VINET" ContactName="Paul Henriot">
   <Order CustomerID="VINET" EmployeeID="5" OrderDate="1996-07-04T00:00:00">
      <OrderDetail OrderID="10248" ProductID="11" Quantity="12"/>
      <OrderDetail OrderID="10248" ProductID="42" Quantity="10"/>
   </Order>
</Customer>
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">
   <Order CustomerID="LILAS" EmployeeID="3" OrderDate="1996-08-16T00:00:00">
      <OrderDetail OrderID="10283" ProductID="72" Quantity="3"/>
   </Order>
</Customer>
</ROOT>';

--Create an internal representation of the XML document.
EXEC sp_xml_preparedocument @hdoc OUTPUT, @doc;

-- Remove the internal representation.
EXEC sp_xml_removedocument @hdoc;
```

### B. Prepare an internal representation for a well-formed XML document with a DTD

The following example returns a handle to the newly created internal representation of the XML document that is provided as input. The stored procedure validates the document loaded against the DTD included in the document. In the call to `sp_xml_preparedocument`, a default namespace prefix mapping is used.

```sql
DECLARE @hdoc int;
DECLARE @doc varchar(2000);
SET @doc = '
<?xml version="1.0" encoding="UTF-8" ?>
<!DOCTYPE root
[<!ELEMENT root (Customers)*>
<!ELEMENT Customers EMPTY>
<!ATTLIST Customers CustomerID CDATA #IMPLIED ContactName CDATA #IMPLIED>]>
<root>
<Customers CustomerID="ALFKI" ContactName="Maria Anders"/>
</root>';

EXEC sp_xml_preparedocument @hdoc OUTPUT, @doc;
```

### C. Specify a namespace URI

The following example returns a handle to the newly created internal representation of the XML document that is provided as input. The call to `sp_xml_preparedocument` preserves the `mp` prefix to the metaproperty namespace mapping and adds the `xyz` mapping prefix to the namespace `urn:MyNamespace`.

```sql
DECLARE @hdoc int;
DECLARE @doc varchar(1000);
SET @doc ='
<ROOT>
<Customer CustomerID="VINET" ContactName="Paul Henriot">
   <Order CustomerID="VINET" EmployeeID="5"
           OrderDate="1996-07-04T00:00:00">
      <OrderDetail OrderID="10248" ProductID="11" Quantity="12"/>
      <OrderDetail OrderID="10248" ProductID="42" Quantity="10"/>
   </Order>
</Customer>
<Customer CustomerID="LILAS" ContactName="Carlos Gonzlez">
   <Order CustomerID="LILAS" EmployeeID="3"
           OrderDate="1996-08-16T00:00:00">
      <OrderDetail OrderID="10283" ProductID="72" Quantity="3"/>
   </Order>
</Customer>
</ROOT>'
--Create an internal representation of the XML document.
EXEC sp_xml_preparedocument @hdoc OUTPUT, @doc, '<ROOT xmlns:xyz="urn:MyNamespace"/>';
```

## Related content

- [XML Stored Procedures(Transact-SQL)](xml-stored-procedures-transact-sql.md)
- [System Stored Procedures(Transact-SQL)](system-stored-procedures-transact-sql.md)
- [OPENXML(Transact-SQL)](../../t-sql/functions/openxml-transact-sql.md)
- [sys.dm_exec_xml_handles (Transact-SQL)](../system-dynamic-management-views/sys-dm-exec-xml-handles-transact-sql.md)
- [sp_xml_removedocument (Transact-SQL)](sp-xml-removedocument-transact-sql.md)
- [nodes() Method (xml Data Type)](../../t-sql/xml/nodes-method-xml-data-type.md)
