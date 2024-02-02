---
title: "SQLGetData (Desktop Database Drivers)"
description: "SQLGetData (Desktop Database Drivers)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLGetData function [ODBC], Desktop Database Drivers"
---
# SQLGetData (Desktop Database Drivers)
This function can retrieve data from any column, whether or not there are bound columns after it and regardless of the order in which the columns are retrieved.  
  
> [!NOTE]  
>  \*pcbValue in **SQLGetData** may return twice as many characters as actually available when binding to ANSI data longer than 510 characters on a Jet 4.0 database. Character values of 510 or less will return the actual cbValue.
