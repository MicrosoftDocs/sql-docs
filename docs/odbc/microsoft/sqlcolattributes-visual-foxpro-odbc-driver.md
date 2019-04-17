---
title: "SQLColAttributes (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLColAttribute function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: d403dfa0-c26d-47d4-91d9-2f29aa387399
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLColAttributes (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Core Level  
  
 Returns descriptor information for a column in a result set. Descriptor information is returned as a character string, a 32-bit descriptor-dependent value, or an integer value.  
  
> [!NOTE]  
>  **SQLColAttributes** cannot be used to return information about the bookmark column (column 0).  
  
 The Visual FoxPro ODBC Driver supports all *fDescType* values. The following table includes comments on the driver's implementation of selected values.  
  
|*fDescType*|Comment|  
|-----------------|-------------|  
|SQL_COLUMN_AUTO_INCREMENT|Returns FALSE: Visual FoxPro has no counter fields.|  
|SQL_COLUMN_CASE_SENSITIVE|Always returns TRUE if the column type is Character.|  
|SQL_COLUMN_LABEL|Returns the column name, which is also returned by SQL_COLUMN_NAME.|  
|SQL_COLUMN_MONEY|Returns TRUE if the column type is Currency (represented by a "Y" in the Visual FoxPro language).|  
|SQL_COLUMN_OWNER_NAME|Always returns an empty string.|  
|SQL_COLUMN_QUALIFIER_NAME|Always returns an empty string.|  
|SQL_COLUMN_SEARCHABLE|Returns SQL_UNSEARCHABLE for columns of type General; these columns cannot be used in a WHERE clause.<br /><br /> Returns SQL_SEARCHABLE for columns of type Character or Memo with NOCPTRANS not set; these columns can be used in a WHERE clause with any comparison operator.<br /><br /> Returns SQL_ALL_EXCEPT_LIKE for all other column types; these columns can be used in a WHERE clause with all comparison operators except LIKE.|  
|SQL_COLUMN_TABLE_NAME|Always returns an empty string.|  
  
 For more information, see [SQLColAttributes](../../odbc/reference/syntax/sqlcolattributes-function.md) in the *ODBC Programmer's Reference*.
