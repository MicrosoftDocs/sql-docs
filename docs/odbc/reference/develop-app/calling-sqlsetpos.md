---
description: "Calling SQLSetPos"
title: "Calling SQLSetPos | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility [ODBC], SQLSetPos"
  - "SQLSetPos function [ODBC], calling"
  - "upgrading applications [ODBC], SQLSetPos"
  - "backward compatibility [ODBC], SqlSetPos"
  - "application upgrades [ODBC], SQLSetPos"
ms.assetid: 846354b8-966c-4c2c-b32f-b0c8e649cedd
author: David-Engel
ms.author: v-davidengel
---
# Calling SQLSetPos
In ODBC *2.x*, the pointer to the row status array was an argument to **SQLExtendedFetch**. The row status array was later updated by a call to **SQLSetPos**. Some drivers have relied on the fact that this array does not change between **SQLExtendedFetch** and **SQLSetPos**. In ODBC *3.x*, the pointer to the status array is a descriptor field and therefore the application can easily change it to point to a different array. This can be a problem when an ODBC *3.x* application is working with an ODBC *2.x* driver but is calling **SQLSetStmtAttr** to set the array status pointer and is calling **SQLFetchScroll** to fetch data. The Driver Manager maps it as a sequence of calls to **SQLExtendedFetch**. In the following code, an error would normally be raised when the Driver Manager maps the second **SQLSetStmtAttr** call when working with an ODBC *2.x* driver:  
  
```  
SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_STATUS_PTR, rgfRowStatus, 0);  
SQLFetchScroll(hstmt, fFetchType, iRow);  
SQLSetStmtAttr(hstmt, SQL_ATTR_ROW_STATUS_PTR, rgfRowStat1, 0);  
SQLSetPos(hstmt, iRow, fOption, fLock);  
```  
  
 The error would be raised if there were no way to change the row status pointer in ODBC *2.x* between calls to **SQLExtendedFetch**. Instead, the Driver Manager performs the following steps when working with an ODBC *2.x* driver:  
  
1.  Initializes an internal Driver Manager flag *fSetPosError* to TRUE.  
  
2.  When an application calls **SQLFetchScroll**, the Driver Manager sets *fSetPosError* to FALSE.  
  
3.  When the application calls **SQLSetStmtAttr** to set SQL_ATTR_ROW_STATUS_PTR, the Driver Manager sets *fSetPosError* equal toTRUE.  
  
4.  When the application calls **SQLSetPos**, with *fSetPosError* equal to TRUE, the Driver Manager raises SQL_ERROR with SQLSTATE HY011 (Attribute cannot be set now) to indicate that the application attempted to call **SQLSetPos** after changing the row status pointer but prior to calling **SQLFetchScroll**.
