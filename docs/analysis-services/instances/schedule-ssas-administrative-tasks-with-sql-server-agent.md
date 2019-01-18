---
title: "Schedule SSAS Administrative Tasks with SQL Server Agent | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Schedule SSAS Administrative Tasks with SQL Server Agent
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Using the SQL Server Agent service, you can schedule [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] administrative tasks to run in the order and times that you need. Scheduled tasks help you automate processes that run on regular or predictable cycles. You can schedule administrative tasks, such as cube processing, to run during times of slow business activity. You can also determine the order in which tasks run by creating job steps within a SQL Server Agent job. For example, you can process a cube and then perform a backup of the cube.  
  
 Job steps give you control over flow of execution. If one job fails, you can configure SQL Server Agent to continue to run the remaining tasks or to stop execution. You can also configure SQL Server Agent to send notifications about the success or failure of job execution.  
  
 This topic is a walkthrough that shows two ways of using SQL Server Agent to run XMLA script. The first example demonstrates how to schedule processing of a single dimension. Example two shows how to combine processing tasks into a single script that runs on a schedule. To complete this walkthrough, you will need to meet the following prerequisites.  
  
## Prerequisites  
 SQL Server Agent service must be installed.  
  
 By default, jobs run under the service account. The default account for SQL Server Agent is NT Service\SQLAgent$\<instancename>. To perform a backup or processing task, this account must be a system administrator on the Analysis Services instance. For more information, see [Grant server admin rights to an  Analysis Services instance](../../analysis-services/instances/grant-server-admin-rights-to-an-analysis-services-instance.md).  
  
 You should also have a test database to work with. You can deploy the AdventureWorks multidimensional sample database or a project from the Analysis Services multidimensional tutorial to use in this walkthrough. For more information, see [Install Sample Data and Projects for the Analysis Services Multidimensional Modeling Tutorial](../../analysis-services/install-sample-data-and-projects.md).  
  
## Example 1: Processing a dimension in a scheduled task  
 This example demonstrates how to create and schedule a job that processes a dimension.  
  
 An [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] scheduled task is an XMLA script that is embedded into a SQL Server Agent job. This job is scheduled to run at desired times and frequency. Because the SQL Server Agent is part of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you work with both the Database Engine and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to create and schedule an administrative task.  
  
###  <a name="bkmk_CreateScript"></a> Create a script for processing a dimension in a SQL Server Agent job  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Open a database folder and find a dimension. Right-click the dimension and select **Process**.  
  
2.  In the **Process Dimension** dialog box, in the **Process Options** column under **Object list**, verify that the option for this column is **Process Full**. If it is not, under **Process Options**, click the option, and then select **Process Full** from the drop-down list.  
  
3.  Click **Script**.  
  
     This step opens an **XML Query** window that contains the XMLA script that processes the dimension.  
  
4.  In the **Process Dimension** dialog box, click **Cancel** to close the dialog box.  
  
5.  In the **XMLA Query** window, highlight the XMLA script, right-click the highlighted script, and select **Copy**.  
  
     This step copies the XMLA script to the Windows Clipboard. You can leave the XMLA script in the Clipboard or paste it into Notepad or another text editor. The following is an example of the XMLA script.  
  
    ```  
    <Batch xmlns="http://schemas.microsoft.com/analysisservices/2003/engine">  
     <Parallel>  
      <Process xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
        <Object>  
          <DatabaseID>Adventure Works DW Multidimensional</DatabaseID>  
          <DimensionID>Dim Account</DimensionID>  
        </Object>  
        <Type>ProcessFull</Type>  
        <WriteBackTableCreation>UseExisting</WriteBackTableCreation>  
      </Process>  
     </Parallel>  
    </Batch>  
    ```  
  
###  <a name="bkmk_ProcessJob"></a> Create and schedule the dimension processing job  
  
1.  Connect to an instance of the Database Engine and then open Object Explorer.  
  
2.  Expand **SQL Server Agent**.  
  
3.  Right-click **Jobs** and select **New Job**.  
  
4.  In the **New Job** dialog box, enter a job name in **Name**.  
  
5.  Under **Select a page**, select **Steps**, and then click **New**.  
  
6.  In the **New Job Step** dialog box, enter a step name in **Step Name**.  
  
7.  In **Server**, type **localhost** for a default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and **localhost\\**\<*instance name*> for a named instance.  
  
     If you will be running the job from a remote computer, use the server name and instance name where the job will run. Use the format \<*server name*> for a default instance, and \<*server name*>\\<*instance name*> for a named instance.  
  
8.  In **Type**, select **SQL Server Analysis Services Command**.  
  
9. In **Command**, right-click and select **Paste**. The XMLA script that you generated in the previous step should appear in the command window.  
  
10. Click **OK**.  
  
11. Under **Select a page**, click **Schedules**, and then click **New**.  
  
12. In the **New Job Schedule** dialog box, enter a schedule name in **Name**, and then click **OK**.  
  
     This step creates a schedule for Sunday at 12:00 AM. The next step shows you how to manually execute the job. You can also specify a schedule that executes the job when you are monitoring it.  
  
13. In the **New Job** dialog box, click **OK**.  
  
14. In **Object Explorer**, expand **Jobs**, right-click the job you created, and then select **Start Job at Step**.  
  
     Because the job has only one step, the job executes immediately. If the job contains more than one step, you can select the step at which the job should start.  
  
15. When the job finishes, click **Close**.  
  
