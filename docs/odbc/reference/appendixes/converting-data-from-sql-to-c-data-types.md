---
title: "Converting Data from SQL to C Data Types"
description: Learn about converting data from SQL to C data types.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: 07/17/2025
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "data conversions from SQL to C types [ODBC]"
  - "data conversions from SQL to C types [ODBC], about converting"
  - "data types [ODBC], C data types"
  - "data types [ODBC], converting data"
  - "data types [ODBC], SQL data types"
  - "SQL data types [ODBC], converting to C types"
  - "converting data from SQL to c types [ODBC]"
  - "converting data from SQL to c types [ODBC], about converting"
  - "C data types [ODBC], converting from SQL types"
---
# Convert data from SQL to C data types

When an application calls `SQLFetch`, `SQLFetchScroll`, or `SQLGetData`, the driver retrieves the data from the data source. If necessary, it converts the data from the data type in which the driver retrieved it to the data type specified by the *TargetType* argument in `SQLBindCol` or `SQLGetData`. Finally, it stores the data in the location pointed to by the *TargetValuePtr* argument in `SQLBindCol` or `SQLGetData` (and the `SQL_DESC_DATA_PTR` field of the ARD).

The following table shows the supported conversions from ODBC SQL data types to ODBC C data types. A filled circle indicates the default conversion for a SQL data type (the C data type to which the data will be converted when the value of *TargetType* is `SQL_C_DEFAULT`). A hollow circle indicates a supported conversion.

For an ODBC *3.x* application working with an ODBC *2.x* driver, conversion from driver-specific data types might not be supported.

The format of the converted data isn't affected by the Windows country/region setting.

:::image type="content" source="media/converting-data-from-sql-to-c-data-types/conversion-table.png" alt-text="Diagram of supported conversions: ODBC C to SQL data types.":::

The tables in the following sections describe how the driver or data source converts data retrieved from the data source; drivers are required to support conversions to all ODBC C data types from the ODBC SQL data types that they support. For a given ODBC SQL data type, the first column of the table lists the legal input values of the *TargetType* argument in `SQLBindCol` and `SQLGetData`. The second column lists the outcomes of a test, often using the *BufferLength* argument specified in `SQLBindCol` or `SQLGetData`, which the driver performs to determine whether it can convert the data. For each outcome, the third and fourth columns list the values placed in the buffers specified by the *TargetValuePtr* and *StrLen_or_IndPtr* arguments specified in `SQLBindCol` or `SQLGetData` after the driver has attempted to convert the data. (The *StrLen_or_IndPtr* argument corresponds to the `SQL_DESC_OCTET_LENGTH_PTR` field of the ARD.) The last column lists the SQLSTATE returned for each outcome by `SQLFetch`, `SQLFetchScroll`, or `SQLGetData`.

If the *TargetType* argument in `SQLBindCol` or `SQLGetData` contains an identifier for an ODBC C data type not shown in the table for a given ODBC SQL data type, `SQLFetch`, `SQLFetchScroll`, or `SQLGetData` returns SQLSTATE 07006 (Restricted data type attribute violation). If the *TargetType* argument contains an identifier that specifies a conversion from a driver-specific SQL data type to an ODBC C data type and this conversion isn't supported by the driver, `SQLFetch`, `SQLFetchScroll`, or `SQLGetData` returns SQLSTATE HYC00 (Optional feature not implemented).

Although it's not shown in the tables, the driver returns `SQL_NULL_DATA` in the buffer specified by the *StrLen_or_IndPtr* argument when the SQL data value is `NULL`. For an explanation of the use of *StrLen_or_IndPtr* when multiple calls are made to retrieve data, see the [SQLGetData Function](../syntax/sqlgetdata-function.md) function description. When SQL data is converted to character C data, the character count returned in *StrLen_or_IndPtr* doesn't include the null-termination byte. If *TargetValuePtr* is a null pointer, `SQLGetData` returns SQLSTATE HY009 (Invalid use of null pointer); in `SQLBindCol`, this unbinds the column.

The following terms and conventions are used in the tables:

- **Byte length of data** is the number of bytes of C data available to return in *TargetValuePtr*, whether or not the data is truncated before it's returned to the application. For string data, this doesn't include the space for the null-termination character.

- **Character byte length** is the total number of bytes needed to display the data in character format. This is as defined for each C data type in the section [Display Size](display-size.md), except that character byte length is in bytes while the display size is in characters.

- Words in *italics* represent function arguments or elements of the SQL grammar. For the syntax of grammar elements, see [Appendix C: SQL Grammar](appendix-c-sql-grammar.md).

This section contains the following articles.

- [SQL to C: Character](sql-to-c-character.md)
- [SQL to C: Numeric](sql-to-c-numeric.md)
- [SQL to C: Bit](sql-to-c-bit.md)
- [SQL to C: Binary](sql-to-c-binary.md)
- [SQL to C: Date](sql-to-c-date.md)
- [SQL to C: GUID](sql-to-c-guid.md)
- [SQL to C: Time](sql-to-c-time.md)
- [SQL to C: Timestamp](sql-to-c-timestamp.md)
- [SQL to C: Year-Month Intervals](sql-to-c-year-month-intervals.md)
- [SQL to C: Day-Time Intervals](sql-to-c-day-time-intervals.md)
- [SQL to C Data Conversion Examples](sql-to-c-data-conversion-examples.md)
