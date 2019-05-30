---
title: "Declaring the Application&#39;s ODBC Version | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "declaring ODBC version [ODBC]"
  - "data sources [ODBC], declaring ODBC version"
  - "ODBC drivers [ODBC], declaring ODBC version"
  - "connecting to driver [ODBC], declaring ODBC version"
  - "connecting to data source [ODBC], declaring ODBC version"
  - "version declaration [ODBC]"
ms.assetid: 083a1ef5-580a-4979-9cf3-50f4549a080a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Declaring the Application&#39;s ODBC Version
Before an application allocates a connection, it must set the SQL_ATTR_ODBC_VERSION environment attribute. This attribute states that the application follows the ODBC 2.*x* or ODBC 3.*x* specification when using the following items:  
  
-   **SQLSTATEs**. Many SQLSTATE values are different in ODBC 2.*x* and ODBC 3.*x*.  
  
-   **Date, Time, and Timestamp Type Identifiers**. The following table shows the type identifiers for date, time, and timestamp data in ODBC 2.*x* and ODBC 3.*x*.  
  
    |ODBC 2.*x*|ODBC 3.*x*|  
    |----------------|----------------|  
    |**SQL Type Identifiers**||  
    |SQL_DATE|SQL_TYPE_DATE|  
    |SQL_TIME|SQL_TYPE_TIME|  
    |SQL_TIMESTAMP|SQL_TYPE_TIMESTAMP|  
    |**C Type Identifiers**||  
    |SQL_C_DATE|SQL_C_TYPE_DATE|  
    |SQL_C_TIME|SQL_C_TYPE_TIME|  
    |SQL_C_TIMESTAMP|SQL_C_TYPE_TIMESTAMP|  
  
-   _CatalogName_  **Argument in SQLTables**. In ODBC 2.*x*, the wildcard characters ("%" and "_") in the *CatalogName* argument are treated literally. In ODBC 3.*x*, they are treated as wildcard characters. Thus, an application that follows the ODBC 2.*x* specification cannot use these as wildcard characters and does not escape them when using them as literals. An application that follows the ODBC 3.*x* specification can use these as wildcard characters or escape them and use them as literals. For more information, see [Arguments in Catalog Functions](../../../odbc/reference/develop-app/arguments-in-catalog-functions.md).  
  
 The ODBC 3*.x* Driver Manager and ODBC 3*.x* drivers check the version of the ODBC specification to which an application is written and respond accordingly. For example, if the application follows the ODBC 2.*x* specification and calls **SQLExecute** before calling **SQLPrepare**, the ODBC 3*.x* Driver Manager returns SQLSTATE S1010 (Function sequence error). If the application follows the ODBC 3*.x* specification, the Driver Manager returns SQLSTATE HY010 (Function sequence error). For more information, see [Backward Compatibility and Standards Compliance](../../../odbc/reference/develop-app/backward-compatibility-and-standards-compliance.md).  
  
> [!IMPORTANT]  
>  Applications that follow the ODBC 3.*x* specification must use conditional code to avoid using functionality new to ODBC 3.*x* when working with ODBC 2.*x* drivers. ODBC 2.*x* drivers do not support functionality new to ODBC 3.*x* just because the application declares that it follows the ODBC 3.*x* specification. Furthermore, ODBC 3.*x* drivers do not cease to support functionality new to ODBC 3.*x* just because the application declares that it follows the ODBC 2.*x* specification.
