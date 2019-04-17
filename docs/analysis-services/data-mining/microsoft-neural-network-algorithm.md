---
title: "Microsoft Neural Network Algorithm | Microsoft Docs"
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
# Microsoft Neural Network Algorithm
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm is an implementation of the popular and adaptable neural network architecture for machine learning.  The algorithm works by testing each possible state of the input attribute against each possible state of the predictable attribute, and calculating probabilities for each combination based on the training data. You can use these probabilities for both classification or regression tasks, to predict an outcome  based on some input attributes. A neural network can also be used for association analysis.  
  
 When you create a mining model using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm, you can include multiple outputs, and the algorithm will create multiple networks. The number of networks contained in a single mining model contains depends on the number of states (or attribute values) in the input columns, as well as the number of predictable columns that the mining model uses and the number of states in those columns.  
  
## Example  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm is useful for analyzing complex input data, such as from a manufacturing or commercial process, or business problems for which a significant quantity of training data is available but for which rules cannot be easily derived by using other algorithms.  
  
 Suggested scenarios for using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm include the following:  
  
-   Marketing and promotion analysis, such as measuring the success of a direct mail promotion or a radio advertising campaign  
  
-   Predicting stock movement, currency fluctuation, or other highly fluid financial information from historical data  
  
-   Analyzing manufacturing and industrial processes  
  
-   Text mining  
  
-   Any prediction model that analyzes complex relationships between many inputs and relatively fewer outputs  
  
## How the Algorithm Works  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm creates a network that is composed of up to three layers of nodes (sometimes called *neurons*). These layers are the *input layer*, the *hidden layer*, and the *output layer*.  
  
 **Input layer:** Input nodes define all the input attribute values for the data mining model, and their probabilities.  
  
 **Hidden layer:** Hidden nodes receive inputs from input nodes and provide outputs to output nodes. The hidden layer is where the various probabilities of the inputs are assigned weights. A weight describes the relevance or importance of a particular input to the hidden node. The greater the weight that is assigned to an input, the more important the value of that input is. Weights can be negative, which means that the input can inhibit, rather than favor, a specific result.  
  
 **Output layer:** Output nodes represent predictable attribute values for the data mining model.  
  
 For a detailed explanation of how the input, hidden, and output layers are constructed and scored, see [Microsoft Neural Network Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-neural-network-algorithm-technical-reference.md).  
  
## Data Required for Neural Network Models  
 A neural network model must contain a key column, one or more input columns, and one or more predictable columns.  
  
 Data mining models that use the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Neural Network algorithm are heavily influenced by the values that you specify for the parameters that are available to the algorithm. The parameters define how data is sampled, how data is distributed or expected to be distributed in each column, and when feature selection is invoked to limit the values that are used in the final model.  
  
 For more information about setting parameters to customize model behavior, see [Microsoft Neural Network Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-neural-network-algorithm-technical-reference.md).  
  
## Viewing a Neural Network Model  
 To work with the data and see how the model correlates inputs with outputs, you can use the **Microsoft Neural Network Viewer**. With this custom viewer, you can filter on input attributes and their values, and see graphs that show how they affect the outputs. Tooltips in the viewer show the probability and lift associated with each pair of input and output values. For more information, see [Browse a Model Using the Microsoft Neural Network Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-neural-network-viewer.md).  
  
 The easiest way to explore the structure of the model is to use the **Microsoft Generic Content Tree Viewer**. You can view the inputs, outputs, and networks created by the model, and click on any node to expand it and see statistics related to the input, output, or hidden layer nodes. For more information, see [Browse a Model Using the Microsoft Generic Content Tree Viewer](../../analysis-services/data-mining/browse-a-model-using-the-microsoft-generic-content-tree-viewer.md).  
  
## Creating Predictions  
 After the model has been processed, you can use the network and the weights stored within each node to make predictions. A neural network model supports regression, association, and classification analysis, Therefore, the meaning of each prediction might be different. You can also query the model itself, to review the correlations that were found and retrieve related statistics. For examples of how to create queries against a neural network model, see [Neural Network Model Query Examples](../../analysis-services/data-mining/neural-network-model-query-examples.md).  
  
 For general information about how to create a query on a data mining model, see [Data Mining Queries](../../analysis-services/data-mining/data-mining-queries.md).  
  
## Remarks  
  
-   Does not support drillthrough or data mining dimensions. This is because the structure of the nodes in the mining model does not necessarily correspond directly to the underlying data.  
  
-   Does not support the creation of models in Predictive Model Markup Language (PMML) format.  
  
-   Supports the use of OLAP mining models.  
  
-   Does not support the creation of data mining dimensions.  
  
## See Also  
 [Microsoft Neural Network Algorithm Technical Reference](../../analysis-services/data-mining/microsoft-neural-network-algorithm-technical-reference.md)   
 [Mining Model Content for Neural Network Models &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-for-neural-network-models-analysis-services-data-mining.md)   
 [Neural Network Model Query Examples](../../analysis-services/data-mining/neural-network-model-query-examples.md)   
 [Microsoft Logistic Regression Algorithm](../../analysis-services/data-mining/microsoft-logistic-regression-algorithm.md)  
  
  
