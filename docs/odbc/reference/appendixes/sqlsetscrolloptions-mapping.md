---
description: "SQLSetScrollOptions Mapping"
title: "SQLSetScrollOptions Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLSetScrollOptions function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLSetScrollOptions"
ms.assetid: a0fa4510-8891-4a61-a867-b2555bc35f05
author: David-Engel
ms.author: v-davidengel
---
# SQLSetScrollOptions Mapping
When an application calls **SQLSetScrollOptions** through an ODBC *3.x* driver and the driver does not support **SQLSetScrollOptions**, the call to  
  
```  
SQLSetScrollOptions(StatementHandle, Concurrency, KeysetSize, RowsetSize)  
```  
  
 will result as follows:  
  
-   A call to  
  
    ```  
    SQLGetInfo(ConnectionHandle, InfoType, InfoValuePtr, BufferLength, StringLengthPtr)  
    ```  
  
     with the *InfoType* argument set to one of the values in the following table, depending on the value of the *KeysetSize* argument in **SQLSetScrollOptions**.  
  
    |*KeysetSize argument*|*InfoType argument*|  
    |---------------------------|-------------------------|  
    |SQL_SCROLL_FORWARD_ONLY|SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES2|  
    |SQL_SCROLL_STATIC|SQL_STATIC_CURSOR_ATTRIBUTES2|  
    |SQL_SCROLL_KEYSET_DRIVEN|SQL_KEYSET_CURSOR_ATTRIBUTES2|  
    |SQL_SCROLL_DYNAMIC|SQL_DYNAMIC_CURSOR_ATTRIBUTES2|  
    |A value greater than the *RowsetSize* argument|SQL_KEYSET_CURSOR_ATTRIBUTES2|  
  
     If the value of the *KeysetSize* argument is not listed in the preceding table, the call to **SQLSetScrollOptions** returns SQLSTATE S1107 (Row value out of range) and none of the following steps are performed.  
  
     The Driver Manager then verifies whether the appropriate bit is set in the **InfoValuePtr* value returned by the call to **SQLGetInfo**, according to the value of the *Concurrency* argument in **SQLSetScrollOptions**.  
  
    |*Concurrency* argument|*InfoType* setting|  
    |----------------------------|------------------------|  
    |SQL_CONCUR_READ_ONLY|SQL_CA2_READ_ONLY_CONCURRENCY|  
    |SQL_CONCUR_LOCK|SQL_CA2_LOCK_CONCURRENCY|  
    |SQL_CONCUR_ROWVER|SQL_CA2_ROWVER_CONCURRENCY|  
    |SQL_CONCUR_VALUES|SQL_CA2_VALUES_CONCURRENCY|  
  
     If the *Concurrency* argument is not one of the values in the preceding table, the call to **SQLSetScrollOptions** returns SQLSTATE S1108 (Concurrency option out of range) and none of the following steps are performed. If the appropriate bit (as indicated in the preceding table) is not set in **InfoValuePtr* to one of the values corresponding to the *Concurrency* argument, the call to **SQLSetScrollOptions** returns SQLSTATE S1C00 (Driver not capable) and none of the following steps are performed.  
  
-   A call to  
  
    ```  
    SQLSetStmtAttr(StatementHandle, SQL_ATTR_CURSOR_TYPE, ValuePtr, 0)  
    ```  
  
     with *\*ValuePtr* set to one of the values in the following table, according to the value of the *KeysetSize* argument in **SQLSetScrollOptions**.  
  
    |*KeysetSize* argument|*\*ValuePtr*|  
    |---------------------------|------------------|  
    |SQL_SCROLL_FORWARD_ONLY|SQL_CURSOR_FORWARD_ONLY|  
    |SQL_SCROLL_STATIC|SQL_CURSOR_STATIC|  
    |SQL_SCROLL_KEYSET_DRIVEN|SQL_CURSOR_KEYSET_DRIVEN|  
    |SQL_SCROLL_DYNAMIC|SQL_CURSOR_DYNAMIC|  
    |A value greater than the *RowsetSize* argument|SQL_CURSOR_KEYSET_DRIVEN|  
  
-   A call to  
  
    ```  
    SQLSetStmtAttr(StatementHandle, SQL_ATTR_CONCURRENCY, ValuePtr, 0)  
    ```  
  
     with *\*ValuePtr* set to the *Concurrency* argument in **SQLSetScrollOptions**.  
  
-   If the *KeysetSize* argument in the call to **SQLSetScrollOptions** is positive, a call to  
  
    ```  
    SQLSetStmtAttr(StatementHandle, SQL_ATTR_KEYSET_SIZE, ValuePtr, 0)  
    ```  
  
     with *\*ValuePtr* set to the *KeysetSize* argument in **SQLSetScrollOptions**.  
  
-   A call to  
  
    ```  
    SQLSetStmtAttr(StatementHandle, SQL_ROWSET_SIZE, ValuePtr, 0)  
    ```  
  
     with *\*ValuePtr* set to the *RowsetSize* argument in **SQLSetScrollOptions**.  
  
    > [!NOTE]  
    >  When the Driver Manager maps **SQLSetScrollOptions** for an application working with an ODBC *3.x* driver that does not support **SQLSetScrollOptions**, the Driver Manager sets the SQL_ROWSET_SIZE statement option, not the SQL_ATTR_ROW_ARRAY_SIZE statement attribute, to the *RowsetSize* argument in **SQLSetScrollOption**. As a result, **SQLSetScrollOptions** cannot be used by an application when fetching multiple rows by a call to **SQLFetch** or **SQLFetchScroll**. It can be used only when fetching multiple rows by a call to **SQLExtendedFetch**.
