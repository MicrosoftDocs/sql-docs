---
title: "Process a Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining structures [Analysis Services], processing"
ms.assetid: 4162f33e-c23f-4293-8905-271781e45fa4
author: minewiskan
ms.author: owend
manager: craigg
---
# Process a Mining Structure
  Before you can browse or work with the mining models that are associated with a mining structure, you have to deploy the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project and process the mining structure and mining models. Also, if you make a change to the mining structure or mining models, you will be prompted to redeploy and process them. Processing the structure in the **Mining Structure** tab of Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] processes all the associated models.  
  
 You can process a mining structure by using these tools:  
  
-   [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]  
  
-   XMLA: Process command  
  
 For information about how to process individual models, see [Process a Mining Model](process-a-mining-model.md).  
  
### To process a mining structure and all associated mining models using SQL Server Data Tools  
  
1.  Select **Process Mining Structure and All Models** from the **Mining Model** menu item in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
     If you made changes to the structure, you will be prompted to redeploy the structure before processing the models. Click **Yes**.  
  
2.  Click **Run** in the **Processing Mining Structure - \<structure>** dialog box.  
  
     The **Process Progress** dialog box opens to display the details of model processing.  
  
3.  Click **Close** in the **Process Progress** dialog box after the models have completed processing.  
  
4.  Click **Close** in the **Processing Mining Structure - \<structure>** dialog box.  
  
## See Also  
 [Mining Structure Tasks and How-tos](mining-structure-tasks-and-how-tos.md)  
  
  
