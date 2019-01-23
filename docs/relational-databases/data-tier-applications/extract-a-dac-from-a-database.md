---
title: "Extract a DAC From a Database | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2016"
ms.prod: sql
ms.technology: 
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.extractdacwizard.validationandsummary.f1"
  - "sql13.swb.extractdacwizard.introduction.f1"
  - "sql13.swb.extractdacwizard.selectdatapage.f1"
  - "sql13.swb.extractdacwizard.setdacproperties.f1"
  - "sql13.swb.extractdacwizard.buildandsave.f1"
helpviewer_keywords: 
  - "extract DAC"
  - "How to [DAC], extract"
  - "data-tier application [SQL Server], extract"
  - "wizard [DAC], extract"
ms.assetid: ae52a723-91c4-43fd-bcc7-f8de1d1f90e5
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Extract a DAC From a Database
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Use either the **Extract Data-tier Application Wizard** or a Windows PowerShell script to extract a data-tier application (DAC) package from an existing SQL Server database. The extraction process creates a DAC package file that contains definitions of the database objects and their related instance-level elements. For example, a DAC package file contains the database tables, stored procedures, views, and users, along with the logins that map to the database users.  
  
 
## Before you begin  
 You can extract a DAC from databases residing on instances of [!INCLUDE[ssSDS](../../includes/sssds-md.md)], or [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] Service Pack 4 or later. If the extraction process is run against a database that was deployed from a DAC, only the definitions of the objects in the database are extracted. The process does not reference the DAC registered in **msdb** (**master** in [!INCLUDE[ssSDS](../../includes/sssds-md.md)]). The extraction process does not register the DAC definition in the current instance of the Database Engine. For more information about registering a DAC, see [Register a Database As a DAC](../../relational-databases/data-tier-applications/register-a-database-as-a-dac.md).  
  
