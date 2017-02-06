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

-   The driver manager does not always correctly convert from UTF-8 to UTF-16. Currently, data corruption will occur when 1 or more characters in the string is not a valid UTF-8 character. ASCII characters will be mapped correctly. The driver manager will attempt this conversion when calling the SQLCHAR versions of the ODBC API (for example, SQLDriverConnectA). The driver manager will not attempt this conversion when calling the SQLWCHAR versions of the ODBC API (for example, SQLDriverConnectW).  

-   The *ColumnSize* parameter of **SQLBindParameter** refers to the number of characters. However, if the SQL data type is varchar(n) or char(n) and the application binds the parameter as SQL_C_CHAR or SQL_C_VARCHAR and if the client uses code page UTF-8, you may get a "String data, right truncation" error from the driver even if the value of *ColumnSize* is aligned with the size of the data type on server. This error can occur when one character in some code pages, such as CP-1252, could be a different number of bytes in UTF-8. For example, a right apostrophe character encoded in CP-1252 as 0x92 is encoded as the sequence 0xe2 0x80 0x99 in UTF-8, which has 3 bytes.  

    So, if you specify 1 for *ColumnSize* in **SQLBindParameter** for such a character, the driver compares *ColumnSize* with the *BufferLength* in **SQLBindParameter** before doing the conversion between the different code pages on the client and server. Because a *ColumnSize* of 1 is less than a *BufferLength* of (for example) 3, the driver generates an error. To avoid this error, use the value of *BufferLength* (and not converted length) in *ColumnSize*. *ColumnSize* cannot be greater than 8000 for varchar(n) type.  

## See Also  
[Microsoft ODBC Driver for SQL Server on macOS](../../../connect/odbc/mac/microsoft-odbc-driver-for-sql-server-on-mac.md)  
