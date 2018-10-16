---
title: "Rules for Conversions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "numeric data type [ODBC], literals"
  - "conversions with numeric literals [ODBC]"
  - "numeric literals [ODBC]"
  - "literals [ODBC], numeric"
ms.assetid: 89f846a3-001d-496a-9843-ac9c38dc1762
author: MightyPen
ms.author: genemi
manager: craigg
---
# Rules for Conversions
The rules in this section apply for conversions involving numeric literals. For the purposes of these rules, the following terms are defined:  
  
-   *Store assignment:* When sending data into a table column in a database. This occurs during calls to **SQLExecute**, **SQLExecDirect**, and **SQLSetPos**. During store assignment, "target" refers to a database column and "source" refers to data in application buffers.  
  
-   *Retrieval assignment:* When retrieving data from the database into application buffers. This occurs during calls to **SQLFetch**, **SQLGetData**, **SQLFetchScroll**, and **SQLSetPos**. During retrieval assignment, "target" refers to the application buffers and "source" refers to the database column.  
  
-   *CS:* The value in the character source.  
  
-   *NT:* The value in the numeric target.  
  
-   *NS:* The value in the numeric source.  
  
-   *CT:* The value in the character target.  
  
-   Precision of an exact numeric literal: the number of digits it contains.  
  
-   The scale of an exact numeric literal: the number of digits to the right of the expressed or implied period.  
  
-   The precision of an approximate numeric literal: the precision of its mantissa.  
  
## Character Source to Numeric Target  
 Following are the rules for converting from a character source (CS) to a numeric target (NT):  
  
1.  Replace CS with the value obtained by removing any leading or trailing spaces in CS. If CS is not a valid *numeric-literal*, SQLSTATE 22018 (Invalid character value for cast specification) is returned.  
  
2.  Replace CS with the value obtained by removing leading zeroes before the decimal point, trailing zeroes after the decimal point, or both.  
  
3.  Convert CS to NT. If the conversion results in a loss of significant digits, SQLSTATE 22003 (Numeric value out of range) is returned. If the conversion results in the loss of nonsignificant digits, SQLSTATE 01S07 (Fractional truncation) is returned.  
  
## Numeric Source to Character Target  
 Following are the rules for converting from a numeric source (NS) to a character target (CT):  
  
1.  Let LT be the length in characters of CT. For retrieval assignment, LT is equal to the length of the buffer in characters minus the number of bytes in the null-termination character for this character set.  
  
2.  Cases:  
  
    -   If NS is an exact numeric type, then let YP equal the shortest character string that conforms to the definition of *exact-numeric-literal* such that the scale of YP is the same as the scale of NS, and the interpreted value of YP is the absolute value of NS.  
  
    -   If NS is an approximate numeric type, then let YP be a character string as follows:  
  
         Case:  
  
         If NS is equal to 0, then YP is 0.  
  
         Let YSN be the shortest character string that conforms to the definition of exact-*numeric-literal* and whose interpreted value is the absolute value of NS. If the length of YSN is less than the (*precision* + 1) of the data type of NS, then let YP equal YSN.  
  
         Otherwise, YP is the shortest character string that conforms to the definition of *approximate-numeric-literal* whose interpreted value is the absolute value of NS and whose *mantissa* consists of a single *digit* that is not '0', followed by a *period* and an *unsigned-integer*.  
  
3.  Case:  
  
    -   If NS is less than 0, then let Y be the result of:  
  
         '-' &#124;&#124; YP  
  
         where '&#124;&#124;' is the string concatenation operator.  
  
         Otherwise, let Y equal YP.  
  
4.  Let LY be the length in characters of Y.  
  
5.  Case:  
  
    -   If LY equals LT, then CT is set to Y.  
  
    -   If LY is less than LT, then CT is set to Y extended on the right by appropriate number of spaces.  
  
         Otherwise (LY > LT), copy the first LT characters of Y into CT.  
  
         Case:  
  
         If this is a store assignment, return the error SQLSTATE 22001 (String data, right-truncated).  
  
         If this is retrieval assignment, return the warning SQLSTATE 01004 (String data, right-truncated). When the copy results in the loss of fractional digits (other than trailing zeros), it is driver-defined whether one of the following occurs:  
  
         (1)   The driver truncates the string in Y to an appropriate scale        (which can be zero also) and writes the result into CT.  
  
         (2)   The driver rounds the string in Y to an appropriate scale        (which can be zero also) and writes the result into CT.  
  
         (3)   The driver neither truncates nor rounds, but just copies        the first LT characters of Y into CT.
