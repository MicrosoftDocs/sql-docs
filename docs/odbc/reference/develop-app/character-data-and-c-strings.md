---
description: "Character Data and C Strings"
title: "Character Data and C Strings | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data buffers [ODBC], length"
  - "data buffers [ODBC], character data"
  - "buffers [ODBC], C strings"
  - "buffers [ODBC], character data"
  - "character data and buffers [ODBC]"
  - "length of data buffers [ODBC]"
  - "data buffers [ODBC], C strings"
  - "buffers [ODBC], length"
  - "C strings and buffers [ODBC]"
ms.assetid: 3a141cb4-229d-4027-9349-615cb2995e36
author: David-Engel
ms.author: v-davidengel
---
# Character Data and C Strings
Input parameters that refer to variable-length character data (such as column names, dynamic parameters, and string attribute values) have an associated length parameter. If the application terminates strings with the null character, as is typical in C, it provides as an argument either the length in bytes of the string (not including the null-terminator) or SQL_NTS (Null-Terminated String). A non-negative length argument specifies the actual length of the associated string. The length argument may be 0 to specify a zero-length string, which is distinct from a NULL value. The negative value SQL_NTS directs the driver to determine the length of the string by locating the null-termination character.  
  
 When character data is returned from the driver to the application, the driver must always null-terminate it. This gives the application the choice of whether to handle the data as a string or a character array. If the application buffer is not large enough to return all of the character data, the driver truncates it to the byte length of the buffer less the number of bytes required by the null-termination character, null-terminates the truncated data, and stores it in the buffer. Therefore, applications must always allocate extra space for the null-termination character in buffers used to retrieve character data. For example, a 51-byte buffer is needed to retrieve 50 characters of data.  
  
 Special care must be taken by both the application and the driver when sending or retrieving long character data in parts with **SQLPutData** or **SQLGetData**. If the data is passed as a series of null-terminated strings, the null-termination characters on these strings must be stripped before the data can be reassembled.  
  
 A number of ODBC programmers have confused character data and C strings. That this has occurred is an artifact of using the C language when defining ODBC functions. If an ODBC driver or application uses another language - remember that ODBC is language-independent - this confusion is less likely to arise.  
  
 When C strings are used to hold character data, the null-termination character is not considered to be part of the data and is not counted as part of its byte length. For example, the character data "ABC" can be held as the C string "ABC\0" or the character array {'A', 'B', 'C'}. The byte length of the data is 3, whether or not it is treated as a string or a character array.  
  
 Although applications and drivers commonly use C strings (null-terminated arrays of characters) to hold character data, there is no requirement to do this. In C, character data can also be treated as an array of characters (without null-termination) and its byte length passed separately in the length/indicator buffer.  
  
 Because character data can be held in a non-null-terminated array and its byte length passed separately, it is possible to embed null characters in character data. However, the behavior of ODBC functions in this case is undefined and it is driver-specific whether a driver handles this correctly. Thus, interoperable applications should always handle character data that can contain embedded null characters as binary data.
