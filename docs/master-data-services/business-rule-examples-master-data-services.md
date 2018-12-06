---
title: "Business Rule Examples (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "01/05/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 3974b9be-4b7c-4a37-ab26-1a36ef455744
author: leolimsft
ms.author: lle
manager: craigg
---
# Business Rule Examples (Master Data Services)

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article shows examples of business rules for [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)]. You'll find these examples in the sample models that are included with your installation of [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)].   
  
For instructions on how to deploy the sample models, see [Master Data Services Installation and Configuration](../master-data-services/master-data-services-installation-and-configuration.md).  
  
  
## Business Rule Examples  
Sample Model |Entity  |Business Rule Name| Description  
---------|---------|---------|-----------|  
Customer    | Customer   | Person pmt terms| Specifies default payment terms for customers.          
In the following business rule, if the CustomerType attribute value meets the `is equal` [rule condition](../master-data-services/business-rule-conditions-master-data-services.md), then the `defaults to` [rule action](../master-data-services/business-rule-conditions-master-data-services.md) is applied to the PaymentTerms attribute. Otherwise no action is taken.  
```  
If  
    CustomerType is equal to 2  
Then  
    PaymentTerms defaults to CASH  
Else  
    None      
```  
  
**--------------------------------------------------**  
  
Sample Model  |Entity  |Business Rule Name|Description    
---------|---------|---------|---------------  
Customer     | Customer    | Org pmt terms | Specifies default payment terms for organizations.         
In the following business rule, if the CustomerType attribute value meets the `is equal` [rule condition](../master-data-services/business-rule-conditions-master-data-services.md), then the `defaults to` [rule action](../master-data-services/business-rule-actions-master-data-services.md) is applied to the PaymentTerms attribute. Otherwise no action is taken.  
```  
If  
    CustomerType is equal to 1  
Then  
    PaymentTerms defaults to 210Net30  
Else  
    None  
```  
  
**--------------------------------------------------**  
  
  
Sample Model  |Entity  |Business Rule Name| Description    
---------|---------|---------|-----------  
Product     |  Product       | DaysToManufacture |Specifies the range of days-to-manufacturing for in house manufacturing.          
In the following business rule, if the InHouseManufacture attribute value meets the `is equal` [rule condition](../master-data-services/business-rule-conditions-master-data-services.md), then the `must be between` [rule action](../master-data-services/business-rule-actions-master-data-services.md) is applied to the DaysToManufacture attribute. Otherwise no action is taken.  
```  
If  
    InHouseManufacture is equal to Y  
Then  
    DaysToManufacture must be between 1 and 10  
Else  
    None  
```  
  
**--------------------------------------------------**  
  
  
Sample Model  |Entity  |Business Rule Name|Description    
---------|---------|---------|-------------  
Product     |Product         |Required fields| Specifies the required attributes for the product entity members.           
In the following business rule, under all conditions the `is required` [validation action](../master-data-services/business-rule-actions-master-data-services.md) is taken for the specified attributes. The attribute values cannot be Null or blank.  
```  
If  
    None  
Then  
    Name is required  
    ProductSubCategory is required  
    Color is required  
    StandardCost is required  
    SafetyStockLevel is required  
    ReorderPoint is required  
    InHouseManufacture is required  
    SellStartDate is required  
    FinishedGoodIndicator is required  
    ProductLine is required  
Else  
    None  
```  
  
**--------------------------------------------------**  
  
  
Sample Model  |Entity  |Business Rule Name|Description    
---------|---------|---------|-----------  
Product     | Product        |  Std Cost| Requires that the standard cost is greater than 0.        
In the following business rule, under all conditions the `must be greater than` [rule action](../master-data-services/business-rule-actions-master-data-services.md) is applied to the StandardCost attribute of products.  
```  
If  
    None  
Then  
    StandardCost must be greater than 0  
Else  
    None  
```  
  
**--------------------------------------------------**  
  
  
Sample Model  |Entity  |Business Rule Name|Description    
---------|---------|---------|------------  
Product     | Product        | FG MSRP Cost|Specifies that if the product is a finished good, the MSRP (manufacturer suggested retail price) and dealer costs must be greater than 0.           
  
In the following business rule, if the FinishedGoodIndicator attribute value meets the `is equal` [rule condition](../master-data-services/business-rule-conditions-master-data-services.md), the `must be greater than` [rule action](../master-data-services/business-rule-actions-master-data-services.md) is applied to the MSRP and DealerCost attributes.  
```  
If  
    FinishedGoodIndicator is equal to Y  
Then  
    MSRP must be greater than 0  
    DealerCost must be greater than 0  
Else  
    None  
```  
  
**--------------------------------------------------**  
  
  
Sample Model  |Entity  |Business Rule Name|Description    
---------|---------|---------|------------  
Product     | Product        |  Default Name| Specifies the default product name based on the values of the Color and Class attributes. When the Color attribute value is not YLO and the Class attribute is not NA, the default name is Yellow NA.         
In the following business rule, if the Color and Class attributes do not meet the `is equal` rule condition, the `defaults to` [rule action](../master-data-services/business-rule-actions-master-data-services.md) is applied to the Name attribute.  
```  
If  
    (Color is equal to YLO AND Class is equal to NA) is not true  
Then  
    Name defaults to Yellow NA  
Else  
    Name defaults to Other  
```  
  
**--------------------------------------------------**  
  
  
**To view the business rule examples in the sample models**  
1. Navigate to the [!INCLUDE[ssMDSshort_md](../includes/ssmdsshort-md.md)] Web site that you set up after installing MDS, and click the **System Administration** box.   
For instructions on setting up the Web site, see [Master Data Services Installation and Configuration](../master-data-services/master-data-services-installation-and-configuration.md).  
2. Click the sample model that contains the business rule, as listed in the tables above, and then click **Entities**.  
3. Click the entity to which the rule applies, as listed in the tables above, and then click **Business Rules**.  
4. Click the name of the business rule that you want to view. The UI expands to show the **If**, **Then** and **Else** statements.  
  
 
  
  
  
  

