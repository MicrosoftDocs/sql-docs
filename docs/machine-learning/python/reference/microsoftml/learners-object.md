--- 
 
# required metadata 
title: "BaseLearner,coef_,fit,get_algo_args,predict,summary_,FastTrees,get_train_node,OneClassSvm,get_train_node,FastForest,get_train_node,FastLinear,get_train_node,LogisticRegression,aic,coef_,deviance_,get_algo_args,get_train_node,NeuralNetwork,get_train_node,OneClassSvm,get_train_node: Learners Python Objects" 
description: "An instance of the following objects is returned by every training function. They all inherit from the class BaseLearner and implement common methods.get_algo_args returns the training parameters,  coef_ retrieves the coefficients,  summary_ returns training information.The content changes based on the trained learner." 
keywords: "models, learners" 
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

# MicrosoftML Learners Objects





## Description

An instance of the following objects is returned by every
training function. They all inherit from the class *BaseLearner* and implement common methods.

* `get_algo_args` returns the training parameters, 

* `coef_`retrieves the coefficients, 

* `summary_` returns training information. 

The content changes based on the trained learner.


## Class BaseLearner



```
microsoftml.modules.base_learner.BaseLearner(**kwargs)
```




Base class for all the learners.



```
coef_
```




Get model coefficients.



```
fit(formula: str, data: [revoscalepy.datasource.RxDataSource.RxDataSource,
    pandas.core.frame.DataFrame], ml_transforms: list = None,
    ml_transform_vars: list = None, row_selection: str = None,
    transforms: dict = None, transform_objects: dict = None,
    transform_function: str = None,
    transform_variables: list = None,
    transform_packages: list = None,
    transform_environment: dict = None, blocks_per_read: int = None,
    report_progress: int = None, verbose: int = 1,
    compute_context: revoscalepy.computecontext.RxComputeContext.RxComputeContext = None,
    **kargs)
```




Fit the model.



```
get_algo_args()
```




Get algorithm arguments.



```
predict(*args, **kwargs)
```




Calls [`microsoftml.rx_predict()`](rx-predict.md).



```
summary_
```




Get model summary.


## Specific Learners



```
microsoftml.FastTrees(method: ['binary', 'regression'] = 'binary',
    num_trees: int = 100, num_leaves: int = 20,
    learning_rate: float = 0.2, min_split: int = 10,
    example_fraction: float = 0.7, feature_fraction: float = 1,
    split_fraction: float = 1, num_bins: int = 255,
    first_use_penalty: float = 0, gain_conf_level: float = 0,
    unbalanced_sets: bool = False, train_threads: int = 8,
    random_seed: int = None,
    ensemble: microsoftml.modules.ensemble.EnsembleControl = None,
    **kargs)
```




FastTree binary or regression model



```
get_train_node(**all_args)
```




get train node



```
microsoftml.OneClassSvm(cache_size: float = 100,
    kernel: [<function linear_kernel at 0x0000007156EAC8C8>,
    <function polynomial_kernel at 0x0000007156EAC950>,
    <function rbf_kernel at 0x0000007156EAC7B8>,
    <function sigmoid_kernel at 0x0000007156EACA60>] = {'Name': 'RbfKernel',
    'Settings': {}}, epsilon: float = 0.001, nu: float = 0.1,
    shrink: bool = True, normalize: ['No', 'Warn', 'Auto',
    'Yes'] = 'Auto',
    ensemble: microsoftml.modules.ensemble.EnsembleControl = None,
    **kargs)
```




one-class svm



```
get_train_node(**all_args)
```




get train node



```
microsoftml.FastForest(method: ['binary', 'regression'] = 'binary',
    num_trees: int = 100, num_leaves: int = 20,
    min_split: int = 10, example_fraction: float = 0.7,
    feature_fraction: float = 0.7, split_fraction: float = 0.7,
    num_bins: int = 255, first_use_penalty: float = 0,
    gain_conf_level: float = 0, train_threads: int = 8,
    random_seed: int = None,
    ensemble: microsoftml.modules.ensemble.EnsembleControl = None,
    **kargs)
```




