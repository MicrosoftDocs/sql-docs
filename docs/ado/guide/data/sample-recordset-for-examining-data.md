---
title: "Sample Recordset for Examining Data | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "record location [ADO]"
  - "current record [ADO]"
ms.assetid: e770e626-68b1-4ddf-a217-d7b30311e2ee
author: MightyPen
ms.author: genemi
manager: craigg
---
# Sample Recordset for Examining Data
First, let's look at the **Recordset** object as returned using the following SQL query, executed against the Northwind sample data base in Microsoft SQL Server.  
  
```  
SELECT ProductID,ProductName,UnitPrice   
FROM Products   
WHERE CategoryID = 7    
```  
  
 The resultant **Recordset** object contains all the produces in the database shown in the following table.  
  
|ProductID|ProductName|UnitPrice|  
|---------------|-----------------|---------------|  
|7|Uncle Bob's Organic Dried Pears|30.0000|  
|14|Tofu|23.2500|  
|28|Rssle Sauerkraut|45.6000|  
|51|Manjimup Dried Apples|53.0000|  
|74|Longlife Tofu|10.0000|  
  
 If you are interested in getting these results yourself, try the following JScript example:  
  
-   [JScript Example to Return a Recordset](../../../ado/guide/data/jscript-code-example-to-return-a-recordset.md)
