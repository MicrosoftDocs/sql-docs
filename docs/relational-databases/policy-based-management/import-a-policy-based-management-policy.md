---
title: "Import a Policy-Based Management Policy | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, import policy"
ms.assetid: 850b7ef9-d2b7-4754-bf04-7cb419ffb776
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Import a Policy-Based Management Policy
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to import a Policy-Based Management policy instance in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To import a policy instance, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ships with policies that can be used to monitor an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, these policies are not installed on the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], but they can be imported from the default location of C:\Program Files\Microsoft SQL Server\###\Tools\Policies\DatabaseEngine\1033 or C:\Program Files (x86)\Microsoft SQL Server\###\Tools\Policies\DatabaseEngine\1033 on 64-bit installs.
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the PolicyAdministratorRole role in the msdb database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To import a policy instance  
  
1.  In **Object Explorer**, click the plus sign to expand the server where the newly-imported policy instance will reside.  
  
2.  Click the plus sign to expand the **Management** folder.  
  
3.  Click the plus sign to expand **Policy Management**.  
  
4.  Right-click the **Policies** folder and select **Import Policy**.  
  
5.  In the **Import** dialog box, type the path and name of the file; or use the Browse (**...**) button to locate the XML file that contains the policy, and then select the file. For more information on the available options in the **Import** dialog box, see [Import Policies Dialog Box](../../relational-databases/policy-based-management/import-policies-dialog-box.md).  
  
6.  When finished, click **OK**.  
  
  
