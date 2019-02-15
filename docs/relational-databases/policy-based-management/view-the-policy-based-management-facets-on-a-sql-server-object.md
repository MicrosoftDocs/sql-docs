---
title: "View the Policy-Based Management Facets on a SQL Server Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, view facets"
ms.assetid: 5f423b9f-a6c4-41a7-9d8d-8f4926ce1fb4
author: VanMSFT
ms.author: vanto
manager: craigg
---
# View the Policy-Based Management Facets on a SQL Server Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to view all of the Policy-Based Management facets applied to a specific SQL Server object in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To view all of the facets in an object, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view all of the facets in an object  
  
1.  In Object Explorer, right-click an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], instance object, database, or database object, and then click **Facets**.  
  
2.  In the **View Facets -**_object_name_ dialog box, in the **Facet** list, select a facet to view its properties. For more information on the available options in this dialog box, see [View Facets Dialog Box](../../relational-databases/policy-based-management/view-facets-dialog-box.md).  
  
3.  When finished, click **OK**.  
  
  
