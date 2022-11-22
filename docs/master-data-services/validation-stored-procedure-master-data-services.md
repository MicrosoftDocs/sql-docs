---
title: Validation Stored Procedure
description: Learn how to use the mdm.udpValidateModel stored procedure to apply business rules to all members in the model version in Master Data Services.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 332d3c86-4440-4f12-a6cb-ffbfbccde52c
author: CordeliaGrey
ms.author: jiwang6
---
# Validation Stored Procedure (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], validate a version to apply business rules to all members in the model version.  
  
 This topic explains how to use the **mdm.udpValidateModel** stored procedure to validate data. If you are an administrator in the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application, you can do validation in the UI instead. For more information, see [Validate a Version against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-a-version-against-business-rules-master-data-services.md).  
  
> [!NOTE]  
>  If you invoke validation before the staging process is complete, members that have not finished staging will not be validated.  
  
## Example  
  
```  
DECLARE @ModelName nVarchar(50) = 'Customer'   
DECLARE @Model_id int   
DECLARE @UserName nvarchar(50)= 'DOMAIN\user_name'   
DECLARE @User_ID int   
DECLARE @Version_ID int   
  
SET @User_ID = (SELECT ID    
                 FROM mdm.tblUser u   
                 WHERE u.UserName = @UserName)   
SET @Model_ID = (SELECT Top 1 Model_ID   
                 FROM mdm.viw_SYSTEM_SCHEMA_VERSION   
                 WHERE Model_Name = @ModelName)   
SET @Version_ID = (SELECT MAX(ID)   
                 FROM mdm.viw_SYSTEM_SCHEMA_VERSION   
                 WHERE Model_ID = @Model_ID)  
  
EXECUTE mdm.udpValidateModel @User_ID, @Model_ID, @Version_ID, 1  
  
```  
  
## Parameters  
 The parameters of this procedure are as follows:  
  
|Parameter|Description|  
|---------------|-----------------|  
|UserID|The user ID.|  
|Model_ID|The model ID.|  
|Version_ID|The version ID.|  
  
## See Also  
 [Overview: Importing Data from Tables &#40;Master Data Services&#41;](../master-data-services/overview-importing-data-from-tables-master-data-services.md)   
 [Validate a Version against Business Rules &#40;Master Data Services&#41;](../master-data-services/validate-a-version-against-business-rules-master-data-services.md)  
  
  
