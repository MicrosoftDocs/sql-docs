---
title: "Dataset Properties Dialog Box, Parameters | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: reference
f1_keywords: 
  - "10150"
  - "sql13.rtp.rptdesigner.datasetproperties.parameters.f1"
  - "10023"
ms.assetid: 43b00aab-e2c3-4e85-abe1-a2b5a21efeed
author: maggiesMSFT
ms.author: maggies
---
# Dataset Properties Dialog Box, Parameters
  Select **Parameters** on the **Dataset Properties** dialog box to add, change, and delete query parameters, including query parameters that link to report parameters.  
  
 Whenever the query is changed on the query tab, the query command is parsed. For each query parameter that is identified, a report parameter with an identical case-sensitive name is created. By default, the query parameter is automatically added to the query parameter list and linked to the corresponding report parameter.  
  
 If there are dependencies for the default values of one report parameter on another report parameter that is linked to a query parameter, the order of report parameters (as they appear in the **Report Parameters Properties** dialog box) is important. Report parameters later in the list can refer to report parameters earlier in the list. For more information about report parameters, see [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
## Options  
 **Add**  
 Add a new parameter to the list.  
  
 **Delete**  
 Remove the selected parameter from the list.  
  
 **Parameter name**  
 Type the name of a query parameter that you added to the dataset on the **Query** tab of the **Dataset Properties** dialog box.  
  
 **Parameter value**  
 Enter a value for the query parameter. This can be a static value or an expression that refers to an object within the report, but it cannot refer to any report items or fields. By default, **Value** contains an expression that points to a report parameter.  
  
 **Up arrow**  
 Move the selected parameter up in the list.  
  
 **Down arrow**  
 Move the selected parameter down in the list.  
  
## See Also  
 [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)   
 [Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md)   
 [Change the Order of a Report Parameter &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/change-the-order-of-a-report-parameter-report-builder-and-ssrs.md)  
  
  
