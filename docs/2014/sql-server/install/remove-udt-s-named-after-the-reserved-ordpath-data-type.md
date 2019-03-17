---
title: "Remove UDT&#39;s named after the reserved ORDPATH data type | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 474e910a-6abb-4e28-acc2-055338c011d4
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Remove UDT&#39;s named after the reserved ORDPATH data type
  Upgrade Advisor detected a user-defined type (UDT) that is named after a term reserved for the `ORDPATH` data type.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 The terms used for built-in data types should not be used as names for either common language runtime (CLR) or alias UDTs.  
  
## Corrective Action  
 Remove the UDT that is named after the data type and recreate the UDT with an unreserved name.  
  
## External Resources  
 [Creating a User-Defined Type](../../relational-databases/clr-integration-database-objects-user-defined-types/creating-user-defined-types.md)  
  
  
