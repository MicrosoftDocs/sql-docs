---
title: "SQLSTATEs | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC], sqlstates"
  - "SQLSTATE [ODBC]"
ms.assetid: f29fff2e-3d09-4a8c-a2f9-2059062cbebf
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSTATEs
SQLSTATEs provide detailed information about the cause of a warning or error. The SQLSTATEs in this manual are based on those found in the ISO/IEF CLI specification, although those SQLSTATEs that start with IM are specific to ODBC.  
  
 Unlike return codes, the SQLSTATEs in this manual are guidelines, and drivers are not required to return them. Therefore, while drivers should return the proper SQLSTATE for any error or warning they are capable of detecting, applications should not count on this always occurring. The reasons for this situation are twofold:  
  
-   **Incompleteness** Although this manual lists a large number of errors and warnings and possible causes for those errors and warnings, it is not complete and probably never will be; driver implementations simply vary too much. Any given driver probably will not return all of the SQLSTATEs listed in this manual and might return SQLSTATEs not listed in this manual.  
  
-   **Complexity** Some database engines - particularly relational database engines - return literally thousands of errors and warnings. The drivers for such engines are unlikely to map all of these errors and warnings to SQLSTATEs because of the effort involved, the inexactness of the mappings, the large size of the resulting code, and the low value of the resulting code, which often returns programming errors that should never be encountered at run time. Therefore, drivers should map as many errors and warnings as seems reasonable and be sure to map those errors and warnings on which application logic might be based, such as SQLSTATE 01004 (Data truncated).  
  
 Because SQLSTATEs are not returned reliably, most applications just display them to the user along with their associated diagnostic message, which is often tailored to the specific error or warning that occurred, and native error code. There is rarely any loss of functionality in doing this, because applications cannot base programming logic on most SQLSTATEs anyway. For example, suppose **SQLExecDirect** returns SQLSTATE 42000 (Syntax error or access violation). If the SQL statement that caused this error is hard-coded or built by the application, this is a programming error and the code needs to be fixed. If the SQL statement is entered by the user, this is a user error and the application has done all that is possible by informing the user of the problem.  
  
 When applications do base programming logic on SQLSTATEs, they should be prepared for the SQLSTATE not to be returned or for a different SQLSTATE to be returned. Exactly which SQLSTATEs are returned reliably can be based only on experience with numerous drivers. However, a general guideline is that SQLSTATEs for errors that occur in the driver or Driver Manager, as opposed to the data source, are more likely to be returned reliably. For example, most drivers probably return SQLSTATE HYC00 (Optional feature not implemented), while fewer drivers probably return SQLSTATE 42021 (Column already exists).  
  
 The following SQLSTATEs indicate run-time errors or warnings and are good candidates on which to base programming logic. However, there is no guarantee that all drivers return them.  
  
-   01004 (Data truncated)  
  
-   01S02 (Option value changed)  
  
-   HY008 (Operation canceled)  
  
-   HYC00 (Optional feature not implemented)  
  
-   HYT00 (Timeout expired)  
  
 SQLSTATE HYC00 (Optional feature not implemented) is particularly significant because it is the only way in which an application can determine whether a driver supports a particular statement or connection attribute.  
  
 For a complete list of SQLSTATEs and what functions return them, see [Appendix A: ODBC Error Codes](../../../odbc/reference/appendixes/appendix-a-odbc-error-codes.md). For a detailed explanation of the conditions under which each function might return a particular SQLSTATE, see that function.