## Example 2: Batch processing a dimension and a partition in a scheduled task  
 The procedures in this example demonstrate how to create and schedule a job that batch processes an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database dimension, and at the same time to process a  cube partition that depends on the dimension for aggregation. For more information about batch processing of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects, see [Batch Processing &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/batch-processing-analysis-services.md).  
  
###  <a name="bkmk_BatchProcess"></a> Create a script for batch processing a dimension and partition in a SQL Server Agent job  
  
1.  Using the same database, expand **Dimensions**, right-click the **Customer** dimension, and select **Process**.  
  
2.  In the **Process Dimension** dialog box, in **Process Options** column under **Object list**, verify that the option for this column is **Process Full**.  
  
3.  Click **Script**.  
  
     This step opens an **XML Query** window that contains the XMLA script that processes the dimension.  
  
4.  In the **Process Dimension** dialog box, click **Cancel** to close the dialog box.  
  
5.  Expand **Cubes**, expand **Adventure Works**, expand **Measure Groups**, expand **Internet Sales**, expand **Partitions**, right-click the last partition in the list, and then select **Process**.  
  
6.  In the **Process Partition** dialog box, in the **Process Options** column under **Object list**, verify that the option for this column is **Process Full**.  
  
7.  Click **Script**.  
  
     This step opens a second **XML Query** window that contains the XMLA script that processes the partition.  
  
8.  In the **Process Partition** dialog box, click **Cancel** to close the editor.  
  
     At this point you must merge the two scripts, and ensure that the dimension is processed first.  
  
    > [!WARNING]  
    >  If the partition is processed first, the subsequent dimension processing causes the partition to become unprocessed. The partition would then require a second processing to reach a processed state.  
  
9. In the **XMLA Query** window that contains the XMLA script that processes the partition, highlight the code inside the `Batch` and `Parallel` tags, right-click the highlighted script, and select **Copy**.  
  
    ```  
    <Process xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
        <Object>  
          <DatabaseID> Adventure Works DW Multidimensional</DatabaseID>  
          <CubeID>Adventure Works</CubeID>  
          <MeasureGroupID>Fact Internet Sales 1</MeasureGroupID>  
          <PartitionID> Internet_Sales_2004</PartitionID>  
        </Object>  
        <Type>ProcessFull</Type>  
        <WriteBackTableCreation>UseExisting</WriteBackTableCreation>  
      </Process>  
    ```  
  
10. Open the **XMLA Query** window that contains the XMLA script that processes the dimension. Right-click within the script to the left of the `</Process>` tag and select **Paste**.  
  
     The following example shows the revised XMLA script.  
  
    ```  
    <Batch xmlns="http://schemas.microsoft.com/analysisservices/2003/engine">  
     <Parallel>  
      <Process xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
        <Object>  
          <DatabaseID>Adventure Works DW Multidimensional</DatabaseID>  
          <DimensionID>Dim Customer</DimensionID>  
        </Object>  
        <Type>ProcessFull</Type>  
        <WriteBackTableCreation>UseExisting</WriteBackTableCreation>  
      </Process>  
      <Process xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
        <Object>  
          <DatabaseID>Adventure Works DW Multidimensional</DatabaseID>  
          <CubeID>Adventure Works</CubeID>  
          <MeasureGroupID>Fact Internet Sales 1</MeasureGroupID>  
          <PartitionID>Internet_Sales_2004</PartitionID>  
        </Object>  
        <Type>ProcessFull</Type>  
        <WriteBackTableCreation>UseExisting</WriteBackTableCreation>  
      </Process>  
     </Parallel>  
    </Batch>  
    ```  
  
11. Highlight the revised XMLA script, right-click the highlighted script, and select **Copy.**  
  
12. This step copies the XMLA script to the Windows Clipboard. You can leave the XMLA script in the Clipboard, save it to a file, or paste it into Notepad or another text editor.  
  
###  <a name="bkmk_Scheduling"></a> Create and schedule the batch processing job  
  
1.  Connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then open Object Explorer.  
  
2.  Expand **SQL Server Agent**. Start the service if is not running.  
  
3.  Right-click **Jobs** and select **New Job**.  
  
4.  In the **New Job** dialog box, enter a job name in **Name**.  
  
5.  In **Steps**, click **New**.  
  
6.  In the **New Job Step** dialog box, enter a step name in **Step Name**.  
  
7.  In **Type**, select **SQL Server Analysis Services Command**.  
  
8.  In **Run as**, select the **SQL Server Agent Service Account**. Recall from the Prerequisites section that this account must have administrative permissions on Analysis Services.  
  
9. In **Server**, specify the server name of the Analysis Services instance.  
  
10. In **Command**, right-click and select **Paste**.  
  
11. Click **OK**.  
  
12. In the **Schedules** page, click **New**.  
  
13. In the **New Job Schedule** dialog box, enter a schedule name in **Name**, and then click **OK**.  
  
     This step creates a schedule for Sunday at 12:00 AM. The next step shows you how to manually execute the job. You can also select a schedule which will execute the job when you are monitoring it.  
  
14. Click **OK** to close the dialog box.  
  
15. In **Object Explorer**, expand **Jobs**, right-click the job you created, and select **Start Job at Step**.  
  
     Because the job has only one step, the job executes immediately. If the job contains more than one step, you can select the step at which the job should start.  
  
16. When the job finishes, click **Close**.  
  
## See Also  
 [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md)   
  
  
