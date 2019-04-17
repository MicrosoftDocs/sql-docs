---
title: "Schedule SSIS packages in Azure | Microsoft Docs"
description: Provides an overview of the available methods for scheduling the execution of SSIS packages deployed to Azure SQL Database.
ms.date: "05/29/2018"
ms.topic: conceptual
ms.prod: sql
ms.prod_service: "integration-services"
ms.custom: ""
ms.technology: integration-services
author: swinarko
ms.author: sawinark
ms.reviewer: douglasl
manager: craigg
---
# Schedule the execution of SQL Server Integration Services (SSIS) packages deployed in Azure

You can schedule the execution of SSIS packages deployed to the SSISDB Catalog on an Azure SQL Database server by choosing one of the methods described in this article. You can schedule a package directly, or schedule a package indirectly as part of an Azure Data Factory pipeline. For an overview about SSIS on Azure, see [Lift and shift SQL Server Integration Services workloads to the cloud](ssis-azure-lift-shift-ssis-packages-overview.md).

- Schedule a package directly

  - [Schedule with the Schedule option in SQL Server Management Studio (SSMS)](#ssms)

  - [SQL Database elastic jobs](#elastic)

  - [SQL Server Agent](#agent)

- [Schedule a package indirectly as part of an Azure Data Factory pipeline](#activity)


## <a name="ssms"></a> Schedule a package with SSMS

In SQL Server Management Studio (SSMS), you can right-click on a package deployed to the SSIS Catalog database, SSISDB, and select **Schedule** to open the **New schedule** dialog box. For more info, see [Schedule SSIS packages in Azure with SSMS](ssis-azure-schedule-packages-ssms.md).

This feature requires SQL Server Management Studio version 17.7 or higher. To get the latest version of SSMS, see [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).

## <a name="elastic"></a> Schedule a package with SQL Database Elastic Jobs

For more info about elastic jobs on SQL Database, see [Managing scaled-out cloud databases](https://docs.microsoft.com/azure/sql-database/sql-database-elastic-jobs-overview).

### Prerequisites

Before you can use elastic jobs to schedule SSIS packages stored in the SSISDB Catalog database on an Azure SQL Database server, you have to do the following things:

1.  Install and configure the Elastic Database jobs components. For more info, see [Installing Elastic Database jobs overview](https://docs.microsoft.com/azure/sql-database/sql-database-elastic-jobs-service-installation).

2. Create database-scoped credentials that jobs can use to send commands to the SSIS Catalog database. For more info, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

### Create an elastic job

Create the job by using a Transact-SQL script similar to the script shown in the following example:

```sql
-- Create Elastic Jobs target group 
EXEC jobs.sp_add_target_group 'TargetGroup' 

-- Add Elastic Jobs target group member 
EXEC jobs.sp_add_target_group_member @target_group_name='TargetGroup', 
	@target_type='SqlDatabase', @server_name='YourSQLDBServer.database.windows.net',
	@database_name='SSISDB' 

-- Add a job to schedule SSIS package execution
EXEC jobs.sp_add_job @job_name='ExecutePackageJob', @description='Description', 
	@schedule_interval_type='Minutes', @schedule_interval_count=60

-- Add a job step to create/start SSIS package execution using SSISDB catalog stored procedures
EXEC jobs.sp_add_jobstep @job_name='ExecutePackageJob', 
	@command=N'DECLARE @exe_id bigint 
		EXEC [SSISDB].[catalog].[create_execution]
            @folder_name=N''folderName'', @project_name=N''projectName'',
            @package_name=N''packageName'', @use32bitruntime=0,
            @runinscaleout=1, @useanyworker=1, 
			@execution_id=@exe_id OUTPUT		 
		EXEC [SSISDB].[catalog].[start_execution] @exe_id, @retry_count=0', 
	@credential_name='YourDBScopedCredentials', 
	@target_group_name='TargetGroup' 

-- Enable the job schedule 
EXEC jobs.sp_update_job @job_name='ExecutePackageJob', @enabled=1, 
	@schedule_interval_type='Minutes', @schedule_interval_count=60 
```

## <a name="agent"></a> Schedule a package with SQL Server Agent on premises

For more info about SQL Server Agent, see [SQL Server Agent Jobs for Packages](../packages/sql-server-agent-jobs-for-packages.md).

### Prerequisite - Create a linked server

Before you can use SQL Server Agent on premises to schedule execution of packages stored on an Azure SQL Database server, you have to add the SQL Database server to your on-premises SQL Server as a linked server.

1.  **Set up the linked server**

    ```sql
    -- Add the SSISDB database on your Azure SQL Database as a linked server to your SQL Server on premises
    EXEC sp_addlinkedserver
        @server='myLinkedServer', -- Name your linked server
        @srvproduct='',     
        @provider='sqlncli', -- Use SQL Server native client
        @datasrc='<server_name>.database.windows.net', -- Add your Azure SQL Database server endpoint
        @location='',
        @provstr='',
        @catalog='SSISDB'  -- Add SSISDB as the initial catalog
    ```

2.  **Set up linked server credentials**

    ```sql
    -- Add your Azure SQL DB server admin credentials
    EXEC sp_addlinkedsrvlogin
        @rmtsrvname = 'myLinkedServer',
        @useself = 'false',
        @rmtuser = 'myUsername', -- Add your server admin username
        @rmtpassword = 'myPassword' -- Add your server admin password
    ```

3.  **Set up linked server options**

    ```sql
    EXEC sp_serveroption 'myLinkedServer', 'rpc out', true;
    ```

For more info, see [Create Linked Servers](../../relational-databases/linked-servers/create-linked-servers-sql-server-database-engine.md) and [Linked Servers](../../relational-databases/linked-servers/linked-servers-database-engine.md).

### Create a SQL Server Agent job

To schedule a package with SQL Server Agent on premises, create a job with a job step that calls the SSIS Catalog stored procedures `[catalog].[create_execution]` and then `[catalog].[start_execution]`. For more info, see [SQL Server Agent Jobs for Packages](../packages/sql-server-agent-jobs-for-packages.md).

1.  In SQL Server Management Studio, connect to the on-premises SQL Server database on which you want to create the job.

2.  Right-click on the **SQL Server Agent** node, select **New**, and then select **Job** to open the **New Job** dialog box.

3.  In the **New Job** dialog box, select the **Steps** page, and then select **New** to open the **New Job Step** dialog box.

4.  In the **New Job Step** dialog box, select `SSISDB` as the **Database.**

5.  In the **Command** field, enter a Transact-SQL script similar to the script shown in the following example:

    ```sql
    -- T-SQL script to create and start SSIS package execution using SSISDB stored procedures
    DECLARE	@return_value int, @exe_id bigint 

    EXEC @return_value = [YourLinkedServer].[SSISDB].[catalog].[create_execution] 
        @folder_name=N'folderName', @project_name=N'projectName', 
        @package_name=N'packageName', @use32bitruntime=0, @runincluster=1, @useanyworker=1,
        @execution_id=@exe_id OUTPUT 

    EXEC [YourLinkedServer].[SSISDB].[catalog].[set_execution_parameter_value] @exe_id,
        @object_type=50, @parameter_name=N'SYNCHRONIZED', @parameter_value=1

    EXEC [YourLinkedServer].[SSISDB].[catalog].[start_execution] @execution_id=@exe_id
    ```

6.  Finish configuring and scheduling the job.

## <a name="activity"></a> Schedule a package as part of an Azure Data Factory pipeline

You can schedule a package indirectly by using a trigger to run an Azure Data Factory pipeline that runs an SSIS package.

To schedule a Data Factory pipeline, use one of the following triggers:

- [Schedule trigger](https://docs.microsoft.com/azure/data-factory/how-to-create-schedule-trigger)

- [Tumbling window trigger](https://docs.microsoft.com/azure/data-factory/how-to-create-tumbling-window-trigger)

- [Event-based trigger](https://docs.microsoft.com/azure/data-factory/how-to-create-event-trigger)

To run an SSIS package as part of a Data Factory pipeline, use one of the following activities:

- [Execute SSIS Package activity](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-ssis-activity).

- [Stored Procedure activity](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-stored-procedure-activity).

## Next steps

Review the options for running SSIS packages deployed to Azure. For more info, see [Run SSIS packages in Azure](ssis-azure-run-packages.md).
