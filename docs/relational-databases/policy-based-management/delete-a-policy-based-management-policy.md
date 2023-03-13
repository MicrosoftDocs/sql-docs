---
title: "Delete a Policy-Based Management Policy"
description: "Delete a Policy-Based Management Policy"
author: VanMSFT
ms.author: vanto
ms.date: "08/01/2016"
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "Policy-Based Management, delete policies"
---
# Delete a Policy-Based Management Policy
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to delete a Policy-Based Management policy in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To delete a policy, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To delete a policy  
  
1.  In Object Explorer, click the plus sign to expand the server that contains the Policy-Based Management policy that you want to delete.  
  
2.  Click the plus sign to expand the **Management** folder.  
  
3.  Click the plus sign to expand **Policy Management**.  
  
4.  Click the plus sign to expand the **Policies** folder.  
  
5.  Right-click the policy that you want to delete and select **Delete**.  
  
6.  In the **Delete Object** dialog box, ensure that the correct condition is selected and then click **OK**.  
  
  
