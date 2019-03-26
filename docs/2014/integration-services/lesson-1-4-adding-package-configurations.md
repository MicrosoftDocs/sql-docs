---
title: "Step 4: Adding Package Configurations | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: e04a5321-63d5-4ec5-85b9-cb4eaf6c87f6
author: janinezhang
ms.author: janinez
manager: craigg
---
# Step 4: Adding Package Configurations
  In this task, you will add a configuration to each package. Configurations update the values of package properties and package objects at run time.  
  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides a variety of configuration types. You can store configurations in environment variables, registry entries, user-defined variables, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tables, and XML files. To provide additional flexibility, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] supports the use of indirect configurations. This means that you use an environment variable to specify the location of the configuration, which in turn specifies the actual values. The packages in the Deployment Tutorial project use a combination of XML configuration files and indirect configurations. An XML configuration file can include configurations for multiple properties, and when appropriate, can be referenced by multiple packages. In this tutorial, you will use a separate configuration file for each package.  
  
 Configuration files frequently contain sensitive information such as connection strings. Therefore, you should use an access control list (ACL) to restrict access to the location or folder where you store the files, and give access only to users or accounts that are permitted to run packages. For more information, see [Access to Files Used by Packages](../../2014/integration-services/access-to-files-used-by-packages.md).  
  
 The packages (DataTransfer and LoadXMLData) that you added to the Deployment Tutorial project in the previous task need configurations to run successfully after they are deployed to the target server. To implement configurations, you will first create the indirect configurations for the XML configuration files, and then you will create the XML configuration files.  
  
 You will create two configuration files, DataTransferConfig.dtsConfig and LoadXMLData.dtsConfig. These files contain name-value pairs that update the properties in packages that specify the location of the data and log files used by the package. Later, as a step in the deployment process, you will update the values in the configuration files to reflect the new location of the files on the destination computer.  
  
 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] recognizes that the DataTransferConfig.dtsConfig and LoadXMLData.dtsConfig are dependencies of the DataTransfer and LoadXMLData packages, and automatically includes the configuration files when you create the deployment bundle in the next lesson.  
  
### To create indirect configuration for the DataTransfer package  
  
1.  In Solution Explorer, double-click DataTransfer.dtsx.  
  
2.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click anywhere in the background of the control flow design surface.  
  
3.  On the **SSIS** menu, click **Package Configurations**.  
  
4.  In the **Package Configuration Organize**r dialog box, select **Enable package configurations** if it is not already selected, and then click **Add**.  
  
5.  On the welcome page of the Package Configuration Wizard, click **Next**.  
  
6.  On the Select Configuration Type page, select **XML configuration file** in the **Configuration type** list, select the **Configuration location is stored in an environment variable** option, and type `DataTransfer,` or select the **DataTransfer** environment variable in the list.  
  
    > [!NOTE]  
    >  To make the environment variable available in the list, you may have to restart your computer after adding the variable. If you do not want to restart the computer, you can type the name of the environment variable.  
  
7.  Click **Next**.  
  
8.  On the Completing the Wizard page, type **DataTransfer EV Configuration** in the **Configuration name** box, review the configuration contents in the **Preview** pane, and then click **Finish**.  
  
9. Close the **Package Configuration Organize**r dialog box.  
  
### To create the XML configuration for the DataTransfer package  
  
1.  In Solution Explorer, double-click DataTransfer.dtsx.  
  
2.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click anywhere in the background of the control flow design surface.  
  
3.  On the **SSIS** menu, click **Package Configurations**.  
  
4.  In the Package Configuration Organizer dialog box, select the **Enable Package Configurations** check-box, and then click **Add**.  
  
5.  On the welcome page of the Package Configuration Wizard, click **Next**.  
  
6.  On the Select Configuration Type page, select **XML configuration file** in the **Configuration type** list and then click **Browse**.  
  
7.  In **Select Configuration File Location** dialog box, navigate to C:\DeploymentTutorial and type **DataTransferConfig** in the **File name** box, and then click **Save**.  
  
8.  On the Select Configuration Type page, click **Next**.  
  
9. On the Select Properties to Export page, expand DataTransfer, Connection Managers, Deployment Tutorial Log, and Properties, and then select the **Connection String** check-box.  
  
10. Within Connection Managers, expand NewCustomers, and then select the **Connection String** check-box.  
  
11. Click **Next**.  
  
12. On the Completing the Wizard page, type **DataTransfer Configuration** in the **Configuration name** box, review the content of the configuration, and then click **Finish**.  
  
13. In the **Package Configuration Organizer** dialog box, verify that DataTransfer EV Configuration is listed first, and DataTransfer Configuration is listed second, and then click **Close**.  
  
### To create indirect configuration for the LoadXMLData package  
  
1.  In Solution Explorer, double-click LoadXMLData.dtsx.  
  
2.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click anywhere in the background of the control flow design surface.  
  
3.  On the **SSIS** menu, click **Package Configurations**.  
  
4.  In the **Package Configuration Organize**r dialog box, Click **Add**.  
  
5.  On the welcome page of the Package Configuration Wizard, click **Next**.  
  
6.  On the Select Configuration Type page, select **XML configuration file** in the **Configuration type** list, select the **Configuration location is stored in an environment variable** option, type `LoadXMLData` or select the `LoadXMLData` environment variable in the list.  
  
    > [!NOTE]  
    >  To make the environment variable available in the list, you may have to restart your computer after adding the variable.  
  
7.  Click **Next**.  
  
8.  On the Completing the Wizard page, type **LoadXMLData EV Configuration** in the **Configuration name** box, review the content of the configuration, and then click **Finish**.  
  
### To create the XML configuration for the LoadXMLData package  
  
1.  In Solution Explorer, double-click LoadXMLData.dtsx.  
  
2.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click anywhere in the background of the control flow design surface.  
  
3.  On the **SSIS** menu, click **Package Configurations**.  
  
4.  In the Package Configuration Organizer dialog box, select the **Enable Package Configurations** check-box, and click **Add**.  
  
5.  On the welcome page of the Package Configuration Wizard, click **Next**.  
  
6.  On the Select Configuration Type page, select **XML configuration file** in the **Configuration type** list and click **Browse**.  
  
7.  In **Select Configuration File Location** dialog box, navigate to C:\DeploymentTutorial and type **LoadXMLDataConfig** in the **File name** box, and then click **Save**.  
  
8.  On the Select Configuration Type page, click **Next**.  
  
9. On the Select Properties to Export page, expand LoadXMLData, Executables, Load XML Data, and Properties, and then select the **[XMLSource].[XMLData]** and **[XMLSource].[XMLSchemaDefinition]** check boxes.  
  
10. Click **Next**.  
  
11. On the Completing the Wizard page, type **LoadXMLData Configuration** in the **Configuration name** box, review the content of the configuration, and then click **Finish**.  
  
12. In the **Package Configuration Organizer** dialog box, verify that the LoadXMLData EV Configuration is listed first, and the LoadXMLData Configuration is listed second, and then click **Close**.  
  
## Next Task in Lesson  
 [Step 5: Testing the Updated Packages](../integration-services/lesson-1-5-testing-the-updated-packages.md)  
  
![Integration Services icon (small)](media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Package Configurations](../../2014/integration-services/package-configurations.md)   
 [Create Package Configurations](../../2014/integration-services/create-package-configurations.md)   
 [Access to Files Used by Packages](../../2014/integration-services/access-to-files-used-by-packages.md)  
  
  
