---
title: "Setting Options Programmatically for the Excel Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Jet-based ODBC drivers [ODBC], Excel driver"
  - "desktop database drivers [ODBC], Excel driver"
  - "ODBC desktop database drivers [ODBC], Excel driver"
  - "Excel driver [ODBC], setting options programmatically"
ms.assetid: b5ee3636-4591-427a-a65a-a2d5926fcc1a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Setting Options Programmatically for the Excel Driver
|Option|Description|Method|  
|------------|-----------------|------------|  
|Data Source Name|A name that identifies the data source, such as Payroll or Personnel.|To set this option dynamically, use the **DSN** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/odbc-jet-sqlconfigdatasource-excel-driver.md).|  
|Database|A Microsoft Access data source can be set up without selecting or creating a database. If no database is provided upon setup, the user will be prompted to choose a database file when connecting to the data source.|To set this option dynamically, use the **DBQ** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/odbc-jet-sqlconfigdatasource-excel-driver.md).|  
|Description|An optional description of the data in the data source; for example, "Hire date, salary history, and current review of all employees."|To set this option dynamically, use the **DESCRIPTION** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/odbc-jet-sqlconfigdatasource-excel-driver.md).|  
|Directory|Displays the currently selected directory.<br /><br /> For Microsoft Excel 3.0/4.0 files, the path display is labeled "Directory", while for Microsoft Excel 5.0, 7.0, or 97 files, the path display is labeled "Workbook".|To set this option dynamically, use the **DEFAULTDIR** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/odbc-jet-sqlconfigdatasource-excel-driver.md).|  
|Read Only|Designates the database as read-only.|To set this option dynamically, use the **READONLY** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/odbc-jet-sqlconfigdatasource-excel-driver.md).|  
|Rows to Scan|The number of rows to scan to determine the data type of each column. The data type is determined given the maximum number of kinds of data found. If data is encountered that does not match the data type guessed for the column, the data type will be returned as a NULL value.<br /><br /> For the Microsoft Excel driver, you can enter a number from 1 to 16 for the rows to scan. The value defaults to 8; if it is set to 0, all rows are scanned. (A number outside the limit will return an error.)|To set this option dynamically, use the **MAXSCANROWS** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/odbc-jet-sqlconfigdatasource-excel-driver.md).|  
|Select Directory|Displays a dialog box where you can select a directory containing the files you want to access.<br /><br /> When defining a data source directory (for all drivers except Microsoft Access), specify the directory where your most commonly used files are located. The ODBC driver uses this directory as the default directory. Copy other files into this directory if they are used frequently. Alternatively, you can qualify file names in a SELECT statement with the directory name:<br /><br /> SELECT \* FROM C:\MYDIR\EMP<br /><br /> Or, you can specify a new default directory by using the **SQLSetConnectOption** function with the SQL_CURRENT_QUALIFIER option.<br /><br /> For Microsoft Excel 3.0 or 4.0 files, the path display is labeled "Directory", and the path selection button is labeled "Select Directory". For Microsoft Excel 5.0, 7.0, or 97 files, the path display is labeled "Workbook", and the path selection button is labeled "Select Workbook". When defining a data source directory, specify the directory where your most commonly used Microsoft Excel files are located for Microsoft Excel 3.0/4.0, or the directory where the workbook file is located for Microsoft Excel 5.0, 7.0, or 97. **Use Current Directory** is disabled for Microsoft Excel 5.0, 7.0, and 97.|To set this option dynamically, use the **DEFAULTDIR** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/odbc-jet-sqlconfigdatasource-excel-driver.md).|
