---
title: "SQLConfigDataSource (Access Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLConfigDataSource function [ODBC], Access Driver"
  - "Access driver [ODBC], SQLConfigDataSource"
ms.assetid: 1b152fb7-fa12-46b9-b168-006bb1355e77
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLConfigDataSource (Access Driver)
> [!NOTE]  
>  This topic provides Access Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 The **SQLConfigDataSource** function that is used to add, modify, or delete a data source dynamically uses the following keywords.  
  
|Keyword|Description|  
|-------------|-----------------|  
|COLLATINGSEQUENCE|The sequence in which the fields are sorted.<br /><br /> This sets the same option as **Collating Sequence** in the setup dialog box.|  
|COMPACT_DB|Performs data compaction on a database file. Has the following format: COMPACT_DB=<path_name><optionaL_sort_order>\<optional ENCRYPT keyword>.<br /><br /> When using the COMPACT_DB keyword in the same statement with a DSN keyword, this driver ignores the DSN keyword. Therefore, compacting a database and specifying a DSN is a two-step process.|  
|CREATE_DB|Creates a database file. Has the following format: CREATE_DB=<path_name>\<optional_sort-order><optional_ENCRYPT keyword>, where the path name is the full path to a Microsoft Access database. An error will be returned if the path name specifies an existing database. The sort order will be as set up in the New Database dialog box displayed when the Create button is pressed in the Microsoft Access Setup dialog box. If no sort order is specified, General is used.<br /><br /> When using the CREATE_DB keyword in the same statement with a DSN keyword, this driver ignores the DSN keyword. Therefore, creating a database and specifying a DSN is a two-step process.When using the CREATE_DB keyword, if the pathname of the Microsoft Access database to be created contains one or more spaces, then the entire pathname must be enclosed by double quotation marks, as shown in the following examples:<br /><br /> "C:\PROGRAM FILES\COMMON FILES\ MyAccess.mdb"<br /><br /> "C:\PROGRAM FILES\Access2.mdb"<br /><br /> CREATE_DB=C:\TEMP\test.mdb (no quotation marks needed)|  
|CREATE_SYSDB|Creates a system database file. Has the following format: CREATE_SYSDB=\<path-name>\<optional-sort-order>, where the path name is the full path to a Microsoft Access database. An error will be returned if the path name specifies an existing database. The sort order will be as set up in the **New Database** dialog box displayed when the **Create** button is clicked in the **ODBC Microsoft Access Setup** dialog box. If no sort order is specified, General is used.|  
|CREATE_V2DB|Creates a database file that is compatible with Microsoft Access 2.0. Has the following format: CREATE_V2DB=\<path-name>\<optional-sort-order>, where the path name is the full path to a Microsoft Access database. An error will be returned if the path name specifies an existing database. The sort order will be as set up in the New Database dialog box displayed when the Create button is pressed in the Microsoft Access Setup dialog box. If no sort order is specified, General is used.<br /><br /> When using the CREATE_V2DB keyword in the same statement with a DSN keyword, this driver ignores the DSN keyword. Therefore, creating a database and specifying a DSN is a two-step process.<br /><br /> When using the CREATE_V2DB keyword, if the pathname of the Microsoft Access database to be created contains one or more spaces, then the entire pathname must be enclosed by double quotation marks, as shown in the following examples:<br /><br /> "C:\PROGRAM FILES\COMMON FILES\ MyAccess.mdb"<br /><br /> "C:\PROGRAM FILES\Access2.mdb"<br /><br /> CREATE_V2DB=C:\TEMP\test.mdb (no quotation marks needed)|  
|DBQ|The name of the database file.<br /><br /> This sets the same option as **Database** in the setup dialog box.|  
|DEFAULTDIR|The path specification to the database file.|  
|DESCRIPTION|A description of the data in the data source.<br /><br /> This sets the same option as **Description** in the setup dialog box.|  
|DRIVER|The path specification to the driver DLL.|  
|DRIVERID|An integer ID for the driver.  25 (Microsoft Access)|  
|FIL|File type   MS Access for Microsoft Access|  
|IMPLICITCOMMITSYNC|Determines whether the Microsoft Access driver will perform internal or implicit commits asynchronously. This value is initially set to "Yes", which means that the Microsoft Access driver will wait for commits in an internal/implicit transaction to be completed.<br /><br /> The value of this option should not be changed without careful consideration of the consequences. For more information about the option, see the *Microsoft Jet Database Engine Programmer's Guide*.<br /><br /> This sets the same option as **ImplicitCommitSync** in the setup dialog box.|  
|MAXBUFFERSIZE|The size of the internal buffer, in kilobytes, that is used by Microsoft Access to transfer data to and from the disk. The default buffer size is 2048 KB (displayed as 2048). Any integer value divisible by 256 can be used. This sets the same option as **Buffer Size** in the setup dialog box.|  
|MAXSCANROWS|The number of rows to be scanned when setting a column's data type based upon existing data.<br /><br /> A number from 1 to 16 can be entered for the rows to scan. The value defaults to 8; if it is set to 0, all rows are scanned. (A number outside the limit will return an error.)<br /><br /> This sets the same option as **Rows to Scan** in the setup dialog box.|  
|PAGETIMEOUT|Specifies the period of time, in milliseconds, that a page (if not used) remains in the buffer before being removed. The default is five-tenths of a second (0.5 seconds). Note that this option applies to all data sources that use the ODBC driver.<br /><br /> This sets the same option as **Page Timeout** in the setup dialog box.|  
|PWD|The password.|  
|READONLY|TRUE to make file read-only; FALSE to make file not read-only.<br /><br /> This sets the same option as **Read Only** in the setup dialog box.|  
|REPAIR_DB|Repairs a database damaged by a failure that occurs during the commit process.<br /><br /> When using the REPAIR_DB keyword in the same statement with a DSN keyword, this driver ignores the DSN keyword. Therefore, repairing a database and specifying a DSN is a two-step process.|  
|SYSTEMDB|For the Microsoft Access driver, the path specification to the system database file.<br /><br /> This sets the same option as **System Database** in the setup dialog box.|  
|THREADS|The number of background threads for the engine to use. This value defaults to 3, but can be changed.<br /><br /> This sets the same option as **Threads** in the setup dialog box.|  
|UID|For the Microsoft Access driver, the user ID name used for login.|  
|USERCOMMITSYNC|Determines whether the Microsoft Access driver will perform user-defined transactions asynchronously. This value is initially set to "Yes", which means that the Microsoft Access driver will wait for commits in a user-defined transaction to be completed.<br /><br /> The value of this option should not be changed without careful consideration of the consequences. For more information about the option, see the *Microsoft Jet Database Engine Programmer's Guide*.<br /><br /> This sets the same option as **UserCommitSync** in the setup dialog box.|
