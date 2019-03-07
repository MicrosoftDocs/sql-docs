---
title: "Length of full-text catalog names restricted to 120 characters | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text catalogs names"
ms.assetid: 50633373-83f6-4ed9-99b9-71f92479a14f
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Length of full-text catalog names restricted to 120 characters
  The length of full-text catalog names is restricted to 120 characters, reduced from the 128 character restriction in previous releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Description  
 This change does not affect existing catalog names; however, scripts that create full-text catalogs with names longer than 120 characters result in an error. The catalog names are used to generate logical file names that correspond to catalogs.  
  
## Corrective Action  
 Modify all scripts that create full-text catalogs to ensure that they restrict catalog names to 120 characters.  
  
## See Also  
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)  
  
  
