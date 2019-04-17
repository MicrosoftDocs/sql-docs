---
title: "SQL to C: Numeric | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data conversions from SQL to C types [ODBC], numeric"
  - "numeric data type [ODBC], converting"
  - "converting data from SQL to C types [ODBC], numeric"
ms.assetid: 76f8b5d5-4bd0-4dcb-a90a-698340e0d36e
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL to C: Numeric

The identifiers for the numeric ODBC SQL data types are the following:

- SQL_DECIMAL  
- SQL_BIGINT  
- SQL_NUMERIC  
- SQL_REAL  
- SQL_TINYINT  
- SQL_FLOAT  
- SQL_SMALLINT  
- SQL_DOUBLE SQL_INTEGER  

The following table shows the ODBC C data types to which numeric SQL data may be converted. For an explanation of the columns and terms in the table, see [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md).  

|C type identifier|Test|**TargetValuePtr*|**StrLen_or_IndPtr*|SQLSTATE|  
|-----------------------|----------|------------------------|----------------------------|--------------|  
|SQL_C_CHAR|Character byte length < *BufferLength*<br /><br /> Number of whole (as opposed to fractional) digits < *BufferLength*<br /><br /> Number of whole (as opposed to fractional) digits >= *BufferLength*|Data<br /><br /> Truncated data<br /><br /> Undefined|Length of data in bytes<br /><br /> Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_WCHAR|Character length < *BufferLength*<br /><br /> Number of whole (as opposed to fractional) digits < *BufferLength*<br /><br /> Number of whole (as opposed to fractional) digits >= *BufferLength*|Data<br /><br /> Truncated data<br /><br /> Undefined|Length of data in characters<br /><br /> Length of data in characters<br /><br /> Undefined|n/a<br /><br /> 01004<br /><br /> 22003|  
|SQL_C_STINYINT<br /><br /> SQL_C_UTINYINT<br /><br /> SQL_C_TINYINT<br /><br /> SQL_C_SBIGINT<br /><br /> SQL_C_UBIGINT<br /><br /> SQL_C_SSHORT<br /><br /> SQL_C_USHORT<br /><br /> SQL_C_SHORT<br /><br /> SQL_C_SLONG<br /><br /> SQL_C_ULONG<br /><br /> SQL_C_LONG<br /><br /> SQL_C_NUMERIC|Data converted without truncation[a]<br /><br /> Data converted with truncation of fractional digits[a]<br /><br /> Conversion of data would result in loss of whole (as opposed to fractional) digits[a]|Data<br /><br /> Truncated data<br /><br /> Undefined|Size of the C data type<br /><br /> Size of the C data type<br /><br /> Undefined|n/a<br /><br /> 01S07<br /><br /> 22003|  
|SQL_C_FLOAT<br /><br /> SQL_C_DOUBLE|Data is within the range of the data type to which the number is being converted[a]<br /><br /> Data is outside the range of the data type to which the number is being converted[a]|Data<br /><br /> Undefined|Size of the C data type<br /><br /> Undefined|n/a<br /><br /> 22003|  
|SQL_C_BIT|Data is 0 or 1[a]<br /><br /> Data is greater than 0, less than 2, and not equal to 1[a]<br /><br /> Data is less than 0 or greater than or equal to 2[a]|Data<br /><br /> Truncated data<br /><br /> Undefined|1[b]<br /><br /> 1[b]<br /><br /> Undefined|n/a<br /><br /> 01S07<br /><br /> 22003|  
|SQL_C_BINARY|Byte length of data <= *BufferLength*<br /><br /> Byte length of data > *BufferLength*|Data<br /><br /> Undefined|Length of data<br /><br /> Undefined|n/a<br /><br /> 22003|  
|SQL_C_INTERVAL_MONTH[c] SQL_C_INTERVAL_YEAR[c] SQL_C_INTERVAL_DAY[c] SQL_C_INTERVAL_HOUR[c] SQL_C_INTERVAL_MINUTE[c] SQL_C_INTERVAL_SECOND[c]|Data not truncated<br /><br /> Fractional seconds portion truncated<br /><br /> Whole part of number truncated|Data<br /><br /> Truncated data<br /><br /> Undefined|Length of data in bytes<br /><br /> Length of data in bytes<br /><br /> Undefined|n/a<br /><br /> 01S07<br /><br /> 22015|  
|SQL_C_INTERVAL_YEAR_TO_MONTH SQL_C_INTERVAL_DAY_TO_HOUR SQL_C_INTERVAL_DAY_TO_MINUTE SQL_C_INTERVAL_DAY_TO_SECOND SQL_C_INTERVAL_HOUR_TO_MINUTE SQL_C_INTERVAL_HOUR_TO_SECOND|Whole part of number truncated|Undefined|Undefined|22015|  
  
 [a]   The value of *BufferLength* is ignored for this conversion. The driver assumes that the size of **TargetValuePtr* is the size of the C data type.  
  
 [b]   This is the size of the corresponding C data type.  
  
 [c]   This conversion is supported only for the exact numeric data types (SQL_DECIMAL, SQL_NUMERIC, SQL_TINYINT, SQL_SMALLINT, SQL_INTEGER, and SQL_BIGINT). It is not supported for the approximate numeric data types (SQL_REAL, SQL_FLOAT, or SQL_DOUBLE).  

