---
title: "Model Properties (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.fileprop.f1"
  - "sql12.asvs.bidtoolset.wspacedbconfig.f1"
ms.assetid: 8ab04656-75a5-485c-9687-7b1ca49f7f80
author: minewiskan
ms.author: owend
manager: craigg
---
# Model Properties (SSAS Tabular)
  This topic describes tabular model properties. Each tabular model project in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] has model properties that affect how the model you are authoring is built, how it is backed up, and how the workspace database is stored. Model properties described here do not apply to models that have already been deployed.  
  
 Sections in this topic:  
  
-   [Model Properties](#bkmk_model_properties)  
  
-   [To configure model property settings](#bkmk_conf)  
  
##  <a name="bkmk_model_properties"></a> Model Properties  
 **Advanced**  
  
|Property|Default setting|Description|  
|--------------|---------------------|-----------------|  
|**Build Action**|Compile|This property specifies how the file relates to the build and deployment process. This property setting has the following options:<br /><br /> **Compile** - A normal build action occurs. Definitions for model objects will be written to the .asdatabase file.<br /><br /> **None** - The output to the .asdatabase file will be empty.|  
|**Copy to Output Directory**|Do Not Copy|This property specifies the source file will be copied to the output directory. This property setting has the following options:<br /><br /> **Do not copy** - No copy is created in the output directory.<br /><br /> **Copy always** - A copy is always created in the output directory.<br /><br /> **Copy if newer** - A copy is created in the output directory only if there are changes to the model.bim file.|  
  
 **Miscellaneous**  
  
> [!NOTE]  
>  Some properties are set automatically when the model is created and cannot be changed.  
  
> [!NOTE]  
>  Workspace Server, Workspace Retention, and Data Backup properties have default settings applied when you create a new model project. You can change the default settings for new models on the Data Modeling page in Analysis Server settings in the Tools\Options dialog box. These properties, as well as others, can also be set for each model in the Properties window. For more information, see [Configure Default Data Modeling and Deployment Properties &#40;SSAS Tabular&#41;](properties-ssas-tabular.md).  
  
|Property|Default setting|Description|  
|--------------|---------------------|-----------------|  
|**Collation**|Default collation for the computer for which Visual Studio is installed.|The collation designator for the model.|  
|**Compatibility Level**|Default or other selected when creating the project.|Applies to SQL Server 2012 Analysis Services SP1 or later. Specifies the features and settings available for this model. For more information, see [Compatibility Level &#40;SSAS Tabular SP1&#41;](compatibility-level-for-tabular-models-in-analysis-services.md).|  
|**Data Backup**|Do not back up to disk|Specifies whether or not a backup of the model data is kept in a backup file. Note that the default setting for this property can be changed on the Data Modeling page in Analysis Server settings in the Tools\Options dialog box. This property setting has the following options:<br /><br /> **Back up to disk** - Specifies to keep a backup of model data on disk. When the model is saved, the data will also be saved to the backup (ABF) file. Selecting this option can cause slower model save and load times<br /><br /> **Do not back up to disk** - Specifies to not keep a backup of model data on disk. This option will minimize save and model loading time.|  
|**DirectQuery Mode**|Off|Specifies whether or not this model operates in DirectQuery Mode. For more information, see [DirectQuery Mode &#40;SSAS Tabular&#41;](directquery-mode-ssas-tabular.md).|  
|**File Name**|Model.bim|Specifies the name of the .bim file. The file name should not be changed.|  
|**Full Path**|Path specified when the project was created.|The model.bim file location. This property cannot be set in the Properties window.|  
|**Language**|English|The default language of the model. The default language is determined by the Visual Studio language. This property cannot be set in the Properties window.|  
|**Workspace Database**|The project name, followed by an underscore, followed by a GUID.|The name of the workspace database used for storing and editing the in-memory model for the selected model.bim file. This database will appear in the Analysis Services instance specified in the Workspace Server property. This property cannot be set in the Properties window. For more information, see [Workspace Database &#40;SSAS Tabular&#41;](workspace-database-ssas-tabular.md).|  
|**Workspace Retention**|Unload from memory|Specifies how a workspace database is retained after a model is closed. A workspace database includes model metadata, the data imported into a model, and impersonation credentials (encrypted). In some cases, the workspace database can be very large and consume a significant amount of memory. By default, workspace databases are unloaded from-memory. When changing this setting, it is important to consider your available memory resources as well as how often you plan to work on the model. Note that the default setting for this property can be changed on the Data Modeling page in Analysis Server settings in the Tools\Options dialog box. This property setting has the following options:<br /><br /> **Keep in memory** - Specifies to keep the workspace database in memory after a model is closed. This option will consume more memory; however, when opening a model in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], fewer resources are consumed and the workspace database will load faster.<br /><br /> **Unload from memory** - Specifies to keep the workspace database on disk, but no longer in memory after a model is closed. This option will consume less memory; however, when opening a model in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], additional resources are consumed and the model will load more slowly than if the workspace database is kept in memory. Use this option when in-memory resources are limited or when working on a remote workspace database.<br /><br /> **Delete workspace** - Specifies to delete the workspace database from memory and not keep the workspace database on disk after the model is closed. This option will consume less memory and storage space; however, when opening a model in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], additional resources are consumed and the model will load more slowly than if the workspace database is kept in memory or on-disk. Use this option when only occasionally working on models.|  
|**Workspace Server**|localhost|This property specifies the default server that will be used to host the workspace database while the model is being authored in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. All available instances of Analysis Services running on the local computer are included in the list box.<br /><br /> Note: It is recommended that you always specify a local Analysis Services server as the workspace server. For workspaces databases on a remote server, importing from PowerPivot is not supported, data cannot be backed up locally, and the user interface may experience latency during queries.<br /><br /> The default setting for this property can be changed on the Data Modeling page in Analysis Server settings in the Tools\Options dialog box.|  
  
##  <a name="bkmk_conf_model_prop"></a>   
###  <a name="bkmk_conf"></a> To configure model property settings  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], in **Solution Explorer**, click the **Model.bim** file.  
  
2.  In the **Properties** window, click a property, and then type a value or click the down arrow to select a setting option.  
  
## See Also  
 [Configure Default Data Modeling and Deployment Properties &#40;SSAS Tabular&#41;](properties-ssas-tabular.md)   
 [Project Properties &#40;SSAS Tabular&#41;](project-properties-ssas-tabular.md)  
  
  
