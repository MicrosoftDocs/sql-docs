---
title: "Installing the Microsoft ODBC Driver for SQL Server on macOS | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology:
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords:
  - "driver, installing"
caps.latest.revision: 69
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Installing the Microsoft ODBC Driver for SQL Server on macOS
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This topic explains how to install the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 13.1 and 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on macOS.

## Installing the Microsoft ODBC Driver 13.1 for SQL Server on macOS  

### OS X 10.11 (El Capitan) and 10.12 (Sierra)
1. Install homebrew:
    * `/usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"`
3. Add msodbcsql tap:
    * `brew tap microsoft/msodbcsql https://github.com/Microsoft/homebrew-msodbcsql`
4. Update homebrew:
    * `brew update`
5. Install msodbcsql package:
    * `brew install msodbcsql`

## Troubleshooting Connection Problems  
If you are unable to make a connection to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] using the ODBC driver on macOS, use the following information to identify the problem.  

The most common connection problem is to have two copies of the UnixODBC Driver Manager installed. Search `/usr/` for `libodbc*.so*`. If you see more than one version of the file, you (possibly) have more than one driver manager installed. Your application might use the wrong version.

Enable the connection log by editing your `/etc/odbcinst.ini` file to contains the following section with these items:

```
[ODBC]
Trace = Yes
TraceFile = (path to log file, or /dev/stdout to output directly to the terminal)
```  

If you get another connection failure and do not see any log output, there (possibly) are two copies of the driver manager on your computer. Otherwise, the log output should be similar to the following:

```  
[ODBC][28783][1321576347.077780][SQLDriverConnectW.c][290]  
        Entry:  
            Connection = 0x17c858e0  
            Window Hdl = (nil)  
            Str In = [DRIVER={ODBC Driver 13 for SQL Server};SERVER={contoso.com};Trusted_Connection={YES};WSID={mydb.contoso.com};AP...][length = 139 (SQL_NTS)]  
            Str Out = (nil)  
            Str Out Max = 0  
            Str Out Ptr = (nil)  
            Completion = 0  
        UNICODE Using encoding ASCII 'UTF8' and UNICODE 'UTF-16LE'  
```  

If the character encoding is not UTF-8, for example:  

```
UNICODE Using encoding ASCII 'ISO8859-1' and UNICODE 'UCS-2LE'  
```

There is more than one Driver Manager installed and your application is using the wrong one, or the Driver Manager was not built correctly.

For more information about resolving connection failures, see:  

-   [Steps to troubleshoot SQL connectivity issues](http://blogs.msdn.com/b/sql_protocols/archive/2008/04/30/steps-to-troubleshoot-connectivity-issues.aspx)  

-   [SQL Server 2005 Connectivity Issue Troubleshoot - Part I](http://blogs.msdn.com/b/sql_protocols/archive/2005/10/22/sql-server-2005-connectivity-issue-troubleshoot-part-i.aspx)  

-   [Connectivity troubleshooting in SQL Server 2008 with the Connectivity Ring Buffer](http://blogs.msdn.com/b/sql_protocols/archive/2008/05/20/connectivity-troubleshooting-in-sql-server-2008-with-the-connectivity-ring-buffer.aspx)  

-   [SQL Server Authentication Troubleshooter](http://blogs.msdn.com/b/sqlsecurity/archive/2010/03/29/sql-server-authentication-troubleshooter.aspx)  

-   [Error details (http://www.microsoft.com/products/ee/transform.aspx?ProdName=Microsoft+SQL+Server&EvtSrc=MSSQLServer&EvtID=11001)](http://www.microsoft.com/products/ee/transform.aspx?ProdName=Microsoft+SQL+Server&EvtSrc=MSSQLServer&EvtID=001)  

    The error number specified in the URL (11001) should be changed to match the error that you see.  

## See Also  
[Microsoft ODBC Driver for SQL Server on macOS](../../../connect/odbc/mac/microsoft-odbc-driver-for-sql-server-on-mac.md)  
