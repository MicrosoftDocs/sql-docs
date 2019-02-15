---
title: "Define Local Currency Reference (Business Intelligence Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.biwizard.currencyconversion.localcurrency.f1"
ms.assetid: 74993b0d-dfca-476b-acba-d66c593680a5
author: minewiskan
ms.author: owend
manager: craigg
---
# Define Local Currency Reference (Business Intelligence Wizard)
  Use the **Define Local Currency Reference** page to define the local currencies for currency conversion functionality that covers the many-to-many or many-to-one conversion types specified on the **Select Conversion Type** page. A local currency is the currency in which the transactions for measures selected in the **Select Measures** page are stored.  
  
> [!NOTE]  
>  This page does not appear if the Business Intelligence Wizard was started from Dimension Designer or by right-clicking a dimension in Solution Explorer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. This page also does not appear if **One-to-Many** was selected on the **Select Conversion Type** page.  
  
## Options  
 **Identifiers in the fact table**  
 Select to specify an attribute that provides currency identifiers for local currencies in a currency dimension referenced by the fact table that contains the measures selected on the **Select Measures** page. (A currency dimension in one whose `Type` property is set to *Currency*.)  
  
 Use this option when the transaction itself determines the local currency for that transaction. For example, in the [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] sample database-[!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)], the Internet Sales measure group has a regular dimension relationship to the Currency dimension. The fact table for that measure group contains a foreign key column that references the currency identifiers in the dimension table for that dimension.  
  
 **Currency dimension and attribute referenced by the fact data**  
 Select the currency attribute within a currency dimension whose members represent currency identifiers for local currencies. (A currency attribute is one whose `Type` property is set to *Currency*.)  
  
> [!NOTE]  
>  This option is not available if the **Identifiers in the fact table** option is not selected.  
  
 **Attributes in the dimension table**  
 Select to specify an attribute from a dimension related to the measure group that contains currency identifiers for local currencies.  
  
 Use this option when the relationship between a transaction and another business entity, such as a location, determines the local currency for that transaction. For example, in the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] sample database-[!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)], the Financial Reporting measure group has a referenced dimension relationship to the Currency dimension through the Organization dimension. That is, the fact table for the Financial Reporting measure group contains a foreign key column that references members in the dimension table for the Organization dimension. The dimension table for the Organization dimension, in turn, contains a foreign key column that references the currency identifiers in the dimension table for the Currency dimension.  
  
 **Dimension attribute that references currency**  
 Select the attribute within a dimension whose members reference the currency identifiers for local currency.  
  
> [!NOTE]  
>  This option is not available if the **Attributes in the dimension table** option is not selected.  
  
## See Also  
 [Business Intelligence Wizard F1 Help](business-intelligence-wizard-f1-help.md)   
 [Cube Designer &#40;Analysis Services - Multidimensional Data&#41;](cube-designer-analysis-services-multidimensional-data.md)   
 [Dimension Designer &#40;Analysis Services - Multidimensional Data&#41;](dimension-designer-analysis-services-multidimensional-data.md)  
  
  
