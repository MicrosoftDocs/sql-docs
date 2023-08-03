---
title: Known issues for the ODBC driver on Linux and macOS
description: "Learn about known issues with the Microsoft ODBC Driver for SQL Server on Linux and macOS and steps for troubleshooting connectivity issues."
author: David-Engel
ms.author: v-davidengel
ms.date: 02/17/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "known issues"
---

# Known issues for the ODBC driver on Linux and macOS

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article contains a list of known issues with the Microsoft ODBC Driver 13, 13.1, 17, and 18 for SQL Server on Linux and macOS. It also contains steps for troubleshooting connectivity issues.

## Known issues

Additional issues will be posted on the [SQL Server Drivers blog](https://techcommunity.microsoft.com/t5/sql-server-blog/bg-p/SQLServer/label-name/SQLServerDrivers).  

- Due to system library limitations, Alpine Linux supports fewer character encodings and locales. For example, `en_US.UTF-8` isn't available. For more information, see [`musl libc` - functional differences from `glibc`](https://wiki.musl-libc.org/functional-differences-from-glibc.html).

- Windows, Linux, and macOS convert characters from the Private Use Area (PUA) or End User-Defined Characters (EUDC) differently. Conversions performed on the server within [!INCLUDE[tsql](../../../includes/tsql-md.md)] use the Windows conversion library. Conversions in the driver use the Windows, Linux, or macOS conversion libraries. Each library may produce different results when performing these conversions. For more information, see [End-User-Defined and Private Use Area Characters](/windows/desktop/Intl/end-user-defined-characters).

- If the client encoding is UTF-8, the driver manager doesn't always correctly convert from UTF-8 to UTF-16. Currently, data corruption occurs when one or more characters in the string aren't valid UTF-8 characters. ASCII characters are mapped correctly. The driver manager attempts this conversion when calling the SQLCHAR versions of the ODBC API (for example, SQLDriverConnectA). The driver manager won't attempt this conversion when calling the SQLWCHAR versions of the ODBC API (for example, SQLDriverConnectW).

- The *ColumnSize* parameter of **SQLBindParameter** refers to the number of characters in the SQL type, while *BufferLength* is the number of bytes in the application's buffer. However, if the SQL data type is `varchar(n)` or `char(n)`, the application binds the parameter as SQL_C_CHAR for the C type, and SQL_CHAR or SQL_VARCHAR for the SQL type, and the character encoding of the client is UTF-8, you may get a "String data, right truncation" error from the driver even if the value of *ColumnSize* is aligned with the size of the data type on the server. This error occurs since conversions between character encodings may change the length of the data. For example, a right apostrophe character (U+2019) is encoded in CP-1252 as the single-byte 0x92, but in UTF-8 as the 3-byte sequence 0xe2 0x80 0x99.

For example, if your encoding is UTF-8 and you specify 1 for both *BufferLength* and *ColumnSize* in **SQLBindParameter** for an out-parameter, and then attempt to retrieve the preceding character stored in a `char(1)` column on the server (using CP-1252), the driver attempts to convert it to the 3-byte UTF-8 encoding, but can't fit the result into a 1-byte buffer. In the other direction, it compares *ColumnSize* with the *BufferLength* in **SQLBindParameter** before doing the conversion between the different code pages on the client and server. Because a *ColumnSize* of 1 is less than a *BufferLength* of (for example) 3, the driver generates an error. To avoid this error, ensure that the length of the data after conversion fits into the specified buffer or column. Note that *ColumnSize* can't be greater than 8000 for the `varchar(n)` type.

## <a id="connectivity"></a> Troubleshooting connection problems

If you're unable to make a connection to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] using the ODBC driver, use the following information to identify the problem.

The most common connection problem is to have two copies of the UnixODBC driver manager installed. Search /usr for libodbc\*.so\*. If you see more than one version of the file, you (possibly) have more than one driver manager installed. Your application might use the wrong version.

Enable the connection log by editing your `/etc/odbcinst.ini` file to contain the following section with these items:

```ini
[ODBC]
Trace = Yes
TraceFile = (path to log file, or /dev/stdout to output directly to the terminal)
```

If you get another connection failure and don't see a log file, there (possibly) are two copies of the driver manager on your computer. Otherwise, the log output should be similar to:

```log
[ODBC][28783][1321576347.077780][SQLDriverConnectW.c][290]
        Entry:
            Connection = 0x17c858e0
            Window Hdl = (nil)
            Str In = [DRIVER={ODBC Driver 18 for SQL Server};SERVER={contoso.com};Trusted_Connection={YES};WSID={mydb.contoso.com};AP...][length = 139 (SQL_NTS)]
            Str Out = (nil)
            Str Out Max = 0
            Str Out Ptr = (nil)
            Completion = 0
        UNICODE Using encoding ASCII 'UTF8' and UNICODE 'UTF16LE'
```

If the ASCII character encoding isn't UTF-8, for example:

```log
UNICODE Using encoding ASCII 'ISO8859-1' and UNICODE 'UCS-2LE'
```

There's more than one driver manager installed and your application is using the wrong one, or the driver manager wasn't built correctly.

Some macOS users encounter the following error with driver version 17.8 or older:\
(This error has been resolved in driver version 17.9+)

```text
[08001][Microsoft][ODBC Driver 17 for SQL Server]SSL Provider: [OpenSSL library could not be loaded, make sure OpenSSL 1.0 or 1.1 is installed]
[08001][Microsoft][ODBC Driver 17 for SQL Server]Client unable to establish connection (0) (SQLDriverConnect)
```

The error can happen when OpenSSL 3.0 is installed. OpenSSL typically is installed through Brew, and it contains the openssl, openssl@1.1, and openssl@3 binaries.

To resolve this error, change the symlink of the openssl binary to openssl@1.1:

```shell
rm -rf $(brew --prefix)/opt/openssl
version=$(ls $(brew --prefix)/Cellar/openssl@1.1 | grep "1.1")
ln -s $(brew --prefix)/Cellar/openssl@1.1/$version $(brew --prefix)/opt/openssl
```

For more information about resolving connection failures, see:

- [Steps to troubleshoot SQL connectivity issues](/archive/blogs/sql_protocols/steps-to-troubleshoot-sql-connectivity-issues)
- [SQL Server 2005 Connectivity Issue Troubleshoot - Part I](https://techcommunity.microsoft.com/t5/sql-server/sql-server-2005-connectivity-issue-troubleshoot-part-i/ba-p/383034)
- [Connectivity troubleshooting in SQL Server 2008 with the Connectivity Ring Buffer](https://techcommunity.microsoft.com/t5/sql-server/connectivity-troubleshooting-in-sql-server-2008-with-the/ba-p/383393)
- [SQL Server Authentication Troubleshooter](/archive/blogs/sqlsecurity/sql-server-authentication-troubleshooter)

## Next steps

For ODBC driver installation instructions, see the following articles:

- [Installing the Microsoft ODBC Driver for SQL Server on Linux](installing-the-microsoft-odbc-driver-for-sql-server.md)
- [Installing the Microsoft ODBC Driver for SQL Server on macOS](install-microsoft-odbc-driver-sql-server-macos.md)

For more information, see the [Programming guidelines](programming-guidelines.md) and the [Release notes](release-notes-odbc-sql-server-linux-mac.md).
