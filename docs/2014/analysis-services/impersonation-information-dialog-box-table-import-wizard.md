---
title: "Impersonation Information Dialog Box (Table Import Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.impersonationinfo.f1"
ms.assetid: bee7eee5-0650-41f1-a372-5076ae97a58c
author: minewiskan
ms.author: owend
manager: craigg
---
# Impersonation Information Dialog Box (Table Import Wizard)
  Use the **Impersonation Information** page to specify the credentials that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] will use to connect to the data source. For more information about credentials impersonation, see [Impersonation &#40;SSAS Tabular&#41;](tabular-models/impersonation-ssas-tabular.md).  
  
## Options  
 **Specific Windows user name and password**  
 Select this option to have the tabular model use the security credentials of a specified Windows user account.  
  
 **User name**  
 Type the domain and name of the user account to be used. Use the following format:  
  
 *\<Domain name>* **\\** *\<User account name>*  
  
 This option is enabled only if **Use a specific name and password** is selected.  
  
 **Password**  
 Type the password of the user account to be used by the Tabular model.  
  
 This option is enabled only if **Use a specific name and password** is selected.  
  
 **Service account**  
 Select this option to use the security credentials associated with the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] service that manages the model.  
  
## See Also  
 [Impersonation &#40;SSAS Tabular&#41;](tabular-models/impersonation-ssas-tabular.md)  
  
  
