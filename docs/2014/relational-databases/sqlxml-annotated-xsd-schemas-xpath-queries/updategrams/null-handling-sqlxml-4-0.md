---
title: "NULL Handling (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "updg:nullvalue attribute"
  - "updategrams [SQLXML], null values"
  - "nullvalue attribute"
  - "null values [SQLXML]"
ms.assetid: 5e11eebb-d94e-4ce6-a6d0-870225706bc1
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# NULL Handling (SQLXML 4.0)
  XML syntax denotes NULL as an absence. (For example, if an attribute or element value is NULL, that attribute or element is absent from the XML document.) In [!INCLUDE[msCoName](../../../includes/msconame-md.md)] SQLXML, the `updg:nullvalue` attribute enables specifying NULL for an element or attribute value.  
  
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
  
 When parameters are passed to an updategram, NULL can be passed as the parameter value. This is done by specifying the `nullvalue` attribute in the `<updg:header>` block. For an example, see [Passing Parameters to Updategrams &#40;SQLXML 4.0&#41;](passing-parameters-to-updategrams-sqlxml-4-0.md).  
  
## See Also  
 [Updategram Security Considerations &#40;SQLXML 4.0&#41;](../security/updategram-security-considerations-sqlxml-4-0.md)  
  
  
