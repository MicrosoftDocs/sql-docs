---
title: "Multidimensional Model Data Access (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Multidimensional Model Data Access (Analysis Services - Multidimensional Data)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Use the information in this topic to learn how to access [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] multidimensional data using programmatic methods, script, or client applications that include built-in support for connecting to an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] server on your network.  
  
 This topic contains the following sections:  
  
 [Client Applications](#bkmk_clientapps)  
  
 [Query Languages](#bkmk_querylang)  
  
 [Programmatic Interfaces](#bkmk_api)  
  
##  <a name="bkmk_clientapps"></a> Client Applications  
 Although Analysis Services provides interfaces that let you build or integrate multidimensional databases programmatically, a more common approach is to use existing client applications from Microsoft and other software vendors that have built-in data access to Analysis Services data.  
  
 The following Microsoft applications support native connections to multidimensional data.  
  
### Excel  
 Analysis Services multidimensional data is often presented using pivot tables and pivot chart controls in an Excel workbook. PivotTables are suited to multidimensional data because the hierarchies, aggregations, and navigational constructs in the model pair well with the data summary features of a PivotTable. An Analysis Services OLE DB data provider is included in an Excel installation to make setting up data connections easier. For more information, see [Connect to or import data from SQL Server Analysis Services](http://go.microsoft.com/fwlink/?linkID=215150).  
  
### Reporting Services Reports  
 You can use Report Builder or Report Designer to create reports that consume Analysis Services databases that contain analytical data. Both Report Builder and Report Designer include an MDX query designer that you can use to type or design MDX statements that retrieve data from an available data source. For more information, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md) and [Analysis Services Connection Type for MDX &#40;SSRS&#41;](../../../reporting-services/report-data/analysis-services-connection-type-for-mdx-ssrs.md).  
  
### PerformancePoint Dashboards  
 PerformancePoint Dashboards are used to create scorecards in SharePoint that communicate business performance against predefined measures. PerformancePoint includes support for data connections to Analysis Services multidimensional data. For more information, [Create an Analysis Services data connection (PerformancePoint Services)](http://go.microsoft.com/fwlink/?linkid=232471).  
  
### SQL Server Data Tools  
 Model and report designers use SQL Server Data Tools to build solutions that include multidimensional models. Deploying the solution to an Analysis Services instance is what creates the database that you subsequently connect to from Excel, Reporting Services, and other business intelligence client applications.  
  
 SQL Server Data Tools is built on a Visual Studio shell and uses projects to organize and contain the model. For more information, see [Creating Multidimensional Models Using SQL Server Data Tools &#40;SSDT&#41;](../../../analysis-services/multidimensional-models/creating-multidimensional-models-using-sql-server-data-tools-ssdt.md).  
  
### SQL Server Management Studio  
 For database administrators, SQL Server Management Studio is an integrated environment for managing your SQL Server instances, including instances of Analysis Services and multidimensional databases. For more information, see [SQL Server Management Studio](http://msdn.microsoft.com/library/66a6b7b1-de6a-4161-82bd-98ded486947b) and [Connect to Analysis Services](../../../analysis-services/instances/connect-to-analysis-services.md).  
  
##  <a name="bkmk_querylang"></a> Query Languages  
 MDX is an industry standard query and calculation language used to retrieve data from OLAP databases. In Analysis Services, MDX is the query language used to retrieve data, but also supports data definition and data manipulation. MDX editors are built into SQL Server Management Studio, Reporting Services, and SQL Server Data Tools. You can use the MDX editors to create ad hoc queries or reusable script if the data operation is repeatable.  
  
 Some tools and applications, such as Excel, use MDX constructs internally to query an Analysis Services data source. You can also use MDX programmatically, by enclosing MDX statement in an XMLA Execute request.  
  
 The following links provide more information about MDX:  
  
 [Querying Multidimensional Data with MDX](../../../analysis-services/multidimensional-models/mdx/querying-multidimensional-data-with-mdx.md)  
  
 [Key Concepts in MDX &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/key-concepts-in-mdx-analysis-services.md)  
  
 [MDX Query Fundamentals &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-query-fundamentals-analysis-services.md)  
  
 [MDX Scripting Fundamentals &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/mdx/mdx-scripting-fundamentals-analysis-services.md)  
  
##  <a name="bkmk_api"></a> Programmatic Interfaces  
 If you are building a custom application that uses multidimensional data, your approach for accessing the data will most likely fall into one of the following categories:  
  
-   **XMLA**. Use XMLA when you require compatibility with a wide variety of operating systems and protocols. XMLA offers the greatest flexibility, but often at the cost of improved performance and ease of programming.  
  
-   **Client libraries**. Use Analysis Services client libraries, such as ADOMD.NET, AMO, and OLE DB when you want to access data programmatically from client applications that run on a Microsoft Windows operating system. The client libraries wrap XMLA with an object model and optimizations that provide better performance.  
  
     ADOMD.NET and AMO client libraries are for applications written in managed code. Use OLE DB for Analysis Services if your application is written in native code.  
  
 The following table provides additional detail and links about the client libraries used for connecting Analysis Services to a custom application.  
  
|Interface|Description|  
|---------------|-----------------|  
|Analysis Services Management Objects (AMO)|AMO is the primary object model for administering Analysis Services instances and multidimensional databases in code. For example, SQL Server Management Studio uses AMO to support server and database administration. For more information, see [Developing with Analysis Management Objects &#40;AMO&#41;](https://docs.microsoft.com/bi-reference/amo/developing-with-analysis-management-objects-amo).|  
|ADOMD.NET|ADOMD.NET is the primary object model creating and accessing multidimensional data in custom applications. You can use ADOMD.NET in a managed client application to retrieve [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] information using common Microsoft .NET Framework data access interfaces. For more information, see [Developing with ADOMD.NET](https://docs.microsoft.com/bi-reference/adomd/developing-with-adomd-net) and [ADOMD.NET Client Programming](https://docs.microsoft.com/bi-reference/adomd/multidimensional-models-adomd-net-client/adomd-net-client-programming).|  
|Analysis Services OLE DB Provider (MSOLAP.dll)|You can use the native OLE DB provider to access [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] programmatically from a non-managed API. For more information, see [Analysis Services OLE DB Provider &#40;Analysis Services - Multidimensional Data&#41;](http://msdn.microsoft.com/library/cdeecd50-1d91-4162-a4a2-01c7799b02a8).|  
|Schema Rowsets|Schema rowset tables are data structures that contain descriptive information about a multidimensional model that is deployed on the server, as well as information about current activity on the server. As a programmer, you can query schema rowset tables in client applications to examine metadata stored on, and retrieve support and monitoring information from, an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. You can use schema rowsets with these programmatic interfaces: OLE DB, OLE DB for Analysis Services, OLE DB for Data Mining, or XMLA. For more information, see [Analysis Services Schema Rowsets](https://docs.microsoft.com/bi-reference/schema-rowsets/analysis-services-schema-rowsets).<br /><br /> The following list explains several approaches for using schema rowsets:<br /><br /> -Run DMV queries in SQL Server Management Studio or in custom reports to access schema rowsets using SQL syntax. For more information, see [Use Dynamic Management Views &#40;DMVs&#41; to Monitor Analysis Services](../../../analysis-services/instances/use-dynamic-management-views-dmvs-to-monitor-analysis-services.md).<br /><br /> -Write ADOMD.NET code that calls a schema rowset.<br /><br /> -Run the XMLA **Discover** method directly against an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance to retrieve schema rowset information. For more information, see [Discover Method &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-discover).|  
|XMLA|XMLA is the lowest level API available to an Analysis Services programmer, and is the common denominator that underlies all Analysis Services data access methodologies. XMLA is an industry standard, SOAP based XML protocol that supports universal data access to any standard multidimensional data source available over an HTTP connection. It uses SOAP to formulate requests and responses for multidimensional data. If your application runs on a non-Windows platform, you can use XMLA to access a multidimensional database that is running on a Windows server on your network. For more information, see [Developing with XMLA in Analysis Services](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md).|  
|Analysis Services Scripting Language (ASSL)|ASSL is a descriptive term that applies to Analysis Services extensions of the XMLA protocol. Whereas the Execute and Discover methods are described by the XMLA protocol, ASSL adds the following capability:<br /><br /> -XMLA script<br /><br /> -XMLA object definitions<br /><br /> -XMLA commands<br /><br /> ASSL extensions enable Analysis Services to use XMLA constructs beyond the basic provisions of the protocol, adding data definition, data manipulation, and data control support. For more information, see [Developing with Analysis Services Scripting Language &#40;ASSL&#41;](../../../analysis-services/multidimensional-models/scripting-language-assl/developing-with-analysis-services-scripting-language-assl.md).|  
  
## See Also  
 [Connect to Analysis Services](../../../analysis-services/instances/connect-to-analysis-services.md)   
 [Developing with Analysis Services Scripting Language &#40;ASSL&#41;](../../../analysis-services/multidimensional-models/scripting-language-assl/developing-with-analysis-services-scripting-language-assl.md)   
 [Developing with XMLA in Analysis Services](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md)   

  
  
