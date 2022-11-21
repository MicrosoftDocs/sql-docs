---
description: "Types of Descriptors"
title: "Types of Descriptors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "descriptors [ODBC], types"
ms.assetid: ec20e446-e540-41ad-8559-d9c0a5b8358f
author: David-Engel
ms.author: v-davidengel
---
# Types of Descriptors
A descriptor is used to describe one of the following:  
  
-   A set of zero or more parameters. A parameter descriptor can be used to describe:  
  
    -   The *application parameter buffer,* which contains either the input dynamic arguments as set by the application or the output dynamic arguments following the execution of a **CALL** statement of SQL.  
  
    -   The *implementation parameter buffer*. For input dynamic arguments, this contains the same arguments as the application parameter buffer, after any data conversion the application may specify. For output dynamic arguments, this contains the returned arguments, before any data conversion that the application may specify.  
  
     For input dynamic arguments, the application must operate on an application parameter descriptor before executing any SQL statement that contains dynamic parameter markers. For both input and output dynamic arguments, the application can specify different data types from those in the implementation parameter descriptor to achieve data conversion.  
  
-   A single row of database data. A row descriptor can be used to describe:  
  
    -   The *implementation row buffer,* which contains the row from the database. (These buffers conceptually contain data as written to or read from the database. However, the stored form of database data is not specified. A database could perform additional conversion on the data from its form in the implementation buffer.)  
  
    -   The *application row buffer,* which contains the row of data as presented to the application, following any data conversion that the application may specify.  
  
     The application operates on the application row descriptor in any case where column data from the database must appear in application variables. To achieve data conversion of column data, the application can specify different data types from those in the implementation row descriptor.  
  
 The descriptor types are summarized in the following table.  
  
|Buffer type|Rows|Dynamic parameters|  
|-----------------|----------|------------------------|  
|**Application buffer**|Application row descriptor (ARD)|Application parameter descriptor (APD)|  
|**Implementation buffer**|Implementation row descriptor (IRD)|Implementation parameter descriptor (IPD)|  
  
 For either the parameter or the row buffers, if the application specifies different data types in corresponding records of the implementation and application descriptors, the driver performs data conversion when it uses the descriptors. For example, it may convert numeric and datetime values to character-string format. (For valid conversions, see [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md).)  
  
 A descriptor can perform different roles. Different statements can share any descriptor that the application explicitly allocates. A row descriptor in one statement can serve as a parameter descriptor in another statement.  
  
 It is always known whether a given descriptor is an application descriptor or an implementation descriptor, even if the descriptor has not yet been used in a database operation. For the descriptors that the implementation implicitly allocates, the implementation records the predefined row relative to the statement handle. Any descriptor that the application allocates by calling **SQLAllocHandle** is an application descriptor.
