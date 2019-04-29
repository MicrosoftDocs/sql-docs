---
title: "Install Sample Data and Projects for the Analysis Services Multidimensional Modeling Tutorial | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: fc475b25-cbb2-408a-901f-9299299538c5
author: minewiskan
ms.author: owend
manager: craigg
---
# Install Sample Data and Projects for the Analysis Services Multidimensional Modeling Tutorial
  Use the instructions and links provided in this topic to install all of the data and project files used in the Analysis Services Tutorials.  
  
## Step 1: Install SQL Server Software  
 The lessons in this tutorial assume that you have the following software installed. All of the following software is installed using SQL Server installation media. For simplicity of deployment, you can install all of the features on a single computer. To install these features, run SQL Server Setup and select them from the Feature Selection page. For more information, see [Install SQL Server 2014 from the Installation Wizard &#40;Setup&#41;](../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md).  
  
-   Database Engine  
  
-   Analysis Services  
  
     Analysis Services is available in these editions only: Evaluation, Enterprise, Business Intelligence, Standard.  
  
     Note that SQL Server Express editions do not include Analysis Services. [Download the Evaluation edition](https://go.microsoft.com/fwlink/?LinkId=392824) if you want to try out the software free of charge.  
  
     By default, Analysis Services is installed as a multidimensional instance, which you can override by choosing Tabular Server Mode in the server configuration page of the Installation Wizard. If you want to run both server modes, rerun SQL Server Setup on the same computer to install a second instance of Analysis Services in the other mode.  
  
-   SQL Server Management Studio  
  
 Optionally, consider installing Excel to browse your multidimensional data as you proceed through the tutorial. Installing Excel enables the **Analyze in Excel** feature that starts Excel using a PivotTable field list that is connected to the cube you are building. Using Excel to browse data is recommended because you can quickly build a pivot report that lets you interact with the data.  
  
 Alternatively, you can browse data using the built-in MDX query designer that is built into [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. The query designer returns the same data, except the data is presented as a flat rowset.  
  
## Step 2: Download SQL Server Data Tools - Business Intelligence for Visual Studio 2012  
 In this release, SQL Server Data Tools is downloaded and installed separately from other SQL Server features. The designers and project templates used to create BI models and reports are now available as a free web download.  
  
-   [Download the Business Intelligence version of SQL Server Data Tools](https://go.microsoft.com/fwlink/p/?LinkID=322038). The file is saved to the Downloads folder. Run setup to install the tool.  
  
     Reboot the computer to complete the installation.  
  
## Step 3: Install Databases  
 An Analysis Services multidimensional model uses transactional data that you import from a relational database management system. For the purposes of this tutorial, you will use the following relational database as your data source.  
  
-   **AdventureWorksDW2012** - This is a relational data warehouse that runs on a Database Engine instance. It provides the original data that will be used by the Analysis Services databases and projects that you build and deploy throughout the tutorial.  
  
     You can use this sample database with [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] as well as [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].  
  
 To install this database, do the following:  
  
1.  Download the [AdventureWorkDW2012](https://go.microsoft.com/fwlink/p/?LinkID=221770) database from the product samples page on codeplex.  
  
     The database file name is AdvntureWorksDW2012_Data.mdf. The file should be in the Downloads folder on your computer.  
  
2.  Copy the AdventureWorksDW2012_Data.mdf file to the data directory of the local SQL Server Database Engine instance. By default, it is located at C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Data.  
  
3.  Start SQL Server Management Studio and connect to the Database Engine instance.  
  
4.  Right-click Databases, click **Attach**.  
  
5.  Click **Add**.  
  
6.  Select the **AdventureWorksDW2012_Data.mdf** database file and click **OK**. If the file is not listed, check the C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Data folder to be sure the file is there.  
  
7.  In database details, remove the Log file entry. The setup program assumes you have a log file, but there is no log file in the sample. A new log file will be created automatically when you attach the database. Select the log file and click **Remove**, and then click **OK** to attach just the primary database file.  
  
## Step 4: Grant Database Permissions  
 The sample projects use data source impersonation settings that specify the security context under which data is imported or processed. By default, the impersonation settings specify the Analysis Services service account for accessing the data. To use this default setting, you must ensure that the service account under which Analysis Services runs has data reader permissions on the **AdventureWorksDW2012** database.  
  
> [!NOTE]  
>  For learning purposes, it is recommended that you use the default service account impersonation option and grant data reader permissions to the service account in SQL Server. Although other impersonation options are available, not all of them are suitable for processing operations. Specifically, the option for using the credentials of the current user is not supported for processing.  
  
1.  Determine the service account. You can use SQL Server Configuration Manager or the Services console application to view account information. If you installed Analysis Services as the default instance, using the default account, the service is running as **NT Service\MSSQLServerOLAPService**.  
  
2.  In Management Studio, connect to the database engine instance.  
  
3.  Expand the Security folder, right-click Logins and select **New Login**.  
  
4.  On the General page, in Login name, type **NT Service\MSSQLServerOLAPService** (or whatever account the service is running as).  
  
5.  Click **User Mapping**.  
  
6.  Select the checkbox next to the **AdventureWorksDW2012** database. Role membership should automatically include **db_datareader** and **public**. Click **OK** to accept the defaults.  
  
## Step 5: Install Projects  
 The tutorial includes sample projects so that you can compare your results against a finished project, or start a lesson that is further on in the sequence.  
  
 The project file for Lesson 4 is particularly important because it provides the basis for not only that lesson, but all the subsequent lessons that follow. In contrast with the previous project files, where the steps in the tutorial result in an exact copy of the completed project files, the Lesson 4 sample project includes new model information that is not found in the model you built in lessons 1 through 3. Lesson 4 assumes that you are starting with a sample project file that is available in the following download.  
  
1.  Download the [Analysis Services Tutorial SQL Server 2012](https://go.microsoft.com/fwlink/p/?LinkID=221866) on the product samples page on codeplex.  
  
     The 2012 tutorials are valid for the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] release.  
  
     The "Analysis Services Tutorial SQL Server 2012.zip" file will be saved to the Downloads folder on your computer.  
  
2.  Move the .zip file to a folder just below the root drive (for example, C:\Tutorial). This step mitigates the "Path too long" error that sometimes occurs if you attempt to unzip the files in the Downloads folder.  
  
3.  Unzip the sample projects: right-click on the file and select **Extract All**. After you extract the files, you should have the following projects installed on your computer:  
  
    -   Lesson 1 Complete  
  
    -   Lesson 2 Complete  
  
    -   Lesson 3 Complete  
  
    -   Lesson 4 Complete  
  
    -   Lesson 4 Start  
  
    -   Lesson 5 Complete  
  
    -   Lesson 6 Complete  
  
    -   Lesson 7 Complete  
  
    -   Lesson 8 Complete  
  
    -   Lesson 9 Complete  
  
    -   Lesson 10 Complete  
  
4.  Remove the read-only permissions on these files. Right-click the parent folder, "Analysis Services Tutorial SQL Server 2012", select **Properties**, and clear the checkbox for **Read-only**. Click **OK**. Apply the changes to this folder, subfolders, and files.  
  
5.  Start [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
6.  Open the solution (.sln) file that corresponds to the lesson you are using. For example, in the folder named "Lesson 1 Complete", you would open the Analysis Services Tutorial.sln file.  
  
7.  Deploy the solution to verify that database permissions and server location information is set up correctly.  
  
     If Analysis Services and the Database Engine are installed as the default instance (MSSQLServer) and all software is running on the same computer, you can click **Deploy Solution** on the Build menu to build and deploy the sample project to the local Analysis Services instance. During deployment, data will be processed (or imported) from the **AdventureWorksDW2012** database on the local Database Engine instance. A new Analysis Services database will be created on the Analysis Services instance that contains the data retrieved from the Database Engine.  
  
     If you encounter errors, review the previous steps on setting up database permissions. Additionally, you might also need to change server names. The default server name is localhost. If your servers are installed on remote computers or as named instances, you must override the default to use a server name that is valid for your installation. Furthermore, if the servers are on remote computers, you might need to configure Windows Firewall to allow access to the servers.  
  
     The server name for connecting to the database engine is specified in the Data Source object of the multidimensional solution (Adventure Works Tutorial), visible in Solution Explorer.  
  
     The server name for connecting to Analysis Services is specified in the Deployment tab of the Property Pages of the project, also visible in Solution Explorer.  
  
8.  Start SQL Server Management Studio. In SQL Server Management Studio, connect to Analysis Services. Verify that a database named **Analysis Services Tutorial** is running on the server.  
  
## Next Step  
 You are now ready to use the tutorial. For more information about how to get started, see [Multidimensional Modeling &#40;Adventure Works Tutorial&#41;](../analysis-services/multidimensional-modeling-adventure-works-tutorial.md).  
  
## See Also  
 [Install SQL Server 2014 from the Installation Wizard &#40;Setup&#41;](../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md)   
 [Configure the Windows Firewall to Allow Analysis Services Access](instances/configure-the-windows-firewall-to-allow-analysis-services-access.md)   
 [Configure the Windows Firewall to Allow SQL Server Access](../../2014/sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)  
  
  
