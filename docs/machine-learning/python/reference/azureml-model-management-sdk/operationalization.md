--- 
 
# required metadata 
title: "Operationalization class"
description: "The Operationalization class is for SQL Machine Learning Services and Machine Learning Server for managing web services." 
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

# Class Operationalization


## Operationalization



```
azureml.deploy.operationalization.Operationalization
```




*Operationalization* is designed to be a low-level abstract foundation class
from which other service operationalization attribute classes in the
*mldeploy* package can be derived. It provides a standard template for
creating attribute-based operationalization lifecycle phases providing a
consistent  *__init()__*, *__del()__* sequence that chains initialization
(initializer), authentication (authentication), and destruction (destructor)
methods for the class hierarchy.



## authentication

```python
authentication(context)
```




Authentication lifecycle method. Invokes the authentication entry-point
for the class hierarchy.

An optional _noonp_ method where subclass implementers MAY provide this
method definition by overriding.

Sub-class should override and implement.


### Arguments


### context

The optional authentication context as defined in the
implementing sub-class.



## delete_service

```python
delete_service(name, **opts)
```




Sub-class should override and implement.



## deploy_realtime

```python
deploy_realtime(name, **opts)
```




Sub-class should override and implement.



## deploy_service

```python
deploy_service(name, **opts)
```




Sub-class should override and implement.



## destructor

```python
destructor()
```




Destroy lifecycle method. Invokes destructors for the class hierarchy.

An optional _noonp_ method where subclass implementers MAY provide this
method definition by overriding.

Sub-class should override and implement.



## get_service

```python
get_service(name, **opts)
```




Retrieve service meta-data from the name source and return a new
service instance.

Sub-class should override and implement.



## initializer

```python
initializer(api_client, config, adapters=None)
```




Init lifecycle method, invoked during construction. Sets up attributes
and invokes initializers for the class hierarchy.

An optional _noonp_ method where subclass implementers MAY provide this
method definition by overriding.

Sub-class should override and implement.



## list_services

```python
list_services(name=None, **opts)
```




Sub-class should override and implement.



## realtime_service

```python
realtime_service(name)
```




Begin fluent API chaining of properties for defining a *real-time* web
service.

**Example:**



```
client.realtime_service('scoring')
   .description('A new real-time web service')
   .version('v1.0.0')
```



### Arguments


### name

The web service name.


### Returns

A [`RealtimeDefinition`](realtime-definition.md) instance for fluent API
chaining.



```
redeploy_realtime(name, force=False, **opts)
```




Sub-class should override and implement.



## redeploy_service

```python
redeploy_service(name, force=False, **opts)
```




Sub-class should override and implement.



## service

```python
service(name)
```




Begin fluent API chaining of properties for defining a *standard* web
service.

**Example:**



```
client.service('scoring')
   .description('A new web service')
   .version('v1.0.0')
```



### Arguments


### name

The web service name.


### Returns

A [`ServiceDefinition`](service-definition.md) instance for fluent API
chaining.
