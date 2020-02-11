---
title: "REPLACENULL (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 70db7832-b5a0-4db5-a8ad-42ad8630d8e8
author: janinezhang
ms.author: janinez
manager: craigg
---
# REPLACENULL (SSIS Expression)
  Returns the value of second expression parameter if the value of first expression parameter is NULL; otherwise, returns the value of first expression.  
  
## Syntax  
  
```vb  
REPLACENULL(expression 1,expression 2)  
```  
  
## Arguments  
 *expression 1*  
 The result of this expression is checked against NULL.  
  
 *expression 2*  
 The result of this expression is returned if the first expression evaluates to NULL.  
  
## Result Types  
 DT_WSTR  
  
## Remarks  
  
-   The length of *expression 2* may be zero.  
  
-   REPLACENULL returns a null result if any argument is null.  
  
-   BLOB columns (DT_TEXT, DT_NTEXT, DT_IMAGE) are not supported for either parameter.  
  
-   The two expressions are expected to have the same return type. If they do not, the function attempts to cast the 2nd expression to the return type of the 1st expression, which may result in an error if the data types are incompatible.  
  
## Expression Examples  
 The following example replaces any NULL value in a database column with a string (1900-01-01). This function is especially used in common Derived Column patterns where you want to replace NULL values with something else.  
  
```  
REPLACENULL(MyColumn, "1900-01-01")  
```  
  
> [!NOTE]  
>  The following example shows how it was done in [!INCLUDE[ssISversion2005](../../includes/ssisversion2005-md.md)]/[!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)].  
  
```  
(DT_DBTIMESTAMP) (ISNULL(MyColumn) ? "1900-01-01" : MyColumn)   
```  
  
  
