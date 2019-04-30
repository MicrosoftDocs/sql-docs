---
title: "Step 3: Adding Packages and Other Files | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: a7e6ec9c-d31d-4613-9525-8947a7b358f7
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 1-3 - Adding Packages and Other Files

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


In this task, you will add existing packages, ancillary files that support individual packages, and a Readme to the Deployment Tutorial project that you created in the previous task. For example, you will add an XML data file that contains the data for a package and a text file that provides Readme information about all the packages in the project.  
  
When you deploy packages to a test or production environment, you typically do not include the data files in the deployment, but instead use configurations to update the paths of the data sources to access test or production versions of the data files or databases. For instructional purposes, this tutorial includes data files in the package deployment.  
  
The packages and the sample data that the packages use are installed when you install the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] samples. You will add the following packages to the Deployment Tutorial project:  
  
-   **DataTransfer.** Basic package that extracts data from a flat file, evaluates column values to conditionally keep rows in the dataset, and loads data into a table in the AdventureWorks database.  
  
-   **LoadXMLData.** Data-transfer package that extracts data from an XML data file, evaluates and aggregates column values, and loads data into a table in the AdventureWorks database.  
  
To support the deployment of these packages, you will add the following ancillary files to the Deployment Tutorial project.  
  
|Package|File|  
|-----------|--------|  
|DataTransfer|NewCustomers.txt|  
|LoadXMLData|orders.xml and orders.xsd|  
  
You will also add a Readme, which is a text file that provides information about the Deployment Tutorial project.  
  
The paths used in the following procedures assume that the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] samples were installed in the default location, [!INCLUDE[ssSampPathIS](../includes/sssamppathis-md.md)]. If you installed the samples to a different location, you should use that location instead in the procedures.  
  
In the next task, you will add configurations to the DataTransfer and LoadXMLData packages. All configurations are stored in XML files, and you will use a system environment variable to specify the location of the files. After you create the configuration files, you will add them to the project.  
  
### To add packages to the Deployment Tutorial project  
  
1.  If [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] is not already open, click **Start**, point to **All Programs**, point to **Microsoft SQL Server**, and then click **SQL Server Data Tools**.  
  
2.  On the **File** menu, click **Open**, click **Project/Solution**, click the **Deployment Tutorial** folder and click **Open**, and then double-click **Deployment Tutorial.sln**.  
  
3.  In Solution Explorer, right-click Deployment Tutorial, click **Add**, and then click **Existing Package**.  
  
4.  In the **Add Copy of Existing Package** dialog box, in **Package location**, select **File System**.  
  
5.  Click the browse **(...)** button, navigate to C:\Program Files\Microsoft SQL Server\100\Samples\Integration ServicesTutorial\Deploying Packages\Completed Packages, select **DataTransfer.dtsx**, and then click **Open**.  
  
6.  Click **OK**.  
  
7.  Repeat steps 3 - 6, and this time add LoadXMLData.dtsx, which is found in C:\Program Files\Microsoft SQL Server\100\Samples\Integration Services\Tutorial\Deploying Packages\Completed Packages.  
  
### To add ancillary files to the Deployment Tutorial project  
  
1.  In Solution Explorer, right-click Deployment Tutorial, click **Add**, and then click **Existing Item**.  
  
2.  In the **Add Existing Item - Deployment Tutorial** dialog box, navigate to C:\Program Files\Microsoft SQL Server\100\Samples\Integration Services\Tutorial\Deployment Packages\Sample Data, select orders.xml, orders.xsd, and NewCustomers.txt, and then click **Add**.  
  
3.  In the **Add Existing Item - Deployment Tutorial** dialog box, navigate to C:\Program Files\Microsoft SQL Server\100\Samples\Integration Services\Tutorial\Deployment Packages\\, select Readme.txt and click **Add**.  
  
4.  On the File menu, click **Save All**.  
  
## Next Task in Lesson  
[Step 4: Adding Package Configurations](../integration-services/lesson-1-4-adding-package-configurations.md)  
  
  
  
