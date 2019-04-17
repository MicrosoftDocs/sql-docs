---
title: "View Information About an Alert | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, alerts"
  - "viewing alerts"
  - "alerts [SQL Server], viewing"
  - "displaying alerts"
  - "status information [SQL Server], alerts"
ms.assetid: a0e3a8c4-e3c2-42a5-b2f8-aa06061d3fa6
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# View Information About an Alert
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to view information about [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Security](#Security)  
  
-   **To view information about an alert, using:**  
  
    [SQL Server Management Studio](#SSMSProcedure)  
  
    [Transact-SQL](#TsqlProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
By default, members of the **sysadmin** fixed server role can view information about an alert. Other users must be granted the **SQLAgentOperatorRole** fixed database role in the **msdb** database.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To view information about an alert  
  
1.  In **Object Explorer,** click the plus sign to expand the server where you want to view information about an alert.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Alerts** folder.  
  
4.  Right-click the alert that has the information you want to view and select **Properties**.  
  
    For more information on the available options contained in the _alert\_name_**alert properties** dialog box, see:  
  
    -   [Alert Properties - New Alert &#40;General Page&#41;](../../ssms/agent/alert-properties-new-alert-general-page.md)  
  
    -   [Alert Properties - New Alert &#40;Response Page&#41;](../../ssms/agent/alert-properties-new-alert-response-page.md)  
  
    -   [Alert Properties - New Alert &#40;Options Page&#41;](../../ssms/agent/alert-properties-new-alert-options-page.md)  
  
    -   [Alert Properties &#40;History Page&#41;](../../ssms/agent/alert-properties-history-page.md)  
  
5.  When finished, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To view information about an alert  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- reports information about the Demo: Sev. 25 Errors alert  
    -- This example assumes that the 'Demo: Sev. 25 Errors' alert exists.  
    USE msdb ;  
    GO  
  
    EXEC sp_help_alert @alert_name = 'Demo: Sev. 25 Errors'  
    GO  
    ```  
  
For more information, see [sp_help_alert (Transact-SQL)](https://msdn.microsoft.com/850cef4e-6348-4439-8e79-fd1bca712091).  
  
