---
title: "Changelog for SQL Server Data Tools (SSDT) | Microsoft Docs"
ms.custom: ""
ms.date: "01/30/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssdt"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: b071f8b8-c8e5-44e0-bbb6-04804dd1863a
caps.latest.revision: 31
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Changelog for SQL Server Data Tools (SSDT)
This change log is for [SQL Server Data Tools (SSDT) for Visual Studio 2015](https://msdn.microsoft.com/library/mt204009.aspx), co-released with [SQL Server 2016](https://msdn.microsoft.com/library/ms130214.aspx).  
  
For detailed posts about what’s new and changed, please visit [the SSDT Team blog](https://blogs.msdn.microsoft.com/ssdt/).  

## SSDT 16.5 (for SQL Server 2016)
Released: October 20, 2016

Build number: 14.0.61021.0

**What's New?**


### Connection Improvements

* The new search box in the **Browse** tab helps you filter your Local servers, Network servers, and Azure SQL databases. This is useful if you have a large number of servers or databases appearing in these lists.
* The **History** tab has right-click menu options to pin / unpin favorites, and a new option to remove connections from history.

### SqlPackage and DacFx API Improvements

Using SqlPackage.exe and the DacFx APIs you can now generate a deployment report, deployment script, and publish to a database all in one action. This is a timesaver for anyone who likes to keep a report of what was published during a deployment. Another benefit is that for Azure scenarios, separate scripts for the master database and the deploy target database are created. Up to now a single script was created which was not useful for repeated deployments.

For SqlPackage’s Publish and Script actions, two new arguments have been added.

* DeployScriptPath (shortname: dsp). This is an optional path to write the deployment script to. For Azure deployment, if there were TSQL commands to create of modify the DB a master script will be written to the same path but with “Filename_Master.sql” as the output file name.
* DeployReportPath (shortname: drp). This is an optional path to write the deployment report to.

Note that for the Script action, either the existing Output Path arguments or the new script/report-specific arguments should be used, but not both.

Sample usage:

**Publish Action**

```Sqlpackage.exe /a:Publish /tsn:(localdb)\ProjectsV13 /tdn:MyDatabase /deployscriptpath:”My\DeployScript.sql” /deployreportpath:”My\DeployReport.xml”```

**Script Action**

```Sqlpackage.exe /a:Script /tsn:(localdb)\ProjectsV13 /tdn:MyDatabase /deployscriptpath:”My\DeployScript.sql” /deployreportpath:”My\DeployReport.xml”```

In DacFx, two new APIs have been added: DacServices.Publish() and DacServices.Script(). These also support performing publish + script + report actions in a single operation. Sample usage:

```
DacServices service = new DacServices(connectionString);
using(DacPackage package = DacPackage.Load(@"C:\My\db.dacpac")) {
var options = new PublishOptions() {
    GenerateDeploymentScript = true, // Should a deployment script be created?
    GenerateDeploymentReport = true, // Should an xml deploy report be created?
    DatabaseScriptPath = @"C:\My\OutputScript.sql", // optional path to save script to
    MasterDbScriptPath = @"C:\My\OutputScript_Master.sql", // optional path to save master script to
    DeployOptions = new DacDeployOptions()
};

// Call publish and receive deployment script & report in the results
PublishResult result = service.Publish(package, "TargetDb", options);
Console.WriteLine(result.DatabaseScript);
Console.WriteLine(result.MasterDbScript);
Console.WriteLine(result.DeploymentReport);

// Call script and receive deployment script & report in results
result = service.Script(package, "TargetDb", options);
Console.WriteLine(result.DatabaseScript);
Console.WriteLine(result.MasterDbScript);
Console.WriteLine(result.DeploymentReport);
```

**Analysis Services & Reporting Services**

SSAS tabular designer DAX parser has improved performance when working with large DAX expressions.
For more information, please read the [Analysis Services blog post](https://blogs.msdn.microsoft.com/analysisservices/2016/09/20/introducing-integrated-workspace-mode-for-sql-server-data-tools-for-analysis-services-tabular-projects-ssdt-tabular/).

### Fixed / Improved this month

**Database Tools**

* [Connect bug 3055711](https://connect.microsoft.com/SQLServer/feedback/details/3055711/columns-cannot-be-selected-from-cross-apply-openjson-with-explicit-schema) – Columns cannot be selected from CROSS APPLY OPENJSON with explicit schema
* Fixed – issue with Auto-generated History table indexes, where DacFx dropped index on redeployment
* Fixed – issue with DacFx batch parser not parsing escaped bracket ‘]’ characters, which caused publish to fail
* Improved – SqlPackage now includes descriptions for each action in the help output
* Fixed – The “Remember Password” option in the connection dialog was not being preserved when editing Advanced options and when editing a connection string saved in Publish, Schema Compare and other files
* Fixed – For connections show in the History tab with IntegratedAuthentication=true, the Authentication field in connection properties was left blank. This now shows “Windows Authentication” as expected
* Fixed – Changes to the SQL Server Tools Intellisense settings under Tools -> Options -> Text Editor were not being preserved
* Improved – the Pin/Unpin button in the connection dialog History tab is now more compact, reducing the likelihood of a scrollbar appearing
* Fixed – several accessibility issues in the connection dialog were fixed.

**Analysis Services & Reporting Services**

* Fixed an issue in SSDT AS tabular designer where clicking the scrollbar thumb in data grid crashed in certain situations
* Fixed an issue where option to impersonate connection as current user in SSDT AS tabular wasn’t available
* Fixed an issue in SSDT AS tabular designer where expanding the formula bar too far could make the project unable to re-open
* Fixed a crash in SSDT AS tabular designer that would occur on key down if table tab was selected
* Fixed an issue in SSDT AS projects where Analyze in Excel would not connect to down-level AS server versions

**Integration Service**

* Fixed Connect bug [1608896](https://connect.microsoft.com/SQLServer/feedback/details/1608896/move-multiple-integration-service-package-tasks): Move Multiple Integration Service Package Tasks





## SSDT 16.4 (for SQL Server 2016)
Released: September 20, 2016

Build number: 14.0.60918

**What's New?**

Schema Compare is now supported in SqlPackage.exe and the Data-Tier Application Framework (DacFx) API. For details, see [Schema Compare in SqlPackage and the Data-Tier Application Framework](https://blogs.msdn.microsoft.com/ssdt/2016/09/20/schema-compare-in-sqlpackage-and-the-data-tier-application-framework-dacfx/).

**Analysis Services – Integrated Workspace Mode for SSDT Tabular (SSAS)**

SSDT Tabular now includes an internal SSAS instance, which SSDT Tabular starts automatically in the background if integrated workspace mode is enabled so that you can add and view tables, columns, and data in the model designer without having to provide an external workspace server instance. Integrated workspace mode does not change how SSDT Tabular works with a workspace server and database. What changes is where SSDT Tabular hosts the workspace database. To enable integrated workspace mode, select the Integrated Workspace option in the Tabular Model Designer dialog box displayed when creating a new tabular project. For existing tabular projects that currently use an explicit workspace server, you can switch to integrated workspace mode by setting the Integrated Workspace Mode parameter to True in the Properties window, which is displayed when you select the Model.bim file in Solution Explorer. For details, see the [Analysis Services blog post](https://blogs.msdn.microsoft.com/analysisservices/2016/09/20/introducing-integrated-workspace-mode-for-sql-server-data-tools-for-analysis-services-tabular-projects-ssdt-tabular/).

**Updates and fixes**
**Database tools:**

- [Connect Issue 3087775](https://connect.microsoft.com/SQLServer/feedback/details/3087775): Temporal tables broken in VS Data Tools July update 14.0.60629.0, "Value cannot be null. Parameter name: reportedElement"
- [Connect Issue 1026648](https://connect.microsoft.com/SQLServer/feedback/details/1026648): IsPersistedNullable shows as different in SSDT Comparison
- [Connect Issue 2054735](https://connect.microsoft.com/SQLServer/feedback/details/2054735): Identity is reset when importing a BACPAC
- [Connect Issue 2900167](https://connect.microsoft.com/SQLServer/feedback/details/2900167): Running SSDT unit tests leaves temp files behind
- [Connect Issue 1807712](https://connect.microsoft.com/SQLServer/feedback/details/1807712): Backwards compatibility breakage – AppLocal and Nugetization

**Analysis Services & Reporting Services**

* Fixed an issue in SSDT where error tip pop-ups were in the way when editing DAX for DirectQuery calculated columns.
* Fixed an issue in SSDT AS tabular grid where the KPI icon wasn't showing in measure grid when Windows scaling factor set at high-DPI 200%+.
* Fixed an issue in SSDT AS where pasting large table data could make the tabular project unresponsive.
* Fixed an issue in SSDT AS tabular model editor to mark the model as needing to save changes when renaming connection friendly name.
* Fixed an issue in the SSDT AS tabular projects where width of columns in the manage relationships dialog could not be resized.
* Fixed an issue in SSDT AS tabular 1200-level models where pasting data from Excel with locale settings like German didn't treat the comma as the decimal separator correctly.
* Fixed an issue in SSDT AS projects with some KPI icon sets which could yield an error "Couldn't retrieve the data for this visual".
* Fixed an issue with SSDT AS project properties dialog to anchored correctly when resized at High-DPI scaling.
* Fixed an issue in SSDT AS projects that may have caused an error upgrading certain models with Pasted tables.
* Fixed an issue in SSDT AS where pasting full sheet rows from Excel was very slow and created many unwanted columns.
* Fixed an issue in SSDT AS where large static DataTable expressions parsing and highlight was really slow or appeared to hang.
* Fixed an issue in SSDT AS to add measures and KPI values to the current perspective selected in the editor.
* Fixed an issue in SSDT where data import into AS project from SQL Azure didn't support schema types other than "dbo".



## SSDT 16.3 (for SQL Server 2016)
Released: August 15, 2016

Build number: 14.0.60812.0  

**What's New?**

- **Release Versioning & Numbering:** Releases are now tagged numerically rather than by month. This aligns with the new SSMS policy and simplifies cases where we have multiple releases or hotfixes in a month. This release is 16.3 which means the third update after the RTM release. Any hotfix will be 16.3.1 and so on, with our next update (planned for next month) being 16.4.
- **Analysis Services – Tabular Model Explorer:** Tabular Model Explorer lets you conveniently navigate through the various metadata objects in a model, such as data sources, tables, measures, and relationships. It is implemented as a separate tools window that you can display by opening the View menu in Visual Studio, pointing to Other Windows, and then clicking Tabular Model Explorer. The Tabular Model Explorer appears by default in the Solution Explorer area on a separate tab. Tabular Model Explorer organizes the metadata objects in a tree structure that closely resembles the schema of a tabular 1200 model and many more new features.
- **Database Tools – Always Encrypted**:  This release provides new [Always Encrypted Key management](https://msdn.microsoft.com/library/mt708953.aspx) dialogs to easily add Column Master Keys or Column Encryption Keys to your database project, or a live database in SQL Server Object Explorer. This release supports certificates in Windows Certificate Store. In upcoming releases, Azure Key Vault and CNG Providers will be supported.
    - While creating Column Master Key or Column Encryption Key, you may experience that the changes are not reflected on SQL Server Object Explorer immediately after clicking Update Database. To workaround, refresh the database node in SQL Server Object Explorer.
    - If you try to encrypt a column in a table with data from SQL Server Object Explorer, you may experience a failure. This feature is currently supported only in SSDT database projects and SSMS. Support for SQL Server Object Explorer will be enabled in a later release.


**Updates and fixes**
* **Database tools:**
    - **SSDT:**
        - Connect bug 1898001 [Fixed a column description issue with 128 character limitation](https://connect.microsoft.com/SQLServer/feedback/details/1898001/column-description-limited-to-128-characters).
        - Fixed an issue where publishing a database from VS did not apply DatabaseServiceObjective property in the publish profile xml.
        - Connect bug 2900167 [Fixed a unit test issue for incorrectly leaving temp files](http://connect.microsoft.com/SQLServer/feedback/details/2900167/running-ssdt-unit-tests-leaves-temp-files-behind).
        - Fixed an issue where Retention Period combo box on Database Settings is truncated.
        - Fixed an issue for the missing verification of empty old password on SQL CLR project properties while changing the password.
    - **DACFx:**
        - Fixed an issue where DACFx compatibility level is not updated for SqlAzureV12 error.
        - Fixed an issue where IsAutoGeneratedHistoryTable property is incorrectly excluded from model comparison.

- **Analysis Services & Reporting Services**
    - **SSDT:**
        - Fixed an issue that Tabular model cannot be saved when the server connection is lost.
        - Fixed an issue that SSDT becomes unresponsive due to a possible infinite loop issue in AS.
        - Fixed a DAX expression issue that caused inconsistent behaviors based on how you commit the expression .
        - Fixed a VS crash issue when creating KPIs.
        - Fixed an issue that generated invalid reports for SQL Server 2008 R2, 2012 and 2014.
        - Fixed a Hierarchy order issue that caused an infinite loop error for .dwpro project.
        - Fixed a RS RDL issue where downgrading RDL required a full rebuild which caused user’s confusion.
        - Fixed a KPI issue where Hide From Client Tools had no effect.
        

 
  
## SSDT July (for SQL Server 2016)  
Released: June 30, 2016  
  
Build number: 14.0.60629.0  
  
**What's New?**  
* **Always Encrypted support:** For Databases that contain Always Encrypted columns, this release adds full support for Always Encrypted through our core APIs and command line tool (SqlPackage.exe). You can build and publish database projects with full support for all Always Encrypted features.  
* **Temporal Tables enhanced support:** Simplified the experience by unlinking temporal tables before alterations and re-linking once these have completed. This means that Temporal Tables have parity with other table types (standard, in-memory) in terms of the operations that are supported. 
* **SqlPackage.exe and installation changes:** Changes to isolate SSDT from SQL Server engine and SSMS updates. For details, see [Changes to SSDT and SqlPackage.exe installation and updates](https://blogs.msdn.microsoft.com/ssdt/2016/06/30/changes-to-ssdt-and-sqlpackage-exe-installation-and-updates/).

 

**Updates and fixes**
* **Database tools:**
    * From now on SSDT will never disable Transparent Data Encryption (TDE) on a database. Previously since the default encryption option in a project’s database settings was disabled, it would turn off encryption. With this fix encryption can be enabled but never disabled during publish. 
    * Increased the retry count and resiliency for Azure SQL DB connections during initial connection.
    * If the default filegroup is not PRIMARY, Import/Publish to to Azure V12 would fail. Now this setting is ignored when publishing.
    * Fixed an issue where when exporting a database with an object with Quoted Identifier on, export validation could fail in some instances.
    * Fixed an issue where the TEXTIMAGE_ON option was incorrectly added for Hekaton table creations where it is not allowed.
    * Fixed an issue where Export took a long time exporting with large amount of data due to a write to the model.xml file after data phase completed caused contents of the .bacpac file to be rewritten.
    * Fixed an issue where Users were not appearing in the Security folder for Azure SQL DW and APS connections.


 * **Analysis Services & Reporting Services:**
    * Fixed a SxS issue with MSOLAP OLEDB provider where only the 32-bit provider was getting installed, impacting 64-bit Excel 2016 connecting to SQL Server 2014 (did not repro with ClickOnce installs from Office365, only MSI Excel install).
    * Fixed an issue for a corner case to be more robust when upgrading AS model with pasted tables from 1103 to 1200 compat-level that could give error "Relationship uses an invalid column ID".
    * Fixed a SxS issue when SSDT-BI 2013 on same machine, could no longer import data in AS model after uninstalling SSDT 2015 (cartridges shared registry setting).
    * Improved robustness to address issues\crashes when the connection to the AS engine is lost (i.e. SSDT left open overnight and AS server recycled, or other cases where the connection is temporarily lost). 
    * Fixed issues with dialogs opening on different screens than VS in multi-monitor scenarios. 
    * Fixed/enabled support for pasting from HTML tables (grid data) in AS model pasted tables. 
    * Fixed issue where upgrade failed to upgrade an empty pasted table to 1200 (used only as container table for measures). 
    * Fixed an issue with upgrading AS tabular model with pasted tables to 1200 to work around an AS engine issue with CalcTables (which are used for Pasted Tables in 1200), to perform a process full on the new calc tables after the upgrade. 
    * Fixed an issue where canceling creation of new AS 1200 model calculated table with incomplete DAX expression could crash. 
    * Fixed an issue importing 1200 model from AS server into SSDT AS project when DB name and a table name were the same. 
    * Fixed an issue with editing KPI measure in 1103 tabular model. 
    * Fixed an Object reference not set exception hit while pasting a KPI measure in the grid for an AS 1200 model. 
    * Fixed an issue where a column in a calculated table could not be deleted from the diagram view in 1200 models. 
    * Fixed an Object Reference not set exception when viewing the model.bim project file properties while in code view. 
    * Fixed an issue with pasting data into AS model grid to create pasted table yielded incorrect values on international locales using comma as decimal separator. 
    * Fixed an issue opening 2008 RS project in SSDT and choosing to not upgrade it. 
    * Fixed issue in 1200 compat-level models calculated table UI when using default formatting for column type to allow changing the formatting type from the UI. 
    

## SSDT June (for SQL Server 2016)  
Released: June 1, 2016  
  
Build number: 14.0.60525.0 

SSDT General Availability (GA) is now released. The SSDT GA update for June 2016 adds support for the latest updates of SQL Server 2016 RTM, and various bug fixes. For details, see [SQL Server Data Tools GA update for June 2016](https://blogs.msdn.microsoft.com/ssdt/2016/06/01/sql-server-data-tools-ga-update-for-june-2016/).

      

## SSDT April (for SQL Server 2016 RC3)  
Released: April 15, 2016  
  
Build number: 14.0.60413.0  
  
**SQL Server Database**  
* **Always Encrypted Support:** For Databases that contain Always Encrypted columns, SSDT and DacFx allows viewing and editing these databases and publishing from a database project to them. Note that support for altering columns with column encryption present will be coming in a future release.  
* **Connection dialog and SQL Server Object Explorer:** Multiple fixes and improvements.  
    * The Details page listing advanced connection properties was overhauled to show the full connection string in a multi-line box, and to improve support on High DPI machines.  
    * We have brought back the traditional error dialog with detailed connection errors. This helps when diagnosing login issues with clearer error messages and a stack trace so that DBAs or CSS can get the information they need to help diagnose your problems.  
    * For users with minimal permissions we fixed a number of issues around listing databases in the Connection Dialog and SQL Server Object Explorer, viewing the Security folder, and more.  
    * Azure SQL DB performance when expanding the databases node to list all DBs has been improved.  
* **SSDT installer:**  
    * Fixed issue where .Net was being downloaded on uninstall.  
    * The installer size is now set correctly on High DPI machines.  
    * Removed the version check blocking SSDT installation if a newer SQL Server version is present.  
    * Schema Compare: Fixed a performance issue where checking/unchecking multiple items took a long time in Visual Studio.  
    * Support for using LocalDB 2014 on x86 machines, since there is no x86 version of SQL Server 2016.  
* **Build and Deployment:**  
    * Fixed issue where computed columns were not supported on Temporal Tables.  
    * The “Execute deployment script in single-user mode” option is ignored when deploying to Azure V12 as this is not supported in cloud scenarios.  
  
  
## SSDT Hotfix (for SQL Server 2016 RC2)  
Released: April 5, 2016  
  
Build number: 14.0.60329.0  
  
This build contains a hotfix for the version of SSDT that provides features for SQL Server Integration Services. Build 14.0.60316.0 can also be used with Analysis Services and Reporting Services in SQL Server 2016.   
  
To get this hotfix, use the [download links on this blog post](https://blogs.msdn.microsoft.com/ssdt/2016/04/05/ssdt-preview-update-rc2/).  
  
Report developers, if you build new reports using this build of SSDT, [read the known issue and workaround](https://blogs.msdn.microsoft.com/ssdt/2016/04/05/ssdt-preview-update-rc2/) for a  for a temporary issue in SSRS reports found only in this hotfix.  
  
## SSDT Hotfix (for SQL Server 2016 RC0)  
Released: March 18, 2016  
  
Build number: 14.0.60316.0  
  
This build contains a hotfix for the version of SSDT that provides features for SQL Server 2016 RC0. There is no RC1 version of SSDT at this time. Build 14.0.60316.0 can be used with either RC0 or RC1 of SQL Server 2016.  
      
## SSDT February 2016 Preview (for SQL Server 2016 RC0)  
Released: March 7, 2016  
  
Build number: 14.0.60305.0  
  
-   **SQL Server project templates**  
  
    No announcements for this SSDT preview release. See [What's New in Database Engine](https://msdn.microsoft.com/library/bb510411.aspx) to learn about other features in this release.  
  
-   **SSIS package project templates**  
  
    SSIS Designer creates and maintains packages for SQL Server 2016, 2014, or 2012. New templates renamed as parts. SSIS Hadoop connector supports for ORC format. See [What's New in Integration Services](https://msdn.microsoft.com/library/bb522534.aspx) for details.  
  
-   **SSAS project templates (Tabular model projects)**  
  
    This month’s update to Analysis Services delivers support for display folders for Tabular models and any models created with new SQL Server 2016 compatibility level is now supported in SSIS packages. For more information. see [What's New in Analysis Services (blog post)](http://blogs.msdn.com/b/analysisservices/archive/2016/01/28/what-s-new-for-sql-server-2016-analysis-services-in-ctp3-3.aspx) for details.  
  
-   **SSRS report project templates**  
  
    No announcements for this SSDT preview release. See [What's New in Reporting Services](https://msdn.microsoft.com/library/ms170438.aspx) to learn  about other features in this release.  
  
## SSDT January 2016 Preview  
Released: Feb 4, 2016  
  
Build number: 14.0.60203.0  
  
-   **SQL Server project templates**  
  
    No announcements for this SSDT preview release. See [What's New in Database Engine](https://msdn.microsoft.com/library/bb510411.aspx) to learn about other features in this CTP.  
  
-   **SSIS package project templates**  
  
    Adds support for ODBC source and destination components, a CDC control task,  
      a CDC source and splitter component, a Microsoft Connector for SAP BW, and an Integration Services Feature Pack for Azure. See [What's New in Integration Services](https://msdn.microsoft.com/library/bb522534.aspx) for details.  
  
-   **SSAS project templates**  
  
    Includes enhancements for Tabular models at 1200 compatibility level, calculated columns and row-level security for models in DirectQuery mode, translations of model metadata,  TMSL script execution in the SSIS Analysis Services Execute DDL Task, and numerous bug fixes.  
    See [What's New in Analysis Services (msdn)](https://msdn.microsoft.com/library/bb522628.aspx) or [What's New in Analysis Services (blog post)](http://blogs.msdn.com/b/analysisservices/archive/2016/01/28/what-s-new-for-sql-server-2016-analysis-services-in-ctp3-3.aspx) for details.  
  
-   **SSRS report project templates**  
  
    No announcements for this SSDT preview release. See [What's New in Reporting Services](https://msdn.microsoft.com/library/ms170438.aspx) to learn  about other features in this CTP.  
  
## SSDT December 2015 Preview  
  
-   **SQL Server project templates** include bug fixes for the Connection dialog box, recent history lists, proper use of authentication context set in the connection property when loading a database list.  
  
    -   Changed test connection timeout value to 15 seconds.  
  
    -   Create an Azure SQL Database server firewall rule if the client IP is not registered when loading a database list.  
  
    -   SQL Server 2016 CTP3.2 feature programmability support.  
  
-   **SSAS project templates** add support for creating calculated tables based on DAX expressions and other objects already defined in the model.  
  
-   **SSIS package project template** additions include SSIS Hadoop connector support for Avro file format and Kerberos authentication.   
    Please note that SSIS designer support for SSIS 2012 and 2014 is not yet included in this update.  
  
## SSDT November 2015 Preview  
  
-   **SQL Server project templates**. Preview of improved connection experience for SQL Server and Azure SQL Database.  
  
-   **SSIS package project templates**. SSIS catalog performance improvement: The performance for most SSIS catalog views for non-ssis-admin user is improved.  
  
-   **SSAS project templates** include enhancements for Tabular model projects in Analysis Services. You can use the **View Code** command to view the model definition in JSON. If you aren't using a full-featured edition Visual Studio 2015, you will need one to get the JSON editor. You can download the [Visual Studio Community edition](https://www.visualstudio.com/downloads/download-visual-studio-vs.aspx) for free.  
  
## SSDT October 2015 Preview  
  
-   New project  templates for BI (Analysis Services models, Reporting Services reports, and Integration Services packages). All  SQL Server project templates are now in one SSDT.  
  
-   New SSIS features including Hadoop connector, control flow template, relaxed max buffer size of data flow task.  
  
-   SQL Server 2016 CTP 3.0 feature support for relational database projects.  
  
-   Various bug fixes in SSIS and support for Windows 7 OS.  
  
## SSDT September 2015 Preview  
  
-   Multi-language support is new in this preview.  
  
## SSDT August 2015 Preview  
  
-   New standalone Setup.exe program for installing SSDT.  You no longer need to use a modified version of SQL Server Setup. This version of SSDT includes a project template for building relational databases deployed to SQL Server or Azure SQL Database.  
  
## See Also  
[Download SQL Server Data Tools &#40;SSDT&#41;](../ssdt/download-sql-server-data-tools-ssdt.md)  
[Previous releases of SQL Server Data Tools &#40;SSDT and SSDT-BI&#41;](../ssdt/previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi.md)  
[What's New in Database Engine](https://msdn.microsoft.com/library/bb510411.aspx)  
[What's New in Analysis Services](https://msdn.microsoft.com/library/bb522628.aspx)  
[What's New in Integration Services](https://msdn.microsoft.com/library/bb522534.aspx)  
  
