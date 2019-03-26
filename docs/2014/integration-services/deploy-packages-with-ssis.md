---
title: "SSIS Tutorial: Deploying Packages | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "deployment tutorial [Integration Services]"
  - "deploying packages [Integration Services]"
  - "SSIS, tutorials"
  - "Integration Services, tutorials"
  - "deploying packages [Integration Services], installing"
  - "SQL Server Integration Services, tutorials"
  - "walkthroughs [Integration Services]"
  - "deployment utility [Integration Services]"
  - "deploying packages [Integration Services], configurations"
ms.assetid: de18468c-cff3-48f4-99ec-6863610e5886
author: janinezhang
ms.author: janinez
manager: craigg
---
# SSIS Tutorial: Deploying Packages
  [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides tools that make it easy to deploy packages to another computer. The deployment tools also manage any dependencies, such as configurations and files that the package needs. In this tutorial, you will learn how to use these tools to install packages and their dependencies on a target computer.  
  
 First, you will perform tasks to prepare for deployment. You will create a new [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] and add existing packages and data files to the project. You will not create any new packages from scratch; instead, you will work only with completed packages that were created just for this tutorial. You will not modify the functionality of the packages in this tutorial; however, after you have added the packages to the project, you might find it useful to open the packages in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer and review the contents of each package. By examining the packages, you will learn about package dependencies such as log files and about other interesting features of the packages.  
  
 In preparation for deployment, you will also update the packages to use configurations. Configurations make the properties of packages and package objects updatable at run time. In this tutorial, you will use configurations to update the connection strings of log and text files and the locations of the XML and XSD files that the package uses. For more information, see [Package Configurations](../../2014/integration-services/package-configurations.md) and [Create Package Configurations](../../2014/integration-services/create-package-configurations.md).  
  
 After you have verified that the packages run successfully in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], you will create the deployment bundle to use to install the packages. The deployment bundle will consist of the package files and other items that you added to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, the package dependencies that [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] automatically includes, and the deployment utility that you built. For more information, see [Create a Deployment Utility](../../2014/integration-services/create-a-deployment-utility.md).  
  
 You will then copy the deployment bundle to the target computer and run the Package Installation Wizard to install the packages and package dependencies. The packages will be installed in the msdb SQL Server database, and the supporting and ancillary files will be installed in the file system. Because the deployed packages use configurations, you will update the configuration to use new values that enable packages to run successfully in the new environment.  
  
 Finally, you will run the packages in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] by using the Execute Package Utility.  
  
 It is the goal of this tutorial to simulate the complexity of real-life deployment issues that you may encounter. However, if it is not possible for you to deploy the packages to a different computer, you can still do this tutorial by installing the packages in the msdb database on a local instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], and then running the packages from [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] on the same instance.  
  
## What You Will Learn  
 The best way to become acquainted with the new tools, controls, and features available in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] is to use them. This tutorial walks you through the steps to create an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project and then add the packages and other necessary files to the project. After the project is complete, you will create a deployment bundle, copy the bundle to the destination computer, and then install the packages on the destination computer.  
  
## Requirements  
 This tutorial is intended for users who are already familiar with fundamental file system operations, but who have limited exposure to the new features available in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. To better understand basic [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] concepts that you will put to use in this tutorial, you might find it useful to first complete the following [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] tutorials: [Run the SQL Server Import and Export Wizard](import-export-data/start-the-sql-server-import-and-export-wizard.md) and [SSIS Tutorial: Creating a Simple ETL Package](../integration-services/ssis-how-to-create-an-etl-package.md).  
  
 **Source computer.** The computer on which you will create the deployment bundle must have the following components installed:  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with the AdventureWorks database. To enhance security, the sample databases are not installed by default. You can download the sample database from [CodePlex](http://msftdbprodsamples.codeplex.com/releases/view/125550).  
  
-   You must have permission to create and drop tables in AdventureWorks.  
  
-   This tutorial also requires sample data, completed packages, configurations, and a Readme. The files for these items are installed together with the samples. If you cannot find the sample data, return to the procedure above and complete installation as described.  
  
-   The business intelligence development environment, [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
 **Destination computer.** The computer to which you deploy packages must have the following components installed:  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with the AdventureWorks database.  
  
-   [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].  
  
-   You must have permission to create and drop tables in AdventureWorksand to run packages in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
-   You must have read and write permission on the sysssispackages table in the msdb[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] system database.  
  
 If you plan to deploy packages to the same computer as the one on which you create the deployment bundle, that computer must meet requirements for both the source and destination computers.  
  
 **Estimated time to complete this tutorial:** 2 hours  
  
## Lessons in This Tutorial  
 [Lesson 1: Preparing to Create the Deployment Bundle](../integration-services/lesson-1-preparing-to-create-the-deployment-bundle.md)  
 In this lesson, you will get ready to deploy an ETL solution by creating a new [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project and adding the packages and other required files to the project.  
  
 [Lesson 2: Creating the Deployment Bundle](../integration-services/lesson-2-create-the-deployment-bundle-in-ssis.md)  
 In this lesson, you will build a deployment utility and verify that the deployment bundle includes the necessary files.  
  
 [Lesson 3: Installing Packages](../integration-services/lesson-3-install-ssis-package.md)  
 In this lesson, you will copy the deployment bundle to the target computer, install the packages, and then run the packages.  
  
![Integration Services icon (small)](media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
  
