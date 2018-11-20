---
title: "Data Access Tracing with the ODBC Driver on Linux and macOS | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data access tracing"
  - "tracing"
ms.assetid: 3149173a-588e-47a0-9f50-edb8e9adf5e8
author: MightyPen
ms.author: genemi
manager: craigg
---
# Data Access Tracing with the ODBC Driver on Linux and macOS
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The unixODBC Driver Manager on macOS and Linux supports tracing of ODBC API call entry and exit of the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

To trace your application's ODBC behavior, edit the `odbcinst.ini` file's `[ODBC]` section to set the values `Trace=Yes` and `TraceFile`
to the path of the file which is to contain the trace output; for example:

```  
Trace=Yes
TraceFile=/home/myappuser/odbctrace.log
```  

(You may also use `/dev/stdout` or any other device name to send trace output there instead of to a persistent file.) With the above settings, every time an application loads the unixODBC Driver Manager, it will record all the ODBC API calls which it performed into the output file.

After you finish tracing your application, remove `Trace=Yes` from the `odbcinst.ini` file to avoid the performance penalty of tracing, and ensure any unnecessary trace files are removed.
  
Tracing applies to all applications that use the driver in `odbcinst.ini`. To not trace all applications (for example, to avoid disclosing sensitive per-user information), you can trace an individual application instance by providing it the location of a private `odbcinst.ini`, using the `ODBCSYSINI` environment variable. For example:  
  
```  
$ ODBCSYSINI=/home/myappuser myapp
```  
  
In this case, you can add `Trace=Yes` to the `[ODBC Driver 13 for SQL Server]` section of `/home/myappuser/odbcinst.ini`.

## Determining which odbc.ini File the Driver is Using

The Linux and macOS ODBC drivers do not know which `odbc.ini` is in use, or the path to the `odbc.ini` file. However, information about which `odbc.ini` file is in use is available from the unixODBC tools `odbc_config` and `odbcinst`, and from the unixODBC Driver Manager documentation.  
  
For example, the following command prints (among other information) the location of system and user `odbc.ini` files which contain, respectively, system and user DSNs:

```
$ odbcinst -j
unixODBC 2.3.1
DRIVERS............: /etc/odbcinst.ini
SYSTEM DATA SOURCES: /etc/odbc.ini
FILE DATA SOURCES..: /etc/ODBCDataSources
USER DATA SOURCES..: /home/odbcuser/.odbc.ini`
SQLULEN Size.......: 8
SQLLEN Size........: 8
SQLSETPOSIROW Size.: 8
```

The [unixODBC documentation](https://www.unixodbc.org/doc/UserManual/) explains the differences between user and system DSNs. In summary:  

- User DSNs --- these are DSNs which are only available to a specific user. Users can connect using, add, modify, and remove their own user DSNs. User DSNs are stored in a file in the user's home directory, or a subdirectory thereof.
  
- System DSNs --- these DSNs are available for every user on the system to connect using them, but can only be added, modified, and removed by a system administrator. If a user has a user DSN with the same name as a system DSN, the user DSN will be used upon connections by that user.

## See Also
[Programming Guidelines](../../../connect/odbc/linux-mac/programming-guidelines.md)
