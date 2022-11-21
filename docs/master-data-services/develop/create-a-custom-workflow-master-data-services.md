---
description: "Create a Custom Workflow (Master Data Services)"
title: Create a Custom Workflow
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services

ms.topic: "reference"
ms.assetid: 8e4403e9-595c-4b6b-9d0c-f6ae1b2bc99d
author: CordeliaGrey
ms.author: jiwang6
---
# Create a Custom Workflow (Master Data Services)

[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] uses business rules to create basic workflow solutions, such as to automatically update and validate data and have e-mail notifications sent, based on conditions you specify. When you require processing that is more complex than what the built-in workflow actions provide, use a custom workflow. A custom workflow is a .NET assembly that you create. When your workflow assembly is called, your code can take whatever action your situation requires. For example, if your workflow requires complex event processing, such as multi-tiered approvals or complicated decision trees, you can configure [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] to start a custom workflow that analyzes the data and determines where to send it for approval.  
  
## How Custom Workflows Are Processed  
 There are three main components involved to process custom workflows: the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, SQL Server MDS Workflow Integration Service, and the workflow handler assembly. These components process a custom workflow as follows:  
  
1.  You use [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] to validate an entity that starts a workflow.  
  
2.  [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] sends members that meet the business rule conditions to a Service Broker queue in the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database.  
  
3.  At regular intervals, SQL Server MDS Workflow Integration Service calls a stored procedure in the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database.  
  
4.  When this stored procedure finds records in the Service Broker queue, it returns them to SQL Server MDS Workflow Integration Service.  
  
5.  SQL Server MDS Workflow Integration Services routes the data to your workflow handler assembly.  
  
> [!NOTE]  
>  Note: SQL Server MDS Workflow Integration Service is meant to trigger simple processes. If your custom code requires complex processing, complete your processing either in a separate thread or outside of the workflow process.  
  
## Configure Master Data Services for Custom Workflows  
 Creating a custom workflow requires writing some custom code and configuring [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] to pass workflow data to your workflow handler. Follow these steps to enable custom workflow processing:  
  
1.  Create a .NET assembly that implements [Microsoft.MasterDataServices.WorkflowTypeExtender.IWorkflowTypeExtender](/previous-versions/sql/sql-server-2016/hh758785(v=sql.130)).  
  
