---
title: "Configure Analysis Services default data modeling and deployment properties | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Configure default data modeling and deployment properties 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  This article describes how to configure the default compatibility level, deployment and workspace database property settings, which can be pre-defined for each new tabular model project you create in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. After a new project is created, these properties can still be changed depending on your particular requirements.  
  
#### To configure the default Compatibility Level property setting for new model projects  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click the **Tools** menu, and then click **Options**.  
  
2.  In the **Options** dialog box, expand **Analysis Services Tabular Designers**, and then click **Compatibility Level**.  
  
3.  Configure the following property settings:  
  
    |Property|Default setting|Description|  
    |--------------|---------------------|-----------------|  
    |**Default compatibility level for new projects**|SQL Server 2016 (1200)|This setting specifies the default compatibility level to use when creating a new Tabular model project. You can choose SQL Server 2012 (1100) if you will deploy to an Analysis Services instance without SP1 applied, or SQL Server 2012 SP1 or later if your deployment instance has SP1 applied. For more information, see [Compatibility Level for Tabular models in Analysis Services](../../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md).|  
    |**Compatibility level options**|All checked|Specifies compatibility level options for new Tabular model projects and when deploying to another Analysis Services instance.|  
  
#### To configure the default deployment server property setting for new model projects  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click the **Tools** menu, and then click **Options**.  
  
2.  In the **Options** dialog box, expand **Analysis Services Tabular Designers**, and then click **Deployment**.  
  
3.  Configure the following property settings:  
  
    |Property|Default setting|Description|  
    |--------------|---------------------|-----------------|  
    |**Default deployment server**|localhost|This setting specifies the default server to use when deploying a model. You can click the down arrow to browse for local network Analysis Services servers you can use or you can type the name of a remote server.|  
  
    > [!NOTE]  
    >  Changes to the Default deployment Server property setting will not affect existing projects created prior to the change.  
  
###  <a name="bkmk_conf_default"></a> To configure the default Workspace Database property settings for new model projects  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click the **Tools** menu, and then click **Options**.  
  
2.  In the **Options** dialog box, expand **Analysis Services Tabular Designers**, and then click **Workspace Database**.  
  
3.  Configure the following property settings:  
  
    |Property|Default setting|Description|  
    |--------------|---------------------|-----------------|  
    |**Default workspace server**|**localhost**|This property specifies the default server that will be used to host the workspace database while the model is being authored in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. All available instances of Analysis Services running on the local computer are included in the listbox.<br /><br /> <br /><br /> Note: It is recommended that you always specify a local Analysis Services server as the workspace server. For workspace databases on a remote server, importing data from [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] is not supported, data cannot be backed up locally, and the user interface may experience latency during queries.|  
    |**Workspace database retention after the model is closed**|**Keep workspace databases on disk, but unload from memory**|Specifies how a workspace database is retained after a model is closed. A workspace database includes model metadata, the data imported into a model, and impersonation credentials (encrypted). In some cases, the workspace database can be very large and consume a significant amount of memory. By default, workspace databases are removed from memory. When changing this setting, it is important to consider your available memory resources as well as how often you plan to work on the model. This property setting has the following options:<br /><br /> **Keep workspaces in memory** - Specifies to keep workspaces in memory after a model is closed. This option will consume more memory; however, when opening a model in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], fewer resources are consumed and the workspace will load faster.<br /><br /> **Keep workspace databases on disk, but unload from memory** - Specifies to keep the workspace database on disk, but no longer in memory after a model is closed. This option will consume less memory; however, when opening a model in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], additional resources are consumed and the model will load more slowly than if the workspace database is kept in memory. Use this option when in-memory resources are limited or when working on a remote workspace database.<br /><br /> **Delete workspace** - Specifies to delete workspace database from memory and not keep workspace database on disk after the model is closed. This option will consume less memory and storage space; however, when opening a model in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], additional resources are consumed and the model will load more slowly than if the workspace database is kept in memory or on-disk. Use this option when only occasionally working on models.|  
    |**Data backup**|**Keep backup of data on disk**|Specifies whether or not a backup of the model data is kept in a backup file. This property setting has the following options:<br /><br /> **Keep backup of data on disk** - Specifies to keep a backup of model data on disk. When the model is saved, the data will also be saved to the backup (ABF) file. Selecting this option can cause slower model save and load times<br /><br /> **Do not keep backup of databack on disk** - Specifies to not keep a backup of model data on disk. This option will minimize save and model loading time.|  
  
> [!NOTE]  
>  Changes to default model properties will not affect properties for existing models created prior to the change.  
  
## See also  
 [Project properties](../../analysis-services/tabular-models/project-properties-ssas-tabular.md)   
 [Model properties](../../analysis-services/tabular-models/model-properties-ssas-tabular.md)   
 [Compatibility level](../../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md)  
  
  
