---
title: "Retrieve and Query XML Data | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "XML data [SQL Server], retrieving"
  - "XML instance retrieval"
ms.assetid: 24a28760-1225-42b3-9c89-c9c0332d9c51
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Retrieve and Query XML Data
  This topic describes the query options that you have to specify to query XML data. It also describes the parts of XML instances that are not preserved when they are stored in databases.  
  
##  <a name="features"></a> Features of an XML Instance That Are Not Preserved  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] preserves the content of the XML instance, but does not preserve aspects of the XML instance that are not considered to be significant in the XML data model. This means that a retrieved XML instance might not be identical to the instance that was stored in the server, but will contain the same information.  
  
### XML Declaration  
 The XML declaration in an instance is not preserved when the instance is stored in the database. For example:  
  
```  
CREATE TABLE T1 (Col1 int primary key, Col2 xml)  
GO  
INSERT INTO T1 values (1, '<?xml version="1.0" encoding="windows-1252" ?><doc></doc>')  
GO  
SELECT Col2  
FROM T1  
```  
  
 The result is `<doc/>`.  
  
 The XML declaration, such as `<?xml version='1.0'?>`, is not preserved when storing XML data in an `xml` data type instance. This is by design. The XML declaration () and its attributes (version/encoding/stand-alone) are lost after data is converted to type `xml`. The XML declaration is treated as a directive to the XML parser. The XML data is stored internally as ucs-2. All other PIs in the XML instance are preserved.  
  
  
### Order of Attributes  
 The order of attributes in an XML instance is not preserved. When you query the XML instance stored in the `xml` type column, the order of attributes in the resulting XML may be different from the original XML instance.  
  
  
### Quotation Marks Around Attribute Values  
 Single quotation marks and double quotations marks around attribute values are not preserved. The attribute values are stored in the database as a name and value pair. The quotation marks are not stored. When an XQuery is executed against an XML instance, the resulting XML is serialized with double quotation marks around the attribute values.  
  
```  
DECLARE @x xml  
-- Use double quotation marks.  
SET @x = '<root a="1" />'  
SELECT @x  
GO  
DECLARE @x xml  
-- Use single quotation marks.  
SET @x = '<root a=''1'' />'  
SELECT @x  
GO  
```  
  
 Both queries return = `<root a="1" />`.  
  
  
### Namespace Prefixes  
 Namespace prefixes are not preserved. When you specify XQuery against an `xml` type column, the serialized XML result may return different namespace prefixes.  
  
```  
DECLARE @x xml  
SET @x = '<ns1:root xmlns:ns1="abc" xmlns:ns2="abc">  
            <ns2:SomeElement/>  
          </ns1:root>'  
SELECT @x  
SELECT @x.query('/*')  
GO  
```  
  
 The namespace prefix in the result may be different. For example:  
  
```  
<p1:root xmlns:p1="abc"><p1:SomeElement/></p1:root>  
```  
  
  
##  <a name="query"></a> Setting Required Query Options  
 When querying `xml` type columns or variables using `xml` data type methods, the following options must be set as shown.  
  
|SET Options|Required Values|  
|-----------------|---------------------|  
|ANSI_NULLS|ON|  
|ANSI_PADDING|ON|  
|ANSI_WARNINGS|ON|  
|ARITHABORT|ON|  
|CONCAT_NULL_YIELDS_NULL|ON|  
|NUMERIC_ROUNDABORT|OFF|  
|QUOTED_IDENTIFIER|ON|  
  
 If the options are not set as shown, queries and modifications on `xml` data type methods will fail.  
  
  
## See Also  
 [Create Instances of XML Data](create-instances-of-xml-data.md)  
  
  
