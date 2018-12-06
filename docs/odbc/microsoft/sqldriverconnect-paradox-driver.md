---
title: "SQLDriverConnect (Paradox Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLDriverConnect function [ODBC], Paradox Driver"
  - "Paradox driver [ODBC], SQLDriverConnect"
ms.assetid: c2ba486e-5e01-4e67-adb1-68511f5f0206
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLDriverConnect (Paradox Driver)
> [!NOTE]  
>  This topic provides Paradox Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 **SQLDriverConnect** enables you to connect to a driver without creating a data source (DSN).  
  
 The following keywords are supported in the connection string for all drivers: **DSN**, **DBQ**, and **FIL**.  
  
 The **PWD** keyword is also supported. The PWD keyword should not include any of the special characters (see SQL_SPECIAL_CHARACTERS in **SQLGetInfo** Returned Values).  
  
 After a password-protected file has been opened by a user, other users are not allowed to open the same file.  
  
 The following table shows the minimum keywords required to connect to each driver, and provides an example of keyword/value pairs used with **SQLDriverConnect**. For a full list of DRIVERID values, see [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).  
  
> [!NOTE]  
>  If DBQ or DefaultDir is not specified for the Paradox driver, the driver will connect to the current directory.  
  
|Driver|Keywords required|Example|  
|------------|-----------------------|-------------|  
|Paradox|Driver, DriverID|Driver={Microsoft Paradox Driver (*.db )}; DBQ=c:\temp;DriverID=26|
