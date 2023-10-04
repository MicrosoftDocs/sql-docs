---
title: Create a maintenance plan
description: Learn how to create a single server or multiserver maintenance plan in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 03/27/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "maintenance plans [SQL Server], creating"
---
# Create a maintenance plan

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to create a single server or multiserver maintenance plan in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you can create maintenance plans in one of two ways: by either using the Maintenance Plan Wizard or the design surface. The Wizard is best for creating basic maintenance plans, while creating a plan using the design surface allows you to utilize enhanced workflow.

## Limitations and restrictions

To create a multiserver maintenance plan, a multiserver environment containing one master server and one or more target servers must be configured. Multiserver maintenance plans must be created and maintained on the master server. These plans can be viewed, but not maintained, on target servers.

## Prerequisites

The [Agent XPs Server Configuration Option](../../database-engine/configure-windows/agent-xps-server-configuration-option.md) must be enabled.

## Permissions

To create or manage maintenance plans, you must be a member of the **sysadmin** fixed server role.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

#### Create a maintenance plan using the Maintenance Plan Wizard

1. In Object Explorer, select the plus sign to expand the server where you want to create a maintenance plan.

1. Select the plus sign to expand the **Management** folder.

1. Right-click the **Maintenance Plans** folder and select **Maintenance Plan Wizard**.

1. Follow the steps of the wizard to create a maintenance plan. For more information, see [Use the Maintenance Plan Wizard](use-the-maintenance-plan-wizard.md).

#### Create a maintenance plan using the design surface

1. In Object Explorer, select the plus sign to expand the server where you want to create a maintenance plan.

1. Select the plus sign to expand the **Management** folder.

1. Right-click the **Maintenance Plans** folder and select **New Maintenance Plan**.

1. Create a maintenance plan following the steps in [Create a Maintenance Plan (Maintenance Plan Design Surface)](create-a-maintenance-plan-maintenance-plan-design-surface.md).

## <a id="TsqlProcedure"></a> Use Transact-SQL

#### Create a maintenance plan

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   In this example, the code creates a daily SQL Agent job that runs at 23:30 (11:30 p.m.), which reorganizes all the indexes on the `HumanResources.Employee` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

   ```sql
   USE [msdb];
   GO
   --  Adds a new job, executed by the SQL Server Agent service, called "HistoryCleanupTask_1".
   EXEC [dbo].[sp_add_job] @job_name = N'HistoryCleanupTask_1',
                           @enabled = 1,
                           @description = N'Clean up old task history';
   GO
   -- Adds a job step for reorganizing all of the indexes in the HumanResources.Employee table to the HistoryCleanupTask_1 job.
   EXEC [dbo].[sp_add_jobstep] @job_name = N'HistoryCleanupTask_1',
                               @step_name = N'Reorganize all indexes on HumanResources.Employee table',
                               @subsystem = N'TSQL',
                               @command = N'USE [AdventureWorks2022];
   GO
   ALTER INDEX [AK_Employee_LoginID]
   ON [HumanResources].[Employee]
   REORGANIZE
   WITH (LOB_COMPACTION = ON);
   GO
   USE [AdventureWorks2022];
   GO
   ALTER INDEX [AK_Employee_NationalIDNumber]
   ON [HumanResources].[Employee]
   REORGANIZE
   WITH (LOB_COMPACTION = ON);
   GO
   USE [AdventureWorks2022];
   GO
   ALTER INDEX [AK_Employee_rowguid]
   ON [HumanResources].[Employee]
   REORGANIZE
   WITH (LOB_COMPACTION = ON);
   GO
   USE [AdventureWorks2022];
   GO
   ALTER INDEX [IX_Employee_OrganizationLevel_OrganizationNode]
   ON [HumanResources].[Employee]
   REORGANIZE
   WITH (LOB_COMPACTION = ON);
   GO
   USE [AdventureWorks2022];
   GO
   ALTER INDEX [IX_Employee_OrganizationNode]
   ON [HumanResources].[Employee]
   REORGANIZE
   WITH (LOB_COMPACTION = ON);
   GO
   USE [AdventureWorks2022];
   GO
   ALTER INDEX [PK_Employee_BusinessEntityID]
   ON [HumanResources].[Employee]
   REORGANIZE
   WITH (LOB_COMPACTION = ON);
   GO',
                               @retry_attempts = 5,
                               @retry_interval = 5;
   GO
   -- Creates a schedule named RunOnce that executes every day when the time on the server is 23:30.
   EXEC [dbo].[sp_add_schedule] @schedule_name = N'RunOnce',
                                @freq_type = 4,
                                @freq_interval = 1,
                                @active_start_time = 233000;
   GO
   -- Attaches the RunOnce schedule to the job HistoryCleanupTask_1.
   EXEC [dbo].[sp_attach_schedule] @job_name = N'HistoryCleanupTask_1',
                                   @schedule_name = N'RunOnce';
   GO
   ```

## Next steps

- [sp_add_job (Transact-SQL)](../system-stored-procedures/sp-add-job-transact-sql.md)
- [sp_add_jobstep (Transact-SQL)](../system-stored-procedures/sp-add-jobstep-transact-sql.md)
- [sp_add_schedule (Transact-SQL)](../system-stored-procedures/sp-add-schedule-transact-sql.md)
- [sp_attach_schedule (Transact-SQL)](../system-stored-procedures/sp-attach-schedule-transact-sql.md)
