---
description: "Types of Changes"
title: "Types of Changes | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility [ODBC], types of changes"
  - "backward compatibility [ODBC], types of changes"
ms.assetid: 6a7db81a-20aa-4915-aed8-429711a36f49
author: David-Engel
ms.author: v-davidengel
---
# Types of Changes
Three types of changes are made in ODBC *3.x* (and any version of ODBC). Each of these affects backward compatibility differently and is handled in a different way. These changes are described in the following table.  
  
|Type of change|Description|  
|--------------------|-----------------|  
|New features|These are features that are new to ODBC *3.x*, such as out-of-line binding or descriptors. These are implemented only when the application and driver, as well as the Driver Manager, are of version *3.x*, so there is no attempt to make these backward compatible.|  
|Duplicated features|These are features that exist in ODBC *2.x* and ODBC *3.x* but are implemented in different ways in each. The functions **SQLAllocHandle** and **SQLAllocStmt** are an example. Backward compatibility issues for these and other duplicated features are mostly handled by mappings in the Driver Manager.|  
|Behavioral changes|These are features that are handled differently in ODBC *2.x* and ODBC *3.x*. A datetime **#define** is an example. These features are handled by the ODBC *3.x* driver based on an environment attribute setting. (See [Behavioral Changes](../../../odbc/reference/develop-app/behavioral-changes.md) for more information.)|
