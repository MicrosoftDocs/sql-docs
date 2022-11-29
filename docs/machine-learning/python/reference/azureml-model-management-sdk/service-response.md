--- 
 
# required metadata 
title: "ServiceResponse class"
description: "The ServiceResponse class is for SQL Machine Learning Services and Machine Learning Server for managing web services." 
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

# Class ServiceResponse


## ServiceResponse



```
azureml.deploy.server.service.ServiceResponse(api, response, output_schema)
```




Represents the response from a service invocation. The response will
contain any outputs and file artifacts produced in addition to any console
output or errors messages.



## api

```python
api
```




Gets the api endpoint.



## artifact

```python
artifact(artifact_name, decode=True, encoding=None)
```




A convenience function to look up a file artifact by name and optionally
base64 decode it.


### Arguments


### artifact_name

The name of the file artifact.


### decode

Whether to decode the Base64 encoded artifact string. The
default is `True`.


### encoding

The encoding scheme to be used. The default is to apply
no encoding. For a list of all encoding schemes please visit
*Standard Encodings:*
[https://docs.python.org/3/library/codecs.html#standard-encodings](https://docs.python.org/3/library/codecs.html#standard-encodings)


### Returns

The file artifact as a Base64 encoded string if
`decode=False` otherwise the decoded string.



## artifacts

```python
artifacts
```




Returns a `list` of non-decoded response file artifacts if present.



## console_output

```python
console_output
```




Gets the console output if present.



## error 

```python
error
```




Gets the error if present.



## output

```python
output(output)
```




A convenience function to look up an output value by name.


### Arguments


### output

The name of the output.


### Returns

The service output's value.



## outputs

```python
outputs
```




Gets the response outputs if present.



## raw_outputs

```python
raw_outputs
```




Gets the raw response outputs if present.
