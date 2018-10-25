---
title: "Define Linked Dimensions | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "dimensions [Analysis Services], linked"
  - "linked dimensions [Analysis Services]"
ms.assetid: d5ad5eae-5dde-46a6-91c3-c8766d016dec
author: minewiskan
ms.author: owend
manager: craigg
---
# Define Linked Dimensions
  A linked dimension is based on a dimension created and stored in another Analysis Services database of the same version and compatibility level. By using a linked dimension, you can create, store, and maintain a dimension on one database, while making it available to users of multiple databases. To users, a linked dimension appears like any other dimension.  
  
 Linked dimensions are read-only. If you want to modify the dimension or create new relationships, you must change the source dimension, then delete and recreate the linked dimension and its relationships. You cannot refresh a linked dimension to pick up changes from the source object.  
  
 All related measure groups and dimensions must come from the same source database. You cannot create new relationships between local measure groups and the linked dimensions you add to your cube. After linked dimensions and measure groups have been added to the current cube, the relationships between them must be maintained in their source database.  
  
> [!NOTE]  
>  Because refresh is not available, most Analysis Services developers copy dimensions rather than link them. You can copy dimensions across projects within the same solution. For more information, see [Refresh of a linked dimension in SSAS](http://sqlblog.com/blogs/marco_russo/archive/2006/09/12/refresh-of-a-linked-dimension-in-ssas.aspx).  
  
## Prerequisites  
 The source database that provides the dimension and the current database that uses it must be at the same version and compatibility level. For more information, see [Set the Compatibility Level of a Multidimensional Database &#40;Analysis Services&#41;](compatibility-level-of-a-multidimensional-database-analysis-services.md).  
  
 The source database must be deployed and online. Servers that publish or consume linked objects must be configured to allow the operation (see below).  
  
 The dimension you want to use cannot itself be a linked dimension.  
  
## Configure server to allow linked objects  
  
1.  In SQL Server Management Studio, connect to an Analysis Services server. In Object Explorer, right-click the server name and select **Facets**.  
  
2.  Set **LinkedObjectsLinksFromOtherInstancesEnabled** to **True** to allow the server to issue requests for linked objects that reside in databases running on other instances.  
  
3.  Set **LinkedObjectsLinksToOtherInstances** to **True** to allow the server to request data for linked on databases running on other instances.  
  
## Create a linked dimension in SQL Server Data Tools  
  
1.  Start the wizard. In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], right-click the **Dimensions** folder in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database or project, and then click **New Linked Dimension**.  
  
2.  Connect to the Analysis Services database that provides the dimension. On the **Select a Data Source** page of the Linked Object Wizard, choose the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source or create a new one.  
  
3.  On the **Select Objects** page of the wizard, choose the dimensions you want to link to in the remote database.  
  
4.  On the **Completing the Wizard** page, you can preview the linked objects. If you link a dimension that has the same name as one that already exists, an ordinal number (starting with '1' for the first duplicated name) is appended to the name. When you complete the wizard, the dimension is added to the **Dimensions** folder.  
  
##  <a name="bkmk_CreateNew"></a> Create a New Data Source Connection to an Analysis Services Database  
 Use the New Data Source wizard to add to your project connection information about the Analysis Services database that provides the dimension. You can start the wizard by clicking **New Data Source** in the Select a Data Source page of the Linked Objects wizard.  
  
1.  In the Data Source Wizard, on the Select how to define the connection page, click **New**.  
  
2.  In Connection Manager, verify that the provider is set to **Native OLE DB\Microsoft OLE DB Provider for Analysis Services 11.0**.  
  
3.  Enter the name of the server (use *servername*\\*instancename* for a named instance)<sup>1</sup> or type **localhost** to connect to an Analysis Services server that is running on the same computer.  
  
4.  Use Windows authentication for the connection.  
  
5.  In **Initial catalog**, click the down arrow to select a database on this server.  
  
6.  On the Data Source Wizard, click **Next** to continue.  
  
7.  On the Impersonation Information page, click **Use the service account**. Click **Next**, and then finish the wizard. The connection you just defined will be selected in the Linked Objects Wizard.  
  
## Next Steps  
 You cannot change the structure of a linked dimension, so you cannot view it with the **Dimension Structure** tab of Dimension Designer. After processing the linked dimension, you can view it with the **Browser** tab. You can also change its name and create a translation for the name.  
  
## See Also  
 [Set the Compatibility Level of a Multidimensional Database &#40;Analysis Services&#41;](compatibility-level-of-a-multidimensional-database-analysis-services.md)   
 [Linked Measure Groups](linked-measure-groups.md)   
 [Dimension Relationships](../multidimensional-models-olap-logical-cube-objects/dimension-relationships.md)  
  
  
