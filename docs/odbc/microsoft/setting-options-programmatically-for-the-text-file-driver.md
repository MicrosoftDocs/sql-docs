---
title: "Setting Options Programmatically for the Text File Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "text file driver [ODBC], setting options programmatically"
  - "ODBC desktop database drivers [ODBC], text file driver"
  - "desktop database drivers [ODBC], text file driver"
  - "Jet-based ODBC drivers [ODBC], text file driver"
ms.assetid: cbde2ca1-5d4e-4444-a371-a72f3ac4d92a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Setting Options Programmatically for the Text File Driver

|Option|Description|Method|  
|------------|-----------------|------------|  
|Data Source Name|A name that identifies the data source, such as Payroll or Personnel.|To set this option dynamically, use the **DSN** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-text-file-driver.md).|  
|Define Format|Displays the **Define Text Format** dialog box and enables you to specify the schema for individual tables in the data source directory.|This option cannot be set dynamically by a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-text-file-driver.md).|  
|Description|An optional description of the data in the data source; for example, "Hire date, salary history, and current review of all employees."|To set this option dynamically, use the **DESCRIPTION** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-text-file-driver.md).|  
|Directory|Selects the targeted directory.|To set this option dynamically, use the **DEFAULTDIR** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-text-file-driver.md).|  
|Extensions List|Lists the file name extensions of the text files on the data source. When the Text driver is used, a file with no extension is created when the CREATE TABLE statement is executed with a name that has no extension. Other drivers create a file with a default extension when no extension is provided. To create a file with a .txt extension, the extension must be included in the name. To display files without extensions in the **Define Text Format** dialog box, "*." must be added to the Extensions List.|To set this option dynamically, use the **EXTENSIONS** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-text-file-driver.md).|  
|Read Only|Designates the database as read-only.|To set this option dynamically, use the **READONLY** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-text-file-driver.md).|  
|Rows to Scan|The number of rows to scan to determine the data type of each column. The data type is determined given the maximum number of kinds of data found. If data is encountered that does not match the data type guessed for the column, the data type will be returned as a NULL value.<br /><br /> For the Text driver, you may enter a number from 1 to 32767 for the number of rows to scan; however, the value will always default to 25. (A number outside the limit will return an error.)|To set this option dynamically, use the **MAXSCANROWS** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-text-file-driver.md).|  
|Select Directory|Displays a dialog box where you can select a directory containing the files you want to access.<br /><br /> When defining a data source directory specify the directory where your most commonly used files are located. The ODBC driver uses this directory as the default directory. Copy other files into this directory if they are used frequently. Alternatively, you can qualify file names in a SELECT statement with the directory name: `SELECT * FROM C:\MYDIR\EMP`<br /><br /> Or, you can specify a new default directory by using the **SQLSetConnectOption** function with the SQL_CURRENT_QUALIFIER option.|To set this option dynamically, use the **DEFAULTDIR** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-text-file-driver.md).|
