---
title: "Tools and applications used in Analysis Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
ms.assetid: 0ddb3b7a-7464-4d04-8659-11cb2e4cf3c3
caps.latest.revision: 17
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Tools and applications used in Analysis Services
  Find the tools and applications you'll need for building Analysis Services models and managing the associated databases on an Analysis Services instance.  
  
## Analysis Services Model Designers  
 Tabular and multidimensional models are created from project templates in a solution built inside a Visual Studio shell. The project template provides the designers for creating tables, relationships, cubes, dimensions, and roles that comprise an Analysis Services solution. The shell provides the visual workspace, property pages, and command framework within which the project is created. The model designer that provides both shell and templates is a free web download.  
  
 Models have a compatibility level setting that determines feature availability and which release of Analysis Services run the model.  Whether you can specify a given compatibility level is determined in part by the model designer.  
  
 Tabular models using the latest functionality in SQL Server 2016, such as BIM files in tabular JSON format and bi-directional cross filtering, must be created at compatibility level 1200, in the version of SQL Server Data Tools for Visual Studio 2015 that ships concurrently with SQL Server 2016 (see below for the download link).  
  
 If you require a lower compatibility level, perhaps because you want to deploy a model on an earlier version of Analysis Services, you can still use the model designer in SSDT for Visual Studio 2015. Newer versions of the tool support creating any model type (tabular or multidimensional), at any compatibility level you require. There is no need to keep earlier tools around just to  build or edit an older model.  
  
### Download the model designer  
 [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], previously known as SQL Server Data Tools for Business Intelligence (SSDT-BI) and before that, as Business Intelligence Development Studio (BIDS), is used to create Analysis Services models.  
  
||  
|-|  
|**[Download SSDT for Visual Studio 2015](https://msdn.microsoft.com/mt429383)**|  
  
 We recommend using SQL Server Data Tools for Visual Studio 2015 over previous versions of the designer. It contains project templates for all SQL Server content types, including the relational database, Analysis Services models, Reporting Services reports, and Integration Services packages.  
  
 SSDT runs in the Visual Studio 2015 shell. If you already have Visual Studio 2015, SSDT Setup adds just the project templates. If you don't have Visual Studio 2015, both shell and templates are installed.  
  
 If you have a prior version of SSDT-BI or BIDS installed on your computer, the newer version is installed side-by-side the previous version.  
  
 After you install SSDT, you should see the Business Intelligence templates in the New Project dialog box.  
  
 ![New Project templates in SSDT](../analysis-services/media/ssdt-biprojects.png "New Project templates in SSDT")  
  
## Administrative tools  
  
### Download SQL Server Management Studio  
 Management Studio is the primary administration tool for all SQL Server features, including Analysis Services. It is now a separate download.  
  
||  
|-|  
|**[Download SQL Server Management Studio](https://msdn.microsoft.com/library/mt238290.aspx)**|  
  
 In SQL Server 2016, Management Studio includes extended events (xEvents) for Analysis Services, providing a lightweight alternative to SQL Server Profiler traces used for monitoring activity and diagnosing server problems. See [Monitor Analysis Services with SQL Server Extended Events](../analysis-services/instances/monitor-analysis-services-with-sql-server-extended-events.md) to learn more.  
  
### SQL Server Profiler  
 Although it's officially deprecated in favor of xEvents, SQL Server Profiler provides a familiar approach for monitoring connections, MDX query execution, and other server operations. SQL Server Profiler is installed by default. You can find it with SQL Server applications on Apps in Windows Server 2012.  
  
### PowerShell  
 You can use PowerShell commands to perform many administrative tasks. See [PowerShell scripting in Analysis Services](../analysis-services/instances/powershell-scripting-in-analysis-services.md) for more information.  
  
### Community and Third-party tools  
 Check the [Analysis Services codeplex page](http://sqlsrvanalysissrvcs.codeplex.com/) for community code samples. [Forums](http://social.msdn.microsoft.com/Forums/sqlserver/home?forum=sqlanalysisservices) can be helpful when seeking recommendations for third-party tools that support Analysis Services.  
  
## See Also  
 [Compatibility Level of a Multidimensional Database &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/compatibility-level-of-a-multidimensional-database-analysis-services.md)   
 [Compatibility Level for Tabular models in Analysis Services](../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md)  
  
  