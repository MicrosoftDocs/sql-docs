---
title: "SQLConfigDataSource (Text File Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "text file driver [ODBC], SQLConfigDataSource"
  - "SQLConfigDataSource function [ODBC], Text File Driver"
ms.assetid: c505d36e-1e72-47b2-a9e5-e4926b408468
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLConfigDataSource (Text File Driver)
> [!NOTE]  
>  This topic provides Text File Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 The **SQLConfigDataSource** function that is used to add, modify, or delete a data source dynamically uses the following keywords.  
  
|Keyword|Description|  
|-------------|-----------------|  
|CHARACTERSET|For the Text driver, OEM or ANSI.|  
|COLNAMEHEADER|For the Text driver, indicates whether the first record of data will specify the column names. Either TRUE or FALSE.|  
|DEFAULTDIR|The path specification to the directory.|  
|DESCRIPTION|A description of the data in the data source.<br /><br /> This sets the same option as **Description** in the setup dialog box.|  
|DRIVER|The path specification to the driver DLL.|  
|DRIVERID|An integer ID for the driver. 27 (Text)|  
|EXTENSIONS|Lists the file name extensions of the Text files on the data source.<br /><br /> This sets the same option as **Extensions List** in the setup dialog box.|  
|FIL|File type   Text|  
|FILETYPE|File type for the Text driver (Text).|  
|FORMAT|For the Text driver, can be FIXEDLENGTH, TABDELIMITED, CSVDELIMITED (by a comma), or DELIMITED() (by the special character specified in the parentheses). The special character is one character in length and can be in character, decimal, or hexadecimal format.|  
|MAXSCANROWS|The number of rows to be scanned when setting a column's data type based upon existing data.<br /><br /> For the Text driver, you can enter a number from 1 to 32767 for the number of rows to scan; however, the value will always default to 25. (A number outside the limit will return an error.)<br /><br /> This sets the same option as **Rows to Scan** in the setup dialog box.|  
|READONLY|TRUE to make file read-only; FALSE to make file not read-only.<br /><br /> This sets the same option as **Read Only** in the setup dialog box.|
