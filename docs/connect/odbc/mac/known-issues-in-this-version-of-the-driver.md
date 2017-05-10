---
title: "Known Issues in this Version of the Driver | Microsoft Docs"
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
  - "known issues"
caps.latest.revision: 30
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Known Issues in this Version of the Driver
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]
This topic contains a list of known issues with the Microsoft ODBC Driver 13 for SQL Server on macOS.  

Additional issues will be posted on the [Microsoft ODBC driver team blog](http://blogs.msdn.com/b/sqlnativeclient/).  

-   Windows and macOS can convert characters from the Private Use Area (PUA) or End User-Defined Characters (EUDC) differently. Conversions performed on the server within [!INCLUDE[tsql](../../../includes/tsql_md.md)] use the Windows conversion library. Conversions in the driver use the macOS conversion library. Each library might produce different results when performing the conversions. For more information, see [End-User-Defined and Private Use Area Characters](http://msdn.microsoft.com/library/dd317802.aspx).  

-   The driver manager does not always correctly convert from UTF-8 to UTF-16. Currently, data corruption will occur when 1 or more characters in the string are not valid UTF-8 characters. ASCII characters will be mapped correctly. The driver manager will attempt this conversion when calling the SQLCHAR versions of the ODBC API (for example, SQLDriverConnectA). The driver manager will not attempt this conversion when calling the SQLWCHAR versions of the ODBC API (for example, SQLDriverConnectW).  

-   The *ColumnSize* parameter of **SQLBindParameter** refers to the number of characters in the SQL type, while *BufferLength* is the number of bytes in the application's buffer. However, if the SQL data type is `varchar(n)` or `char(n)`, the application binds the parameter as SQL_C_CHAR or SQL_C_VARCHAR, and the character encoding of the client is UTF-8, you may get a "String data, right truncation" error from the driver even if the value of *ColumnSize* is aligned with the size of the data type on the server. This error occurs since conversions between character encodings may change the length of the data. For example, a right apostrophe character (U+2019) is encoded in CP-1252 as the single byte 0x92, but in UTF-8 as the 3-byte sequence 0xe2 0x80 0x99.

    For example, if you specify 1 for both *BufferLength* and *ColumnSize* in **SQLBindParameter** for an out-parameter, and then attempt to retrieve the above character stored in a `char(1)` column on the server (using CP-1252), the driver attempts to convert it to the 3-byte UTF-8 encoding, but cannot fit the result into a 1-byte buffer. In the other direction, it compares *ColumnSize* with the *BufferLength* in **SQLBindParameter** before doing the conversion between the different code pages on the client and server. Because a *ColumnSize* of 1 is less than a *BufferLength* of (for example) 3, the driver generates an error. To avoid this error, ensure that the length of the data after conversion fits into the specified buffer or column. Note that *ColumnSize* cannot be greater than 8000 for the `varchar(n)` type.  

## See Also  
[Microsoft ODBC Driver for SQL Server on macOS](../../../connect/odbc/mac/microsoft-odbc-driver-for-sql-server-on-mac.md)  
