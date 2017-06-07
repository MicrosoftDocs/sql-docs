---
title: "Remove Square Brackets from JSON - WITHOUT_ARRAY_WRAPPER Option | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-json"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "WITHOUT_ARRAY_WRAPPER"
ms.assetid: aa86c2d1-458e-465f-abfa-75470137d054
caps.latest.revision: 11
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Remove Square Brackets from JSON - WITHOUT_ARRAY_WRAPPER Option
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

To remove the square brackets that surround the JSON output of the **FOR JSON** clause by default, specify the **WITHOUT_ARRAY_WRAPPER** option. Use this option with a single-row result to generate a single JSON object as output instead of an array with a single element.

If you use this option with a multiple-row result, the resulting output is not valid JSON because of the multiple elements and the missing square brackets.  
  
## Example (single-row result)  
The following example shows the output of the **FOR JSON** clause with and without the **WITHOUT_ARRAY_WRAPPER** option.  
  
 **Query**  
  
```sql  
SELECT 2015 as year, 12 as month, 15 as day  
FOR JSON PATH, WITHOUT_ARRAY_WRAPPER 
```  

 **Result** with the **WITHOUT_ARRAY_WRAPPER** option  
  
```json  
{
	"year": 2015,
	"month": 12,
	"day": 15
} 
```  
  
 **Result** (default) without the **WITHOUT_ARRAY_WRAPPER** option  
  
```json  
[{
	"year": 2015,
	"month": 12,
	"day": 15
}]
```  

## Example (multiple-row result)
Here's another example of a **FOR JSON** clause with and without the **WITHOUT_ARRAY_WRAPPER** option. This example produces a multiple-row result. The output is not valid JSON because of the multiple elements and the missing square brackets.
  
 **Query**  
  
```sql  
SELECT TOP 3 SalesOrderNumber, OrderDate, Status  
FROM Sales.SalesOrderHeader  
ORDER BY ModifiedDate  
FOR JSON PATH, WITHOUT_ARRAY_WRAPPER 
```  
  
 **Result** with the **WITHOUT_ARRAY_WRAPPER** option  
  
```json  
{
	"SalesOrderNumber": "SO43662",
	"OrderDate": "2011-05-31T00:00:00",
	"Status": 5
}, {
	"SalesOrderNumber": "SO43661",
	"OrderDate": "2011-05-31T00:00:00",
	"Status": 5
}, {
	"SalesOrderNumber": "SO43660",
	"OrderDate": "2011-05-31T00:00:00",
	"Status": 5
} 
```  
  
 **Result** (default) without the **WITHOUT_ARRAY_WRAPPER** option  
  
```json  
[{
	"SalesOrderNumber": "SO43662",
	"OrderDate": "2011-05-31T00:00:00",
	"Status": 5
}, {
	"SalesOrderNumber": "SO43661",
	"OrderDate": "2011-05-31T00:00:00",
	"Status": 5
}, {
	"SalesOrderNumber": "SO43660",
	"OrderDate": "2011-05-31T00:00:00",
	"Status": 5
}]
```  
  
## See Also  
 [FOR Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-for-clause-transact-sql.md)  
  
  
