---
title: "MSSQLSERVER_10737 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "10737 (Database Engine error)"
ms.assetid: 208af6ed-b271-4ab8-803e-7666025385c8
caps.latest.revision: 10
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_10737
  
## Details  
  
|||  
|-|-|  
|Product Name|MSSQLSERVER|  
|Event ID|10737|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REBUILD_PARTITION_ALL_NOT_SPECIFIED|  
|Message Text|In an ALTER TABLE REBUILD or ALTER INDEX REBUILD statement, when a partition is specified in a DATA_COMPRESSION clause, PARTITION=ALL must be specified. The PARTITION=ALL clause is used to reinforce that all partitions of the table or index will be rebuilt, even if only a subset is specified in the DATA_COMPRESSION clause.|  
  
## User Action  
Add the PARTITION=ALL clause to the ALTER TABLE or ALTER INDEX statement. Or, to rebuild a specific partition, use REBUILD PARTITION = \<partition-number-expr> WITH (DATA_COMPRESSION={ON | OFF}).  
  
