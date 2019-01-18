---
title: "SQLConfigDataSource (dBASE Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "DBase driver [ODBC], SQLConfigDataSource"
  - "SQLConfigDataSource function [ODBC], dBASE Driver"
ms.assetid: 19909902-054c-4e19-9c06-a212aace13fe
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLConfigDataSource (dBASE Driver)
> [!NOTE]  
>  This topic provides dBASE Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 The **SQLConfigDataSource** function that is used to add, modify, or delete a data source dynamically uses the following keywords.  
  
|Keyword|Description|  
|-------------|-----------------|  
|COLLATINGSEQUENCE|The sequence in which the fields are sorted.<br /><br /> The sequence can be: ASCII (the default) or International.<br /><br /> This sets the same option as **Collating Sequence** in the setup dialog box.|  
|DEFAULTDIR|The path specification to the directory.|  
|DELETED|For the dBASE driver, specifies whether or not rows that have been marked as deleted can be retrieved or positioned on. If set to 1, deleted rows are not displayed; if set to 0, deleted rows are treated the same as non-deleted rows.<br /><br /> This sets the same option as **Show Deleted Rows** in the setup dialog box.|  
|DESCRIPTION|A description of the data in the data source.<br /><br /> This sets the same option as **Description** in the setup dialog box.|  
|DRIVER|The path specification to the driver DLL.|  
|DRIVERID|An integer ID for the driver.<br /><br /> 21 (dBASE III)<br /><br /> 277 (dBASE IV)<br /><br /> 533 (dBASE 5.0)|  
|FIL|File type   dBase III, dBase IV, or dBase 5|  
|PAGETIMEOUT|Specifies the period of time, in tenths of a second, that a page (if not used) remains in the buffer before being removed. The default is 600 tenths of a second (60 seconds). Note that this option applies to all data sources that use the ODBC driver.<br /><br /> This sets the same option as **Page Timeout** in the setup dialog box.|  
|READONLY|TRUE to make file read-only; FALSE to make file not read-only.<br /><br /> This sets the same option as **Read Only** in the setup dialog box.|  
|STATISTICS|For the dBASE driver, determines whether table size statistics are approximated. Note that this option applies to all data sources that use the ODBC driver.<br /><br /> This sets the same option as **Approximate Row Count** in the setup dialog box.|  
|THREADS|The number of background threads for the engine to use. This value is 3 and cannot be changed.<br /><br /> This sets the same option as **Threads** in the setup dialog box.|
