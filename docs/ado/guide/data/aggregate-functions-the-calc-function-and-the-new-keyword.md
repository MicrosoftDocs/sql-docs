---
title: "Aggregate Functions, the CALC Function, and the NEW Keyword | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "data shaping [ADO], functions"
  - "CALC function [ADO]"
  - "NEW keyword [ADO]"
  - "aggregate functions [ADO]"
ms.assetid: 0590b466-2a36-49a2-868e-028ef5e49394
author: MightyPen
ms.author: genemi
manager: craigg
---
# Aggregate Functions, the CALC Function, and the NEW Keyword
Data shaping supports the following functions. The name assigned to the chapter containing the column to be operated on is the *chapter-alias*.  
  
 A chapter-alias can be fully qualified, consisting of each chapter column name leading to the chapter containing the *column-name,* all separated by periods. For example, if the parent chapter, chap1, contains a child chapter, chap2, that has an amount column, amt, then the qualified name would be chap1.chap2.amt.  
  
|Aggregate Functions|Description|  
|-------------------------|-----------------|  
|SUM(*chapter-alias*.*column-name*)|Calculates the sum of all values in the specified column.|  
|AVG(*chapter-alias*.*column-name*)|Calculates the average of all values in the specified column.|  
|MAX(*chapter-alias*.*column-name*)|Calculates the maximum value in the specified column.|  
|MIN(*chapter-alias*.*column-name*)|Calculates the minimum value in the specified column.|  
|COUNT(*chapter-alias*[.*column-name*])|Counts the number of rows in the specified alias. If a column is specified, only rows for which that column is non-Null are included in the count.|  
|STDEV(*chapter-alias*.*column-name*)|Calculates the standard deviation in the specified column.|  
|ANY(*chapter-alias*.*column-name*)|A value of the specified column. ANY has a predictable value only when the value of the column is the same for all rows in the chapter.<br /><br /> **Note** If the column does not contain the same value for all of the rows in the chapter, the SHAPE command arbitrarily returns one of the values to be the value of the ANY function.|  
  
|Calculated expression|Description|  
|---------------------------|-----------------|  
|CALC(*expression*)|Calculates an arbitrary expression, but only on the row of the **Recordset** containing the CALC function. Any expression using these [Visual Basic for Applications (VBA) Functions](../../../ado/guide/data/visual-basic-for-applications-functions.md) is allowed.|  
  
|NEW keyword|Description|  
|-----------------|-----------------|  
|NEW *field-type* [(*width* &#124; *scale* &#124; *precision* &#124; *error* [, *scale* &#124; *error*])]|Adds an empty column of the specified type to the **Recordset**.|  
  
 The *field-type* passed with the NEW keyword can be any of the following data types.  
  
|OLE DB data types|ADO data type equivalent(s)|  
|-----------------------|-----------------------------------|  
|DBTYPE_BSTR|adBSTR|  
|DBTYPE_BOOL|adBoolean|  
|DBTYPE_DECIMAL|adDecimal|  
|DBTYPE_UI1|adUnsignedTinyInt|  
|DBTYPE_I1|adTinyInt|  
|DBTYPE_UI2|adUnsignedSmallInt|  
|DBTYPE_UI4|adUnsignedInt|  
|DBTYPE_I8|adBigInt|  
|DBTYPE_UI8|adUnsignedBigInt|  
|DBTYPE_GUID|adGuid|  
|DBTYPE_BYTES|adBinary, AdVarBinary, adLongVarBinary|  
|DBTYPE_STR|adChar, adVarChar, adLongVarChar|  
|DBTYPE_WSTR|adWChar, adVarWChar, adLongVarWChar|  
|DBTYPE_NUMERIC|adNumeric|  
|DBTYPE_DBDATE|adDBDate|  
|DBTYPE_DBTIME|adDBTime|  
|DBTYPE_DBTIMESTAMP|adDBTimeStamp|  
|DBTYPE_VARNUMERIC|adVarNumeric|  
|DBTYPE_FILETIME|adFileTime|  
|DBTYPE_ERROR|adError|  
  
 When the new field is of type decimal (in OLE DB, DBTYPE_DECIMAL, or in ADO, adDecimal), you must specify the precision and scale values.  
  
## See Also  
 [Data Shaping Example](../../../ado/guide/data/data-shaping-example.md)   
 [Formal Shape Grammar](../../../ado/guide/data/formal-shape-grammar.md)   
 [Shape Commands in General](../../../ado/guide/data/shape-commands-in-general.md)
