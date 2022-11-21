---
description: "Transferring Data in Its Binary Form"
title: "Transferring Data in Its Binary Form | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "data types [ODBC], transferring in binary form"
  - "transferring data in binary form [ODBC]"
  - "binary data transfers [ODBC]"
ms.assetid: 4b12a9de-51d0-416a-87f4-9bf84959cad9
author: David-Engel
ms.author: v-davidengel
---
# Transferring Data in Its Binary Form
An application can safely transfer data (in the internal form used by a specified DBMS) between two data sources that use the same DBMS and hardware platform. For a given piece of data, the SQL data types must be the same in the source and target data sources. The C data type is SQL_C_BINARY.  
  
 When the application calls **SQLFetch**, **SQLFetchScroll**, or **SQLGetData** to retrieve the data from the source data source, the driver retrieves the data from the data source and transfers it, without conversion, to a storage location of type SQL_C_BINARY. When the application calls **SQLBulkOperations**, **SQLExecute**, **SQLExecDirect**, **SQLPutData, or SQLSetPos** to send the data to the target data source, the driver retrieves the data from the storage location and transfers it, without conversion, to the target data source.  
  
> [!NOTE]  
>  Applications that transfer any data (except binary data) in this manner are not interoperable among DBMSs.  
  
 **SQLCopyDesc** can be used to copy row bindings from the source DBMS to parameter bindings in the target DBMS.
