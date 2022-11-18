---
description: "Delete a Policy-Based Management Condition"
title: "Delete a Policy-Based Management Condition | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, delete policy conditions"
ms.assetid: e04148b8-f6dd-4c50-a5ef-c650b71dbf4d
author: VanMSFT
ms.author: vanto
---
# Delete a Policy-Based Management Condition
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to delete a Policy-based Management condition in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To delete a condition, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To delete a condition  
  
1.  In **Object Explorer**, click the plus sign to expand the server that contains the condition that you want to delete.  
  
2.  Click the plus sign to expand the **Management** folder.  
  
3.  Click the plus sign to expand **Policy Management**.  
  
4.  Click the plus sign to expand the **Conditions** folder.  
  
5.  Right-click the condition that you want to delete and select **Delete**.  
  
6.  In the **Delete Object** dialog box, ensure that the correct condition is selected and then click **OK**.  

