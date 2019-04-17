---
title: "Compatibility Level (SSAS Tabular SP1) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.versioncompat.f1"
ms.assetid: 8943d78d-4a73-4be8-ad14-3d428f5abd06
author: minewiskan
ms.author: owend
manager: craigg
---
# Compatibility Level (SSAS Tabular SP1)
  You can specify *Compatibility Level* when creating new Tabular model projects, when upgrading existing Tabular model projects, when upgrading existing deployed Tabular model databases, or when importing PowerPivot workbooks.  
  
## Compatibility Level  
 It is common to install new versions and service packs on development and test computers prior to installing on production computers. In such cases, it is important to understand setting compatibility level for both new Tabular model projects as well as those that have already been deployed into a production environment.  
  
 A [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Analysis Services instance supports the following compatibility levels (database version):  
  
-   SQL Server 2012 (1100)  
  
-   SQL Server 2012 SP1 (1103)  
  
-   SQL Server 2014 (1103)  
  
### Set compatibility level when creating a new Tabular model project  
 When creating a new Tabular model project in SQL Server Data Tools (SSDT), on the **New Tabular project options** dialog you can specify the compatibility level. You can choose between creating a new project to be deployed to an [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] or later Analysis Services instance or to an SQL Server 2012 Analysis Services instance (without Service Pack 1).  
  
 You can also specify a default compatibility level by selecting the **Do not show this message again** option. All subsequent projects will use the compatibility level you specified. You can change the default compatibility level in SSDT in Options.  
  
### Upgrade an existing Tabular model project to 1103 compatibility level  
 You can upgrade a Tabular model project created in SSDT prior to installing [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] or later to be database version 1103 compatible by using the **Compatibility Level** property in the model **Properties** window. In order to upgrade a Tabular model project, the computer on which SSDT is installed must have [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] or later installed and the Analysis Services instance on which the workspace database resides must also have [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] or later installed. You cannot downgrade to an earlier version.  
  
### Upgrade a deployed Tabular model database to 1103 compatibility level  
 You can upgrade an existing deployed Tabular model database to database version 1103 compatible in SQL Server Management Studio (SSMS) by using the **Compatibility Level** property in **Database Properties**. In order to upgrade, the computer on which the SQL Server Analysis Services instance is installed must have [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)] or later installed. You cannot downgrade a deployed Tabular model database to an earlier version.  
  
### Import from PowerPivot  
 When creating a new Tabular model project by importing from PowerPivot, you can specify if you want to upgrade the compatibility level to the default compatibility level (if previously configured in SSDT) or leave the compatibility level to that already specified in the PowerPivot workbook.  
  
### Check compatibility level for a Tabular model database in SSMS  
 You can check the compatibility level for a Tabular model database in SSMS by viewing the **Compatibility Level** property (new in [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]) in **Database Properties**.  
  
### Check supported compatibility level for an Analysis Services instance in SSMS  
 You can check the supported compatibility level in SSMS by viewing the **Supported Compatibility Level** property on the **Information** page (new in [!INCLUDE[ssSQL11SP1](../../includes/sssql11sp1-md.md)]) in **Analysis Services Properties**. A supported compatibility level of 1103 indicates SQL Server SP1 or later is installed. The supported compatibility level cannot be changed.  
  
  
