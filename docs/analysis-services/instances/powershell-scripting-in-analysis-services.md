---
title: "PowerShell scripting in Analysis Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 60bb9610-7229-42eb-a95f-a377268a8720
caps.latest.revision: 44
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# PowerShell scripting in Analysis Services
  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] includes PowerShell components to navigate, administer, and query [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] server, tabular, and multidimensional objects:  
  
-   **SQLAS** provider, used for navigation of the object hierarchy, is available when you have any local instance of Analysis Services (server mode is irrelevant).  
  
-   **SQLASCMDLETS** module provides task-specific cmdlets, such as backup, restore, process, as well as the general purpose [Invoke-ASCmd cmdlet](../../analysis-services/powershell/invoke-ascmd-cmdlet.md) that accepts any ASSL/XMLA or Tabular Model Scripting Language (TMSL) query or script input file.  
  
 Both components implement a subset of the Analysis Services Management Object ([Microsoft.AnalysisServices Namespace](http://msdn.microsoft.com/library/ms146720\(SQL.130\).aspx)) administrative interface, providing cmdlets for managing and creating [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects.  Both  are extensions of the **SQLPS** root module for SQL Server. To use [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] PowerShell components, you start by importing **SQLPS**. Syntax and examples for all cmdlets can be found in the [Analysis Services PowerShell Reference](../../analysis-services/powershell/analysis-services-powershell-reference.md).  For an example of how to use AMO types in PowerShell to create a Tabular database, see [AMO PowerShell Example](../../analysis-services/powershell/amo-powershell-example.md).  
  
##  <a name="bkmk_prereq"></a> Step 1: Install PowerShell components  
 The recommended approach for getting PowerShell components is by installing  SQL Server Management Studio (SSMS). This approach provides the PowerShell modules for SQL Server and the Analysis Services Management (AMO) data provider. Having SSMS also gives you a tool for easily generating XMLA and TMSL inputs for use in your PowerShell script.  
  
 Consider installing a local instance of Analysis Services. A local instance enables navigation of the object hierarchy via the **SQLAS** provider, even if you never use it to host a database.  
  
1.  Go to  [Download SQL Server Management Studio](https://msdn.microsoft.com/en-us/library/mt238290.aspx) to get the latest version. The latest release includes updated AMO that supports tabular metadata object definitions, for Tabular models created at compatibility level 1200 and higher.  
  
2.  After installing Management Studio, open a PowerShell window. It doesn't have to be an admin window.  
  
3.  Enter `Get-Module -ListAvailable` to confirm that **SQLPS** and **SQLASCMDLETS** modules appear in the list.  
  
     You won't see **SQLAS** unless you also install a local instance of Analysis Services (either Tabular or Multidimensional mode).  
  
4.  Enter `Get-Command -Module sqlascmdlets` to produce a list of the cmdlets used in Analysis Services administration.  
  
     **SQLASCMDLETS** are available even when the **SQLAS** provider is missing.  
  
    -   [Add-RoleMember cmdlet](../../analysis-services/powershell/add-rolemember-cmdlet.md)  
  
    -   [Backup-ASDatabase cmdlet](../../analysis-services/powershell/backup-asdatabase-cmdlet.md)  
  
    -   [Invoke-ASCmd cmdlet](../../analysis-services/powershell/invoke-ascmd-cmdlet.md) **-inputfile**  
  
    -   [Invoke-ProcessASDatabase](../../analysis-services/powershell/invoke-processasdatabase.md)  
  
    -   [Invoke-ProcessCube cmdlet](../../analysis-services/powershell/invoke-processcube-cmdlet.md)  
  
    -   [Invoke-ProcessDimension cmdlet](../../analysis-services/powershell/invoke-processdimension-cmdlet.md)  
  
    -   [Invoke-ProcessPartition cmdlet](../../analysis-services/powershell/invoke-processpartition-cmdlet.md)  
  
    -   [Invoke-ProcessTable cmdlet](../../analysis-services/powershell/invoke-processtable-cmdlet.md)  
  
    -   [Merge-Partition cmdlet](../../analysis-services/powershell/merge-partition-cmdlet.md)  
  
    -   [New-RestoreFolder cmdlet](../../analysis-services/powershell/new-restorefolder-cmdlet.md)  
  
    -   [New-RestoreLocation cmdlet](../../analysis-services/powershell/new-restorelocation-cmdlet.md)  
  
    -   [Remove-RoleMember cmdlet](../../analysis-services/powershell/remove-rolemember-cmdlet.md)  
  
    -   [Restore-ASDatabase cmdlet](../../analysis-services/powershell/restore-asdatabase-cmdlet.md)  
  
> [!NOTE]  
>  Windows PowerShell is installed by default on newer versions of the Windows operating systems. The recommendation is 4.0 or later.  
  
## Step 2: Load components to start an interactive session  
 After the components are installed, loading them starts an interactive session.  
  
1.  Enter `Import-Module sqlps -DisableNameChecking`,  
  
     Loads the SQL Server PowerShell components, including those for Analysis Services, and suppresses the warning about invalid verb names.  
  
2.  Enter `sqlserver:`  
  
     You should see the prompt  change to **PS SQLSERVER:\\>**.  
  
3.  If Analysis Services is installed locally, you can navigate the object hierarchy. Enter `cd sqlas` to open the **SQLAS** provider.  
  
4.  Type `dir` to list Analysis Services instances. The provider does not distinguish between tabular and multidimensional instances.  
  
## Permissions  
 An interactive PowerShell session runs under the security identity of the person who starts it. Most tasks will require that the person initiating the session is also an Analysis Services server administrator.  
  
 Scheduled PowerShell tasks should be considered unattended operations. The account running the scheduler, such as SQL Server Agent service, most likely needs to be an Analysis Services administrator (depending on the task).  
  
 Local data folders, such as the default backup and data directories, are already configured with file system permissions that allow a local instance to read and write to those locations.  
  
 Remote administration, especially when conducted from client computers that don't have Analysis Services installed, requires that the remote Analysis Services server instance performing the action have file system permissions to read files during restore, or write files during backup.  
  
## Script input files: ASSL/XMLA or TMSL  
 If you are using **invoke-ascmd** to execute script, the server mode, database type, and compatibility level will factor into how you construct the script.  
  
-   Multidimensional databases and Tabular databases at 1050-1103 compatibility levels respond to script written in XMLA (using the ASSL extensions specific to Analysis Services object definitions).  
  
-   Tabular databases at compatibility level 1200 and higher respond to TMSL script.  
  
 If you are working with mixed versions of Tabular models on the same SQL Server 2016 instance, remember to use the right script.  
  
> [!NOTE]  
>  Scripts can be generated in SQL Server management Studio and then modified as needed. Tabular databases at 1200 compatibility level and higher are scripted in TMSL. All others are scripted in XMLA/ASSL. A SQL Server 2016 instance in Tabular mode supports both scripting languages.  
  
## Local and remote administration  
 Local administration of Analysis Services via PowerShell scripts and commands is easier for two reasons:  
  
-   Enables use of the **SQLAS** provider for navigating the object hierarchy.  
  
-   File permissions that enable Analysis Services to read from default data folders, such as for backup and restore tasks, are already in place, assuming you're using those folders as the database location, and the local server instance is used for the operation.  
  
 Managing a remote instance requires extra configuration. The following steps assume that you completed the installation steps for Management Studio, but that the service instance is on a remote computer. You must have administrator rights on the Analysis Services server.  
  
 Because Analysis Services is remote, there is no **SQLAS** provider for local navigation of the object hierarchy. If you are restoring files from a local folder rather than an Analysis Services instance, you will have to create shares and grant the server read-access to the files.  
  
1.  Open an administrator PowerShell window.  
  
2.  Enter `Set-ExecutionPolicy RemoteUnsigned`  
  
3.  In File Explorer, make sure that any folders storing data files are shared, and that the Analysis Services instance has read permissions to the contents.  
  
##  <a name="bkmk_vers"></a> Supported Versions and Modes of Analysis Services  
 The following table shows the availability of Analysis Services PowerShell in different contexts.  
  
|Context|PowerShell Feature Availability|  
|-------------|-------------------------------------|  
|Multidimensional instances and databases|Supported for local and remote administration.<br /><br /> Merge-partition requires a local connection.|  
|Tabular instances and databases|Supported for local and remote administration, at all compatibility levels.<br /><br /> SQLAS cmdlets for Tabular models at the 1200 and higher compatibility level use Tabular Model Scripting Language (TMSL) in JSON instead of XMLA.|  
|[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint instances and databases|Limited support. You can use HTTP connections and the SQLAS provider to view instance and database information.<br /><br /> However, using the cmdlets is not supported. You can't use Analysis Services PowerShell to backup and restore an in-memory [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] database, nor should you add or remove roles, process data, or run arbitrary XMLA script.<br /><br /> For configuration purposes, [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint has built-in PowerShell support that is provided separately. For more information, see [PowerShell Reference for Power Pivot for SharePoint](../../analysis-services/powershell/powershell-reference-for-power-pivot-for-sharepoint.md).|  
|Native connections to local cubes<br /><br /> “Data Source=c:\backup\test.cub”|Not supported.|  
|HTTP connections to BI semantic model (.bism) connection files in SharePoint<br /><br /> `Data Source=http://server/shared_docs/name.bism`|Not supported.|  
|Embedded connections to [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] databases<br /><br /> “Data Source=$Embedded$”|Not supported.|  
|Local server context in Analysis Services stored procedures<br /><br /> “Data Source=*”|Not supported.|  
  
## Examples of server administration tasks with PowerShell  
 **List the server properties**  
  
 Server properties are exposed in several ways.  If you are familiar with properties exposed in msmdsrv. ini or the property pages of Management Studio, you'll see from the examples below that the properties are actually returned from different objects.  
  
 This script loads an AMO Server object. If you need a fully-qualified property name, you can run this script to return the list.  
  
```  
sqlps  
$as = New-Object Microsoft.AnalysisServices.Server  
$as.connect("server-name\instance-name")  
$as.serverproperties  
  
```  
  
 This script returns properties and methods at the instance level. From this list, you can read values like **ServerMode**, **Version**, **ProductName**, or **ProductLevel**.  
  
```  
sqlps  
$as = New-Object Microsoft.AnalysisServices.Server  
$as | get-member  
  
```  
  
 **Get the server mode**  
  
```  
sqlps  
$as = New-Object Microsoft.AnalysisServices.Server  
$as.ServerMode  
```  
  
 **Get the default compatibility level**  
  
```  
sqlps  
$as = New-Object Microsoft.AnalysisServices.Server  
$as.DefaultCompatibilityLevel  
```  
  
 **Get a list of databases**  
  
```  
sqlps  
$as = New-Object Microsoft.AnalysisServices.Server  
$as.databases  
```  
  
 **Change the port number**  
  
 This script creates an object for a named instance, declares the port, sets the port, updates the server instance, disconnects the object, and restarts the service. As a verification step, you can open a new connection and return the port.  
  
 You can also verify the port in the msmdsrv.ini file or in the server's General properties page in Management Studio.  
  
```  
sqlps  
$as = New-Object Microsoft.AnalysisServices.Server  
$as.connect("server-name\instance-name")  
$port = $as.serverproperties['Port']  
$port | select *  
$port.Value = [int]55555  
$as.Update()  
$as.Disconnect()  
restart-service 'MSOLAP$TABULAR'  
$as.connect("server-name\instance-name")  
$port | select *  
  
```  
  
## See Also  
 [Grant server admin rights to an  Analysis Services instance](../../analysis-services/instances/grant-server-admin-rights-to-an-analysis-services-instance.md)   
 [Compatibility Level for Tabular models in Analysis Services](../../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md)   
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)   
 [Install SQL Server PowerShell](../../database-engine/install-windows/install-sql-server-powershell.md)   
 [Manage Tabular Models Using PowerShell](http://go.microsoft.com/fwlink/?linkID=227685)   
 [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](../../analysis-services/instances/configure-http-access-to-analysis-services-on-iis-8-0.md)  
  
  