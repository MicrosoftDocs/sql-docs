---
title: "Select Conversion Type (Business Intelligence Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.biwizard.currencyconversion.conversiontype.f1"
ms.assetid: 2c664138-e8a1-4c47-8e7d-ee01c57e4692
author: minewiskan
ms.author: owend
manager: craigg
---
# Select Conversion Type (Business Intelligence Wizard)
  Use the **Select Conversion Type** page to define the relationship between local currencies and reporting currencies for transactions stored in multiple currencies. A local currency is the currency in which the transactions for measures selected in the **Select Measures** page are stored. A reporting currency is the currency in which the transactions selected in the **Select Measures** page are translated.  
  
> [!NOTE]  
>  This page does not appear if the Business Intelligence Wizard was started from Dimension Designer or by right-clicking a dimension in Solution Explorer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
## Options  
 **Many-to-many**  
 Stores transactions using local currencies. The currency conversion functionality converts such transactions into the pivot currency specified in the **Set Currency Conversion Options** page, and then to one or more other reporting currencies.  
  
 For example, the pivot currency can be set to United States dollars (USD), and the fact table stores transactions in euros (EUR), Australian dollars (AUD), and Mexican pesos (MXN). Selecting this option converts these transactions from their specified local currencies to the pivot currency, and then the converted transactions are converted again from the pivot currency to the specified reporting currencies. The result is that transactions can be stored in the specified local currencies and viewed either in the specified pivot currency or in any one of the reporting currencies specified in the **Specify Reporting Currencies** page.  
  
 **Many-to-one**  
 Stores transactions using local currencies. The currency conversion functionality converts such transactions into the pivot currency specified in the **Set Currency Conversion Options** page. The pivot currency serves as the only specified reporting currency.  
  
> [!NOTE]  
>  If this option is selected, the **Specify Reporting Currencies** page does not appear.  
  
 For example, the pivot currency can be set to United States dollars (USD), and the fact table stores transactions in euros (EUR), Australian dollars (AUD), and Mexican pesos (MXN). Selecting this option converts these transactions from their specified local currencies to the pivot currency. The result is that transactions can be stored in the specified local currencies and viewed in the specified pivot currency.  
  
 **One-to-many**  
 Store transactions using the pivot currency specified in the **Set Currency Conversion Options** page, and then to one or more other reporting currencies.  
  
> [!NOTE]  
>  If this option is selected, the **Define Local Currency Reference** page does not appear.  
  
 For example, the pivot currency can be set to United States dollars (USD), and the fact table stores transactions in USD. Selecting this option converts these transactions from the pivot currency to the specified reporting currencies. The result is that transactions can be stored in the specified pivot currency and viewed either in the specified pivot currency or in any one of the reporting currencies specified in the **Specify Reporting Currencies** page.  
  
## See Also  
 [Business Intelligence Wizard F1 Help](business-intelligence-wizard-f1-help.md)   
 [Cube Designer &#40;Analysis Services - Multidimensional Data&#41;](cube-designer-analysis-services-multidimensional-data.md)   
 [Dimension Designer &#40;Analysis Services - Multidimensional Data&#41;](dimension-designer-analysis-services-multidimensional-data.md)  
  
  
