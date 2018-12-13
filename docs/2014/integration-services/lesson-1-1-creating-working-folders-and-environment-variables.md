---
title: "Step 1: Creating Working Folders and Environment Variables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 45091ba2-ea3d-4399-9814-489d812b42cc
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 1: Creating Working Folders and Environment Variables
  In this task, you will create the working folder (C:\DeploymentTutorial) and the new system environment variables (`DataTransfer` and `LoadXMLData`) that you will use in later tutorial tasks.  
  
 The working folder is at the root of the C drive. If you must use a different drive or location, you can do that. However, you need to note this location and then use it wherever the tutorial refers to the location of the DeploymentTutorial working folder.  
  
 In a later lesson, you will deploy packages that are saved to the file system to the sysssispackages table in the msdb[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database. Ideally you will deploy the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages to a different computer. If that is not possible, you can still learn a lot from doing this tutorial by deploying the packages to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that is on the local computer. The environment variables that are used on the local and destination computers have the same variable names, but different values are stored in the variables. For example, on the local computer, the value of the environment variable `DataTransfer` references the C:\DeploymentTutorial folder, whereas on the target computer the environment variable `DataTransfer` references the C:\DeploymentTutorialInstall folder.  
  
 If you plan to deploy to the local computer, you need to create only one set of environment variables; however, you will need to update the value of the environment variables to an appropriate value before you do the local deployment.  
  
 If you plan to deploy the packages to a different computer, you must create two sets of environment variables: one set for the local computer, and one set for the destination computer. You can create only the variables for the source computer now, and create the variables for the destination computer later, but you must have both the folder and environment variables available on the destination computer before you can install the packages on that computer.  
  
### To create the local working folder  
  
1.  Right-click the Start menu, and click Explore.  
  
2.  Click **Local Disk (C:)**.  
  
3.  On the **File** menu, point to **New**, and then click **Folder**.  
  
4.  Rename New Folder to `DeploymentTutorial`.  
  
### To create local environment variables  
  
1.  On the **Start** menu, click **Control Panel**.  
  
2.  In Control Panel, double-click **System**.  
  
3.  In the **System Properties** dialog box, click the **Advanced** tab, and then click **Environment Variables**.  
  
4.  In the **Environment Variables** dialog box, in the **System variables** frame, click **New**.  
  
5.  In the **New System Variable** dialog box, type `DataTransfer` in the **Variable name** box, and `C:\DeploymentTutorial\datatransferconfig.dtsconfig` in the **Variable value** box.  
  
6.  Click **OK**.  
  
7.  Click **New** again, and type `LoadXMLData` in the **Variable name** box, and `C:\DeploymentTutorial\loadxmldataconfig.dtsconfig` in the **Variable value** box.  
  
8.  Click **OK** to exit the **Environment Variables** dialog box.  
  
9. Click **OK** to exit the **System Properties** dialog box.\  
  
10. Optionally, restart your computer. If you do not restart the computer, the name of the new variable will not be displayed in the Package Configuration Wizard, but you can still use it.  
  
### To create destination environment variables  
  
1.  On the **Start** menu, click **Control Panel**.  
  
2.  In Control Panel, double-click **System**.  
  
3.  In the **System Properties** dialog box, click the **Advanced** tab, and then click **Environment Variables**.  
  
4.  In the **Environment Variables** dialog box, in **System variables** frame, click **New**.  
  
5.  In the **New System Variables** dialog box, type `DataTransfer` in the **Variable name** box, and `C:\DeploymentTutorialInstall\datatransferconfig.dtsconfig` in the **Variable value** box.  
  
6.  Click **OK**.  
  
7.  Click **New** again, and type `LoadXMLData` in the **Variable name** box, and `C:\DeploymentTutorialInstall\loadxmldataconfig.dtsconfig` in the **Variable value** box.  
  
8.  Click **OK** to exit the **Environment Variables** dialog box.  
  
9. Click **OK** to exit the **System Properties** dialog box.\  
  
10. Optionally, restart your computer.  
  
## Next Task in Lesson  
 [Step 2: Creating the Deployment Project](../integration-services/lesson-1-2-creating-the-deployment-project.md)  
  
![Integration Services icon (small)](media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
  
