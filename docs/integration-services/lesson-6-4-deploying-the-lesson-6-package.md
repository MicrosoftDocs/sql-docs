---
title: "Step 4: Deploy the Lesson 6 package | Microsoft Docs"
ms.custom: ""
ms.date: "01/11/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: b613cef7-7993-4d89-a429-a8251d74d435
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 6-4: Deploy the Lesson 6 package

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]



Deploying the package involves adding the package to the SSISDB catalog in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] on an instance of SQL Server. In this lesson, you add the Lesson 6 package to the SSISDB catalog, set the new parameter, and execute the package. For this lesson, you use SQL Server Management Studio to add the Lesson 6 package to the SSISDB catalog, and deploy the package. After deploying the package, you modify the parameter to point to a new location and then run the package.   
In this task, you:  

1. Add the package to the SSISDB catalog in the SSIS node in SQL Server.  
  
2. Deploy the package.  
  
3. Set the package parameter value.  

4. Execute the package in SSMS.  
  
## Locate or add the SSISDB catalog  
  
1.  Select **Start** > **All Programs** > **Microsoft SQL Server 2017**, and then select **SQL Management Studio**.  
  
2.  On the **Connect to Server** dialog box, verify the default settings, and then select **Connect**. To connect, the **Server** name must be the name of the computer where SQL Server is installed. If the **Database Engine** is a named instance, the **Server** name must be the instance name in the format *\<computer_name>\\\<instance_name>*. 
  
3.  In **Object Explorer**, expand **Integration Services Catalogs**.  
  
4.  If there are no catalogs listed under **Integration Services Catalogs**, then add the SSISDB catalog.  
  
5.  To add the SSISDB catalog, right-click **Integration Services Catalogs** and select **Create Catalog**.  
  
6.  On the **Create Catalog** dialog box, select **Enable CLR Integration**.  
  
7.  In the **Password** box, enter a password and then enter it again in the **Retype Password** box. 
  
8.  Select **OK** to add the SSISDB catalog.  
  
## Add the package to the SSISDB catalog  
  
1.  In **Object Explorer**, right-click **SSISDB** and select **Create Folder**.  
  
2.  In the **Create Folder** dialog box, enter SSIS Tutorial in the Folder name box, and select **OK**.  
  
3.  Expand the **SSIS Tutorial** folder, right-click **Projects**, and select **Import Packages**.  
  
4.  On the **Integration Services Project Conversion Wizard** **Introduction** page, select **Next**.  
  
5.  On the **Locate Packages** page, make sure that **File system** is selected in the **Source** list, then select **Browse**.  
  
6.  On the **Browse For Folder** dialog box, browse to the folder containing this SSIS Tutorial project, then select **OK**.  
  
7.  Select **Next**.  
  
8.  On the Select Packages page, you should see all six packages from the SSIS Tutorial. In the **Packages** list, select **Lesson 6.dtsx**, then select **Next**.  
  
9. On the **Select Destination** page, enter **SSIS Tutorial Deployment** in the **Project Name** box then select **Next**.

10. Select **Next** on each of the remaining wizard pages until you get to the **Review** page.  
  
11. On the **Review** page, select **Convert**.  
  
12. After the conversion completes, select **Close**.  
  
When you close the Integration Services Project Conversion Wizard, SSIS displays the Integration Services Deployment Wizard. You use this wizard now to deploy the Lesson 6 package.  
  
1.  On the **Integration Services Deployment Wizard** **Introduction** page, review the steps for deploying the project, then select **Next**.  
  
2.  On the **Select Destination** page, verify the server name is the instance of SQL Server containing the SSISDB catalog, and the path shows **SSIS Tutorial Deployment**, and then select **Next**.  
  
3.  On the **Review** page, review the **Summary** then select **Deploy**.  
  
4.  When the deployment completes, select **Close**.  
  
5.  In **Object Explorer**, right-click **Integration Services Catalogs** and select **Refresh**.  
  
6.  Expand **Integration Services Catalogs** then expand **SSISDB**. Continue to expand the tree under **SSIS Tutorial** until you have completely expanded the project. You should see **Lesson 6.dtsx** under the **Packages** node of the **SSIS Tutorial Deployment** node.  
  
7.  To verify that the package is complete, right-click **Lesson 6.dtsx** and select **Configure**. On the **Configure** dialog box, select **Parameters**, and verify that there is an entry with **Lesson 6.dtsx** as the **Container**, **VarFolderName** as the **Name**, and the path to **New Sample Data** as the value, and then select **Close**.  
  
## Create and populate a new sample data folder  
  
1.  In **Windows Explorer**, at the root level of your drive (for example, **C:\\**), create a folder named **Sample Data Two**.  
  
2.  Open the **Sample Data** folder from the [Lesson 1 prerequisites](../integration-services/lesson-1-create-a-project-and-basic-package-with-ssis.md#prerequisites) and then copy any three of the sample files.  
  
3.  Browse to the **Sample Data Two** folder and paste the copied files.  
  
## Change the package parameter to point to the new sample data  
  
1.  In **Object Explorer**, right-click **Lesson 6.dtsx**, and select **Configure**.  
  
2.  On the **Configure** dialog box, change the parameter value to the path to **Sample Data Two**, for example, **C:\\Sample Data Two**.  
  
3.  Select **OK** to close the **Configure** dialog box.  
  
## Test the Lesson 6 package deployment  
  
1.  In **Object Explorer**, right-click **Lesson 6.dtsx** and select **Execute**.  
  
2.  On the **Execute Package** dialog box, select **OK**.  
  
3.  On the message dialog box, select **Yes** to open the **Overview Report**.  
  
The **Overview Report** for the package displays the name of the package and a status summary. The **Execution Overview** section shows the result from each task in the package. The **Parameters Used** section shows the names and values of all parameters used in the package execution, including **VarFolderName**.  
  
