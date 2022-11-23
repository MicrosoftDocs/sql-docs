---
title: International features
description: Learn about the internationalization features of the JDBC driver and how you can create a localized application experience.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/12/2019
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# International features of the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The internationalization features of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] include the following items:

- Support for a fully localized experience in the same languages as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]
- Support for the Java language conversions for locale sensitive [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data
- Support for international languages, regardless of operating system
- Support for international domain names (starting with Microsoft JDBC Driver 6.0 for SQL Server)

## Handling of character data

Character data in Java is handled as Unicode by default; the Java **String** object represents Unicode character data. In the JDBC driver, the only exception to this rule is the ASCII stream getter and setter methods, which are special cases because they use byte streams with the implicit assumption of single well-known code pages (ASCII).

Also, the JDBC driver provides the **sendStringParametersAsUnicode** connection string property. This property can be used to specify that prepared parameters for character data are sent as ASCII or Multi-byte Character Set (MBCS) instead of Unicode. For more information about the **sendStringParametersAsUnicode** connection string property, see [Setting the Connection Properties](setting-the-connection-properties.md).

### Driver incoming conversions

Unicode text data coming from the server doesn't have to be converted. It's passed directly as Unicode. Non-Unicode data coming from the server is converted from the code page for the data, at the database or column level, to Unicode. The JDBC driver uses the Java Virtual Machine (JVM) conversion routines to do these conversions. These conversions are done on all typed String and Character stream getter methods.

If the JVM doesn't have the proper code page support for the data from the database, the JDBC driver throws an "XXX codepage not supported by the Java environment" exception. To solve this problem, you should install the full international character support required for that JVM.

### Driver outgoing conversions

Character data going from the driver to the server can be ASCII or Unicode. For example, the new JDBC 4.0 national character methods, such as setNString, setNCharacterStream, and setNClob methods of [SQLServerPreparedStatement](reference/sqlserverpreparedstatement-class.md) and [SQLServerCallableStatement](reference/sqlservercallablestatement-class.md) classes, always send their parameter values to the server in Unicode.

On the other hand, the non-national character API methods, such as setString, setCharacterStream, and setClob methods of [SQLServerPreparedStatement](reference/sqlserverpreparedstatement-class.md) and [SQLServerCallableStatement](reference/sqlservercallablestatement-class.md) classes send their values to the server in Unicode only when the **sendStringParametersAsUnicode** property is set to "true", which is the default value.

## Non-unicode parameters

For optimal performance with **CHAR**, **VARCHAR** or **LONGVARCHAR** type of non-Unicode parameters, set the **sendStringParametersAsUnicode** connection string property to "false" and use the non-national character methods.

## Formatting issues

For date, time, and currencies, all formatting with localized data is done at the Java language level using the Locale object; and the various formatting methods for **Date**, **Calendar**, and **Number** data types. In the rare case where the JDBC driver must pass along locale sensitive data in a localized format, the proper formatter is used with the default JVM locale.

## Collation support

The JDBC Driver 3.0 supports all the collations supported by [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], and the new collations or new versions of Windows collation names introduced in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].

For more information on the collations, see [Collation and Unicode Support](/previous-versions/sql/sql-server-2008-r2/ms143503(v=sql.105)) and [Windows Collation Name (Transact-SQL)](../../t-sql/statements/windows-collation-name-transact-sql.md).

## Using International domain names (IDN)

The JDBC Driver 6.0 for SQL Server supports the use of Internationalized Domain Names (IDNs) and can convert a Unicode serverName to ASCII compatible encoding (Punycode) when required during a connection.  If the IDNs are stored in the Domain Name System (DNS) as ASCII strings in the Punycode format (specified by RFC 3490), enable the conversion of the Unicode server name by setting the serverNameAsACE property to true.  Otherwise, if the DNS service is configured to allow the use of Unicode characters, set the serverNameAsACE property as false (the default).  For older versions of the JDBC driver, it's also possible to convert the serverName to Punycode using [Java's IDN.toASCII](https://docs.oracle.com/javase/8/docs/api/java/net/IDN.html) methods before setting that property for a connection.

> [!NOTE]
> Most resolver software written for non-Windows platforms is based on the Internet DSN standards and is therefore most likely to use the Punycode format for IDNs, while a Windows-based DNS Server on a private network can be configured to allow the use of UTF-8 characters on a per-server basis.  For more information, see [Unicode character support](/previous-versions/windows/it-pro/windows-server-2003/cc738403(v=ws.10)).

## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)
