---
description: "Default C Data Types"
title: "Default C Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "data types [ODBC], pseudo-type identifiers"
  - "pseudo-type identifiers [ODBC], about pseudo-type identifiers"
  - "pseudo-type identifiers [ODBC]"
ms.assetid: 229140ae-af8f-4ec8-9ccf-1e92360e0bac
author: David-Engel
ms.author: v-davidengel
---
# Default C Data Types
If an application specifies SQL_C_DEFAULT in **SQLBindCol**, **SQLGetData**, or **SQLBindParameter**, the driver assumes that the C data type of the output or input buffer corresponds to the SQL data type of the column or parameter to which the buffer is bound.  
  
> [!IMPORTANT]  
>  Interoperable applications should not use SQL_C_DEFAULT. Instead, they should always specify the C type of the buffer they are using. This is because drivers cannot always correctly determine the default C type, for the following reasons:  
  
-   If the DBMS promotes an SQL data type of a column or parameter, the driver cannot determine the original SQL data type of a column or parameter. Therefore, it cannot determine the corresponding default C data type.  
  
-   If the driver cannot determine whether a particular column or parameter is signed, as is often the case when this is handled by the DBMS, the driver cannot determine whether the corresponding default C data type should be signed or unsigned.  
  
     Because SQL_C_DEFAULT is provided only as a programming convenience, the application does not lose any functionality when it specifies the actual C data type.  
  
 A table showing the default C data type for each SQL data type is included in [Converting Data from SQL to C Data Types](../../../odbc/reference/appendixes/converting-data-from-sql-to-c-data-types.md), later in this appendix.
