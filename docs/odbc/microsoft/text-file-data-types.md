---
title: "Text File Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "text file driver [ODBC], data types"
  - "ODBC desktop database drivers [ODBC], text file driver"
  - "desktop database drivers [ODBC], text file driver"
  - "text file data types [ODBC]"
  - "Jet-based ODBC drivers [ODBC], text file driver"
ms.assetid: e113112e-ae42-469e-8e4b-a365a10d9071
author: MightyPen
ms.author: genemi
manager: craigg
---
# Text File Data Types
The following table shows how text data types are mapped to ODBC SQL data types. Note that not all ODBC SQL data types are supported by the ODBC Text driver.  
  
|Text data type|ODBC data type|  
|--------------------|--------------------|  
|CHAR|SQL_VARCHAR|  
|DATETIME|SQL_TIMESTAMP|  
|FLOAT|SQL_DOUBLE|  
|INTEGER|SQL_INTEGER|  
|LONGCHAR|SQL_LONGVARCHAR|  
  
> [!NOTE]  
>  **SQLGetTypeInfo** returns ODBC data types. All conversions in Appendix D of the *ODBC Programmer's Reference* are supported for the SQL data types listed in the previous table.  
  
 The following table shows limitations on Text data types.  
  
|Data type|Description|  
|---------------|-----------------|  
|CHAR|Creating a CHAR column of zero or unspecified length actually returns a 255-bit column.<br /><br /> In delimited files, a CHAR column may or may not have double quotation mark delimiters at the beginning and the end; in fixed-length files, double quotation marks are not used as delimiters.|  
|DATETIME|MM-DD-YY (for example, 01-17-92)<br /><br /> MMM-DD-YY (for example, Jan-17-92)<br /><br /> DD-MMM-YY (for example, 17-Jan-92)<br /><br /> YYYY-MM-DD (for example, 1992-01-17)<br /><br /> YYYY-MMM-DD (for example, 1992-Jan-17)<br /><br /> Mixed date separators are not allowed within a table.<br /><br /> The Text ISAM formats a DATETIME field in the United States or European format, depending upon the International setting in the Windows Control Panel.|  
|FLOAT|The maximum width includes the sign and decimal point. In Schema.ini, the width is denoted as follows:<br /><br /> 14.083 is FLOAT Width 6<br /><br /> -14.083 is FLOAT Width 7<br /><br /> +14.083 is FLOAT Width 7<br /><br /> 14083. is FLOAT Width 6<br /><br /> ODBC always returns 8 for FLOAT columns.<br /><br /> FLOAT columns can also be in scientific notation, for example:<br /><br /> -3.04E+2 is Float Width 8<br /><br /> 25E4 is Float Width 4<br /><br /> **Note** Decimal and scientific notation cannot be mixed in a column.<br /><br /> NULL values are represented by a blank padded string in fixed-length files, and are omitted in delimited files.<br /><br /> Float data can be padded with leading blanks.|  
|INTEGER|Valid values for INTEGER columns are 32767 to -32766.<br /><br /> In Schema.ini, the width is denoted as follows:<br /><br /> 14083 is INTEGER Width 5<br /><br /> 0 is INTEGER Width 1<br /><br /> ODBC always returns 4 for INTEGER columns.<br /><br /> The maximum width includes a sign. The maximum width of an INTEGER column is 11, although the width can be greater due to blanks that are allowed in fixed-format tables.|  
|LONGCHAR|The theoretical limit on the width of a LONGCHAR column in either a fixed-length or delimited table is 65500K. The Text ISAM is more likely to provide reliable support up to about 32K.|  
  
 More limitations on data types can be found in [Data Type Limitations](../../odbc/microsoft/data-type-limitations.md).
