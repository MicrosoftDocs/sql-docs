---
title: "Copy a Policy-Based Management facet state to XML File"
description: Describes how to copy the state 0f a Policy-Based Management facet to an XML file using SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, copy facet state to XML file"
ms.assetid: 7d604ab1-6dd6-4f8e-a79c-eba99ab106fd
author: VanMSFT
ms.author: vanto
---
# Copy a Policy-Based Management Facet State to an XML File
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to how to copy the state of a Policy-Based Management facet to an XML file in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To copy a facet state to an XML file, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 The procedures in this topic require membership in the PolicyAdministratorRole role in the msdb database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To copy a facet state to an XML file  
  
1.  In Object Explorer, right-click an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], instance object, database, or database object, and then click **Facets**.  
  
2.  In the **View Facets -**_object_name_ dialog box, click **Export Current State as Policy**.  
  
3.  In the **Export as Policy** dialog box, type the path and name of the file; or use the Browse (**...**) button to locate the file, and then type the name of the XML file. For more information on the available options in this dialog box, see [Export As Policy Dialog Box](../../relational-databases/policy-based-management/export-as-policy-dialog-box.md)  
  
4.  When finished, click **OK**.  
  
  
