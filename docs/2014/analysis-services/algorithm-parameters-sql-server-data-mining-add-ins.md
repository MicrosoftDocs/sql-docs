---
title: "Algorithm Parameters (SQL Server Data Mining Add-ins) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "MAXIMUM_STATES"
  - "FORCED_REGRESSOR"
  - "PERIODICITY_HINT"
  - "HOLDOUT_SEED"
  - "MAXIMUM_ITEMSET_SIZE"
  - "HIDDEN_NODE_RATIO"
  - "CLUSTERING_METHOD"
  - "FORECAST_METHOD"
  - "STOPPING_TOLERANCE"
  - "MISSING_VALUE_SUBSTITUTION"
  - "MINIMUM_IMPORTANCE"
  - "HISTORIC_MODEL_COUNT"
  - "SPLIT_METHOD"
  - "MAXIMUM_OUTPUT_ATTRIBUTES"
  - "HOLDOUT_PERCENTAGE"
  - "MINIMUM_PROBABILITY"
  - "SAMPLE_SIZE"
  - "HISTORICAL_MODEL_GAP"
  - "CLUSTER_SEED"
  - "SCORE_METHOD"
  - "INSTABILITY_SENSITIVITY"
  - "AUTO_DETECT_PERIODICITY"
  - "MAXIMUM_SEQUENCE_STATES"
  - "MINIMUM_DEPENDENCY_PROBABILITY"
  - "MINIMUM_SUPPORT"
  - "MAXIMUM_SUPPORT"
  - "MINIMUM_ITEMSET_SIZE"
  - "PREDICTION_SMOOTHING"
  - "algorithm parameters"
  - "MAXIMUM_SERIES_VALUE"
  - "MODELLING_CARDINALITY"
  - "MAXIMUM_INPUT_ATTRIBUTES"
  - "MINIMUM_SERIES_VALUE"
  - "MAXIMUM_ITEMSET_COUNT"
  - "CLUSTER_COUNT"
  - "COMPLEXITY_PENALTY"
ms.assetid: fcdc3f85-813d-4279-90b0-16e26edd008d
author: minewiskan
ms.author: owend
manager: craigg
---
# Algorithm Parameters (SQL Server Data Mining Add-ins)
  When you perform data mining by using the Table Analysis Tools for Excel, you do not need to configure the data mining algorithm or parameters; each tool analyzes your data and automatically selects the optimum parameters. However, if you want to modify the model, or create a mining model from scratch, the Data Mining Client for Excel offers several options for customization.  
  
-   Create a data mining model manually, by clicking **Advanced** and then clicking **Add Model to Structure**.  
  
-   Use any of the modeling wizards in the Data Mining Client, and click **Parameters** to control the behavior of the [!INCLUDE[msCoName](../includes/msconame-md.md)] data mining algorithms.  
  
-   Click **Query** to open the Query Model wizard, and then click **Advanced** to open the **Data Mining Advanced Query Editor**. In this editor, you can build models by using DMX templates.  
  
 You can also modify the behavior of mining models that are already created, or you can filter the results, by setting parameters in the mining model viewer.  
  
## List of Algorithm Parameters  
 All [!INCLUDE[msCoName](../includes/msconame-md.md)] algorithms can be customized by setting parameters. Because the best parameter settings depend on the composition of your data, a full explanation of the effects of changing parameters is beyond the scope of this topic.  
  
 The following table lists the parameters, describes their functionality, and provides links to more technical information.  
  
