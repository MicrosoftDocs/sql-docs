---
title: "MSSQLSERVER_15661 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "15661 (Database Engine error)"
ms.assetid: 88b01bfb-74ce-4aa0-aec0-7885261c7ef3
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_15661
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|15661|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum15661|  
|Message Text|The sp_estimate_data_compression_savings stored procedure cannot be used for temporary tables.|  
  
## Explanation  
 A temporary table was used as an argument for the sp_estimate_data_compression_savings stored procedure. Although the compression of temporary tables is supported, you cannot use sp_estimate_data_compression_savings to estimate the compression savings.  
  
## User Action  
 Remove the temporary table as an argument for sp_estimate_data_compression_savings.  
  
  
