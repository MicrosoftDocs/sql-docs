---
title: "Schedule SSIS package execution on Azure | Microsoft Docs"
ms.date: "05/07/2018"
ms.topic: conceptual
ms.prod: sql
ms.prod_service: "integration-services"
ms.component: "lift-shift"
ms.suite: "sql"
ms.custom: ""
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Schedule the execution of an SSIS package on Azure
You can schedule the execution of packages stored in the SSISDB Catalog database on an Azure SQL Database server by choosing one of the following scheduling options:
-   [The Schedule option in SQL Server Management Studio (SSMS)](#ssms)
-   [The Azure Data Factory Execute SSIS Package activity](#execute)
-   [The Azure Data Factory SQL Server Stored Procedure activity](#stored proc)
-   [SQL Database elastic jobs](#elastic)
-   [SQL Server Agent](#agent)

## <a name="ssms"></a> Schedule a package with the SSMS

In SQL Server Management Studio (SSMS), you can right-click on a package deployed to the SSIS Catalog database, SSISDB, and select **Schedule** to open the **New schedule** dialog box. For more info, see [Schedule the execution of an SSIS package on Azure with SSMS](ssis-azure-schedule-packages-ssms.md).

## <a name="execute"></a> Schedule a package with the Execute SSIS Package activity

For info about how to schedule an SSIS package by using the Execute SSIS Package activity in Azure Data Factory, see [Run an SSIS package using SSIS activity in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-ssis-activity).

## <a name="storedproc"></a> Schedule a package with the Stored Procedure activity

For info about how to schedule an SSIS package by using the Stored Procedure activity in Azure Data Factory, see [Run an SSIS package using stored procedure activity in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-stored-procedure-activity).

For Data Factory version 1, see [Run an SSIS package using stored procedure activity in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/v1/how-to-invoke-ssis-package-stored-procedure-activity).

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

## <a name="agent"></a> Schedule a package with SQL Server Agent

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
        @location=‘’,
        @provstr=‘’,
        @catalog='SSISDB'  -- Add SSISDB as the initial catalog
    ```

2.  **Set up linked server credentials**

    ```sql
    -- Add your Azure SQL DB server admin credentials
    EXEC sp_addlinkedsrvlogin
        @rmtsrvname = 'myLinkedServer’,
        @useself = 'false’,
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

## Next steps
For more info about SQL Server Agent, see [SQL Server Agent Jobs for Packages](../packages/sql-server-agent-jobs-for-packages.md).

For more info about elastic jobs on SQL Database, see [Managing scaled-out cloud databases](https://docs.microsoft.com/azure/sql-database/sql-database-elastic-jobs-overview).
