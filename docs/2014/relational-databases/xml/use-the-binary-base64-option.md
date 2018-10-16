---
title: "Use the BINARY BASE64 Option | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "AUTO FOR XML mode, BINARY BASE64 option"
ms.assetid: 86a7bb85-7f83-412a-b775-d2c379702fe9
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Use the BINARY BASE64 Option
  If the BINARY BASE64 option is specified in the query, the binary data is returned in base64 encoding format. By default, if the BINARY BASE64 option is not specified, AUTO mode supports URL encoding of binary data. That is, instead of binary data, a reference to a relative URL to the virtual root of the database where the query was executed is returned. This reference can be used to access the actual binary data in subsequent operations by using the SQLXML ISAPI dbobject query. The query must provide enough information, such as primary key columns, to identify the image.  
  
 In specifying a query, if an alias is used for the binary column of the view, the alias is returned in the URL encoding of the binary data. In subsequent operations, the alias is meaningless, and the URL encoding cannot be used to retrieve the image. Therefore, do not use aliases when querying a view using FOR XML AUTO mode.  
  
 For example, in a SELECT query, casting any column to a binary large object (BLOB) makes it a temporary entity in that it loses its associated table name and column name. This causes AUTO mode queries to generate an error, because it does not know where to put this value in the XML hierarchy. For example:  
  
```  
CREATE TABLE MyTable (Col1 int PRIMARY KEY, Col2 binary)  
INSERT INTO MyTable VALUES (1, 0x7);  
```  
  
 This query produces an error, because of the casting to a binary large object (BLOB):  
  
```  
SELECT Col1,  
CAST(Col2 as image) as Col2  
FROM MyTable  
FOR XML AUTO;  
```  
  
 The solution is to add the BINARY BASE64 option to the FOR XML clause. If you remove the casting, the query produces the results as expected:  
  
```  
SELECT Col1,  
CAST(Col2 as image) as Col2  
FROM MyTable  
FOR XML AUTO, BINARY BASE64;  
```  
  
 This is the result:  
  
```  
<MyTable Col1="1" Col2="Bw==" />  
```  
  
## See Also  
 [Use AUTO Mode with FOR XML](use-auto-mode-with-for-xml.md)  
  
  
