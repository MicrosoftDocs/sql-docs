---
title: "Currency Conversions in Analysis Services | Microsoft Docs"
ms.date: 05/09/2019
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Currency conversions in Analysis Services

[!INCLUDE[ssas-appliesto-sqlas-aas](../includes/ssas-appliesto-sqlas-aas.md)]

 Analysis Services uses a combination of features, guided by Multidimensional Expressions (MDX) scripts, to provide currency conversion support in cubes supporting multiple currencies.  
  
## Currency conversion terminology  

 The following terminology is used to describe currency conversion functionality:  
  
 **Pivot currency** - Is the currency against which exchange rates are entered in the rate measure group.  
  
 **Local currency** - Is the currency used to store transactions on which measures to be converted are based.  
  
Local currency can be identified by either:  
  
-   A currency identifier in the fact table stored with the transaction, as is commonly the case with banking applications where the transaction itself identifies the currency used for that transaction.  
  
-   A currency identifier associated with an attribute in a dimension table that is then associated with a transaction in the fact table, as is commonly the case in financial applications where a location or other identifier, such as a subsidiary, identifies the currency used for an associated transaction.  
  
**Reporting currency** - Is the currency to which transactions are converted from the pivot currency.  
  
> [!NOTE]  
>  For many-to-one currency conversions, the pivot currency and reporting currency are the same.  
  
 **Currency dimension** - A database dimension defined with the following settings:  
  
-   The **Type** property of the dimension is set to Currency.  
  
-   The **Type** property of one attribute for the dimension is set to CurrencyName.  
  
The values of this attribute must be used in all columns that should contain a currency identifier.  
  
**Rate measure group** - A measure group in a cube, defined with the following settings:  
  
-   A regular dimension relationship exists between a currency dimension and the rate measure group.  
  
-   A regular dimension relationship exists between a time dimension and the rate measure group.  
  
-   Optionally, the **Type** property is set to ExchangeRate. While the Business Intelligence Wizard uses the relationships with the currency and time dimensions to identify likely rate measure groups, setting the **Type** property to ExchangeRate allows client applications to more easily identify rate measure groups.  
  
-   One or more measures, representing the exchange rates contained by the rate measure group.  
  
**Reporting currency dimension** - Is the dimension, defined by the Business Intelligence Wizard after a currency conversion is defined, that contains the reporting currencies for that currency conversion. The reporting currency dimension is based on a named query, defined in the data source view on which the currency dimension associated with the rate measure group is based, from the dimension main table of the currency dimension. The dimension is defined with the following settings:  
  
-   The **Type** property of the dimension is set to Currency.  
  
-   The **Type** property of the key attribute for the dimension is set to CurrencyName.  
  
-   The **Type** property of one attribute within the dimension is set to CurrencyDestination, and the column bound to the attribute contains the currency identifiers that represent the reporting currencies for the currency conversion.  
  
## Defining currency conversions  

 You can use the Business Intelligence Wizard to define currency conversion functionality for a cube, or you can manually define currency conversions using MDX scripts.  
  
### Prerequisites  

 Before you can define a currency conversion in a cube using the Business Intelligence Wizard, you must first define at least one currency dimension, at least one time dimension, and at least one rate measure group. From these objects, the Business Intelligence Wizard can retrieve the data and metadata used to construct the reporting currency dimension and MDX script needed to provide currency conversion functionality.  
  
### Decisions  

 You need to make the following decisions before the Business Intelligence Wizard can construct the reporting currency dimension and MDX script needed to provide currency conversion functionality:  
  
-   Exchange rate direction  
  
-   Converted members  
  
-   Conversion type  
  
-   Local currencies  
  
-   Reporting currencies  
  
### Exchange rate directions  

 The rate measure group contains measures representing exchange rates between local currencies and the pivot currency (commonly referred to as the corporate currency). The combination of exchange rate direction and conversion type determines the operation performed on measures to be converted by the MDX script generated using the Business Intelligence Wizard. The following table describes the operations performed depending on the exchange rate direction and conversion type, based on the exchange rate direction options and conversion directions available in the Business Intelligence Wizard.  
  
