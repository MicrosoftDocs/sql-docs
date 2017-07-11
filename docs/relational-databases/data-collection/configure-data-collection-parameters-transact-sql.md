---
title: "Configure Data Collection Parameters (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "data collection [SQL Server]"
ms.assetid: 850905b6-35d2-4ed1-ab51-de64daa832b2
caps.latest.revision: 16
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Configure Data Collection Parameters (Transact-SQL)
  Before you create a custom collection set, you must first configure data collection parameters. You can do this by using the stored procedures that are provided with the data collector. Accomplishing this task involves using Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to carry out the following procedure.  
  
> [!NOTE]  
>  You only configure data collection parameters once. After configuration, these parameters are used for any additional collection sets that you create.  
  
### Configure data collection parameters  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the database where you want to create a custom collection set.  
  
2.  In Query Editor, issue the following statements.  
  
    ```tsql  
    USE msdb;  
    EXEC sp_syscollector_set_warehouse_instance_name N'INSTANCE_NAME';-- where instance name is the name of the SQL Server instance  
    EXEC sp_syscollector_set_warehouse_database_name N'MDW';  
    EXEC sp_syscollector_set_cache_directory N'D:\tempdata';  
    ```  
  
## See Also  
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)  
  
  