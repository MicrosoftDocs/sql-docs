---
title: "Schema Views | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "schema views [ODBC]"
  - "functions [ODBC], catalog functions"
  - "catalog functions [ODBC], schema views"
ms.assetid: f1dfb3e8-a7bd-46c3-92b6-c19531e8409d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Schema Views
An application can retrieve metadata information from the DBMS either by calling ODBC catalog functions or by using INFORMATION_SCHEMA views. The views are defined by the ANSI SQL-92 standard.  
  
 If supported by the DBMS and the driver, the INFORMATION_SCHEMA views provide a more powerful and comprehensive means of retrieving metadata than the ODBC catalog functions provide. An application can execute its own custom **SELECT** statement against one of these views, can join views, or can perform a union on views. While offering greater utility and a wider range of metadata, INFORMATION_SCHEMA views are not often supported by the DBMS. This might change as more DBMSs and drivers achieve compliance with SQL-92.  
  
 To determine which views are supported, an application calls **SQLGetInfo** with the SQL_INFO_SCHEMA_VIEWS option. To retrieve metadata from a supported view, the application executes a **SELECT** statement that specifies the schema information required.
