---
title: "SQLConfigDataSource (Paradox Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLConfigDataSource function [ODBC], Paradox Driver"
  - "Paradox driver [ODBC], SQLConfigDataSource"
ms.assetid: 59e84c4e-debe-49d7-b97b-84c736b0c793
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLConfigDataSource (Paradox Driver)
> [!NOTE]  
>  This topic provides Paradox Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 The **SQLConfigDataSource** function that is used to add, modify, or delete a data source dynamically uses the following keywords.  
  
|Keyword|Description|  
|-------------|-----------------|  
|COLLATINGSEQUENCE|The sequence in which the fields are sorted.<br /><br /> When the Paradox driver is used, the sequence can be ASCII (default), International, Swedish-Finnish, or Norwegian-Danish.<br /><br /> This sets the same option as **Collating Sequence** in the setup dialog box.|  
|DBQ|The name of the database file.<br /><br /> This sets the same option as **Database** in the setup dialog box.|  
|DEFAULTDIR|The path specification to the directory.|  
|DESCRIPTION|A description of the data in the data source.<br /><br /> This sets the same option as **Description** in the setup dialog box.|  
|DRIVER|The path specification to the driver DLL.|  
|DRIVERID|An integer ID for the driver.<br /><br /> 26 (Paradox 3.x)<br /><br /> 282 (Paradox 4.x)<br /><br /> 538 (Paradox 5.x)|  
|EXCLUSIVE|Determines whether the database will be opened in exclusive mode (accessed by only one user at a time) or shared mode (accessed by more than one user at a time). Can be true (exclusive mode) or false (shared mode).<br /><br /> This sets the same option as **Exclusive** in the setup dialog box.|  
|FIL|File type  Paradox 3.x, Paradox 4.x, or Paradox 5.x|  
|FILETYPE|File type for the Text driver (Text).|  
|PAGETIMEOUT|Specifies the period of time, in tenths of a second, that a page (if not used) remains in the buffer before being removed. The default is 600 tenths of a second (60 seconds). Note that this option applies to all data sources that use the ODBC driver.<br /><br /> This sets the same option as **Page Timeout** in the setup dialog box.|  
|PARADOXNETPATH|The full path of the directory containing a Paradox lock database, because it contains either the PDOXUSRS.net file (in Paradox 4.*x*) or the PARADOX.net file (in Paradox 5.*x*). If the directory does not contain one of these files, the Paradox driver creates one. For information about these files, see the Paradox documentation.<br /><br /> Before a network directory can be selected, a Paradox user name must be entered.<br /><br /> This sets the same option as **Select Network Directory** in the setup dialog box.|  
|PARADOXNETSTYLE|For the Paradox driver, the network access style to use when accessing Paradox data: either "3.x" for Paradox 3.*x* or "4.x" for Paradox 4.*x* or 5.*x*. Can be set to "3.x" or "4.x" if the version is Paradox 4.*x* or 5.*x*; if the version is Paradox 3.*x*, the style must be "3.x".<br /><br /> This sets the same option as **Net Style** in the setup dialog box.|  
|PARADOXUSERNAME|For the Paradox driver, the Paradox user name.<br /><br /> This sets the same option as **User Name** in the setup dialog box.|  
|PWD|The password.<br /><br /> This is an optional keyword and will never be written to the file by the driver. It is used in a call to **SQLDriverConnect** against password-secured Paradox files. The password used will be valid whenever a table is opened. If no password is passed in the connection string, no password is established for that table. If tables have different passwords, more than one cannot be opened in the same session, nor can the tables be joined.|  
|READONLY|TRUE to make file read-only; FALSE to make file not read-only.<br /><br /> This sets the same option as **Read Only** in the setup dialog box.|  
|THREADS|The number of background threads for the engine to use. This value is 3, and cannot be changed.<br /><br /> This sets the same option as **Threads** in the setup dialog box.|
