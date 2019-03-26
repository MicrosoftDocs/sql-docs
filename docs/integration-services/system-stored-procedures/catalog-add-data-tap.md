---
title: "catalog.add_data_tap | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "language-reference"
ms.assetid: a25ebcc7-535e-4619-adf6-4e2b5a62ba37
author: janinezhang
ms.author: janinez
manager: craigg
---
# catalog.add_data_tap
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Adds a data tap on the output of a component in a package data flow, for an instance of the execution.  
  
## Syntax  
  
```sql  
catalog.add_data_tap [ @execution_id = ] execution_id  
, [ @task_package_path = ] task_package_path  
, [ @dataflow_path_id_string = ] dataflow_path_id_string  
, [ @data_filename = ] data_filename  
, [ @max_rows = ] max_rows  
, [ @data_tap_id = ] data_tap_id OUTPUT  
```  
  
## Arguments  
 [ @execution_id = ] *execution_id*  
 The execution ID for the execution that contains the package. The *execution_id* is a **bigint**.  
  
 [ @task_package_path = ] *task_package_path*  
 The package path for the data flow task. The **PackagePath** property for the data flow task specifies the path. The path is case-sensitive. To locate the package path, in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] right-click the Data Flow task, and then click **Properties**. The **PackagePath** property appears in the **Properties** window.  
  
 The *task_package_path* is a **nvarchar(max)**.  
  
 [ @dataflow_path_id_string = ] *dataflow_path_id_string*  
 The identification string for the data flow path. A path connects two data flow components. The **IdentificationString** property for the path specifies the string.  
  
 To locate the identification string, in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] right-click the path between two data flow components and then click **Properties**. The **IdentificationString** property appears in the **Properties** window.  
  
 The *dataflow_path_id_string* is a **nvarchar(4000)**.  
  
 [ @data_filename = ] *data_filename*  
 The name of the file that stores the tapped data. If the data flow task executes inside a Foreach Loop or a For Loop container, separate files store tapped data for each iteration of the loop. Each file is prefixed with a number that corresponds to an iteration.  
  
 By default, the file is stored in the \<*drive*>:\Program Files\Microsoft SQL Server\130\DTS\DataDumps folder.  
  
 The *data_filename* is a **nvarchar(4000)**.  
  
 [ @max_rows = ] *max_rows*  
 The number of rows that are captured during the data tap. If this value is not specified, all rows are captured. The *max_rows* is an **int**.  
  
 [ @data_tap_id = ] *data_tap_id*  
 Returns the ID of the data tap. The *data_tap_id* is a **bigint**.  
  
## Example  
 In the following example, a data tap is created on the data flow path, `'Paths[OLE DB Source.OLE DB Source Output]`, in the data flow task, `\Package\Data Flow Task`. The tapped data is stored in the `output0.txt` file in the DataDumps folder (\<*drive*>:\Program Files\Microsoft SQL Server\130\DTS\DataDumps).  
  
```sql
Declare @execution_id bigint  
Exec SSISDB.Catalog.create_execution @folder_name='Packages',@project_name='SSISPackages', @package_name='Package.dtsx',@reference_id=Null, @use32bitruntime=False, @execution_id=@execution_id OUTPUT  
  
Exec SSISDB.Catalog.set_execution_parameter_value @execution_id,50, 'LOGGING_LEVEL', 0  
  
Exec SSISDB.Catalog.add_data_tap @execution_id, @task_package_path='\Package\Data Flow Task', @dataflow_path_id_string = 'Paths[OLE DB Source.OLE DB Source Output]', @data_filename = 'output0.txt'  
  
Exec SSISDB.Catalog.start_execution @execution_id  
```  
  
## Remarks  
 To add data taps, the instance of the execution must be in the created state (a value of 1 in the **status** column of the [catalog.operations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operations-ssisdb-database.md)view) . The state value changes once you run the execution. You can create an execution by calling [catalog.create_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database.md).  
  
 The following are considerations for the add_data_tap stored procedure.  
  
-   If an execution contains a parent package and one or more child packages, you need to add a data tap for each package that you want to tap data for.  
  
-   If a package contains more than one data flow task with the same name, the task_package_path uniquely identifies the data flow task that contains the component output that is tapped.  
  
-   When you add data tap, it is not validated before the package is run.  
  
-   It is recommended that you limit the number of rows that are captured during the data tap, to avoid generating large data files. If the machine on which the stored procedure is executed, runs out of storage space for the data files, the package stops running and an error message is written to a log.  
  
-   Running the add_data_tap stored procedure impacts the performance of the package. It is recommended that you run the stored procedure only to troubleshoot data issues.  
  
-   To access the file that stores the tapped data, you must be an administrator on the machine on which the stored procedure is run. You must also be the user who started the execution that contains the package with the data tap.  
  
## Return Codes  
 0 (success)  
  
 When the stored procedure fails, it throws an error.  
  
## Result Set  
 None  
  
## Permissions  
 This stored procedure requires one of the following permissions:  
  
-   MODIFY permissions on the instance of execution  
  
-   Membership to the **ssis_admin** database role  
  
-   Membership to the **sysadmin** server role  
  
## Errors and Warnings  
 The following list describes conditions that cause the stored procedure to fail.  
  
-   The user does not have MODIFY permissions.  
  
-   The data tap for the specified component, in the specified package, has already been added.  
  
-   The value specified for the number of rows to capture, is not valid.  
  
## Requirements  
  
## External Resources  
 Blog entry, [SSIS 2012: A Peek to Data Taps](https://go.microsoft.com/fwlink/?LinkId=239983), on rafael-salas.com.  
  
## See Also  
 [catalog.add_data_tap_by_guid](../../integration-services/system-stored-procedures/catalog-add-data-tap-by-guid.md)  
  
  
