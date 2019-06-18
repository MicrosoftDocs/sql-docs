---
title: "What&#39;s New in Integration Services in SQL Server 2016 | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "09/28/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services, what's new"
  - "what's new [Integration Services]"
ms.assetid: da6999c7-e5e3-4a59-a284-1da635995af1
author: janinezhang
ms.author: janinez
manager: craigg
---
# What&#39;s New in Integration Services in SQL Server 2016

[!INCLUDE[ssis-appliesto](../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]



This topic describes the features that have been added or updated in SQL Server 2016 [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. It also includes features added or updated in the [Azure Feature Pack for Integration Services &#40;SSIS&#41;](../integration-services/azure-feature-pack-for-integration-services-ssis.md) during the SQL Server 2016 time frame.  

## New for SSIS in Azure Data Factory

With the public preview of Azure Data Factory version 2 in September 2017, you can now do the following things:
-   Deploy packages to the SSIS Catalog database (SSISDB) on Azure SQL Database.
-   Run packages deployed to Azure on the Azure-SSIS Integration Runtime, a component of Azure Data Factory version 2.

For more info, see [Lift and shift SQL Server Integration Services workloads to the cloud](lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md).

These new capabilities require SQL Server Data Tools (SSDT) version 17.2 or later, but do not require SQL Server 2017 or SQL Server 2016. When you deploy packages to Azure, the Package Deployment Wizard always upgrades the packages to the latest package format.

## 2016 improvements by category  
  
-   **Manageability**  
  
    -   Better deployment  
  
        -   [SSISDB Upgrade Wizard](#ssisdbupgrwiz)  
  
        -   [Support for Always On in the SSIS Catalog](#AlwaysOn)  
  
        -   [Incremental package deployment](#IncrementalDeployment)  
  
        -   [Support for Always Encrypted in the SSIS Catalog](#encrypted)  
  
    -   Better debugging  
  
        -   [New ssis_logreader database-level role in the SSIS catalog](#LogReader)  
  
        -   [New RuntimeLineage logging level in the SSIS catalog](#RuntimeLineage)  
  
        -   [New custom logging level in the SSIS catalog](#CustomLogging)  
  
        -   [Column names for errors in the data flow](#ErrorColumn)  
  
        -   [Expanded support for error column names](#getidstring)  
  
        -   [Support for server-wide default logging level](#ServerLogLevel)  
  
        -   [New IDTSComponentMetaData130 interface in the API](#CMD130)  
  
    -   Better package management  
  
        -   [Improved experience for project upgrade](#ProjectUpgrade)  
  
        -   [AutoAdjustBufferSize property automatically calculates buffer size for data flow](#BufferSize)  
  
        -   [Reusable control flow templates](#Templates)  
  
        -   [New templates renamed as parts](#Parts)  
  
-   **Connectivity**  
  
    -   Expanded connectivity on premises  
  
        -   [Support for OData v4 data sources](#ODatav4)  
  
        -   [Explicit support for Excel 2013 data sources](#Excel2013)  
  
        -   [Support for the Hadoop file system (HDFS)](#HDFS)  
  
        -   [Expanded support for Hadoop and HDFS](#more_hadoop)  
  
        -   [HDFS File Destination now supports ORC file format](#hdfsORC)  
  
        -   [ODBC components updated for SQL Server 2016](#odbc2016)  
  
        -   [Explicit support for Excel 2016 data sources](#Excel2016)  
  
        -   [Connector for SAP BW for SQL Server 2016 released](#SAPBW)
        
        -   [Connectors v4.0 for Oracle and Teradata released](#oracleteradata)
        
        -   [Connectors for Analytics Platform System (PDW) Appliance Update 5 released](#pdwau5)
  
    -   Expanded connectivity to the cloud  
  
        -   Azure Storage connectors and Hive and Pig tasks for HDInsight - [Azure Feature Pack for SSIS released for SQL Server 2016](#AFP2016)
        
        -   [Support for Microsoft Dynamics online resources released in Service Pack 1](#dynamics)
        
        -   [Support for Azure Data Lake Store released](#datalakestore)
        
        -   [Support for Azure SQL Data Warehouse released](#sqldwupload)
  
-   **Usability and productivity**  
  
    -   Better install experience  
  
        -   [Upgrade blocked when SSISDB belongs to an Availability Group](#Upgrade)  
  
    -   Better design experience  
  
        -   [SSIS Designer creates and maintains packages for SQL Server 2016, 2014, or 2012](#OneDesigner)  
  
        -   Multiple designer improvements and bug fixes.  
  
    -   Better management experience in SQL Server Management Studio
  
        -   [Improved performance for SSIS Catalog views](#CatViews)  
  
    -   Other enhancements  
  
        -   [Balanced Data Distributor transformation is now part of SSIS](#BDDinbox)  
  
        -   [Data Feed Publishing Components are now part of SSIS](#ComplexFeedinbox)  
  
        -   [Support for Azure Blob Storage in the SQL Server Import and Export Wizard](#AzureBlob)  
  
        -   [Change Data Capture Designer and Service for Oracle for Microsoft SQL Server 2016 released](#CDCOracle)  
  
        -   [CDC components updated for SQL Server 2016](#cdc2016)  
  
        -   [Analysis Services Execute DDL Task updated](#ASDDL)  
  
        -   [Analysis Services tasks support tabular models](#ssasrc0)  
  
        -   [Support for Built-in R Services](#builtinR)  
  
        -   [Rich XML validation output in the XML Task](#ValidateXML)  
  
## Manageability  

### Better deployment

####  <a name="ssisdbupgrwiz"></a> SSISDB Upgrade Wizard  
 Run the SSISDB Upgrade Wizard to upgrade the SSIS Catalog database, SSISDB, when the database is older than the current version of the SQL Server instance. This occurs when one of the following conditions is true.  
  
-   You restored the database from an older version of SQL Server.  
  
-   You did not remove the database from an Always On Availability Group before upgrading the SQL Server instance. This prevents the automatic upgrade of the database. For more info, see [Upgrading SSISDB in an availability group](../integration-services/catalog/ssis-catalog.md#Upgrade).  
  
 For more info, see [SSIS Catalog &#40;SSISDB&#41;](../integration-services/catalog/ssis-catalog.md). 

####  <a name="AlwaysOn"></a> Support for Always On in the SSIS Catalog  
 The Always On Availability Groups feature is a high-availability and disaster-recovery solution that provides an enterprise-level alternative to database mirroring. An availability group supports a failover environment for a discrete set of user databases known as availability databases that fail over together. For more information, see [Always On Availability Groups](../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md).  
  
 In SQL Server 2016, SSIS introduces new capabilities that let you easily deploy to a centralized SSIS Catalog (i.e. SSISDB user database). In order to provide high availability for the SSISDB database and its contents - projects, packages, execution logs, and so on - you can add the SSISDB database to an Always On Availability Group, just like any other user database. When a failover occurs, one of the secondary nodes automatically becomes the new primary node.  
  
 For a detailed overview and step-by-step instructions for enabling Always On for SSISDB, see [SSIS Catalog](../integration-services/catalog/ssis-catalog.md).  

####  <a name="IncrementalDeployment"></a> Incremental package deployment  
The Incremental Package Deployment feature lets you deploy one or more packages to an existing or new project without deploying the whole project. You can incrementally deploy packages by using the following tools.  
  
-   Deployment Wizard  
  
-   SQL Server Management Studio (which uses the Deployment Wizard)  
  
-   SQL Server Data Tools (Visual Studio) (which also uses the Deployment Wizard)  
  
-   Stored procedures  
  
-   The Management Object Model (MOM) API  
  
 For more info, see [Deploy Integration Services (SSIS) Projects and Packages](../integration-services/packages/deploy-integration-services-ssis-projects-and-packages.md).  

####  <a name="encrypted"></a> Support for Always Encrypted in the SSIS Catalog  
 SSIS already supports the Always Encrypted feature in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more info, see the following blog posts.  
  
-   [SSIS with Always Encrypted](https://blogs.msdn.com/b/ssis/archive/2015/12/18/ssis-with-always.aspx)  
  
-   [Lookup transformation with Always Encrypted](https://blogs.msdn.com/b/ssis/archive/2015/12/18/lookup-transformation-with-always-encrypted.aspx)  

### Better debugging

####  <a name="LogReader"></a> New ssis_logreader database-level role in the SSIS catalog  
 In previous versions of the SSIS catalog, only users in the **ssis_admin** role can access the views that contain logging output. There is now a new **ssis_logreader** database-level role that you can use to grant permissions to access the views that contain logging output to users who aren't administrators.  
  
 There is also a new **ssis_monitor** role. This role supports Always On and is for internal use only by the SSIS catalog.  

####  <a name="RuntimeLineage"></a> New RuntimeLineage logging level in the SSIS catalog  
 The new **RuntimeLineage** logging level in the SSIS catalog collects the data required to track lineage information in the data flow. You can parse this lineage information to map the lineage relationship between tasks. ISVs and developers  can build custom lineage mapping tools with this information. 

####  <a name="CustomLogging"></a> New custom logging level in the SSIS catalog  
 Previous versions of the SSIS catalog let you choose from four built-in logging levels when you run a package: **None, Basic, Performance, or Verbose**. SQL Server 2016 adds the **RuntimeLineage** logging level. In addition, you can now create and save multiple customized logging levels in the SSIS catalog, and pick the logging level to use every time you run a package. For each customized logging level,  select only the statistics and events you want to capture. Optionally include the event context to see variable values, connection strings, and task properties. For more info, see [Enable Logging for Package Execution on the SSIS Server](../integration-services/performance/integration-services-ssis-logging.md#server_logging). 

####  <a name="ErrorColumn"></a> Column names for errors in the data flow  
 When you redirect rows in the data flow that contain errors to an error output, the output contains a numeric identifier for the column in which the error occurred, but does not display the name of the column. There are now several ways to find or display the name of the column in which the error occurred.  
  
-   When you configure logging, select the **DiagnosticEx** event for logging. This event writes a data flow column map to the log. You can then look up the column name in this column map by using the column identifier captured by an error output. For more info, see [Error Handling in Data](../integration-services/data-flow/error-handling-in-data.md).  
  
-   In the Advanced Editor, you can see the column name for the upstream column when you view the properties of an input or output column of a data flow component.  
  
-   To see the names of the columns in which the error occurred, attach a Data Viewer to an error output.  The Data Viewer now displays both the description of the error and the name of the column in which the error occurred.  
  
-   In the Script Component or a custom data flow component, call the new <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData130.GetIdentificationStringByID%2A> method of the IDTSComponentMetadata100 interface.  
  
 For more info about this improvement, see the following blog post by SSIS developer Bo Fan: [Error Column Improvements for SSIS Data Flow](https://blogs.msdn.com/b/ssis/archive/2015/11/27/error-column-improvement-for-ssis-data-flow.aspx).  
  
> [!NOTE]  
>  (This support has been expanded in subsequent releases. For more info, see [Expanded support for error column names](#getidstring) and [New IDTSComponentMetaData130 interface in the API](#CMD130).)  

####  <a name="getidstring"></a> Expanded support for error column names  
 The **DiagnosticEx** event now logs column information for all input and output columns, not just lineage columns. As a result we now call the output a pipeline column map instead of a pipeline lineage map.  
  
 The method GetIdentificationStringByLineageID has been renamed to <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData130.GetIdentificationStringByID%2A>. For more info, see [Column names for errors in the data flow](#ErrorColumn).  
  
 For more info about this change and about the error column improvement, see the following updated blog post. [Error Column Improvements for SSIS Data Flow (Updated for CTP3.3)](https://blogs.msdn.com/b/ssis/archive/2015/11/27/error-column-improvement-for-ssis-data-flow.aspx)  
  
> [!NOTE]  
>  (In RC0, this method has been moved to the new <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData130> interface. For more info, see [New IDTSComponentMetaData130 interface in the API](#CMD130).)  

####  <a name="ServerLogLevel"></a> Support for server-wide default logging level  
 In SQL Server **Server Properties**, under the **Server logging level** property, you can now select a default server-wide logging level. You can pick from one of the built-in logging levels - basic, none, verbose, performance, or runtime lineage - or you can pick an existing customized logging level. The selected logging level applies to all packages deployed to the SSIS Catalog. It also applies by default to a SQL Agent job step that runs an SSIS package.  

####  <a name="CMD130"></a> New IDTSComponentMetaData130 interface in the API  
 The new <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData130> interface adds new functionality in SQL Server 2016 to the existing <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface, especially the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData130.GetIdentificationStringByID%2A> method. (The **GetIdentificationStringByID** method is moved to the new interface from the <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSComponentMetaData100> interface.)There are also new <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSInputColumn130> and <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.IDTSOutputColumn130> interfaces, both of which provide the **LineageIdentificationString** property. For more info, see [Column names for errors in the data flow](#ErrorColumn).  

### Better package management

####  <a name="ProjectUpgrade"></a> Improved experience for project upgrade  
 When you upgrade SSIS projects from previous versions to the current version, the project-level connection managers continue to work as expected and the package layout and annotations are retained.  

####  <a name="BufferSize"></a> AutoAdjustBufferSize property automatically calculates buffer size for data flow  
 When you set the value of the new **AutoAdjustBufferSize** property to **true**, the data flow engine automatically calculates the buffer size for the data flow. For more info, see [Data Flow Performance Features](../integration-services/data-flow/data-flow-performance-features.md).  

####  <a name="Templates"></a> Reusable control flow templates  
 Save a commonly used control flow task or container to a standalone template file and reuse it multiple times in one or more packages in a project by using control flow templates. This reusability makes SSIS packages easier to design and maintain. For more info, see [Reuse Control Flow across Packages by Using Control Flow Package Parts](../integration-services/reuse-control-flow-across-packages-by-using-control-flow-package-parts.md).  

####  <a name="Parts"></a> New templates renamed as parts  
 The new reusable control flow templates released in CTP 3.0 have been renamed as control flow parts or package parts. For more info about this feature, see [Reuse Control Flow across Packages by Using Control Flow Package Parts](../integration-services/reuse-control-flow-across-packages-by-using-control-flow-package-parts.md).  

## Connectivity  

### Expanded connectivity on premises

####  <a name="ODatav4"></a> Support for OData v4 data sources  
 The OData Source and the OData Connection Manager now support the OData v3 and v4 protocols.  
  
-   For OData V3 protocol, the component supports the ATOM and JSON data formats .  
  
-   For OData V4 protocol, the component supports the JSON data format .  
  
 For more info, see [OData Source](../integration-services/data-flow/odata-source.md).  

####  <a name="Excel2013"></a> Explicit support for Excel 2013 data sources  
 The Excel Connection Manager, the Excel Source and the Excel Destination, and the SQL Server Import and Export Wizard now provide explicit support for Excel 2013 data sources. 

####  <a name="HDFS"></a> Support for the Hadoop file system (HDFS)  
 Support for HDFS contains connection managers to connect to Hadoop clusters and tasks to do common HDFS  operations. For more info, see [Hadoop and HDFS Support in Integration Services &#40;SSIS&#41;](../integration-services/hadoop-and-hdfs-support-in-integration-services-ssis.md).  

####  <a name="more_hadoop"></a> Expanded support for Hadoop and HDFS  
  
-   The Hadoop Connection Manager now supports both Basic and Kerberos authentication. For more info, see [Hadoop Connection Manager](../integration-services/connection-manager/hadoop-connection-manager.md).  
  
-   The HDFS File Source and the HDFS File Destination how support both Text and Avro format. For more info, see  [HDFS File Source](../integration-services/data-flow/hdfs-file-source.md) and  [HDFS File Destination](../integration-services/data-flow/hdfs-file-destination.md).  
  
-   The Hadoop File System task now supports the CopyWithinHadoop option in addition to the CopyToHadoop and the CopyFromHadoop options. For more info, see [Hadoop File System Task](../integration-services/control-flow/hadoop-file-system-task.md).  

####  <a name="hdfsORC"></a> HDFS File Destination now supports ORC file format  
 The HDFS File Destination now supports the ORC file format in addition to Text and Avro. (The HDFS File Source supports only Text and Avro.) For more info about this component, see [HDFS File Destination](../integration-services/data-flow/hdfs-file-destination.md).  

####  <a name="odbc2016"></a> ODBC components updated for SQL Server 2016  
 The ODBC Source and Destination components have been updated to provide full compatibility with SQL Server 2016. There is no new functionality and there are no changes in behavior.  

####  <a name="Excel2016"></a> Explicit support for Excel 2016 data sources  
 The Excel Connection Manager, the Excel Source, and the Excel Destination now provide explicit support for Excel 2016 data sources.  

####  <a name="SAPBW"></a> Connector for SAP BW for SQL Server 2016 released  
 The MicrosoftÂ® Connector for SAP BW for Microsoft SQL ServerÂ® 2016 has been released as part of the SQL Server 2016 Feature Pack. To download components of the Feature Pack, see [MicrosoftÂ® SQL ServerÂ® 2016 Feature Pack](https://go.microsoft.com/fwlink/?LinkID=746297).
 
#### <a name="oracleteradata"></a> Connectors v4.0 for Oracle and Teradata released
The Microsoft Connectors v4.0 for Oracle and Teradata have been released. To download the connectors, see [Microsoft Connectors v4.0 for Oracle and Teradata](https://www.microsoft.com/download/details.aspx?id=52950).

### <a name="pdwau5"></a> Connectors for Analytics Platform System (PDW) Appliance Update 5 released
The destination adapters for loading data into PDW with AU5 have been released. To download the adapters, see [Analytics Platform System Appliance Update 5 Documentation and Client Tools](https://www.microsoft.com/download/details.aspx?id=51610).

### Expanded connectivity to the cloud

####  <a name="AFP2016"></a> Azure Feature Pack for SSIS released for SQL Server 2016  
 The Azure Feature Pack for Integration Services has been released for SQL Server 2016. The feature pack contains connection managers to connect to Azure data sources and tasks to do common Azure operations. For more info, see [Azure Feature Pack for Integration Services &#40;SSIS&#41;](../integration-services/azure-feature-pack-for-integration-services-ssis.md).  

#### <a name="dynamics"></a> Support for Microsoft Dynamics online resources released in Service Pack 1

With SQL Server 2016 Service Pack 1 installed, the OData Source and OData Connection Manager now support connecting to the OData feeds of Microsoft Dynamics AX Online and Microsoft Dynamics CRM Online.

#### <a name="datalakestore"></a> Support for Azure Data Lake Store released

The latest version of the Azure Feature Pack includes a connection manager, source, and destination to move data to and from Azure Data Lake Store. For more info, see [Azure Feature Pack for Integration Services &#40;SSIS&#41;](../integration-services/azure-feature-pack-for-integration-services-ssis.md)

#### <a name="sqldwupload"></a> Support for Azure SQL Data Warehouse released

The latest version of the Azure Feature Pack includes the Azure SQL DW Upload task for populating SQL Data Warehouse with data. For more info, see [Azure Feature Pack for Integration Services &#40;SSIS&#41;](../integration-services/azure-feature-pack-for-integration-services-ssis.md)

## Usability and productivity  
 
### Better install experience

####  <a name="Upgrade"></a> Upgrade blocked when SSISDB belongs to an Availability Group  
 If the SSIS catalog database (SSISDB) belongs to an Always On Availability Group, you have to remove SSISDB from the availability group, upgrade SQL Server, then add SSISDB back to the availability group. For more info, see [Upgrading SSISDB in an availability group](../integration-services/catalog/ssis-catalog.md#Upgrade).  

### Better design experience

####  <a name="OneDesigner"></a> Multi-targeting and multi-version support in SSIS Designer  
 You can now use SSIS Designer in SQL Server Data Tools (SSDT) for Visual Studio 2015 to create, maintain, and run packages that target SQL Server 2016, SQL Server 2014, or SQL Server 2012. To get SSDT, see [Download Latest SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt.md). 

 In Solution Explorer, right-click on an Integration Services project and select **Properties** to open the property pages for the project. On the **General** tab of **Configuration Properties**, select the **TargetServerVersion** property,  and then choose SQL Server 2016, SQL Server 2014, or SQL Server 2012.  
   
 ![TargetServerVersion property in project properties dialog box](../integration-services/media/targetserverversion2.png "TargetServerVersion property in project properties dialog box")  

> [!IMPORTANT]
> If you develop custom extensions for SSIS, see [Support multi-targeting in your custom components](../integration-services/extending-packages-custom-objects/support-multi-targeting-in-your-custom-components.md) and [Getting your SSIS custom extensions to be supported by the multi-version support of SSDT 2015 for SQL Server 2016](https://blogs.msdn.microsoft.com/ssis/2016/04/19/getting-your-ssis-custom-extensions-to-be-supported-by-the-multi-version-support-of-ssdt-2015-for-sql-server-2016/).  

### Better management experience in SQL Server Management Studio

####  <a name="CatViews"></a> Improved performance for SSIS Catalog views  
 Most SSIS catalog views now perform better when they're run by a user who is not a member of the ssis_admin role.  

### Other enhancements

####  <a name="BDDinbox"></a> Balanced Data Distributor transformation is now part of SSIS  
 The Balanced Data Distributor transformation, which required a separate download in previous versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], is now installed when you install [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. For more info, see [Balanced Data Distributor Transformation](../integration-services/data-flow/transformations/balanced-data-distributor-transformation.md).  
  
####  <a name="ComplexFeedinbox"></a> Data Feed Publishing Components are now part of SSIS  
 The Data Feed Publishing Components, which required a separate download in previous versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], are now installed when you install [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. For more info, see [Data Streaming Destination](../integration-services/data-flow/data-streaming-destination.md).  

####  <a name="AzureBlob"></a> Support for Azure Blob Storage in the SQL Server Import and Export Wizard  
 The SQL Server Import and Export Wizard can now import data from, and save data to, Azure Blob Storage. For more info, see [Choose a Data Source &#40;SQL Server Import and Export Wizard&#41;](../integration-services/import-export-data/choose-a-data-source-sql-server-import-and-export-wizard.md) and [Choose a Destination &#40;SQL Server Import and Export Wizard&#41;](../integration-services/import-export-data/choose-a-destination-sql-server-import-and-export-wizard.md). 

####  <a name="CDCOracle"></a> Change Data Capture Designer and Service for Oracle for Microsoft SQL Server 2016 released  
 The MicrosoftÂ® Change Data Capture Designer and Service for Oracle by Attunity for Microsoft SQL ServerÂ® 2016 have been released as part of the SQL Server 2016 Feature Pack.  These components now support Oracle 12c in classic installation. (Multitenant installation is not supported) To download components of the Feature Pack, see [MicrosoftÂ® SQL ServerÂ® 2016 Feature Pack](https://go.microsoft.com/fwlink/?LinkID=746297).  
  
####  <a name="cdc2016"></a> CDC components updated for SQL Server 2016  
 The CDC (Change Data Capture) Control Task, Source, and Splitter Transformation components have been updated to provide full compatibility with SQL Server 2016. There is no new functionality and there are no changes in behavior.  
  
####  <a name="ASDDL"></a> Analysis Services Execute DDL Task updated  
 The Analysis Services Execute DDL Task has been updated to accept Tabular Model Scripting Language commands.

####  <a name="ssasrc0"></a> Analysis Services tasks support tabular models  
 You can now use all the SSIS task and destinations that support SQL Server Analysis Services (SSAS) with SQL Server 2016 tabular models. The SSIS tasks have been updated to represent tabular objects instead of multidimensional objects. For example, when you select objects to process, the Analysis Services Processing Task automatically detects a Tabular model and displays a list of Tabular objects instead of showing measure groups and dimensions. The Partition Processing Destination now also shows tabular objects and supports pushing data into a partition.  
  
 The Dimension Processing Destination does not work for Tabular models with the SQL 2016 compatibility level.  The Analysis Services Processing Task and the Partition Processing Destination are all you need for tabular processing. 

####  <a name="builtinR"></a> Support for Built-in R Services  
 SSIS already supports the built-in R services in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. You can use SSIS not only to extract data and load the output of analysis, but to build, run and periodically retrain R models. For more info, see the following log post. [Operationalize your machine learning project using SQL Server 2016 SSIS and R Services](https://blogs.msdn.com/b/ssis/archive/2016/01/12/operationalize-your-machine-learning-project-using-sql-server-2016-ssis-and-r-services.aspx). 

####  <a name="ValidateXML"></a> Rich XML validation output in the XML Task  
 Validate XML documents and get rich error output by enabling the **ValidationDetails** property of the XML Task. Before the **ValidationDetails** property was available, XML validation by the XML Task returned only a true or false result, with no information about errors or their locations. Now, when you set **ValidationDetails** to true, the output file contains detailed information about every error including the line number and the position. You can use this information to understand, locate, and fix errors in XML documents. For more info, see [Validate XML with the XML Task](../integration-services/control-flow/validate-xml-with-the-xml-task.md).  
  
 [!INCLUDE[ssIS](../includes/ssis-md.md)] introduced the **ValidationDetails** property in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] Service Pack 2. This new property was not announced or documented at that time. The **ValidationDetails** property is also available in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] and in [!INCLUDE[ssSQL15](../includes/sssql15-md.md)].   

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]

## See Also  
 [What's New in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md)   
 [Editions and Supported Features for SQL Server 2016](../sql-server/editions-and-supported-features-for-sql-server-2016.md)