|||||  
|-|-|-|-|  
|Exchange rate direction|**Many-to-one**|**One-to-many**|**Many-to-many**|  
|**n pivot currency to 1 sample currency**|Multiply the measure to be converted by the exchange rate measure for the local currency in order to convert the measure into the pivot currency.|Divide the measure to be converted by the exchange rate measure for the reporting currency in order to convert the measure into the reporting currency.|Multiply the measure to be converted by the exchange rate measure for the local currency in order to convert the measure into the pivot currency, then divide the converted measure by the exchange rate measure for the reporting currency in order to convert the measure into the reporting currency.|  
|**n sample currency to 1 pivot currency**|Divide the measure to be converted by the exchange rate measure for the local currency in order to convert the measure into the pivot currency.|Multiply the measure to be converted by the exchange rate measure for the reporting currency in order to convert the measure into the reporting currency.|Divide the measure to be converted by the exchange rate measure for the local currency in order to convert the measure into the pivot currency, then multiply the converted measure by the exchange rate measure for the reporting currency in order to convert the measure into the reporting currency.|  
  
 You choose the exchange rate direction on the **Set currency conversion options** page of the Business Intelligence Wizard. For more information about setting conversion direction, see [Set Currency Conversion Options &#40;Business Intelligence Wizard&#41;](http://msdn.microsoft.com/library/a49d4e1f-bdda-4a83-ab4f-ce8c500e1d6d).  
  
### Converted members  

 You can use the Business Intelligence Wizard to specify which measures from the rate measure group are used to convert values for:  
  
-   Measures in other measure groups.  
  
-   Members of an attribute hierarchy for an account attribute in a database dimension.  
  
-   Account types, used by members of an attribute hierarchy for an account attribute in a database dimension.  
  
 The Business Intelligence Wizard uses this information within the MDX script generated by the wizard to determine the scope of the currency conversion calculation. For more information about specifying members for currency conversion, see [Select Members &#40;Business Intelligence Wizard&#41;](http://msdn.microsoft.com/library/1a147461-d594-41e7-a41d-09d2d003e1e0).  
  
### Conversion types  

 The Business Intelligence Wizard supports three different types of currency conversion:  
  
-   **One-to-many**  
  
     Transactions are stored in the fact table in the pivot currency, and then converted to one or more other reporting currencies.  
  
     For example, the pivot currency can be set to United States dollars (USD), and the fact table stores transactions in USD. This conversion type converts these transactions from the pivot currency to the specified reporting currencies. The result is that transactions can be stored in the specified pivot currency and viewed either in the specified pivot currency or in any of the reporting currencies specified in the reporting currency dimension defined for the currency conversion.  
  
-   **Many-to-one**  
  
     Transactions are stored in the fact table in local currencies, and then converted into the pivot currency. The pivot currency serves as the only specified reporting currency in the reporting currency dimension.  
  
     For example, the pivot currency can be set to United States dollars (USD), and the fact table stores transactions in euros (EUR), Australian dollars (AUD), and Mexican pesos (MXN). This conversion type converts these transactions from their specified local currencies to the pivot currency. The result is that transactions can be stored in the specified local currencies and viewed in the pivot currency, which is specified in the reporting currency dimension defined for the currency conversion.  
  
-   **Many-to-many**  
  
     Transactions are stored in the fact table in local currencies. The currency conversion functionality converts such transactions into the pivot currency, and then to one or more other reporting currencies.  
  
     For example, the pivot currency can be set to United States dollars (USD), and the fact table stores transactions in euros (EUR), Australian dollars (AUD), and Mexican pesos (MXN). This conversion type converts these transactions from their specified local currencies to the pivot currency, and then the converted transactions are converted again from the pivot currency to the specified reporting currencies. The result is that transactions can be stored in the specified local currencies and viewed either in the specified pivot currency or in any of the reporting currencies that are specified in the reporting currency dimension defined for the currency conversion.  
  
 Specifying the conversion type allows the Business Intelligence Wizard to define the named query and dimension structure of the reporting currency dimension, as well as the structure of the MDX script defined for the currency conversion.  
  
### Local currencies  

 If you choose a many-to-many or many-to-one conversion type for your currency conversion, you need to specify how to identify the local currencies from which the MDX script generated by the Business Intelligence Wizard performs the currency conversion calculations. The local currency for a transaction in a fact table can be identified in one of two ways:  
  
-   The measure group contains a regular dimension relationship to the currency dimension. For example, in the [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)] sample [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, the Internet Sales measure group has a regular dimension relationship to the Currency dimension. The fact table for that measure group contains a foreign key column that references the currency identifiers in the dimension table for that dimension. In this case, you can select the attribute from the currency dimension that is referenced by the measure group to identify the local currency for transactions in the fact table for that measure group. This situation most often occurs in banking applications, where the transaction itself determines the currency used within the transaction.  
  
-   The measure group contains a referenced dimension relationship to the currency dimension, through another dimension that directly references the currency dimension. For example, in the [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)] sample [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] database, the Financial Reporting measure group has a referenced dimension relationship to the Currency dimension through the Organization dimension. The fact table for that measure group contains a foreign key column that references members in the dimension table for the Organization dimension. The dimension table for the Organization dimension, in turn, contains a foreign key column that references the currency identifiers in the dimension table for the Currency dimension. This situation most often occurs in financial reporting applications, where the location or subsidiary for a transaction determines the currency for the transaction. In this case, you can select the attribute that references the currency dimension from the dimension for the business entity.  
  
### Reporting currencies  

 If you choose a many-to-many or one-to-many conversion type for your currency conversion, you need to specify the reporting currencies for which the MDX script generated by the Business Intelligence Wizard performs the currency conversion calculations. You can either specify all the members of the currency dimension related to the rate measure group, or select individual members from the dimension.  
  
 The Business Intelligence Wizard creates a reporting currency dimension, based on a named query constructed from the dimension table for the currency dimension using the selected reporting currencies.  
  
> [!NOTE]  
>  If you select the one-to-many conversion type, a reporting currency dimension is also created. The dimension contains only one member representing the pivot currency, because the pivot currency is also used as the reporting currency for a one-to-many currency conversion.  
  
 A separate reporting currency dimension is defined for each currency conversion defined in a cube. You can change the name of the reporting currency dimensions after creation, but if you do so you must also update the MDX script generated for that currency conversion to ensure that the correct name is used by the script command when referencing the reporting currency dimension.  
  
## Defining multiple currency conversions  

 Using the Business Intelligence Wizard, you can define as many currency conversions as needed for your business intelligence solution. You can either overwrite an existing currency conversion or append a new currency conversion to the MDX script for a cube. Multiple currency conversions defined in a single cube provide flexibility in business intelligence applications that have complex reporting requirements, such as financial reporting applications that support multiple, separate conversion requirements for international reporting.  
  
### Currency conversion in multidimensional models by using Business Intelligence Wizard  
 
The Business Intelligence Wizard identifies each currency conversion by framing the script commands for the currency conversion in the following comments:  
  
 `//<Currency conversion>`  
  
 `...`  
  
 `[MDX statements for the currency conversion]`  
  
 `...`  
  
 `//</Currency conversion>`  
  
 If you change or remove these comments, the Business Intelligence Wizard is unable to detect the currency conversion, so you should not change these comments.  
  
 The wizard also stores metadata in comments within these comments, including the creation date and time, the user, and the conversion type. These comments should also not be changed because the Business Intelligence Wizard uses this metadata when displaying existing currency conversions.  
  
 You can change the script commands contained in a currency conversion as needed. If you overwrite the currency conversion, however, your changes will be lost.  
  
## See also  
 [Globalization scenarios for Analysis Services](../analysis-services/globalization-scenarios-for-analysis-services.md)  
  
  
