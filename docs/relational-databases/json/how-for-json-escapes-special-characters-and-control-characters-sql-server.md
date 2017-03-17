---
title: "How FOR JSON escapes special characters and control characters (SQL Server) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "2017-02-03"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-json"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "FOR JSON, special characters"
ms.assetid: 4ba90025-5a09-4f0a-836a-54c886324530
caps.latest.revision: 15
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# How FOR JSON escapes special characters and control characters (SQL Server)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  This topic describes how the **FOR JSON** clause of a **SELECT** statement escapes special characters and represents control characters in the JSON output.  

> [!NOTE]
> This page describes the built-in JSON support in Microsoft SQL Server. For general info about escaping and encoding in JSON, see Section 2.5 of the RFC - [http://www.ietf.org/rfc/rfc4627.txt](http://www.ietf.org/rfc/rfc4627.txt).

## Escaping of special characters  
 The **FOR JSON** clause escapes special characters in the JSON output with `\`, as shown in the following table. This escaping occurs both in the names of properties and in their values.  
  
|**Special character**|**Escaped output**|  
|---------------------------|--------------------------|  
|Quotation mark (")|\\"|  
|Backslash (\\)|\\\|  
|Slash (/)|\\/|  
|Backspace|\b|  
|Form feed|\f|  
|New line|\n|  
|Carriage return|\r|  
|Horizontal tab|\t|  
  
## Control characters  
 The **FOR JSON** clause encodes control characters in the JSON output in `\u<code>` format, as shown in the following table.  
  
|**Control character**|**Encoded output**|  
|---------------------------|--------------------------|  
|CHAR(0)|\u0000|  
|CHAR(1)|\u0001|  
|…|…|  
|CHAR(31)|\u001f|  
  
## Example  
 Here's an example of a **FOR JSON** clause that includes both special characters and control characters.  
  
 Query:  
  
```tsql  
SELECT  
  'VALUE\    /  
  "' as [KEY\/"],  
  CHAR(0) as '0',  
  CHAR(1) as '1',  
  CHAR(31) as '31'  
FOR JSON PATH  
```  
  
 Result:  
  
```json  
{
	"KEY\\\t\/\"": "VALUE\\\t\/\r\n\"",
	"0": "\u0000",
	"1": "\u0001",
	"31": "\u001f"
}
```  
  
## See Also  
 [Format Query Results as JSON with FOR JSON &#40;SQL Server&#41;](../../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md)  
[FOR Clause](../../t-sql/queries/select-for-clause-transact-sql.md)