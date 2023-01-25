---
title: Features of the Microsoft ODBC Driver
description: Learn about the different features supported by the Microsoft ODBC Driver for SQL Server on Windows.
author: v-makouz
ms.author: v-davidengel
ms.date: 02/15/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Features of the Microsoft ODBC Driver for SQL Server on Windows

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

## Microsoft ODBC Driver 18.0 for SQL Server on Windows

<a id="longasmax"></a>
The ODBC Driver 18.0 allows users to send long data types as max data types using the keyword `LongAsMax` to toggle the setting on or off. When enabled, the SQL types SQL_LONGVARCHAR, SQL_LONGVARBINARY, and SQL_LONGWVARCHAR will be sent as varchar(max), varbinary(max), and nvarchar(max) instead of text, image, and ntext respectively.

The connection encryption defaults have changed. The default value of Encrypt is now Yes, and the new keywords Optional and Mandatory have been introduced as synonyms for No and Yes, respectively. For connecting using the TDS 8.0 protocol, the Strict mode (`Encrypt=Strict`) has been added. In this mode, the server certificate is always verified (`TrustServerCertificate` is ignored). A new keyword, `HostnameInCertificate`, can be used to specify the expected hostname found in the certificate if it differs from the specified server. `HostnameInCertificate` is usable in all encryption modes and is also applicable if the server-side `Force Encryption` option is enabled, which will cause the driver to verify the certificate in Optional or Mandatory modes unless disabled using `TrustServerCertificate`.

<a id="getdataextensions"></a>
The ODBC Driver 18 includes SQLGetData extensions. When enabled, SQLGetData can be used to retrieve column data in any order within a row, including going backwards. The feature is provided for compatibility purposes and will significantly degrade performance and increase memory usage. It is highly recommended that application always access the columns in ascending order. To enable SQLGetData extensions one can either use the `GetDataExtensions` connection string attribute, or use the `SQLSetConnectAttr` function to set `SQL_COPT_SS_GETDATA_EXTENSIONS` to `SQL_EN_ON`.

## Microsoft ODBC Driver 17.4 for SQL Server on Windows

The ODBC Driver 17.4 includes the ability to adjust TCP Keep-Alive settings. They can be modified by adding values to the Driver or DSN registry keys. The keys are located in `HKEY_LOCAL_MACHINE\Software\ODBC\` for system data sources, and in  `HKEY_CURRENT_USER\Software\ODBC\` for user data sources. For DSN, the values need to be added to `...\Software\ODBC\ODBC.INI\<DSN Name>` and for the Driver to `...\Software\ODBC\ODBCINST.INI\ODBC Driver 17 for SQL Server`.

For more information, see [Registry Entries for ODBC Components](../../../odbc/reference/install/registry-entries-for-odbc-components.md).

The values are `REG_SZ` and are as follows:

- `KeepAlive` controls how often TCP attempts to verify that an idle connection is still intact by sending a keep-alive packet. The default is 30 seconds.

- `KeepAliveInterval` determines the interval separating keep-alive retransmissions until a response is received. The default is 1 second.

## Microsoft ODBC Driver 13.1 for SQL Server on Windows

The ODBC Driver 13.1 for SQL Server contains all the functionality of the previous version (11). It also adds support for Always Encrypted and Azure Active Directory authentication.

Always Encrypted allows clients to encrypt sensitive data inside client applications and never reveal the encryption keys to SQL Server. An Always Encrypted enabled driver installed on the client computer achieves this security by automatically encrypting and decrypting sensitive data in the SQL Server client application. The driver encrypts the data in sensitive columns before passing the data to SQL Server. It also automatically rewrites queries so that the semantics to the application are preserved. Similarly, the driver transparently decrypts data stored in encrypted database columns that are contained in query results. For more information, see [Using Always Encrypted with the ODBC Driver](../using-always-encrypted-with-the-odbc-driver.md).

Azure Active Directory allows users, DBAs, and application programmers to use Azure Active Directory authentication. For more information, see [Using Azure Active Directory with the ODBC Driver](../using-azure-active-directory.md), and [Connecting to SQL Database or Azure Synapse Analytics By Using Azure Active Directory Authentication](/azure/sql-database/sql-database-aad-authentication).

## Microsoft ODBC Driver 11 for SQL Server on Windows

The ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] contains all the functionality of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver that shipped in [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]. For more information about SQL Server Native Client, see [SQL Server Native Client Programming](../../../relational-databases/native-client/sql-server-native-client-programming.md). The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver is based on the ODBC driver that ships in the Windows operating system. For more information about that driver, see [Windows Data Access Components SDK](/previous-versions/windows/desktop/legacy/aa968814(v=vs.85)).

This release of the ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] contains the following new features:

### bcp.exe -l option for specifying a login timeout

The -l option specifies the number of seconds before a `bcp.exe` login to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] times out when you try to connect to a server. The default login timeout is 15 seconds. The login timeout must be a number between 0 and 65534. If the value supplied isn't numeric or doesn't fall into that range, `bcp.exe` generates an error message. A value of 0 specifies an infinite timeout. A login timeout of less than (approximately) 10 seconds isn't reliable.

### Driver-aware connection pooling

The ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports [Driver-Aware Connection Pooling](../../../odbc/reference/develop-app/driver-aware-connection-pooling.md). For more information, see [Driver-Aware Connection Pooling in the ODBC Driver for SQL Server](driver-aware-connection-pooling-in-the-odbc-driver-for-sql-server.md).

### Asynchronous execution (notification method)

The ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports [Asynchronous Execution (Notification Method)](../../../odbc/reference/develop-app/asynchronous-execution-notification-method.md). For a usage sample, see [Asynchronous Execution &#40;Notification Method&#41; Sample](asynchronous-execution-notification-method-sample.md).

### Connection resiliency

To ensure that applications remain connected to a Microsoft Azure SQL Database, the ODBC driver on Windows can restore idle connections. For more information, see [Connection Resiliency in the Windows ODBC Driver](../connection-resiliency.md).

## Behavior changes

In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, the `-y0` option for `sqlcmd.exe` caused output to be truncated at 1 MB if the display width was 0.

Beginning in the ODBC Driver 11 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], there's no limit on the amount of data that can be retrieved in a single column when `-y0` is specified. `sqlcmd.exe` now streams columns as large as 2 GB ([!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type maximum).

Another difference is that specifying both `-h` and `-y0` now produces an error reporting that the options are incompatible. `-h`, which specifies the number of rows to print between the column headings and has never been compatible with `-y0`, was ignored although no headers were printed.

`-y0` can cause performance issues on both the server and the network, depending on the size of the data returned.

## See also

[Microsoft ODBC Driver for SQL Server on Windows](microsoft-odbc-driver-for-sql-server-on-windows.md)