## SQL_C_NUMERIC and SQLSetDescField

 The [SQLSetDescField Function](../../../odbc/reference/syntax/sqlsetdescfield-function.md) is required to perform manual binding with SQL_C_NUMERIC values. (Note that SQLSetDescField was added in ODBC 3.0.) To perform manual binding, you must first get the descriptor handle.  

```cpp
if (fCType == SQL_C_NUMERIC) {   
   // special processing required for NUMERIC to get right scale & precision  
   // Modify the fields in the implicit application parameter descriptor  
   SQLHDESC hdesc=NULL;  
  
   // Use SQL_ATTR_APP_ROW_DESC for calls to SQLBindCol()  
   // Use SQL_ATTR_APP_PARAM_DESC for calls to SQLBindParameter()  
   //  
   // retcode = SQLGetStmtAttr(hstmt, SQL_ATTR_APP_ROW_DESC, &hdesc, 0, NULL);  
   retcode = SQLGetStmtAttr(hstmt, SQL_ATTR_APP_PARAM_DESC, &hdesc, 0, NULL);  
   if (!ODBC_CALL_SUCCESS(retcode)) {  
      printf ("\nSQLGetStmtAttr failed");  
      i = 1;  
      sqlstate[7] = '\0';  
      while (SQLGetDiagRec(SQL_HANDLE_STMT, hstmt, i, sqlstate, &NativeError, wrkbuf, sizeof(wrkbuf), &len) != SQL_NO_DATA) {  
         printf("\niTestCase = %d Failed...Precision = %d, Scale = %d\nNativeError=%d, State=%s, \n  Message=%s",   
            iTestCase, Precision, Scale, NativeError, sqlstate, wrkbuf);  
         i++;  
      }  
      continue;  
   }  
   retcode = SQLSetDescField(hdesc, iCol, SQL_DESC_TYPE, (SQLPOINTER) SQL_C_NUMERIC, 0);  
   if (!ODBC_CALL_SUCCESS(retcode))  
      goto error;  
   retcode = SQLSetDescField(hdesc, iCol, SQL_DESC_PRECISION, (SQLPOINTER)num.precision, 0);  
   if (!ODBC_CALL_SUCCESS(retcode))  
      goto error;  
   retcode = SQLSetDescField(hdesc, iCol, SQL_DESC_SCALE, (SQLPOINTER)num.scale, 0);  
   if (!ODBC_CALL_SUCCESS(retcode))  
      goto error;  
   retcode = SQLSetDescField(hdesc, iCol, SQL_DESC_DATA_PTR, (SQLPOINTER) &(num), sizeof(num));  
```
