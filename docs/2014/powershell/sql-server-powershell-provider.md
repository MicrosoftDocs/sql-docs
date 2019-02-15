---
title: "SQL Server PowerShell Provider | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: scripting
ms.topic: conceptual
helpviewer_keywords: 
  - "PowerShell [SQL Server], provider"
  - "PowerShell [SQL Server], SQL Server PowerShell Provider"
  - "Providers [PowerShell]"
  - "SMO [SQL Server], PowerShell"
  - "PowerShell [SQL Server], SMO"
  - "SQL Server Management Objects, PowerShell"
ms.assetid: b97acc43-fcd2-4ae5-b218-e183bab916f9
author: stevestein
ms.author: sstein
manager: craigg
---
# SQL Server PowerShell Provider
  The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider for Windows PowerShell exposes the hierarchy of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] objects in paths similar to file system paths. You can use the paths to locate an object, and then use methods from the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Management Object (SMO) models to perform actions on the objects.  
  
## Benefits of the SQL Server PowerShell Provider  
 The paths implemented by the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider enable easily and interactively reviewing all of the objects in an instance of SQL Server. You can navigate the paths using Windows PowerShell aliases similar to the commands you typically use to navigate file system paths.  
  
## The SQL Server PowerShell Hierarchy  
 Products whose data or object models can be represented in a hierarchy use Windows PowerShell providers to expose the hierarchies. The hierarchy is exposed by using a drive and path structure similar to what the Windows file system uses.  
  
 Each Windows PowerShell provider implements one or more drives. Each drive is the root node of a hierarchy of related objects. The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider implements a SQLSERVER: drive. The provider also defines a set of primary folders for the SQLSERVER: drive. Each folder and its subfolders represent the set of objects that can be accessed by using a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] management object model. When you are focused on a subfolder in a path that starts with one of these primary folders, you can use the methods from the associated object model to perform actions on the object that is represented by the node. The Windows PowerShell folders implemented by the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] provider are listed in the following table.  
  
|Folder|SQL Server object model namespace|Objects|  
|------------|---------------------------------------|-------------|  
|SQLSERVER:\SQL|<xref:Microsoft.SqlServer.Management.Smo><br /><br /> <xref:Microsoft.SqlServer.Management.Smo.Agent><br /><br /> <xref:Microsoft.SqlServer.Management.Smo.Broker><br /><br /> <xref:Microsoft.SqlServer.Management.Smo.Mail>|Database objects, such as tables, views, and stored procedures.|  
|SQLSERVER:\SQLPolicy|<xref:Microsoft.SqlServer.Management.Dmf><br /><br /> <xref:Microsoft.SqlServer.Management.Facets>|Policy-based management objects, such as policies and facets.|  
|SQLSERVER:\SQLRegistration|<xref:Microsoft.SqlServer.Management.RegisteredServers><br /><br /> <xref:Microsoft.SqlServer.Management.Smo.RegSvrEnum>|Registered server objects, such as server groups and registered servers.|  
|SQLSERVER:\Utility|<xref:Microsoft.SqlServer.Management.Utility>|Utility objects, such as managed instances of the [!INCLUDE[ssDE](../includes/ssde-md.md)].|  
|SQLSERVER:\DAC|<xref:Microsoft.SqlServer.Management.DAC>|Data-tier application objects such as DAC packages, and operations such as deploying a DAC.|  
|SQLSERVER:\DataCollection|<xref:Microsoft.SqlServer.Management.Collector>|Data collector objects, such as collection sets and configuration stores.|  
|SQLSERVER:\IntegrationServices|<xref:Microsoft.SqlServer.Management.IntegrationServices>|[!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] objects such as projects, packages, and environments.|  
|SQLSERVER:\SQLAS|<xref:Microsoft.AnalysisServices>|[!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] objects such as cubes, aggregations, and dimensions.|  
  
 For example, you can use the SQLSERVER:\SQL folder to start paths that can represent any object that is supported by the SMO object model. The leading part of a SQLSERVER:\SQL path is SQLSERVER:\SQL\\*ComputerName*\\*InstanceName*. The nodes after the instance name alternate between object collections (such as *Databases* or *Views*) and object names (such as AdventureWorks2012). Schemas are not represented as object classes. When you specify the node for a top-level object in a schema, such as a table or view, you must specify the object name in the format *SchemaName.ObjectName*.  
  
 This is the path of the Vendor table in the Purchasing schema of the AdventureWorks2012 database in a default instance of the [!INCLUDE[ssDE](../includes/ssde-md.md)] on the local computer:  
  
```  
SQLSERVER:\SQL\localhost\DEFAULT\Databases\AdventureWorks2012\Tables\Purchasing.Vendor  
```  
  
 For more information about the SMO object model hierarchy, see [SMO Object Model Diagram](../relational-databases/server-management-objects-smo/smo-object-model-diagram.md).  
  
 Collection nodes in a path are associated with a collection class in the associated object model. Object name nodes are associated with an object class in the associated object model, as in the following table.  
  
|Path|SMO class|  
|----------|---------------|  
|SQLSERVER:\SQL\MyComputer\DEFAULT\Databases|<xref:Microsoft.SqlServer.Management.Smo.DatabaseCollection>|  
|SQLSERVER:\SQL\MyComputer\DEFAULT\Databases\AdventureWorks2012|<xref:Microsoft.SqlServer.Management.Smo.Database>|  
  
## SQL Server Provider Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to use Windows PowerShell cmdlets to navigate through the nodes in a path, and at each node get a list of the objects at that node.|[Navigate SQL Server PowerShell Paths](navigate-sql-server-powershell-paths.md)|  
|Describes how to use the SMO methods and properties to report on and perform work on the object represented by a node in a path. Also describes how to get a list of the SMO methods and properties for that node.|[Work With SQL Server PowerShell Paths](work-with-sql-server-powershell-paths.md)|  
|Describes how to convert a SMO Uniform Resource Name (URN) to a SQL Server provider path.|[Convert URNs to SQL Server Provider Paths](../database-engine/convert-urns-to-sql-server-provider-paths.md)|  
|Describes how to open SQL Server Authentication connections by using the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider. By default, the provider uses Windows Authentication connections made using the credentials of the Windows account running the Windows PowerShell session.|[Manage Authentication in Database Engine PowerShell](manage-authentication-in-database-engine-powershell.md)|  
  
## See Also  
 [SQL Server PowerShell](sql-server-powershell.md)  
  
  
