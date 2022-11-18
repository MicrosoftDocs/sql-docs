--- 
 
# required metadata 
title: "MLServer class"
description: "The MLServer class is for SQL Machine Learning Services and Machine Learning Server for managing web services." 
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

# Class MLServer


## MLServer

```python
azureml.deploy.server.MLServer
```

Bases: [`azureml.deploy.operationalization.Operationalization`](operationalization.md)

This module provides a service implementation for the ML Server.

## authentication

```python
authentication(context)
```




**Override**

Authentication lifecycle method called by the framework. Invokes
the authentication entry-point for the class hierarchy.

ML Server supports two forms of authentication contexts:

* LDAP: tuple *(username, password)* 

* Azure Active Directory (AAD): dict *{…}* 

* access-token: str *=4534535* 


### Arguments


### context

The authentication context: LDAP, Azure Active Directory
(AAD), or existing *access-token* string.


### HttpException

If an HTTP fault occurred calling the ML Server.

<a name="create_or_update_service_pool"></a>

## create_or_update_service_pool

```python
 create_or_update_service_pool(name, version, initial_pool_size, max_pool_size, **opts)
```


Creates or updates the pool for the published web service, with given initial and maximum pool sizes
on the ML Server by *name* and *version*.

**Example:**

```python
>>> client.create_or_update_service_pool(
        'regression',
        version = 'v1.0.0',
        initial_pool_size = 1,
        maximum_pool_size = 10)
<Response [200]>
>>>
```

### Arguments


### name

The unique web service name.


### version

The web service version.


### initial_pool_size

The initial pool size for the web service.


### max_pool_size

The max pool size for the web service. This cannot be less than initial_pool_size.


### Returns

requests.models.Response: HTTP Status indicating if the request was submitted successfully or not.


### HttpException

If an HTTP fault occurred calling the ML Server.

## delete_service

```python
delete_service(name, **opts)
```




Delete a web service.



```python
success = client.delete_service('example', version='v1.0.1')
print(success)
True
```



### Arguments


### name

The web service name.


### opts

