---
title: "List of bugs fixed | Microsoft Docs"
ms.custom: ""
ms.date: "06/29/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "driver"
ms.assetid: f78b81ed-5214-43ec-a600-9bfe51c5745a
author: "v-makouz"
ms.author: v-jizho2
manager: "kenvh"
---
# List of bugs fixed

This page contains a listing of bugs fixed in each release, starting with [!INCLUDE[msCoName](../../includes/msconame_md.md)] ODBC Driver 17 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

### Bug fixes in the [!INCLUDE[msCoName](../../includes/msconame_md.md)] ODBC Driver 17.3 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

- Fixed TCP send notification event handle memory leak
- Fixed redefinition issue of enum _SQL_FILESTREAM_DESIRED_ACCESS in msodbcsql.h header file
- Fixed missing ACCESS_TOKEN and AUTHENTICATION related definition in msodbcsql.h header file for Linux

### Bug fixes in the [!INCLUDE[msCoName](../../includes/msconame_md.md)] ODBC Driver 17.2 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

- Fixed an error message about Azure Active Directory Authentication
- Fixed encoding detection when locale environment variables are set differently
- Fixed a crash upon disconnect with connection recovery in progress
- Fixed detection of connection liveness
- Fixed incorrect detection of closed sockets
- Fixed an infinite wait when attempting to release a statement handle during failed recovery
- Fixed incorrect uninstallation behavior when both version 13 and 17 are installed on Windows
- Fixed decryption behavior on older Windows platform (Windows 7, 8 and Server 2012)
- Fixed a cache issue when using ADAL Authentication on Windows
- Fixed an issue which was locking and overwriting trace logs on Windows

### Bug fixes in the [!INCLUDE[msCoName](../../includes/msconame_md.md)] ODBC Driver 17.1 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

- Fixed 1-second delay when calling SQLFreeHandle with MARS enabled and connection attribute "Encrypt=yes"
- Fixed an error 22003 crash in SQLGetData when the size of the buffer passed in is smaller then the data being retrieved (Windows)
- Fixed truncated ADAL error messages
- Fixed a rare bug on 32-bit Windows when converting a floating point number to an integer
- Fixed an issue where inserting double into decimal field with Always Encrypted on would return data truncation error
- Fixed a warning on MacOS installer
- Fixed sending incorrect state to SQL Server during Session Recovery attempt when Connection Resiliency and Connection Pooling both are enabled, causing session to be dropped by the Server

### Bug fixes in the [!INCLUDE[msCoName](../../includes/msconame_md.md)] ODBC Driver 17 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

- Fixed a bug where when using Kerberos authentication, bulk insert could fail with "access denied" error
- Removed workaround for a unixODBC bug present in version below 2.3.1 (driver doubled the sizes of certain buffers passed to unixODBC)
- Fixed Connection Resiliency (reconnect) hanging when using ColumnEncryption=enabled
- Fixed DSN creation bug, where when using "Active Directory Interactive authentication" option Azure Authentication window could become unresponsive (Windows)
- Fixed a rare crash during ODBC shutdown when asynchronous execution is enabled (happened when clearing connection handle)
- Fixed an issue where SQL Driver caused high CPU consumption while executing long stored procedures
- Fixed inability to retrieve data in an encrypted varbinary(max) column without conversion
- Fixed a problem where after a null varchar(max) encrypted column is fetched using SQLGetData() on a static cursor, the following column is also nulled even if it has data
- Fixed an issue with fetching varbinary(max) field with Always Encrypted on
- Fixed a problem of setlocale() not working with Always Encrypted
- Fixed an issue with SQLDescribeParam() returning error when called on XML-type stored procedure parameter with Always Encrypted on
- Fixed escaped underscores not working in SQLTables
- Fixed a bug where Hebrew data (varchar) is truncated when returned as wide chars on Linux
- Fixed an issue with querying Shift-JIS encoded char/varchar from UTF-8 application
- Fixed the bug where calling SQLGetInfo with SQL_DRIVER_NAME parameter returned Linux-style filename on MacOS
- Fixed an issue where loading Windows-1252 character data, using input files larger than 32k bytes into VARCHAR columns using the BCP utility would result in failures
