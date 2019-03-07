---
title: "Transfer Error Messages Task Editor (Messages Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.transfererrormessagestask.errormessages.F1"
helpviewer_keywords: 
  - "Transfer Error Messages Task Editor"
ms.assetid: cb2226a0-3037-4fdf-966f-81eabc0da9cf
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Transfer Error Messages Task Editor (Messages Page)
  Use the **Messages** page of the **Transfer Error Messages Task Editor** dialog box to specify properties for copying one or more [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] user-defined error messages from one instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to another. For more information about this task, see [Transfer Error Messages Task](control-flow/transfer-error-messages-task.md).  
  
## Options  
 **SourceConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the source server.  
  
 **DestinationConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the destination server.  
  
 **IfObjectExists**  
 Select whether the task should overwrite existing user-defined error messages, skip existing messages, or fail if error messages of the same name already exist on the destination server.  
  
 **TransferAllErrorMessages**  
 Select whether the task should copy all or only the specified user-defined messages from the source server to the destination server.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|Copy all user-defined messages.|  
|**False**|Copy only the specified user-defined messages.|  
  
 **ErrorMessagesList**  
 Click the browse button **(...)** to select the error messages to copy.  
  
> [!NOTE]  
>  You must specify the **SourceConnection** before you can select error messages to copy.  
  
 **ErrorMessageLanguagesList**  
 Click the browse button **(...)** to select the languages for which to copy user-defined error messages to the destination server. A us_english (code page 1033) version of the message must exist on the destination server before you can transfer other language versions of the message to that server.  
  
> [!NOTE]  
>  You must specify the **SourceConnection** before you can select error messages to copy.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Integration Services Tasks](control-flow/integration-services-tasks.md)   
 [Transfer Error Messages Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [SMO Connection Manager](connection-manager/smo-connection-manager.md)   
 [Transfer Error Messages Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [SMO Connection Manager](connection-manager/smo-connection-manager.md)  
  
  
