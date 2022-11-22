---
title: "getSampleDataDir function (MicrosoftML)"
description: "Location where downloaded sample data is stored."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (MicrosoftML), getSampleDataDir, transform
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


 # getSampleDataDir: Get Package Sample Data Location 
 

Location where downloaded sample data is stored.


 ## Usage

```   
  getSampleDataDir(sampleDataDir = NULL, createDir = TRUE)

```

 ## Arguments



 ### `sampleDataDir`
 Specifies the path to the location where downloaded sample data is (or is to be) stored or `NULL`. 



 ### `createDir`
 `TRUE` to create the directory if it does not exist; `FALSE` not to create the directory. 



 ## Details

If `sampleDataDir` is `NULL`, the function first 
checks to see if an option has been set containing `sampleDataDir`,
 i.e. `getOption("sampleDataDir")`. If that is `NULL` too, a
'sampleDataDir' subdirectory of the current working directory is used. If
`createDir` is `TRUE`, the directory is created if it does not
exist.


 ## Value

A character string containing the path to the location of the 
sample data.

 ## Author(s)

Microsoft Corporation [`Microsoft Technical Support`](https://go.microsoft.com/fwlink/?LinkID=698556&clcid=0x409)



 ## Examples

 ```

  # This example sets the option to be the same as the default
  options(sampleDataDir = file.path(getwd(), "sampleDataDir"))

  dataDir <- getSampleDataDir(createDir = FALSE)
```



