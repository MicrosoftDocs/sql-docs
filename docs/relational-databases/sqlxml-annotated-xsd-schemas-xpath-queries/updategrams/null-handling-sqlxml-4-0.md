---
title: "NULL Handling (SQLXML)"
description: "Learn how NULL attributes or elements can be specified in an SQLXML 4.0 updategram by using the updg:nullvalue attribute."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
ms.custom: "seo-lt-2019"
ms.assetid: 5e11eebb-d94e-4ce6-a6d0-870225706bc1
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# NULL Handling (SQLXML 4.0)
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  XML syntax denotes NULL as an absence. (For example, if an attribute or element value is NULL, that attribute or element is absent from the XML document.) In [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML, the **updg:nullvalue** attribute enables specifying NULL for an element or attribute value.  
  
 For example, the following updategram ensures that the **Title** value for a contact with **ContactID** of 64 is NULL, and then updates the **Title** value to "Mr." for this contact.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
  <updg:sync updg:nullvalue="IsNULL"  >  
    <updg:before>  
       <Person.Contact ContactID="64" Title="IsNULL" />  
    </updg:before>  
    <updg:after>  
       <Person.Contact ContactID="64" Title="Mr." />  
    </updg:after>  
  </updg:sync>  
</ROOT>  
```  
  
 When parameters are passed to an updategram, NULL can be passed as the parameter value. This is done by specifying the **nullvalue** attribute in the **\<updg:header>** block. For an example, see [Passing Parameters to Updategrams &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/passing-parameters-to-updategrams-sqlxml-4-0.md).  
  
## See Also  
 [Updategram Security Considerations &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/security/updategram-security-considerations-sqlxml-4-0.md)  
  
  
