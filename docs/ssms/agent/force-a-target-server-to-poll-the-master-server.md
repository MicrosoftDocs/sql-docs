---
title: "Force a Target Server to Poll the Master Server | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "forcing master server polling"
  - "polling master servers [SQL Server]"
  - "master servers [SQL Server], polling"
  - "target servers [SQL Server], polling the master server"
ms.assetid: f1189a47-5ac3-45e2-9c5f-847810672279
author: "markingmyname"
ms.author: "maghan"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Force a Target Server to Poll the Master Server
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to force a target server to poll the master server. The target server must be a registered server on the master server.  
  
A job is a specified series of actions that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent performs. A multiserver job is a job that a master server runs on one or more target servers. Each target server can run one instance of the same job at the same time. Each target server periodically polls the master server, downloads a copy of any new jobs assigned to the target server, and then disconnects. The target server runs the job locally and then reconnects to the master server to upload the job outcome status.  
  
> [!NOTE]  
> If the master server is inaccessible when the target server tries to upload job status, the job status is spooled until the master server can be accessed.  
  
-   **Before you begin:**  [Limitations and Restrictions](#Restrictions), [Security](#Security)  
  
-   **To force a target server to poll the master server, using:** [SQL Server Management Studio](#SSMS)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
The target server must be a registered server on the master server. You must run the instructions given in this topic from the master server.  
  
### <a name="Security"></a>Security  
For detailed information, see [Implement SQL Server Agent Security](../../ssms/agent/implement-sql-server-agent-security.md) and [Choose the Right SQL Server Agent Service Account for Multiserver Environments](../../ssms/agent/choose-the-right-sql-server-agent-service-account-for-multiserver-environments.md).  
  
## <a name="SSMS"></a>Using SQL Server Management Studio  
**To force a target server to poll the master server**  
  
1.  In **Object Explorer**, expand the master server.  
  
2.  Right-click **SQL Server Agent**, point to **Multi Server Administration**, and then click **Manage Target Servers**.  
  
3.  Click a target server, and then click **Force Poll**.  
  
