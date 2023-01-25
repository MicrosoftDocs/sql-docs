---
description: "Lesson 3-3 - Testing the Deployed Packages"
title: "Step 3: Testing the Deployed Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: tutorial
ms.assetid: 9159da3f-c9ca-4015-9e85-3bf4373a1349
author: chugugrace
ms.author: chugu
---
# Lesson 3-3 - Testing the Deployed Packages

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]


In this task, you will test the packages that you deployed to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
In other [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] tutorials, you ran packages in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the development environment for [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], using the **Start Debugging** option on the **Debug** menu. This time you will run the packages differently.  
  
[!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] provides several tools that you can use to run packages in the test and production environment: the command prompt utility **dtexec** and the Execute Package Utility. The Execute Package Utility is a graphical tool that is built on **dtexec**. Both of these tools execute the package immediately. In addition, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provides a subsystem of SQL Server Agent that is especially designed for scheduling package execution as a step in a SQL Server Agent job.  
  
You will use the Execute Package Utility to run the deployed packages. The packages will be used as is; therefore, you do not have to update information on any pages in the dialog box. You will run the packages from the General page, which is the first page in the Execute Package Utility. If you like, you can click the other pages too see the information that they contain for each package.  
  
> [!NOTE]  
> To ensure that the packages run successfully in the context of this tutorial, you should not modify any options.  
  
Before you run packages in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] by using the Execute Package Utility, ensure that the Integration Services service is running. The Integration Services service provides support for package storage and execution. If the service is stopped, you cannot connect to [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] and [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] does not list the packages to run. You also must have permissions to run the package on the instance where the package has been deployed. For more information, see [Integration Services Roles &#40;SSIS Service&#41;](../integration-services/security/integration-services-roles-ssis-service.md).  
  
The top-level folders within the Stored Packages folder are the user-defined folders that Integration Services service monitors. You can specify as many or few folders in the MsDtsSrvr.ini.xml file as you want. This tutorial assumes that you are using the default MsDtsSrvr.ini.xml file, and that the names of the top-level folders within Stored Packages are File System and MSDB.  
  
### To connect to Integration Services in SQL Server Management Studio  
  
1.  Click **Start**, point to **All Programs**, point to **Microsoft SQL Server**, and then click **SQL Server Management Studio**.  
  
2.  In the **Connect to Server** dialog box, select **Integration Services** in the **Server type** list, provide a server name in the **Server name** box, and click **Connect**.  
  
    > [!IMPORTANT]  
    > If you cannot connect to [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service is likely not running. To learn the status of the service, click **Start**, point to **All Programs**, point to **Microsoft SQL Server**, point to **Configuration Tools**, and then click **SQL Server Configuration Manager**. In the left pane, click **SQL Server Services**. In the right pane, find the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service. Start the service if it is not already running.  
  
    [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] opens. By default the Object Explorer window is open and placed in the upper right corner of the studio. If Object Explorer is not open, click **Object Explorer** on the **View** menu.  
  
### To run the packages using the Execute Package Utility  
  
1.  In Object Explorer, expand the Stored Packages folder.  
  
2.  Expand the MSDB folder. Because you deployed the packages to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], all the deployed packages are stored in the msdb [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database, and all deployed packages appear in the MSDB folder. The File System folder is empty, unless you have deployed packages to the file system outside of the Deployment Tutorial.  
  
3.  Starting at the top of the package list, right-click DataTransfer, and click **Run Package**.  
  
4.  In the **Execute Package Utility** dialog box, click **Execute**.  
  
5.  In the **Execute Package Utility** dialog box, view the progress and execution results of the package. When the **Stop** button becomes unavailable, which indicates that the package has completed, click **Close**.  
  
    > [!IMPORTANT]  
    > If you click **Stop** while the package is running, the package will not finish.  
  
6.  In the **Execute Package Utility** dialog box, click **Close**.  
  
7.  Repeat steps 3 - 6 for the LoadXML package.  
  
8.  On the **File** menu, click **Exit**.  
  
### To verify the results of the DataTransfer package  
  
1.  On the toolbar in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], click **New Query**.  
  
2.  In the **Connect to Server** dialog box, select **Database Engine** in the **Server type** list, provide the name of the server name on which you installed the tutorial packages or type (local) in the **Server name** box, and select an authentication mode. If using SQL Server Authentication, provide a user name and password.  
  
3.  Click **Connect**.  
  
4.  In the query window, type or paste the following SQL statement:  
  
    `USE AdventureWorks`  
  
    `SELECT * FROM HighIncomeCustomers`  
  
5.  Press **F5** or click the Execute icon on the toolbar.  
  
    The query returns 31 rows of data. The return result contains any rows from the text file, Customers.txt, that have values larger than 100000 in the YearlyIncome column.  
  
6.  Locate the DeploymentTutorial folder, right-click the log XML file, Deployment Tutorial Log, and then click **Open**. You can open the file by using Notepad or the text/XML editor of choice.  
  
### To verify the results of the LoadXMLData package  
  
1.  On the toolbar in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], click **New Query**.  
  
2.  If prompted to connect again, in the **Connect to Server** dialog box, select **Database Engine** in the **Server type** list, provide the name of the server on which you installed the tutorial packages or enter (local) in the **Server name** box, and select an authentication mode. If using SQL Server Authentication, provide a user name and password.  
  
3.  Click **Connect**.  
  
4.  In the query window, type or paste the following SQL statement:  
  
    `USE AdventureWorks`  
  
    `SELECT * FROM OrderDatesByCountryRegion`  
  
5.  Press **F5** or click the Execute icon on the toolbar.  
  
    The query returns 21 rows of data. The return result consists of the rows from the XML data file, orders.xml. Each row is a summary by country/region; the row lists the name of a country/region, the number of orders for each country/region and the dates of the newest and oldest orders.  
  
## See Also  
[dtexec Utility](../integration-services/packages/dtexec-utility.md)  
  
  
  
