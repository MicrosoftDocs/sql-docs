---
description: "ODBC Jet SQLConfigDataSource (Excel Driver)"
title: "ODBC Jet SQLConfigDataSource (Excel Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLConfigDataSource function [ODBC], Excel Driver"
  - "Excel driver [ODBC], SqlConfigDataSource"
ms.assetid: 885b3bea-f4b6-4902-b994-f78a912b612f
author: David-Engel
ms.author: v-davidengel
---
# ODBC Jet SQLConfigDataSource (Excel Driver)
> [!NOTE]  
>  This topic provides Excel Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 The **SQLConfigDataSource** function that is used to add, modify, or delete a data source dynamically uses the following keywords.  
  
|Keyword|Description|  
|-------------|-----------------|  
|DBQ|For the Microsoft Excel driver when accessing Microsoft Excel 5.0 or later files, the name of the workbook file.<br /><br /> This sets the same option as **Database** in the setup dialog box.|  
|DEFAULTDIR|The path specification to the directory.<br /><br /> This sets the same option as **Select Directory** or **Select Workbook** in the setup dialog box.|  
|DESCRIPTION|A description of the data in the data source.<br /><br /> This sets the same option as **Description** in the setup dialog box.|  
|DRIVER|The path specification to the driver DLL.|  
|DRIVERID|An integer ID for the driver.<br /><br /> 534 (Microsoft Excel 3.0)<br /><br /> 278 (Microsoft Excel 4.0)<br /><br /> 22 (Microsoft Excel 5.0/7.0)<br /><br /> 790 (Microsoft Excel 97-2003)|  
|FIL|File type, for example, Excel 3.0, Excel 4.0, Excel 5.0, Excel 7.0, Excel 97, Excel 2000, or Excel 2003.|  
|FIRSTROWHASNAMES|Indicates whether the cells of the first row of the range contain the column names for the table (1) or not (0).|  
|MAXSCANROWS|The number of rows to be scanned when setting a column's data type based upon existing data.<br /><br /> A number from 1 to 16 can be entered for the rows to scan. The value defaults to 8; if it is set to 0, all rows are scanned. (A number outside the limit will return an error.)<br /><br /> This sets the same option as **Rows to Scan** in the setup dialog box.|  
|READONLY|TRUE to make file read-only; FALSE to make file not read-only.<br /><br /> This sets the same option as **Read Only** in the setup dialog box.|  
|THREADS|The number of background threads for the engine to use. For the Microsoft Access driver, this value defaults to 3, but can be changed. For the dBASE, MicrosoftExceldriver this value is 3, and cannot be changed.<br /><br /> This sets the same option as **Threads** in the setup dialog box.|
