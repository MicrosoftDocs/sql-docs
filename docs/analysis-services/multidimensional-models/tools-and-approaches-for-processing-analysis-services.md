---
title: "Tools and Approaches for Processing (Analysis Services) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Tools and Approaches for Processing (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Processing is an operation in which Analysis Services queries a relational data source and populates Analysis Services objects using that data.  
  
 As an Analysis Services system administrator, you can execute and monitor the processing of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects using these approaches:  
  
-   Run Impact Analysis to understand object dependencies and scope of operations  
  
-   Process individual objects in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
  
-   Process individual or multiple objects in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]  
  
-   Run Impact Analysis to review a list of related objects that will be unprocessed as result of the current action.  
  
-   Generate and run a script in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] XMLA Query window in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to process individual or multiple objects  
  
-   Use [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] PowerShell cmdlets  
  
-   Use control flows and tasks in [!INCLUDE[ssIS](../../includes/ssis-md.md)] packages  
  
-   Monitor processing with SQL Server Profiler  
  
-   Program a custom solution using AMO. For more information, see [Programming AMO OLAP Basic Objects](https://docs.microsoft.com/bi-reference/amo/programming-amo-olap-basic-objects).  
  
 Processing is a highly configurable operation, controlled by a set of processing options that determine whether full or incremental processing occurs at the object level. For more information about processing options and objects, see [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md) and [Processing Analysis Services Objects](../../analysis-services/multidimensional-models/processing-analysis-services-objects.md).  
  
> [!NOTE]  
>  This topic describes the tools and approaches for processing multidimensional models. For more information about processing tabular models, see [Process Database, Table, or Partition &#40;Analysis Services&#41;](../../analysis-services/tabular-models/process-database-table-or-partition-analysis-services.md) and [Process Data](../../analysis-services/tabular-models/process-data-ssas-tabular.md).  
  
### Processing objects in SQL Server Management Studio  
  
1.  Start [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and connect to Analysis Services.  
  
2.  Right-click the Analysis Services object you want to process and then click **Process**. You can process data at any of these levels:  
  
    -   Databases  
  
    -   Cubes  
  
    -   Measure Groups or individual partitions in the measure group  
  
    -   Dimensions  
  
    -   Mining Models  
  
    -   Mining Structures  
  
     Analysis Services objects are hierarchical. If you choose database, processing can occur for all of the objects contained in the database. Whether processing actually occurs will vary depending on the process option you select and the state of the object. Specifically, if an object is unprocessed, processing its parent will result in that object getting processed. For more information about object dependencies, see [Processing Analysis Services Objects](../../analysis-services/multidimensional-models/processing-analysis-services-objects.md).  
  
3.  In the **Process** dialog box, in **Process Options**, use the default value provided or select a different option from the list. For more information about each option, see [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md).  
  
4.  Click **Impact Analysis** to identify and optionally process dependent objects that are affected if the objects listed in the Process dialog box are processed.  
  
5.  Optionally, click **Change Settings** to modify the processing order, processing behavior relative to specific types of errors, and other settings.  
  
6.  Click **OK**.  
  
     The Process Progress dialog box provides ongoing status for each command. If a status message is truncated, you can click **View Details** to read the entire message.  
  
### Processing Objects in SQL Server Data Tools  
  
1.  Start [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and open a project that has been deployed.  
  
2.  In Solution Explorer, under the deployed project, expand the **Dimensions** folder.  
  
3.  Right-click a dimension, and then click **Process**. You can right-click multiple dimensions to process multiple objects at once. For more information, see [Batch Processing &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/batch-processing-analysis-services.md).  
  
4.  In the **Process Dimension** dialog box, in the **Process Options** column under **Object list**, verify that the option for this column is **Process Full**. If it is not, under **Process Options**, click the option, and select **Process Full** from the drop-down list.  
  
5.  Click **Run**.  
  
6.  When processing is finished, click **Close**.  
  
##  <a name="bkmk_impactanalysis"></a> Run Impact Analysis to identify object dependencies and scope of operations  
  
1.  Before you process an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] object in either [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] or [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you can analyze the effect on related objects by clicking **Impact Analysis** in one of the **Process Objects** dialog boxes.  
  
2.  Right-click a dimension, cube, measure group, or partition to open a **Process Objects** dialog box.  
  
3.  Click **Impact Analysis**. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] scans the model and reports on reprocessing requirements for objects that are related to the one you selected for processing.  
  
### Processing objects using XMLA  
  
1.  Start [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and connect to Analysis Services.  
  
2.  Right-click the object to be processed and then click **Process**.  
  
3.  In the **Process** dialog box, select the process option you want to use. Modify any other settings. Run Impact Analysis to identify any changes you might need to make.  
  
4.  Click **Script** on the **Process Objects** screen.  
  
     This generates an XMLA script and opens an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] XMLA Query window.  
  
5.  Close the dialog box. The script contains the processing command and options that were specified in the dialog box.  
  
6.  Optionally, you can continue adding to the script if you want to process additional objects in the same batch. To continue, repeat the previous steps, appending the generated script so that you have a single script for all processing operations. To view an example, see [Schedule SSAS Administrative Tasks with SQL Server Agent](../../analysis-services/instances/schedule-ssas-administrative-tasks-with-sql-server-agent.md).  
  
7.  From the menu bar, click **Query**, and then click **Execute**.  
  
### Processing objects using PowerShell  
  
1.  Starting in this release of SQL Server, you can use Analysis Services PowerShell cmdlets to process objects. The following cmdlets can be run interactively or in script:  
  
    -   [Invoke-ProcessCube cmdlet](../../analysis-services/powershell/invoke-processcube-cmdlet.md)  
  
    -   [Invoke-ProcessDimension cmdlet](../../analysis-services/powershell/invoke-processdimension-cmdlet.md)  
  
    -   [Invoke-ProcessPartition cmdlet](../../analysis-services/powershell/invoke-processpartition-cmdlet.md)  
  
    -   [Invoke-ASCmd cmdlet](../../analysis-services/powershell/invoke-ascmd-cmdlet.md), which can be used to execute XMLA, MDX, or DMX script that includes processing commands.  
  
### Monitoring object processing using SQL Server Profiler  
  
1.  Connect to an Analysis Services instance in SQL Server Profiler.  
  
2.  In Events Selection, click **Show all events** to add all events to the list.  
  
3.  Choose the following events:  
  
    -   **Command Begin** and **Command End** to show when processing starts and stops  
  
    -   **Error** to capture any errors  
  
    -   **Progress Report Begin**, **Progress Report Current**, and **Progress Report End** to report on process status and show the SQL queries used to retrieve the data  
  
    -   **Execute MDX Script Begin** and **Execute MDX Script End** to show the cube calculations  
  
    -   Optionally, add lock events if you are diagnosing performance problems related to processing  
  
### Process Analysis Services objects using Integration Services  
  
1.  In [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], create a package that uses the Analysis Services Processing Task to automatically populate objects with new data when you make regular updates to your source relational database.  
  
2.  In the **SSIS Toolbox**, double-click **Analysis Services Processing** to add it to the package.  
  
3.  Edit the task to specify a connection to the database, which objects to process, and the process option. For more information about how to implement this task, see [Analysis Services Processing Task](../../integration-services/control-flow/analysis-services-processing-task.md).  
  
## See Also  
 [Processing a multidimensional model &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md)  
  
  
