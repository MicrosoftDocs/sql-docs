---
title: "Grant permissions on a dimension (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.roledesignerdialog.dimensions.f1"
helpviewer_keywords: 
  - "dimensions [Analysis Services], security"
  - "read/write permissions"
  - "user access rights [Analysis Services], dimensions"
  - "permissions [Analysis Services], dimensions"
ms.assetid: be5b2746-0336-4b12-827e-131462bdf605
author: minewiskan
ms.author: owend
manager: craigg
---
# Grant permissions on a dimension (Analysis Services)
  Dimension security is used to set permissions on a dimension object, not its data. Typically, allowing or denying access to processing operations is the main objective when setting permissions on a dimension.  
  
 But perhaps your objective is not to control processing operations, but rather data access to a dimension, or the attributes and hierarchies it contains. For example, a company with regional sales divisions might want to make sales performance information off limits to those outside the division. To allow or deny access to portions of dimension data for different constituents, you can set permissions on dimension attributes and dimension members. Notice that you cannot deny access to an individual dimension object itself, only to its data. If your immediate goal is to allow or deny access to members in a dimension, including access rights to individual attribute hierarchies, see [Grant custom access to dimension data &#40;Analysis Services&#41;](grant-custom-access-to-dimension-data-analysis-services.md) for more information.  
  
 The remainder of this topic covers permissions that you can set on the dimension object itself, including:  
  
-   Read or Read/Write permissions (you can only choose from Read or Read/Write; specifying "none" is not an option). As noted, if your goal is to restrict access to dimension data, see [Grant custom access to dimension data &#40;Analysis Services&#41;](grant-custom-access-to-dimension-data-analysis-services.md) for details.  
  
-   Processing permissions (do this when scenarios require a processing strategy that calls for custom permissions on individual objects)  
  
-   Read definition permissions (typically you would do this to support interactive processing in a tool, or to provide visibility into a model. Read definition lets you see the structure of a dimension, without permission to its data or the ability to modify its definition).  
  
 When defining roles for a dimension, available permissions vary depending on whether the object is a standalone database dimension ─internal to the database but external to a cube─ or a cube dimension.  
  
> [!NOTE]  
>  By default, permissions on a database dimension are inherited by a cube dimension. For example, if you enable **Read/Write** on a Customer database dimension, the Customer cube dimension inherits **Read/Write** in the context of the current role. You can clear inherited permissions if you want to override a permission setting.  
  
## Set permissions on a database dimension  
 Database dimensions are standalone objects within a database, allowing for dimension reuse within the same model. Consider a DATE database dimension that is used multiple times in a model, as Order Date, Ship Date, and Due Date cube dimensions. Because cubes and database dimensions are peer objects in a database, you can set processing permissions independently on each object.  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], expand **Roles** for the appropriate database in Object Explorer, and then click a database role (or create a new database role).  
  
2.  In the **Dimensions** pane, the dimension set should be set to **All database dimensions**.  
  
     By default, permissions are set to **Read**.  
  
     Although **Read/Write** is available, we recommend that you do not use this permission. **Read/Write** is used for dimension writeback scenarios, which have been deprecated. See [Deprecated Analysis Services Features in SQL Server 2014](../deprecated-analysis-services-features-in-sql-server-2014.md).  
  
     Optionally, you can set **Read Definition** and **Process** permissions on individual dimension objects, as long as those permissions are not already set at the database level. See [Grant process permissions &#40;Analysis Services&#41;](grant-process-permissions-analysis-services.md) and [Grant read definition permissions on object metadata &#40;Analysis Services&#41;](grant-read-definition-permissions-on-object-metadata-analysis-services.md) for details.  
  
## Set permissions on a cube dimension  
 Cube dimensions are database dimensions that have been added to a cube. As such, they are structurally dependent on associated measure groups. Although you can process these objects atomically, in terms of authorization, it makes sense to treat the cube and cube dimensions as a single entity.  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], expand **Roles** for the appropriate database in Object Explorer, and then click a database role (or create a new database role).  
  
2.  In the **Dimensions** pane, change the dimension set to \<cube-name> **cube dimensions**.  
  
     By default, permissions are inherited from a corresponding database dimension. Clear the **Inherit** check box to alter permissions from **Read** to **Read/Write**. Before using **Read/Write**, be sure to read the note in the previous section.  
  
> [!IMPORTANT]  
>  If you configure database role permissions by using Analysis Management Objects (AMO), any reference to a cube dimension in a cube's DimensionPermission attribute severs the permission inheritance from the database's DimensionPermission attribute. For more information about AMO, see [Developing with Analysis Management Objects &#40;AMO&#41;](https://docs.microsoft.com/bi-reference/amo/developing-with-analysis-management-objects-amo).  
  
## See Also  
 [Roles and Permissions &#40;Analysis Services&#41;](roles-and-permissions-analysis-services.md)   
 [Grant cube or model permissions &#40;Analysis Services&#41;](grant-cube-or-model-permissions-analysis-services.md)   
 [Grant permissions on data mining structures and models &#40;Analysis Services&#41;](grant-permissions-on-data-mining-structures-and-models-analysis-services.md)   
 [Grant custom access to dimension data &#40;Analysis Services&#41;](grant-custom-access-to-dimension-data-analysis-services.md)   
 [Grant custom access to cell data &#40;Analysis Services&#41;](grant-custom-access-to-cell-data-analysis-services.md)  
  
  
