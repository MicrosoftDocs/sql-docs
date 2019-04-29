---
title: "Creating a Related Sequence Clustering Model (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 1fb4f5bc-1756-45ca-9cd7-411a8c5992a9
author: minewiskan
ms.author: owend
manager: kfile
---
# Creating a Related Sequence Clustering Model (Intermediate Data Mining Tutorial)
  Through your exploration of the sequence clustering model, you learned that other attributes such as Region or Income have a strong effect on the models; therefore, to understand the sequences better, you will create a related sequence clustering model and remove the attributes related to customer demographics.  
  
 In this task, you will create a copy of the regional sequence clustering model, and then remove from the model any columns that are not directly related to the sequences.  
  
 The new model will contain all the same columns as the mining model on which it is based. However, you do not need to remove the columns from the mining structure, only specify that the new mining model ignore the columns.  
  
### To make a copy of the sequence clustering model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], in the Data Mining Designer, click the **Mining Models** tab.  
  
2.  Right-click the model you want to copy, and select **New Mining Model**.  
  
3.  In the **New Mining Model** dialog box, type a model name, and select Microsoft `Sequence Clustering`.  
  
     For this tutorial, type the name `Sequence Clustering`.  
  
4.  Click **OK**.  
  
### To remove columns from the mining model  
  
1.  In the **Mining Model** tab, in the column for the new model named Sequence Clustering, click the row for the **Income Group** attribute, and select **Ignore**.  
  
2.  Repeat this step for the attribute **Region**.  
  
3.  Click the plus sign next to the table name, **v Assoc Seq Line Items**, to expand the table and view the columns from the nested table.  
  
     The new model should have only the following columns:  
  
     **Order NumberKey**  
  
     **Line Number Key**  
  
     **Model Predict**  
  
### To process the new sequence clustering model  
  
1.  In the **Mining Model** tab, right-click the new model named `Sequence Clustering`, and select **Process Model**.  
  
     Because the new simplified mining model is based on a structure that has already been processed, you do not need to reprocess the structure. You can process just the new mining model.  
  
2.  Click **Yes** to deploy the updated data mining project to the server.  
  
3.  In the **Process Mining Model** dialog box, click **Run**.  
  
4.  Click **Close** to close the **Process Progress** dialog box, and then click **Close** again in the **Process Mining Model** dialog box.  
  
## Next Task in Lesson  
 [Creating Predictions on a Sequence Clustering Model &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/create-predictions-on-model-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Processing Requirements and Considerations &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md)  
  
  
