---
title: "Setting Options Programmatically for the Paradox Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC desktop database drivers [ODBC], Paradox driver"
  - "Paradox driver [ODBC], setting options programmatically"
  - "desktop database drivers [ODBC], Paradox driver"
  - "Jet-based ODBC drivers [ODBC], Paradox driver"
ms.assetid: 7996d3f8-b5f5-4cac-8a66-fc96a42b603e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Setting Options Programmatically for the Paradox Driver

|Option|Description|Method|  
|------------|-----------------|------------|  
|Directory|Sets the targeted directory.|To set this option dynamically, use the **DEFAULTDIR** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).|  
|Collating Sequence|The sequence in which the fields are sorted.<br /><br /> The sequence can be ASCII (the default), International, Swedish-Finnish, or Norwegian-Danish.|To set this option dynamically, use the **COLLATINGSEQUENCE** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).|  
|Description|An optional description of the data in the data source; for example, "Hire date, salary history, and current review of all employees."|To set this option dynamically, use the **DESCRIPTION** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).|  
|Exclusive|If the **Exclusive** box is selected, the database will be opened in Exclusive mode and can be accessed by only one user at a time. Performance is enhanced when running in Exclusive mode.|To set this option dynamically, use the **EXCLUSIVE** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).|  
|Net Style|The network access style to use when accessing Paradox data: either "3.*x*" for Paradox 3.*x* or "4.*x*" for Paradox 4.*x* or 5.*x*. Can be set to "3.*x*" or "4.*x*" if the version is Paradox 4.*x* or 5.*x*; if the version is Paradox 3.*x*, the style must be "3.*x*".|To set this option dynamically, use the **PARADOXNETSTYLE** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).|  
|Page Timeout|Specifies the period of time, in tenths of a second, that a page (if not used) remains in the buffer before being removed. The default is 600 tenths of a second (60 seconds). This option applies to all data sources that use the ODBC driver.<br /><br /> The page timeout cannot be 0 because of an inherent delay. The page timeout cannot be less than the inherent delay, even if the page timeout option is set below that value.|To set this option dynamically, use the **PAGETIMEOUT** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).|  
|Read Only|Designates the database as read-only.|To set this option dynamically, use the **READONLY** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).|  
|Select Directory|Displays a dialog box where you can select a directory containing the files you want to access.<br /><br /> When defining a data source directory specify the directory where your most commonly used files are located. The ODBC driver uses this directory as the default directory. Copy other files into this directory if they are used frequently. Alternatively, you can qualify file names in a SELECT statement with the directory name:<br /><br /> SELECT \* FROM C:\MYDIR\EMP<br /><br /> Or, you can specify a new default directory by using the **SQLSetConnectOption** function with the SQL_CURRENT_QUALIFIER option.|To set this option dynamically, use the **DEFAULTDIR** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).|  
|Select Network Directory|The full path of the directory containing a Paradox lock database, because it contains either the Pdoxusrs.net file (in Paradox 4.*x*) or the Paradox.net file (in Paradox 5.*x*). If the directory does not contain one of these files, the Paradox driver creates one. For information about these files, see the Paradox documentation.<br /><br /> Before you can select a network directory, you must enter your Paradox user name in the **User Name** text box. Click **Select Network Directory** to select a network directory.|To set this option dynamically, use the **PARADOXNETPATH** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).|  
|User Name|The Paradox user name. This is the name displayed to other users of Paradox files when a lock is encountered.|To set this option dynamically, use the **PARADOXUSERNAME** keyword in a call to [SQLConfigDataSource](../../odbc/microsoft/sqlconfigdatasource-paradox-driver.md).|
