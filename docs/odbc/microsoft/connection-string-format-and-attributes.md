---
description: "Connection String Format and Attributes"
title: "Connection String Format and Attributes | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "connection strings [ODBC], ODBC driver for Oracle"
  - "ODBC driver for Oracle [ODBC], connection strings"
ms.assetid: 0c360112-8720-4e54-a1a6-b9b18d943557
author: David-Engel
ms.author: v-davidengel
---
# Connection String Format and Attributes
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 Instead of using a dialog box, some applications might require a connection string that specifies data source connection information. The connection string is made up of a number of attributes that specify how a driver connects to a data source. An attribute identifies a specific piece of information that the driver needs to know before it can make the appropriate data source connection. Each driver might have a different set of attributes, but the connection string format is always the same. A connection string has the following format:  
  
```  
"DSN=data-source-name[;SERVER=value] [;PWD=value] [;UID=value] [;<Attribute>=<value>]"  
```  
  
> [!NOTE]  
>  The Microsoft ODBC Driver for Oracle supports the connection string format of the first version of the driver, which used `CONNECTSTRING`= instead of `SERVER=`.  
  
 If you are connecting to a data source provider that supports Windows authentication, you should specify `Trusted_Connection=yes` instead of user ID and password information in the connection string.  
  
 You must specify the data source name if you do not specify the UID, PWD, SERVER (or CONNECTSTRING), and DRIVER attributes. However, all other attributes are optional. If you do not specify an attribute, that attribute defaults to the one specified in the relevant DSN tab of the **ODBC Data Source Administrator** dialog box. The attribute value might be case-sensitive.  
  
 The attributes for the connection string are as follows:  
  
|Attribute|Description|Default value|  
|---------------|-----------------|-------------------|  
|DSN|The data source name listed in the Drivers tab of the **ODBC Data Source Administrator** dialog box.|""|  
|PWD|The password for the Oracle Server that you want to access. This driver supports limitations that Oracle places on passwords.|""|  
|SERVER|The connect string for the Oracle Server that you want to access.|""|  
|UID|The Oracle Server user name. Depending on your system, this attribute might not be optional - that is, certain databases and tables might require this attribute for security purposes.<br /><br /> Use "/" to use Oracle's operating system authentication.|""|  
|BUFFERSIZE|The optimal buffer size used when fetching columns.<br /><br /> The driver optimizes fetching so that one fetch from the Oracle Server returns enough rows to fill a buffer of this size. Larger values tend to increase performance if you fetch a lot of data.|65535|  
|SYNONYMCOLUMNS|When this value is true (1), an SQLColumn( ) API call returns column information. Otherwise, SQLColumn( ) returns only columns for tables and views. The ODBC Driver for Oracle provides faster access when this value is not set.|1|  
|REMARKS|When this value is true (1), the driver returns Remarks columns for the [SQLColumns](../../odbc/microsoft/level-1-api-functions-odbc-driver-for-oracle.md) result set. The ODBC Driver for Oracle provides faster access when this value is not set.|0|  
|StdDayOfWeek|Enforces the ODBC standard for the DAYOFWEEK scalar. By default this is turned on, but users who need the localized version can change the behavior to use whatever Oracle returns.|1|  
|GuessTheColDef|Specifies whether or not the driver should return a non-zero value for the *cbColDef* argument of **SQLDescribeCol**. Applies only to columns where there is no Oracle-defined scale, such as computed numeric columns and columns defined as NUMBER without a precision or scale. A **SQLDescribeCol** call returns 130 for the precision when Oracle does not provide that information.|0|  
  
 For example, a connection string that connects to the MyDataSource data source using the MyOracleServerOracle Server and the Oracle User MyUserID would be:  
  
```  
"DSN={MyDataSource};UID={MyUserID};PWD={MyPassword};SERVER={MyOracleServer}"  
```  
  
 A connection string that connects to the MyOtherDataSource data source using operating system authentication and the MyOtherOracleServerOracle Server would be:  
  
```  
"DSN=MyOtherDataSource;UID=/;PWD=;SERVER=MyOtherOracleServer"  
```
