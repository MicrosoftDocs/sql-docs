--- 
 
# required metadata 
title: "ServiceDefinition class"
description: "The ServiceDefinition class is for SQL Machine Learning Services and Machine Learning Server for managing web services." 
keywords: "" 
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
#ms.subservice: "" 
ms.custom: ""
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
 
---

# ServiceDefinition


## Class ServiceDefinition



```
azureml.deploy.operationalization.ServiceDefinition(name, op)
```




Bases: [`azureml.deploy.operationalization.OperationalizationDefinition`](operationalization-definition.md)

Service class defining a *standard* service's properties for publishing.



```python
alias(alias)
```




Set the optional service function name alias to use in order to consume
the service.

**Example:**



```
service = client.service('score-service').alias('score').deploy()

# `score()` is the function that will call the `score-service`
result = service.score()
```



### Arguments


### alias

The service function name alias to use in order to consume
the service.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API.



## artifact

```python
artifact(artifact)
```




Define a service's optional supported file artifact by name. A
convenience to calling `.artifacts(['file.png'])` with a list of one.


### Arguments


### artifact

A single file artifact by name.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API
chaining.



## artifacts

```python
artifacts(artifacts)
```




Defines a service's optional supported file artifacts by name.


### Arguments


### artifacts

A `list` of file artifacts by name.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API
chaining.



## code_fn

```python
code_fn(code, init=None)
```




Set the service consume function as a function.

**Example:**



```
def init():
    pass

def score(df):
    pass

.code_fn(score, init)
```



### Arguments


### code

A function handle as a reference to run python code.


### init

An optional function handle as a reference to initialize
the service.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API
chaining.



## code_str

```python
code_str(code, init=None)
```




Set the service consume function as a block of python code as a `str`.



```
init = 'import pandas as pd'
code = 'print(pd)'

.code_str(code, init)
```



### Arguments


### code

A block of python code as a `str`.


### init

An optional block of python code as a `str` to initialize
the service.


### Returns

A [`ServiceDefinition`](service-definition.md) for fluent API chaining.



## deploy

```python
deploy()
```




Bundle up the definition properties and publish the service.


### Returns

A new instance of [`Service`](service.md) representing the
service *deployed*.



## description

```python
description(description)
```




Set the service's optional description.


### Arguments


### description

The description of the service.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API.



## inputs

```python
inputs(**inputs)
```




Defines a service's optional supported inputs by name and type.

**Example:**



```
.inputs(a=float, b=int, c=str, d=bool, e='pandas.DataFrame')
```



### Arguments


### inputs

The inputs by name and type.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API
chaining.



## models

```python
models(**models)
```




Include any model(s) used for this service.

**Example:**



```
cars_model = rx_lin_mod(formula="am ~ hp + wt",data=mtcars)

.models(cars_model=cars_model)
```



### Arguments


### models

Any models by name and value.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API
chaining.



## objects

```python
objects(**objects)
```




Include any object(s) used for this service.

**Example:**



```
x = 5
y = 'hello'

.objects(x=x, y=y)
```



### Arguments


### objects

Any objects by name and value.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API
chaining.



## outputs

```python
outputs(**outputs)
```




Defines a service's optional supported outputs by name and type.

**Example:**



```
.outputs(a=float, b=int, c=str, d=bool, e='pandas.DataFrame')
```



### Arguments


### outputs

The outputs by name and type.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API
chaining.



## redeploy

```python
redeploy(force=False)
```




Bundle up the definition properties and update the service.


### Returns

A new instance of [`Service`](service.md) representing the
service *deployed*.



## version

```python
version(version)
```




Set the service's optional version.


### Arguments


### version

The version of the service.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API.
