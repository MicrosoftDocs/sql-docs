---
title: "Tune Automated Administration Across an Enterprise | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "performance [SQL Server], automated administration"
  - "tuning automated administration [SQL Server]"
  - "monitoring performance [SQL Server], automated administration"
ms.assetid: 671fed35-3859-4b0d-8f38-4dd07436857e
author: "markingmyname"
ms.author: "maghan"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Tune Automated Administration Across an Enterprise
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

Multiserver administration with Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent takes advantage of the self-tuning features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore, under normal conditions, additional job tuning is not necessary. However, network loads increase when you run jobs, generate alerts, and notify operators. You can tune automated administration across an enterprise to minimize the network traffic these activities cause.  
  
## See Also  
[Monitoring Performance of the Data Flow Engine](../../integration-services/performance/performance-counters.md)  
  
