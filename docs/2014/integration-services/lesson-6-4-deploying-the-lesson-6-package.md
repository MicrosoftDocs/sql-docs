---
title: "Step 4: Deploying the Lesson 6 Package | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: b613cef7-7993-4d89-a429-a8251d74d435
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 4: Deploying the Lesson 6 Package
  Deploying the package involves adding the package to the SSISDB catalog in Integration Services on an instance of SQL Server. In this lesson you will add the Lesson 6 package to the SSISDB catalog, set the parameter, and execute the package. For this lesson you will use SQL Server Management Studio to add the Lesson 6 package to the SSISDB catalog, and deploy the package. After deploying the package you will modify the parameter to point to a new location then execute the package.  
  
 In this lesson you will:  
  
-   Add the package to the SSISDB catalog in the SSIS node in SQL Server.  
  
-   Deploy the package.  
  
-   Set the package parameter value.  
  
-   Execute the package in SSMS.  
  
### To Locate or add the SSISDB catalog  
  
1.  Click Start, point to All Programs, point to Microsoft SQL Server 2012, and then click SQL Management Studio.  
  
2.  On the Connect to Server dialog box, verify the default settings, and then click Connect. To connect, the Server name box must contain the name of the computer where SQL Server is installed. If the Database Engine is a named instance, the Server name box should also contain the instance name in the format <computer_name>\\<instance_name>.  
  
3.  In Object Explorer expand Integration Services Catalogs.  
  
4.  If there are no catalogs listed under Integration Services Catalogs then add the SSISDB catalog.  
  
5.  To Add the SSISDB catalog, right-click Integration Services Catalogs and click Create Catalog.  
  
6.  On the Create Catalog dialog box select Enable CLR Integration.  
  
7.  In the Password box, type a new password then type it again in the Retype Password box. Be sure to remember the password you type.  
  
8.  Click OK to add the SSISDB catalog.  
  
### To add the package to the SSISDB catalog  
  
1.  In Object Explorer, right-click SSISDB and click Create Folder.  
  
2.  In the Create Folder dialog box type SSIS Tutorial in the Folder name box and click OK.  
  
3.  Expand the SSIS Tutorial folder, right-click Projects, and click Import Packages.  
  
4.  On the Integration Services Project Conversion Wizard Introduction page click Next.  
  
5.  On the Locate Packages page, ensure that File system is selected in the Source list, then click Browse.  
  
6.  On the Browse For Folder dialog box, browse to the folder containing the SSIS Tutorial project, then click OK.  
  
7.  Click Next.  
  
8.  On the Select Packages page you should see all six packages from the SSIS Tutorial. In the Packages list, select Lesson 6.dtsx, then click Next.  
  
9. On the Select Destination page, type SSIS Tutorial Deployment in the Project Name box then click Next.  
  
10. Click Next on each of the remaining wizard pages until you get to the Review page.  
  
11. On the Review page, click Convert.  
  
12. When the conversion completes, click Close.  
  
 When you close the Integration Services Project Conversion Wizard, SSIS displays the Integration Services Deployment Wizard. You will use this wizard now to deploy the Lesson 6 package.  
  
1.  On the Integration Services Deployment Wizard Introduction page, review the steps for deploying the project, then click Next.  
  
2.  On the Select Destination page verify that the server name is the instance of SQL Server containing the SSISDB catalog and that the path shows SSIS Tutorial Deployment, then click Next.  
  
3.  On the Review page, review the Summary then click Deploy.  
  
4.  When the deployment completes, click Close.  
  
5.  In Object Explorer, right-click Integration Services Catalogs and click Refresh.  
  
6.  Expand Integration Services Catalogs then expand SSISDB. Continue to Expand the tree under SSIS Tutorial until you have completely expanded the project. You should see Lesson 6.dtsx under the Packages node of the SSIS Tutorial Deployment node.  
  
 To verify that the package is complete, right-click Lesson 6.dtsx and click Configure. On the Configure dialog box, select Parameters and verify that there is an entry with Lesson 6.dtsx as the Container, VarFolderName as the Name and the path to New Sample Data as the value, then click Close.  
  
 Before continuing create a new sample data folder, name it Sample Data Two, and copy any three of the original sample files into it.  
  
### To create and populate a new sample data folder  
  
1.  In Windows Explorer, at the root level of your drive (for example, C:\\), create a new folder named Sample Data Two.  
  
2.  Open the c:\Program Files\Microsoft SQL Server\110\Samples\Integration Services\Tutorial\Creating a Simple ETL Package\Sample Data folder and then copy any three of the sample files from the folder.  
  
3.  In the New Sample Data folder, paste the copied files.  
  
### To change the package parameter to point to the new sample data  
  
1.  In Object Explorer, right click Lesson 6.dtsx and click Configure.  
  
2.  On the Configure dialog box, change the parameter value to the path to Sample Data Two. For example C:\Sample Data Two if you placed the new folder in the root folder on the C drive.  
  
3.  Click OK to close the Configure dialog box.  
  
### To test the Lesson 6 package deployment  
  
1.  In Object Explorer, right click Lesson 6.dtsx and click Execute.  
  
2.  On the Execute Package dialog box, click OK.  
  
3.  On the message dialog box click Yes to open Overview Report.  
  
 The Overview report for the package is displayed showing the name of the package and a status summary. The Execution Overview section shows the result from each task in the package and the Parameters Used section shows the names and values of all parameters used in the package execution, including VarFolderName.  
  
  
