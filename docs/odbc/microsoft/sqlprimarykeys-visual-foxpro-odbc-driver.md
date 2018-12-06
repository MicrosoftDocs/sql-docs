---
title: "SQLPrimaryKeys (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLPrimaryKeys function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 8dbe2903-efdc-45e0-a079-9e357c5fd81b
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLPrimaryKeys (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Full  
  
 ODBC API Conformance: Level 2  
  
 Returns the column names that comprise the primary key for a table. The Visual FoxPro ODBC Driver implementation of **SQLPrimaryKeys** behaves as follows:  
  
-   Ignores the *szTableOwner* and *cbTableOwner* arguments.  
  
-   Works only for data sources that are [databases](../../odbc/microsoft/visual-foxpro-terminology.md). The driver returns the error "Driver does not support this function" if the data source is a directory of [free tables](../../odbc/microsoft/visual-foxpro-terminology.md).  
  
 For more information, see [SQLPrimaryKeys](../../odbc/reference/syntax/sqlprimarykeys-function.md) in the *ODBC Programmer's Reference*.
