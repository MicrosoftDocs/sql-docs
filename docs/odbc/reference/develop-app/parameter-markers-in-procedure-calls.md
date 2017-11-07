---
title: "Parameter Markers in Procedure Calls | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SQL statements [ODBC], interoperability"
  - "parameter markers [ODBC]"
  - "interoperability of SQL statements [ODBC], parameter markers"
ms.assetid: cda56f2b-6eec-4cbc-8dbb-36d8fa9f9216
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Parameter Markers in Procedure Calls
When calling procedures that accept parameters, interoperable applications should use parameter markers instead of literal parameter values. Some data sources do not support the use of literal parameter values in procedure calls. For more information about parameters, see [Statement Parameters](../../../odbc/reference/develop-app/statement-parameters.md). For more information about calling procedures, see [Procedure Calls](../../../odbc/reference/develop-app/procedure-calls.md), later in this section.