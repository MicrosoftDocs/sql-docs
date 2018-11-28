---
title: "Validating Data (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: microsoft-excel-add-in
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 71eda98f-01a4-4fff-8246-be3133782523
author: leolimsft
ms.author: lle
manager: craigg
---
# Validating Data (MDS Add-in for Excel)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], when you publish data, two types of validation take place:  
  
-   Any defined business rules are applied to the data.  
  
-   Data is checked against allowed attribute values (for example, number of characters or type of data).  
  
 In each case, valid data is published to the MDS repository. Data that is not valid is highlighted, and details of the error can be shown in status columns.  
  
## When Validation Occurs  
 In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], validation occurs when you publish new or changed data, or when you manually apply business rules.  
  
 When business rules fail, the data is still published to the MDS repository. When input validation fails, the data is not published to the repository.  
  
## Validation Statuses  
 In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], the following validation statuses are possible.  
  
 For information about additional statuses, see [Validation Statuses &#40;Master Data Services&#41;](../../master-data-services/validation-statuses-master-data-services.md)  
  
|Status|Description|  
|------------|-----------------|  
|Validation Failed|One or more values in the row failed validation against business rules defined by an MDS administrator.|  
|Validation Succeeded|All values in the row have passed validation against business rules.|  
  
## Input Statuses  
 In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], the following input statuses are possible  
  
|Status|Description|  
|------------|-----------------|  
|Error|One or more values in the row don't meet system requirements like length or data type. The value is not updated in the MDS repository.|  
|New Row|The values in the row have not yet been published to the MDS repository.|  
|Read Only|The logged in user has Read-only permissions to one or more values in the row and the value(s) cannot be updated.|  
|Unchanged|No values in the row have been changed in the worksheet. This does not mean the values in the repository have not changed; to get the latest data in the sheet, in the **Connect and Load** group, click **Load or Refresh**.<br /><br /> This is the default setting for each row.|  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Determine which values do not pass the defined business rules.|[Apply Business Rules &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/apply-business-rules-mds-add-in-for-excel.md)|  
|To help correct validation errors, view all transactions that have taken place for a member.|[View All Annotations or Transactions for a Member &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/view-all-annotations-or-transactions-for-a-member-mds-add-in-for-excel.md)|  
  
## Related Content  
  
-   [Overview: Importing Data from Excel &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/overview-importing-data-from-excel-mds-add-in-for-excel.md)  
  
  
