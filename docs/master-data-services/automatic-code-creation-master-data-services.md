---
description: "Automatic Code Creation (Master Data Services)"
title: Automatic Code Creation
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 9adbd5e1-f28c-4fb5-afa7-082de2831f3e
author: CordeliaGrey
ms.author: jiwang6
---
# Automatic Code Creation (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], numeric values can be automatically generated for the Code attribute, or for any other numeric attribute. When codes are generated automatically, you are not prevented from entering other values for codes; rather an initial value is automatically set.  
  
## Generating Code Values  
 Administrators can configure automatically-generated values for the Code attribute by editing the associated entity's properties. They can specify an initial value, and each subsequent value is increased by one.  
  
 When you enter Code values into MDS, either in one of the tools or by using the staging process, you can leave the Code value blank and a Code value will be automatically generated. Or you can specify a Code value of your choice.  
  
## Generating Other Attribute Values  
 Administrators can automatically generate values for attributes other than Code by creating business rules. They can specify an initial value, and specify the number each subsequent value is incremented by.  
  
 When you enter attribute values into MDS, either in one of the tools or by using the staging process, you can leave the attribute value blank. When business rules are applied, the values will be incremented based on the highest existing value. For example, if your rule is "Default attribute to a generated value that starts at 1 and increments by 4" and the highest current value for the attribute is 700, the value for the next member that's added will be 704.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Automatically generate values for the Code attribute.|[Automatically Generate Code Attribute Values &#40;Master Data Services&#41;](../master-data-services/automatically-generate-code-attribute-values-master-data-services.md)|  
|Automatically generate values for other attributes.|[Automatically Generate Attribute Values Other Than Code &#40;Master Data Services&#41;](../master-data-services/automatically-generate-attribute-values-other-than-code-master-data-services.md)|  
  
## Related Content  
  
-   [Master Data Services Overview &#40;MDS&#41;](../master-data-services/master-data-services-overview-mds.md)  
  
-   [Business Rules &#40;Master Data Services&#41;](../master-data-services/business-rules-master-data-services.md)  
  
-   [Entities &#40;Master Data Services&#41;](../master-data-services/entities-master-data-services.md)  
  
  