|Parameter name|Used in|Description|  
|--------------------|-------------|-----------------|  
|AUTO_DETECT_PERIODICITY|Microsoft Time Series Algorithm|Specifies a numeric value between 0 and 1 that is used to detect periodicity. Setting this value closer to 1 favors the discovery of many near-periodic patterns and the automatic generation of periodicity hints. Dealing with many periodicity hints will likely lead to significantly longer model training times and more accurate models. If the value is closer to 0, periodicity is detected only for strongly periodic data.<br /><br /> The default is 0.6.|  
|CLUSTER_COUNT|Microsoft Clustering Algorithm<br /><br /> Microsoft Sequence Clustering Algorithm|Specifies the approximate number of clusters to be built by the algorithm. If the approximate number of clusters cannot be built from the data, the algorithm builds as many clusters as possible. Setting the CLUSTER_COUNT to 0 causes the algorithm to use heuristics to best determine the number of clusters to build.<br /><br /> The default is 10.|  
|CLUSTER_SEED|Microsoft Clustering Algorithm|Specifies the seed number that is used to randomly generate clusters for the initial stage of model building.<br /><br /> The default is 0.|  
|CLUSTERING_METHOD|Microsoft Clustering Algorithm|Specifies the clustering method for the algorithm to use. The following clustering methods are available: scalable EM (1), non-scalable EM (2), scalable K-Means (3), and non-scalable K-Means (4).<br /><br /> The default is 1.|  
|COMPLEXITY_PENALTY|Microsoft Decision Trees Algorithm<br /><br /> Microsoft Time Series Algorithm|Controls the growth of the decision tree. A low value increases the number of splits, and a high value decreases the number of splits. The default value is based on the number of attributes for a particular model, as described in the following list:<br /><br /> For 1 through 9 attributes, the default is 0.5.<br /><br /> For 10 through 99 attributes, the default is 0.9.<br /><br /> For 100 or more attributes, the default is 0.99.<br /><br /> Note: In time series models, this parameter applies only to models that are built by using the ARTxp algorithm, or to mixed models.|  
|FORCED_REGRESSOR|Microsoft Decision Trees Algorithm<br /><br /> Microsoft Linear Regression Algorithm|Forces the algorithm to use the indicated columns as regressors, regardless of the importance of the columns as calculated by the algorithm.<br /><br /> Note: This parameter is only used for decision trees that are predicting a continuous attribute. By definition, a linear regression model is a special case of decision trees that predicts continuous attributes. However, any decision tree model can contain a node that represents a linear regression formula.|  
|FORECAST_METHOD|Microsoft Time Series Algorithm|Indicates whether predictions should be made using the ARTxp algorithm, the ARIMA algorithm, or a combination of both.<br /><br /> The default is MIXED.|  
|HIDDEN_NODE_RATIO|Microsoft Neural Network Algorithm|Specifies the ratio of hidden neurons to input and output neurons. The following formula determines the initial number of neurons in the hidden layer:<br /><br /> HIDDEN_NODE_RATIO * SQRT(Total input neurons \* Total output neurons)<br /><br /> The default value is 4.0.|  
|HISTORIC_MODEL_COUNT|Microsoft Time Series Algorithm|Specifies the number of historic models that will be built.<br /><br /> The default is 1.|  
|HISTORICAL_MODEL_GAP|Microsoft Time Series Algorithm|Specifies the time lag between two consecutive historic models. For example, setting this value to g causes historic models to be built for data that is truncated by time slices at intervals of g, 2*g, 3\*g, and so on.<br /><br /> The default is 10.|  
|HOLDOUT_PERCENTAGE|Microsoft Logistic Regression Algorithm<br /><br /> Microsoft Neural Network Algorithm|Specifies the percentage of cases within the training data used to calculate the holdout error, which is used as part of the stopping criteria while training the mining model.<br /><br /> The default value is 30.<br /><br /> Note: This parameter is different from the holdout percentage value that applies to a mining structure.|  
|HOLDOUT_SEED|Microsoft Logistic Regression Algorithm<br /><br /> Microsoft Neural Network Algorithm|Specifies a number that is used to seed the pseudo-random generator when the algorithm randomly determines the holdout data. If this parameter is set to 0, the algorithm generates the seed based on the name of the mining model, to guarantee that the model content remains the same during reprocessing.<br /><br /> The default value is 0.<br /><br /> Note: This parameter is different from the holdout seed value that applies to a mining structure.|  
|INSTABILITY_SENSITIVITY|Microsoft Time Series Algorithm|Controls the point at which prediction variance exceeds a certain threshold and the ARTxp algorithm suppresses predictions. The default value is 1.<br /><br /> Note: This parameter applies only to mixed models or models that use the ARTxp algorithm.|  
|MAXIMUM_INPUT_ATTRIBUTES|Microsoft Clustering Algorithm<br /><br /> Microsoft Decision Trees Algorithm<br /><br /> Microsoft Linear Regression Algorithm<br /><br /> Microsoft Naïve Bayes Algorithm<br /><br /> Microsoft Neural Network Algorithm<br /><br /> Microsoft Logistic Regression Algorithm|Defines the number of input attributes that the algorithm can handle before it invokes feature selection. Set this value to 0 to turn off feature selection.<br /><br /> The default is 255.|  
|MAXIMUM_ITEMSET_COUNT|Microsoft Association Algorithm|Specifies the maximum number of itemsets to produce. If no number is specified, the algorithm generates all possible itemsets.<br /><br /> The default is 200000.|  
|MAXIMUM_ITEMSET_SIZE|Microsoft Association Algorithm|Specifies the maximum number of items that are allowed in an itemset. Setting this value to 0 specifies that there is no limit to the size of the itemset.<br /><br /> The default is 3.|  
|MAXIMUM_OUTPUT_ATTRIBUTES|Microsoft Decision Trees Algorithm<br /><br /> Microsoft Linear Regression Algorithm<br /><br /> Microsoft Logistic Regression Algorithm<br /><br /> Microsoft Naïve Bayes Algorithm<br /><br /> Microsoft Neural Network Algorithm|Defines the number of output attributes that the algorithm can handle before it invokes feature selection. Set this value to 0 to turn off feature selection.<br /><br /> The default is 255.|  
|MAXIMUM_SEQUENCE_STATES|Microsoft Sequence Clustering Algorithm|Specifies the maximum number of states that a sequence can have. Setting this value to a number greater than 100 may cause the algorithm to create a model that does not provide meaningful information.<br /><br /> The default is 64.|  
|MAXIMUM_SERIES_VALUE|Microsoft Time Series Algorithm|Specifies the maximum value to use for predictions. This parameter is used, together with MINIMUM_SERIES_VALUE, to constrain the predictions to some expected range. For example, you can specify that the predicted sales quantity for any day should never exceed the number of products in inventory.|  
|MAXIMUM_STATES|Microsoft Clustering Algorithm<br /><br /> Microsoft Neural Network Algorithm<br /><br /> Microsoft Sequence Clustering Algorithm|Specifies the maximum number of attribute states that the algorithm supports. If the number of states that an attribute has is larger than the maximum number of states, the algorithm uses the attribute's most popular states and ignores the remaining states.<br /><br /> The default is 100.|  
|MAXIMUM_SUPPORT|Microsoft Association Algorithm|Specifies the maximum number of cases in which an itemset can have support. If this value is less than 1, the value represents a percentage of the total cases. If this value is greater than 1, the value represents the absolute number of cases that can contain the itemset.<br /><br /> The default is 1.|  
|MINIMUM_IMPORTANCE|Microsoft Association Algorithm|Specifies the importance threshold for association rules. Rules with importance less than this value are filtered out.|  
|MINIMUM_ITEMSET_SIZE|Microsoft Association Algorithm|Specifies the minimum number of items that are allowed in an itemset.<br /><br /> The default is 1.|  
|MINIMUM_DEPENDENCY_PROBABILITY|Microsoft Naïve Bayes Algorithm|Specifies the minimum dependency probability between input and output attributes. This value is used to limit the size of the content that is generated by the algorithm. This property can be set from 0 to 1. Larger values reduce the number of attributes in the content of the model.<br /><br /> The default is 0.5.|  
|MINIMUM_PROBABILITY|Microsoft Association Algorithm|Specifies the minimum probability that a rule is true. For example, setting this value to 0.5 specifies that no rule with less than fifty percent probability is generated.<br /><br /> The default is 0.4.|  
|MINIMUM_SERIES_VALUE|Microsoft Time Series Algorithm|Specifies the lower constraint for any time series prediction. Predicted values will never be smaller than this constraint.|  
|MINIMUM_SUPPORT|Microsoft Association Algorithm|Specifies the minimum number of cases that must contain the itemset before the algorithm generates a rule. Setting this value to less than 1 specifies the minimum number of cases as a percentage of the total cases. Setting this value to a whole number greater than 1 specifies the minimum number of cases as the absolute number of cases that must contain the itemset. The algorithm may increase the value of this parameter, if memory is limited.<br /><br /> The default is 0.03.|  
|MINIMUM_SUPPORT|Microsoft Clustering Algorithm|Specifies the minimum number of cases in each cluster.<br /><br /> The default is 1.|  
|MINIMUM_SUPPORT|Microsoft Decision Trees Algorithm|Determines the minimum number of leaf cases that is required to generate a split in the decision tree.<br /><br /> The default is 10.|  
|MINIMUM_SUPPORT|Microsoft Sequence Clustering Algorithm|Specifies the minimum number of cases in each cluster.<br /><br /> The default is 10.|  
|MINIMUM_SUPPORT|Microsoft Time Series Algorithm|Specifies the minimum number of time slices that are required to generate a split in each time series tree.<br /><br /> The default is 10.|  
|MISSING_VALUE_SUBSTITUTION|Microsoft Time Series Algorithm|Specifies the method that is used to fill the gaps in historical data. By default, irregular gaps or ragged edges in data are not allowed. The following methods can be used to fill in irregular gaps or edges: use the previous value, use the mean value, or use a specific numeric constant.|  
|MODELLING_CARDINALITY|Microsoft Clustering Algorithm|Specifies the number of sample models that are constructed during the clustering process.<br /><br /> The default is 10.|  
|PERIODICITY_HINT|Microsoft Time Series Algorithm|Provides a hint to the algorithm as to the periodicity of the data. For example, if sales vary by year, and the unit of measurement in the series is months, the periodicity is 12. This parameter takes the format of {n [, n]}, where n is any positive number. The n within the brackets [] is optional and can be repeated as frequently as needed.<br /><br /> The default is {1}.|  
|PREDICTION_SMOOTHING|Microsoft Time Series Algorithm|Controls the blend of ARTXP and ARIMA time series algorithms. The specified value is only valid when the FORECAST_METHOD parameter is set to MIXED. Values must be between 0 and 1. If the value is 0, the model uses only ARTXP. If the value is 1, the model uses only ARIMA. A value closer to 0 is more heavily weighted to ARTXP. A value closer to 1 is more heavily weighted to ARIMA.|  
|SAMPLE_SIZE|Microsoft Clustering Algorithm|Specifies the number of cases that the algorithm uses on each pass if the CLUSTERING_METHOD parameter is set to one of the scalable clustering methods. Setting the SAMPLE_SIZE parameter to 0 will cause the whole dataset to be clustered in a single pass. This can cause memory and performance issues.<br /><br /> The default is 50000.|  
|SAMPLE_SIZE|Microsoft Logistic Regression Algorithm<br /><br /> Microsoft Neural Network Algorithm|Specifies the number of cases to be used to train the model. The algorithm provider uses either this number or the percentage of total of cases that are not included in the holdout percentage as specified by the HOLDOUT_PERCENTAGE parameter, whichever value is smaller.<br /><br /> In other words, if HOLDOUT_PERCENTAGE is set to 30, the algorithm will use either the value of this parameter, or a value that is equal to 70 percent of the total number of cases, whichever is smaller.<br /><br /> The default is 10000.|  
|SCORE_METHOD|Microsoft Decision Trees Algorithm|Determines the method that is used to calculate the split score. The following options are available: (1) Entropy, (2) Bayesian with K2 Prior, or (3) Bayesian Dirichlet Equivalent (BDE) Prior.<br /><br /> The default is 3.|  
|SPLIT_METHOD|Microsoft Decision Trees Algorithm|Determines the method that is used to split the node. The following options are available: Binary (1), Complete (2), or Both (3).<br /><br /> The default is 3.|  
|STOPPING_TOLERANCE|Microsoft Clustering Algorithm Technical Reference|Specifies the value that is used to determine when convergence is reached and the algorithm is finished building the model. Convergence is reached when the overall change in cluster probabilities is less than the ratio of the STOPPING_TOLERANCE parameter divided by the size of the model.<br /><br /> The default is 10.|  
  
### Comments  
 For additional detail about the algorithms, see SQL Server Books Online.  
  
## See Also  
 [Data Mining Algorithms &#40;SQL Server Data Mining Add-ins&#41;](data-mining-algorithms-sql-server-data-mining-add-ins.md)  
  
  
