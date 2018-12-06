---
title: "Create a New Policy-Based Management Condition | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, creating policy conditions"
ms.assetid: 8a612f7e-6c70-49db-a4de-48431e097cc5
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Create a New Policy-Based Management Condition
  This topic describes how to create a Policy-based Management condition in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To create a condition, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a condition  
  
1.  In **Object Explorer**, click the plus sign to expand the server where you want to create a Policy-based Management condition.  
  
2.  Click the plus sign to expand the **Management** folder.  
  
3.  Click the plus sign to expand **Policy Management**.  
  
4.  Click the plus sign to expand the **Facets** folder.  
  
5.  Right-click the facet in which you want to create a new condition and select **New Condition**.  
  
6.  In the **Create New Condition** dialog box, in the **Name** box, type the name of the new condition.  
  
7.  Confirm the correct facet in the **Facet** list, or select a different facet.  
  
8.  Under **Expression**, construct condition expressions by selecting a facet property in the **Field** box, together with its associated operator and value. When you add multiple expressions, the expressions can be joined by using **And** or **Or**. For more information on the available options in this dialog box, see [Create New Condition or Open Condition Dialog Box, General Page](../../integration-services/general-page-of-integration-services-designers-options.md), [Create New Condition or Open Condition Dialog Box, Description Page](create-new-condition-or-open-condition-dialog-box-description-page.md), and [Advanced Edit &#40;Condition&#41; Dialog Box](advanced-edit-condition-dialog-box.md).  
  
9. When finished, click **OK**.  
  
  
