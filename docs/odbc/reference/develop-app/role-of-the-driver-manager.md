---
title: "Role of the Driver Manager | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC], SqlGetDiagField"
  - "diagnostic information [ODBC], driver manager error checking"
  - "SQLGetDiagField function [ODBC], driver manager"
  - "SQLGetDiagRec function [ODBC], driver manager"
  - "ODBC driver manager [ODBC]"
  - "diagnostic information [ODBC], SqlGetDiagRec"
  - "driver manager [ODBC], error checking"
ms.assetid: 7b861c82-357e-4590-8074-45136e9ed15e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Role of the Driver Manager
The Driver Manager determines the final order in which to return status records that it generates. In particular, it determines which record has the highest rank and is to be returned first. The driver is responsible for ordering status records that it generates. If status records are posted by both the Driver Manager and the driver, the Driver Manager is responsible for ordering them. For more information, see [Sequence of Status Records](../../../odbc/reference/develop-app/sequence-of-status-records.md).  
  
 The Driver Manager does as much error checking as it can. This saves every driver from checking for the same errors. For example, if a function argument accepts a discrete number of values, such as *Operation* in **SQLSetPos**, the Driver Manager checks that the specified value is legal.  
  
 The following sections describe the types of conditions checked by the Driver Manager. They are not intended to be exhaustive; for a complete list of the SQLSTATEs the Driver Manager returns, see the "Diagnostics" section of each function; the description of each check made by the Driver Manager starts with the letters "(DM)." Also see the state transition tables in [Appendix B: ODBC State Transition Tables](../../../odbc/reference/appendixes/appendix-b-odbc-state-transition-tables.md); errors shown in parentheses are detected by the Driver Manager.  
  
 This section contains the following topics.  
  
-   [Argument Value Checks](../../../odbc/reference/develop-app/argument-value-checks.md)  
  
-   [State Transition Checks](../../../odbc/reference/develop-app/state-transition-checks.md)  
  
-   [General Error Checks](../../../odbc/reference/develop-app/general-error-checks.md)  
  
-   [Driver Manager Error and Warning Checks](../../../odbc/reference/develop-app/driver-manager-error-and-warning-checks.md)
