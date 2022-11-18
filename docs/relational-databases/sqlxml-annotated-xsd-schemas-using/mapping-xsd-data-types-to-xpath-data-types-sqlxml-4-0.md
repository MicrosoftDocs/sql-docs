---
title: "Mapping XSD Data Types to XPath Data Types (SQLXML)"
description: Learn how to map XSD data types to XPath data types when performing an XPath query in SQLXML 4.0.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "mapping data types [SQLXML]"
  - "data types [SQLXML], converting"
  - "annotated XSD schemas, mapping data types"
  - "XPath queries [SQLXML], mapping data types"
  - "converting data types [SQLXML]"
  - "data types [SQLXML], mapping data types"
  - "XPath data types [SQLXML]"
  - "XSD schemas [SQLXML], mapping data types"
ms.assetid: ced1a95e-18d4-4a5a-8da8-dbb6d58bbd45
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Mapping XSD Data Types to XPath Data Types (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]
  When an XPath query is executed against an XSD schema and the XSD type is specified in the **xsd:type** attribute, XPath uses the data type specified when it processes the query.  
  
 The XPath data type of a node is derived from the XSD data type in the schema, as shown in the following table. (The EmployeeID node is used for the purpose of illustration.)  
  
|XSD data type|XDR data type|Equivalent<br /><br /> XPath data type|SQL Server<br /><br /> conversion that is used|  
|-------------------|-------------------|------------------------------------|--------------------------------------------|  
|**Base64Binary**<br /><br /> **HexBinary**|**None**<br /><br /> **bin.base64bin.hex**|**Not applicable**|None<br /><br /> EmployeeID|  
|**Boolean**|**boolean**|**boolean**|CONVERT(bit, EmployeeID)|  
|**Decimal, integer, float, byte, short, int, long, float, double, unsignedByte, unsignedShort, unsignedInt, unsignedLong**|**number, int, float,i1, i2, i4, i8,r4, r8ui1, ui2, ui4, ui8**|**number**|CONVERT(float(53), EmployeeID)|  
|**id, idref, idrefsentity, entities, notation, nmtoken, nmtokens, DateTime, string, AnyURI**|**id, idref, idrefsentity, entities, enumeration, notation, nmtoken, nmtokens, char, dateTime, dateTime.tz, string, uri, uuid**|**string**|CONVERT(nvarchar(4000), EmployeeID, 126)|  
|**decimal**|**fixed14.4**|**Not applicable (There is no data type in XPath that is equivalent to the fixed14.4 XDR data type.)**|CONVERT(money, EmployeeID)|  
|**date**|**date**|**string**|LEFT(CONVERT(nvarchar(4000), EmployeeID, 126), 10)|  
|**time**|**time**<br /><br /> **time.tz**|**string**|SUBSTRING(CONVERT(nvarchar(4000), EmployeeID, 126), 1 + CHARINDEX(N'T', CONVERT(nvarchar(4000), EmployeeID, 126)), 24)|  
  
  
