---
title: "Database Properties Dialog Box (SSAS - Multidimensional) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.sqlserverstudio.databaseproperties.f1"
ms.assetid: 70f000b7-917f-4699-b142-7a0d13ff767c
author: minewiskan
ms.author: owend
manager: craigg
---
# Database Properties Dialog Box (SSAS - Multidimensional)
  Use the **Database Properties** dialog box in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to set the properties of a database in an [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database. You can display the **Database Properties** dialog box by right-clicking a database in Object Explorer and selecting **Properties**.  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Name**|Type to change the name of the database.|  
|**ID**|Displays the identifier of the database.|  
|**Description**|Type to change the description of the database.|  
|**Create Timestamp**|Displays the date and time the database was created.|  
|**Last Schema Update**|Displays the date and time the metadata for the database was last updated.|  
|**Last Update**|Displays the date and time the data for the database was last updated.|  
|**Data Source Impersonation Info**|Select the impersonation information used by the database when connecting to and interacting with data sources contained by the database. Valid values include the following:<br /><br /> **ImpersonateAccount** (use a specific Windows user name and password).<br /><br /> **ImpersonateService** (use the service account).<br /><br /> **ImpersonateCurrentUser** (use the credentials of the current user).<br /><br /> **Default** (use the service account for MOLAP operations and current user for data mining).<br /><br /> Although you can set data source impersonation settings at the database level, doing so will only affect those data sources that specify **Inherit** for their impersonation settings. Impersonation settings specified directly on the data source will always override any settings that are specified at the database level.<br /><br /> When choosing an impersonation option, consider the types of operations that will need to be supported. Some operations, such as processing, cannot be performed by|  
|**Last Processed**|Displays the date and time the database was last processed.|  
|**Estimated Size**|Displays the estimated size of the database.|  
|**Storage Location**|Specifies the location of the database. If the database is located in the default Data directory, this value will be empty.|  
  
## See Also  
 [Analysis Services Designers and Dialog Boxes &#40;Multidimensional Data&#41;](analysis-services-designers-and-dialog-boxes-multidimensional-data.md)   
 [Multidimensional Model Databases &#40;SSAS&#41;](multidimensional-models/multidimensional-model-databases-ssas.md)  
  
  
