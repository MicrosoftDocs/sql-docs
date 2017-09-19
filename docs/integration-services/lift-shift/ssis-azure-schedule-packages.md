---
title: "Schedule  | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Schedule execution of an SSIS package on Azure SQL Database
You can schedule the execution of packages on Azure SQL Database by choosing one of the following scheduling options:
-   [SQL Server Agent](#agent)
-   [SQL Database elastic jobs](#elastic)
-   [The Azure Data Factory SQL Server Stored Procedure activity](#sproc)

## <a name="agent"></a> Schedule a package with SQL Server Agent

### Prerequisite

Before you can use SQL Server Agent on-premises to schedule package execution in SSIS Everest, you have to add the SQL Database or the SQL Server Managed Instance as a linked server. For more info, see [Create Linked Servers](../relational-databases/linked-servers/create-linked-servers-sql-server-database-engine.md) and [Linked Servers](../relational-databases/linked-servers/linked-servers-database-engine.md).

### Create a SQL Server Agent job

To schedule a package with SQL Server Agent, create a job with a job step that calls the SSIS Catalog stored procedures `[catalog].[create_execution]` and then `[catalog].[start_execution]`.

1.  In SQL Server Management Studio, connect to the on-premises SQL Server database in which you want to create the job.

2.  Right-click on the **SQL Server Agent** node, select **New**, and then select **Job** to open the **New Job** dialog box.

3.  In the **New Job** dialog box, select the **Steps** page, and then select **New** to open the **New Job Step** dialog box.

4.  In the **New Job Step** dialog box, select `SSISDB` as the **Database.**

5.  In the command field, enter a Transact-SQL script similar to the script shown in the following example:

    ```sql
    DECLARE	@return_value int, @exe_id bigint 

    EXEC @return_value = [YourLinkedServer].[SSISDB].[catalog].[create_execution] 
	@folder_name=N'folderName', @project_name=N'projectName', 
	@package_name=N'packageName', @use32bitruntime=0, 
    @runincluster=1, @useanyworker=1, @execution_id=@exe_id OUTPUT 
 
    EXEC [YourLinkedServer].[SSISDB].[catalog].[start_execution] @execution_id=@exe_id

    GO
    ```

6.  Finish configuring and scheduling the job.

## <a name="elastic"></a> Schedule a package with SQL Database Elastic Jobs

For more info about elastic jobs on SQL Database, see [Managing scaled-out cloud databases](https://docs.microsoft.com/azure/sql-database/sql-database-elastic-jobs-overview).

### Prerequisites

Before you can use elastic jobs to schedule SSIS packages on SQL Database, you have to do the following things.

1.  Install and configure the Elastic Database jobs components. For more info, see [Installing Elastic Database jobs overview](/azure/sql-database/sql-database-elastic-jobs-service-installation.md).

2. Create database-scoped credentials that jobs can use to send commands to the SSIS Catalog database.

### Create an elastic job

Create the job by using a Transact-SQL script similar to the script shown in the following example.

```sql
-- Create Elastic Jobs target group 
EXEC jobs.sp_add_target_group 'TargetGroup' 
? 
-- Add Elastic Jobs target group member 
EXEC jobs.sp_add_target_group_member @target_group_name='TargetGroup', 
	@target_type='SqlDatabase', @server_name='YourSQLDBServer.database.windows.net',
	@database_name='SSISDB' 
? 
-- Add a job to schedule SSIS package execution
EXEC jobs.sp_add_job @job_name='ExecutePackageJob', @description='Description', 
	@schedule_interval_type='Minutes', @schedule_interval_count=60

-- Add a job step to create/start SSIS package execution using SSISDB sprocs
EXEC jobs.sp_add_jobstep @job_name='ExecutePackageJob', 
	@command=N'DECLARE @exe_id bigint 
		EXEC [SSISDB].[catalog].[create_execution]
            @folder_name=N''folderName'', @project_name=N''projectName'',
            @package_name=N''packageName'', @use32bitruntime=0,
            @runincluster=1, @useanyworker=1, 
			@execution_id=@exe_id OUTPUT		 
		EXEC [SSISDB].[catalog].[start_execution] @exe_id, @retry_count=0', 
	@credential_name='YourDBScopedCredentials', 
	@target_group_name='TargetGroup' 

-- Enable the job schedule 
EXEC jobs.sp_update_job @job_name='ExecutePackageJob', @enabled=1, 
	@schedule_interval_type='Minutes', @schedule_interval_count=60 
```

## <a name="sproc"></a> Schedule a package with the Data Factory SQL Server Stored Procedure activity

To schedule a package with the Data Factory SQL Server Stored Procedure activity, do the following things:
1.  Create a Data Factory.
2.  Created a linked service for the SQL Database that hosts SSISDB.
3.  Create an output dataset that drives the scheduling.
4.  Create a Data Factory pipeline that uses the SQL Server Stored Procedure activity to run the SSIS package.

This section provides an overview of these steps. A complete Data Factory tutorial is beyond the scope of this article. For more info, see [SQL Server Stored Procedure Activity](https://docs.microsoft.com/en-us/azure/data-factory/data-factory-stored-proc-activity).

### Created a linked service for the SQL Database that hosts SSISDB
The linked service lets Data Factory connect to SSISDB.

```json
{
	"name": "AzureSqlLinkedService",
	"properties": {
		"description": "",
		"type": "AzureSqlDatabase",
		"typeProperties": {
			"connectionString": "Data Source = tcp: YourSQLDBServer.database.windows.net, 1433; Initial Catalog = SSISDB; User ID = YourUsername; Password = YourPassword; Integrated Security = False; Encrypt = True; Connect Timeout = 30 "
		}
	}
}
```

### Create an output dataset
The output dataset contains the scheduling info.

```json
{
	"name": "sprocsampleout",
	"properties": {
		"type": "AzureSqlTable",
		"linkedServiceName": "AzureSqlLinkedService",
		"typeProperties": {
			"tableName": "sampletable"
		},
		"availability": {
			"frequency": "Hour",
			"interval": 1
		}
	}
}
```
### Create a Data Factory pipeline
The pipeline uses the SQL Server Stored Procedure activity to run the SSIS package.

```json
{
	"name": "SprocActivitySamplePipeline",
	"properties": {
		"activities": [{
			"name": "SprocActivitySample",
			"type": "SqlServerStoredProcedure",
			"typeProperties": {
				"storedProcedureName": "sp_executesql",
				"storedProcedureParameters": {
					"stmt": "Transact-SQL script to create and start SSIS package execution using SSISDB stored procedures"
				}
			},
			"outputs": [{
				"name": "sprocsampleout"
			}],
			"scheduler": {
				"frequency": "Hour",
				"interval": 1
			}
		}],
		"start": "2017-10-01T00:00:00Z",
		"end": "2017-10-01T05:00:00Z",
		"isPaused": false
	}
}
```

You don't have to create a new stored procedure to encapsulate the Transact-SQL commands required to create and start SSIS package execution. You can simply provide the script as the value of the `stmt` parameter in the preceding JSON sample. Here is a sample script:

```sql
-- T-SQL script to create and start SSIS package execution using SSISDB sprocs
DECLARE @return_value INT,@exe_id BIGINT,@err_msg NVARCHAR(150)

EXEC @return_value=[SSISDB].[catalog].[create_execution] @folder_name=N'folderName', @project_name=N'projectName', @package_name=N'packageName', @use32bitruntime=0, @runincluster=1,@useanyworker=1, @execution_id=@exe_id OUTPUT
                                                         
EXEC [SSISDB].[catalog].[start_execution] @execution_id=@exe_id,@retry_count=0
-- To synchronize SSIS package execution, poll package execution status
-- created (1)
-- running (2)
-- canceled (3)
-- failed (4)
-- pending (5)
-- ended unexpectedly (6)
-- succeeded (7)
-- stopping (8)
-- completed (9) 
                                          
WHILE(SELECT [status]
      FROM [SSISDB].[catalog].[executions]
      WHERE execution_id=@exe_id) NOT IN(3,4,6,7,9)
BEGIN
    WAITFOR DELAY '00:00:01';
END

-- Raise an error for unsuccessful package execution
IF(SELECT [status]
   FROM [SSISDB].[catalog].[executions]
   WHERE execution_id=@exe_id)<>7
BEGIN
    SET @err_msg=N'Your package execution did not succeed for execution ID: '+CAST(@exe_id AS NVARCHAR(20))
    RAISERROR(@err_msg,15,1)
END
GO
```

For more info about the code in this script, see [Deploy and Execute SSIS Packages using Stored Procedures](https://docs.microsoft.com/en-us/sql/integration-services/packages/deploy-integration-services-ssis-projects-and-packages#deploy-and-execute-ssis-packages-using-stored-procedures).

## Next steps
