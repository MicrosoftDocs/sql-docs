---
title: "Microsoft Neural Network Algorithm | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "training neural networks"
  - "output neurons [Analysis Services]"
  - "algorithms [data mining]"
  - "neural network algorithms [Analysis Services]"
  - "neurons [Analysis Services]"
  - "classification algorithms [Analysis Services]"
  - "neural networks"
  - "multilayer perceptron network of neurons [Analysis Services]"
  - "hidden neurons"
  - "Back-Propagated Delta Rule network"
  - "input neurons [Analysis Services]"
  - "regression algorithms [Analysis Services]"
ms.assetid: 61eb4861-8a6a-4214-a4b8-1dd278ad7a68
author: minewiskan
ms.author: owend
manager: craigg
---
# Microsoft Neural Network Algorithm
  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm combines each possible state of the input attribute with each possible state of the predictable attribute, and uses the training data to calculate probabilities. You can later use these probabilities for classification or regression, and to predict an outcome of the predicted attribute, based on the input attributes.  
  
 A mining model that is constructed with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm can contain multiple networks, depending on the number of columns that are used for both input and prediction, or that are used only for prediction. The number of networks that a single mining model contains depends on the number of states that are contained by the input columns and predictable columns that the mining model uses.  
  
## Example  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm is useful for analyzing complex input data, such as from a manufacturing or commercial process, or business problems for which a significant quantity of training data is available but for which rules cannot be easily derived by using other algorithms.  
  
 Suggested scenarios for using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm include the following:  
  
-   Marketing and promotion analysis, such as measuring the success of a direct mail promotion or a radio advertising campaign.  
  
-   Predicting stock movement, currency fluctuation, or other highly fluid financial information from historical data.  
  
-   Analyzing manufacturing and industrial processes.  
  
-   Text mining.  
  
-   Any prediction model that analyzes complex relationships between many inputs and relatively fewer outputs.  
  
## How the Algorithm Works  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm creates a network that is composed of up to three layers of neurons. These layers are an input layer, an optional hidden layer, and an output layer.  
  
 **Input layer:** Input neurons define all the input attribute values for the data mining model, and their probabilities.  
  
 **Hidden layer:** Hidden neurons receive inputs from input neurons and provide outputs to output neurons. The hidden layer is where the various probabilities of the inputs are assigned weights. A weight describes the relevance or importance of a particular input to the hidden neuron. The greater the weight that is assigned to an input, the more important the value of that input is. Weights can be negative, which means that the input can inhibit, rather than favor, a specific result.  
  
 **Output layer:** Output neurons represent predictable attribute values for the data mining model.  
  
 For a detailed explanation of how the input, hidden, and output layers are constructed and scored, see [Microsoft Neural Network Algorithm Technical Reference](microsoft-neural-network-algorithm-technical-reference.md).  
  
## Data Required for Neural Network Models  
 A neural network model must contain a key column, one or more input columns, and one or more predictable columns.  
  
 Data mining models that use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm are heavily influenced by the values that you specify for the parameters that are available to the algorithm. The parameters define how data is sampled, how data is distributed or expected to be distributed in each column, and when feature selection is invoked to limit the values that are used in the final model.  
  
 For more information about setting parameters to customize model behavior, see [Microsoft Neural Network Algorithm Technical Reference](microsoft-neural-network-algorithm-technical-reference.md).  
  
## Viewing a Neural Network Model  
 To work with the data and see how the model correlates inputs with outputs, you can use the **Microsoft Neural Network Viewer**. With this custom viewer, you can filter on input attributes and their values, and see graphs that show how they affect the outputs. The tooltips in the viewer show you the probability and lift associated with each pair of input and output values. For more information, see [Browse a Model Using the Microsoft Neural Network Viewer](browse-a-model-using-the-microsoft-neural-network-viewer.md).  
  
 The easiest way to explore the structure of the model is to use the **Microsoft Generic Content Tree Viewer**. You can view the inputs, outputs, and networks created by the model, and click on any node to expand it and see statistics related to the input, output, or hidden layer nodes. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](browse-a-model-using-the-microsoft-generic-content-tree-viewer.md).  
  
## Creating Predictions  
 After the model has been processed, you can use the network and the weights stored within each node to make predictions. A neural network model supports regression, association, and classification analysis, Therefore, the meaning of each prediction might be different. You can also query the model itself, to review the correlations that were found and retrieve related statistics. For examples of how to create queries against a neural network model, see [Neural Network Model Query Examples](neural-network-model-query-examples.md).  
  
 For general information about how to create a query on a data mining model, see [Data Mining Queries](data-mining-queries.md).  
  
## Remarks  
  
-   Does not support drillthrough or data mining dimensions. This is because the structure of the nodes in the mining model does not necessarily correspond directly to the underlying data.  
  
-   Does not support the creation of models in Predictive Model Markup Language (PMML) format.  
  
-   Supports the use of OLAP mining models.  
  
-   Does not support the creation of data mining dimensions.  
  
## See Also  
 [Microsoft Neural Network Algorithm Technical Reference](microsoft-neural-network-algorithm-technical-reference.md)   
 [Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](mining-model-content-for-neural-network-models-analysis-services-data-mining.md)   
 [Neural Network Model Query Examples](neural-network-model-query-examples.md)   
 [Microsoft Logistic Regression Algorithm](microsoft-logistic-regression-algorithm.md)  
  
  
