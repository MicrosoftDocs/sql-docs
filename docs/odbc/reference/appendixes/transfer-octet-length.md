---
title: "Transfer Octet Length | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "transfer octet length of data types [ODBC]"
  - "size of data types [ODBC]"
  - "SQL data types [ODBC], column characteristics"
  - "data types [ODBC], transfer octet length"
ms.assetid: 9fdc9762-e203-4cff-9212-54f450bf18d9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Transfer Octet Length
The transfer octet length of a column is the maximum number of bytes returned to the application when data is transferred to its default C data type. For character data, the transfer octet length does not include space for the null-termination character. The transfer octet length of a column may be different than the number of bytes required to store the data on the data source.  
  
 The transfer octet length defined for each ODBC SQL data type is shown in the following table.  
  
|SQL type identifier|Length|  
|-------------------------|------------|  
|All character types[a]|The defined or the maximum (for variable type) length of the column in bytes. This is the same value as the descriptor field SQL_DESC_OCTET_LENGTH.|  
|SQL_DECIMAL<br />SQL_NUMERIC|The number of bytes required to hold the character representation of this data if the character set is ANSI, and twice this number if the character set is UNICODE. This is the maximum number of digits plus two, because the data is returned as a character string and characters are needed for the digits, a sign, and a decimal point. For example, the transfer length of a column defined as NUMERIC(10,3) is 12.|  
|SQL_TINYINT|1|  
|SQL_SMALLINT|2|  
|SQL_INTEGER|4|  
|SQL_BIGINT|The number of bytes required to hold the character representation of this data if the character set is ANSI, and twice this number if the character set is UNICODE, because this data type is returned as a character string by default. The character representation consists of 20 characters: 19 for digits and a sign, if signed, or 20 digits, if unsigned. Therefore, the length is 20.|  
|SQL_REAL|4|  
|SQL_FLOAT|8|  
|SQL_DOUBLE|8|  
|SQL_BIT|1|  
|All binary types[a]|The number of bytes required to hold the defined (for fixed types) or maximum (for variable types) number of characters.|  
|SQL_TYPE_DATE<br />SQL_TYPE_TIME|6 (the size of the SQL_DATE_STRUCT or SQL_TIME_STRUCT structure).|  
|SQL_TYPE_TIMESTAMP|16 (the size of the SQL_TIMESTAMP_STRUCT structure).|  
|All interval data types|34 (the size of the interval structure).|  
|SQL_GUID|16 (the size of the GUID structure).|  
  
 [a]   If the driver cannot determine the column or parameter length for variable types, it returns SQL_NO_TOTAL.
