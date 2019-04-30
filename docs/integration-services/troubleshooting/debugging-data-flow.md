---
title: "Debugging Data Flow | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "progress reporting [Integration Services]"
  - "data viewers [Integration Services]"
  - "data flow [Integration Services], debugging"
  - "debugging [Integration Services], data flow"
  - "counting rows"
ms.assetid: 1c574f1b-54f7-4c05-8e42-8620e2c1df0f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Debugging Data Flow

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer include features and tools that you can use to troubleshoot the data flows in an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package.  
  
-   [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides data viewers.  
  
-   [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] transformations provide row counts.  
  
-   [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides progress reporting at run time.  
  
## Data Viewers  
 Data viewers display data between two components in a data flow. Data viewers can display data when the data is extracted from a data source and first enters a data flow, before and after a transformation updates the data, and before the data is loaded into its destination.  
  
 To view the data, you attach data viewers to the path that connects two data flow components. The ability to view data between data flow components makes it easier to identify unexpected data values, view the way a transformation changes column values, and discover the reason that a transformation fails. For example, you may find that a lookup in a reference table fails, and to correct this you may want to add a transformation that provides default data for blank columns.  
  
 A data viewer can display data in a grid. Using a grid, you select the columns to display. The values for the selected columns display in a tabular format.  
  
 You can also include multiple data viewers on a path. You can display the same data in different formats-for example, create a chart view and a grid view of the data-or create different data viewers for different columns of data.  
  
 When you add a data viewer to a path, [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer adds a data viewer icon to the design surface of the **Data Flow** tab, next to the path. Transformations that can have multiple outputs, such as the Conditional Split transformation, can include a data viewer on each path.  
  
 At run time, a **Data Viewer** window opens and displays the information specified by the data viewer format. For example, a data viewer that uses the grid format shows data for the selected columns, the number of output rows passed to the data flow component, and the number of rows displayed. The information displays buffer by buffer and, depending on the width of the rows in the data flow, a buffer may contain more or fewer rows.  
  
 In the **Data Viewer** dialog box, you can copy the data to the Clipboard, clear all data from the table, reconfigure the data viewer, resume the flow of data, and detach or attach the data viewer.  
  
#### To add a data viewer  
  
-   [Add a Data Viewer to a Data Flow](#add_viewer)  
  
## Row Counts  
 The number of rows that have passed through a path is displayed on the design surface of the **Data Flow** tab in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer next to the path. The number is updated periodically while the data moves through the path.  
  
 You can also add a Row Count transformation to the data flow to capture the final row count in a variable. For more information, see [Row Count Transformation](../../integration-services/data-flow/transformations/row-count-transformation.md).  
  
## Progress Reporting  
 When you run a package, [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer depicts progress on the design surface of the **Data Flow** tab by displaying each data flow component in a color that indicates status. When each component starts to perform its work, it changes from no color to yellow, and when it finishes successfully, it changes to green. A red color indicates that the component failed.  
  
 The following table describes the color-coding.  
  
|Color|Description|  
|-----------|-----------------|  
|No color|Waiting to be called by the data flow engine.|  
|Yellow|Performing a transformation, extracting data, or loading data.|  
|Green|Ran successfully.|  
|red|Ran with errors.|  

## Analysis of Data Flow
  You can use the [catalog.execution_data_statistics](../../integration-services/system-views/catalog-execution-data-statistics.md) **SSISDB** database view to analyze the data flow of packages. This view displays a row each time a data flow component sends data to a downstream component. The information can be used to gain a deeper understanding of the rows that are sent to each component.  
  
> [!NOTE]  
>  The logging level must be set to **Verbose** in order to capture information with the catalog.execution_data_statistics view.  
  
 The following example displays the number of rows sent between components of a package.  
  
```sql
use SSISDB  
select package_name, task_name, source_component_name, destination_component_name, rows_sent  
from catalog.execution_data_statistics  
where execution_id = 132  
order by source_component_name, destination_component_name   
```  
  
 The following example calculates the number of rows per millisecond sent by each component for a specific execution. The calculated values are:  
  
-   **total_rows** - the sum of all the rows sent by the component  
  
-   **wall_clock_time_ms** - the total elapsed execution time, in milliseconds, for each component  
  
-   **num_rows_per_millisecond** - the number of rows per millisecond sent by each component  
  
 The **HAVING** clause is used to prevent a divide-by-zero error in the calculations.  
  
```sql  
use SSISDB  
select source_component_name, destination_component_name,  
    sum(rows_sent) as total_rows,  
    DATEDIFF(ms,min(created_time),max(created_time)) as wall_clock_time_ms,  
    ((0.0+sum(rows_sent)) / (datediff(ms,min(created_time),max(created_time)))) as [num_rows_per_millisecond]  
from [catalog].[execution_data_statistics]  
where execution_id = 132  
group by source_component_name, destination_component_name  
having (datediff(ms,min(created_time),max(created_time))) > 0  
order by source_component_name desc  
```  

## Configure an Error Output in a Data Flow Component
  Many data flow components support error outputs, and depending on the component, [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer provides different ways to configure an error output. In addition to configuring an error output, you can also configure the columns of an error output. This includes configuring the **ErrorCode** and **ErrorColumn** columns that are added by the component.  
  
### Configuring an Error Output  
 To configure an error output, you have two options:  
  
-   Use the **Configure Error Output** dialog box. You can use this dialog box to configure an error output on any data flow component that supports an error output.  
  
-   Use the editor dialog box for the component. Some components let you configure error outputs directly from their editor dialog box. However, you cannot configure error outputs from the editor dialog box for the ADO NET source, the Import Column transformation, the OLE DB Command transformation, or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Compact destination.  
  
 The following procedures describe how to use these dialog boxes to configure error outputs.  
  
#### To configure an error output using the Configure Error Output dialog box  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the **Data Flow** tab.  
  
4.  Drag the error output, represented by the red arrow, from the component that is the source of the errors to another component in the data flow.  
  
5.  In the **Configure Error Output** dialog box, select an action in the **Error** and **Truncation** columns for each column in the component input.  
  
6.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
#### To add an error output using the editor dialog box for the component  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the **Data Flow** tab.  
  
4.  Double-click the data flow components in which you want to configure an error output and, depending on the component, do one of the following steps:  
  
    -   Click **Configure Error Output**.  
  
    -   Click **Error Output**.  
  
5.  Set the **Error** option for each column.  
  
6.  Set the **Truncation** option for each column.  
  
7.  Click **OK**.  
  
8.  To save the updated package, on the **File** menu, click **Save Selected Items**.  
  
### Configuring Error Output Columns  
 To configure the error output columns, you have to use the **Input and Output Properties** tab of the **Advanced Editor** dialog box.  
  
#### To configure error output columns  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the **Data Flow** tab.  
  
4.  Right-click the component whose error output columns you want to configure and click **Show Advanced Editor**.  
  
5.  Click the **Input and Output Properties** tab and expand **\<component name> Error Output** and then expand **Output Columns**.  
  
6.  Click a column and update its properties.  
  
    > [!NOTE]  
    >  The list of columns includes the columns in the component input, the **ErrorCode** and **ErrorColumn** columns added by previous error outputs, and the **ErrorCode** and **ErrorColumn** columns added by this component.  
  
7.  Click **OK.**  
  
8.  To save the updated package, on the **File** menu, click **Save Selected Items**.  

## <a name="add_viewer"></a> Add a Data Viewer to a Data Flow
  This topic describes how to add and configure a data viewer in a data flow. A data viewer displays data that is moving between two data flow components. For example, a data viewer can display the data that is extracted from a data source before a transformation in the data flow modifies the data.  
  
 A path connects components in a data flow by connecting the output of one data flow component to the input of another component.  
  
 Before you can add data viewers to a package, the package must include a Data Flow task and at least two data flow components that are connected.  
  
 Add a data viewer to an error output to see the description of the error and the name of the column in which the error occurred. By default the error output includes only numeric identifiers for the error and the column.  
  
### To add a data viewer to a data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Control Flow** tab, if it is not already active.  
  
4.  Click the Data Flow task to whose data flow you want to attach a data viewer and then click the **Data Flow** tab.  
  
5.  Right-click a path between two data flow components, and click **Edit**.  
  
6.  On the **General** page, you can view and edit path properties. For example, from the **PathAnnotation** drop-down list you can select the annotation that appears next to the path.  
  
7.  On the **Metadata** page, you can view the column metadata and copy the metadata to the Clipboard.  
  
8.  On the **Data Viewer** page, click **Enable data viewer**.  
  
9. In the Columns to display area, select the columns you want to display in the data viewer. By default, all the available columns are selected and listed in the **Displayed Columns** list. Move columns that you do not want to use to the **Unused Column** list by selecting them and then clicking the left arrow.  
  
    > [!NOTE]  
    >  In the grid, values that represent the DT_DATE, DT_DBTIME2, DT_FILETIME, DT_DBTIMESTAMP, DT_DBTIMESTAMP2, and DT_DBTIMESTAMPOFFSET data types appear as ISO 8601 formatted strings and a space separator replaces the **T** separator. Values that represent the DT_DATE and DT_FILETIME data types include seven digits for fractional seconds. Because the DT_FILETIME data type stores only three digits of fractional seconds, the grid displays zeros for the remaining four digits. Values that represent the DT_DBTIMESTAMP data type include three digits for fractional seconds. For values that represent the DT_DBTIME2, DT_DBTIMESTAMP2, and DT_DBTIMESTAMPOFFSET data types, the number of digits for fractional seconds corresponds to the scale specified for the column's data type. For more information about ISO 8601 formats, see [Date and Time Formats](https://msdn.microsoft.com/library/bed6e2c1-791a-4fa1-b29f-cbfdd1fa8d39). For more information about data types, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
10. Click **OK**.  

## Data Flow Taps
 You can add a data tap on a data flow path of a package at runtime and direct the output from the data tap to an external file. To use this feature, you must deploy your SSIS project using the project deployment model to an SSIS Server. After you deploy the package to the server, you need to execute T-SQL scripts against the SSISDB database to add data taps before executing the package. Here is a sample scenario:  
  
1.  Create an execution instance of a package by using the [catalog.create_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database.md) stored procedure.  
  
2.  Add a data tap by using either [catalog.add_data_tap](../../integration-services/system-stored-procedures/catalog-add-data-tap.md) or [catalog.add_data_tap_by_guid](../../integration-services/system-stored-procedures/catalog-add-data-tap-by-guid.md) stored procedure.  
  
3.  Start the execution instance of the package by using the [catalog.start_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database.md).  
  
 Here is a sample SQL script that performs the steps described in the above scenario:  
  
```sql
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
  
 As mentioned earlier, you can also use [catalog.add_data_tap_by_guid](../../integration-services/system-stored-procedures/catalog-add-data-tap-by-guid.md)stored procedure instead of using add_data_tap stored procedure. This stored procedure takes the ID of data flow task as a parameter instead of task_package_path. You can get the ID of data flow task from the properties window in Visual Studio.  
  
### Removing a data tap  
 You can remove a data tap before starting the execution by using the [catalog.remove_data_tap](../../integration-services/system-stored-procedures/catalog-remove-data-tap.md) stored procedure. This stored procedure takes the ID of data tap as a parameter, which you can get as an output of the add_data_tap stored procedure.  
  
```sql
DECLARE @tap_id bigint  
EXEC [SSISDB].[catalog].add_data_tap @execution_id = @execid, @task_package_path = '\Package\Data Flow Task', @dataflow_path_id_string = 'Paths[Flat File Source.Flat File Source Output]', @data_filename = 'output.txt' @data_tap_id=@tap_id OUTPUT  
EXEC [SSISDB].[catalog].remove_data_tap @tap_id  
```  
  
### Listing all data taps  
 You can also list all the data taps by using the catalog.execution_data_taps view. The following example extracts data taps for a specification execution instance (ID: 54).  
  
```sql 
select * from [SSISDB].[catalog].execution_data_taps where execution_id=@execid  
```  
  
### Performance consideration  
 Enabling verbose logging level and adding data taps increase the I/O operations performed by your data integration solution. Hence, we recommend that you add data taps only for troubleshooting purposes  
  
### Video  
 This [video on TechNet](https://technet.microsoft.com/sqlserver/dn600163) demonstrates how to add/use data taps in SQL Server 2012 SSISDB catalog that help with debugging packages programmatically and capturing the partial results at the runtime. It also discusses how to list/ remove these data taps and best practices for using data taps in SSIS packages.  
 
## See Also  
 [Error Handling in Data](../../integration-services/data-flow/error-handling-in-data.md)  
  
  
