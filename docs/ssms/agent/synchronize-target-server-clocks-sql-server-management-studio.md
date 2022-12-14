---
description: "Synchronize Target Server Clocks"
title: Synchronize Target Server Clocks
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, target servers"
  - "clocks [SQL Server]"
  - "master servers [SQL Server], clock synchronization"
  - "synchronization [SQL Server], target server clocks"
  - "target servers [SQL Server], clock synchronization"
ms.assetid: 4fb80502-d271-4d06-bcbc-bfbbceb5f2a2
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 01/19/2017
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016"
---

# Synchronize Target Server Clocks

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

> [!IMPORTANT]  
> On [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Managed Instance T-SQL differences from SQL Server](/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to synchronize target server clocks in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] with the master server clock by using SQL Server Management Studio or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Synchronizing these system clocks supports your job schedules.  

## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Requires membership in the **sysadmin** fixed server role.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To synchronize target server clocks  
  
1.  In **Object Explorer,** click the plus sign to expand the server where you want to synchronize the target server clocks with the master server clock.  
  
2.  Right-click **SQL Server Agent**, point to **Multi Server Administration**, and select **Manage Target Servers**.  
  
3.  In the **Manage Target Servers** dialog box, click **Post Instructions**.  
  
4.  In the **Instruction type** list, select **Synchronize clocks**.  
  
5.  Under **Recipients**, do one of the following:  
  
    -   Click **All target servers** to synchronize all target server clocks with the master server clock.  
  
    -   Click **These target servers** to synchronize certain server clocks, and then select each target server whose clock you want to synchronize with the master server clock.  
  
6.  When finished, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To synchronize target server clocks  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE msdb ;  
    GO  
    -- resynchronizes the SEATTLE1 target server  
    EXEC dbo.sp_resync_targetserver  
        N'SEATTLE1' ;  
    GO  
    ```  
  
For more information, see [sp_resync_targetserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-resync-targetserver-transact-sql.md).  
