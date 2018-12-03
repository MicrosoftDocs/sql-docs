---
title: "Select Rows That Do Not Match a Value (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "search conditions [SQL Server], rows not matching value"
  - "row not matching value [SQL Server]"
  - "NOT operator [Visual Database Tools]"
  - "search criteria [SQL Server], rows not matching value"
ms.assetid: 19898578-7b2f-401c-bb8f-9f2a017efdf7
author: stevestein
ms.author: sstein
manager: craigg
---
# Select Rows That Do Not Match a Value (Visual Database Tools)
  To find rows that do not match a value, use the NOT operator.  
  
### To find rows that do not match a value  
  
1.  If you have not done so already, add the columns or expressions that you want to use within your search condition to the [Criteria pane](visual-database-tools.md).  
  
2.  Locate the row containing the data column or expression to search, and then in the **Filter** grid column, enter the NOT operator followed by a search value.  
  
 For example, to find all the rows in a `products` table where the values in the product code column begin with a character other than "A," you can enter a search condition such as the following:  
  
```  
NOT LIKE 'A%'  
```  
  
## See Also  
 [Rules for Entering Search Values &#40;Visual Database Tools&#41;](rules-for-entering-search-values-visual-database-tools.md)   
 [Specify Search Criteria &#40;Visual Database Tools&#41;](specify-search-criteria-visual-database-tools.md)  
  
  
