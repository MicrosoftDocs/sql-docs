---
title: "Specifying a Testing Data Set for the Structure (Basic Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 75cd508f-b126-418b-848d-3c4c3e6c303f
author: minewiskan
ms.author: owend
manager: kfile
---
# Specifying a Testing Data Set for the Structure (Basic Data Mining Tutorial)
  In the final few screens of the Data Mining Wizard you will split your data into a testing set and a training set. You will then name your structure and enable drillthrough on the model.  
  
## Specifying a Testing Set  
 Separating data into training and testing sets when you create a mining structure makes it possible to easily assess the accuracy of the mining models that you create later. For more information on testing sets, see [Training and Testing Data Sets](../../2014/analysis-services/data-mining/training-and-testing-data-sets.md).  
  
#### To specify the testing set  
  
1.  On the **Create Testing Set** page, for **Percentage of data for testing**, leave the default value of `30`.  
  
2.  For **Maximum number of cases in testing data set**, type `1000`.  
  
3.  Click **Next**.  
  
## Specifying Drillthrough  
 Drillthrough can be enabled on models and on structures. The checkbox in this dialog box enables drillthrough on the named model. After the model has been processed,  you will be able to retrieve detailed information from the training data that were used to create the model.  
  
 If the underlying mining structure has also been configured to allow drillthrough, you can retrieve detailed information from both the model cases and the mining structure, including columns that were not included in the mining model. For more information, see [Drillthrough Queries &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/drillthrough-queries-data-mining.md).  
  
#### To name the model and structure and specify drillthrough  
  
1.  On the **Completing the Wizard** page, in **Mining structure name**, type `Targeted Mailing`.  
  
2.  In **Mining model name**, type `TM_Decision_Tree`.  
  
3.  Select the **Allow drill through** check box.  
  
4.  Review the **Preview** pane. Notice that only those columns selected as **Key**, **Input** or **Predictable** are shown. The other columns you selected (e.g., AddressLine1) are not used for building the model but will be available in the underlying structure, and can be queried after the model is processed and deployed.  
  
5.  Click **Finish**.  
  
## Previous Task in Lesson  
 [Specifying the Data Type and Content Type &#40;Basic Data Mining Tutorial&#41;](../../2014/tutorials/specifying-the-data-type-and-content-type-basic-data-mining-tutorial.md)  
  
## Next Lesson  
 [Lesson 3: Adding and Processing Models](../../2014/tutorials/lesson-3-adding-and-processing-models.md)  
  
## See Also  
 [Enable Drillthrough for a Mining Model](../../2014/analysis-services/data-mining/enable-drillthrough-for-a-mining-model.md)   
 [Drillthrough Queries &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/drillthrough-queries-data-mining.md)   
 [Specify the Training Data &#40;Data Mining Wizard&#41;](../../2014/analysis-services/specify-the-training-data-data-mining-wizard.md)  
  
  
