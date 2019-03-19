---
title: "What's new in SQL Server 2016 Analysis Services | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# What&#39;s New in Analysis Services
[!INCLUDE[ssas-appliesto-sql2016](../includes/ssas-appliesto-sql2016.md)]

SQL Server 2016 Analysis Services includes many new enhancements providing improved performance, easier solution authoring, automated database management, enhanced relationships with bi-directional cross filtering, parallel partition processing, and much more. At the heart of most enhancements for this release is the new 1200 compatibility level for tabular model databases.     

## Azure Analysis Services
Announced at the 2016 SQL PASS Conference, Analysis Services is now available in the cloud as an Azure service. **Azure Analysis Services** supports tabular models at the 1200 and higher compatibility levels. DirectQuery, partitions, row-level security, bi-directional relationships, and translations are all supported. To learn more and give it a try for free, see [Azure Analysis Services](http://azure.microsoft.com/services/analysis-services/). 

## What's new in SQL Server 2016 Service Pack 1 (SP1) Analysis Services

[Download SQL Server 2016 SP1](http://www.microsoft.com/download/details.aspx?id=54276) 

SQL Server 2016 Service SP1 Analysis Services provides improved performance and scalability through Non-Uniform Memory Access (NUMA) awareness and optimized memory allocation based on **Intel Threading Building Blocks** (Intel TBB). This new functionality helps lower Total Cost of Ownership (TCO) by supporting more users on fewer, more powerful enterprise servers. 

In particular, SQL Server 2016 SP1 Analysis Services features improvements in these key areas:

-	**NUMA awareness** - For better NUMA support, the in-memory (VertiPaq) engine inside Analysis Services now maintains a separate job queue on each NUMA node. This guarantees the segment scan jobs run on the same node where the memory is allocated for the segment data. Note, NUMA awareness is only enabled by default on systems with at least four NUMA nodes. On two-node systems, the costs of accessing remote allocated memory generally doesn't warrant the overhead of managing NUMA specifics.
-	**Memory allocation** - Analysis Services has been accelerated with Intel Threading Building Blocks, a scalable allocator that provides separate memory pools for every core. As the number of cores increases, the system can scale almost linearly.
-	**Heap fragmentation** - The Intel TBB-based scalable allocator also helps to mitigate performance problems due to heap fragmentation that have been shown to occur with the Windows Heap.

Performance and scalability testing showed significant gains in query throughput when running SQL Server 2016 SP1 Analysis Services on large multi-node enterprise servers.


## What's new in SQL Server 2016 Analysis Services

While most enhancements in this release are specific to tabular models, a number of enhancements have been made to multidimensional models; for example, distinct count ROLAP optimization for data sources like DB2 and Oracle, drill-through multi-selection support with Excel 2016, and Excel query optimizations.    

#### Get the latest tools
In order to take full advantage of all the enhancements in this release, be sure to install the latest versions of SSDT and SSMS.    
- [Download SQL Server Data Tools (SSDT)](http://msdn.microsoft.com/library/mt204009.aspx)    
- [Download SQL Server Management Studio (SSMS)](http://msdn.microsoft.com/library/mt238290.aspx)   

If you have a custom AMO-dependent application, you might need to install an updated version of AMO. For instructions, see [Install Analysis Services data providers &#40;AMO, ADOMD.NET, MSOLAP&#41;](../analysis-services/instances/install-windows/install-analysis-services-data-providers-amo-adomd-net-msolap.md).    

## Modeling    
### Improved modeling performance for tabular 1200 models    
For tabular 1200 models,  metadata operations in SSDT are much faster than tabular 1100 or 1103 models. By comparison, on the same hardware, creating a relationship on a model set to the SQL Server 2014 compatibility level (1103) with 23 tables takes 3 seconds, whereas the same relationship on a model created set to compatibility level 1200 takes just under a second.    
### Project templates added for tabular 1200 models in SSDT    
With this release, you no longer need two versions of SSDT for building relational and BI projects. [SQL Server Data Tools for Visual Studio 2015](http://msdn.microsoft.com/library/mt204009.aspx) adds project templates for Analysis Services solutions, including **Analysis Services Tabular Projects** used for building models at the 1200 compatibility level. Other Analysis Services project templates for multidimensional and data mining solutions are also included, but  at the same functional level (1100 or 1103) as in previous releases.    
### Display folders
Display folders are now available for tabular 1200 models. Defined in SQL Server Data Tools and rendered in client applications like Excel or Power BI Desktop, display folders help you organize large numbers of measures into individual folders, adding a visual hierarchy for easier navigation in field lists.
### Bi-directional cross filtering
New in this release is a built-in approach for enabling bi-directional cross filters in tabular models, eliminating the need for hand-crafted DAX workarounds for propagating filter context across table relationships. Filters are only auto-generated when the direction can be established with a high degree of certainty. If there is ambiguity in the form of multiple query paths across table relationships, a filter won't be created automatically. See [Bi-directional cross filters for tabular models in SQL Server 2016 Analysis Services](../analysis-services/tabular-models/bi-directional-cross-filters-tabular-models-analysis-services.md) for details.
 ### Translations    
 You can now store translated metadata in a tabular 1200 model. Metadata in the model includes fields for **Culture**, translated captions, and translated descriptions. To add translations, use the **Model** > **Translations** command in [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)]. See [Translations in tabular models &#40;Analysis Services&#41;](../analysis-services/tabular-models/translations-in-tabular-models-analysis-services.md) for details.    
 ### Pasted tables    
 You can now upgrade an 1100 or 1103 tabular model to 1200 when the model contains pasted tables. We recommend using [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)]. In SSDT, set **CompatibilityLevel** to 1200 and then deploy to a [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. See [Compatibility Level for Tabular models in Analysis Services](../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md) for details.    
 ### Calculated tables in SSDT    
A *calculated table* is a model-only construction based on a DAX expression or query in SSDT. When deployed in a database, a calculated table is indistinguishable from regular tables.    

 There are several uses for calculated tables, including the creation of new tables to expose an existing table in a specific role. The classic example is a Date table that operates in multiple contexts (order date, ship date, and so forth). By creating a calculated table for a given role, you can now activate a table relationship to facilitate queries or data interaction using the calculated table. Another use for calculated tables is to combine parts of existing tables into an entirely new table that exists only in the model.  See [Create a Calculated Table](../analysis-services/tabular-models/create-a-calculated-table-ssas-tabular.md) to learn more.    
 ### Formula fixup    
 With formula fixup on a tabular 1200 model,  SSDT will automatically update any measures that is referencing a column or table that was renamed.    
 ### Support for Visual Studio configuration manager    
 To support multiple environments, like Test and Pre-production environments, Visual Studio allows developers to create multiple project configurations using the configuration manager. Multidimensional models already leverage this but tabular models did not. With this release, you can now use configuration manager to deploy to different servers.    

## Instance management    
 ### Administer Tabular 1200 models in SSMS    
 In this release, an Analysis Services instance in Tabular server mode can run tabular models at any compatibility level (1100, 1103, 1200). The latest [SQL Server Management Studio](http://msdn.microsoft.com/library/mt238290.aspx) is updated to display properties and provide database model administration for tabular models at the 1200 compatibility level.    
 ### Parallel processing for multiple table partitions in tabular models    
 This release includes new parallel processing functionality for tables with two or more partitions, increasing processing performance. There are no configuration settings for this feature. For more information about configuring partitions and processing tables, see [Tabular Model Partitions](../analysis-services/tabular-models/tabular-model-partitions-ssas-tabular.md).    
 ### Add computer accounts as Administrators in SSMS    
 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] administrators can now use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to configure computer accounts to be members of the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] administrators group. In the **Select Users or Groups** dialog, set the **Locations** for the computers domain and then add the **Computers** object type. For more information, see [Grant server admin rights to an  Analysis Services instance](../analysis-services/instances/grant-server-admin-rights-to-an-analysis-services-instance.md).    
 ### DBCC for Analysis Services    
 Database Consistency Checker (DBCC) runs internally to detect potential data corruption issues on database load, but can also be run on demand if you suspect problems in your data or model. DBCC runs different checks depending on whether the model is tabular or multidimensional. See [Database Consistency Checker &#40;DBCC&#41; for Analysis Services tabular and multidimensional databases](../analysis-services/instances/database-consistency-checker-dbcc-for-analysis-services.md) for details.    
 ### Extended Events updates    
 This release adds a graphical user interface to [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to configure and manage [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] Extended Events. You can set up live data streams to monitor server activity in real time, keep session data loaded in memory for faster analysis, or save data streams to a file for offline analysis. For more information, see [Monitor Analysis Services with SQL Server Extended Events](../analysis-services/instances/monitor-analysis-services-with-sql-server-extended-events.md) and [Using extended events with Analysis Services (Guy in a Cube blog post and video)](http://blogs.msdn.com/b/analysisservices/archive/2015/09/22/using-extended-events-with-sql-server-analysis-services-2016-cpt-2-3.aspx).    



## Scripting
 ### PowerShell for Tabular models    
 This release includes PowerShell enhancements for tabular models at compatibility level 1200. You can use all of the applicable cmdlets, plus cmdlets specific to Tabular mode: [Invoke-ProcessASDatabase](../analysis-services/powershell/invoke-processasdatabase.md) and [Invoke-ProcessTable cmdlet](../analysis-services/powershell/invoke-processtable-cmdlet.md).    
 ### SSMS scripting database operations    
 In the [latest SQL Server Management Studio (SSMS)](http://msdn.microsoft.com/library/mt238290.aspx), script is now enabled for database commands, including Create, Alter, Delete, Backup, Restore, Attach, Detach. Output is Tabular Model Scripting Language (TMSL) in JSON. See [Tabular Model Scripting Language &#40;TMSL&#41; Reference](https://docs.microsoft.com/bi-reference/tmsl/tabular-model-scripting-language-tmsl-reference) for more information.    
 ### Analysis Services Execute DDL Task    
 [Analysis Services Execute DDL Task](../integration-services/control-flow/analysis-services-execute-ddl-task.md) now also accepts Tabular Model Scripting Language (TMSL) commands.     
 ### SSAS PowerShell cmdlet    
 SSAS PowerShell cmdlet **Invoke-ASCmd** now accepts Tabular Model Scripting Language (TMSL) commands. Other SSAS PowerShell cmdlets may be updated in a future release to use the new tabular metadata (exceptions will be called out in the release notes).    
See [Analysis Services PowerShell Reference](../analysis-services/powershell/analysis-services-powershell-reference.md) for details.    
 ### Tabular Model Scripting Language (TMSL) supported in SSMS    
  Using the [latest version of SSMS](http://msdn.microsoft.com/library/mt238290.aspx), you can now create scripts to automate most administrative tasks for tabular 1200 models. Currently, the following tasks can be scripted: Process at any level, plus CREATE, ALTER, DELETE at the database level.    
    
 Functionally, TMSL is equivalent to the XMLA ASSL extension that provides multidimensional object definitions, except that TMSL uses native descriptors like **model**, **table**, and **relationship** to describe tabular metadata. See [Tabular Model Scripting Language &#40;TMSL&#41; Reference](https://docs.microsoft.com/bi-reference/tmsl/tabular-model-scripting-language-tmsl-reference) for details about the schema.    
    
 A generated JSON-based script for a tabular model might look like the following:    
    
```    
{    
  "create": {    
    "database": { 
      "name": "AdventureWorksTabular1200",    
      "id": "AdventureWorksTabular1200",    
      "compatibilityLevel": 1200,    
      "readWriteMode": "readWrite",    
      "model": {}    
    }    
  }    
}    
```    

The payload is a JSON document that can be as minimal as the example shown above, or highly embellished with  the full set of object definitions. [Tabular Model Scripting Language &#40;TMSL&#41; Reference](https://docs.microsoft.com/bi-reference/tmsl/tabular-model-scripting-language-tmsl-reference) describes the syntax.

At the database level, CREATE, ALTER, and DELETE commands will output TMSL script in the familiar XMLA window.  Other commands, such as Process, can also be scripted in this release. Script support for many other actions may be added in a future release.    

**Scriptable commands** | **Description**
--------------- | ----------------
create|Adds a database, connection, or partition. The ASSL equivalent is CREATE.
createOrReplace|Updates an existing object definition (database, connection, or partition) by overwriting a previous version. The ASSL equivalent is ALTER with AllowOverwrite set to true and ObjectDefinition to ExpandFull.
delete|Removes an object definition. ASSL equivalent is DELETE.
refresh|Processes the object. ASSL equivalent is PROCESS.

## DAX
### Improved DAX formula editing
Updates to the formula bar help you write formulas with more ease by differentiating functions, fields and measures using syntax coloring, it provides intelligent function and field suggestions and tells you if parts of your DAX expression are wrong using error *squiggles*. It also allows you to use multiple lines (Alt + Enter) and indentation (Tab). The formula bar now also allows you to write comments as part of your measures, just type "//" and everything after these characters on the same line will be considered a comment.

### DAX variables    
This release now includes support for variables in DAX. Variables can now store the result of an expression as a named variable, which can then be passed as an argument to other measure expressions. Once resultant values have been calculated for a variable expression, those values do not change, even if the variable is referenced in another expression. For more information, see [VAR Function](http://msdn.microsoft.com/library/mt243785.aspx).    
### New DAX functions
With this release, DAX introduces over fifty new functions to support faster calculations and enhanced visualizations in Power BI. To learn more, see [New DAX Functions](http://msdn.microsoft.com/library/mt704075.aspx).
### Save incomplete measures
You can now save incomplete DAX measures directly in a tabular 1200 model project and pick it up again when you are ready to continue.
### Additional DAX enhancements
- Non empty calculation - Reduces the number of scans needed for non empty.
- Measure Fusion - Multiple measures from the same table will be combined into a single storage engine - query.
- Grouping sets - When a query asks for measures at multiple granularities (Total/Year/Month), a single - query is sent at the lowest level and the rest of the granularities are derived from the lowest level.     
- Redundant join elimination - A single query to the storage engine returns both the dimension columns and the measure values.
- Strict evaluation of IF/SWITCH - A branch whose condition is false will no longer result in storage engine queries. Previously, branches were eagerly evaluated but results discarded later on.     
    
## Developer    
 ### Microsoft.AnalysisServices.Tabular namespace for Tabular 1200 programmability in AMO
 Analysis Services Management Objects (AMO) is updated to include a new tabular namespace for managing a Tabular Mode instance of SQL Server 2016 Analysis Services, as well as provide the data definition language for creating or modifying tabular 1200 models programmatically. Visit [Microsoft.AnalysisServices.Tabular](http://msdn.microsoft.com/library/microsoft.analysisservices.tabular.aspx) to read up on the API.    
 ### Analysis Services Management Objects (AMO) updates
 [Analysis Services Management Objects &#40;AMO&#41;](http://msdn.microsoft.com/library/mt436122.aspx) has been re-factored to include a second assembly, Microsoft.AnalysisServices.Core.dll. The new assembly separates out common classes like Server, Database, and Role that have broad application in Analysis Services, irrespective of server mode.    
    
 Previously, these classes were part of the original Microsoft.AnalysisServices assembly. Moving them to a new assembly paves the way for future extensions to AMO, with clear division between generic and context-specific APIs.    
    
 Existing applications are unaffected by the new assemblies. However, should you choose to rebuild applications using the new AMO assembly for any reason, be sure to add a reference to Microsoft.AnalysisServices.Core.    
    
 Similarly, PowerShell scripts that load and call into AMO must now load Microsoft.AnalysisServices.Core.dll. Be sure to update any scripts.  

### JSON editor for BIM files
Code View in Visual Studio 2015 now renders the BIM file in JSON format for tabular 1200 models. The version of Visual Studio determines whether the BIM file is rendered in JSON via the built-in JSON Editor, or as simple text.

To use the JSON editor, with the ability to expand and collapse sections of the model, you will need the latest version of SQL Server Data Tools plus Visual Studio 2015 (any edition, including the free Community edition). For all other versions of SSDT or Visual Studio, the BIM file is rendered in JSON as simple text.
At a minimum, an empty model will contain  the following JSON:

    ```    
    {    
      "name": "SemanticModel",
      "id": "SemanticModel",
      "compatibilityLevel": 1200,
      "readWriteMode": "readWrite",
      "model": {}
    }    
    ```    
    
> [!WARNING]
> Avoid editing the JSON directly. Doing so can corrupt the model.    
>  ### New elements in MS-CSDLBI 2.0 schema    
>  The following elements have been added to the **TProperty** complex type defined in the [MS-CSDLBI] 2.0 schema:    
    
|Element|Definition|    
|-------------|----------------|    
|DefaultValue|A property that specifies the value used when evaluating the query. The DefaultValue property is optional, but it is automatically selected if the values from the member cannot be aggregated.|    
|Statistics|A set of statistics from the underlying data that is associated with the column. These statistics are defined by the TPropertyStatistics complex type and are provided only if they are not computationally expensive to generate, as described in section 2.1.13.5 of the Conceptual Schema Definition File Format with Business Intelligence Annotations document.|    
    
## DirectQuery    
### New DirectQuery implementation    
This release sees significant enhancements in DirectQuery for tabular 1200 models. Here's a summary:    
-   DirectQuery now generates simpler queries that provide better performance.    
-   Extra  control over defining sample datasets used for model design and testing.    
-   Row level security (RLS) is now supported for tabular 1200 models in DirectQuery mode. Previously, the presence of RLS prevented deploying a tabular model in DirectQuery mode.    
-   Calculated columns are now supported for tabular 1200 models in DirectQuery mode. Previously, the presence of calculated columns prevented deploying a tabular model in DirectQuery mode.    
-   Performance optimizations include redundant join elimination for VertiPaq and DirectQuery. 

### New data sources for DirectQuery mode    
 Data sources supported for tabular 1200 models in DirectQuery mode now include Oracle, Teradata and Microsoft Analytics Platform (formerly known as Parallel Data Warehouse).    
    
To learn more, see [DirectQuery Mode](../analysis-services/tabular-models/directquery-mode-ssas-tabular.md).    

## See Also
[Analysis Services team blog](http://blogs.msdn.microsoft.com/analysisservices/)    
[What's New in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md)    
     

