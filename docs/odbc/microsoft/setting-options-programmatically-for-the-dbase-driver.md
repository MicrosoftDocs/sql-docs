---
description: "Setting Options Programmatically for the dBASE Driver"
title: "Setting Options Programmatically for the dBASE Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Jet-based ODBC drivers [ODBC], DBasedriver"
  - "desktop database drivers [ODBC], DBasedriver"
  - "DBase driver [ODBC], setting options programmatically"
  - "ODBC desktop database drivers [ODBC], DBasedriver"
ms.assetid: 336d0fd4-5448-4d8c-b7d9-49e857228e36
author: David-Engel
ms.author: v-davidengel
---
# Setting Options Programmatically for the dBASE Driver

|Option|Description|Method|  
|------------|-----------------|------------|  
|Approximate Row Count|Determines whether table size statistics are approximated. This option applies to all data sources that use the ODBC driver.|To set this option dynamically, use the **STATISTICS** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-dbase-driver.md).|  
|Collating Sequence|The sequence in which the fields are sorted.<br /><br /> The sequence can be: ASCII (the default) or International.|To set this option dynamically, use the **COLLATINGSEQUENCE** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-dbase-driver.md).|  
|Data Source Name|A name that identifies the data source, such as Payroll or Personnel.|To set this option dynamically, use the **DSN** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-dbase-driver.md).|  
|Database|A Microsoft Access data source can be set up without selecting or creating a database. If no database is provided upon setup, users will be prompted to select a database file when they connect to the data source.|To set this option dynamically, use the **DBQ** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-dbase-driver.md).|  
|Description|An optional description of the data in the data source; for example, "Hire date, salary history, and current review of all employees."|To set this option dynamically, use the **DESCRIPTION** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-dbase-driver.md).|  
|Exclusive|If the **Exclusive** box is selected, the database will be opened in Exclusive mode and can be accessed by only one user at a time. Performance is enhanced when it runs in Exclusive mode.|To set this option dynamically, use the **EXCLUSIVE** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-dbase-driver.md).|  
|Page Timeout|Specifies the period of time, in tenths of a second, that a page (if not used) remains in the buffer before it is removed. The default is 600 tenths of a second (60 seconds). This option applies to all data sources that use the ODBC driver.<br /><br /> The page timeout cannot be 0 because of an inherent delay. The page timeout cannot be less than the inherent delay, even if the page timeout option is set below that value.|To set this option dynamically, use the **PAGETIMEOUT** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-dbase-driver.md).|  
|Read Only|Designates the database as read-only.|To set this option dynamically, use the **READONLY** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-dbase-driver.md).|  
|Select Directory|Displays a dialog box where you can select a directory that contains the files you want to access.<br /><br /> When you define a data source directory, specify the directory where your most frequently used files are located. The ODBC driver uses this directory as the default directory. Copy other files into this directory if they are used frequently. Alternatively, you can qualify file names in a SELECT statement with the directory name:<br /><br /> SELECT \* FROM C:\MYDIR\EMP<br /><br /> Or, you can specify a new default directory by using the **SQLSetConnectOption** function with the SQL_CURRENT_QUALIFIER option.|To set this option dynamically, use the **DEFAULTDIR** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-dbase-driver.md).|  
|Show Deleted Rows|Specifies whether rows that have been marked as deleted can be retrieved or positioned on. If cleared, deleted rows are not displayed; if selected, deleted rows are treated the same as non-deleted rows. The default is cleared.|To set this option dynamically, use the **DELETED** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-dbase-driver.md).|
