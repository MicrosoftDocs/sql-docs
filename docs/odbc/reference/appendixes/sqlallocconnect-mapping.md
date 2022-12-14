---
description: "SQLAllocConnect Mapping"
title: "SQLAllocConnect Mapping | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLAllocConnect function [ODBC], mapping"
  - "mapping deprecated functions [ODBC], SQLAllocConnect"
ms.assetid: ac89dd1f-c565-47cc-8fa3-6fa5f80b5d63
author: David-Engel
ms.author: v-davidengel
---
# SQLAllocConnect Mapping
When an application calls **SQLAllocConnect** through an ODBC 3.*x* driver, the call to **SQLAllocConnect**(*henv*, *phdbc*) is mapped to **SQLAllocHandle** as follows:  
  
1.  The Driver Manager allocates a connection and returns it to the application.  
  
2.  When the application establishes a connection, the Driver Manager calls  
  
    ```  
    SQLAllocHandle(SQL_HANDLE_DBC, InputHandle, OutputHandlePtr)  
    ```  
  
     in the driver with *InputHandle* set to *henv*, and *OutputHandlePtr* set to *phdbc*.
