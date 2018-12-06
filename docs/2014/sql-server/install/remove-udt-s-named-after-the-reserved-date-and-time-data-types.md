---
title: "Remove UDT&#39;s named after the reserved DATE and TIME data types | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "time data type [SQL Server], UDTs"
  - "date data type [SQL Server], UDTs"
ms.assetid: 48f109af-b1d1-4f03-a7e3-8a0b05ed94e8
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Remove UDT&#39;s named after the reserved DATE and TIME data types
  Upgrade Advisor detected a user-defined type (UDT) that is named after a term reserved for either the `date` or the `time` data types.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 The terms used for data types should not be used as names for either common language runtime (CLR) or alias UDTs.  
  
## Corrective Action  
 Remove the UDT that is named after the data type and recreate the UDT with an unreserved name.  
  
  
