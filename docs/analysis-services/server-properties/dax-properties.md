---
title: "Analysis Services DAX Properties | Microsoft Docs"
ms.date: 10/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: 
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DAX Properties
[!INCLUDE[ssas-appliesto-sqlas-all](../../includes/ssas-appliesto-sqlas-all.md)]

   The DAX section of msmdsrv.ini contains settings used to control certain query behaviors in Analysis Services, such as the upper limit on the number of rows returned in a DAX query result set.

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
