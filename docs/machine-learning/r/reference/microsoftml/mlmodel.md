---
title: "summary.mlModel function (MicrosoftML)"
description: "Summary of a Microsoft R Machine Learning model."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), summary.mlModel, coef.mlModel, file, manip
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---



 # summary.mlModel: Summary of a Microsoft R Machine Learning model. 
 

Summary of a Microsoft R Machine Learning model.


 ## Usage

```   
 ## S3 method for class `mlModel':
summary  (object, top = 20, ...)

```

 ## Arguments



 ### `object`
 A model object returned from a **MicrosoftML** analysis. 



 ### `top`
 Specifies the count of top coefficients to show in the summary for linear models such as [rxLogisticRegression](rxLogisticRegression.md) and  [rxFastLinear](rxFastLinear.md). The bias appears first, followed by other weights, sorted by their absolute values in descending order. If set to `NULL`,  all non-zero coefficients are shown. Otherwise, only the first `top`coefficients are shown. 



 ### ` ...`
 Additional arguments to be passed to the summary method. 



 ## Details

Provides summary information about the original function call, the   
 data set used to train the model, and statistics for coefficients in the 
 model.


 ## Value

The `summary` method of the **MicrosoftML** analysis objects
returns a list that includes the original function call and the underlying
parameters used. The `coef` method returns a named vector of weights,
processing information from the model object.

For [rxLogisticRegression](rxLogisticRegression.md), the following statistics may also
present in the summary when `showTrainingStats` is set to `TRUE`.


### `training.size `
The size, in terms of row count, of the data set used to train the model.



### `deviance `
The model deviance is given by `-2 * ln(L)` where `L` is the likelihood of obtaining the observations with all features incorporated in the model.



### `null.deviance `
The null deviance is given by `-2 * ln(L0)` where `L0` is the likelihood of obtaining the observations with no effect from the features. The null model includes the bias if there is one in the model.



### `aic`
The AIC (Akaike Information Criterion) is defined as `2 * k ``+ deviance`, where `k` is the number of coefficients of the model. The bias counts as one of the coefficients. The AIC is a measure of the relative quality of the model. It deals with the trade-off between the goodness of fit of the model (measured by deviance) and the complexity of the model (measured by number of coefficients).



### `coefficients.stats`
This is a data frame containing the statistics for each coefficient in the model. For each coefficient, the following statistics are shown. The bias appears in the first row, and the remaining coefficients in the ascending order of p-value.   
*   EstimateThe estimated coefficient value of the model.  
*   Std ErrorThis is the square root of the large-sample variance of the estimate of the coefficient. 
*   z-ScoreWe can test against the null hypothesis, which states that the coefficient should be zero, concerning the significance of the coefficient by calculating the ratio of its estimate and its standard error.  Under the null hypothesis, if there is no regularization applied, the estimate of the concerning coefficient follows a normal distribution with mean 0 and a standard deviation equal to the standard error computed above.  The z-score outputs the ratio between the estimate of a coefficient and the standard error of the coefficient.  
*   Pr(>|z|) This is the corresponding p-value for the two-sided test of the z-score. Based on the significance level, a significance indicator is appended to the p-value. If `F(x)` is the CDF of the standard normal distribution `N(0, 1)`, then `P(>|z|) = 2 - ``2 * F(|z|)`.  



 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## See also

[rxFastTrees](rxFastTrees.md), [rxFastForest](rxFastForest.md),
[rxFastLinear](rxFastLinear.md), [rxOneClassSvm](rxOneClassSvm.md),
[rxNeuralNet](rxNeuralNet.md), [rxLogisticRegression](rxLogisticRegression.md).

 ## Examples

 ```

  # Estimate a logistic regression model
  logitModel <- rxLogisticRegression(isCase ~ age + parity + education + spontaneous + induced,
                    transforms = list(isCase = case == 1),
                    data = infert)
  # Print a summary of the model
  summary(logitModel)

  # Score to a data frame
  scoreDF <- rxPredict(logitModel, data = infert, 
      extraVarsToWrite = "isCase")

  # Compute and plot the Radio Operator Curve and AUC
  roc1 <- rxRoc(actualVarName = "isCase", predVarNames = "Probability", data = scoreDF) 
  plot(roc1)
  rxAuc(roc1)

  #######################################################################################
  # Multi-class logistic regression  
  testObs <- rnorm(nrow(iris)) > 0
  testIris <- iris[testObs,]
  trainIris <- iris[!testObs,]
  multiLogit <- rxLogisticRegression(
      formula = Species~Sepal.Length + Sepal.Width + Petal.Length + Petal.Width,
      type = "multiClass", data = trainIris)

  # Score the model
  scoreMultiDF <- rxPredict(multiLogit, data = testIris, 
      extraVarsToWrite = "Species")    
  # Print the first rows of the data frame with scores
  head(scoreMultiDF)
  # Look at confusion matrix
  table(scoreMultiDF$Species, scoreMultiDF$PredictedLabel)

  # Look at the observations with incorrect predictions
  badPrediction = scoreMultiDF$Species != scoreMultiDF$PredictedLabel
  scoreMultiDF[badPrediction,]
```