##  <a name="LimitationsRestrictions"></a> Limitations and restrictions  
 A DAC can only be extracted from a database in [!INCLUDE[ssSDS](../../includes/sssds-md.md)], or [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 4 (SP4) or later. You cannot extract a DAC if the database has objects that are not supported in a DAC, or contained users. For more information about the types of objects supported in a DAC, see [DAC Support For SQL Server Objects and Versions](../../relational-databases/data-tier-applications/dac-support-for-sql-server-objects-and-versions.md).  
  
##  <a name="Permissions"></a> Permissions  
 Extracting a DAC requires at least ALTER ANY LOGIN and database scope VIEW DEFINITION permissions, as well as SELECT permissions on **sys.sql_expression_dependencies**. Extracting a DAC can be done by members of the securityadmin fixed server role who are also members of the database_owner fixed database role in the database from which the DAC is extracted. Members of the sysadmin fixed server role or the built-in SQL Server system administrator account named **sa** can also extract a DAC.  
  
##  <a name="UsingDACExtractWizard"></a> Using the Extract Data-tier Application Wizard  
 **To Extract a DAC Using a Wizard**  
  
1.  In **Object Explorer**, expand the node for the instance containing the database from which the DAC is to be extracted.  
  
2.  Expand the **Databases** node.  
  
3.  Right-click the node for the database from which the DAC is to be extracted, point to **Tasks**, and then select **Extract Data-tier Application...**  
  
4.  Complete the wizard dialogs:  
  
    1.  [Introduction Page](#Introduction)  
  
    2.  [Select Data Page](#SelectData)  
  
    3.  [Set Properties Page](#SetProperties)  
  
    4.  [Validation and Summary Page](#ValidateSummary)  
  
    5.  [Build Package Page](#BuildPackage)  
  
###  <a name="Introduction"></a> Wizard introduction page  
 This page describes the steps for extracting a data-tier application.  
  
 **Do not show this page again.** - Click the check box to stop the page from being displayed in the future.  
  
 **Next >** - Proceeds to the **Choose Method** page.  
  
 **Cancel** - Ends the wizard without extracting a data-tier application from the database.  
  
 [&#91;Extract Wizard&#93;](#UsingDACExtractWizard)  
  
###  <a name="SelectData"></a> Select data page  
Select the reference data that you want to include in your data-tier application (DAC) package file. Including data in your DAC package is optional. The DAC package will already include the schema of all supported database objects and instance objects related to your database.  
  
 You can include up to 10 MB of reference data in your DAC package file. However, for tables to be included in the DAC, they may not contain binary large object (BLOB) data types such as **image** or **varchar(max)**. To extract larger amounts of data for transferring to another database, use SQL Server Integration Services, the bulk copy utility, or one of many other data migration techniques.  
  
 **Database table** - Select the check box next to the database tables which contain the data that you want to include in your DAC package. You can select up to ten tables that have 10,000 rows or less.  
  
 [&#91;Extract Wizard&#93;](#UsingDACExtractWizard)  
  
###  <a name="SetProperties"></a> Set properties page  
 Use this page of the wizard to describe the data-tier application (DAC). These properties are used to identify the DAC and help distinguish it from others.  
  
 **Name** - This name identifies the DAC. It can be different than the name of the DAC package file and should describe your application. For example, if the database is used for a finance application, you may wish to name the DAC Finance.  
  
 **Version (use xx.xx.xx.xx, where x is a number)** - A numeric value that identifies the version of the DAC. The DAC version is used in Visual Studio to identify the version of the DAC that developers are working on. When deploying a DAC, the version is stored in the **msdb** database and can later be viewed under the **Data-tier Applications** node in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **Description:** - Optional. Describes the DAC. When deploying a DAC, the description is stored in the **msdb** database and can later be viewed under the **Data-tier Applications** node in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
 **Save to DAC package file (include .dacpac extension with file name):** - Saves the DAC to a DAC package file, with a .dacpac extension. Click the **Browse** button to specify a name and location for the file.  
  
 **Overwrite existing file** - Select this check box to replace the DAC package file if one already exists with the same name.  
  
###  <a name="ValidateSummary"></a> Validation and summary page  
 On this page, the wizard validates that all database objects are supported by a data-tier application (DAC). It also checks dependencies across database objects to determine the set of objects that can be successfully included in the DAC. After that, it displays the validation report and summarizes the options that you have selected in this wizard. To change an option, click **Previous**. To begin extracting a DAC, click **Next**.  
  
> **NOTE!**If one or more objects are not supported by a DAC, then the **Next** button is disabled and the extraction process may not continue. In such cases, it is recommended to remove the non-supported objects and then run this wizard again.  
  
 **Summary** - A summary of the options you have selected are listed under **DAC properties**. The results of the validation are listed under **DAC objects**. There are three types of results from the validation:  
  
-   **Objects included in DAC successfully**: these objects and their dependencies are supported, and can be included in the DAC successfully.  
  
-   **Objects included in DAC with warnings**: these objects are supported, but depend on other objects that are not supported in a DAC.  
  
-   **Objects not included in DAC**: these objects are not supported and must be removed from the database before successfully extracting a DAC.  
  
 The validation process checks multiple levels of dependencies. For example, if a stored procedure depends on a table that uses the unsupported CLR data type, the stored procedure will be listed under **Objects included in DAC with warnings**.  
  
 If one or more objects are not supported by a DAC, then the **Next** button is disabled and the extraction process will not continue. In such cases, it is recommended to remove the objects that are not supported and then run this wizard again.  
  
 **Save Report** - Enables you to save an HTML-based file that lists all of the objects under the **DAC Objects** node in the summary. This report can be useful when some of your database objects are not supported in a DAC. Use the report to change or remove objects that are not supported, before trying to extract the DAC again.  
  
 ###  <a name="BuildPackage"></a> Build package page  
 Use this page to monitor the progress of the wizard as it extracts the data-tier application (DAC).  
  
 **Action** - During the **Create and save DAC package file** action, the wizard extracts a DAC from your SQL Server database. Then, a DAC package is created in memory and saved to the location you specified. Click on the links in the **Result** column to see the outcome of the corresponding step.  
  
 **Save Report** - Click to save the results of the wizard's progress to a file.  
  
 **Finish** - Click to close the wizard after processing has completed, or if an error occurs.  
   
##  <a name="ExtractDACPowerShell"></a> Extract a DAC using PowerShell  
 **To extract a DAC from a database using the Extract() method in a PowerShell script**  
  
1.  Create a SMO Server object and set it to the instance that contains the database from which the DAC is to be extracted.  
  
2.  Add a variable that specifies the name of the database.  
  
3.  Specify the metadata for the DAC, such as the DAC name, version, and description.  
  
4.  Specify the path and file name for the extracted DAC package file.  
  
5.  Run the Extract method with the information specified above.  
  
### Example (PowerShell)  
 The following example extracts a DAC named MyApplication from a database named MyDB.  
  
```  
## Set a SMO Server object to the default instance on the local computer.  
CD SQLSERVER:\SQL\localhost\DEFAULT  
$srv = get-item .  
  
## Specify the database to extract to a DAC.  
$dbname = "MyDB"  
  
## Specify the DAC metadata.  
$applicationname = "MyApplication"  
$version = "1.0.0.0"  
$description = "This DAC defines the database used by my application."  
  
## Specify the location and name for the extracted DAC package.  
$dacpacPath = "C:\MyDACs\MyApplication.dacpac"  
  
## Extract the DAC.  
$extractionunit = New-Object Microsoft.SqlServer.Management.Dac.DacExtractionUnit($srv, $dbname, $applicationname, $version)  
$extractionunit.Description = $description  
$extractionunit.Extract($dacpacPath)  
```  
  
## See also  
 [Data-tier Applications](../../relational-databases/data-tier-applications/data-tier-applications.md)  
  
  
