---
title: "Create Instances of XML Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "type casting string instances [XML in SQL Server]"
  - "XML [SQL Server], typed"
  - "xml data type [SQL Server], generating instances"
  - "casting string instances [XML in SQL Server]"
  - "typed XML"
  - "generating XML instances [SQL Server]"
  - "XML [SQL Server], generating instances"
  - "white space [XML in SQL Server]"
ms.assetid: dbd6c06f-db6e-44a7-855a-6a55bf374907
author: MightyPen
ms.author: genemi
manager: craigg
---
# Create Instances of XML Data
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  This topic describes how to generate XML instances.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can generate XML instances in the following ways:  
  
-   Type casting string instances.  
  
-   Using the SELECT statement with the FOR XML clause.  
  
-   Using constant assignments.  
  
-   Using bulk load.  
  
## Type Casting String and Binary Instances  
 You can parse any of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] string data types, such as [**n**][**var**]**char**, **[n]text**, **varbinary**,and **image**, into the **xml** data type by casting (CAST) or converting (CONVERT) the string to the **xml** data type. Untyped XML is checked to confirm that it is well formed. If there is a schema associated with the **xml** type, validation is also performed. For more information, see [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md).  
  
 XML documents can be encoded with different encodings (for example, UTF-8, UTF-16, windows-1252). The following outlines the rules on how the string and binary source types interact with the XML document encoding and how the parser behaves.  
  
 Since **nvarchar** assumes a two-byte unicode encoding such as UTF-16 or UCS-2, the XML parser will treat the string value as a two-byte Unicode encoded XML document or fragment. This means that the XML document needs to be encoded in a two-byte Unicode encoding as well to be compatible with the source data type. A UTF-16 encoded XML document can have a UTF-16 byte order mark (BOM), but it does not need to, since the context of the source type makes it clear that it can only be a two-byte Unicode encoded document.  
  
 The content of a **varchar** string is treated as a one-byte encoded XML document/fragment by the XML parser. Since the **varchar** source string has a code page associated, the parser will use that code page for the encoding if no explicit encoding is specified in the XML itself If an XML instance has a BOM or an encoding declaration, the BOM or declaration needs to be consistent with the code page, otherwise the parser will report an error.  
  
 The content of **varbinary** is treated as a codepoint stream that is passed directly to the XML parser. Thus, the XML document or fragment needs to provide the BOM or other encoding information inline. The parser will only look at the stream to determine the encoding. This means that UTF-16 encoded XML needs to provide the UTF-16 BOM and an instance without BOM and without a declaration encoding will be interpreted as UTF-8.  
  
 If the encoding of the XML document is not known in advance and the data is passed as string or binary data instead of XML data before casting to XML, it is recommended to treat the data as **varbinary**. For example, when reading data from an XML file using OpenRowset(), one should specify the data to be read as a **varbinary(max)** value:  
  
```  
select CAST(x as XML)   
from OpenRowset(BULK 'filename.xml', SINGLE_BLOB) R(x)  
```  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internally represents XML in an efficient binary representation that uses UTF-16 encoding. User-provided encoding is not preserved, but is considered during the parse process.  
  
