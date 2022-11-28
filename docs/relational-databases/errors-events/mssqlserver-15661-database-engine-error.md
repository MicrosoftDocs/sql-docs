---
description: "MSSQLSERVER_15661"
title: "MSSQLSERVER_15661 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "15661 (Database Engine error)"
ms.assetid: 88b01bfb-74ce-4aa0-aec0-7885261c7ef3
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_15661
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
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
  
