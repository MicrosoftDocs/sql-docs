---
title: "Data Mining Tools | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "tools [Analysis Services]"
  - "mining models [Analysis Services], tools"
  - "data mining [Analysis Services], tools"
  - "data mining [Analysis Services], development"
ms.assetid: 003ada6a-0bcd-4f16-8c34-1a9ffc75cd2c
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Mining Tools
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] provides the following tools that you can use to create data mining solutions:  
  
-   The **Data Mining Wizard** in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] makes it easy to create mining structures and mining models, using either relational data sources or multidimensional data in cubes.  
  
     In the wizard, you choose data to use, and then apply specific data mining techniques, such as clustering, neural networks, or time series modeling.  
  
-   **Model viewers** are provided in both [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], for exploring your mining models after they are created.  You can browse models using viewers tailored to each algorithm, or go deeper into analysis by using the model content viewer.  
  
-   The **Prediction Query Builder** is provided in both [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to help you create prediction queries. You can also test the accuracy of models against a holdout data set or external data, or use cross-validation to assess the quality of your data set.  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is the interface where you manage existing data mining solutions that have been deployed to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. You can reprocess structures and models to update the data in them.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] contains tools that you can use to clean data, to automate tasks such as creating predictions and updating models, and to create text mining solutions.  
  
 The following sections provide more information about the data mining tools in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Data Mining Wizard  
 Use the Data Mining Wizard to get started creating data mining solutions. The wizard is quick and easy, and guides you through the process of creating a data mining structure and an initial related mining model, and includes the tasks of selecting an algorithm type and a data source, and defining the case data used for analysis.  
  
 **For More Information:** [Data Mining Wizard &#40;Analysis Services - Data Mining&#41;](data-mining-wizard-analysis-services-data-mining.md)  
  
## Data Mining Designer  
 After you have created a mining structure and mining model by using the Data Mining Wizard, you can use the Data Mining Designer from either [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to work with existing models and structures.  
  
 The designer includes tools for these tasks:  
  
-   Modify the properties of mining structures, add columns and create column aliases, change the binning method or expected distribution of values.  
  
-   Add new models to an existing structure; copy models, change model properties or metadata, or define filters on a mining model.  
  
-   Browse the patterns and rules within the model; explore associations or decision trees. Get detailed statistics about  
  
     Custom viewers are provided for each different time of model, to help you analyze your data and explore the patterns revealed by data mining.  
  
-   Validate models by creating lift charts, or analyzing the profit curve for models. Compare models using classification matrices, or validate a data set and its models by using cross-validation.  
  
-   Create predictions and content queries against existing mining models. Build one-off queries, or set up queries to generate predictions for entire tables of external data.  
  
 **For More Information:** [Data Mining Designer](data-mining-designer.md)  
  
## SQL Server Management Studio  
 After you create and deploy mining models to a server, you can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to manage the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database that hosts the data mining objects. You can also continue to perform tasks that use the model, such as exploring the models, processing new data, and creating predictions.  
  
 [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] also contains query editors that you can use to design and execute Data Mining Extensions (DMX) queries, or ot work with data mining objects by using XMLA.  
  
## Integration Services Data Mining Tasks and Transformations  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides many components that support data mining.  
  
 Some tools in [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] are designed to help automate common data mining tasks, including prediction, model building, and processing. For example:  
  
-   Create an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that automatically updates the model every time the dataset is updated with new customers  
  
-   Perform custom segmentation or custom sampling of case records.  
  
-   Automatically generate models passed on parameters.  
  
 However, you can also use data mining in a package workflow, as an input to other processes. For example:  
  
-   Use probability values generated by the model to weight scores for text mining or other classification tasks.  
  
-   Automatically generate predictions based on prior data and use those values to assess the validity of new data.  
  
-   Using logistic regression to segment incoming customers by risk.  
  
 **For More Information:** [Related Projects for Data Mining Solutions](data-mining-solutions.md)  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Reference](/sql/dmx/data-mining-extensions-dmx-reference)   
 [Mining Model Tasks and How-tos](mining-model-tasks-and-how-tos.md)   
 [Mining Model Viewer Tasks and How-tos](mining-model-viewer-tasks-and-how-tos.md)   
 [Data Mining Solutions](data-mining-solutions.md)  
  
  
