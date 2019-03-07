---
title: "Remove the cdc schema if you plan to enable change data capture | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "cdc schema"
  - "change data capture"
ms.assetid: 6a84aa25-0f31-4be3-b2dd-4f249b8254ae
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Remove the cdc schema if you plan to enable change data capture
  A database already contains a cdc schema. If you plan to enable change data capture after upgrade, you must first drop this cdc schema. When you enable a database for change data capture, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will create a new schema named cdc.  
  
  
