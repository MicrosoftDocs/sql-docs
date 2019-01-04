---
title: "Set the Max Degree of Parallelism Option for Optimal Performance | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: ec908006-67ae-4674-9a61-25ea741d6197
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Set the Max Degree of Parallelism Option for Optimal Performance
  This rule determines whether the max degree of parallelism (MAXDOP) option for a value greater than 8. Setting this option to a larger value often causes unwanted resource consumption and performance degradation.  
  
## Best Practices Recommendations  
 Set the max degree of parallelism option to 8 or less by using sp_configure.  
  
## For More Information  
 [Microsoft Knowledge Base article 329204](https://go.microsoft.com/fwlink/?linkid=117786)  
  
 [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md)  
  
 [sp_configure &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-configure-transact-sql)  
  
  
