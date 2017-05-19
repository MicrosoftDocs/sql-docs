---
title: "Compatibility Level for Tabular models in Analysis Services | Microsoft Docs"
ms.custom: ""
ms.date: "05/09/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.asvs.bidtoolset.versioncompat.f1"
ms.assetid: 8943d78d-4a73-4be8-ad14-3d428f5abd06
caps.latest.revision: 27
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Compatibility level for Analysis Services tabular models
  The *compatibility level* refers to release-specific behaviors in the Analysis Services engine. For example, DirectQuery and tabular object metadata have different implementations depending on the compatibility level. In-general, you should choose the latest compatibility level supported by your servers.

  **The latest compatibility level is 1400** 
  
## Supported compatibility levels by version
  
|||  
|-|-|- 
|**Compatibility level**|**Server version**| 
|1400|Azure Analysis Services, SQL Server 2017 |  
|1200|Azure Analysis Services, SQL Server 2017, SQL Server 2016| 
|1103|SQL Server 2017, SQL Server 2016, SQL Server 2014, SQL Server 2012 SP1|  
|1100|SQL Server 2017, SQL Server 2016, SQL Server 2014, SQL Server 2012 SP1, SQL Server 2012| 

  
## Set compatibility level 
 When creating a new Tabular model project in SQL Server Data Tools (SSDT), you can specify the compatibility level on the **Tabular model designer** dialog. 
  
 ![ssas_tabularproject_compat1200](../../analysis-services/tabular-models/media/ssas-tabularproject-compat1200.png)  
  
 If you select the **Do not show this message again** option, all subsequent projects will use the compatibility level you specified as the default. You can change the default compatibility level in SSDT in **Tools** > **Options**.  
  
 To upgrade a tabular model project in SSDT, set  the **Compatibility Level** property in the model **Properties** window. Keep in-mind, upgrading the compatibility level is irreversible.
  
## Check compatibility level for a database in SSMS  
 In SSMS, right-click the database name > **Properties** > **Compatibility Level**.  
  
## Check supported compatibility level for a server in SSMS  
 In SSMS, right-click the server name>  **Properties** > **Supported Compatibility Level**.  
  
 This property specifies the highest compatibility level of a database that will run on the server. The supported compatibility level is read-only cannot be changed.  
  
## See Also  
 [Compatibility Level of a multidimensional database](../../analysis-services/multidimensional-models/compatibility-level-of-a-multidimensional-database-analysis-services.md)   
 [What's new in Analysis Services](../../analysis-services/what-s-new-in-analysis-services.md)   
 [Create a new tabular model project](../../analysis-services/tabular-models/create-a-new-tabular-model-project-analysis-services.md)  
  
  
