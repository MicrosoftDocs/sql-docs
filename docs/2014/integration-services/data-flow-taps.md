---
title: "Data Flow Taps | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
ms.assetid: 2d847adf-4b3d-4949-a195-ef43de275077
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Flow Taps
  [!INCLUDE[ssISCurrent](../includes/ssiscurrent-md.md)] introduces a new feature that allows you to add a data tap on a data flow path of a package at runtime and direct the output from the data tap to an external file. To use this feature, you must deploy your SSIS project using the project deployment model to an SSIS Server. After you deploy the package to the server, you need to execute T-SQL scripts against the SSISDB database to add data taps before executing the package. Here is a sample scenario:  
  
1.  Create an execution instance of a package by using the [catalog.create_execution &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database) stored procedure.  
  
2.  Add a data tap by using either [catalog.add_data_tap](/sql/integration-services/system-stored-procedures/catalog-add-data-tap) or [catalog.add_data_tap_by_guid](/sql/integration-services/system-stored-procedures/catalog-add-data-tap-by-guid) stored procedure.  
  
3.  Start the execution instance of the package by using the [catalog.start_execution &#40;SSISDB Database&#41;](/sql/integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database).  
  
 Here is a sample SQL script that performs the steps described in the above scenario:  
  
```  
  
Declare @execid bigint  
EXEC [SSISDB].[catalog].[create_execution] @folder_name=N'ETL Folder', @project_name=N'ETL Project', @package_name=N'Package.dtsx', @execution_id=@execid OUTPUT  
EXEC [SSISDB].[catalog].add_data_tap @execution_id = @execid, @task_package_path = '\Package\Data Flow Task', @dataflow_path_id_string = 'Paths[Flat File Source.Flat File Source Output]', @data_filename = 'output.txt'  
EXEC [SSISDB].[catalog].[start_execution] @execid  
  
```  
  
 The folder name, project name, and package name parameters of the create_execution stored procedure correspond to the folder, project, and package names in the Integration Services catalog. You can get the folder, project, and package names to use in the create_execution call from the SQL Server Management Studio as shown in the following image. If you do not see your SSIS project here, you may not have deployed the project to SSIS server yet. Right-click on SSIS project in Visual Studio and click Deploy to deploy the project to the expected SSIS server.  
  
 Instead of typing the SQL statements, you can generate the execute package script by performing the following steps:  
  
1.  Right-click **Package.dtsx** and click **Execute**.  
  
2.  Click **Script** toolbar button to generate the script.  
  
3.  Now, add the add_data_tap statement before the start_execution call.  
  
 The task_package_path parameter of add_data_tap stored procedure corresponds to the PackagePath property of the data flow task in Visual Studio. In Visual Studio, right-click the **Data Flow Task**, and click **Properties** to launch the Properties window.  Note the value of the **PackagePath** property to use it as a value for the task_package_path parameter for add_data_tap stored procedure call.  
  
 The dataflow_path_id_string  parameter of add_data_tap stored procedure corresponds to the IdentificationString property of the data flow path to which you want to add a data tap. To get the dataflow_path_id_string, click the data flow path (arrow between tasks in the data flow), and note the value of the **IdentificationString** property in the Properties window.  
  
 When you execute the script, the output file is stored in \<Program Files>\Microsoft SQL Server\110\DTS\DataDumps. If a file with the name already exists, a new file with a suffix (for example: output[1].txt)  is created.  
  
 As mentioned earlier, you can also use [catalog.add_data_tap_by_guid](/sql/integration-services/system-stored-procedures/catalog-add-data-tap-by-guid)stored procedure instead of using add_data_tap stored procedure. This stored procedure takes the ID of data flow task as a parameter instead of task_package_path. You can get the ID of data flow task from the properties window in Visual Studio.  
  
## Removing a data tap  
 You can remove a data tap before starting the execution by using the [catalog.remove_data_tap](/sql/integration-services/system-stored-procedures/catalog-remove-data-tap) stored procedure. This stored procedure takes the ID of data tap as a parameter, which you can get as an output of the add_data_tap stored procedure.  
  
```  
  
DECLARE @tap_id bigint  
EXEC [SSISDB].[catalog].add_data_tap @execution_id = @execid, @task_package_path = '\Package\Data Flow Task', @dataflow_path_id_string = 'Paths[Flat File Source.Flat File Source Output]', @data_filename = 'output.txt' @data_tap_id=@tap_id OUTPUT  
EXEC [SSISDB].[catalog].remove_data_tap @tap_id  
  
```  
  
## Listing all data taps  
 You can also list all the data taps by using the catalog.execution_data_taps view. The following example extracts data taps for a specification execution instance (ID: 54).  
  
```  
select * from [SSISDB].[catalog].execution_data_taps where execution_id=@execid  
```  
  
## Performance consideration  
 Enabling verbose logging level and adding data taps increase the I/O operations performed by your data integration solution. Hence, we recommend that you add data taps only for troubleshooting purposes  
  
## Video  
 This [video on TechNet](https://technet.microsoft.com/sqlserver/dn600163) demonstrates how to add/use data taps in SQL Server 2012 SSISDB catalog that help with debugging packages programmatically and capturing the partial results at the runtime. It also discusses how to list/ remove these data taps and best practices for using data taps in SSIS packages.  
  
## Related Tasks  
 [Debugging Data Flow](troubleshooting/debugging-data-flow.md)  
  
 [Troubleshooting Tools for Package Execution](troubleshooting/troubleshooting-tools-for-package-execution.md)  
  
  
