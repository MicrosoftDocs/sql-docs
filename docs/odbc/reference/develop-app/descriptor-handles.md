---
description: "Descriptor Handles"
title: "Descriptor Handles | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "application parameter descriptor [ODBC]"
  - "automatically allocated descriptors [ODBC]"
  - "implementation row descriptor [ODBC]"
  - "descriptor handles [ODBC]"
  - "handles [ODBC], descriptor"
  - "implementation parameter descriptor [ODBC]"
  - "apd [ODBC]"
  - "ard [ODBC]"
  - "explicitly allocated descriptors [ODBC]"
  - "ipd [ODBC]"
  - "ird [ODBC]"
  - "application row descriptor [ODBC]"
ms.assetid: 7741035c-f3e7-4c89-901e-fe528392f67d
author: David-Engel
ms.author: v-davidengel
---
# Descriptor Handles
A *descriptor* is a collection of metadata that describes the parameters of an SQL statement or the columns of a result set, as seen by the application or driver (also known as the *implementation*). Thus, a descriptor can fill any of four roles:  
  
-   **Application Parameter Descriptor (APD).** Contains information about the application buffers bound to the parameters in an SQL statement, such as their addresses, lengths, and C data types.  
  
-   **Implementation Parameter Descriptor (IPD).** Contains information about the parameters in an SQL statement, such as their SQL data types, lengths, and nullability.  
  
-   **Application Row Descriptor (ARD).** Contains information about the application buffers bound to the columns in a result set, such as their addresses, lengths, and C data types.  
  
-   **Implementation Row Descriptor (IRD).** Contains information about the columns in a result set, such as their SQL data types, lengths, and nullability.  
  
 Four descriptors (one filling each role) are allocated automatically when a statement is allocated. These are known as *automatically allocated descriptors* and are always associated with that statement. Applications can also allocate descriptors with **SQLAllocHandle**. These are known as *explicitly allocated descriptors*. They are allocated on a connection and can be associated with one or more statements on that connection to fulfill the role of an APD or ARD on those statements.  
  
 Most operations in ODBC can be performed without explicit use of descriptors by the application. However, descriptors provide a convenient shortcut for some operations. For example, suppose an application wants to insert data from two different sets of buffers. To use the first set of buffers, it would repeatedly call **SQLBindParameter** to bind them to the parameters in an **INSERT** statement and then execute the statement. To use the second set of buffers, it would repeat this process. Alternatively, it could set up bindings to the first set of buffers in one descriptor and to the second set of buffers in another descriptor. To switch between the sets of bindings, the application would simply call **SQLSetStmtAttr** and associate the correct descriptor with the statement as the APD.  
  
 For more information about descriptors, see [Types of Descriptors](../../../odbc/reference/develop-app/types-of-descriptors.md).
