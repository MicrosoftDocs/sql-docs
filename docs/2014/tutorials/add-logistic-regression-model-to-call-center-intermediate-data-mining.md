---
title: "Adding a Logistic Regression Model to the Call Center Structure (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 97abb77a-346c-44fa-8959-688dee1af6a8
author: minewiskan
ms.author: owend
manager: craigg
---
# Adding a Logistic Regression Model to the Call Center Structure (Intermediate Data Mining Tutorial)
  In addition to analyzing the factors that might affect call center operations, you were also asked to provide some specific recommendations on how the staff can improve service quality. In this task, you will use the same mining structure that you used to build the exploratory model and add a mining model that will be used for creating predictions.  
  
 In [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], a logistic regression model is based on the neural networks algorithm, and therefore provides the same flexibility and power as a neural network model. However, logistic regression is particularly well-suited for predicting binary outcomes.  
  
 For this scenario, you will use the same mining structure that you used for the neural network model. However, you will customize the new model to target your business questions. You are interested in improving service quality and determining how many experienced operators you need, so you will set up your model to predict those values.  
  
 To ensure that all the models based on the call center data are as similar as possible, you will use the same seed value as before. Setting the seed parameter ensures that the model processes the data from the same starting point, and minimizes variations caused by artifacts in the data.  
  
### To add a new mining model to the call center mining structure  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], in Solution Explorer, right-click the mining structure, **Call Center Binned**, and select **Open Designer**.  
  
2.  In Data Mining Designer, click the **Mining Models** tab.  
  
3.  Click **Create a related mining model**.  
  
4.  In the **New Mining Model** dialog box, for **Model name**, type `Call Center - LR`.  For **Algorithm name**, select **Microsoft Logistic Regression**.  
  
5.  Click **OK**.  
  
     The new mining model is displayed in the **Mining Models** tab.  
  
### To customize the logistic regression model  
  
1.  In the column for the new mining model, `Call Center - LR`, leave Fact CallCenter ID as the key.  
  
2.  Change the value of ServiceGrade and Level Two Operators to **Predict**.  
  
     These columns will be used both as input and for prediction. In essence, you are creating two separate models on the same data: one that predicts the number of operators, and one that predicts the service grade.  
  
3.  Change all other columns to **Input**.  
  
### To specify the seed and process the models  
  
1.  In the **Mining Model** tab, right-click the column for the model named Call Center - LR, and select **Set Algorithm Parameters**.  
  
2.  In the row for the HOLDOUT_SEED parameter, click the empty cell under **Value**, and type `1`. Click **OK**.  
  
    > [!NOTE]  
    >  The value that you choose as the seed does not matter, as long as you use the same seed for all related models.  
  
3.  In the **Mining Models** menu, select **Process Mining Structure and All Models**. Click **Yes** to deploy the updated data mining project to the server.  
  
4.  In the **Process Mining Model** dialog box, click **Run**.  
  
5.  Click **Close** to close the **Process Progress** dialog box, and then click **Close** again in the **Process Mining Model** dialog box.  
  
## Next Task in Lesson  
 [Creating Predictions for the Call Center Models &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/create-predictions-call-center-models-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Processing Requirements and Considerations &#40;Data Mining&#41;](../../2014/analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md)  
  
  
