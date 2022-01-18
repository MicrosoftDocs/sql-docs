--- 

# required metadata 
title: "getSampleDataDir function (MicrosoftML) " 
description: " Location where downloaded sample data is stored. " 
keywords: "(MicrosoftML), getSampleDataDir, transform" 
author: "dphansen"
ms.author: "davidph" 
manager: "cgronlun" 
ms.date: 07/15/2019
ms.topic: "reference" 
ms.prod: "mlserver" 
ms.service: "" 
ms.assetid: "" 

# optional metadata 
ROBOTS: "" 
audience: "" 
ms.devlang: "" 
ms.reviewer: "" 
ms.suite: "" 
ms.tgt_pltfrm: "" 
#ms.technology: "" 
ms.custom: "" 

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



