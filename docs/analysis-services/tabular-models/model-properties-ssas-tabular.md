---
title: "Analysis Services tabular model properties | Microsoft Docs"
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
# Model properties 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  This article describes tabular model properties. Each tabular model project has model properties that affect how the model you are authoring in SQL Server Development Tools is built, how it is backed up, and how the workspace database is stored. Model properties described here do not apply to models that have already been deployed.  
  
 Sections in this topic:  
  
-   [Model properties](#bkmk_model_properties)  
  
-   [Configure model property settings](#bkmk_conf_model_prop)  
  
##  <a name="bkmk_model_properties"></a> Model properties  
 **Advanced**  
  
|Property|Default setting|Description|  
|--------------|---------------------|-----------------|  
|**Build Action**|Compile|This property specifies how the file relates to the build and deployment process. This property setting has the following options:<br /><br /> **Compile** - A normal build action occurs. Definitions for model objects will be written to the .asdatabase file.<br /><br /> **None** - The output to the .asdatabase file will be empty.|  
|**Copy to Output Directory**|Do Not Copy|This property specifies the source file will be copied to the output directory. This property setting has the following options:<br /><br /> **Do not copy** - No copy is created in the output directory.<br /><br /> **Copy always** - A copy is always created in the output directory.<br /><br /> **Copy if newer** - A copy is created in the output directory only if there are changes to the model.bim file.|  
  
 **Miscellaneous**  
  
> [!NOTE]  
>  Some properties are set automatically when the model is created and cannot be changed.  
  
> [!NOTE]  
>  Workspace Server, Workspace Retention, and Data Backup properties have default settings applied when you create a new model project. You can change the default settings for new models on the Data Modeling page in Analysis Server settings in the Tools\Options dialog box. These properties, as well as others, can also be set for each model in the Properties window. For more information, see [Configure default data modeling and deployment properties](../../analysis-services/tabular-models/configure-default-data-modeling-and-deployment-properties-ssas-tabular.md).  
  
|Property|Default setting|Description|  
|--------------|---------------------|-----------------|  
|**Collation**|Default collation for the computer for which Visual Studio is installed.|The collation designator for the model.|  
|**Compatibility Level**|Default or other selected when creating the project.|Applies to SQL Server 2012 Analysis Services SP1 or later. Specifies the features and settings available for this model. For more information, see [Compatibility Level for Tabular models in Analysis Services](../../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md).|  
|**Data Backup**|Do not back up to disk|Specifies whether or not a backup of the model data is kept in a backup file. This property setting has the following options:<br /><br /> **Back up to disk** - Specifies to keep a backup of model data on disk. When the model is saved, the data will also be saved to the backup (ABF) file. Selecting this option can cause slower model save and load times<br /><br /> **Do not back up to disk** - Specifies to not keep a backup of model data on disk. This option will minimize save and model loading time.<br /><br /> <br /><br /> The default setting for this property can be changed on the Data Modeling page in Analysis Server settings in the Tools\Options dialog box.| 
|**Default filter direction**|Single direction|Determines the default filter direction for new relationships.| 
|**DirectQuery Mode**|Off|Specifies whether or not this model operates in DirectQuery Mode. For more information, see [DirectQuery Mode](../../analysis-services/tabular-models/directquery-mode-ssas-tabular.md).|  
|**File Name**|Model.bim|Specifies the name of the .bim file. The file name should not be changed.|  
|**Full Path**|Path specified when the project was created.|The model.bim file location. This property cannot be set in the Properties window.|  
|**Language**|English|The default language of the model. The default language is determined by the Visual Studio language. This property cannot be set in the Properties window.|  
|**Workspace Database**|The project name, followed by an underscore, followed by a GUID.|The name of the workspace database used for storing and editing the in-memory model for the selected model.bim file. This database will appear in the Analysis Services instance specified in the Workspace Server property. This property cannot be set in the Properties window. For more information, see [Workspace Database](../../analysis-services/tabular-models/workspace-database-ssas-tabular.md).|  
|**Workspace Retention**|Unload from memory|Specifies how a workspace database is retained after a model is closed. A workspace database includes model metadata, the data imported into a model, and impersonation credentials (encrypted). In some cases, the workspace database can be very large and consume a significant amount of memory. By default, workspace databases are unloaded from-memory. When changing this setting, it is important to consider your available memory resources as well as how often you plan to work on the model. This property setting has the following options:<br /><br /> **Keep in memory** - Specifies to keep the workspace database in memory after a model is closed. This option will consume more memory; however, when opening a model, fewer resources are consumed and the workspace database will load faster.<br /><br /> **Unload from memory** - Specifies to keep the workspace database on disk, but no longer in memory after a model is closed. This option will consume less memory; however, when opening a model, additional resources are consumed and the model will load more slowly than if the workspace database is kept in memory. Use this option when in-memory resources are limited or when working on a remote workspace database.<br /><br /> **Delete workspace** - Specifies to delete the workspace database from memory and not keep the workspace database on disk after the model is closed. This option will consume less memory and storage space; however, when opening a model, additional resources are consumed and the model will load more slowly than if the workspace database is kept in memory or on-disk. Use this option when only occasionally working on models.<br /><br /> <br /><br /> The default setting for this property can be changed on the Data Modeling page in Analysis Server settings in the Tools\Options dialog box.|  
|**Workspace Server**|localhost|This property specifies the default server that will be used to host the workspace database while the model is being authored. All available instances of Analysis Services running on the local computer are included in the listbox.<br /><br /> The default setting for this property can be changed on the Data Modeling page in Analysis Server settings in the Tools\Options dialog box.<br /><br /> <br /><br /> Note: It is recommended that you always specify a local Analysis Services server as the workspace server. For workspaces databases on a remote server, importing from [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] is not supported, data cannot be backed up locally, and the user interface may experience latency during queries.|  
  
##  <a name="bkmk_conf_model_prop"></a> Configure model property settings  
  
1.  In SSDT, in **Solution Explorer**, click the **Model.bim** file.  
  
2.  In the **Properties** window, click a property, and then type a value or click the down arrow to select a setting option.  
  
## See Also  
 [Configure default data modeling and deployment properties](../../analysis-services/tabular-models/configure-default-data-modeling-and-deployment-properties-ssas-tabular.md)   
 [Project properties](../../analysis-services/tabular-models/project-properties-ssas-tabular.md)  
  
  
