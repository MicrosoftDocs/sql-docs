---
title: "Job Categories - Manage Job Categories | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.ag.job.categories.f1"
helpviewer_keywords: 
  - "Job Categories dialog box"
ms.assetid: 38276438-40b1-43ce-9aae-6805be6d9332
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Job Categories - Manage Job Categories
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Use the **Job Categories** dialog box to add or delete job categories. Built-in job categories cannot be deleted.  
  
## Options  
**Name**  
The name of the job category.  
  
**Number of jobs in category**  
The number of jobs defined for this category.  
  
**View Jobs**  
Opens the **Properties** dialog box for the selected category, to list all jobs currently defined for that category.  
  
**Add**  
Opens the **New Job Category** dialog box, to add a new job category  
  
**Delete**  
Removes the selected job category. Only enabled for user-defined job categories.  
  
**Refresh**  
Queries the server for the current information.  
  
#### To access the Job Categories dialog box  
  
1.  In **Object Explorer**, expand **SQL Server Agent**, right-click **Jobs**, and then click **Manage Job Categories**.  
  
