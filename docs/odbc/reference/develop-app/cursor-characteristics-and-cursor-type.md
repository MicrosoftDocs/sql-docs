---
description: "Cursor Characteristics and Cursor Type"
title: "Cursor Characteristics and Cursor Type | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "scrollable cursors [ODBC]"
  - "cursors [ODBC], scrollable"
  - "cursors [ODBC], creating"
ms.assetid: 6f67edd2-ae71-4ca0-9b2d-abf4c20dc17b
author: David-Engel
ms.author: v-davidengel
---
# Cursor Characteristics and Cursor Type
An application can specify the characteristics of a cursor instead of specifying the cursor type (forward-only, static, keyset-driven, or dynamic). To do this, the application selects the cursor's scrollability (by setting the SQL_ATTR_CURSOR_SCROLLABLE statement attribute) and sensitivity (by setting the SQL_ATTR_CURSOR_SENSITIVITY statement attribute) before opening the cursor on the statement handle. The driver then chooses the cursor type that most efficiently provides the characteristics that the application requested.  
  
 Whenever an application sets any of the statement attributes SQL_ATTR_CONCURRENCY, SQL_ATTR_CURSOR_SCROLLABLE, SQL_ATTR_CURSOR_SENSITIVITY, or SQL_ATTR_CURSOR_TYPE, the driver makes any required change to the other statement attributes in this set of four attributes so that their values remain consistent. As a result, when the application specifies a cursor characteristic, the driver can change the attribute that indicates cursor type based on this implicit selection; when the application specifies a type, the driver can change any of the other attributes to be consistent with the characteristics of the selected type. For more information about these statement attributes, see the [SQLSetStmtAttr](../../../odbc/reference/syntax/sqlsetstmtattr-function.md) function description.  
  
 An application that sets statement attributes to specify both a cursor type and cursor characteristics runs the risk of obtaining a cursor that is not the most efficient method available on that driver of meeting the application's requirements.  
  
 The implicit setting of statement attributes is driver-defined except that it must follow these rules:  
  
-   Forward-only cursors are never scrollable; see the definition of SQL_ATTR_CURSOR_SCROLLABLE in [SQLSetStmtAttr](../../../odbc/reference/syntax/sqlsetstmtattr-function.md).  
  
-   Insensitive cursors are never updatable (and thus their concurrency is read-only); this is based on their definition of insensitive cursors in the ISO SQL standard.  
  
 Consequently, the implicit setting of statement attributes occurs in the cases described in the following table.  
  
|Application sets attribute to|Other attributes set implicitly|  
|-----------------------------------|-------------------------------------|  
|SQL_ATTR_CONCURRENCY to SQL_CONCUR_READ_ONLY|SQL_ATTR_CURSOR_SENSITIVITY to SQL_INSENSITIVE.|  
|SQL_ATTR_CONCURRENCY to SQL_CONCUR_LOCK, SQL_CONCUR_ROWVER, or SQL_CONCUR_VALUES|SQL_ATTR_CURSOR_SENSITIVITY to SQL_UNSPECIFIED or SQL_SENSITIVE, as defined by the driver. It can never be set to SQL_INSENSITIVE, because insensitive cursors are always read-only.|  
|SQL_ATTR_CURSOR_SCROLLABLE to SQL_NONSCROLLABLE|SQL_ATTR_CURSOR_TYPE to SQL_CURSOR_FORWARD_ONLY|  
|SQL_ATTR_CURSOR_SCROLLABLE to SQL_SCROLLABLE|SQL_ATTR_CURSOR_TYPE to SQL_CURSOR_STATIC, SQL_CURSOR_KEYSET_DRIVEN, or SQL_CURSOR_DYNAMIC, as specified by the driver. It is never set to SQL_CURSOR_FORWARD_ONLY.|  
|SQL_ATTR_CURSOR_SENSITIVITY to SQL_INSENSITIVE|SQL_ATTR_CONCURRENCY to SQL_CONCUR_READ_ONLY.<br /><br /> SQL_ATTR_CURSOR_TYPE to SQL_CURSOR_STATIC.|  
|SQL_ATTR_CURSOR_SENSITIVITY to SQL_SENSITIVE|SQL_ATTR_CONCURRENCY to SQL_CONCUR_LOCK, SQL_CONCUR_ROWVER, or SQL_CONCUR_VALUES, as specified by the driver. It is never set to SQL_CONCUR_READ_ONLY.<br /><br /> SQL_ATTR_CURSOR_TYPE to SQL_CURSOR_FORWARD_ONLY, SQL_CURSOR_STATIC, SQL_CURSOR_KEYSET_DRIVEN, or SQL_CURSOR_DYNAMIC, as specified by the driver.|  
|SQL_ATTR_CURSOR_SENSITIVITY to SQL_UNSPECIFIED|SQL_ATTR_CONCURRENCY to SQL_CONCUR_READ_ONLY, SQL_CONCUR_LOCK, SQL_CONCUR_ROWVER, or SQL_CONCUR_VALUES, as specified by the driver.<br /><br /> SQL_ATTR_CURSOR_TYPE to SQL_CURSOR_FORWARD_ONLY, SQL_CURSOR_STATIC, SQL_CURSOR_KEYSET_DRIVEN, or SQL_CURSOR_DYNAMIC, as specified by the driver.|  
|SQL_ATTR_CURSOR_TYPE to SQL_CURSOR_DYNAMIC|SQL_ATTR_SCROLLABLE to SQL_SCROLLABLE.<br /><br /> SQL_ATTR_CURSOR_SENSITIVITY to SQL_SENSITIVE. (But only if SQL_ATTR_CONCURRENCY is not equal to SQL_CONCUR_READ_ONLY. Updatable dynamic cursors are always sensitive to changes that were made in their own transaction.)|  
|SQL_ATTR_CURSOR_TYPE to SQL_CURSOR_FORWARD_ONLY|SQL_ATTR_CURSOR_SCROLLABLE to SQL_NONSCROLLABLE.|  
|SQL_ATTR_CURSOR_TYPE to SQL_CURSOR_KEYSET_DRIVEN|SQL_ATTR_SCROLLABLE to SQL_SCROLLABLE.<br /><br /> SQL_ATTR_SENSITIVITY to SQL_UNSPECIFIED or SQL_SENSITIVE (according to driver-defined criteria, if SQL_ATTR_CONCURRENCY is not SQL_CONCUR_READ_ONLY).|  
|SQL_ATTR_CURSOR_TYPE to SQL_CURSOR_STATIC|SQL_ATTR_SCROLLABLE to SQL_SCROLLABLE.<br /><br /> SQL_ATTR_SENSITIVITY to SQL_INSENSITIVE (if SQL_ATTR_CONCURRENCY is SQL_CONCUR_READ_ONLY).<br /><br /> SQL_ATTR_SENSITIVITY to SQL_UNSPECIFIED or SQL_SENSITIVE (if SQL_ATTR_CONCURRENCY is not SQL_CONCUR_READ_ONLY).|
