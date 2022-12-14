---
title: "maOptimizer function (MicrosoftML)"
description: "Specifies Optimization Algorithms for Neural Net."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), maOptimizer, adaDeltaSgd, sgd, optimizer
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---




 # maOptimizer: Optimization Algorithms 
 

Specifies Optimization Algorithms for Neural Net.


 ## Usage

```   
  adaDeltaSgd(decay = 0.95, conditioningConst = 1e-06)

  sgd(learningRate = 0.001, momentum = 0, nag = FALSE, weightDecay = 0,
    lRateRedRatio = 1, lRateRedFreq = 100, lRateRedErrorRatio = 0)

```

 ## Arguments



 ### `decay`
 Specifies the decay rate applied to gradients when calculating the step in the ADADELTA adaptive optimization algorithm. This rate is used  to ensure that the learning rate continues to make progress by giving smaller weights to remote gradients in the calculation of the step size. Mathematically, it replaces the mean square of the gradients with an exponentially decaying  average of the squared gradients in the denominator of the update rule. The  value assigned must be in the range (0,1). 



 ### `conditioningConst`
 Specifies a conditioning constant for the ADADELTA  adaptive optimization algorithm that is used to condition the step size in   regions where the exponentially decaying average of the squared gradients  is small. The value assigned must be in the range (0,1). 



 ### `learningRate`
 Specifies the size of the step taken in the direction of the negative gradient for each iteration of the learning process.   The default value is `= 0.001`. 



 ### `momentum`
 Specifies weights for each dimension that control the contribution of the previous step to the size of the next step during  training. This modifies the `learningRate` to speed up training. The value must be `>= 0` and `< 1`. 



 ### `nag`
 If `TRUE`, Nesterov's Accelerated Gradient Descent is used.  This method reduces the oracle complexity of gradient descent and is optimal  for smooth convex optimization. 



 ### `weightDecay`
 Specifies the scaling weights for the step size. After  each weight update, the weights in the network are scaled by `(1 -  ``learningRate * weightDecay)`. The value must be `>= 0` and `< 1`. 



 ### `lRateRedRatio`
 Specifies the learning rate reduction ratio: the ratio by which the learning rate is reduced during training. Reducing the learning rate can avoid local minima. The value must be `> 0` and `<= 1`.    
*   A value of `1.0` means no reduction.   
*   A value of `0.9` means the learning rate is reduced to 90  its current value.  
 The reduction can be triggered either periodically, to occur after a fixed   number of iterations, or when a certain error criteria concerning increases  or decreases in the loss function are satisfied.    
*   To trigger a periodic rate reduction, specify the frequency  by setting the number of iterations between reductions with the  `lRateRedFreq` argument.   
*   To trigger rate reduction based on an error criterion, specify a number   in `lRateRedErrorRatio`. 




 ### `lRateRedFreq`
 Sets the learning rate reduction frequency by specifying  number of iterations between reductions. For example, if `10` is  specified, the learning rate is reduced once every 10 iterations. 



 ### `lRateRedErrorRatio`
 Specifies the learning rate reduction error criterion.  If set to `0`, the learning rate is reduced if the loss increases between iterations. If set to a fractional value greater than`0`, the learning rate is reduced if the loss decreases by less than that fraction of its previous value. 



 ## Details

These functions can be used for the `optimizer` argument in 
[rxNeuralNet](rxNeuralNet.md). 


The `sgd` function specifies Stochastic Gradient Descent. maOptimizer

The `adaDeltaSgd` function specifies the AdaDelta gradient 
descent, described in the 2012 paper "ADADELTA: An Adaptive Learning Rate 
Method" by Matthew D.Zeiler. 




 ## Value

A character string that contains the specification for the 
 optimization algorithm.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## References

[`ADADELTA: An Adaptive Learning Rate Method`](https://arxiv.org/abs/1212.5701)



 ## See also

[rxNeuralNet](rxNeuralNet.md),

 ## Examples

 ```

  myIris = iris
  myIris$Setosa <- iris$Species == "setosa"

  res1 <- rxNeuralNet(formula = Setosa~Sepal.Length + Sepal.Width + Petal.Width,
          data = myIris, 
          optimizer = sgd(learningRate = .002))

  res2 <- rxNeuralNet(formula = Setosa~Sepal.Length + Sepal.Width + Petal.Width,
          data = myIris, 
          optimizer = adaDeltaSgd(decay = .9, conditioningConst = 1e-05))
```



