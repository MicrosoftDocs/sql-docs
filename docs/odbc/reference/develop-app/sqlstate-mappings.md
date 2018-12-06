---
title: "SQLSTATE Mappings | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility [ODBC], SQLSTATE"
  - "backward compatibility [ODBC], SQLSTATE"
  - "SQLSTATE [ODBC]"
ms.assetid: 6e6cabcf-a204-40eb-b77d-8a0c4a5e8524
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSTATE Mappings
This topic discusses SQLSTATE values for ODBC 2.*x* and ODBC 3.*x*. For more information on ODBC 3.*x* SQLSTATE values, see [Appendix A: ODBC Error Codes](../../../odbc/reference/appendixes/appendix-a-odbc-error-codes.md).  
  
 In ODBC 3.*x*, HYxxx SQLSTATEs are returned instead of S1xxx, and 42Sxx SQLSTATEs are returned instead of S00XX. This was done to align with Open Group and ISO standards. In many cases, the mapping is not one-to-one because the standards have redefined the interpretation of several SQLSTATEs.  
  
 When an ODBC 2.*x* application is upgraded to an ODBC 3.*x* application, the application has to be changed to expect ODBC 3.*x* SQLSTATEs instead of ODBC 2.*x* SQLSTATEs. The following table lists the ODBC 3.*x* SQLSTATEs that each ODBC 2.*x* SQLSTATE is mapped to.  
  
 When the SQL_ATTR_ODBC_VERSION environment attribute is set to SQL_OV_ODBC2, the driver posts ODBC 2.*x* SQLSTATEs instead of ODBC 3.*x* SQLSTATEs when **SQLGetDiagField** or **SQLGetDiagRec** is called. A specific mapping can be determined by noting the ODBC 2*.x* SQLSTATE in column 1 of the following table that corresponds to the ODBC 3.*x* SQLSTATE in column 2.  
  
|ODBC 2.*x* SQLSTATE|ODBC 3.*x* SQLSTATE|Comments|  
|-------------------------|-------------------------|--------------|  
|01S03|01001||  
|01S04|01001||  
|22003|HY019||  
|22008|22007||  
|22005|22018||  
|24000|07005||  
|37000|42000||  
|70100|HY018||  
|S0001|42S01||  
|S0002|42S02||  
|S0011|42S11||  
|S0012|42S12||  
|S0021|42S21||  
|S0022|42S22||  
|S0023|42S23||  
|S1000|HY000||  
|S1001|HY001||  
|S1002|07009|ODBC 2.*x* SQLSTATE S1002 is mapped to ODBC 3.*x* SQLSTATE 07009 if the underlying function is **SQLBindCol**, **SQLColAttribute**, **SQLExtendedFetch**, **SQLFetch**, **SQLFetchScroll**, or **SQLGetData**.|  
|S1003|HY003||  
|S1004|HY004||  
|S1008|HY008||  
|S1009|HY009|Returned for an invalid use of a null pointer.|  
|S1009|HY024|Returned for an invalid attribute value.|  
|S1009|HY092|Returned for updating or deleting data by a call to **SQLSetPos**, or adding, updating, or deleting data by a call to **SQLBulkOperations**, when the concurrency is read-only.|  
|S1010|HY007 HY010|SQLSTATE S1010 is mapped to SQLSTATE HY007 when **SQLDescribeCol** is called prior to calling **SQLPrepare**, **SQLExecDirect**, or a catalog function for the *StatementHandle*. Otherwise, SQLSTATE S1010 is mapped to SQLSTATE HY010.|  
|S1011|HY011||  
|S1012|HY012||  
|S1090|HY090||  
|S1091|HY091||  
|S1092|HY092||  
|S1093|07009|ODBC 3.*x* SQLSTATE 07009 is mapped to ODBC 2.*x* SQLSTATE S1093 if the underlying function is **SQLBindParameter** or **SQLDescribeParam**.|  
|S1096|HY096||  
|S1097|HY097||  
|S1098|HY098||  
|S1099|HY099||  
|S1100|HY100||  
|S1101|HY101||  
|S1103|HY103||  
|S1104|HY104||  
|S1105|HY105||  
|S1106|HY106||  
|S1107|HY107||  
|S1108|HY108||  
|S1109|HY109||  
|S1110|HY110||  
|S1111|HY111||  
|S1C00|HYC00||  
|S1T00|HYT00||  
  
> [!NOTE]  
>  ODBC 3.*x* SQLSTATE 07008 is mapped to ODBC 2.*x* SQLSTATE S1000.
