---
title: "sys.database_usage (Azure SQL Database) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "database_usage"
  - "database_usage_TSQL"
  - "sys.database_usage_TSQL"
  - "sys.database_usage"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database_usage"
  - "sys.database_usage"
ms.assetid: be6820de-60bf-4ddd-ace7-4077893d630f
caps.latest.revision: 13
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.database_usage (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  **Note: This applies only to Azure SQL Database V11.**  
  
 Lists the number, type, and duration of databases on the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server.  
  
 The **sys.database_usage** view contains the following columns.  
  
|Column Name|Description|  
|-----------------|-----------------|  
|time|The date when the usage events occurred.|  
|sku|The type of service tier for the database: **Web**, **Business**, **Basic**, **Standard**, **Premium**|  
|quantity|The maximum number of databases of an SKU type that existed during that day.|  
  
## Permissions  
 Read-only access to this view is available to all users with permissions to connect to the **master** database.  
  
## Remarks  
 The **sys.database_usage** view returns one row for each day of your subscription.  
  
## See Also  
 [SQL Database Pricing Details](http://go.microsoft.com/fwlink/?LinkID=394978)   
 [Accounts and Billing in Windows Azure SQL Database](http://msdn.microsoft.com/library/windowsazure/ee621788.aspx)  
  
  