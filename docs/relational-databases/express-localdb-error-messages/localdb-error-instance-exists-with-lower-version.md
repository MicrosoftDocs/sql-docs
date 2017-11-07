---
title: "LOCALDB_ERROR_INSTANCE_EXISTS_WITH_LOWER_VERSION | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: a7c5ce08-8841-49a3-b252-116807ba469a
caps.latest.revision: 8
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# LOCALDB_ERROR_INSTANCE_EXISTS_WITH_LOWER_VERSION
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|258|  
|Event Source|SQL Server Local Database Runtime 12.0|  
|Component|Local Database Runtime API|  
|Message Text|Unable to create the Local Database instance with specified version. An instance with the same name already exists, but it has lower version than the specified version.|  
  
## Explanation  
 The specified instance already exists but its version is lower than requested.  
  
## User Action  
 Delete the existing instance and retry the operation.  
  
  