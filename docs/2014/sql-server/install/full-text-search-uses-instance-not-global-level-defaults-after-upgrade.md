---
title: "Upgrading will cause Full-Text Search to use instance-level, not global, word breakers and filters by default | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "filters [Full-Text Search]"
  - "word breakers [Full-Text Search]"
ms.assetid: 93ee8fcb-d11c-49fa-8fac-51ed31a8f008
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Upgrading will cause Full-Text Search to use instance-level, not global, word breakers and filters by default
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a way to allow instance-level registration of new word breakers and filters.  
  
## Component  
 Full-Text Search  
  
## Description  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows the instance-level registration of new word breakers and filters. This instance-level registration provides functional and security isolation between instances.  
  
## Corrective Action  
 After upgrading, use the `sp_fulltext_service` to set the service property and `load_os_resources`, which allows the components to be loaded. You must run the following:  
  
 `sp_fulltext_service 'load_os_resources', 1`  
  
## See Also  
 [Working with Upgrade Advisor](../../../2014/sql-server/install/working-with-upgrade-advisor.md)  
  
  
