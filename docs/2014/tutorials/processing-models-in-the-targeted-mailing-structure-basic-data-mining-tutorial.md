---
title: "Processing Models in the Targeted Mailing Structure (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 9d8233bb-117e-4563-9302-8a5a8ad1fae2
author: minewiskan
ms.author: owend
manager: kfile
---
# Processing Models in the Targeted Mailing Structure (Basic Data Mining Tutorial)
  Before you can browse or work with the mining models that you have created, you must deploy the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] project and process the mining structure and mining models.  
  
-   *Deploying* sends the project to a server and creates any objects in that project on the server.  
  
-   *Processing* populates [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] objects with data from relational data sources.  
  
 Models cannot be used until they have been deployed and processed. Also, when you make any changes to the model, such as adding new data, you must redeploy and reprocess the models.  
  
## Ensuring Consistency with HoldoutSeed  
 When you deploy a project and process the structure and models, individual rows in your data structure are assigned to either the training set or testing set based on a numerical seed value. By default, the numerical seed value is computed based on attributes of the data structure. However, if you ever change some aspects of your model, the seed value would change, leading to subtly different results. Therefore, in order to ensure that your results are the same as described here, we will arbitrarily assign a fixed *holdout seed* of `12`. The holdout seed is used to initialize the sampling algorithm, and ensures that the data is partitioned in roughly the same way for all mining structures and their models.  
  
 This value does not affect the number of cases in the training set; it simply ensures that the same partitioning method will be used each time you build the model.  
  
 For more information on holdout seed, see [Training and Testing Data Sets](../../2014/analysis-services/data-mining/training-and-testing-data-sets.md).  
  
#### To set the Holdout Seed  
  
1.  Click on the **Mining Structure** tab or the **Mining Models** tab in Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
     **Targeted Mailing MiningStructure** displays in the **Properties** pane.  
  
2.  Ensure that the **Properties** pane is open by pressing **F4**.  
  
3.  Ensure that **CacheMode** is set to **KeepTrainingCases**.  
  
4.  Enter `12` for **HoldoutSeed**.  
  
## Deploying and Processing the Models  
 In Data Mining Designer, you can decide which objects to process, depending on the scope of changes you've made to your model or the underlying data:  
  
 For this task, because the data and the models are new, you will process the structure and all the models at the same time.  
  
#### To deploy the project and process all the mining models  
  
1.  In the **Mining Model** menu, select **Process Mining Structure and All Models**.  
  
     If you made changes to the structure, you will be prompted to build and deploy the project before processing the models. Click **Yes**.  
  
2.  Click **Run** in the **Processing Mining Structure - Targeted Mailing** dialog box.  
  
     The **Process Progress** dialog box opens to display the details of model processing. Model processing might take some time, depending on your computer.  
  
3.  Click **Close** in the **Process Progress** dialog box after the models have completed processing.  
  
4.  Click **Close** in the **Processing Mining Structure - \<structure>** dialog box.  
  
## Previous Task in Lesson  
 [Adding New Models to the Targeted Mailing Structure &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/adding-new-models-to-the-targeted-mailing-structure-basic-data-mining-tutorial.md)  
  
## Next Lesson  
 [Lesson 4: Exploring the Targeted Mailing Models &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/lesson-4-exploring-the-targeted-mailing-models-basic-data-mining-tutorial.md)  
  
## See Also  
 [Processing Requirements and Considerations &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md)  
  
  
