---
title: "Staging Stored Procedure (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 6a613106-9f87-4caf-a23a-a726fc6561c5
author: leolimsft
ms.author: lle
manager: craigg
---
# Staging Stored Procedure (Master Data Services)
  When initiating the staging process from [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], you use one of three stored procedures.  
  
-   stg.udp_name_Leaf  
  
-   stg.udp_name_Consolidated  
  
-   stg.udp_name_Relationship  
  
 Where name is the name of the staging table that was specified when the entity was created.  
  
## Staging Process Stored Procedure Parameters  
 The following table lists the parameters of these stored procedures.  
  
|Parameter|Description|  
|---------------|-----------------|  
|**VersionName**<br /><br /> Required|The name of the version. This may or may not be case-sensitive, depending on your [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] collation setting.|  
|**LogFlag**<br /><br /> Required|Determines whether transactions are logged during the staging process. Possible values are:<br /><br /> **0**: Do not log transactions.<br />**1**: Log transactions.<br /><br /> <br /><br /> For more information about transactions, see [Transactions &#40;Master Data Services&#41;](transactions-master-data-services.md).|  
|**BatchTag**<br /><br /> Required, except by web service|The **BatchTag** value as specified in the staging table.|  
|**Batch_ID**<br /><br /> Required by web service only|The **Batch_ID** value as specified in the staging table.|  
  
### Staging Process Stored Procedure Example  
 The following example shows how to start the staging process by using the staging stored procedure.  
  
```  
USE [DATABASE_NAME]  
GO  
  
EXEC[stg].[udp_name_Leaf]  
      @VersionName = N'VERSION_1',  
@LogFlag = 1,  
@BatchTag = N'batch1'  
  
GO  
  
```  
  
## See Also  
 [Validation Stored Procedure &#40;Master Data Services&#41;](../../2014/master-data-services/validation-stored-procedure-master-data-services.md)   
 [Load or Update Members in Master Data Services by Using the Staging Process](add-update-and-delete-data-master-data-services.md)   
 [View Errors that Occur During the Staging Process &#40;Master Data Services&#41;](view-errors-that-occur-during-staging-master-data-services.md)  
  
  
