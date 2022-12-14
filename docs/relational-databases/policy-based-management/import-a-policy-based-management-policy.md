---
description: "Import a Policy-Based Management Policy"
title: "Import a Policy-Based Management Policy | Microsoft Docs"
ms.custom: ""
ms.date: 08/06/2020
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, import policy"
ms.assetid: 850b7ef9-d2b7-4754-bf04-7cb419ffb776
author: VanMSFT
ms.author: vanto
---
# Import a Policy-Based Management Policy
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to import a Policy-Based Management policy instance in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
## Permissions
 Requires membership in the PolicyAdministratorRole role in the msdb database.

  
##  Using SQL Server Management Studio  
  
### To import a policy instance  
  
1.  In **Object Explorer**, click the plus sign to expand the server where the newly-imported policy instance will reside.  
  
2.  Click the plus sign to expand the **Management** folder.  
  
3.  Click the plus sign to expand **Policy Management**.  
  
4.  Right-click the **Policies** folder and select **Import Policy**.  
  
5.  In the **Import** dialog box, type the path and name of the file; or use the Browse (**...**) button to locate the XML file that contains the policy, and then select the file. For more information on the available options in the **Import** dialog box, see [Import Policies Dialog Box](../../relational-databases/policy-based-management/import-policies-dialog-box.md).  
  
6.  When finished, click **OK**.  


## Example policies
 Example policies are available in the [SQL Server Samples code repository](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/epm-framework/sample-policies). These policies can be imported and used as a basis for your own policy-based management policies.
