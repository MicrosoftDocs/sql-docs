---
title: "Remote Processing (Analysis Services) | Microsoft Docs"
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
# Remote Processing (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can run scheduled or unattended processing on a remote [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, where the processing request originates from one computer but executes on another computer on the same network.  
  
## Prerequisites  
  
-   If you are running different versions of SQL Server on each computer, the client libraries must match the version of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance that is processing the model.
  
-   On the remote server, **Allow remote connections to this computer** must be enabled, and the account issuing the processing request must be listed as an allowed user.  
  
-   Windows firewall rules must be configured to allow inbound connections to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Verify you can connect to the remote [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. See [Configure the Windows Firewall to Allow Analysis Services Access](../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md).  
  
-   Resolve any existing local processing errors before attempting remote processing. Verify that when the processing request is local, data can be successfully retrieved from the external relational data source. See [Set Impersonation Options &#40;SSAS - Multidimensional&#41;](../../analysis-services/multidimensional-models/set-impersonation-options-ssas-multidimensional.md) for instructions on specifying credentials used to retrieve data.  
  
## On-demand remote processing  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] accepts processing requests from user or application accounts that have [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] administrator permissions. If you are an administrator, verify that you can connect to the remote instance and process the database manually over the remote connection.  
  
1.  On the computer that will be used to schedule processing, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and connect to the remote [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.  
  
2.  Right-click the database, select **Process**, point to **Script** and choose **Script Action to New Query Window**. Commands used to invoke processing will appear in the query window.  
  
3.  Click **OK** to begin processing.  
  
     Successful completion of this task provides an XMLA query that you can embed in a scheduled job. It also confirms there are no connection problems.  
  
## Schedule remote processing using SQL Server Agent Service  
 By default, SQL Server Agent service runs under a virtual account, with network connections made using the machine account. To avoid having to give a machine account administrative rights on the remote [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, you should change the SQL Server Agent service account to run as a least-privilege domain user account.  
  
 Be sure to grant all of the necessary permissions, including giving the account **sysadmin** rights on the Database Engine instance providing the service.  
  
 Use the following links to set permissions:  
  
-   [Configure SQL Server Agent](../../ssms/agent/configure-sql-server-agent.md)  
  
-   [SQL Server Agent Components](../../ssms/agent/sql-server-agent.md) suggests alternative fixed server roles if granting **sysadmin** permissions is not possible.  
  
 After account permissions are configured, continue with these steps.  
  
#### Grant the SQL Server Agent account administrator permission on SSAS  
  
1.  Using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], connect to the remote [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.  
  
2.  Right-click the server name, click P**roperties**, and then click **Security**.  
  
3.  Click **Add** to add the SQL Server Agent account.  
  
#### Create the Job  
  
1.  In Management Studio, connect to the local Database Engine instance. SQL Server Agent is the last item in Object Explorer. If necessary, start the service.  
  
2.  Right-click **Job**s, click **New Job** and then enter a name.  
  
3.  In Steps, click **New** and then enter a name.  
  
4.  In Type, choose **SQL Server Analysis Services Command**.  
  
5.  In Server, enter the name of the remote [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.  
  
6.  In Command, paste the XMLA command for processing the database. This is the XMLA script that you generated in the verification step for on-demand remote processing. Click **OK** to save the job.  
  
#### Start SQL Server Profiler  
  
1.  On the remote computer, start SQL Server Profiler. Connect to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance, and click **Run** to start the trace using the default events.  
  
     You will use SQL Server Profiler to monitor the processing events as they occur.  
  
2.  Optionally, you can set trace properties to send the trace to a file or table in a database.  
  
#### Run the job  
  
1.  On the computer used to run the job, verify that the job can perform the basic operation. In Object Explorer, under SQL Server Agent, expand **Jobs**, right-click the job you just created, and click **Start Job at Step**. The job starts immediately. You can monitor progress in SQL Server Profiler.  
  
2.  As a final step, modify the job to run on a schedule that you define, adding any alerts or notifications necessary to administer the job. You might also want to refine the processing script, or create multiple steps in the job to process objects independently.  
  
## See Also  
 [SQL Server Agent Components](../../ssms/agent/sql-server-agent.md)   
 [Schedule SSAS Administrative Tasks with SQL Server Agent](../../analysis-services/instances/schedule-ssas-administrative-tasks-with-sql-server-agent.md)   
 [Batch Processing &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/batch-processing-analysis-services.md)   
 [Processing a multidimensional model &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md)   
 [Processing Objects &#40;XMLA&#41;](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/processing-objects-xmla.md)  
  
  
