---
title: "Programming Guidelines (ODBC Driver for SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "01/11/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
author: MightyPen
ms.author: genemi
manager: craigg
---
# Programming Guidelines

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The programming features of the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on macOS and Linux are based on ODBC in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ([SQL Server Native Client (ODBC)](https://go.microsoft.com/fwlink/?LinkID=134151)). [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client is based on ODBC in Windows Data Access Components ([ODBC Programmer's Reference](https://go.microsoft.com/fwlink/?LinkID=45250)).  

An ODBC application can use Multiple Active Result Sets (MARS) and other [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] specific features by including `/usr/local/include/msodbcsql.h` after including the unixODBC headers (`sql.h`, `sqlext.h`, `sqltypes.h`, and `sqlucode.h`). Then use the same symbolic names for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-specific items that you would use in your Windows ODBC applications.

## Available Features  
The following sections from the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client documentation for ODBC ([SQL Server Native Client (ODBC)](https://go.microsoft.com/fwlink/?LinkID=134151)) are valid when using the ODBC driver on macOS and Linux:  

-   [Communicating with SQL Server (ODBC)](https://msdn.microsoft.com/library/ms131692.aspx)  
-   [Connection and query timeout support](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md)  
-   [Cursors](../../../relational-databases/native-client-odbc-cursors/using-cursors-odbc.md)  
-   [Date/Time Improvements (ODBC)](https://msdn.microsoft.com/library/bb677319.aspx)  
-   [Executing Queries (ODBC)](https://msdn.microsoft.com/library/ms131677.aspx)  
-   [Handling Errors and Messages](../../../relational-databases/native-client-odbc-error-messages/handling-errors-and-messages.md)  
-   [Kerberos authentication](../../../relational-databases/native-client/features/service-principal-name-spn-support-in-client-connections.md)  
-   [Large CLR User-Defined Types (ODBC)](https://msdn.microsoft.com/library/bb677316.aspx)  
-   [Performing Transactions (ODBC) (except distributed transactions)](https://msdn.microsoft.com/library/ms131706.aspx)  
-   [Processing Results (ODBC)](https://msdn.microsoft.com/library/ms130812.aspx)  
-   [Running Stored Procedures](../../../relational-databases/native-client-odbc-stored-procedures/running-stored-procedures.md)
-   [Sparse Columns Support (ODBC)](https://msdn.microsoft.com/library/cc280357.aspx)
-   [SSL encryption](../../../relational-databases/native-client/features/using-encryption-without-validation.md)
-   [Table Valued Parameters](https://docs.microsoft.com/sql/relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc)
-   [UTF-8 and UTF-16 for command and data API](https://msdn.microsoft.com/library/ff878241.aspx)
-   [Using Catalog Functions](../../../relational-databases/native-client/odbc/using-catalog-functions.md)  

## Unsupported Features

The following features have not been verified to work correctly in this release of the ODBC driver on macOS and Linux:

-   Failover Cluster Connection
-   [Transparent Network IP Resolution](https://docs.microsoft.com/sql/connect/odbc/linux/using-transparent-network-ip-resolution) (before ODBC Driver 17)
-   [Advanced Driver Tracing](https://blogs.msdn.microsoft.com/mattn/2012/05/15/enabling-advanced-driver-tracing-for-the-sql-native-client-odbc-drivers/)

The following features are not available in this release of the ODBC driver on macOS and Linux: 

-   Distributed Transactions (SQL_ATTR_ENLIST_IN_DTC attribute is not supported)  
-   Database Mirroring  
-   FILESTREAM  
-   Profiling ODBC driver performance, discussed in [SQLSetConnectAttr](https://go.microsoft.com/fwlink/?LinkId=234099), and the following performance-related connection attributes:  
    -   SQL_COPT_SS_PERF_DATA  
    -   SQL_COPT_SS_PERF_DATA_LOG  
    -   SQL_COPT_SS_PERF_DATA_LOG_NOW  
    -   SQL_COPT_SS_PERF_QUERY  
    -   SQL_COPT_SS_PERF_QUERY_INTERVAL  
    -   SQL_COPT_SS_PERF_QUERY_LOG  
-   SQLBrowseConnect  
-   C interval types such as SQL_C_INTERVAL_YEAR_TO_MONTH (documented in [Data Type Identifiers and Descriptors](https://msdn.microsoft.com/library/ms716351(VS.85).aspx))
-   The SQL_CUR_USE_ODBC value of the SQL_ATTR_ODBC_CURSORS attribute of the SQLSetConnectAttr function.

## Character Set Support

For ODBC Driver 13 and 13.1, SQLCHAR data must be UTF-8. No other encodings are supported.

For ODBC Driver 17, SQLCHAR data in one of the following character sets/encodings is supported:

|Name|Description|
|-|-|
|UTF-8|Unicode|
|CP437|MS-DOS Latin US|
|CP850|MS-DOS Latin 1|
|CP874|Latin/Thai|
|CP932|Japanese, Shift-JIS|
|CP936|Simplified Chinese, GBK|
|CP949|Korean, EUC-KR|
|CP950|Traditional Chinese, Big5|
|CP1251|Cyrillic|
|CP1253|Greek|
|CP1256|Arabic|
|CP1257|Baltic|
|CP1258|Vietnamese|
|ISO-8859-1 / CP1252|Latin-1|
|ISO-8859-2 / CP1250|Latin-2|
|ISO-8859-3|Latin-3|
|ISO-8859-4|Latin-4|
|ISO-8859-5|Latin/Cyrillic|
|ISO-8859-6|Latin/Arabic|
|ISO-8859-7|Latin/Greek|
|ISO-8859-8 / CP1255|Hebrew|
|ISO-8859-9 / CP1254|Turkish|
|ISO-8859-13|Latin-7|
|ISO-8859-15|Latin-9|

Upon connection, the driver detects the current locale of the process it is loaded in. If it uses one of the encodings above, the driver uses that encoding for SQLCHAR (narrow-character) data; otherwise, it defaults to UTF-8. Since all processes start in the "C" locale by default (and thus cause the driver to default to UTF-8), if an application needs to use one of the encodings above, it should use the **setlocale** function to set the locale appropriately before connecting; either by specifying the desired locale explicitly, or using an empty string for example `setlocale(LC_ALL, "")` to use the locale settings of the environment.

Thus, in a typical Linux or Mac environment where the encoding is UTF-8, users of ODBC Driver 17 upgrading from 13 or 13.1 will not observe any differences. However, applications that use a non-UTF-8 encoding in the above list via `setlocale()` need to use that encoding for data to/from the driver instead of UTF-8.

SQLWCHAR data must be UTF-16LE (Little Endian).

When binding input parameters with SQLBindParameter, if a narrow character SQL type such as SQL_VARCHAR is specified, the driver converts the supplied data from the client encoding to the default (typically codepage 1252) [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encoding. For output parameters, the driver converts from the encoding specified in the collation information associated with the data to the client encoding. However, data loss is possible --- characters in the source encoding not representable in the target encoding will convert to a question mark ('?').

To avoid this data loss when binding input parameters, specify a Unicode SQL character type such as SQL_NVARCHAR. In this case, the driver converts from the client encoding to UTF-16, which can represent all Unicode characters. Furthermore, the target column or parameter on the server must also be either a Unicode type (**nchar**, **nvarchar**, **ntext**) or one with a collation/encoding, which can represent all the characters of the original source data. For avoiding data loss with output parameters, specify a Unicode SQL type, and either a Unicode C type (SQL_C_WCHAR), causing the driver to return data as UTF-16; or a narrow C type, and ensure that the client encoding can represent all the characters of the source data (this is always possible with UTF-8.)

For more information about collations and encodings, see [Collation and Unicode Support](../../../relational-databases/collations/collation-and-unicode-support.md).

There are some encoding conversion differences between Windows and several versions of the iconv library on Linux and macOS. Text data in codepage 1255 (Hebrew) has one code point (0xCA) that behaves differently upon conversion to Unicode. On Windows, this character converts to the UTF-16 code point of 0x05BA. On macOS and Linux with libiconv versions earlier than 1.15, it converts to 0x00CA. On Linux with iconv libraries that do not support the 2003 revision of Big5/CP950 (named `BIG5-2003`), characters added with that revision will not convert correctly. In codepage 932 (Japanese, Shift-JIS), the result of decoding of characters not originally defined in the encoding standard also differs. For example, the byte 0x80 converts to U+0080 on Windows but may become U+30FB on Linux and macOS, depending on iconv version.

In ODBC Driver 13 and 13.1, when UTF-8 multibyte characters or UTF-16 surrogates are split across SQLPutData buffers, it results in data corruption. Use buffers for streaming SQLPutData that do not end in partial character encodings. This limitation has been removed with ODBC Driver 17.

## Additional Notes  

1.  You can make a dedicated administrator connection (DAC) using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication and **host,port**. A member of the Sysadmin role first needs to discover the DAC port. See [Diagnostic Connection for Database Administrators](https://docs.microsoft.com/sql/database-engine/configure-windows/diagnostic-connection-for-database-administrators#dac-port) to discover how. For example, if the DAC port were 33000, you could connect to it with `sqlcmd` as follows:  

    ```
    sqlcmd -U <user> -P <pwd> -S <host>,33000
    ```

    > [!NOTE]  
    > DAC connections must use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication.  
    
2.  The UnixODBC driver manager returns "invalid attribute/option identifier" for all statement attributes when they are passed through SQLSetConnectAttr. On Windows, when SQLSetConnectAttr receives a statement attribute value, it causes the driver to set that value on all active statements which are children of the connection handle.  

## See Also  
[Frequently Asked Questions](../../../connect/odbc/linux-mac/frequently-asked-questions-faq-for-odbc-linux.md)

[Known Issues in this Version of the Driver](../../../connect/odbc/linux-mac/known-issues-in-this-version-of-the-driver.md)

[Release Notes](../../../connect/odbc/linux-mac/release-notes.md)
