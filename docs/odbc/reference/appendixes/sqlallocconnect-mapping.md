---
title: "SQLAllocConnect Mapping"
description: "SQLAllocConnect Mapping"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLAllocConnect function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLAllocConnect"
---
# SQLAllocConnect Mapping
When an application calls **SQLAllocConnect** through an ODBC 3.*x* driver, the call to **SQLAllocConnect**(*henv*, *phdbc*) is mapped to **SQLAllocHandle** as follows:  
  
1.  The Driver Manager allocates a connection and returns it to the application.  
  
2.  When the application establishes a connection, the Driver Manager calls  
  
    ```  
    SQLAllocHandle(SQL_HANDLE_DBC, InputHandle, OutputHandlePtr)  
    ```  
  
     in the driver with *InputHandle* set to *henv*, and *OutputHandlePtr* set to *phdbc*.
