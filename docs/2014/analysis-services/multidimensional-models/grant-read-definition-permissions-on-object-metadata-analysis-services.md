---
title: "Grant read definition permissions on object metadata (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "metadata [Analysis Services]"
  - "permissions [Analysis Services], read metadata"
  - "read metadata permissions"
ms.assetid: c857e48e-64b0-4ffe-900d-a0a3ddafcefb
author: minewiskan
ms.author: owend
manager: craigg
---
# Grant read definition permissions on object metadata (Analysis Services)
  Permission to read an object definition, or metadata, on selected objects lets an administrator grant permission to view object information, without also granting permission to modify the object's definition, modify the object's structure, or view the actual data for the object. `Read Definition` permissions can be granted at the database, data source, dimension, mining structure, and mining model levels. If you require `Read Definition` permissions for a cube, you must enable `Read Definition` for the database.Remember that permissions are additive. For example, one role grants permission to read the metadata for a cube, while a second role grants the same user permission to read the metadata for a dimension. The permissions from the two different roles combine to give the user permission to both read metadata for the cube and the metadata for the dimension within that database.  
  
> [!NOTE]  
>  Permission to read a database's metadata is the minimum permission required to connect to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database using either [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. A user who has permission to read metadata can also use the DISCOVER_XML_METADATA schema rowset to query the object and view its metadata. For more information, see [DISCOVER_XML_METADATA Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/xml/discover-xml-metadata-rowset).  
  
## Set read definition permissions on a database  
 Granting permission to read database metadata also grants permission to read the metadata of all objects in the database.  
  
 We suggest that you include the `Read Definition` permission at the database level whenever you are setting up roles for dedicated processing. Having `Read Definition` allows non-administrators to view a model's object hierarchy in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and navigate to individual objects for subsequent processing.  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], expand **Roles** for the appropriate database in Object Explorer, and then click a database role (or create a new database role).  
  
2.  On the **General** tab, select the `Read Definition` option.  
  
3.  In the **Membership** pane, enter the Windows user and group accounts that connect to Analysis Services using this role.  
  
4.  Click **OK** to finish creating the role.  
  
## Set read definition permissions on individual objects  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], open the **Databases** folder, select a database, expand **Roles** for the appropriate database in Object Explorer, and then click a database role (or create a new database role).  
  
2.  In the **General** pane, clear the database permission for `Read Definition`. This step removes permission inheritance so that you can set permissions on individual objects.  
  
3.  Select the object for which you are specifying `Read Definition` properties:  
  
    -   In the **Data Sources** pane, click the `Read Definition` check box for that data source. Role members can view the connection string to the data source, including the server name and possibly the user name. This permission is available in case you want to provide connection string information, without also granting permission to modify the connection string or view the definitions of any other objects.  
  
    -   In the **Dimensions** pane, click the `Read Definition` check box for that dimension. Experienced analysts and developers may need to view the definition without permission to modify it or view the definitions of other objects (such as other dimensions, cube objects, or mining structures and models).  
  
    -   In the Mining Structures pane, click the `Read Definition` check box for data mining structures or models. `Read Definition` is required for browsing the data model. See [Grant permissions on data mining structures and models &#40;Analysis Services&#41;](grant-permissions-on-data-mining-structures-and-models-analysis-services.md) for details.  
  
4.  In the **Membership** pane, enter the Windows user and group accounts that connect to Analysis Services using this role.  
  
5.  Click **OK** to finish creating the role.  
  
## See Also  
 [Grant database permissions &#40;Analysis Services&#41;](grant-database-permissions-analysis-services.md)   
 [Grant process permissions &#40;Analysis Services&#41;](grant-process-permissions-analysis-services.md)  
  
  
