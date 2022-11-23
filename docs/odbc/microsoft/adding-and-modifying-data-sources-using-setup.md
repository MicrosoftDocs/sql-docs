---
description: "Adding and Modifying Data Sources Using Setup"
title: "Adding and Modifying Data Sources Using Setup | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [ODBC], adding"
  - "editing data sources [ODBC], ODBC driver for Oracle"
  - "adding data sources [ODBC], ODBC driver for Oracle"
  - "data sources [ODBC], changing"
  - "data sources [ODBC], ODBC driver for Oracle"
  - "ODBC driver for Oracle [ODBC], adding data sources"
ms.assetid: 54b2d61d-6ce5-45af-a776-e03180470ecf
author: David-Engel
ms.author: v-davidengel
---
# Adding and Modifying Data Sources Using Setup
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 A data source identifies a path to data that can include a network library, server, database, and other attributes - in this case, the data source is the path to an Oracle database. To connect to a data source, the Driver Manager checks the Windows registry for specific connection information.  
  
 The registry entry created by the ODBC Data Source Administrator is used by the ODBC Driver Manager and ODBC drivers. This entry contains information about each data source and its associated driver. Before you can connect to a data source, its connection information must be added to the registry.  
  
 To add and configure data sources, use the [ODBC Data Source Administrator](../../odbc/admin/odbc-data-source-administrator.md). The ODBC Administrator updates your data source connection information. As you add data sources, the ODBC Administrator updates the registry information for you.  
  
### To add a data source for Windows  
  
1.  Open the ODBC Data Source Administrator.  
  
2.  In the ODBC Data Source Administrator dialog box, click Add. The Creat New Data Source dialog box appears.  
  
3.  Select Microsoft ODBC for Oracle and then click Finish. The Microsoft ODBC for Oracle Setup dialog box appears.  
  
4.  In the Data Source Name box, type the name of the data source you want to access. It can be any name that you choose.  
  
5.  In the Description box, type the description for the driver. This optional field describes the database driver that the data source connects to. It can be any name that you choose.  
  
6.  In the User Name box, type your database user name (your database user ID).  
  
7.  In the Server box, type the Database Alias or connect string for the Oracle Server engine that you want to access.  
  
8.  Click OK to add this data source.  
  
> [!NOTE]  
>  The Data Sources dialog box appears, and the ODBC Administrator updates the registry information. The user name and connect string that you typed become the default connection values for this data source when you connect to it.  
  
1.  Click Options make more specifications about the ODBC Driver for Oracle setup:  
  
    -   **Translation** - Click Select to choose a loaded data translator. The default is \<No Translator>.  
  
    -   **Performance** - The Include REMARKS in Catalog Functions check box specifies whether the driver returns Remarks columns for the [SQLColumns](../../odbc/microsoft/level-1-api-functions-odbc-driver-for-oracle.md) result set. The ODBC Driver for Oracle provides faster access when this value is not set.  
  
         The Include SYNONYMS in SQL Columns check box specifies whether the driver returns column information. **Buffer Size** specifies the size, in bytes, allocated to receive fetched data. The driver optimizes fetching so that one fetch from the Oracle Server returns enough rows to fill a buffer of the specified size. Larger values tend to increase performance when fetching a lot of data.  
  
    -   **Customization** - The Enforce ODBC DayOfWeek Standard check box specifies whether the result set will conform to the ODBC specified day-of-week format (Sunday = 1; Saturday = 7). If this check box is cleared, the locale-specific Oracle value is returned.  
  
         The SQLDescribeCol **always returns a value for precision** check box specifies whether or not the driver should return a non-zero value for the *cbColDef* argument of **SQLDescribeCol**. This connection string attribute applies only to columns where there is no Oracle-defined scale, such as computed numeric columns and columns defined as NUMBER without a precision or scale. A **SQLDescribeCol** call returns 130 for the precision when Oracle does not provide that information. If this check box is cleared, the driver will return 0 for these types of columns instead.  
  
2.  Click Add to add another data source, or click Close to exit.  
  
### To modify a data source for Windows  
  
1.  Open the ODBC Data Source Administrator. Click the appropriate DSN tab.  
  
2.  Select the Oracle data source you want to modify and then click Configure. The Microsoft ODBC for Oracle Setup dialog box appears.  
  
3.  Modify the applicable data source fields, and then click OK.  
  
 When you have finished modifying the information in this dialog box, the ODBC Administrator updates the registry information.
