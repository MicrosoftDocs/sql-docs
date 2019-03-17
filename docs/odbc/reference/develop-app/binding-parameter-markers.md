---
title: "Binding Parameter Markers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "parameter markers [ODBC]"
  - "binding parameter markers [ODBC]"
ms.assetid: fe88c1c2-4ee4-45e0-8500-b8c25c047815
author: MightyPen
ms.author: genemi
manager: craigg
---
# Binding Parameter Markers
The application binds parameters by calling **SQLBindParameter**. **SQLBindParameter** binds one parameter at a time. With it, the application specifies the following:  
  
-   The parameter number. Parameters are numbered in increasing parameter order in the SQL statement, starting with the number 1. While it is legal to specify a parameter number that is higher than the number of parameters in the SQL statement, the parameter value will be ignored when the statement is executed.  
  
-   The parameter type (input, input/output, or output). Except for parameters in procedure calls, all parameters are input parameters. For more information, see [Procedure Parameters](../../../odbc/reference/develop-app/procedure-parameters.md), later in this section.  
  
-   The C data type, address, and byte length of the variable bound to the parameter. The driver must be able to convert the data from the C data type to the SQL data type or an error is returned. For a list of supported conversions, see [Converting Data from C to SQL Data Types](../../../odbc/reference/appendixes/converting-data-from-c-to-sql-data-types.md) in Appendix D: Data Types.  
  
-   The SQL data type, precision, and scale of the parameter itself.  
  
-   The address of a length/indicator buffer. It provides the byte length of binary or character data, specifies that the data is NULL, or specifies that the data will be sent with **SQLPutData**. For more information, see [Using Length/Indicator Values](../../../odbc/reference/develop-app/using-length-and-indicator-values.md).  
  
 For example, the following code binds *SalesPerson* and *CustID* to parameters for the SalesPerson and CustID columns. Because *SalesPerson* contains character data, which is variable length, the code specifies the byte length of *SalesPerson* (11) and binds *SalesPersonLenOrInd* to contain the byte length of the data in *SalesPerson*. This information is not necessary for *CustID* because it contains integer data, which is of fixed length.  
  
```  
SQLCHAR       SalesPerson[11];  
SQLINTEGER    SalesPersonLenOrInd, CustIDInd;  
SQLUINTEGER   CustID;  
  
// Bind SalesPerson to the parameter for the SalesPerson column and  
// CustID to the parameter for the CustID column.  
SQLBindParameter(hstmt1, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_CHAR, 10, 0,  
                  SalesPerson, sizeof(SalesPerson), &SalesPersonLenOrInd);  
SQLBindParameter(hstmt1, 2, SQL_PARAM_INPUT, SQL_C_ULONG, SQL_INTEGER, 10, 0,  
                  &CustID, 0, &CustIDInd);  
  
// Set values of salesperson and customer ID and length/indicators.  
strcpy_s((char*)SalesPerson, _countof(SalesPerson), "Garcia");  
SalesPersonLenOrInd = SQL_NTS;  
CustID = 1331;  
CustIDInd = 0;  
  
// Execute a statement to get data for all orders made to the specified  
// customer by the specified salesperson.  
SQLExecDirect(hstmt1,"SELECT * FROM Orders WHERE SalesPerson=? AND CustID=?",SQL_NTS);  
```  
  
 When **SQLBindParameter** is called, the driver stores this information in the structure for the statement. When the statement is executed, it uses the information to retrieve the parameter data and send it to the data source.  
  
> [!NOTE]  
>  In ODBC 1.0, parameters were bound with **SQLSetParam**. The Driver Manager maps calls between **SQLSetParam** and **SQLBindParameter**, depending on the versions of ODBC used by the application and driver.
