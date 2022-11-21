---
description: "Descriptors"
title: "Descriptors | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "descriptors [ODBC]"
  - "descriptors [ODBC], about descriptors"
  - "descriptor handles [ODBC]"
  - "handles [ODBC], descriptor"
ms.assetid: ef2cbb93-cd00-40f8-b1d2-5f5723a991aa
author: David-Engel
ms.author: v-davidengel
---
# Descriptors
A descriptor handle refers to a data structure that holds information about either columns or dynamic parameters.  
  
 ODBC functions that operate on column and parameter data implicitly set and retrieve descriptor fields. For instance, when **SQLBindCol** is called to bind column data, it sets descriptor fields that completely describe the binding. When **SQLColAttribute** is called to describe column data, it returns data stored in descriptor fields.  
  
 An application calling ODBC functions need not concern itself with descriptors. No database operation requires that the application gain direct access to descriptors. However, for some applications, gaining direct access to descriptors streamlines many operations. For example, direct access to descriptors provides a way to rebind column data, which can be more efficient than calling **SQLBindCol** again.  
  
> [!NOTE]  
>  The physical representation of the descriptor is not defined. Applications gain direct access to a descriptor only by manipulating its fields by calling ODBC functions with the descriptor handle.  
  
 This section contains the following topics.  
  
-   [Types of Descriptors](../../../odbc/reference/develop-app/types-of-descriptors.md)  
  
-   [Descriptor Fields](../../../odbc/reference/develop-app/descriptor-fields.md)  
  
-   [Allocating and Freeing Descriptors](../../../odbc/reference/develop-app/allocating-and-freeing-descriptors.md)  
  
-   [Getting and Setting Descriptor Fields](../../../odbc/reference/develop-app/getting-and-setting-descriptor-fields.md)
