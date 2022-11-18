---
description: "SQLDriverConnect (Access Driver)"
title: "SQLDriverConnect (Access Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Access driver [ODBC], SQLDriverConnect"
  - "SQLDriverConnect function [ODBC], Access Driver"
ms.assetid: 9d133e9b-7545-464d-aa3c-677fa7e2a41d
author: David-Engel
ms.author: v-davidengel
---
# SQLDriverConnect (Access Driver)
> [!NOTE]  
>  This topic provides Access Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 **SQLDriverConnect** enables you to connect to a driver without creating a data source (DSN).  
  
 The following keywords are supported in the connection string for all drivers: **DSN**, **DBQ**, and **FIL**.  
  
 The **UID** and **PWD** keywords are also supported.  
  
 The PWD keyword should not include any of the special characters (see SQL_SPECIAL_CHARACTERS in **SQLGetInfo** Returned Values).  
  
 The following table shows the minimum keywords required to connect to each driver, and provides an example of keyword/value pairs used with **SQLDriverConnect**. For a full list of DRIVERID values, see [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).  
  
|Driver|Keywords Required|Examples|  
|------------|-----------------------|--------------|  
|Microsoft Access|Driver, DBQ|Driver={Microsoft Access Driver (*.mdb)}; DBQ=c:\\\temp\\\sample.mdb|
