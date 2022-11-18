--- 
 
# required metadata 
title: "mutualinformation_select: Machine Learning Mutual Information Mode Feature Selection Transform" 
description: "Selects the top k features across all specified columns ordered by their mutual information with the label column." 
keywords: "feature, selection, mutual, information" 
author: WilliamDAssafMSFT
ms.author: wiassaf 
ms.date: 07/15/2019
ms.topic: "reference" 
ms.service: sql
ms.subservice: "machine-learning-services" 
ms.assetid: "" 
 
# optional metadata 
ROBOTS: "" 
audience: "" 
ms.devlang: "Python" 
ms.reviewer: "" 
ms.suite: "" 
ms.tgt_pltfrm: "" 
ms.custom: "" 
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
 
---

# *microsoftml.mutualinformation_select*: Feature selection based on mutual information





## Usage



```
microsoftml.mutualinformation_select(cols: [list, str], label: str,
    num_features_to_keep: int = 1000, num_bins: int = 256, **kargs)
```





## Description

Selects the top k features across all specified columns ordered by their mutual information with the label column.


## Details

The mutual information of two random variables `X` and `Y` is a
measure of the mutual dependence between the variables. Formally, the
mutual information can be written as:

`I(X;Y) = E[log(p(x,y)) - log(p(x)) - log(p(y))]`

where the expectation is taken over the joint distribution of `X` and
`Y`. Here `p(x,y)` is the joint probability density function of
`X` and `Y`, `p(x)` and `p(y)` are the marginal
probability density functions of `X` and `Y` respectively. In
general, a higher mutual information between the dependent variable (or
label) and an independent variable (or feature) means that the label has
higher mutual dependence over that feature.

The mutual information feature selection mode selects the features based on
the mutual information. It keeps the top `num_features_to_keep` features
with the largest mutual information with the label.


## Arguments


### cols

Specifies character string or list of the names of the variables to select.


### label

Specifies the name of the label.


### num_features_to_keep

If the number of features to keep is specified to
be `n`, the transform picks the `n` features that have the highest
mutual information with the dependent variable. The default value is 1000.


### num_bins

Maximum number of bins for numerical values. Powers of 2
are recommended. The default value is 256.


### kargs

Additional arguments sent to compute engine.


## Returns

An object defining the transform.


## See also

[`count_select`](count-select.md)


## References

[Wikipedia: Mutual Information](https://en.wikipedia.org/wiki/Mutual_information)
