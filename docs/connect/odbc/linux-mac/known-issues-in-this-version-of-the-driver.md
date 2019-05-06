---
title: "Known Issues in this Version of the Driver for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "02/15/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "known issues"
author: MightyPen
ms.author: genemi
manager: craigg
---
# Known Issues in this Version of the Driver

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article contains a list of known issues with the Microsoft ODBC Driver 13, 13.1, and 17 for SQL Server on Linux and macOS.

Additional issues will be posted on the [Microsoft ODBC driver team blog](https://blogs.msdn.com/b/sqlnativeclient/).  

- Windows, Linux, and macOS convert characters from the Private Use Area (PUA) or End User-Defined Characters (EUDC) differently. Conversions performed on the server within [!INCLUDE[tsql](../../../includes/tsql-md.md)] use the Windows conversion library. Conversions in the driver use the Windows, Linux, or macOS conversion libraries. Each library may produce different results when performing these conversions. For more information, see [End-User-Defined and Private Use Area Characters](/windows/desktop/Intl/end-user-defined-characters).

- If the client encoding is UTF-8, the driver manager does not always correctly convert from UTF-8 to UTF-16. Currently, data corruption occurs when one or more characters in the string are not valid UTF-8 characters. ASCII characters are mapped correctly. The driver manager attempts this conversion when calling the SQLCHAR versions of the ODBC API (for example, SQLDriverConnectA). The driver manager will not attempt this conversion when calling the SQLWCHAR versions of the ODBC API (for example, SQLDriverConnectW).  

- The *ColumnSize* parameter of **SQLBindParameter** refers to the number of characters in the SQL type, while *BufferLength* is the number of bytes in the application's buffer. However, if the SQL data type is `varchar(n)` or `char(n)`, the application binds the parameter as SQL_C_CHAR or SQL_C_VARCHAR, and the character encoding of the client is UTF-8, you may get a "String data, right truncation" error from the driver even if the value of *ColumnSize* is aligned with the size of the data type on the server. This error occurs since conversions between character encodings may change the length of the data. For example, a right apostrophe character (U+2019) is encoded in CP-1252 as the single byte 0x92, but in UTF-8 as the 3-byte sequence 0xe2 0x80 0x99.

For example, if your encoding is UTF-8 and you specify 1 for both *BufferLength* and *ColumnSize* in **SQLBindParameter** for an out-parameter, and then attempt to retrieve the preceding character stored in a `char(1)` column on the server (using CP-1252), the driver attempts to convert it to the 3-byte UTF-8 encoding, but cannot fit the result into a 1-byte buffer. In the other direction, it compares *ColumnSize* with the *BufferLength* in **SQLBindParameter** before doing the conversion between the different code pages on the client and server. Because a *ColumnSize* of 1 is less than a *BufferLength* of (for example) 3, the driver generates an error. To avoid this error, ensure that the length of the data after conversion fits into the specified buffer or column. Note that *ColumnSize* cannot be greater than 8000 for the `varchar(n)` type.

## See Also  
[Programming Guidelines](../../../connect/odbc/linux-mac/programming-guidelines.md)  
[Release Notes](../../../connect/odbc/linux-mac/release-notes-odbc-sql-server-linux-mac.md)  

