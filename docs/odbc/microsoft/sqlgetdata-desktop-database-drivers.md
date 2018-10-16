---
title: "SQLGetData (Desktop Database Drivers) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLGetData function [ODBC], Desktop Database Drivers"
ms.assetid: c9d9a32d-5dc2-4189-9bfb-2b008bc3d6a3
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetData (Desktop Database Drivers)
This function can retrieve data from any column, whether or not there are bound columns after it and regardless of the order in which the columns are retrieved.  
  
> [!NOTE]  
>  \*pcbValue in **SQLGetData** may return twice as many characters as actually available when binding to ANSI data longer than 510 characters on a Jet 4.0 database. Character values of 510 or less will return the actual cbValue.