### Type Casting CLR user-defined types  
 If a CLR user-defined type has an XML Serialization, instances of that type can be explicitly cast to an XML datatype. For more details about the XML serialization of a CLR user-defined typed, see [XML Serialization from CLR Database Objects](https://msdn.microsoft.com/library/ac84339b-9384-4710-bebc-01607864a344).  
  
### White Space Handling in Typed XML  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], white space inside element content is considered insignificant if it occurs inside a sequence of white-space-only character data delimited by markup, such as begin or end tags, and is not entitized. (CDATA sections are ignored.) This handling of white space handling is different from how white space is described in the XML 1.0 specification published by the World Wide Web Consortium (W3C). This is because the XML parser in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] recognizes only a limited number of DTD subsets, as defined in XML 1.0. For more information about the limited DTD subsets supported in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md).  
  
 By default, the XML parser discards insignificant white space when it converts string data to XML if either of the following is true:  
  
-   The `xml:space` attribute is not defined on an element or its ancestor elements.  
  
-   The `xml:space` attribute in effect on an element, or one of its ancestor elements, has the value of default.  
  
 For example:  
  
```  
declare @x xml  
set @x = '<root>      <child/>     </root>'  
select @x   
```  
  
 This is the result:  
  
```  
<root><child/></root>  
```  
  
 However, you can change this behavior. To preserve white space for an xml DT instance, use the CONVERT operator and its optional *style* parameter set to a value of 1. For example:  
  
```  
SELECT CONVERT(xml, N'<root>      <child/>     </root>', 1)  
```  
  
 If the *style* parameter is either not used or its value is set to 0, insignificant white space is not preserved for the conversion of the xml DT instance. For more information about how to use the CONVERT operator and its *style* parameter when converting string data to xml DT instances, see [CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md).  
  
### Example: Cast a string value to typed xml and assign it to a column  
 The following example casts a string variable that contains an XML fragment to the **xml** data type and then stores it in the **xml** type column:  
  
```  
CREATE TABLE T(c1 int primary key, c2 xml)  
go  
DECLARE  @s varchar(100)  
SET @s = '<Cust><Fname>Andrew</Fname><Lname>Fuller</Lname></Cust>'   
```  
  
 The following insert operation implicitly converts from a string to the **xml** type:  
  
```  
INSERT INTO T VALUES (3, @s)   
```  
  
 You can explicitly cast() the string to the **xml** type:  
  
```  
INSERT INTO T VALUES (3, cast (@s as xml))  
```  
  
 Or you can use convert(), as shown in the following:  
  
```  
INSERT INTO T VALUES (3, convert (xml, @s))   
```  
  
### Example: Convert a string to typed xml and assign it to a variable  
 In the following example, a string is converted to **xml** type and assigned to a variable of the **xml** data type:  
  
```  
declare @x xml  
declare  @s varchar(100)  
SET @s = '<Cust><Fname>Andrew</Fname><Lname>Fuller</Lname></Cust>'   
set @x =convert (xml, @s)  
select @x  
```  
  
## Using the SELECT Statement with a FOR XML Clause  
 You can use the FOR XML clause in a SELECT statement to return results as XML. For example:  
  
```  
DECLARE @xmlDoc xml  
SET @xmlDoc = (SELECT Column1, Column2  
               FROM   Table1, Table2  
               WHERE   Some condition  
               FOR XML AUTO)  
 ...  
```  
  
 The SELECT statement returns a textual XML fragment that is then parsed during the assignment to the **xml** data type variable.  
  
 You can also use the [TYPE directive](../../relational-databases/xml/type-directive-in-for-xml-queries.md) in the FOR XML clause that directly returns a FOR XML query result as **xml** type:  
  
```  
Declare @xmlDoc xml  
SET @xmlDoc = (SELECT ProductModelID, Name  
               FROM   Production.ProductModel  
               WHERE  ProductModelID=19  
               FOR XML AUTO, TYPE)  
SELECT @xmlDoc  
```  
  
 This is the result:  
  
```  
<Production.ProductModel ProductModelID="19" Name="Mountain-100" />...  
```  
  
 In the following example, the typed **xml** result of a FOR XML query is inserted into an **xml** type column:  
  
```  
CREATE TABLE T1 (c1 int, c2 xml)  
go  
INSERT T1(c1, c2)  
SELECT 1, (SELECT ProductModelID, Name  
           FROM Production.ProductModel  
           WHERE ProductModelID=19  
           FOR XML AUTO, TYPE)  
SELECT * FROM T1  
go  
```  
  
 For more information about FOR XML, see [FOR XML &#40;SQL Server&#41;](../../relational-databases/xml/for-xml-sql-server.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns **xml** data type instances to the client as a result of different server constructs such as FOR XML queries that use the TYPE directive, or where the **xml** data type is used to return XML from SQL columns, variables, and output parameters. In client application code, the ADO.NET provider requests that this **xml** data type information be sent in a binary encoding from the server. However, if you are using FOR XML without the TYPE directive, the XML data returns as a string type. In any case, the client provider will always be able to handle either form of XML.  
  
## Using Constant Assignments  
 A string constant can be used where an instance of the **xml** data type is expected. This is the same as an implied CAST of string to XML. For example:  
  
```  
DECLARE @xmlDoc xml  
SET @xmlDoc = '<Cust><Fname>Andrew</Fname><Lname>Fuller</Lname></Cust>'   
-- Or  
SET @xmlDoc = N'<?xml version="1.0" encoding="ucs-2"?><doc/>'  
```  
  
 The previous example implicitly converts the string to the **xml** data type and assigns it to an **xml** type variable.  
  
 The following example inserts a constant string into an **xml** type column:  
  
```  
CREATE TABLE T(c1 int primary key, c2 xml)  
INSERT INTO T VALUES (3, '<Cust><Fname>Andrew</Fname><Lname>Fuller</Lname></Cust>')   
```  
  
> [!NOTE]  
>  For typed XML, the XML is validated against the specified schema. For more information, see [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md).  
  
## Using Bulk Load  
 The enhanced [OPENROWSET (Transact-SQL)](../../t-sql/functions/openrowset-transact-sql.md) functionality allows you to bulk load XML documents in the database. You can bulk load XML instances from files into the **xml** type columns in the database. For working samples, see [Examples of Bulk Import and Export of XML Documents &#40;SQL Server&#41;](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md). For more information about loading XML documents, see [Load XML Data](../../relational-databases/xml/load-xml-data.md).  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Retrieve and Query XML Data](../../relational-databases/xml/retrieve-and-query-xml-data.md)|Describes the parts of XML instances that are not preserved when they are stored in databases.|  
  
## See Also  
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)   
 [XML Data Modification Language &#40;XML DML&#41;](../../t-sql/xml/xml-data-modification-language-xml-dml.md)   
 [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md)  
  
  
