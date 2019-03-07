---
title: "String Functions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "functions [ODBC], string functions"
  - "string functions [ODBC]"
ms.assetid: 270f669e-8aab-4db0-95a4-f2b3c69538b3
author: MightyPen
ms.author: genemi
manager: craigg
---
# String Functions
The following table lists string manipulation functions. An application can determine which string functions are supported by a driver by calling **SQLGetInfo** with an *information type* of SQL_STRING_FUNCTIONS.  
  
## Remarks  
 Arguments denoted as *string_exp* can be the name of a column,a *character-string-literal*, or the result of another scalar function, where the underlying data type can be represented as SQL_CHAR, SQL_VARCHAR, or SQL_LONGVARCHAR.  
  
 Arguments denoted as *character_exp* are a variable-length character string.  
  
 Arguments denoted as *start*, *length*, or *count* can be a *numeric-literal* or the result of another scalar function, where the underlying data type can be represented as SQL_TINYINT, SQL_SMALLINT, or SQL_INTEGER.  
  
 The string functions listed here are 1-based; that is, the first character in the string is character 1.  
  
 The BIT_LENGTH, CHAR_LENGTH, CHARACTER_LENGTH, OCTET_LENGTH, and POSITION string scalar functions have been added in ODBC 3.0 to align with SQL-92.  
  
|Function|Description|  
|--------------|-----------------|  
|**ASCII(** _string_exp_ **)**  (ODBC 1.0)|Returns the ASCII code value of the leftmost character of *string_exp* as an integer.|  
|**BIT_LENGTH(** _string_exp_ **)**  (ODBC 3.0)|Returns the length in bits of the string expression.<br /><br /> Does not work only for string data types, therefore will not implicitly convert *string_exp* to string but instead will return the (internal) size of whatever datatype it is given.|  
|**CHAR(** _code_ **)**  (ODBC 1.0)|Returns the character that has the ASCII code value specified by *code*. The value of *code* should be between 0 and 255; otherwise, the return value is data source-dependent.|  
|**CHAR_LENGTH(** _string_exp_ **)**  (ODBC 3.0)|Returns the length in characters of the string expression, if the string expression is of a character data type; otherwise, returns the length in bytes of the string expression (the smallest integer not less than the number of bits divided by 8). (This function is the same as the CHARACTER_LENGTH function.)|  
|**CHARACTER_LENGTH(** _string_exp_ **)**  (ODBC 3.0)|Returns the length in characters of the string expression, if the string expression is of a character data type; otherwise, returns the length in bytes of the string expression (the smallest integer not less than the number of bits divided by 8). (This function is the same as the CHAR_LENGTH function.)|  
|**CONCAT(** _string_exp1_,_string_exp2_**)**  (ODBC 1.0)|Returns a character string that is the result of concatenating *string_exp2* to *string_exp1*. The resulting string is DBMS-dependent. For example, if the column represented by *string_exp1* contained a NULL value, DB2 would return NULL but SQL Server would return the non-NULL string.|  
|**DIFFERENCE(** _string_exp1_,_string_exp2_**)**  (ODBC 2.0)|Returns an integer value that indicates the difference between the values returned by the SOUNDEX function for *string_exp1* and *string_exp2*.|  
|**INSERT(** _string_exp1_, *start*, *length*, _string_exp2_**)**  (ODBC 1.0)|Returns a character string where *length* characters have been deleted from *string_exp1*, beginning at *start*, and where *string_exp2* has been inserted into *string_exp,* beginning at *start*.|  
|**LCASE(** _string_exp_ **)** (ODBC 1.0)|Returns a string equal to that in *string_exp*, with all uppercase characters converted to lowercase.|  
|**LEFT(** _string_exp_, _count_**)** (ODBC 1.0)|Returns the leftmost *count* characters of *string_exp*.|  
|**LENGTH(** _string_exp_ **)** (ODBC 1.0)|Returns the number of characters in *string_exp,* excluding trailing blanks.<br /><br /> **LENGTH** only accepts strings. Therefore will implicitly convert *string_exp* to a string, and return the length of this string (not the internal size of the datatype).|  
|**LOCATE(** _string_exp1_, *string_exp2*[, *start*]**)** (ODBC 1.0)|Returns the starting position of the first occurrence of *string_exp1* within *string_exp2*. The search for the first occurrence of *string_exp1* begins with the first character position in *string_exp2* unless the optional argument, *start*, is specified. If *start* is specified, the search begins with the character position indicated by the value of *start*. The first character position in *string_exp2* is indicated by the value 1. If *string_exp1* is not found within *string_exp2*, the value 0 is returned.<br /><br /> If an application can call the LOCATE scalar function with the *string_exp1*, *string_exp2*, and *start* arguments, the driver returns SQL_FN_STR_LOCATE when **SQLGetInfo** is called with an *Option* of SQL_STRING_FUNCTIONS. If the application can call the LOCATE scalar function with only the *string_exp1* and *string_exp2* arguments, the driver returns SQL_FN_STR_LOCATE_2 when **SQLGetInfo** is called with an *Option* of SQL_STRING_FUNCTIONS. Drivers that support calling the LOCATE function with either two or three arguments return both SQL_FN_STR_LOCATE and SQL_FN_STR_LOCATE_2.|  
|**LTRIM(** _string_exp_ **)** (ODBC 1.0)|Returns the characters of *string_exp*, with leading blanks removed.|  
|**OCTET_LENGTH(** _string_exp_ **)** (ODBC 3.0)|Returns the length in bytes of the string expression. The result is the smallest integer not less than the number of bits divided by 8.<br /><br /> Does not work only for string data types, therefore will not implicitly convert *string_exp* to string but instead will return the (internal) size of whatever datatype it is given.|  
|**POSITION(** _character_exp_ **IN** _character_exp_**)** (ODBC 3.0)|Returns the position of the first character expression in the second character expression. The result is an exact numeric with an implementation-defined precision and a scale of 0.|  
|**REPEAT(** _string_exp,_ _count_**)** (ODBC 1.0)|Returns a character string composed of *string_exp* repeated *count* times.|  
|**REPLACE(** _string_exp1_, *string_exp2*, _string_exp3_**)** (ODBC 1.0)|Search *string_exp1* foroccurrences of *string_exp2*, and replace with *string_exp3*.|  
|**RIGHT(** _string_exp_, _count_**)** (ODBC 1.0)|Returns the rightmost *count* characters of *string_exp*.|  
|**RTRIM(** _string_exp_ **)** (ODBC 1.0)|Returns the characters of *string_exp* with trailing blanks removed.|  
|**SOUNDEX(** _string_exp_ **)** (ODBC 2.0)|Returns a data source-dependent character string representing the sound of the words in *string_exp*. For example, SQL Server returns a 4-digit SOUNDEX code; Oracle returns a phonetic representation of each word.|  
|**SPACE(** _count_ **)** (ODBC 2.0)|Returns a character string consisting of *count* spaces.|  
|**SUBSTRING(** _string_exp_, *start*, length**)** (ODBC 1.0)|Returns a character string that is derived from *string_exp*, beginning at the character position specified by *start* for *length* characters.|  
|**UCASE(** _string_exp_ **)** (ODBC 1.0)|Returns a string equal to that in *string_exp*, with all lowercase characters converted to uppercase.|
