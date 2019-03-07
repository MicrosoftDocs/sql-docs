---
title: "How FOR JSON escapes special characters and control characters (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "FOR JSON, special characters"
ms.assetid: 4ba90025-5a09-4f0a-836a-54c886324530
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# How FOR JSON escapes special characters and control characters (SQL Server)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  This topic describes how the **FOR JSON** clause of a SQL Server **SELECT** statement escapes special characters and represents control characters in the JSON output.  

> [!IMPORTANT]
> This page describes the built-in support for JSON in Microsoft SQL Server. For general info about escaping and encoding in JSON, see Section 2.5 of the JSON RFC - [https://www.ietf.org/rfc/rfc4627.txt](https://www.ietf.org/rfc/rfc4627.txt).

## Escaping of special characters  
If the source data contains special characters, the **FOR JSON** clause escapes them in the JSON output with `\`, as shown in the following table. This escaping occurs both in the names of properties and in their values.  
  
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
If the source data contains control characters, the **FOR JSON** clause encodes them in the JSON output in `\u<code>` format, as shown in the following table.  
  
|**Control character**|**Encoded output**|  
|---------------------------|--------------------------|  
|CHAR(0)|\u0000|  
|CHAR(1)|\u0001|  
|...|...|  
|CHAR(31)|\u001f|  
  
## Example  
 Here's an example of the **FOR JSON** output for source data that includes both special characters and control characters.  
  
 Query:  
  
```sql  
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

## Learn more about JSON in SQL Server and Azure SQL Database  
  
### Microsoft videos

For a visual introduction to the built-in JSON support in SQL Server and Azure SQL Database, see the following videos:

-   [SQL Server 2016 and JSON Support](https://channel9.msdn.com/Shows/Data-Exposed/SQL-Server-2016-and-JSON-Support)

-   [Using JSON in SQL Server 2016 and Azure SQL Database](https://channel9.msdn.com/Shows/Data-Exposed/Using-JSON-in-SQL-Server-2016-and-Azure-SQL-Database)

-   [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven/SQLServer2016/JSON-as-a-bridge-betwen-NoSQL-and-relational-worlds)
  
## See Also  
 [Format Query Results as JSON with FOR JSON &#40;SQL Server&#41;](../../relational-databases/json/format-query-results-as-json-with-for-json-sql-server.md)  
[FOR Clause](../../t-sql/queries/select-for-clause-transact-sql.md)
