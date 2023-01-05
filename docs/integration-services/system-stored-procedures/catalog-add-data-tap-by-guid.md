---
description: "catalog.add_data_tap_by_guid"
title: "catalog.add_data_tap_by_guid | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "language-reference"
ms.assetid: ed9d7fa3-61a1-4e21-ba43-1ead7dfc74eb
author: chugugrace
ms.author: chugu
---
# catalog.add_data_tap_by_guid 

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Adds a data tap to a specific data flow path in a package data flow, for an instance of the execution.  
  
## Syntax  
  
```sql  
catalog.add_data_tap_by_guid [ @execution_id = ] execution_id  
, [ @dataflow_task_guid = ] dataflow_task_guid   
, [ @dataflow_path_id_string = ] dataflow_path_id_string  
, [ @data_filename = ] data_filename  
, [ @max_rows = ] max_rows  
, [ @data_tap_id = ] data_tap_id  
```  
  
## Arguments  
 [ @execution_id = ] *execution_id*  
 The execution ID for the execution that contains the package. The *execution_id* is a **bigint**.  
  
 [ @dataflow_task_guid = ] *dataflow_task_guid*  
 The ID for the data task flow in the package that contains the data flow path to be tapped. The *dataflow_task_guid* is a**uniqueidentifier**.  
  
 [ @dataflow_path_id_string = ] *dataflow_path_id_string*  
 The identification string for the data flow path. A path connects two data flow components. The **IdentificationString** property for the path specifies the string.  
  
 To locate the identification string, in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] right-click the path between two data flow components and then click **Properties**. The **IdentificationString** property appears in the **Properties** window.  
  
 The *dataflow_path_id_string* is a **nvarchar(4000)**.  
  
 [ @data_filename = ] *data_filename*  
 The name of the file that stores the tapped data. If the data flow task executes inside a Foreach Loop or a For Loop container, separate files store tapped data for each iteration of the loop. Each file is prefixed with a number that corresponds to an iteration. Data tap files are written to the folder "*\<SQL Server installation folder>*\130\DTS\\". The *data_filename* is a **nvarchar(4000)**.  
  
 [ @max_rows = ] max_rows  
 The number of rows that are captured during the data tap. If this value is not specified, all rows are captured. The max_rows is an **int**.  
  
 [ @data_tap_id = ] *data_tap_id*  
 The ID of the data tap. The *data_tap_id* is a **bigint**.  
  
## Example  
 In the following example, a data tap is created on the data flow path,  `Paths[SRC DimDCVentor.OLE DB Source Output]`, in the data flow task `{D978A2E4-E05D-4374-9B05-50178A8817E8}`. The tapped data is stored in the DCVendorOutput.csv file.  
  
```sql
exec catalog.add_data_tap_by_guid   @execution_id,   
'{D978A2E4-E05D-4374-9B05-50178A8817E8}',   
'Paths[SRC DimDCVentor.OLE DB Source Output]',   
'D:\demos\datafiles\DCVendorOutput.csv'  
```  
  
## Remarks  
 To add data taps, the instance of the execution must be in the created state (a value of 1 in the **status** column of the [catalog.operations &#40;SSISDB Database&#41;](../../integration-services/system-views/catalog-operations-ssisdb-database.md)view) . The state value changes once you run the execution. You can create an execution by calling [catalog.create_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database.md).  
  
 The following are considerations for the add_data_tap_by_guid stored procedure.  
  
-   When you add a data tap, it is not validated before the package is run.  
  
-   It is recommended that you limit the number of rows that are captured during the data tap, to avoid generating large data files. If the machine on which the stored procedure is executed, runs out of storage space for the data files, the stored procedure stops running.  
  
-   Running the add_data_tap_by_guid stored procedure impacts the performance of the package. It is recommended that you run the stored procedure only to troubleshoot data issues.  
  
-   To access the file that stores the tapped data, you must have administrator permissions on the machine on which the stored procedure is run, or you must be the user that started the execution that contains the package with the data tap.  
  
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
  
## See Also  
 [catalog.add_data_tap](../../integration-services/system-stored-procedures/catalog-add-data-tap.md)  
  
  
