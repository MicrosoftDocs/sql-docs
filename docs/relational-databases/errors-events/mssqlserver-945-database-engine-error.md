---
title: "MSSQLSERVER_945 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "945 (Database Engine error)"
ms.assetid: ee501d13-0bd9-4627-896c-ed5b1bdb88b3
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_945
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|945|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DB_IS_SHUTDOWN|  
|Message Text|Database '%.*ls' cannot be opened due to inaccessible files or insufficient memory or disk space.  See the SQL Server error log for details.|  
  
## Explanation  
The database could not be accessed because files or other resources are missing.  
  
## User Action  
Check the error log for additional information about memory, disk space, or permission failure. Confirm the location of the .mdf and .ndf files for the affected database and confirm that the account used by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] has permission to access those files. After correcting the problem, restart the database by using ALTER DATABASE to set it ONLINE.  
  
