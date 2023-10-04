---
title: Data access tracing on Linux and macOS
description: Learn how to enable tracing on Linux and macOS with the Microsoft ODBC Driver for SQL Server. You can output a log file when you're troubleshooting application behavior.
author: David-Engel
ms.author: v-davidengel
ms.date: 09/01/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "data access tracing"
  - "tracing"
---
# Data access tracing on Linux and macOS

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The unixODBC Driver Manager on macOS and Linux supports tracing of ODBC API call entry and exit of the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

To trace your application's ODBC behavior, edit the `[ODBC]` section of the `odbcinst.ini` file. Set the values `Trace=Yes` and `TraceFile` to the path of the file that will contain the trace output. For example:

```ini
[ODBC]
Trace=Yes
TraceFile=/home/myappuser/odbctrace.log
```

You can also use `/dev/stdout` or any other device name to send trace output there, instead of to a persistent file. With the preceding settings, every time an application loads the unixODBC Driver Manager, it records all ODBC API calls made, into the output file.

After you finish tracing your application, remove `Trace=Yes` from the `odbcinst.ini` file to avoid the performance penalty of tracing, and ensure that any unnecessary trace files are removed.

Tracing applies to all applications that use the driver in `odbcinst.ini`. To not trace all applications (for example, to avoid disclosing sensitive per-user information), you can trace an individual application instance. Provide the instance the location of a private `odbcinst.ini`, by using the `ODBCSYSINI` environment variable. For example:

```bash
$ ODBCSYSINI=/home/myappuser myapp
```

In this case, you can add `Trace=Yes` to the `[ODBC Driver 17 for SQL Server]` section of `/home/myappuser/odbcinst.ini`.

## Determine which file the driver is using

The Linux and macOS ODBC drivers don't know which `odbc.ini` file is in use, or the path to the `odbc.ini` file. Information about which `odbc.ini` file is in use is available from the unixODBC tools `odbc_config` and `odbcinst`. You can also get this information from the unixODBC Driver Manager documentation.

For example, the following command prints the location of system and user `odbc.ini` files that contain, respectively, system and user data source names (DSNs):

```bash
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

*User DSNs* are only available to a specific user. User DSNs are stored in a file in the user's home directory, or a subdirectory. *System DSNs* are available for every user on the system, but can only be added, modified, and removed by a system administrator. If a user has a user DSN with the same name as a system DSN, the user DSN will be used upon connections by that user. For more information, see the [unixODBC documentation](http://www.unixodbc.org/doc/UserManual/).

## See also

- [Programming guidelines](programming-guidelines.md)
