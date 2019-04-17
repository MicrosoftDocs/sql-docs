---
title: "Set Job Execution Shutdown (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "jobs [SQL Server Agent], stopping"
  - "SQL Server Agent jobs, stopping"
  - "stopping jobs"
  - "SQL Server Agent jobs, execution shutdowns"
ms.assetid: ac23e88f-53fc-41de-bb16-0c27c002d5a5
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Set Job Execution Shutdown (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to set the time that [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent will wait for executing jobs to finish before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent itself finishes in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Security](#Security)  
  
-   **To set a shutdown time for a SQL Server Agent job, using:**  
  
    [SQL Server Management Studio](#SSMSProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
By default, members of the **sysadmin** fixed server role can set the time that [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent will wait for executing jobs to finish before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent itself finishes. Other users must be granted one of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the **msdb** database:  
  
-   **SQLAgentUserRole**  
  
-   **SQLAgentReaderRole**  
  
-   **SQLAgentOperatorRole**  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To set job execution shutdown  
  
1.  In **Object Explorer,** , click the plus sign to expand the server where you want to set a job execution shutdown interval.  
  
2.  Right-click **SQL Server Agent** and select **Properties**.  
  
3.  Under **Select a page**, select **Job System**.  
  
4.  Set the **Shutdown time-out interval** in seconds to increase or decrease the shutdown time-out interval. This determines how long [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent will wait for executing jobs to finish before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent itself finishes.  
  
