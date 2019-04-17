---
title: "Working with Policy-Based Management Facets | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing Policy-Based Management facets"
  - "facets [Policy-Based Management], copying"
  - "facets [Policy-Based Management], viewing"
  - "copying Policy-Based Management facets"
ms.assetid: 88d025c4-07c2-4e4d-8634-204249a8c82c
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Working with Policy-Based Management Facets
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  A Policy-Based Management facet is a set of logical properties that are related to an area of management interest. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes several predefined facets. For example, the Surface Area Configuration facet defines, as properties, the features that are off by default.  
  
 When you manage many similar [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environments, you can configure a facet in one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], copy the state of the facet to a file, and then import that file into another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as a policy. When the state has been converted to a policy, the policy can be applied to other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], instance objects, databases, or database objects.  
  
 This topic describes how to copy the state of a facet to an XML file.  
  
##  <a name="BeforeYouBegin"></a> Permissions  
 The procedures in this topic require membership in the PolicyAdministratorRole role in the msdb database.  
  
## Viewing and Copying Facet States  
 [View the Policy-Based Management Facets on a SQL Server Object](../../relational-databases/policy-based-management/view-the-policy-based-management-facets-on-a-sql-server-object.md)  
  
 [Copy a Policy-Based Management Facet State to an XML File](../../relational-databases/policy-based-management/copy-a-policy-based-management-facet-state-to-an-xml-file.md)  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)  
  
  