FastForest binary or regression model



```
get_train_node(**all_args)
```




get train node



```
microsoftml.FastLinear(method: ['binary', 'regression'] = 'binary',
    loss_function: {'binary': [<function hinge_loss at 0x0000007156E8EA60>,
    <function log_loss at 0x0000007156E8E6A8>,
    <function smoothed_hinge_loss at 0x0000007156E8EAE8>],
    'regression': [<function squared_loss at 0x0000007156E8E950>]} = None,
    l2_weight: float = None, l1_weight: float = None,
    train_threads: int = None, convergence_tolerance: float = 0.1,
    max_iterations: int = None, shuffle: bool = True,
    check_frequency: int = None, normalize: ['No', 'Warn', 'Auto',
    'Yes'] = 'Auto',
    ensemble: microsoftml.modules.ensemble.EnsembleControl = None,
    **kargs)
```




SDCA binary or regression model



```
get_train_node(**all_args)
```




get train node



```
microsoftml.LogisticRegression(method: ['binary',
    'multiClass'] = 'binary', l2_weight: float = 1,
    l1_weight: float = 1, opt_tol: float = 1e-07,
    memory_size: int = 20, init_wts_diameter: float = 0,
    max_iterations: int = 2147483647,
    show_training_stats: bool = False, sgd_init_tol: float = 0,
    train_threads: int = None, dense_optimizer: bool = False,
    normalize: ['No', 'Warn', 'Auto', 'Yes'] = 'Auto',
    ensemble: microsoftml.modules.ensemble.EnsembleControl = None,
    **kargs)
```




logistic regression



```
aic(k=2)
```




get model aic



```
coef_
```




get model coefficients



```
deviance_
```




get residual deviance



```
get_algo_args()
```




get algorithm arguments



```
get_train_node(**all_args)
```




get train node



```
microsoftml.NeuralNetwork(method: ['binary', 'multiClass',
    'regression'] = 'binary', num_hidden_nodes: int = 100,
    num_iterations: int = 100, optimizer: ['adadelta_optimizer',
    'sgd_optimizer'] = {'Name': 'SgdOptimizer', 'Settings': {}},
    net_definition: str = None, init_wts_diameter: float = 0.1,
    max_norm: float = 0, acceleration: ['avx_math', 'clr_math',
    'gpu_math', 'mkl_math', 'sse_math'] = {'Name': 'AvxMath',
    'Settings': {}}, mini_batch_size: int = 1, normalize: ['No',
    'Warn', 'Auto', 'Yes'] = 'Auto',
    ensemble: microsoftml.modules.ensemble.EnsembleControl = None,
    **kargs)
```




neural network



```
get_train_node(**all_args)
```




get train node



```
microsoftml.OneClassSvm(cache_size: float = 100,
    kernel: [<function linear_kernel at 0x0000007156EAC8C8>,
    <function polynomial_kernel at 0x0000007156EAC950>,
    <function rbf_kernel at 0x0000007156EAC7B8>,
    <function sigmoid_kernel at 0x0000007156EACA60>] = {'Name': 'RbfKernel',
    'Settings': {}}, epsilon: float = 0.001, nu: float = 0.1,
    shrink: bool = True, normalize: ['No', 'Warn', 'Auto',
    'Yes'] = 'Auto',
    ensemble: microsoftml.modules.ensemble.EnsembleControl = None,
    **kargs)
```




one-class svm



```
get_train_node(**all_args)
```




get train node


### See also

[`rx_fast_forest`](rx-fast-forest.md),
[`rx_fast_trees`](rx-fast-trees.md),
[`rx_fast_linear`](rx-fast-linear.md),
[`rx_logistic_regression`](rx-logistic-regression.md),
[`rx_neural_network`](rx-neural-network.md),
[`rx_oneclass_svm`](rx-oneclass-svm.md),
[`rx_predict`](rx-predict.md)
