---
description: "Setting Options Programmatically for the Access Driver"
title: "Setting Options Programmatically for the Access Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Access driver [ODBC], setting options programmatically"
  - "ODBC desktop database drivers [ODBC], Access driver"
  - "Jet-based ODBC drivers [ODBC], Access driver"
  - "desktop database drivers [ODBC], Access driver"
ms.assetid: 1690eb71-0cd3-4c00-9e15-f6a3ac5316dd
author: David-Engel
ms.author: v-davidengel
---
# Setting Options Programmatically for the Access Driver

|Option|Description|Method|  
|------------|-----------------|------------|  
|Buffer Size|The size of the internal buffer, in kilobytes, that is used by Microsoft Access to transfer data to and from the disk. The default buffer size is 2048 KB (displayed as 2048). Any integer value divisible by 256 can be entered.|To set this option dynamically, use the MAXBUFFERSIZE keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).|  
|Data Source Name|A name that identifies the data source, such as Payroll or Personnel.|To set this option dynamically, use the **DSN** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).|  
|Database|A Microsoft Access data source can be set up without selecting or creating a database. If no database is provided upon setup, the user will be prompted to choose a database file when connecting to the data source.|To set this option dynamically, use the **DBQ** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).|  
|Description|An optional description of the data in the data source; for example, "Hire date, salary history, and current review of all employees."|To set this option dynamically, use the **DESCRIPTION** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).|  
|Exclusive|If the **Exclusive** box is selected, the database will be opened in Exclusive mode and can be accessed by only one user at a time. Performance is enhanced when running in Exclusive mode.|To set this option dynamically, use the **EXCLUSIVE** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).|  
|ImplicitCommitSync|Determines how changes made outside of a transaction are written to the database. This value is initially set to "Yes", which means that the Microsoft Access driver will wait for commits in an internal/implicit transaction to be completed.|This option is included in the **Set Advanced Options** dialog box for the Microsoft Access driver.|  
|Page Timeout|Specifies the period of time, in milliseconds, that a page (if not used) remains in the buffer before being removed. For the Microsoft Access driver, the default is 500 milliseconds (0.5 seconds). This option applies to all data sources that use the ODBC driver.<br /><br /> The page timeout cannot be 0 because of an inherent delay. The page timeout cannot be less than the inherent delay, even if the page timeout option is set below that value.|To set this option dynamically, use the **PAGETIMEOUT** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).|  
|Read Only|Designates the database as read-only.|To set this option dynamically, use the **READONLY** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).|  
|System Database|The full path of the Microsoft Access system database to be used with the Microsoft Access database you want to access.<br /><br /> Click the **System Database** button to select the system database to be used. The ODBC Microsoft Access driver prompts the user for a name and password. The default name is Admin and the default password in Microsoft Access for the Admin user is an empty string.<br /><br /> To increase the security of your Microsoft Access database, create a new user to replace the Admin user and delete the Admin user, or change the objects to which the Admin user has access.|To set this option dynamically, use the **SYSTEMDB** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).|  
|Threads|The number of background threads for the engine to use. For the Microsoft Access driver, this value defaults to 3, but can be changed. The user may want to increase the number of threads if there is a large amount of activity in the database.<br /><br /> This option is included in the **Set Advanced Options** dialog box for the Microsoft Access driver.|To set this option dynamically, use the **THREADS** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).|  
|UserCommitSync|Determines whether the Microsoft Access driver will perform an explicit user-defined transactions asynchronously. This value is initially set to "Yes", which means that the Microsoft Access driver will wait for commits in a user-defined transaction to be completed.<br /><br /> Setting this option to False can have unpredictable consequences in a multi-user environment.|To set this option dynamically, use the **USERCOMMITSYNC** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-access-driver.md).|