The web service version (*version='v1.0.1*).


### Returns

A `bool` indicating the service deletion was succeeded.


### HttpException

If an HTTP fault occurred calling the ML Server.


<a name="delete_service_pool"></a>

## delete_service_pool

```python
delete_service_pool(name, version, **opts)
```


Delete the pool for the published web service on the ML Server by *name* and *version*.

**Example:**

```python
>>> client.delete_service_pool('regression', version = 'v1.0.0')
<Response [200]>
>>>
```

### Arguments


### name

The unique web service name.


### version

The web service version.


### Returns

requests.models.Response: HTTP Status if the pool was deleted for the service.


### HttpException

If an HTTP fault occurred calling the ML Server.

## deploy_realtime

```python
deploy_realtime(name, **opts)
```




Publish a new *real-time* web service on the ML Server by *name* and
*version*.

All input and output types are defined as a `pandas.DataFrame`.

**Example:**

```python
model = rx_serialize_model(model, realtime_scoring_only=True)
opts = {
    'version': 'v1.0.0',
    'description': 'Real-time service description.',
    'serialized_model': model
}

service = client.deploy_realtime('scoring', **opts)
df = movie_reviews.as_df()
res = service.consume(df)
answer = res.outputs
```


> [!NOTE]
> Using *deploy_realtime()* in this fashion is identical to
publishing a service using the fluent APIS
[`deploy()`](realtime-definition.md)


### Arguments


### name

The web service name.


### opts

The service properties to publish as a `dict`. The *opts*
supports the following optional properties:

* version (str) - Defines a unique alphanumeric web service version. If the version is left blank, a unique *guid* is generated in its place. Useful during service development before the author is ready to officially publish a semantic version to share. 

* description (str) - The service description. 

* alias (str) - The consume function name. Defaults to *consume*. 


### Returns

A new instance of [`Service`](service.md) representing the
real-time service *redeployed*.


### HttpException

If an HTTP fault occurred calling the ML Server.

## deploy_service

```python
deploy_service(name, **opts)
```




Publish a new web service on the ML Server by *name* and *version*.

**Example:**

```python
opts = {
   'version': 'v1.0.0',
   'description': 'Service description.',
   'code_fn': run,
   'init_fn': init,
   'objects': {'local_obj': 50},
   'models': {'model': 100},
   'inputs': {'x': int},
   'outputs': {'answer': float},
   'artifacts': ['histogram.png'],
   'alias': 'consume_service_fn_alias'
 }

 service = client.deploy('regression', **opts)
 res = service.consume_service_fn_alias(100)
 answer = res.output('answer')
 histogram = res.artifact('histogram.png')
```


> [!NOTE]
> Using `deploy_service()` in this fashion is identical to
publishing a service using the fluent APIS
[`deploy()`](service-definition.md).


### Arguments


### name

The unique web service name.


### opts

The service properties to publish. *opts* dict supports the
following optional properties:

* version (str) - Defines a unique alphanumeric web service version. If the version is left blank, a unique *guid* is generated in its place. Useful during service development before the author is ready to officially publish a semantic version to share. 

* description (str) - The service description. 

* code_str (str) - A block of python code to run and evaluate. 

* init_str (str) - A block of python code to initialize service. 

* code_fn (function) - A Function to run and evaluate. 

* init_fn (function) - A Function to initialize service. 

* objects (dict) - Name and value of *objects* to include. 

* models (dict) - Name and value of *models* to include. 

* inputs (dict) - Service input schema by *name* and *type*. The following types are supported: 

  * int 

  * float 

  * str 

  * bool 

  * numpy.array 

  * numpy.matrix 

  * pandas.DataFrame 

* outputs (dict) - Defines the web service output schema. If empty, the service will not return a response value. *outputs* are defined as a dictionary `{'x'=int}` or `{'x': 'int'}` that describes the output parameter names and their corresponding data *types*. The following types are supported: 

  * int 

  * float 

  * str 

  * bool 

  * numpy.array 

  * numpy.matrix 

  * pandas.DataFrame 

* artifacts (list) - A collection of file artifacts to return. File content is encoded as a *Base64 String*. 

* alias (str) - The consume function name. Defaults to *consume*. If *code_fn* function is provided, then it will use that function name by default. 


### Returns

A new instance of [`Service`](service.md) representing the
service *deployed*.


### HttpException

If an HTTP fault occurred calling the ML Server.


## destructor

```python
destructor()
```




**Override**

Destroy lifecycle method called by the framework. Invokes destructors
for the class hierarchy.

## get_service

```python
get_service(name, **opts)
```


Get a web service for consumption.


```python
service = client.get_service('example', version='v1.0.1')
print(service)
<ExampleService>
   ...
   ...
   ...
```



### Arguments


### name

The web service name.


### opts

The optional web service version. If `version=None` the
most recent service will be returned.


### Returns

A new instance of [`Service`](service.md).


### HttpException

If an HTTP fault occurred calling the ML Server.


<a name="get_service_pool_status"></a>

## get_service_pool_status

```python
get_service_pool_status(name, version, **opts)
```




Get status of pool on each compute node of the ML Server for the published services with the provided *name*
and *version*.

**Example:**

```python
>>> client.create_or_update_service_pool(
        'regression',
        version = 'v1.0.0',
        initial_pool_size = 5,
        maximum_pool_size = 5)
<Response [200]>
>>> client.get_service_pool_status('regression', version = 'v1.0.0')
[{'computeNodeEndpoint': 'http://localhost:12805/', 'status': 'Pending'}]
>>> client.get_service_pool_status('regression', version = 'v1.0.0')
[{'computeNodeEndpoint': 'http://localhost:12805/', 'status': 'Success'}]
```

### Arguments


### name

The unique web service name.


### version

The web service version.


### Returns

str: json representing the status of pool on each compute node for the deployed service.


### HttpException

If an HTTP fault occurred calling the ML Server.

```python
initializer(http_client, config, adapters=None)
```




**Override**

Init lifecycle method called by the framework, invoked during
construction. Sets up attributes and invokes initializers for the class
hierarchy.


### Arguments


### http_client

The http request session to manage and persist
settings across requests (auth, proxies).


### config

The global configuration.


### adapters

A `dict` of transport adapters by url.

## list_services

```python
list_services(name=None, **opts)
```




List the different published web services on the ML Server.

The service *name* and service *version* are optional. This call allows
you to retrieve service information regarding:

* All services published 

* All versioned services for a specific named service 

* A specific version for a named service 

Users can use this information along with the
`[get_service()](#getservice)` operation to interact with and consume
the web service.

**Example:**

```python
all_services = client.list_services()
all_versions_of_add_service = client.list_services('add-service')
add_service_v1 = client.list_services('add-service', version='v1')
```



### Arguments


### name

The web service name.


### opts

The optional web service version.


### Returns

A `list` of service metadata.


### HttpException

If an HTTP fault occurred calling the ML Server.

## realtime_service

```python
realtime_service(name)
```




Begin fluent API chaining of properties for defining a *real-time* web
service.

**Example:**



```python
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

## redeploy_realtime

```python
redeploy_realtime(name, **opts)
```




Updates properties on an existing *real-time* web service on the
Server by *name* and *version*. If `version=None` the most recent
service will be updated.

All input and output types are defined as a `pandas.DataFrame`.

**Example:**



```python
model = rx_serialize_model(model, realtime_scoring_only=True)
opts = {
    'version': 'v1.0.0',
    'description': 'Real-time service description.',
    'serialized_model': model
 }

 service = client.redeploy_realtime('scoring', **opts)
 df = movie_reviews.as_df()
 res = service.consume(df)
 answer = res.outputs
```


> [!NOTE]
> Using *redeploy_realtime()* in this fashion is identical to
updating a service using the fluent APIS
[`redeploy()`](realtime-definition.md)


### Arguments


### name

The web service name.


### opts

The service properties to update as a `dict`.  The *opts*
supports the following optional properties:

* version (str) - Defines the web service version. 

* description (str) - The service description. 

* alias (str) - The consume function name. Defaults to *consume*. 


### Returns

A new instance of [`Service`](service.md) representing the
real-time service *redeployed*.


### HttpException

If an HTTP fault occurred calling the ML Server.

## redeploy_service

```python
redeploy_service(name, **opts)
```




Updates properties on an existing web service on the ML Server by *name*
and *version*. If `version=None` the most recent service will be
updated.

**Example:**



```python
opts = {
   'version': 'v1.0.0',
   'description': 'Service description.',
   'code_fn': run,
   'init_fn': init,
   'objects': {'local_obj': 50},
   'models': {'model': 100},
   'inputs': {'x': int},
   'outputs': {'answer': float},
   'artifacts': ['histogram.png'],
   'alias': 'consume_service_fn_alias'
 }

 service = client.redeploy('regression', **opts)
 res = service.consume_service_fn_alias(100)
 answer = res.output('answer')
 histogram = res.artifact('histogram.png')
```


> [!NOTE]
> Using *redeploy_service()* in this fashion is identical to
updating a service using the fluent APIS
[`redeploy()`](service-definition.md)


### Arguments


### name

The web service name.


### opts

The service properties to update as a `dict`. The *opts*
supports the following optional properties:

* version (str) - Defines a unique alphanumeric web service version. If the version is left blank, a unique *guid* is generated in its place. Useful during service development before the author is ready to officially publish a semantic version to share. 

* description (str) - The service description. 

* code_str (str) - A block of python code to run and evaluate. 

* init_str (str) - A block of python code to initialize service. 

* code_fn (function) - A Function to run and evaluate. 

* init_fn (function) - A Function to initialize service. 

* objects (dict) - Name and value of *objects* to include. 

* models (dict) - Name and value of *models* to include. 

* inputs (dict) - Service input schema by *name* and *type*. The following types are supported: - int - float - str - bool - numpy.array - numpy.matrix - pandas.DataFrame 

* outputs (dict) - Defines the web service output schema. If empty, the service will not return a response value. *outputs* are defined as a dictionary `{'x'=int}` or `{'x': 'int'}` that describes the output parameter names and their corresponding data *types*. The following types are supported: - int - float - str - bool - numpy.array - numpy.matrix - pandas.DataFrame 

* artifacts (list) - A collection of file artifacts to return. File content is encoded as a *Base64 String*. 

* alias (str) - The consume function name. Defaults to *consume*. If *code_fn* function is provided, then it will use that function name by default. 


### Returns

A new instance of [`Service`](service.md) representing the
service *deployed*.


### HttpException

If an HTTP fault occurred calling the ML Server.

## service

```python
service(name)
```




Begin fluent API chaining of properties for defining a *standard* web
service.

**Example:**



```python
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
