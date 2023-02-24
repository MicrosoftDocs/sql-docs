---
description: "Parameter Binding Offsets"
title: "Parameter Binding Offsets | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "offsets of parameters [ODBC]"
  - "binding offsets of parameters [ODBC]"
ms.assetid: 309339e9-9ccd-4a58-8aa4-b6dc88f4eb7c
author: David-Engel
ms.author: v-davidengel
---
# Parameter Binding Offsets
An application can specify that an offset is added to bound parameter buffer addresses and the corresponding length/indicator buffer addresses when **SQLExecDirect** or **SQLExecute** is called. The result of these additions determines the addresses used in these operations.  
  
 Bind offsets allow an application to change bindings without calling **SQLBindParameter** for previously bound parameters. A call to **SQLBindParameter** to rebind a parameter changes the buffer address and the length/indicator pointer. Rebinding with an offset, on the other hand, simply adds an offset to the existing bound parameter buffer address and length/indicator buffer address. When offsets are used, the bindings are a "template" of how the application buffers are laid out and the application can move this "template" to different areas of memory by changing the offset. A new offset can be specified at any time and is always added to the originally bound values.  
  
 To specify a bind offset, the application sets the SQL_ATTR_PARAM_BIND_OFFSET_PTR statement attribute to the address of an SQLINTEGER buffer. Before the application calls a function that uses the bindings, it places an offset in bytes in this buffer, as long as neither the parameter buffer address nor the length/indicator buffer address is 0, and the bound parameter is in the SQL statement. The sum of the address and the offset must be a valid address. (This means that either or both the offset and the address to which the offset is added can be invalid, as long as their sum is a valid address.)  
  
> [!NOTE]  
>  Binding offsets are not supported by ODBC 2.*x* drivers.