2.  Configure SQL Server MDS Workflow Integration Service to connect to your [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database and to associate a tag with your workflow handler.  
  
3.  Start SQL Server MDS Workflow Integration Service.  
  
4.  Create a business rule in [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] that starts a workflow that is tagged with the name of your workflow handler.  
  
5.  Apply the business rule to a member that triggers your custom workflow.  
  
### Create the Workflow Handler Assembly  
 A custom workflow is a .NET class library assembly that implements the [Microsoft.MasterDataServices.WorkflowTypeExtender.IWorkflowTypeExtender](/previous-versions/sql/sql-server-2016/hh758785(v=sql.130)) interface. SQL Server MDS Workflow Integration Service calls the [Microsoft.MasterDataServices.WorkflowTypeExtender.IWorkflowTypeExtender.StartWorkflow*](/previous-versions/sql/sql-server-2016/hh759009(v=sql.130))  method to run your code. For example code that implements [Microsoft.MasterDataServices.WorkflowTypeExtender.IWorkflowTypeExtender.StartWorkflow*](/previous-versions/sql/sql-server-2016/hh759009(v=sql.130)) , see [Custom Workflow Example &#40;Master Data Services&#41;](../../master-data-services/develop/create-a-custom-workflow-example.md).  
  
 Follow these steps to use Visual Studio 2010 to create an assembly that SQL Server MDS Workflow Integration Service can call to handle a custom workflow:  
  
1.  In Visual Studio 2010, create a new **Class Library** project that uses the language of your choice. To create a C# Class Library, select the **Visual C#\Windows** project types and select the **Class Library** template. Enter a name for your project, such as **MDSWorkflowTest**, and click **OK**.  
  
2.  Add a reference to Microsoft.MasterDataServices.WorkflowTypeExtender.dll. This assembly can be found in \<Your installation folder>\Master Data Services\WebApplication\bin.  
  
3.  Add 'using Microsoft.MasterDataServices.Core.Workflow;' to your C# code file.  
  
4.  Inherit from [Microsoft.MasterDataServices.WorkflowTypeExtender.IWorkflowTypeExtender](/previous-versions/sql/sql-server-2016/hh758785(v=sql.130)) in your class declaration. The class declaration should be similar to: 'public class WorkflowTester : IWorkflowTypeExtender'.  
  
5.  Implement the [Microsoft.MasterDataServices.WorkflowTypeExtender.IWorkflowTypeExtender](/previous-versions/sql/sql-server-2016/hh758785(v=sql.130)) interface. The [Microsoft.MasterDataServices.WorkflowTypeExtender.IWorkflowTypeExtender.StartWorkflow*](/previous-versions/sql/sql-server-2016/hh759009(v=sql.130))  method is called by SQL Server MDS Workflow Integration Service to start your workflow.  
  
6.  Copy your assembly to the location of the SQL Server MDS Workflow Integration Service executable, named Microsoft.MasterDataServices.Workflow.exe, in \<Your installation folder>\Master Data Services\WebApplication\bin.  
  
### Configure SQL Server MDS Workflow Integration Service  
 Edit the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] configuration file to include connection information for your [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database and to associate a tag with your workflow handler assembly by following these steps:  
  
1.  Find Microsoft.MasterDataServices.Workflow.exe.config in \<Your installation folder>\Master Data Services\WebApplication\bin.  
  
2.  Add the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database connection information to the "ConnectionString" setting. If your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation uses case-sensitive collation, the name of the database must be entered in the same case as in the database. For example, the complete setting tag might look like this:  
  
    ```xml  
    <setting name="ConnectionString" serializeAs="String">  
        <value>Server=myServer;Database=myDatabase;Integrated Security=True</value>  
    </setting>  
    ```  
  
3.  Below the "ConnectionString" setting add a "WorkflowTypeExtenders" setting to associate a tag name with your workflow handler assembly. For example:  
  
    ```xml  
    <setting name="WorkflowTypeExtenders" serializeAs="String">  
        <value>TEST=MDSWorkflowTestLib.WorkflowTester, MDSWorkflowTestLib</value>  
    </setting>  
    ```  
  
     The inner text of the \<value> tag is in the form of \<Workflow tag>=\<assembly-qualified workflow type name>. \<Workflow tag> is a name you use to identify the workflow handler assembly when you create a business rule in [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)]. \<assembly-qualified workflow type name> is the namespace-qualified name of your workflow class, followed by a comma, followed by the display name of the assembly. If your assembly is strongly named, you also have to include version information and its PublicKeyToken. You can include multiple \<setting> tags if you have created multiple workflow handlers for different kinds of workflows.  
  
> [!NOTE]  
>  Depending on the configuration of your server, you may see an "Access is denied" error when you try to save the Microsoft.MasterDataServices.Workflow.exe.config file. If this occurs, temporarily disable User Account Control (UAC) on the server. To do this, open Control Panel, click **System and Security**. Under **Action Center**, click **Change User Account Control Settings**. In the **User Account Control Settings** dialog, slide the bar to the bottom so that you are never notified. Restart your computer and repeat the preceding steps to edit your configuration file. After saving the file, reset your UAC settings to the default level.  
  
### Start SQL Server MDS Workflow Integration Service  
 By default, SQL Server MDS Workflow Integration Service is not installed. You must install the service before it can be used. For the greatest security, create a local user for the service and grant this user only the permissions needed to perform workflow operations. To create a user, install the service, and start the service, follow these steps:  
  
1.  Use the Local Users and Groups manager to create a local user named, for example, mds_workflow_service.  
  
2.  Use SQL Server Management Studio to grant the mds_workflow_service user permission to execute the [mdm].[udpExternalActionsGet] stored procedure. To do this, create a new login for the mds_workflow_service account, create a new user in the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database, map this user to the mds_workflow_service login, and grant the user EXECUTE permission to the [mdm].[udpExternalActionsGet] stored procedure.  
  
3.  Grant the mds_workflow_service user permission to execute the workflow handler assembly. To do this, add the mds_workflow_service user to the **Security** tab of the **Properties** of the workflow handler assembly and grant the mds_workflow_service user READ and EXECUTE permission.  
  
4.  Grant the mds_workflow_service user permission to execute the SQL Server MDS Workflow Integration Service executable. To do this, add the mds_workflow_service user to the **Security** tab of the **Properties** of Microsoft.MasterDataServices.Workflow.exe, in \<Your installation folder>\Master Data Services\WebApplication\bin and grant the mds_workflow_service user READ and EXECUTE permission.  
  
5.  Install SQL Server MDS Workflow Integration Service by using the .NET installation utility, named InstallUtil.exe. InstallUtil.exe can be found in the .NET installation folder, such as C:\Windows\Microsoft.NET\Framework\v4.0.30319\\. Install SQL Server MDS Workflow Integration Service by entering the following in an elevated command prompt:  
  
    ```  
    C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil Microsoft.MasterDataServices.Workflow.exe  
    ```  
  
     Specify the mds_workflow_service user when prompted during installation.  
  
6.  Start SQL Server MDS Workflow Integration Service by using the Services snap-in. To do this, find SQL Server MDS Workflow Integration Service in the Services snap-in, select it, and click the  **Start** link.  
  
### Create a Workflow Business Rule  
 Use [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] to create and publish a business rule that will start the workflow when applied. You should ensure that your business rule contains actions that change attribute values, so that the rule evaluates to false after it has been applied once. For example, your business rule might evaluate to true when a Price attribute value is greater than 500 and the Approved attribute value is blank. The rule can then include two actions: one to set the Approved attribute value to Pending and one to start the workflow. Alternatively, you may want to create a rule that uses the "has changed" condition and add your attributes to change tracking groups. For more information about business rules, see [Business Rules &#40;Master Data Services&#41;](../../master-data-services/business-rules-master-data-services.md).  
  
 Create a business rule that starts a custom workflow in [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] by following these steps:  
  
1.  In the business rule editor of [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)], after you have specified the conditions of your business rule, drag the **Start Workflow** action from the **External actions** list to the **THEN** pane's **Action** label.  
  
2.  In the **Edit Action** pane, in the **Workflow type** box, type the tag that identifies your workflow handler assembly. This is the tag you specified in the configuration file for your assembly, for example, TEST.  
  
3.  Optionally, select the **Include member data** check box. Choose this to include attribute names and values in the XML that is passed to the workflow handler.  
  
4.  In the **Workflow site** box, type the name of a website. For your custom workflow this may not apply, but can be used for added context.  
  
5.  In the **Workflow name** box, type the name of your workflow from Visual Studio. For your custom workflow this may not apply, but can be used for added context.  
  
6.  Save and publish the business rule.  
  
### Apply Business Rules to Start a Workflow  
 Apply the business rule to your data to start the workflow. To do this, use [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] to edit the entity that contains the members you want to validate. Click **Apply business rules**. In response to the business rule, [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] populates the Service Broker queue of the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database. When SQL Server MDS Workflow Integration Service checks the queue, it sends the data to the specified workflow handler assembly and clears the queue. The workflow handler assembly performs whatever actions you have coded into it.  
  
## Troubleshoot Custom Workflows  
 If your workflow handler assembly doesn't receive data, you can try debugging SQL Server MDS Workflow Integration Service or viewing the Service Broker queue.  
  
### Debug SQL Server MDS Workflow Integration Service  
 To debug SQL Server Workflow Integration Service, take the following steps:  
  
1.  Use the Services snap-in to stop the service.  
  
2.  Open a command prompt, navigate to the location of the service, and run the service in console mode by entering: Microsoft.MasterDataServices.Workflow.exe -console.  
  
3.  In [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)], update your member and apply business rules again. Detailed logs are displayed in the console window.  
  
### View the Service Broker Queue  
 The Service Broker queue that contains the master data passed as part of the workflow is: mdm.microsoft/mdm/queue/externalaction. Queues can be found in the **Object Explorer** of SQL Management Studio under the Service Broker node of the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database. Be aware that, if the service cleared the queue properly, this queue will be empty.  
  
## See Also  
 [Custom Workflow Example &#40;Master Data Services&#41;](../../master-data-services/develop/create-a-custom-workflow-example.md)   
 [Custom Workflow XML Description &#40;Master Data Services&#41;](../../master-data-services/develop/create-a-custom-workflow-xml-description.md)  
  
  
