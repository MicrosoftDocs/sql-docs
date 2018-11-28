---
title: "Import a Data Mining Project using the Analysis Services Import Wizard | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Import a Data Mining Project using the Analysis Services Import Wizard
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This topic describes how to create a new data mining project by importing the metadata from an existing data mining project on another server, using the template, **Import from Server (Multidimensional and Data Mining) Project**, in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
## Import data sources, mining structures, and mining models from an existing data mining project  
 When you use the template, **Import from Server (Multidimensional and Data Mining) Project**, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] creates a new data mining project, and then copies the metadata from the specified data mining project. The new project contains the same data sources, data source views, mining structures, and mining models as the ssASnoversion database that you imported from. However, the project cannot be used until you have updated certain properties and processed the objects as described:  
  
-   The data itself is not copied from the source server to the new data mining project-only the definitions of the data sources and data source views are imported. Therefore, after the import process has completed, and the objects have been created, you must populate the objects with data by training the mining structures and dependent models. You can use the command **Process All** in Data Mining Designer to train the models and structures.  
  
-   If you are importing a project that was created in a previous version of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the data source might use providers that are not installed on the server to which you are importing the project. If you encounter errors when processing the imported mining structures, right-click each data source and select **Open Designer** to edit the connection string and review the provider properties.  
  
     At this time, you might also need to verify that the account you are using to process the data mining objects or query data mining models has the necessary permissions on the data source.  
  
-   By default, when you import a project, the workspace database is set to localhost, or whatever default instance is configured as the **Default Target Server** in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. To set this property, from the **Options** menu, select **Business Intelligence Designers**, select **Analysis Services**, and then select **General**.  
  
     Note that, in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], there is another, separate option that you can set to configure the default deployment server for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] tabular model projects. The setting, **Default Deployment Server**, determines the default workspace database for tabular model projects. You cannot use instances that support tabular models for data mining projects  
  
     If you cannot change the default deployment database to use an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] running in multidimensional or data mining mode, you can always specify the deployment database by using the **Project Properties** dialog box.  
  
#### To create a new data mining project by importing an existing data mining project  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], on the **File** menu, click **New**, and then click **Project**.  
  
2.  In the **New Project** dialog box, under **Installed Templates**, click **Business Intelligence**, click **Analysis Services**, and then click **Import from Server (Multidimensional/Data Mining)**.  
  
3.  For **Name**, type a name for the project, then specify a location and solution name, and then click **OK**.  
  
     The **Import Analysis Services Database wizard** starts. Click OK on the Welcome page to proceed.  
  
4.  On the page, **Select Source Database**, for **Server**, specify the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance that contains the solution you want to import.  
  
     For **Database**, choose the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database that contains the data mining objects you want to import.  
  
    > [!WARNING]  
    >  You cannot specify the objects you want to import; when you choose an existing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, all multidimensional and data mining objects are imported.  
  
     Click **Next**.  
  
5.  The page, **Completing the Wizard**, displays the progress of the import operation. You cannot cancel the operation or change the objects that are being imported. Click **Finish** when done.  
  
     The new project is automatically opened using [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
## See Also  
 [Project Properties](../../analysis-services/tabular-models/project-properties-ssas-tabular.md)  
  
  
