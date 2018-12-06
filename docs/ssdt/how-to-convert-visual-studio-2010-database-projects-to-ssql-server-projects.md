---
title: "How to: Convert a Visual Studio 2010 Database Projects to SQL Server Database Projects and Retarget to a Different Platform | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.projectconversion.dialog"
  - "sql.data.tools.ImportDAC"
ms.assetid: 7e5acf94-5c46-44c7-9ff5-ca7926f5332a
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Convert a Visual Studio 2010 Database Projects to SQL Server Database Projects and Retarget to a Different Platform
In SQL Server Data Tools (SSDT), you can convert existing SQL Server Database, CLR, and Data-Tier Application projects created in Visual Studio 2010 to the new SQL Server database project. By doing so, you can take advantage of the new database development experience that SSDT provides, such as an updated Transact\-SQL editing experience, and the ability to retarget your project to Microsoft SQL Server 2012 and SQL Azure with code validation. The conversion process converts objects (table, views, stored-procedures, property files, or scripts) that have an equivalent type in SSDT, including their permissions and DAC policy files. Artifacts that cannot be converted are highlighted in a conversion log report.  
  
The following table lists all the project artifacts that can or cannot be converted by SSDT.  
  
|Project artifacts that can be converted|Project artifacts that cannot be converted|  
|-------------------------------------------|----------------------------------------------|  
|Project Files<br /><br />.dbproj (Visual Studio 2010 Database and Server projects, Data-Tier Application projects) project files<br /><br />.csproj and .vbproj CLR project files can be converted, but may result in data loss|Database Unit Test Projects<br /><br />Partial Projects such as .files items|  
|Properties Files<br /><br />*.sqldeployment, .sqlsettings, and .sqlpolicy files are converted to their corresponding Project Property pages<br /><br />.sqlpermissions files are converted to Transact\-SQL scripts|Project Properties<br /><br />Server.sqlsettings<br /><br />SQLCMD variables defined in .sqlcmd files|  
|.sql files are imported using their existing folder structure.|Extensibility files.|  
|Pre-Deployment and Post-Deployment scripts|Database references will have to manually re-established after project conversion.|  
|Schema Compare files|Data Generation files.|  
  
### To convert a project  
  
1.  Open a SQL Server 2005 or 2008 Database Project.  
  
2.  The **Convert to SQL Server Database project** wizard automatically opens. Select **Convert to SQL Server Database project** and click **OK**. Keep the default setting to backup existing files checked.  
  
3.  A conversion report is automatically generated, listing all files that have been converted. To read more information about the conversion process, click the **+** sign next to the project filename.  
  
4.  Notice that in **Solution Explorer**, project file, property files, and schema objects are all converted.  
  
### To change a project's target platform  
  
1.  Right-click your newly converted project in **Solution Explorer** and select **Properties** to access the **Project Settings** dialog box.  
  
2.  Select any of the SSDT-supported platforms in the **Target platform** dropdown list.  
  
## See Also  
[How to: Change Target Platform and Publish a Database Project](../ssdt/how-to-change-target-platform-and-publish-a-database-project.md)  
  
