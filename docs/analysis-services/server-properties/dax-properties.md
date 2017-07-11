---
title: "DAX Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: aa928dc5-d00d-4f8a-80b9-7e6973d2196c
caps.latest.revision: 6
author: "HeidiSteen"
ms.author: "heidist"
manager: "erikre"
---
# DAX Properties
   The DAX section of msmdsrv.ini contains settings used to control certain query behaviors in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], such as the upper limit on the number of rows returned in a DAX query result set. 
  
  For very large rowsets, such as those returned in DirectQuery models, the default of one million rows could be insufficient. You'll know whether the limit needs adjusting if you get this error: "The result set of a query to external data source has exceeded the maximum allowed size of '1000000' rows."
 
To increase the upper limit, specify the **MaxIntermediateRowSize** configuration setting. You will need to manually add the entire element to the DAX section of the configuration file. The setting is not present in the file until you add it.
  
## Configuration snippet

```
<ConfigurationSettings>
. . .
<DAX>   
  <PredicateCheckSpoolCardinalityThreshold>5000
  </PredicateCheckSpoolCardinalityThreshold>
  <DQ>
     <MaxIntermediateRowsetSize>1000000
     </MaxIntermediateRowsetSize>
  </DQ>
</DAX>
. . . 
```

## Property descriptions

Setting |Value |Description
--------|-------|-----------
MaxIntermediateRowsetSize | 1000000 | Maximum number of rows returned in a DAX query. Manually add this entry to the msmdsrv.ini file and increase the value if the default is too low. 
PredicateCheckSpoolCardinalityThreshold| 5000 | An advanced property that you should not change, except under the guidance of Microsoft support.

For more information about additional server properties and how to set them, see [Server Properties in Analysis Services](../../analysis-services/server-properties/server-properties-in-analysis-services.md). 