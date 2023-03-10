---
title: "Step 4: Deploy the Lesson 6 package"
description: "Lesson 6-4: Deploy the Lesson 6 package"
author: chugugrace
ms.author: chugu
ms.reviewer: randolphwest
ms.date: 03/09/2023
ms.service: sql
ms.subservice: integration-services
ms.topic: tutorial
---
# Lesson 6-4: Deploy the Lesson 6 package

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]

Deploying the package involves adding the package to the SSISDB catalog in [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] on an instance of SQL Server. In this lesson, you add the Lesson 6 package to the SSISDB catalog, set the new parameter, and execute the package. For this lesson, you use SQL Server Management Studio to add the Lesson 6 package to the SSISDB catalog, and deploy the package. After deploying the package, you modify the parameter to point to a new location, and then run the package.  
In this task, you:

1. Add the package to the SSISDB catalog in the SSIS node in SQL Server.

1. Deploy the package.

1. Set the package parameter value.

1. Execute the package in SSMS.

## Locate or add the SSISDB catalog

1. Select **Start** > **All Programs** > **Microsoft SQL Server 2017**, and then select **SQL Management Studio**.

1. On the **Connect to Server** dialog box, verify the default settings, and then select **Connect**. To connect, the **Server** name must be the name of the computer where SQL Server is installed. If the **Database Engine** is a named instance, the **Server** name must be the instance name in the format *\<computer_name>\\\<instance_name>*.

1. In **Object Explorer**, expand **Integration Services Catalogs**.

1. If there are no catalogs listed under **Integration Services Catalogs**, then add the SSISDB catalog.

1. To add the SSISDB catalog, right-click **Integration Services Catalogs** and select **Create Catalog**.

1. On the **Create Catalog** dialog box, select **Enable CLR Integration**.

1. In the **Password** box, enter a password and then enter it again in the **Retype Password** box.

1. Select **OK** to add the SSISDB catalog.

## Add the package to the SSISDB catalog

1. In **Object Explorer**, right-click **SSISDB** and select **Create Folder**.

1. In the **Create Folder** dialog box, enter SSIS Tutorial in the Folder name box, and select **OK**.

1. Expand the **SSIS Tutorial** folder, right-click **Projects**, and select **Import Packages**.

1. On the **Integration Services Project Conversion Wizard** **Introduction** page, select **Next**.

1. On the **Locate Packages** page, make sure that **File system** is selected in the **Source** list, then select **Browse**.

1. On the **Browse For Folder** dialog box, browse to the folder containing this SSIS Tutorial project, then select **OK**.

1. Select **Next**.

1. On the Select Packages page, you should see all six packages from the SSIS Tutorial. In the **Packages** list, select **Lesson 6.dtsx**, then select **Next**.

   > [!IMPORTANT]
   The previous step may result in an error stating that `One or more selected packages are not ready. Review the Status column for more information.` with a status message including `The version or pipeline version or both for the specified component is higher than the current version. This package was probably created on a new version of DTS or the component than is installed on the current PC.` To resolve this error, change the Project Properties in Visual Studio as follows:
   >
   > 1. Right-click the **SSIS Tutorial (package deployment)** project and select **Properties**.
   >
   > 1. Under **Configuration Properties**, select **General**.
   >
   > 1. Set the **Deployment Target Version** to an older version. For example, change from **SQL Server 2022** to **SQL Server 2019**, then select **OK**.
   >
   > 1. Retry the previous step.

1. On the **Select Destination** page, enter **SSIS Tutorial Deployment** in the **Project Name** box then select **Next**.

1. Select **Next** on each of the remaining wizard pages until you get to the **Review** page.

1. On the **Review** page, select **Convert**.

1. After the conversion completes, select **Close**.

When you close the Integration Services Project Conversion Wizard, SSIS displays the Integration Services Deployment Wizard. You use this wizard now to deploy the Lesson 6 package.

1. On the **Integration Services Deployment Wizard** **Introduction** page, review the steps for deploying the project, then select **Next**.

1. On the **Select Destination** page, verify the server name is the instance of SQL Server containing the SSISDB catalog, and select **Connect**.

1. The path should show **SSIS Tutorial Deployment**, then select **Next**.

1. On the **Review** page, review the **Summary** then select **Deploy**.

1. When the deployment completes, select **Close**.

1. In **Object Explorer**, right-click **Integration Services Catalogs** and select **Refresh**.

1. Expand **Integration Services Catalogs** then expand **SSISDB**. Continue to expand the tree under **SSIS Tutorial** until you have completely expanded the project. You should see **Lesson 6.dtsx** under the **Packages** node of the **SSIS Tutorial Deployment** node.

1. To verify that the package is complete, right-click **Lesson 6.dtsx** and select **Configure**. On the **Configure** dialog box, select **Parameters**, and verify that there is an entry with **Lesson 6.dtsx** as the **Container**, **VarFolderName** as the **Name**, and the path to **New Sample Data** as the value, and then select **Cancel**.

## Create and populate a new sample data folder

1. In **Windows Explorer**, at the root level of your drive (for example, **C:\\**), create a folder named **Sample Data Two**.

1. Open the **Sample Data** folder from the [Lesson 1 prerequisites](../integration-services/lesson-1-create-a-project-and-basic-package-with-ssis.md#prerequisites) and then copy any three of the sample files.

1. Browse to the **Sample Data Two** folder and paste the copied files.

## Change the package parameter to point to the new sample data

1. In **Object Explorer**, right-click **Lesson 6.dtsx**, and select **Configure**.

1. On the **Configure** dialog box, change the parameter value to the path to **Sample Data Two**, for example, **C:\\Sample Data Two**.

1. Select **OK** to close the **Configure** dialog box.

## Test the Lesson 6 package deployment

1. In **Object Explorer**, right-click **Lesson 6.dtsx** and select **Execute**.

1. On the **Execute Package** dialog box, select **OK**.

1. On the message dialog box, select **Yes** to open the **Overview Report**.

The **Overview Report** for the package displays the name of the package and a status summary. The **Execution Overview** section shows the result from each task in the package. The **Parameters Used** section shows the names and values of all parameters used in the package execution, including **VarFolderName**.
