---
title: "Historical Attribute Options (Slowly Changing Dimension Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.loaddimwizard.histattriboption.f1"
ms.assetid: a176ec66-ec39-4c99-99d1-c1afa8450e1e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Historical Attribute Options (Slowly Changing Dimension Wizard)
  Use the **Historical Attributes Options** dialog box to show historical attributes by start and end dates, or to record historical attributes in a column specially created for this purpose.  
  
 To learn more about this wizard, see [Slowly Changing Dimension Transformation](slowly-changing-dimension-transformation.md).  
  
## Options  
 **Use a single column to show current and expired records**  
 If you choose to use a single column to record the status of historical attributes, the following options are available:  
  
|Option|Description|  
|------------|-----------------|  
|**Column to indicate current record**|Select a column in which to indicate the current record.|  
|**Value when current**|Use either **True** or **Current** to show whether the record is current.|  
|**Expiration value**|Use either **False** or **Expired** to show whether the record is a historical value.|  
  
 **Use start and end dates to identify current and expired records**  
 The dimension table for this option must include a date column. If you choose to show historical attributes by start and end dates, the following options are available:  
  
|Option|Description|  
|------------|-----------------|  
|**Start date column**|Select the column in the dimension table to contain the start date.|  
|**End date column**|Select the column in the dimension table to contain the end date.|  
|**Variable to set date values**|Select a date variable from the list.|  
  
## See Also  
 [Configure Outputs Using the Slowly Changing Dimension Wizard](configure-outputs-using-the-slowly-changing-dimension-wizard.md)  
  
  
